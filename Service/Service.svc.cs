using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
   [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class Service : IService
    {
        public ResultStatus GetTXTFile(string adress)
        {
            ResultStatus result = new ResultStatus();
            result.IsSuccess = true;
            result.ExceptionMessage = "";

            try
            {
                System.IO.StreamReader txtFileReader = new System.IO.StreamReader(adress);
                result.textFILE = txtFileReader.ReadToEnd();
                txtFileReader.Close();
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.ExceptionMessage = "Произошла ошибка при открытии файла на службе!\n " + e.Message;
            }
            return result;
        }

        public ConnectionStatus Test()
        {
            ConnectionStatus result = new ConnectionStatus();
            result.IsSuccess = true;
            return result;
        }
    }
}
