using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicTeacher
{
    class MusicInterval : APlayablePattern
    {
        // Parent constructor
        public MusicInterval(MusicNote baseNote, PatternDefinitionInterval patternDefinition) :
            base(baseNote, patternDefinition)
        {
            this.timeBetweenNotes = 1000;
        }
    }
}
