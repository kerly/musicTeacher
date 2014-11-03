using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicTeacher
{
    class PatternDefinitionChord : APatternDefinition
    {
        // Parent constructor
        public PatternDefinitionChord(String name, List<int> intervals, List<int> fingerPositions) : 
            base(name, intervals, fingerPositions) 
        {
        }
    }
}
