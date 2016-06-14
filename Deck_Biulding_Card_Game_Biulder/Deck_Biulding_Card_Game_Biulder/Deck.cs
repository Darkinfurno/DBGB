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
            if(DeckList.Count > 0 )
            {
                hand.Add(DeckList[0]);
                DeckList.RemoveAt(0);
                return;
            }
            else if (DeckList.Count + Discarded.Count != 0)
            {
                shuffle();
                draw();
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
            List<Card> source = Discarded;
            var rnd = new Random();
            var result = source.OrderBy(item => rnd.Next());
            DeckList = (List<Card>)result;
            Discarded.Clear();
        }
    }
}
