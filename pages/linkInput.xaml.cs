using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Audio_Controller.pages
{
    /// <summary>
    /// Interaction logic for linkInput.xaml
    /// </summary>
    public partial class linkInput : Window
    {
        public linkInput()
        {
            InitializeComponent();
        }

        private void LinkDownloadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void URLTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            URLTextBox.Text = "";
        }
    }
}
