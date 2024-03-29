﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Audio_Controller.classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Xabe.FFmpeg;
using FFMpegCore;
using Path = System.IO.Path;
using System.Collections.ObjectModel;
using NAudio.Wave;
using System.Diagnostics;
using System.Windows.Data;
using System.ComponentModel;

namespace Audio_Controller.pages
{
    /// <summary>
    /// Interaction logic for tracks.xaml
    /// </summary>
    /// 
    public partial class tracks : Page
    {
        Globals globals = App.GlobalsInstance;
        private SongPlayer songPlayer;
        public List<Song> songs = new List<Song>();

        public tracks()
        {
            InitializeComponent();
            updateFileList();
            songPlayer = new SongPlayer();
            DataContext = this;
            this.Resources.Add("SongPlayerResource", songPlayer);


            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 5); // 5 seconds
            timer.Start();
        }

        private void searchBar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (searchBar.Text == " Begin by typing to choose tracks by title...")
            {
                searchBar.Text = "";
                searchBar.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void searchBar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (searchBar.Text == "")
            {
                searchBar.Text = " Begin by typing to choose tracks by title...";
                searchBar.Foreground = new SolidColorBrush(Colors.DarkGray);
            }
        }

        private void FileUpload_BtnClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool response = (bool)openFileDialog.ShowDialog();
            if (response == true)
            {
                string filePath = openFileDialog.FileName;
                string fileName = Path.GetFileName(filePath);
                FileUpload_Btn.Content = "Uploading " + fileName + "...";
                saveUploadedFile(filePath, fileName);
            }
        }

        private void FileUpload_BtnDrop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string fileName = Path.GetFileName(files[0]);
                string filePath = Path.GetDirectoryName(files[0]) + "\\" + fileName; ;
                FileUpload_Btn.Content = "Uploading " + fileName + "...";
                saveUploadedFile(filePath, fileName);

            }
        }

        public void saveUploadedFile(string path, string fileName)
        {
            string uploadFolder = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\tracks\\" + fileName;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.WorkingDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            startInfo.Arguments = "/C ffmpeg -i \"" + path + "\" \"tracks\\" + fileName.Substring(0, fileName.Length - 4) + ".mp3\"";
            process.StartInfo = startInfo;
            process.Start();
            updateFileList();
            FileUpload_Btn.Content = "Choose a file or drag it here to upload."; // reset the text

        }

        public void updateFileList()
        {
            songs.Clear();

            string currentPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            if (currentPath.Contains("bin\\Debug"))
            {
                currentPath = currentPath.Replace("bin\\Debug", "");
            }

            string[] mp3Files = Directory.GetFiles(currentPath + @"tracks\", "*.mp3"); // get all mp3 files in the folder

            foreach (string filePath in mp3Files)
            {
                string fileName = Path.GetFileName(filePath).Replace(".mp3", "");
                try
                {
                    Mp3FileReader reader = new Mp3FileReader(filePath);
                    TimeSpan duration = reader.TotalTime;
                    Song song = new Song(fileName, duration.ToString("mm\\:ss"), filePath, 0, 0);

                    if (duration.ToString("mm\\:ss").StartsWith("0"))
                    {
                        song = new Song(fileName, duration.ToString("m\\:ss"), filePath, 0, 0);
                    }

                    if (songs != null)
                    {
                        songs.Add(song);
                    }
                }
                catch
                {
                    MessageBox.Show("currently in use by another process - unable to add a new song");
                }
            }

            // the List<Song> "songs" now contains all the songs in the "tracks" folder

            TracksView.ItemsSource = songs;
        }

        private void LinkUploadBtn_Click(object sender, RoutedEventArgs e)
        {
            linkInput window = new linkInput();
            window.Show();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (globals.currentlyPlaying.Count == 0)
            {
                updateFileList();
            }
            
        }

        private static T FindVisualChild<T>(DependencyObject parent, string name) where T : FrameworkElement
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T frameworkElement && frameworkElement.Name == name)
                {
                    return frameworkElement;
                }
                T childOfChild = FindVisualChild<T>(child, name);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }
            return null;
        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TracksView != null && TracksView.ItemsSource != null && searchBar.Text != " Begin by typing to choose tracks by title...")
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(TracksView.ItemsSource);

                if (view != null)
                {
                    // apply filtering logic based on the search bar text
                    view.Filter = item =>
                    {
                        if (item is Song song)
                        {
                            return string.IsNullOrEmpty(searchBar.Text) || song.Title.StartsWith(searchBar.Text, StringComparison.OrdinalIgnoreCase);
                        }
                        return false;
                    };
                }
            }
        }
    }
}