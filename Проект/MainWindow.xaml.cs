using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace InterfaceWindow
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //------------Адрес файла на сервере------------------
        private  string networkFileAddress = "Введите путь к файлу";

        public string NetworkFileAddress
        {
            get { return networkFileAddress; }
            set
            {
                if (networkFileAddress != value)
                {
                    networkFileAddress = value;
                    NotifyPropertyChanged("NetworkFileAddress");
                }
            }
        }
        //---------------Адрес службы-----------------------------
        private string serviceAddress = "http://nikom.us.to/WCFSERVICE/Service.svc";

        public string ServiceAddress
        {
            get { return serviceAddress; }
            set
            {
                if (serviceAddress != value)
                {
                    serviceAddress = value;
                    NotifyPropertyChanged("ServiceAddress");
                }
            }
        }
        //-------------------Реализация INotifyPropertyChanged --------------------------------------------------------------------------------------------------------------------------------------
        public event PropertyChangedEventHandler PropertyChanged;
        //-------------------------------
        /// <summary>
        /// Уведомить о изменениях свойства.
        /// </summary>
        /// <param name="info">Имя свойства</param>
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            if (networkFileAddress == "Введите путь к файлу" || networkFileAddress == "")
            {
                MessageBox.Show("Вы не ввели путь к файлу!");
                return;
            }

            InterfaceWindow.ProectServiceClient.ServiceClient client;
			InterfaceWindow.ProectServiceClient.ResultStatus result = new InterfaceWindow.ProectServiceClient.ResultStatus();
			try
			{
				client = ProxyMaker.GetProxy(ServiceAddress);
				client.Open();
				result = client.GetTXTFile(networkFileAddress);
				client.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка!Не удалось соединиться со службой!" + ex.Message, "Ошибка", MessageBoxButton.OK);
			}
			if (result.IsSuccess == false)
			{
				MessageBox.Show("Ошибка на сервере!" + result.ExceptionMessage, "Ошибка", MessageBoxButton.OK);
			}
			else if (result.IsSuccess == true)
			{
				TextFileTextBox.Text = result.textFILE;
			}
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow sWindow = new SettingsWindow(this);
            sWindow.Owner = this;
            sWindow.ShowDialog();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedFileName = "";

            Microsoft.Win32.OpenFileDialog OpenTXTFileDialog = new Microsoft.Win32.OpenFileDialog();
            OpenTXTFileDialog.Filter = "TXT файлы(*.txt)|*.txt" + "|Все файлы (*.*)|*.*";
            OpenTXTFileDialog.CheckFileExists = true;
            OpenTXTFileDialog.Multiselect = false;
            OpenTXTFileDialog.Title = "Выберите текстовый файл";

            if (OpenTXTFileDialog.ShowDialog(this) == true)
            {
                selectedFileName = OpenTXTFileDialog.FileName;
                SelectedFileNameTextBox.Text = OpenTXTFileDialog.FileName;
                if (OpenTXTFileDialog.FileName != "" && OpenTXTFileDialog.FileName != null)
                {
                    // Read the file as one string.
                    try
                    {
                        System.IO.StreamReader txtFileReader = new System.IO.StreamReader(selectedFileName);
                        TextFileTextBox.Text = txtFileReader.ReadToEnd();
                        txtFileReader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при открытии файла!\n" + ex.Message);
                    }
                }
            }
        }
        //-----------------------------------------------------------------------------
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            TextFileTextBox.Text = "";
            this.SelectedFileNameTextBox.Text = "Введите путь к файлу";
        }

       //----------------------------------------------------------------------------

        private void DefaultButton_Click(object sender, RoutedEventArgs e)
        {
            this.SelectedFileNameTextBox.Text = "C:\\Inetpub\\nikom.us.to\\WCFSERVICE\\книги.txt";
        }
    }
}
