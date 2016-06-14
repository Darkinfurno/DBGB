using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck_Biulding_Card_Game_Biulder
{
    class Deck
    {
        List<Card> DeckList = new List<Card>();
        InPlayCardset played = new InPlayCardset();
        List<Card> Discarded = new List<Card>();
        List<Card> hand = new List<Card>();
        int HandSize;

        public void discard(Card card)
        {
            hand.Remove(card);
            Discarded.Add(card);
        }
        public void endTurn()
        {

        }

        public Card playcard(int index)
        {
            Card played = hand[index];
            hand.RemoveAt(index);
            return played;
        }

        public void draw()
        {
            if(DeckList.Count > 0)
            {

            }
            else
            {

            }
        }
         
        public int _defaultHandSize
        { 
            get{ return HandSize; }
            set{ HandSize = value;}
        }

        public void refillHand()
        {

        }

        public void shuffle()
        {

        }
    }
}
