using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CP8507_v7
{
    public static class ProtocolGlobals
    {
        public const string ETHERNET_TAG = "Ethernet";
        public const string RS485_TAG = "RS-485";
        public const string NO_RESPONSE_MESSAGE = "Нет ответа от устройства";
        public const string OUT_OF_FIRST_RECORD = "Выход за пределы первой записи";
        public const string DATE_TIME_FORMAT = "dd.MM.yyyy  HH:mm:ss";
        public const string CRC_ERROR_MESSAGE = "Ошибка контрольной суммы";
        public const string RECORDING_DONE_MESSAGE = "Информация записана";
        public const string OPERATION_SUCCESS_MESSAGE = "Операция завершена успешно";
        public const string INCORRECT_DEVICE_ADDRESS_MESSAGE = "Некорректно введен адрес устройства";
        public const string INCORRECT_DATA_MESSAGE = "Данные введены некорректно!";
        public const string OFF_BUTTON = "Отключение";
        public const string ON_BUTTON = "Включить";
        public const string DATA_PASSED_ON_MESSAGE = "Данные переданы";
        public const string INFO_SAVED = "Информация сохранена";
        public const string DATA_PASS_ON_ERROR_MESSAGE = "Ошибка передачи данных";
    }
}
