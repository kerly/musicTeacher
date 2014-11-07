using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicTeacher
{
    class MusicScale : APlayablePattern
    {
        // Parent constructor
        public MusicScale(MusicNote baseNote, PatternDefinitionScale patternDefinition) :
            base(baseNote, patternDefinition)
        {
            this.timeBetweenNotes = 1000;
        }
    }
}
