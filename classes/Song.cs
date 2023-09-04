using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using NAudio.Wave;

namespace Audio_Controller.classes
{
    public class Song : INotifyPropertyChanged
    {
        Globals globals = App.GlobalsInstance;
        public ICommand PlayCommand { get; private set; }

        private SongPlayer songPlayer;
        public string Title { get; set; }
        public string Path { get; set; }
        public string OriginalDuration { get; set; }
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
       

        public Song(string title, string duration, string path, SongPlayer player)
        {
            Title = title;
            Duration = duration;
            OriginalDuration = Duration;
            Path = path;
            songPlayer = new SongPlayer();

            PlayCommand = new RelayCommand(Play);

            // attach an event handler to update the duration as the song progresses
            songPlayer.PropertyChanged += OnSongPlayerPropertyChanged;
        }

        private void Play(object obj)
        {
            globals.currentlyPlaying.Add(this);
            songPlayer.PlaySong(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnSongPlayerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentDuration")
            {
                // update the Duration property here based on the current position
                // calculate the new duration string and assign it to Duration
                TimeSpan currentPosition = songPlayer.CurrentDuration;
                TimeSpan totalDuration = songPlayer.TotalDuration;
                Duration = $"{currentPosition:mm\\:ss} / {totalDuration:mm\\:ss}"; // displaying it this way may cause a time discrepancy
            }
        }

    }
}