using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace TreasureHunter
{
    class Popup
    {
        /*
         * Created By Joshua Ray
         * 11/11/2012
         * 
         * Depreicated. Doesn't match game aesthetic
         * Asks a Yes/No Question
         * 
         */

        DialogResult result;
        /// <summary>
        /// Depreicated. Doesn't match game aesthetic
        /// Asks a Yes/No Question
        /// </summary>
        /// <param name="Question">Question to ask</param>
        public Popup(string Question) {
           result = MessageBox.Show("You are Hurt! Restart Level?", Question, MessageBoxButtons.YesNo);
        }

        /// <summary>
        /// Test if user responded with "Yes" to the given question
        /// </summary>
        /// <returns>True if user responded with Yes to the given question, false otherwise</returns>
        public bool Yes() {
            return result == DialogResult.Yes;
        }

        /// <summary>
        /// Test if user responded with "No" to the given question
        /// </summary>
        /// <returns>True if user responded with "No" to the given question, false otherwise</returns>
        public bool No() {
            return result == DialogResult.No;
        }
    }
}
