using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicTeacher
{
    class Intervals
    {
        private String interval;
        private String imagefile;
        
        //constructor
        public Intervals(String interval, String picture)
        {
            this.interval = interval;
            this.imagefile = picture;
        }

        //getter method
        public String getinterval()
        {
            return this.interval; 
        }

        public String getpicture()
        {
            return this.imagefile; 
        }

    }
}
