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


namespace musicTeacher.forms
{
    public partial class EarTrainingPage : Form
    {
        int total_questions = 0;
        APlayablePattern play;
        int user_score = 0;
        String answer;
        public static int currentOctave = 3;
        List<String> pianokeys = new List<String>{
             /*   "C2", "C#2", "D2", "D#2", "E2", "F2", "F#2", "G2", "G#2", "A2", "A#2", "B2",*/
                "C3", "C#3", "D3", "D#3", "E3", "F3", "F#3", "G3", "G#3", "A3", "A#3", "B3",
                /*"C4", "C#4", "D4", "D#4", "E4", "F4", "F#4", "G4", "G#4", "A4", "A#4", "B4"*/
        };

        public EarTrainingPage()
        {
            InitializeComponent();
            // generatingList(); 

            // Initialize the applications definitions
            MusicDefinitions.initDefinitions();
            RandomAudio();
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


        private void trainingModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTrainingPage trainingpage = new FormTrainingPage();
            this.Hide();
            trainingpage.Show();
        }

        //this function will play a random audio based on the three
        //choices that was chosen: Scale, Chord, Interval
        private void RandomAudio()
        {
            //First Find the name of the checked Radio Box
            //Choices: Scale, Chord, Interval
            // Find the pattern definition by the selected radio button
            RadioButton checkedBox = panel1.Controls.OfType<RadioButton>()
                .FirstOrDefault(r => r.Checked);
            String type = checkedBox.Text.Trim();

            RadioButton checkedBox1 = panel4.Controls.OfType<RadioButton>()
                .FirstOrDefault(r => r.Checked);

            //Using the Random operator to pick a random note from the list
            Random rnd = new Random();
            int index = rnd.Next(pianokeys.Count);
            String noteName = pianokeys[index];

            answer = noteName;
            MusicNote baseNote = NoteFinder.findNoteByName(noteName);
            String major_minor;
            APatternDefinition definition = null;
            if (type.Equals("Chord"))
            {
                major_minor = checkedBox1.Text + " Chord";
                List<APatternDefinition> parentList = MusicDefinitions.allChordDefinitions.Cast<APatternDefinition>().ToList();
                definition = NoteFinder.findPatternDefinitionByName(major_minor, parentList);
            }
            else if (type.Equals("Scale"))
            {
                major_minor = checkedBox1.Text + " Scale";
                List<APatternDefinition> parentList = MusicDefinitions.allScaleDefinitions.Cast<APatternDefinition>().ToList();
                definition = NoteFinder.findPatternDefinitionByName(major_minor, parentList);
            }
            else if (type.Equals("Interval"))
            {
                List<APatternDefinition> parentList = MusicDefinitions.allIntervalDefinitions.Cast<APatternDefinition>().ToList();
                definition = NoteFinder.findPatternDefinitionByName(checkedBox1.Text, parentList);
            }

            // Get the concrete pattern for the selected radio button and base note
            APlayablePattern concretePattern = NoteFinder.generateConcretePattern(baseNote, definition);
            if (concretePattern != null)
            {
                concretePattern.Play();
            }

            //save this to a global array to replay if necessary
            play = concretePattern;
        }//end of RandomAudio function



        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            panel2.BackColor = Color.FromArgb(150, Color.White);

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            panel6.BackColor = Color.FromArgb(150, Color.White);

        }

        int timeLeft;
        private void EarTrainingPage_Load(object sender, EventArgs e)
        {
            startTimer();

        }//end of function

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

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            panel7.BackColor = Color.FromArgb(150, Color.White);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            panel4.BackColor = Color.FromArgb(150, Color.White);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            user_score = 0;
            total_questions = 0;
            label3.Text = "Score: " + user_score + "/" + total_questions;
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuPage menupg = new MenuPage();
            this.Hide();
            menupg.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            panel3.BackColor = Color.FromArgb(150, Color.White);

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


        private void FormsClosed_TestingMode(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label10.Text = "";
            RandomAudio();
            total_questions++;
            label3.Text = "Score: " + user_score + "/" + total_questions;
            startTimer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            label4.Text = "";
            label10.Text = "Answer: " + answer;
        }

        private void checkAnswer(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
           // Find the base note for the selected button
            String noteName = button.Text.Trim() + currentOctave;
            
            if (noteName == answer)
            {
                label11.Text = "Correct Answer";
                button.BackColor = Color.LimeGreen;
                user_score++; 
                RandomAudio();
                total_questions++;
                label3.Text = "Score: " + user_score + "/" + total_questions;
                startTimer();
               
            }
            else
            {
                label10.Text = "Please try again.";
                button.BackColor = Color.Crimson;
            }

            button.BackColor = Color.Transparent;
            label10.Text = "";
            label11.Text = ""; 
        }

    }//end of namespace
}//end of project