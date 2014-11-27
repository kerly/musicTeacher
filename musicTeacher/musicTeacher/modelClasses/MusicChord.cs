﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicTeacher
{
    class MusicChord : APlayablePattern
    {
        // Parent constructor
        public MusicChord(MusicNote baseNote, PatternDefinitionChord patternDefinition) :
            base(baseNote, patternDefinition)
        {
            this.timeBetweenNotes = 1000;
        }

    }
}
