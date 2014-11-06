using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace musicTeacher
{
    public class AudioPlayer
    {
        // Windows api method
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string command, StringBuilder returnValue,
            int returnLength, IntPtr winHandle);

        // Variables
        private string fileName;
        private bool isOpen;
        private string aliasName;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="aliasName"></param>
        public AudioPlayer(String fileName, String aliasName)
        {
            this.fileName = fileName;
            this.aliasName = aliasName;
            this.isOpen = false;
        }

        /// <summary>
        /// Closes the audio file if it is open
        /// </summary>
        private void ClosePlayer()
        {
            if (isOpen)
            {
                String closeCommand = "Close " + aliasName;
                mciSendString(closeCommand, null, 0, IntPtr.Zero);
                isOpen = false;
            }
        }

        /// <summary>
        /// Opens the media file
        /// </summary>
        private void OpenAudioFile()
        {
            ClosePlayer();
            string openCommand = "Open \"" + fileName + "\" type mpegvideo alias " + aliasName;
            mciSendString(openCommand, null, 0, IntPtr.Zero);
            isOpen = true;
        }

        /// <summary>
        /// Plays the media file
        /// </summary>
        private void PlayAudioFile()
        {
            if (isOpen)
            {
                string playCommand = "Play " + aliasName;
                mciSendString(playCommand, null, 0, IntPtr.Zero);
            }
        }

        /// <summary>
        /// Opens then plays the audio file
        /// </summary>
        public void Play()
        {
            OpenAudioFile();
            PlayAudioFile();
        }

        /// <summary>
        /// Closes the audio file
        /// </summary>
        public void Stop()
        {
            ClosePlayer();
        }

    }
}
