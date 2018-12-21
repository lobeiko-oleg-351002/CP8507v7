using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace CP8507_v7
{
    public abstract class Protocol
    {
        public Stopwatch stopWatch;
        public System.Timers.Timer noAnswer_Timer;
        protected MainForm mainForm;

        public Protocol()
        {
            noAnswer_Timer = new System.Timers.Timer();
            noAnswer_Timer.Elapsed += new System.Timers.ElapsedEventHandler(NoAnswer_Timer_Tick);
            noAnswer_Timer.Interval = 2000;
            noAnswer_Timer.Stop();
        }

        public abstract bool CheckProtocol(byte[] buffer);

        public abstract void ProcessPackage(byte[] buffer);

        public virtual void SendMessage(SerialPort comPort, string portName, byte[] buffer)
        {
            TryToOpenPort(comPort, portName);

            if (comPort.IsOpen)
            {
                try
                {
                    comPort.Write(buffer, 0, buffer.Length);
                    stopWatch = Stopwatch.StartNew();
                    noAnswer_Timer.Start();
                    mainForm.ColorStatusButton = Color.Transparent;
                }
                catch (Exception ex)
                {
                    //MainForm.WriteLog("Ошибка передачи данных: " + ex.Message + ex.InnerException + ex.Source + ex.StackTrace);
                   // MessageBox.Show("Ошибка передачи данных");
                    mainForm.StatusLabel = ProtocolGlobals.DATA_PASS_ON_ERROR_MESSAGE;
                }
            }
            else
            {
                MessageBox.Show("Ошибка открытия порта");
            }
        }

        protected virtual void SendMessageTCP(TcpClient client, byte[] buffer)
        {
            if (client.Connected)
            {
                try
                {
                    NetworkStream tcpStream = client.GetStream();
                    tcpStream.Write(buffer, 0, buffer.Length);
                    stopWatch = Stopwatch.StartNew();
                    noAnswer_Timer.Start();
                    mainForm.ColorStatusButton = Color.Transparent;
                }
                catch (Exception ex)
                {
                   // MainForm.WriteLog("Ошибка передачи данных: " + ex.Message + ex.InnerException + ex.Source + ex.StackTrace);
                    mainForm.StatusLabel = ProtocolGlobals.DATA_PASS_ON_ERROR_MESSAGE;
                   // MessageBox.Show("Ошибка передачи данных");
                }
            }
            else
            {
                MessageBox.Show("Соединение закрыто!");
            }
        }


        protected ushort CalculateCRC16(byte[] message)
        {
            byte uchCRCHi = 0xFF;         // high byte of CRC initialized
            byte uchCRCLo = 0xFF;         // low byte of CRC initialized
            UInt32 uIndex;                       // will index into CRC lookup table
            for (int i = 0; i < message.Length - 2; i++)
            {
                uIndex = (UInt32)(uchCRCHi ^ message[i]);      // calculate the CRC
                uchCRCHi = (byte)(uchCRCLo ^ auchCRCHi[uIndex]);
                uchCRCLo = auchCRCLo[uIndex];
            }
            return (ushort)(uchCRCHi << 8 | uchCRCLo);
        }

        protected bool CheckCRC(byte[] message)
        {
            ushort crcCalculated = CalculateCRC16(message);

            byte[] temp = new byte[2];
            temp[0] = message[message.Length - 1];
            temp[1] = message[message.Length - 2];
            ushort crcReceived = BitConverter.ToUInt16(temp, 0);

            if (crcCalculated == crcReceived) return true;
            else return false;
        }

    

        protected virtual float ConvertByteToFloat(byte[] buffer, int offset)
        {
            if (offset < buffer.Length && offset + 4 < buffer.Length)
            {
                byte[] floatBuffer = new byte[4];

                floatBuffer[0] = buffer[offset + 1];
                floatBuffer[1] = buffer[offset + 0];
                floatBuffer[2] = buffer[offset + 3];
                floatBuffer[3] = buffer[offset + 2];

                return BitConverter.ToSingle(floatBuffer, 0);
            }
            else
                return -1.0f;
        }

        static public void TryToOpenPort(SerialPort port, string portName)
        {
            if (port.PortName != portName || !port.IsOpen) // пытаемся открыть порт
            {
                try
                {
                    if (port.IsOpen) port.Close();
                    port.PortName = portName;
                    port.Open();
                }
                catch (Exception ex)
                {
                    MainForm.WriteLog("Ошибка открытия порта: " + ex.Message);
                }
            }
        }

        public bool TryToConnectTCP(string address, int port)
        {
            Regex regIP = new Regex(@"\b(([01]?\d?\d|2[0-4]\d|25[0-5])\.){3}([01]?\d?\d|2[0-4]\d|25[0-5])\b");
            if (!regIP.IsMatch(address))
            {
                MessageBox.Show("Неверный формат IP адреса");
                return false;
            }
            try
            {
                IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
                TcpConnectionInformation[] tcpConnections = ipProperties.GetActiveTcpConnections();
                if (tcpConnections != null && tcpConnections.Length > 0)
                {
                    for (int i = 0; i < tcpConnections.Length; i++)
                    {
                        if (tcpConnections[i].LocalEndPoint.Equals(mainForm.tcpClient.Client.LocalEndPoint)
                            && tcpConnections[i].RemoteEndPoint.Equals(mainForm.tcpClient.Client.RemoteEndPoint))
                        {
                            TcpState stateOfConnection = tcpConnections[i].State;
                            if (stateOfConnection == TcpState.Established)
                            {
                                if (((IPEndPoint)mainForm.tcpClient.Client.RemoteEndPoint).Port == port
                                    && ((IPEndPoint)mainForm.tcpClient.Client.RemoteEndPoint).Address.ToString() == address)
                                    return true;
                            }
                            mainForm.tcpClient.Close();
                            mainForm.tcpClient = new TcpClient();
                            break;
                        }
                    }
                }

                Exception ex = null;
                mainForm.tcpClient = new TcpClient();
                Thread thread = new Thread(() => TcpConnect(ref ex, mainForm.tcpClient, address, port));
                thread.Start();
                thread.Join(2000);
                thread.Abort();
                if (ex != null) throw ex;

                mainForm.AddIPToComboBox(address);
                mainForm.StatusLabel = "TCP соединение установлено";
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка установки TCP соединения");
              //  MainForm.WriteLog("Ошибка установки tcp соединения: " + ex.Message);
            }
            return false;
        }

        public void TcpConnect(ref Exception exception, TcpClient tcpClient, string address, int port)
        {
            try
            {
                tcpClient.Connect(address, port);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        }

        static public void TryToChangeBoudRate(SerialPort port, int baudRate)
        {
            try
            {
                bool portState = port.IsOpen;

                if (port.IsOpen) port.Close();
                if (port.BaudRate != baudRate) port.BaudRate = baudRate;
                

                if (portState)
                    Protocol.TryToOpenPort(port, port.PortName);
            }
            catch (Exception ex)
            {
                //mainForm.StatusLabel = "Ошибка смены скорости порта:";
               // MainForm.WriteLog("Ошибка смены скорости порта: " + ex.Message);
                MessageBox.Show("Ошибка смены скорости порта ");
            }
        }

        protected virtual void NoAnswer_Timer_Tick(object sender, EventArgs e) { }


        /* Table of CRC values for high–order byte */
        private readonly byte[] auchCRCHi = {
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
            0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01,
            0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81,
            0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01,
            0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
            0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01,
            0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
            0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01,
            0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
            0x40 };

        /* Table of CRC values for low–order byte */
        private readonly byte[] auchCRCLo = {
            0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7, 0x05, 0xC5, 0xC4,
            0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E, 0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09,
            0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD,
            0x1D, 0x1C, 0xDC, 0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
            0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32, 0x36, 0xF6, 0xF7,
            0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A,
            0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE,
            0x2E, 0x2F, 0xEF, 0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
            0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1, 0x63, 0xA3, 0xA2,
            0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F,
            0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB,
            0x7B, 0x7A, 0xBA, 0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
            0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0, 0x50, 0x90, 0x91,
            0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C,
            0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88,
            0x48, 0x49, 0x89, 0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
            0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83, 0x41, 0x81, 0x80,
            0x40 };

       
    }
}
