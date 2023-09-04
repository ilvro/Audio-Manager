using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using NAudio.Wave;

namespace Audio_Controller.classes
{
    public class SongPlayer : INotifyPropertyChanged
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private DispatcherTimer timer;

        private Song currentSong;
        public Song CurrentSong
        {
            get { return currentSong; }
            set
            {
                if (currentSong != value)
                {
                    currentSong = value;
                    OnPropertyChanged(nameof(CurrentSong));
                }
            }
        }

        private TimeSpan currentDuration;
        public TimeSpan CurrentDuration
        {
            get { return currentDuration; }
            set
            {
                if (currentDuration != value)
                {
                    currentDuration = value;
                    OnPropertyChanged(nameof(CurrentDuration));
                }
            }
        }

        private TimeSpan totalDuration;
        public TimeSpan TotalDuration
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

        public event PropertyChangedEventHandler PropertyChanged;

        public SongPlayer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Update every second
            timer.Tick += Timer_Tick;
        }

        public void PlaySong(Song song)
        {
            if (song != null)
            {
                CurrentSong = song;
                mediaPlayer.Open(new Uri(song.Path));
                mediaPlayer.Play();
                timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                TimeSpan currentPosition = mediaPlayer.Position;
                TotalDuration = mediaPlayer.NaturalDuration.TimeSpan; // Update TotalDuration

                CurrentDuration = currentPosition; // Update CurrentDuration

                // Check if the song has ended
                if (currentPosition >= TotalDuration)
                {
                    mediaPlayer.Stop();
                    timer.Stop();
                    CurrentSong.Duration = CurrentSong.OriginalDuration;
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}