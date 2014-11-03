using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace musicTeacher
{
    static class NoteFinder
    {

        /// <summary>
        /// Translates a pattern definition into a list of music notes by finding all 
        /// notes by their base note and the next interval
        /// </summary>
        /// <param name="baseNote"></param>
        /// <param name="patternDefinition"></param>
        /// <returns></returns>
        public static List<MusicNote> translatePattern(MusicNote baseNote, APatternDefinition patternDefinition)
        {
            List<MusicNote> result = new List<MusicNote>();

            foreach(int interval in patternDefinition.getIntervals()) {
                result.Add(getNextNote(baseNote, interval));
            }

            return result;
        }

        /// <summary>
        /// Get's the next note by adding the base note and the interval
        /// </summary>
        /// <param name="baseNote"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static MusicNote getNextNote(MusicNote baseNote, int interval)
        {
            return findNoteByMidiNumber(baseNote.getMidiNumber() + interval);
        }

        /// <summary>
        /// Finds a MusicNote by it's midiNumber
        /// </summary>
        /// <param name="midiNumber"></param>
        /// <returns></returns>
        public static MusicNote findNoteByMidiNumber(int midiNumber)
        {
            return MusicDefinitions.allMusicNotes.Find(i => i.getMidiNumber() == midiNumber);
        }

        /// <summary>
        /// Finds a MusicNote by it's name
        /// </summary>
        /// <param name="noteName"></param>
        /// <returns></returns>
        public static MusicNote findNoteByName(String noteName)
        {
            return MusicDefinitions.allMusicNotes.Find(i => i.getName() == noteName);
        }

        /// <summary>
        /// Finds a piano button by it's note's name
        /// </summary>
        /// <param name="noteName"></param>
        /// <returns></returns>
        public static Button findPianoButtonByNoteName(String noteName)
        {
            return MusicDefinitions.allPianoButtons.Find(i => i.Text == noteName);
        }
    }
}
