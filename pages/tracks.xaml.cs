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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Audio_Manager.pages
{
    /// <summary>
    /// Interaction logic for tracks.xaml
    /// </summary>
    public partial class tracks : Page
    {
        public tracks()
        {
            InitializeComponent();
        }

        private void searchBar_GotMouseCapture(object sender, MouseEventArgs e)
        {
            searchBar.Text = "";
        }

        private void searchBar_LostFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

    }
}
