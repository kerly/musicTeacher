using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicTeacher
{
    class MusicNote
    {
        // Variables
        private String name;
        private System.Media.SoundPlayer soundPlayer;
        private int midiNumber;

        // Constructor
        public MusicNote(String name, String soundFile, int midiNumber)
        {
            this.name = name;
            this.soundPlayer = new System.Media.SoundPlayer(soundFile);
            this.soundPlayer.Load();
            this.midiNumber = midiNumber;
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
        public System.Media.SoundPlayer getSoundPlayer()
        {
            return this.soundPlayer;
        }
    }
}
