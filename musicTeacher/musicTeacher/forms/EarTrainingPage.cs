using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;
using System.IO;


namespace musicTeacher.forms
{
    public partial class EarTrainingPage : Form
    {
        //Initializing dynamic Arrays and Variables
        int total_questions = -1;
        int timeLeft;
        APlayablePattern play;
        int user_score = 0;
        String answer;
        String answer2 = "";
        public static int currentOctave = 3;
        private static List<FlashCards> allflashcards = null;
        private static List<Intervals> allIntervals = null;
        // Flag to determine exit method
        private int closeFlag = 0;
        
        List<String> pianokeys = new List<String>{
             /*   "C2", "C#2", "D2", "D#2", "E2", "F2", "F#2", "G2", "G#2", "A2", "A#2", "B2",*/
                "C3", "C#3", "D3", "D#3", "E3", "F3", "F#3", "G3", "G#3", "A3", "A#3", "B3",
        };   

        public EarTrainingPage()
        {
            InitializeComponent();
            pictureBox1.Visible = false;
            panel1.Visible = false;
            panel8.Visible = false;
            label4.Text = "";
            label11.Text = "";
            label10.Text = "";

            // Initialize the applications definitions
            MusicDefinitions.initDefinitions();
            allflashcards = createAllflashcards();
            allIntervals = createAllIntervals();
            changeopacity(); 
        }

        /// <summary>
        /// Changes the opacity of the panels
        /// </summary>
        public void changeopacity()
        {
            panel2.BackColor = Color.FromArgb(150, Color.White);
            panel7.BackColor = Color.FromArgb(150, Color.White);
            panel1.BackColor = Color.FromArgb(150, Color.White);
            panel3.BackColor = Color.FromArgb(150, Color.White);
            panel4.BackColor = Color.FromArgb(150, Color.White);


        }

        /// <summary>
        /// Maps every Interval to its appropriate Interval picture
        /// </summary>
        private static List<Intervals> createAllIntervals()
        {
            List<Intervals> result = new List<Intervals>();
            result.Add(new Intervals("Minor 2nd", "\\2_m.png"));
            result.Add(new Intervals("Major 2nd", "\\2_w.png"));
            result.Add(new Intervals("Minor 3rd", "\\3_m.png"));
            result.Add(new Intervals("Major 3rd", "\\3_w.png"));
            result.Add(new Intervals("Perfect 4th", "\\4_czy.png"));
            result.Add(new Intervals("Tritone", "\\Tryton.png"));
            result.Add(new Intervals("Perfect 5th", "\\5_cz.png"));
            result.Add(new Intervals("Minor 6th", "\\6_m.png"));
            result.Add(new Intervals("Major 6th", "\\6_w.png"));
            result.Add(new Intervals("Major 7th", "\\7_w.png"));
            result.Add(new Intervals("Minor 7th", "\\7_m.png"));
            result.Add(new Intervals("Octave", "\\8_cz.png"));

            return result;
        }

        /// <summary>
        /// Maps every major/minor name with its picture
        /// </summary>
        private static List<FlashCards> createAllflashcards()
        {
            List<FlashCards> result = new List<FlashCards>();
            result.Add(new FlashCards("C", "A", "\\C-major_a-minor.png", "", ""));
            result.Add(new FlashCards("C#", "A#", "\\C-sharp-major_a-sharp-minor.png", "Db", "Bb"));
            result.Add(new FlashCards("Cb", "Ab", "\\C-flat-major_a-flat-minor.png", "B", ""));
            result.Add(new FlashCards("D", "B", "\\D-major_b-minor.png", "", ""));
            result.Add(new FlashCards("Db", "Bb", "\\D-flat-major_b-flat-minor.png", "C#", "A#"));
            result.Add(new FlashCards("E", "C#", "\\E-major_c-sharp-minor.png", "", ""));
            result.Add(new FlashCards("Eb", "C", "\\E-flat-major_c-minor.png", "", ""));
            result.Add(new FlashCards("F", "D", "\\F-major_d-minor.png", "", ""));
            result.Add(new FlashCards("F#", "D#", "\\F-sharp-major_d-sharp-minor.png", "Gb", "Eb"));
            result.Add(new FlashCards("G", "E", "\\G-major_e-minor.png", "", ""));
            result.Add(new FlashCards("Gb", "Eb", "\\G-flat-major_E-flat-minor.png", "F#", "D#"));
            result.Add(new FlashCards("A", "F#", "\\A-Major_F-Sharp-Minor.png", "", ""));
            result.Add(new FlashCards("Ab", "F", "\\A-flat-major_f-minor.png", "", "G#"));
            result.Add(new FlashCards("B", "G#", "\\B-major_g-sharp-minor.png", "Cb", "Ab"));
            result.Add(new FlashCards("Bb", "G", "\\B-flat-major_g-minor.png", "", ""));

            return result;
        }

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

        private void trainingModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTrainingPage trainingPage = new FormTrainingPage();
            closeFlag = 1;
            trainingPage.Show();
            this.Close();
        }

        public void RandomFlashCard()
        {

            //  String noteName = allflashcards[index];
            RadioButton checkedBox = panel1.Controls.OfType<RadioButton>()
            .FirstOrDefault(r => r.Checked);
            String type = checkedBox.Text.Trim();
            Random rnd = new Random();
            int index;
            if (type.Equals("Major"))
            {
                //Using the Random operator to pick a random card from the list

                index = rnd.Next(allflashcards.Count);
                answer = allflashcards[index].getmajor();
                answer2 = allflashcards[index].getchoice2_major();
                displayCard(allflashcards[index].getpicture());
            }
            else if (type.Equals("Minor"))
            {
                //Using the Random operator to pick a random card from the list
                index = rnd.Next(allflashcards.Count);

                answer = allflashcards[index].getminor();
                answer2 = allflashcards[index].getchoice2_minor();
                displayCard(allflashcards[index].getpicture());
            }
            else
            {
                //Using the Random operator to pick a random card from the list
                index = rnd.Next(allIntervals.Count);
                answer = allIntervals[index].getinterval();
                displayCard(allIntervals[index].getpicture());

            }
            startTimer();
        }

        /// <summary>
        /// Display the Random FlashCard in the Picture Box
        /// </summary>
        public void displayCard(String filename)
        {
            pictureBox1.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\images\\Staff\\" + filename);
        }

        //this function will play a random audio based on the three
        //choices that was chosen: Scale, Chord, Interval
        private void RandomAudio()
        {
            // Find the pattern definition by the selected radio button
            RadioButton checkedBox = panel4.Controls.OfType<RadioButton>()
                .FirstOrDefault(r => r.Checked);
            String type = checkedBox.Text.Trim();

            //Using the Random operator to pick a random note from the list
            Random rnd = new Random();
            int index = rnd.Next(pianokeys.Count);
            String noteName = pianokeys[index];

            answer = noteName.Split('3')[0];
            MusicNote baseNote = NoteFinder.findNoteByName(noteName);
            List<APatternDefinition> parentList = null;
            if (type.Contains("Chord"))
            {
                parentList = MusicDefinitions.allChordDefinitions.Cast<APatternDefinition>().ToList();
            }
            else if (type.Contains("Scale"))
            {
                parentList = MusicDefinitions.allScaleDefinitions.Cast<APatternDefinition>().ToList();
            }
            else
            {
                parentList = MusicDefinitions.allIntervalDefinitions.Cast<APatternDefinition>().ToList();

            }
            APatternDefinition definition = NoteFinder.findPatternDefinitionByName(checkedBox.Text, parentList);

            // Get the concrete pattern for the selected radio button and base note
            APlayablePattern concretePattern = NoteFinder.generateConcretePattern(baseNote, definition);
            if (concretePattern != null)
            {
                concretePattern.Play();
            }

            //save this to a global array to replay if necessary
            play = concretePattern;
            startTimer();
        }//end of RandomAudio function

        public void startTimer()
        {
            timeLeft = 20;
            label4.Text = "20 seconds";
            timer1.Start();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                //displaying the new time that is left
                //this can be done by updating the time label
                timeLeft = timeLeft - 1;
                label4.Text = timeLeft + " seconds";
            }
            else
            {
                timer1.Stop();
                label4.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            user_score = 0;
            total_questions = 0;
            label3.Text = "Score: " + user_score + "/" + total_questions;
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeFlag = 1;
            this.Close();
            musicTeacher.forms.MenuPage.menuPage.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            String noteName = "C3";
            NoteFinder.PlayNoteByName(noteName);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (play != null)
            {
                play.Play();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Button button in panelTabNotes.Controls.OfType<Button>())
            {
                button.BackColor = Color.Transparent;
            }

            foreach (Button button in panel8.Controls.OfType<Button>())
            {
                button.BackColor = Color.Transparent;
            }

            label11.Text = ""; 
            label4.Text = "";
            label10.Text = "";

            RadioButton checkedBox = panel6.Controls.OfType<RadioButton>()
            .FirstOrDefault(r => r.Checked);
            String type = checkedBox.Text.Trim();

            total_questions++;
            label3.Text = "Score: " + user_score + "/" + total_questions;

            if (type.Contains("Staff"))
            {
                RandomFlashCard();
            }
            else
            {
                RandomAudio();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            label4.Text = "";
            label11.Text = "";
            label10.Text = "Answer: " + answer;
        }

        private void checkAnswer(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            // Find the base note for the selected button
            String trim_answer = answer.Trim();
            String noteName = button.Text.Trim();

            if (noteName == trim_answer || noteName == answer2)
            {
                timer1.Stop();
                label10.Text = ""; 
                label11.Text = "Correct Answer";
                button.BackColor = Color.LimeGreen;
                user_score++;
                label3.Text = "Score: " + user_score + "/" + (total_questions + 1);
                label4.Text = "";
            }
            else
            {
                label10.Text = "Please try again.";
                button.BackColor = Color.Crimson;
            }
        }

        private void radioButton15_CheckedChanged(object sender, EventArgs e)
        {
            panel8.Visible = true;
            panelTabNotes.Visible = false;
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panelTabNotes.Visible = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panelTabNotes.Visible = true;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            panel4.Visible = true;
            panel1.Visible = false;
            pictureBox1.Visible = false;
            label4.Text = "";
            timer1.Stop(); 

        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel1.Visible = true;
            pictureBox1.Visible = true;
            label4.Text = "";
            timer1.Stop(); 

        }
        
        private void EarTrainingPage_FormClosing(object sender, FormClosingEventArgs e)
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


    }//end of namespace
}//end of project