using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using Audio_Controller;

namespace Audio_Controller {

    public static class PlayButtonBehavior
    {
        public static readonly DependencyProperty PlayCommandProperty =
            DependencyProperty.RegisterAttached("PlayCommand", typeof(ICommand), typeof(PlayButtonBehavior), new UIPropertyMetadata(null, OnPlayCommandChanged));

        public static ICommand GetPlayCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(PlayCommandProperty);
        }

        public static void SetPlayCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(PlayCommandProperty, value);
        }

        private static void OnPlayCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Button button)
            {
                button.Click += (sender, args) =>
                {
                    ICommand command = GetPlayCommand(button);
                    if (command != null && command.CanExecute(button.DataContext))
                    {
                        command.Execute(button.DataContext);
                    }
                };
            }
        }
    }
}