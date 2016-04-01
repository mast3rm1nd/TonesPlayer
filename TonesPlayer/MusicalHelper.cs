using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TonesPlayer
{
    class MusicalHelper
    {
        public static double CalculateToneFrequency(string musicalNoteNotation)
        {
            if (musicalNoteNotation.Length != 2 && musicalNoteNotation.Length != 3)
                return 0;                                           //length is incorrect

            int note = 0;

            musicalNoteNotation = musicalNoteNotation.ToLower();

            switch (musicalNoteNotation[0])                             //note index within octave
            {
                case 'c': note = 1; break;
                case 'd': note = 3; break;
                case 'e': note = 5; break;
                case 'f': note = 6; break;
                case 'g': note = 8; break;
                case 'a': note = 10; break;
                case 'h':
                case 'b': note = 12; break;
            }

            if (note == 0)
                return 0;                                           //note is incorrect


            int octave = -1;
            if (musicalNoteNotation.Length == 2)
                octave = (int)(musicalNoteNotation[1] - 48);
            else
            {
                if (musicalNoteNotation[1] == '#')
                    note++;
                else if (musicalNoteNotation[1] == 'b')
                    note--;
                else
                    return 0;                                      //wrong alteration sign

                octave = (int)(musicalNoteNotation[2] - 48);
            }


            if (octave < 0 || octave > 8)
                return 0;                                           //octave is incorrect

            int keyboard_key_index = (octave - 1) * 12 + note + 3;

            if (keyboard_key_index < 1 || keyboard_key_index > 88)
                return 0;                                          //notation format was correct, but there is no such note (=



            float a4_frequency = 440f;                             //key is 49


            var keys_diff = keyboard_key_index - 49;
            var cents_diff = keys_diff * 100f;
            var power_of_two = cents_diff / 1200f;
            var multiplier = Math.Pow(2, power_of_two);
            var result = multiplier * a4_frequency;

            return result;
        }
    }
}
