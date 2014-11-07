using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace musicTeacher
{

    public class MusicNote
    {

        // Variables
        private String name;
        private int midiNumber;
        private AudioPlayer audioPlayer;

        // Constructor
        public MusicNote(String name, String soundFile, int midiNumber)
        {
            this.name = name;
            this.midiNumber = midiNumber;
            this.audioPlayer = new AudioPlayer(soundFile, name);
        }

        // Getters
        public String getName()
        {
            return this.name;
        }
        public int getMidiNumber()
        {
            return this.midiNumber;
        }

        // Play method
        public void Play()
        {
            audioPlayer.Play();
        }

    }
}
