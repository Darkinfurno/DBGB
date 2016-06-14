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
    public partial class Form1 : Form
    {
        int currentPlayer;
        List<PlayerDeck> playerList = new List<PlayerDeck>();
        List<MainDeck> mainDeckList = new List<MainDeck>();
        List<Card> removedCards = new List<Card>();
        List<Card> startingHand = new List<Card>();

        public Form1()
        {
            InitializeComponent();



        }
        public void addStarterCard(Card card, int amount)
        {
            for (int i = 0; i<amount ;i++)
            {
                startingHand.Add(card);
            }
        }

        public void cardEvent(int EventName)
        {

        }

        public void createCard()
        {

        }
    }
}
