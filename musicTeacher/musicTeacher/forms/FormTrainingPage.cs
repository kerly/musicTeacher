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
    }
}
