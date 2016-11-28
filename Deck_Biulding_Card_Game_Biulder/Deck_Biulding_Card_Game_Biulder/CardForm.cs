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
    public partial class CardForm : Form
    {
        public CardForm()
        {
            InitializeComponent();
        }
        Card card;

        public void setCard(Card card)
        {
            this.card = card;
        }

        private void CardForm_MouseClick(object sender, MouseEventArgs e)
        {

        }
        
    }
}
