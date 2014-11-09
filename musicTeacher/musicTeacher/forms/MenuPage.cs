using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace musicTeacher.forms
{
    public partial class MenuPage : Form
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormTrainingPage trainingPage = new FormTrainingPage();
            this.Hide();
            trainingPage.Show();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EarTrainingPage eartrainingpage = new EarTrainingPage();
            this.Hide();
            eartrainingpage.Show(); 
        }

    }
}
