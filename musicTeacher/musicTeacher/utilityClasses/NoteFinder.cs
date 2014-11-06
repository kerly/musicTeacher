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
        /// Creates a concrete instance of a pattern based on the baseNote and patternDefinition
        /// </summary>
        /// <param name="baseNote"></param>
        /// <param name="patternDefinition"></param>
        /// <returns></returns>
        public static APlayablePattern generateConcretePattern(MusicNote baseNote, APatternDefinition patternDefinition)
        {
            APlayablePattern result = null;
            
            // Create the final end object based on patternDefinition type
            Type type = patternDefinition.GetType();
            if(type == typeof(PatternDefinitionChord))
            {
                result = new MusicChord(baseNote, (PatternDefinitionChord)patternDefinition);
            }
            else if (type == typeof(PatternDefinitionScale))
            {
                result = new MusicScale(baseNote, (PatternDefinitionScale)patternDefinition);
            }
            else if (type == typeof(PatternDefinitionInterval))
            {
                result = new MusicInterval(baseNote, (PatternDefinitionInterval)patternDefinition);
            }

            return result;
        }

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

        public static void PlayNoteByName(String name)
        {
            MusicNote note = findNoteByName(name);
            if (note != null)
            {
                note.Play();
            }
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
            return MusicDefinitions.allMusicNotes.Find(i => i.getName().Equals(noteName));
        }

        /// <summary>
        /// Finds a piano button by it's note's name
        /// </summary>
        /// <param name="noteName"></param>
        /// <returns></returns>
        public static Button findPianoButtonByNoteName(String noteName, List<Button> buttons)
        {
            return buttons.Find(i => i.Text.Equals(noteName));
        }

        /// <summary>
        /// Generates a pattern based on the names of the identifiers
        /// </summary>
        /// <param name="noteName"></param>
        /// <param name="patternName"></param>
        /// <returns></returns>
        public static APlayablePattern getPatternByNames(String noteName, String patternName, List<APatternDefinition> patterns)
        {
            MusicNote baseNote = findNoteByName(noteName);
            APatternDefinition definition = findPatternDefinitionByName(patternName, patterns);
            return generateConcretePattern(baseNote, definition);
        }

        /// <summary>
        /// Finds a pattern definition by it's name
        /// </summary>
        /// <param name="patternName"></param>
        /// <param name="definitions"></param>
        /// <returns></returns>
        public static APatternDefinition findPatternDefinitionByName(String patternName, List<APatternDefinition> definitions)
        {
            return definitions.Find(i => i.getName().Equals(patternName));
        }
    }
}
