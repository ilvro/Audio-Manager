﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio_Controller.classes
{
    public class Song
    {
        public string Title { get; set; }
        public string Duration { get; set; }

        public Song(string title, string duration)
        {
            Title = title;
            Duration = duration;
        }
    }
}
