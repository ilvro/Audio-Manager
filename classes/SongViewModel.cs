using Audio_Controller.classes;
using System.ComponentModel;

namespace Audio_Controller.classes
{
    public class SongViewModel : INotifyPropertyChanged
    {
        private Song song;

        public Song Song
        {
            get { return song; }
            set
            {
                if (song != value)
                {
                    song = value;
                    OnPropertyChanged(nameof(Song));
                }
            }
        }

        public SongViewModel()
        {
        }

        // Properties that expose data from the Song class
        public string Title => Song.Title;
        public string Duration => Song.Duration;
        public string Path => Song.Path;
        public double TotalDuration => Song.TotalDuration;
        public double CurrentPosition => Song.CurrentPosition;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}



