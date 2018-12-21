using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;


namespace CP8507_v7
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
           // try
           // {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                StartLogo st = new StartLogo();
                st.Show();
                Application.DoEvents();

                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
                Application.Run(new MainForm(st));
           // }
           // catch(Exception ex)
           // {
           //     MessageBox.Show(ex.Message);
           // }
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assemblyName = new AssemblyName(args.Name).Name;
            if (assemblyName == "System.Windows.Forms.DataVisualization")
            {
                using (var stream = typeof(Program).Assembly.GetManifestResourceStream("CP8507_v7." + assemblyName + ".dll"))
                {
                    byte[] assemblyData = new byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            }
            else if (assemblyName == "Microsoft.Office.Interop.Excel")
            {
                using (var stream = typeof(Program).Assembly.GetManifestResourceStream("CP8507_v7." + assemblyName + ".dll"))
                {
                    byte[] assemblyData = new byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            }
            else
            {
                return null;
            }
        }
    }
}
