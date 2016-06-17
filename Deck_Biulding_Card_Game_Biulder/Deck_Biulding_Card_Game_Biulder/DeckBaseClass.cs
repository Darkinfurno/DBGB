using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck_Biulding_Card_Game_Biulder
{
    public abstract class DeckBaseClass
    {
        protected List<Card> Deck = new List<Card>();
        protected List<Card> AvailableCards = new List<Card>();
        protected List<Card> RemovedCards = new List<Card>();
        protected int availableNum;
        public void draw()
        {
            if (Deck.Count > 0)
            {
                AvailableCards.Add(Deck[0]);
                Deck.RemoveAt(0);
                return;
            }
            drawAttemptFinish();
            
        }

        public void refillCards()
        {
            while (AvailableCards.Count < availableNum)
            {
                draw();
            }
        }

        public int AvailableNum
        {
            get;
            set;
        }
        public abstract void drawAttemptFinish();

        public List<Card> shuffle(List<Card> Deck)
        {
            List<Card> source = Deck;
            var rnd = new Random();
            var result = source.OrderBy(item => rnd.Next());
            return (List<Card>)result;
        }
    }
}
