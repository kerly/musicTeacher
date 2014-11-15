using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicTeacher
{
    class FlashCards
    {
         //Variables
        private String major;
        private String minor;
        private String imagefile;
        private String choice2_major;
        private String choice2_minor; 

        //constructor
        public FlashCards(String major, String minor, String picture, String choice2_major, String choice2_minor)
        {
            this.major = major;
            this.minor = minor;
            this.imagefile = picture;
            this.choice2_major = choice2_major;
            this.choice2_minor = choice2_minor; 
        }

        //getter method
        public String getmajor()
        {
            return this.major; 
        }

        public String getminor()
        {
            return this.minor; 
        }

        public String getpicture()
        {
            return this.imagefile; 
        }

        public String getchoice2_major()
        {
            return this.choice2_major; 
        }

        public String getchoice2_minor()
        {
            return this.choice2_minor; 
        }
    }
}
