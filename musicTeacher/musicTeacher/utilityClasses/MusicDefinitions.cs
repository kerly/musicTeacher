using System;
using System.Collections.Generic;
using System.IO;
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
        public static int lowestMidiNumber;
        public static int highestMidiNumber;
        public static List<MusicNote> allMusicNotes;
        public static List<PatternDefinitionChord> allChordDefinitions;
        public static List<PatternDefinitionScale> allScaleDefinitions;
        public static List<PatternDefinitionInterval> allIntervalDefinitions;
        public static Dictionary<Char, String> pianoKeyMap;

        /// <summary>
        /// Wrapper function that sets all the definitions for this application
        /// </summary>
        public static void initDefinitions()
        {
            allMusicNotes = createAllMusicNotes(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\audio files\\Piano notes");
            allChordDefinitions = createAllChordDefinitions();
            allScaleDefinitions = createAllScaleDefinitions();
            allIntervalDefinitions = createAllIntervalDefinitions();
            pianoKeyMap = createPianoKeyMap();
        }

        // Static methods
        /// <summary>
        /// Creates all MusicNote objects
        /// </summary>
        /// <returns>List of all MusicNotes</returns>
        private static List<MusicNote> createAllMusicNotes(String baseFolder)
        {
            List<MusicNote> result = new List<MusicNote>();

            // Add all music note objects here
            int midiNumber = 36;
            lowestMidiNumber = midiNumber;
            result.Add(new MusicNote("C2", baseFolder + "\\C2.wav", midiNumber++));
            result.Add(new MusicNote("C#2", Path.Combine(baseFolder, "C#2.wav"), midiNumber++));
            result.Add(new MusicNote("D2", Path.Combine(baseFolder, "D2.wav"), midiNumber++));
            result.Add(new MusicNote("D#2", Path.Combine(baseFolder, "D#2.wav"), midiNumber++));
            result.Add(new MusicNote("E2", Path.Combine(baseFolder, "E2.wav"), midiNumber++));
            result.Add(new MusicNote("F2", Path.Combine(baseFolder, "F2.wav"), midiNumber++));
            result.Add(new MusicNote("F#2", Path.Combine(baseFolder, "F#2.wav"), midiNumber++));
            result.Add(new MusicNote("G2", Path.Combine(baseFolder, "G2.wav"), midiNumber++));
            result.Add(new MusicNote("G#2", Path.Combine(baseFolder, "G#2.wav"), midiNumber++));
            result.Add(new MusicNote("A2", Path.Combine(baseFolder, "A2.wav"), midiNumber++));
            result.Add(new MusicNote("A#2", Path.Combine(baseFolder, "A#2.wav"), midiNumber++));
            result.Add(new MusicNote("B2", Path.Combine(baseFolder, "B2.wav"), midiNumber++));
            result.Add(new MusicNote("C3", Path.Combine(baseFolder, "C3.wav"), midiNumber++));
            result.Add(new MusicNote("C#3", Path.Combine(baseFolder, "C#3.wav"), midiNumber++));
            result.Add(new MusicNote("D3", Path.Combine(baseFolder, "D3.wav"), midiNumber++));
            result.Add(new MusicNote("D#3", Path.Combine(baseFolder, "D#3.wav"), midiNumber++));
            result.Add(new MusicNote("E3", Path.Combine(baseFolder, "E3.wav"), midiNumber++));
            result.Add(new MusicNote("F3", Path.Combine(baseFolder, "F3.wav"), midiNumber++));
            result.Add(new MusicNote("F#3", Path.Combine(baseFolder, "F#3.wav"), midiNumber++));
            result.Add(new MusicNote("G3", Path.Combine(baseFolder, "G3.wav"), midiNumber++));
            result.Add(new MusicNote("G#3", Path.Combine(baseFolder, "G#3.wav"), midiNumber++));
            result.Add(new MusicNote("A3", Path.Combine(baseFolder, "A3.wav"), midiNumber++));
            result.Add(new MusicNote("A#3", Path.Combine(baseFolder, "A#3.wav"), midiNumber++));
            result.Add(new MusicNote("B3", Path.Combine(baseFolder, "B3.wav"), midiNumber++));
            result.Add(new MusicNote("C4", Path.Combine(baseFolder, "C4.wav"), midiNumber++));
            result.Add(new MusicNote("C#4", Path.Combine(baseFolder, "C#4.wav"), midiNumber++));
            result.Add(new MusicNote("D4", Path.Combine(baseFolder, "D4.wav"), midiNumber++));
            result.Add(new MusicNote("D#4", Path.Combine(baseFolder, "D#4.wav"), midiNumber++));
            result.Add(new MusicNote("E4", Path.Combine(baseFolder, "E4.wav"), midiNumber++));
            result.Add(new MusicNote("F4", Path.Combine(baseFolder, "F4.wav"), midiNumber++));
            result.Add(new MusicNote("F#4", Path.Combine(baseFolder, "F#4.wav"), midiNumber++));
            result.Add(new MusicNote("G4", Path.Combine(baseFolder, "G4.wav"), midiNumber++));
            result.Add(new MusicNote("G#4", Path.Combine(baseFolder, "G#4.wav"), midiNumber++));
            result.Add(new MusicNote("A4", Path.Combine(baseFolder, "A4.wav"), midiNumber++));
            result.Add(new MusicNote("A#4", Path.Combine(baseFolder, "A#4.wav"), midiNumber++));
            result.Add(new MusicNote("B4", Path.Combine(baseFolder, "B4.wav"), midiNumber));
            highestMidiNumber = midiNumber;

            return result;
        }

        /// <summary>
        /// Creates the chord definitions
        /// </summary>
        /// <returns></returns>
        private static List<PatternDefinitionChord> createAllChordDefinitions()
        {
            List<PatternDefinitionChord> result = new List<PatternDefinitionChord>();

            result.Add(new PatternDefinitionChord("Major Chord", new List<int> { 0, 4, 7 }, new List<int> { 1, 3, 5 }));
            result.Add(new PatternDefinitionChord("Minor Chord", new List<int> { 0, 3, 7 }, new List<int> { 1, 3, 5 }));

            return result;
        }

        /// <summary>
        /// Creates the interval definitions
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Create the scale definitions
        /// </summary>
        /// <returns></returns>
        private static List<PatternDefinitionScale> createAllScaleDefinitions()
        {
            List<PatternDefinitionScale> result = new List<PatternDefinitionScale>();

            result.Add(new PatternDefinitionScale("Major Scale", new List<int> { 0, 2, 4, 5, 7, 9, 11, 12 }, new List<int> { 1, 2, 3, 4, 5, 1, 2, 3 }));
            result.Add(new PatternDefinitionScale("Minor Scale", new List<int> { 0, 2, 3, 5, 7, 9, 11, 12 }, new List<int> { 1, 2, 3, 4, 5, 1, 2, 3 }));

            return result;
        }

        /// <summary>
        /// Maps computer keyboard keys to Piano notes
        /// </summary>
        /// <returns></returns>
        private static Dictionary<Char, String> createPianoKeyMap()
        {
            Dictionary<Char, String> result = new Dictionary<Char,String>();

            result.Add('a', "C");
            result.Add('w', "C#");
            result.Add('s', "D");
            result.Add('e', "D#");
            result.Add('d', "E");
            result.Add('f', "F");
            result.Add('t', "F#");
            result.Add('g', "G");
            result.Add('y', "G#");
            result.Add('h', "A");
            result.Add('u', "A#");
            result.Add('j', "B");

            return result;
        }
    }
}
