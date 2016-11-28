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
    public partial class DeckForm : Form
    {
        public DeckForm()
        {
            InitializeComponent();
        }
        CardForm[] DeckCards;
        DeckBaseClass Deck;
        int location = 0;
        public void Assign_Deck(DeckBaseClass Deck)
        {
            this.Deck = Deck;
        }

        private void Deck_Load(object sender, EventArgs e)
        {
            DeckCards= new CardForm[Deck.AvailableCards.Count];
            for (int i = 0; i < Deck.AvailableCards.Count; i++ )
            {
                DeckCards[i].setCard(Deck.AvailableCards[i]);
                DeckCards[i].MdiParent = this;
            }
            
        }

        private void SetShownCards()
        {
            for (int i = location; i < Deck.AvailableCards.Count && i < location + 5; i++)
            {
                DeckCards[i].Show();
            }
            this.LayoutMdi(MdiLayout.TileVertical);
            if (Deck.AvailableCards.Count > location + 5) this.Right.Enabled = true;
            else this.Right.Enabled = false;
            if (location > 0) this.Left.Enabled = true;
            else this.Left.Enabled = false;
        }

        private void Right_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }
            location += 5;

            SetShownCards();

        }

        private void Left_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }
            location -= 5;

            SetShownCards();

        }
        
    }
}
