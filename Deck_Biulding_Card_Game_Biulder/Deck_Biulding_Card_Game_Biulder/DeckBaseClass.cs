using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck_Biulding_Card_Game_Biulder
{
    abstract class DeckBaseClass
    {
        protected List<Card> DeckList = new List<Card>();
        protected List<Card> AvailableCards = new List<Card>();
        protected List<Card> RemovedCards = new List<Card>();

        public void draw()
        {
            if (DeckList.Count > 0)
            {
                AvailableCards.Add(DeckList[0]);
                DeckList.RemoveAt(0);
                return;
            }
            drawAttemptFinish();
            
        }
        abstract void drawAttemptFinish();
        
    }
}
