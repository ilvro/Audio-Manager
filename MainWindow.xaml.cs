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

namespace Audio_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Audio Manager";
        }

        private void TrackBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("clicked on track");
        }

        private void PlaylistBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("clicked on playlist");
        }
    }
}
