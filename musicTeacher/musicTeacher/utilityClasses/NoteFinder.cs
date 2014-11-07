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
        public static Button findPianoButtonByNoteName(String noteName, List<String> noteNames, List<Button> buttons)
        {
            return buttons.ElementAt(noteNames.IndexOf(noteName));
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

        /// <summary>
        /// Generates a random pattern based on the type required (Chord, Scale or Interval)
        /// </summary>
        /// <param name="patternType"></param>
        /// <returns></returns>
        public static APlayablePattern getRandomPattern(String patternType)
        {
            APlayablePattern result = null;

            // Find a random pattern
            APatternDefinition definition = null;
            MusicNote note = null;
            Random random = new Random();
            if (patternType.Contains("Chord"))
            {
                int randomNumber = random.Next(0, MusicDefinitions.allChordDefinitions.Count - 1);
                definition = MusicDefinitions.allChordDefinitions.ElementAt(randomNumber);
                note = getRandomNoteInBounds(definition);
                result = new MusicChord(note, (PatternDefinitionChord)definition);
            }
            else if (patternType.Contains("Scale"))
            {
                int randomNumber = random.Next(0, MusicDefinitions.allScaleDefinitions.Count - 1);
                definition = MusicDefinitions.allScaleDefinitions.ElementAt(randomNumber);
                note = getRandomNoteInBounds(definition);
                result = new MusicScale(note, (PatternDefinitionScale)definition);
            }
            else if (patternType.Contains("Interval"))
            {
                int randomNumber = random.Next(0, MusicDefinitions.allIntervalDefinitions.Count - 1);
                definition = MusicDefinitions.allIntervalDefinitions.ElementAt(randomNumber);
                note = getRandomNoteInBounds(definition);
                result = new MusicInterval(note, (PatternDefinitionInterval)definition);
            }

            return result;
        }

        /// <summary>
        /// Finds a random note that will fit within the bounds of the pattern and notes
        /// </summary>
        /// <param name="definition"></param>
        /// <returns></returns>
        public static MusicNote getRandomNoteInBounds(APatternDefinition definition)
        {
            MusicNote result = null;

            Random random = new Random();
            do
            {
                int randomNoteIndex = random.Next(0, MusicDefinitions.allMusicNotes.Count - 1);
                result = MusicDefinitions.allMusicNotes.ElementAt(randomNoteIndex);
            } while (!isInBounds(result, definition.getNoteRange()));

            return result;
        }

        /// <summary>
        /// Finds a random note in the current octave
        /// </summary>
        /// <param name="definition"></param>
        /// <param name="octave"></param>
        /// <returns></returns>
        public static MusicNote getRandomNoteInBoundsForOctave(APatternDefinition definition, int octave)
        {
            MusicNote result = null;

            Random random = new Random();
            do
            {
                int randomNoteIndex = random.Next(0, MusicDefinitions.allMusicNotes.Count - 1);
                result = MusicDefinitions.allMusicNotes.ElementAt(randomNoteIndex);
            } while (!isInBounds(result, definition.getNoteRange()) && !result.getName().Contains(octave.ToString()));

            return result;
        }

        /// <summary>
        /// Checks if the next note is within the bounds of the highest note
        /// </summary>
        /// <param name="note"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static bool isInBounds(MusicNote note, int interval)
        {
            bool result = true;

            if (note.getMidiNumber() + interval > MusicDefinitions.highestMidiNumber)
            {
                result = false;
            }

            return result;
        }
    }
}
