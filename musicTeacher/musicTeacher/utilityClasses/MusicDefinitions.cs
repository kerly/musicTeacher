using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace musicTeacher
{
    static class MusicDefinitions
    {
        // Constants
        public const int NUM_OF_NOTES = 12;
        public const int NUM_OF_PIANO_KEYS = 36;
        public const int UNISON = 0;
        public const int MINOR_SECOND = 1;
        public const int MAJOR_SECOND = 2;
        public const int MINOR_THIRD = 3;
        public const int MAJOR_THIRD = 4;
        public const int PERFECT_FOURTH = 5;
        public const int TRITONE = 6;
        public const int PERFECT_FIFTH = 7;
        public const int MINOR_SIXTH = 8;
        public const int MAJOR_SIXTH = 9;
        public const int MINOR_SEVENTH = 10;
        public const int MAJOR_SEVENTH = 11;
        public const int OCTAVE = 12;

        // Static variables
        public static List<Button> allPianoButtons;
        public static List<Button> allNoteChoiceButtons;
        public static List<Button> allChordButtons;
        public static List<Button> allScaleButtons;
        public static List<Button> allIntervalButtons;
        public static List<MusicNote> allMusicNotes;
        public static List<PatternDefinitionChord> allChordDefinitions;
        public static List<PatternDefinitionScale> allScaleDefinitions;
        public static List<PatternDefinitionInterval> allIntervalDefinitions;

        /// <summary>
        /// Wrapper function that sets all the definitions for this application
        /// </summary>
        public static void initDefinitions()
        {
            allMusicNotes = createAllMusicNotes();
            allChordDefinitions = createAllChordDefinitions();
            allScaleDefinitions = createAllScaleDefinitions();
            allIntervalDefinitions = createAllIntervalDefinitions();
        }

        // Static methods
        /// <summary>
        /// Creates all MusicNote objects
        /// </summary>
        /// <returns>List of all MusicNotes</returns>
        private static List<MusicNote> createAllMusicNotes()
        {
            List<MusicNote> result = new List<MusicNote>();

            // Add all music note objects here
            //result.Add(new MusicNote("C2", "SOUNDFILE.WAV", 36));
            //result.Add(new MusicNote("C#2", "SOUNDFILE2.WAV", 37));

            return result;
        }

        private static List<PatternDefinitionChord> createAllChordDefinitions()
        {
            List<PatternDefinitionChord> result = new List<PatternDefinitionChord>();

            result.Add(new PatternDefinitionChord("Major Chord", new List<int> { 0, 4, 7 }, new List<int> { 1, 3, 5 }));
            result.Add(new PatternDefinitionChord("Minor Chord", new List<int> { 0, 3, 7 }, new List<int> { 1, 3, 5 }));

            return result;
        }

        private static List<PatternDefinitionInterval> createAllIntervalDefinitions()
        {
            List<PatternDefinitionInterval> result = new List<PatternDefinitionInterval>();

            result.Add(new PatternDefinitionInterval("Minor 2nd", MINOR_SECOND, new List<int> { 1, 2 }));
            result.Add(new PatternDefinitionInterval("Major 2nd", MAJOR_SECOND, new List<int> { 1, 2 }));
            result.Add(new PatternDefinitionInterval("Minor 3rd", MINOR_THIRD, new List<int> { 1, 3 }));
            result.Add(new PatternDefinitionInterval("Major 3rd", MAJOR_THIRD, new List<int> { 1, 3 }));
            result.Add(new PatternDefinitionInterval("Perfect 4th", PERFECT_FOURTH, new List<int> { 1, 3 }));
            result.Add(new PatternDefinitionInterval("Tritone", TRITONE, new List<int> { 1, 3 }));
            result.Add(new PatternDefinitionInterval("Perfect 5th", PERFECT_FIFTH, new List<int> { 1, 4 }));
            result.Add(new PatternDefinitionInterval("Minor 6th", MINOR_SIXTH, new List<int> { 1, 4 }));
            result.Add(new PatternDefinitionInterval("Major 6th", MAJOR_SIXTH, new List<int> { 1, 4 }));
            result.Add(new PatternDefinitionInterval("Minor 7th", MINOR_SEVENTH, new List<int> { 1, 5 }));
            result.Add(new PatternDefinitionInterval("Major 7th", MAJOR_SEVENTH, new List<int> { 1, 5 }));
            result.Add(new PatternDefinitionInterval("Octave", OCTAVE, new List<int> { 1, 5 }));

            return result;
        }

        private static List<PatternDefinitionScale> createAllScaleDefinitions()
        {
            List<PatternDefinitionScale> result = new List<PatternDefinitionScale>();

            result.Add(new PatternDefinitionScale("Major Scale", new List<int> { 0, 2, 4, 5, 7, 9, 11, 12 }, new List<int> { 1, 2, 3, 4, 5, 1, 2, 3 }));
            result.Add(new PatternDefinitionScale("Minor Scale", new List<int> { 0, 2, 3, 5, 7, 9, 11, 12 }, new List<int> { 1, 2, 3, 4, 5, 1, 2, 3 }));

            return result;
        }
    }
}
