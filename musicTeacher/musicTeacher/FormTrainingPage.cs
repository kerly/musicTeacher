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
            MusicDefinitions.initDefinitions();
        }

        /// <summary>
        /// Gets all pianoButtons
        /// </summary>
        /// <returns>List of all pianoButtons</returns>
        private List<Button> getAllPianoButtons()
        {
            Button[] pianoButtons = new Button[MusicDefinitions.NUM_OF_NOTES] { 
                btnPianoC2, btnPianoCs2, btnPianoD2, btnPianoDs2, btnPianoE2, btnPianoF2, 
                btnPianoFs2, btnPianoG2, btnPianoGs2, btnPianoA2, btnPianoAs2, btnPianoB2,
                btnPianoC3, btnPianoCs3, btnPianoD3, btnPianoDs3, btnPianoE3, btnPianoF3, 
                btnPianoFs3, btnPianoG3, btnPianoGs3, btnPianoA3, btnPianoAs3, btnPianoB3,
                btnPianoC4, btnPianoCs4, btnPianoD4, btnPianoDs4, btnPianoE4, btnPianoF4, 
                btnPianoFs4, btnPianoG4, btnPianoGs4, btnPianoA4, btnPianoAs4, btnPianoB4};

            return new List<Button>(pianoButtons);
        }
    }
}
