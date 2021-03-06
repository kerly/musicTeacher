﻿using System;
using System.IO; 
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
    public partial class CircleOfFifths : Form
    {
        private static List<Intervals> allIntervals = null;
        private int closeFlag = 0;
        int index; 

        public CircleOfFifths()
        {
            InitializeComponent();
            allIntervals = createAllIntervals();
            panel1.BackColor = Color.FromArgb(190, Color.White);
            panel2.BackColor = Color.FromArgb(190, Color.White);
            panel3.BackColor = Color.FromArgb(190, Color.White);
            first_pic(); 
        }

        private void trainingModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTrainingPage formpage = new FormTrainingPage();
            closeFlag = 1;
            formpage.Show();
            this.Close();
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeFlag = 1;
            musicTeacher.forms.MenuPage.menuPage.Show();
            this.Close();
        }

        private void testingModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EarTrainingPage testingPage = new EarTrainingPage();
            closeFlag = 1;
            testingPage.Show();
            this.Close();
        }

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
        /// Display the Random FlashCard in the Picture Box
        /// </summary>
        public void displayCard(String filename)
        {
            pictureBox2.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\images\\Staff\\" + filename);

        }

        //first picture
        private void first_pic()
        {
            index = 0; 
            displayCard(allIntervals[index].getpicture());
            label3.Text = "Interval:" + allIntervals[index].getinterval(); 
        }

        private void next_pic(object sender, MouseEventArgs e)
        {
            index++;

            if (index > allIntervals.Count - 1)
            {
                index = 0;
            }

            displayCard(allIntervals[index].getpicture());
            label3.Text = "Interval:" + allIntervals[index].getinterval(); 
        }

        private void prev_picture(object sender, MouseEventArgs e)
        {
            index--;

            if (index < 0)
            {
                index = allIntervals.Count - 1;
            }

            displayCard(allIntervals[index].getpicture());
            label3.Text = "Interval:" + allIntervals[index].getinterval(); 
        }


        private void CircleOfFifths_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closeFlag == 1)
            {
                e.Cancel = false;
                this.Dispose();
            }
            else
                Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
