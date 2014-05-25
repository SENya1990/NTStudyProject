using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IService
    {

        [OperationContract]
        ResultStatus GetTXTFile(string value);
        
        [OperationContract]
        ConnectionStatus Test();
    }


    // Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    [DataContract]
    public class ResultStatus
    {
        [DataMember]
        public string textFILE { get; set; }

        [DataMember]
        public bool IsSuccess { get; set; }

        [DataMember]
        public string ExceptionMessage { get; set; }
    }

    [DataContract]
    public class ConnectionStatus
    {
        [DataMember]
        public bool IsSuccess { get; set; }

        [DataMember]
        public string ExceptionMessage { get; set; }
    }
}
