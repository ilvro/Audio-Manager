using System;
using System.ComponentModel;
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

        private string playbackPosition;
        public string PlaybackPosition
        {
            get { return playbackPosition; }
            set
            {
                if (playbackPosition != value)
                {
                    playbackPosition = value;
                    OnPropertyChanged(nameof(PlaybackPosition));
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
                TimeSpan totalDuration = mediaPlayer.NaturalDuration.TimeSpan;
                PlaybackPosition = $"{currentPosition:mm\\:ss} / {totalDuration:mm\\:ss}";

                // Check if the song has ended
                if (currentPosition >= totalDuration)
                {
                    mediaPlayer.Stop();
                    timer.Stop();
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
