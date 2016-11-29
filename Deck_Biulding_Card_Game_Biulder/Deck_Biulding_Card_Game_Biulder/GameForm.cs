using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Deck_Biulding_Card_Game_Biulder
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
        }

        GameController Game = new GameController();

        private void GameForm_Load(object sender, EventArgs e)
        {
            List<DeckForm> Decks = new List<DeckForm>();

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        
    }
}
