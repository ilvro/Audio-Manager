﻿using System;
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

<<<<<<< HEAD
namespace Audio_Controller.pages
=======
namespace Audio_Manager.pages
>>>>>>> 99b28e728fe314328daff63fc435a15fd877dfcf
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

<<<<<<< HEAD
        private void searchBar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (searchBar.Text == " Begin by typing to choose tracks by title...")
            {
                searchBar.Text = "";
            }
        }

        private void searchBar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (searchBar.Text == "")
            {
                searchBar.Text = " Begin by typing to choose tracks by title...";
            }
        }
=======
        private void searchBar_GotMouseCapture(object sender, MouseEventArgs e)
        {
            searchBar.Text = "";
        }

        private void searchBar_LostFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

>>>>>>> 99b28e728fe314328daff63fc435a15fd877dfcf
    }
}
