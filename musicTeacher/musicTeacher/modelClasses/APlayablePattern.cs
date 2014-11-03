using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicTeacher
{
    abstract class APlayablePattern
    {
        // Variables
        private String wholeName;
        private APatternDefinition patternDefinition;
        private List<MusicNote> notes;

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

        // Abstract methods
        public abstract void Play();
    }
}
