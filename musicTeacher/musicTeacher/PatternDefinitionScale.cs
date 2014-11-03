using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicTeacher
{
    class PatternDefinitionScale : APatternDefinition
    {
        // Parent constructor
        public PatternDefinitionScale(String name, List<int> intervals, List<int> fingerPositions) : 
            base(name, intervals, fingerPositions) 
        {
        }
    }
}
