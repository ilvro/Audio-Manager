﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using NAudio.Wave;
using Xabe.FFmpeg;

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
        private TimeSpan playbackPosition;
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


        // change song speed: mediaPlayer.PlaybackSession.PlaybackRate = 2.0;
        private void Play(object obj)
        {
            if (!globals.currentlyPlaying.Contains(this))
            {
                songPlayer.PlaySong(this);
            }
            else
            {
                songPlayer.PauseSong(this);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnSongPlayerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentDuration" && songPlayer.CurrentSong == this)
            {
                // Update the Duration property here based on the current position
                TimeSpan currentPosition = songPlayer.CurrentDuration;
                TimeSpan totalDuration = songPlayer.TotalDuration;
                Duration = $"{currentPosition:mm\\:ss} / {totalDuration:mm\\:ss}";
            }

            // Store the playback position whenever it changes
            if (e.PropertyName == "CurrentPosition" && songPlayer.CurrentSong == this)
            {
                playbackPosition = songPlayer.mediaPlayer.Position; // equivalent to currentPosition
            }
        }

        public TimeSpan GetPlaybackPosition()
        {
            return playbackPosition;
        }

        public void SetPlaybackPosition(TimeSpan position)
        {
            playbackPosition = position;
        }



    }
}