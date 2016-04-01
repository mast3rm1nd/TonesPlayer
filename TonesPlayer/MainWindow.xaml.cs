using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TonesPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            notesPlayer.PlayingStopped += NotesPlayer_PlayingStopped;

            //NotesPlayer.PlayNotes(System.IO.File.ReadAllText("Max Payne.txt"));
        }

        private void NotesPlayer_PlayingStopped(object sender, EventArgs e)
        {
            isPlaying = false;
            Dispatcher.Invoke(new Action(delegate
            {
                PlayStop_button.Content = "Play";
            }));
            
        }

        static NotesPlayer notesPlayer = new NotesPlayer();
        

        bool isPlaying = false;
        private void PlayStop_button_Click(object sender, RoutedEventArgs e)
        {
            if(isPlaying)
            {
                notesPlayer.Stop();
                PlayStop_button.Content = "Play";
            }                
            else
            {
                notesPlayer.PlayNotes(Notes_textBox.Text);
                PlayStop_button.Content = "Stop";
            }
                

            isPlaying = !isPlaying;
        }
    }
}
