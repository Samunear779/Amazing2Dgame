using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Amazing2Dgame
{
    public partial class MainScreen : UserControl
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            // f is the form that this control is on - ("this" is the current User Control) 
            Form f = this.FindForm();
            f.Controls.Remove(this);

            // Create an instance of the Game Screen
            GameScreen gs = new GameScreen();

            // Add the User Control to the Form 
            f.Controls.Add(gs);
            gs.Focus();
            //play music 
            SoundPlayer player = new SoundPlayer(Properties.Resources.Lounge_Game2);
            player.PlayLooping();
        }
    }
}
