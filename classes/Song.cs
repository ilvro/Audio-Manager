using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio_Controller.classes
{
    public class Song
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Duration { get; set; }

        public Song(string title, string artist, string album, string duration)
        {
            Title = title;
            Artist = artist;
            Album = album;
            Duration = duration;
        }
    }
}
