using System.Windows.Input;
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
        }

        private void Play(object obj)
        {
            songPlayer.PlaySong(this);
        }
    }
}