using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Audio_Controller.classes
{
    public class Globals : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Song lastPlayed { get; set; }
        private bool _isPaused;

        public bool isPaused
        {
            get
            {
                return _isPaused;
            }
            set
            {
                _isPaused = value;
                OnPropertyChanged(nameof(isPaused));
            }
        }

        public PackIconMaterialKind PausePlayIcon
        {
            get { return this.isPaused ? PackIconMaterialKind.Play : PackIconMaterialKind.Pause; }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
    