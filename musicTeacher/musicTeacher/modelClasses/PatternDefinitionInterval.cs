using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicTeacher
{
    class PatternDefinitionInterval : APatternDefinition
    {
        // Parent constructor
        public PatternDefinitionInterval(String name, int interval, List<int> fingerPositions) :
            base(name, new List<int> { 0, interval}, fingerPositions)
        {
        }
    }
}
