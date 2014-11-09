using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicTeacher
{
    /// <summary>
    /// The abstract class APatternDefinition is used to define 
    /// a pattern definition's structure.
    /// </summary>
    public abstract class APatternDefinition
    {
        // Variables
        private String name;
        private List<int> intervals;
        private List<int> fingerPositions;
        private int noteRange;

        // Constructor
        public APatternDefinition(String name, List<int> intervals, List<int> fingerPositions)
        {
            this.name = name;
            this.intervals = intervals;
            this.fingerPositions = fingerPositions;
            this.noteRange = intervals.Max();
        }

        // Getter methods
        public String getName()
        {
            return this.name;
        }
        public List<int> getIntervals()
        {
            return this.intervals;
        }
        public List<int> getFingerPositions()
        {
            return this.fingerPositions;
        }
        public int getNoteRange() 
        {
            return this.noteRange;
        }
    }
}
