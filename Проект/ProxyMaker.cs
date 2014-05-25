using System.ServiceModel;
using System;
using System.Windows;

namespace InterfaceWindow
{
    public class ProxyMaker
    {
        // ----   ----   ----   ----   ----   ----   ----   ----    ----   ----   ----   ----   ----   ----
        /// <summary>
        /// Создание посредника 
        /// </summary>
		static public InterfaceWindow.ProectServiceClient.ServiceClient GetProxy(string serviceAddress)
        {
            var client = new ProectServiceClient.ServiceClient();
            client.Endpoint.Address = new EndpointAddress(serviceAddress);
			return client;
        }

        // ----   ----   ----   ----   ----   ----   ----   ----    ----   ----   ----   ----   ----   ----
        /// <summary>
        /// Проверка существования службы по указанному адресу 
        /// </summary>
        /// <param name="Uri">проверяемый адрес службы</param>
        /// <returns></returns>
       // static public bool CheckServiceAddress(string checkedUri)
		static public bool CheckServiceAddress(string serviceAddress)
        {
            try
            {
               // EndpointAddress endpointAddress = new EndpointAddress(checkedUri);
                ProectServiceClient.ServiceClient client = new ProectServiceClient.ServiceClient();
                client.Endpoint.Address = new EndpointAddress(serviceAddress);
				//InterfaceWindow.ProectServiceClient.ProectLibraryServiceClient client = new InterfaceWindow.ProectServiceClient.ProectLibraryServiceClient("BasicHttpBinding_IService", endpointAddress);
				client.Open();
				ProectServiceClient.ConnectionStatus result = client.Test();
                client.Close();
                return result.IsSuccess;
            }
			
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка!Нет соединения со службой!"+ex.Message, "Ошибка",MessageBoxButton.OK);
			}
            return false;
        }
        // ----   ----   ----   ----   ----   ----   ----   ----    ----   ----   ----   ----   ----   ----

    }

}

