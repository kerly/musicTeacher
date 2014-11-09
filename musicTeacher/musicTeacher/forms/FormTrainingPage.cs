using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace musicTeacher
{
    public partial class FormTrainingPage : Form
    { 

        // Public variables
        public static int currentOctave = 3;

        /// <summary>
        /// Constructor / Initializer
        /// </summary>
        public FormTrainingPage()
        {
            InitializeComponent();

            // Initialize the applications definitions
            MusicDefinitions.allPianoButtons = getAllPianoButtons();
            MusicDefinitions.allNoteChoiceButtons = getAllNoteChoiceButtons();
            MusicDefinitions.allChordButtons = getAllChordButtons();
            MusicDefinitions.allScaleButtons = getAllScaleButtons();
            MusicDefinitions.allIntervalButtons = getAllIntervalButtons();
            MusicDefinitions.initDefinitions();
        }

        /// <summary>
        /// Gets all pianoButtons
        /// </summary>
        /// <returns>List of all pianoButtons</returns>
        private List<Button> getAllPianoButtons()
        {
            Button[] pianoButtons = new Button[MusicDefinitions.NUM_OF_PIANO_KEYS] { 
                btnPianoC2, btnPianoCs2, btnPianoD2, btnPianoDs2, btnPianoE2, btnPianoF2, 
                btnPianoFs2, btnPianoG2, btnPianoGs2, btnPianoA2, btnPianoAs2, btnPianoB2,
                btnPianoC3, btnPianoCs3, btnPianoD3, btnPianoDs3, btnPianoE3, btnPianoF3, 
                btnPianoFs3, btnPianoG3, btnPianoGs3, btnPianoA3, btnPianoAs3, btnPianoB3,
                btnPianoC4, btnPianoCs4, btnPianoD4, btnPianoDs4, btnPianoE4, btnPianoF4, 
                btnPianoFs4, btnPianoG4, btnPianoGs4, btnPianoA4, btnPianoAs4, btnPianoB4};

            return new List<Button>(pianoButtons);
        }

        private List<Button> getAllNoteChoiceButtons()
        {
            Button[] noteChoiceButtons = new Button[MusicDefinitions.NUM_OF_NOTES + 1] { 
                btnTabNoteC, btnTabNoteCs, btnTabNoteD, btnTabNoteDs, btnTabNoteE, btnTabNoteF,
                btnTabNoteFs, btnTabNoteG, btnTabNoteGs, btnTabNoteA, btnTabNoteAs, btnTabNoteB,
                btnTabNoteRandom};

            return new List<Button>(noteChoiceButtons);
        }

        private List<Button> getAllChordButtons()
        {
            List<Button> result = new List<Button>();

            return result;
        }

        private List<Button> getAllScaleButtons()
        {
            List<Button> result = new List<Button>();

            return result;
        }

        private List<Button> getAllIntervalButtons()
        {
            List<Button> result = new List<Button>();

            return result;
        }

        private void FormTrainingPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        // User events
        /// <summary>
        /// Play a piano note when the piano button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPianoKeyClick(object sender, EventArgs e)
        {
            // Get the button instance that created this event
            Button button = (Button)sender;

            // Play the note
            NoteFinder.PlayNoteByName(button.Text.Trim());
        }

        /// <summary>
        /// Play a piano note when a mapped computer key is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormTrainingPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            String noteName = "";

            if (MusicDefinitions.pianoKeyMap.TryGetValue(e.KeyChar, out noteName))
            {
                noteName = noteName + currentOctave;
                NoteFinder.PlayNoteByName(noteName);
            }
        }
        
        /// <summary>
        /// Play a pattern when a base note is pressed and a radio button is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTrainingNoteClick(object sender, MouseEventArgs e)
        {
            // Get the button instance that created this event
            Button button = (Button)sender;
             
            // Find the base note for the selected button
            String noteName = button.Text.Trim() + currentOctave;
            MusicNote baseNote = NoteFinder.findNoteByName(noteName);

            // Find the pattern definition by the selected radio button
            RadioButton checkedBox = panelDefinitionBox.Controls.OfType<RadioButton>()
                .FirstOrDefault(r => r.Checked);
            String patternName = checkedBox.Text.Trim();

            // Find which kind of pattern definition this button references
            APatternDefinition definition = null;
            if (patternName.Contains("Chord"))
            {
                List<APatternDefinition> parentList = MusicDefinitions.allChordDefinitions.Cast<APatternDefinition>().ToList();
                definition = NoteFinder.findPatternDefinitionByName(checkedBox.Text, parentList);
            }
            else if (patternName.Contains("Scale"))
            {
                List<APatternDefinition> parentList = MusicDefinitions.allScaleDefinitions.Cast<APatternDefinition>().ToList();
                definition = NoteFinder.findPatternDefinitionByName(checkedBox.Text, parentList);
            }
            else if (patternName.Contains("Interval"))
            {
                List<APatternDefinition> parentList = MusicDefinitions.allIntervalDefinitions.Cast<APatternDefinition>().ToList();
                definition = NoteFinder.findPatternDefinitionByName(checkedBox.Text, parentList);
            }
            
            // Get the concrete pattern for the selected radio button and base note
            APlayablePattern concretePattern = NoteFinder.generateConcretePattern(baseNote, definition);
            if (concretePattern != null)
            {
                concretePattern.Play();
            }
            
        }

    }
}
