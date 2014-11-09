using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicTeacher
{
    public abstract class APlayablePattern
    {
        // Variables
        private String wholeName;
        private APatternDefinition patternDefinition;
        private List<MusicNote> notes;
        protected int timeBetweenNotes;

        // Constructor
        public APlayablePattern(MusicNote baseNote, APatternDefinition patternDefinition)
        {
            this.wholeName = baseNote.getName() + " " + patternDefinition.getName();
            this.patternDefinition = patternDefinition;
            this.notes = NoteFinder.translatePattern(baseNote, patternDefinition);
        }

        // Getter methods
        public String getWholeName()
        {
            return this.wholeName;
        }
        public APatternDefinition getPatternDefinition()
        {
            return this.patternDefinition;
        }
        public List<MusicNote> getNotes()
        {
            return this.notes;
        }
        public int getTimeBetweenNotes()
        {
            return this.timeBetweenNotes;
        }

        // Play methods
        public void Play(float lengthMultiplier)
        {
            // Compute the length of time to play this pattern
            Play((int)(timeBetweenNotes * lengthMultiplier));
        }
        public void Play()
        {
            // Play the pattern with the standar length
            Play(timeBetweenNotes);
        }

        /// <summary>
        /// The real Play method, CALL THIS METHOD IN A NEW THREAD please and thank you
        /// </summary>
        /// <param name="actualPlayTime"></param>
        private void Play(int actualPlayTime)
        {
            foreach (MusicNote note in notes)
            {
                Stopwatch stopWatch = new Stopwatch();
                note.Play();

                // Start the watch and wait until the time is up
                stopWatch.Start();
                while (stopWatch.Elapsed.TotalMilliseconds < actualPlayTime)
                {
                    // wait
                }
                stopWatch.Stop();
            }
        }
    }
}
