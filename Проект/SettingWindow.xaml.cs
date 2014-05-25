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
using System.Windows.Shapes;

namespace InterfaceWindow
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
		bool changesAccepted = false;
        MainWindow owner;
        
        //----------------------------------------------------------------
        public SettingsWindow(MainWindow own)
        {
            InitializeComponent();
            owner = own;
			ServicePathNameTextBox.Text = owner.ServiceAddress;
        }
		//-----------------------------------------------------------------
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {	
			changesAccepted = true;     
            this.Close();
        }
        //--------------------------------------------------------------
		private void Window_Closed(object sender, EventArgs e)
		{
			if (changesAccepted == true)
                owner.ServiceAddress = ServicePathNameTextBox.Text;
		}
        //--------------------------------------------------------------
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServicePathNameTextBox.Text == "")
            {
                MessageBox.Show("Введите адрес службы!");
                return;
            }

            if (ProxyMaker.CheckServiceAddress(ServicePathNameTextBox.Text) == true)
                MessageBox.Show("Есть соединение со службой");
            else
                MessageBox.Show("Нет соединения со службой");
        }
        //--------------------------------------------------------------
        private void DefaultButton_Click(object sender, RoutedEventArgs e)
        {
            ServicePathNameTextBox.Text = "http://nikom.us.to/WCFSERVICE/Service.svc";
        }
    }
}
