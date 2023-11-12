using System;
using System.ComponentModel;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using NAudio.Wave;

namespace Audio_Controller.classes
{
    public class SongPlayer : INotifyPropertyChanged
    {
        public Globals globals = App.GlobalsInstance;
        public MediaPlayer mediaPlayer = new MediaPlayer();
        private DispatcherTimer timer;
        public event EventHandler<TimeSpan> PositionChanged;

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
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        public void PlaySong(Song song)
        {
            if (song != null)
            {
                CurrentSong = song;
                globals.currentlyPaused.Remove(song);
                globals.currentlyPlaying.Add(song);
                mediaPlayer.Open(new Uri(song.Path));
                // restore the playback position if the song was paused
                if (song.GetPlaybackPosition() != TimeSpan.Zero)
                {
                    mediaPlayer.Position = song.GetPlaybackPosition();
                }
                mediaPlayer.Play();
                timer.Start();
            }
        }

        public void PauseSong(Song song)
        {
            if (song != null)
            {
                CurrentSong = song;

                // store the playback position when pausing
                song.SetPlaybackPosition(mediaPlayer.Position);

                globals.currentlyPaused.Add(song);
                globals.currentlyPlaying.Remove(song);
                mediaPlayer.Pause();
                timer.Stop();
               
            }
        }

        

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                TimeSpan currentPosition = mediaPlayer.Position;
                TotalDuration = mediaPlayer.NaturalDuration.TimeSpan; // update TotalDuration
                CurrentDuration = currentPosition; // update CurrentDuration
                

                // check if the song has ended
                if (currentPosition >= TotalDuration)
                {
                    mediaPlayer.Stop();
                    timer.Stop();
                    CurrentSong.Duration = CurrentSong.OriginalDuration;
                }

                // raise the PositionChanged event with the updated position
                PositionChanged?.Invoke(this, currentPosition);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}