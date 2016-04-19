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

namespace Connection
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Page
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConnectDB foo = new ConnectDB();
            System.Data.DataTable dt = new System.Data.DataTable();
            //foo.transactInsertOrUpdate("INSERT ESTADO FROM trefpuesto WHERE CODPUESTO = k").ToString();
            //dt = foo.executeQuery("SELECT * FROM trefpuesto WHERE CODPUESTO = 3");
            //mallaDatos.DataContext = dt.DefaultView;
           // foo.returnValue("SELECT * FROM trefpuesto WHERE CODPUESTO = 915");
           foo.write();
        }




    }
}
