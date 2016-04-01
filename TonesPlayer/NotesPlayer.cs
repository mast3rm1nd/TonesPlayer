using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Text.RegularExpressions;

namespace TonesPlayer
{
    class NotesPlayer
    {
        static Thread PlayingThread;
        static List<Note> NotesToPlay = new List<Note>();

        void PlayAllSequence()
        {
            foreach(var note in NotesToPlay)
            {
                Console.Beep((int)note.Frequency, note.Duration);

                Thread.Sleep(note.Pause);
                //Thread.Sleep(10);
            }

            OnPlayingStopped(null);
        }

        public void Stop()
        {
            if(PlayingThread != null)
            if (PlayingThread.IsAlive)
                PlayingThread.Abort();

            OnPlayingStopped(null);
        }

        public event EventHandler PlayingStopped;

        protected virtual void OnPlayingStopped(EventArgs e)
        {
            EventHandler handler = PlayingStopped;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void PlayNotes(string notes)
        {
            var notesList = notes.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var note in notesList)
                if (!IsNotationHasValidFormat(note))
                    return;

            NotesToPlay.Clear();

            foreach (var note in notesList)
                NotesToPlay.Add(new Note(note));

            PlayingThread = new Thread(new ThreadStart(PlayAllSequence));
            PlayingThread.IsBackground = true;
            PlayingThread.Start();
        }

        static string notationFormatRegex = "(?<Note>[a-hA-H][b#]*\\d*)-(?<Duration>\\d+)-(?<Pause>\\d+)";
        private static bool IsNotationHasValidFormat(string notation)
        {
            notation = notation.ToLower();

            return Regex.IsMatch(notation, notationFormatRegex);
        }

        class Note
        {
            public double Frequency { get;}
            public int Duration { get;}
            public int Pause { get;}

            public Note(string notation)
            {
                var match = Regex.Match(notation, notationFormatRegex);

                Frequency = MusicalHelper.CalculateToneFrequency(match.Groups["Note"].Value);
                Duration = int.Parse(match.Groups["Duration"].Value);
                Pause = int.Parse(match.Groups["Pause"].Value);
            }
        }
    }
}
