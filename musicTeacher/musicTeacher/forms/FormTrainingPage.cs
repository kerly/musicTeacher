using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace musicTeacher
{
    public partial class FormTrainingPage : Form
    {
        // Constants
        private const int _NOTE_REMAIN_TIME = 500;
        private const int _PATTERN_REMAIN_TIME = 4000;
        private const int _MAX_CONCURRENT_NOTES = 5;

        // Public variables
        public static int currentOctave = 3;

        // Private variables
        private static bool isPlayingPattern = false;
        private static List<String> keyTextList = null;
        private static List<Button> allPianoButtons = null;
        private static List<Keys> currentlyHeldKeys = new List<Keys>();
        private static int numOfNotesPlaying = 0;
        private int closeFlag = 0;
        private static int playSpeed = 5;

        /// <summary>
        /// Constructor / Initializer
        /// </summary>
        public FormTrainingPage()
        {
            InitializeComponent();

            // Initialize the applications definitions
            allPianoButtons = getAllPianoButtons();
            keyTextList = getKeyText();
            MusicDefinitions.initDefinitions();

            // Audio Player
            MusicDefinitions.allMusicNotes.ElementAt(0).Play();
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

        /// <summary>
        /// Method to store the labels of the piano keys
        /// </summary>
        /// <returns></returns>
        public List<String> getKeyText()
        {
            String[] keyText = new String[36];

            for (int i = 0; i < 36; i++)
            {
                keyText[i] = allPianoButtons[i].Text;
            }

            return new List<String>(keyText);
        }

        // User events
        /// <summary>
        /// Play a piano note when the piano button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPianoKeyClick(object sender, EventArgs e)
        {
            if (isPlayingPattern == false)
            {
                // Get the button instance that created this event
                Button button = (Button)sender;

                // Play the note
                Thread playNoteThread = new Thread(() => PlayNoteOnPiano(
                    MusicDefinitions.allMusicNotes.ElementAt(allPianoButtons.IndexOf(button)),
                    _NOTE_REMAIN_TIME));
                playNoteThread.Start();
            }
        }

        /// <summary>
        /// Play a pattern when a base note is pressed and a radio button is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTrainingNoteClick(object sender, MouseEventArgs e)
        {
            // Check to make sure a pattern isnt already playing
            if (isPlayingPattern == false)
            {
                // Get the button instance that created this event
                Button button = (Button)sender;

                // Find the pattern definition by the selected radio button
                RadioButton checkedBox = panelDefinitionBox.Controls.OfType<RadioButton>()
                    .FirstOrDefault(r => r.Checked);
                String patternName = checkedBox.Text.Trim();

                // Find which kind of pattern definition this button references
                APatternDefinition definition = null;
                List<APatternDefinition> parentList = null;
                if (patternName.Contains("Chord"))
                {
                    parentList = MusicDefinitions.allChordDefinitions.Cast<APatternDefinition>().ToList();
                }
                else if (patternName.Contains("Scale"))
                {
                    parentList = MusicDefinitions.allScaleDefinitions.Cast<APatternDefinition>().ToList();
                }
                else
                {
                    parentList = MusicDefinitions.allIntervalDefinitions.Cast<APatternDefinition>().ToList();
                }
                definition = NoteFinder.findPatternDefinitionByName(checkedBox.Text, parentList);

                // Find the base note for the selected button
                String noteName = button.Text.Trim();
                MusicNote baseNote = null;
                if (noteName.Equals("Random"))
                {
                    baseNote = NoteFinder.getRandomNoteInBoundsForOctave(definition, currentOctave);
                }
                else
                {
                    baseNote = NoteFinder.findNoteByName(noteName + currentOctave);
                }

                // Get the concrete pattern for the selected radio button and base note
                APlayablePattern concretePattern = NoteFinder.generateConcretePattern(baseNote, definition);
                if (concretePattern != null)
                {
                    Thread playPianoThread = new Thread(() => PlayPatternOnPiano(concretePattern));
                    playPianoThread.Start();
                }
            }

        }

        /// <summary>
        /// Hide the piano 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hidePiano_CheckedChanged(object sender, EventArgs e)
        {
            if (pianoPanel.Visible == true)
            {
                pianoPanel.Visible = false;
            }
            else
            {
                pianoPanel.Visible = true;
            }
        }

        /// <summary>
        /// Hide labels on the piano
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hideLabels_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < allPianoButtons.Count; i++)
            {
                if (allPianoButtons[i].Text != "")
                {
                    allPianoButtons[i].Text = "";
                }
                else
                {
                    allPianoButtons[i].Text = keyTextList[i];
                }
            }
        }

        /// <summary>
        /// Plays a pattern on the Piano as well as the audio files for each note in the pattern
        /// </summary>
        /// <param name="pattern"></param>
        private void PlayPatternOnPiano(APlayablePattern pattern)
        {
            // Set a global variable so other audio sources won't be played
            isPlayingPattern = true;

            // Loop through the notes displaying and playing each one
            List<Button> pianoButtons = new List<Button>();
            int waitTime = pattern.getTimeBetweenNotes();
            foreach (MusicNote note in pattern.getNotes())
            {
                Stopwatch stopWatch = new Stopwatch();
                Button button = NoteFinder.findPianoButtonByNoteName(note.getName(), keyTextList, allPianoButtons);
                pianoButtons.Add(button);
                button.BackColor = Color.LightSkyBlue;
                note.Play();

                // Start the watch and wait until the time is up
                stopWatch.Start();
                while (stopWatch.Elapsed.TotalMilliseconds < waitTime)
                {
                    // wait
                }
                stopWatch.Stop();
            }

            // Wait a while longer before turning the keys back to their original color
            Stopwatch secondWatch = new Stopwatch();
            secondWatch.Start();
            while (secondWatch.Elapsed.TotalMilliseconds < _PATTERN_REMAIN_TIME)
            {
                // wait
            }
            secondWatch.Stop();

            // Set all the piano buttons back to their original color
            foreach (Button button in pianoButtons)
            {
                if (button.Name.Contains("s"))
                {
                    button.BackColor = Color.Black;
                }
                else
                {
                    button.BackColor = Color.White;
                }
            }

            // Reset the global variable so a new instance of this method can be called
            isPlayingPattern = false;
        }

        private static void PlayNoteOnPiano(MusicNote note, int colorTime)
        {
            numOfNotesPlaying++;
            Stopwatch stopWatch = new Stopwatch();
            Button button = NoteFinder.findPianoButtonByNoteName(note.getName(), keyTextList, allPianoButtons);
            button.BackColor = Color.LightSkyBlue;
            note.Play();

            // Start the watch and wait until the time is up
            stopWatch.Start();
            while (stopWatch.Elapsed.TotalMilliseconds < colorTime)
            {
                // wait
            }
            stopWatch.Stop();

            // Set the piano button back to it's original color
            if (button.Name.Contains("s"))
            {
                button.BackColor = Color.Black;
            }
            else
            {
                button.BackColor = Color.White;
            }

            numOfNotesPlaying--;
        }
        
        // Make the GroupBox semi-ttransparent
        private void groupBoxTrainingChoices_Paint(object sender, PaintEventArgs e)
        {
            groupBoxTrainingChoices.BackColor = Color.FromArgb(200, Color.White);
        }

        private void panelDefinitionBox_Paint(object sender, PaintEventArgs e)
        {
            panelDefinitionBox.BackColor = Color.FromArgb(0, Color.White);
        }

        private void panelTabNotes_Paint(object sender, PaintEventArgs e)
        {
            panelTabNotes.BackColor = Color.FromArgb(0, Color.White);
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeFlag = 1;
            musicTeacher.forms.MenuPage.menuPage.Show();
            this.Close();
        }

        private void testingModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            forms.EarTrainingPage earTrainingPage = new forms.EarTrainingPage();
            closeFlag = 1;
            earTrainingPage.Show();
            this.Close();
        }

        private void circleOfFifthsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            forms.CircleOfFifths circleOfFifths = new forms.CircleOfFifths();
            closeFlag = 1;
            circleOfFifths.Show();
            this.Close();
        }

        private void FormTrainingPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Determine whether the user was trying to exit or go to different menu
            if (closeFlag == 1)
            {
                e.Cancel = false;
                this.Dispose();
            }
            else
                Application.Exit();
        }

        private void FormTrainingPage_KeyDown(object sender, KeyEventArgs e)
        {
            String noteName = "";

            if (isPlayingPattern == false && numOfNotesPlaying < _MAX_CONCURRENT_NOTES)
            {
                if (MusicDefinitions.pianoKeyMap.TryGetValue(e.KeyCode, out noteName) && !currentlyHeldKeys.Contains(e.KeyCode))
                {
                    currentlyHeldKeys.Add(e.KeyCode);
                    noteName = noteName + currentOctave;
                    Thread playNoteThread = new Thread(() =>
                        PlayNoteOnPiano(NoteFinder.findNoteByName(noteName), _NOTE_REMAIN_TIME));
                    playNoteThread.Start();
                }
            }
        }

        private void FormTrainingPage_KeyUp(object sender, KeyEventArgs e)
        {
            currentlyHeldKeys.Remove(e.KeyCode);
        }


        // Method used to change octave
        private void octaveInc_Click(object sender, EventArgs e)
        {
            if (currentOctave == 2)
            {
                currentOctave = 3;
                octaveLabel.Text = "Octave: " + currentOctave;
            }
            else if (currentOctave == 3)
            {
                currentOctave = 4;
                octaveLabel.Text = "Octave: " + currentOctave;
            }
            else
            {
                currentOctave = 2;
                octaveLabel.Text = "Octave: " + currentOctave;
            }
        }

        // Method used to change octave
        private void octaveDec_Click(object sender, EventArgs e)
        {
            if (currentOctave == 2)
            {
                currentOctave = 4;
                octaveLabel.Text = "Octave: " + currentOctave;
            }
            else if (currentOctave == 3)
            {
                currentOctave = 2;
                octaveLabel.Text = "Octave: " + currentOctave;
            }
            else
            {
                currentOctave = 3;
                octaveLabel.Text = "Octave: " + currentOctave;
            }
        }

        // Method used to change play speed
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            playSpeed = Math.Abs(trackBar1.Value - 5);
            playSpeedLab.Text = "Play Speed: " + playSpeed;
        }
    }
}