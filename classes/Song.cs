using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Audio_Controller.classes
{
    public class Song
    {
        public string Title { get; set; }
        public string Duration { get; set; }
        public string Path { get; set; }
        public ICommand PlayCommand { get; private set; }
        Globals globals = new Globals();
        private MediaPlayer mediaPlayer = new MediaPlayer(); // should this be here??

        public Song(string title, string duration, string path)
        {
            Title = title;
            Duration = duration;
            Path = path;

            PlayCommand = new RelayCommand(Play);
        }

        private void Play(object obj)
        {
            string songPath = Path;
            mediaPlayer.Open(new Uri(songPath));
            mediaPlayer.Play();
            globals.lastPlayed = this; // save last played
            globals.isPaused = false;
        }
    }
}
