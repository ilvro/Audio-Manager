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
    public class Song : INotifyPropertyChanged
    {
        public Globals globals = App.GlobalsInstance;
        public ICommand PlayCommand { get; private set; }

        private SongPlayer songPlayer;
        public string Title { get; set; }
        public string Path { get; set; }
        public string OriginalDuration { get; set; }

        private static double totalDuration; // totalDuration is OriginalDuration but as a double instead of string 
        public double TotalDuration
        {
            get { return totalDuration; }
            set
            {
                if (totalDuration != value)
                {
                    totalDuration = value;
                    OnPropertyChanged(nameof(TotalDuration));
                }
            }
        }
        private static double currentPosition; // currentPosition is Duration but as a double instead of string 
        public double CurrentPosition
        {
            get { return currentPosition; }
            set
            {
                if (currentPosition != value)
                {
                    currentPosition = value;
                    OnPropertyChanged(nameof(CurrentPosition));
                }
            }
        }
        private TimeSpan playbackPosition;
        private string duration;

        public string Duration
        {
            get { return duration; }
            set
            {
                if (duration != value)
                {
                    duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }

        public Song(string title, string duration, string path, double totalduration, double currentposition)
        {
            Title = title;
            Duration = duration;
            OriginalDuration = Duration;
            Path = path;
            songPlayer = new SongPlayer();
            TotalDuration = totalduration;
            CurrentPosition = currentposition;
            PlayCommand = new RelayCommand(Play);

            // attach an event handler to update the duration as the song progresses
            songPlayer.PropertyChanged += OnSongPlayerPropertyChanged;
        }





        // change song speed: mediaPlayer.PlaybackSession.PlaybackRate = 2.0;
        private void Play(object obj)
        {
            if (!globals.currentlyPlaying.Contains(this))
            {
                songPlayer.PlaySong(this);
            }
            else
            {
                songPlayer.PauseSong(this);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnSongPlayerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentDuration" && songPlayer.CurrentSong == this)
            {
                // update the Duration property here based on the current position
                TimeSpan currentPosition = songPlayer.CurrentDuration;
                TimeSpan totalDuration = songPlayer.TotalDuration;
                Duration = $"{currentPosition:mm\\:ss} / {totalDuration:mm\\:ss}";
                TotalDuration = totalDuration.TotalSeconds;
                CurrentPosition = currentPosition.TotalSeconds;
                
            }

            // store the playback position whenever it changes
            if (e.PropertyName == "CurrentPosition" && songPlayer.CurrentSong == this)
            {
                playbackPosition = songPlayer.mediaPlayer.Position; // equivalent to currentPosition
            }
        }

        public TimeSpan GetPlaybackPosition()
        {
            return playbackPosition;
        }

        public void SetPlaybackPosition(TimeSpan position)
        {
            playbackPosition = position;
        }

        public class SmoothProgressBar
        {
            public static double GetSmoothValue(DependencyObject obj)
            {
                return (double)obj.GetValue(SmoothValueProperty);
            }

            public static void SetSmoothValue(DependencyObject obj, double value)
            {
                obj.SetValue(SmoothValueProperty, value);
            }

            public static readonly DependencyProperty SmoothValueProperty =
                DependencyProperty.RegisterAttached("SmoothValue", typeof(double), typeof(SmoothProgressBar), new PropertyMetadata(0.0, changing));

            private static void changing(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                var anim = new DoubleAnimation((double)e.OldValue, (double)e.NewValue, new TimeSpan(0, 0, 0, 0, 250));
                (d as ProgressBar).BeginAnimation(ProgressBar.ValueProperty, anim, HandoffBehavior.Compose);
            }
        }

        private void ProgressBar_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ProgressBar progressBar = (ProgressBar)sender;

                // Calculate the position where the user clicked as a percentage of the total width
                double clickPosition = e.GetPosition(progressBar).X / progressBar.ActualWidth;

                // Update the song's playback position based on the click position
                double newPlaybackPosition = clickPosition * songPlayer.CurrentSong.TotalDuration;
                songPlayer.CurrentSong.SetPlaybackPosition(TimeSpan.FromSeconds(newPlaybackPosition));

                // Update the MediaPlayer's position
                songPlayer.mediaPlayer.Position = TimeSpan.FromSeconds(newPlaybackPosition);
            }
        }

    }
}