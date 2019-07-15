using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingPairsGame
{
    public partial class Form1 : Form
    {
        // firstClicked stores the first label clicked by the user (only trace, not create new object)
        // secondClicked stores the second label clicked by the user (only trace, not create new object)
        Label firstClicked = null;
        Label secondClicked = null;


        Random random = new Random();
        List<string> icons = new List<string>()
        {
            "r", "r", "j", "j", "s", "s", "h", "h",
            "m", "m", "v", "v", "k", "k", "z", "z"
        };
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquare();
        }
        private void AssignIconsToSquare()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                // as followed by a defined type, this means incoLable should be a Lable type
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];

                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        /* Do not need to call this method, it is an event method, called when label clicked */
        private void Label_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;
            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)
                    //tells the program stop executing code
                    return;
                /* Block not get clicked, and user click it */
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                Check_Win();

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }
                
                timer1.Start();

            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            // hide the pattern
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            firstClicked = null;
            secondClicked = null;
        }

        private void Check_Win()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                // game not end
                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            MessageBox.Show("You Match All Icons!", "Congratulations!");
            Close();
        }
    }
}
