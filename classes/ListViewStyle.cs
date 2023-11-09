using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using NAudio.Wave;
using Xabe.FFmpeg;

namespace Audio_Controller.classes
{
    public partial class ListViewStyle : ResourceDictionary
    {
        private void ProgressBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ProgressBar progressBar = (ProgressBar)sender;
                Song song = (Song)progressBar.DataContext;
                SongPlayer songPlayer = song.songPlayer;

                double clickPosition = e.GetPosition(progressBar).X / progressBar.ActualWidth;
                double newPlaybackPosition = clickPosition * songPlayer.CurrentSong.TotalDuration;
                songPlayer.CurrentSong.SetPlaybackPosition(TimeSpan.FromSeconds(newPlaybackPosition));
                song.SetPlaybackPosition(TimeSpan.FromSeconds(newPlaybackPosition));
                songPlayer.mediaPlayer.Position = TimeSpan.FromSeconds(newPlaybackPosition);
            }
        }
    }
}