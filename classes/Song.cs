using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using NAudio.Wave;

namespace Audio_Controller.classes
{
    public class Song
    {
        public string Title { get; set; }
        public string Duration { get; set; }
        public string Path { get; set; }

        public ICommand PlayCommand { get; private set; }

        private SongPlayer songPlayer;

        public Song(string title, string duration, string path, SongPlayer player)
        {
            Title = title;
            Duration = duration;
            Path = path;
            songPlayer = new SongPlayer();

            PlayCommand = new RelayCommand(Play);

            // Attach an event handler to update the duration as the song progresses
            songPlayer.PropertyChanged += OnSongPlayerPropertyChanged;
        }

        private void Play(object obj)
        {
            songPlayer.PlaySong(this);
        }

        private void OnSongPlayerPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentDuration")
            {
                // Update the Duration property here based on the current position
                // Calculate the new duration string and assign it to Duration
                TimeSpan currentPosition = songPlayer.CurrentDuration;
                TimeSpan totalDuration = songPlayer.TotalDuration;
                Duration = $"{currentPosition:mm\\:ss} / {totalDuration:mm\\:ss}";
                MessageBox.Show(Duration);
            }
        }
    }
}
