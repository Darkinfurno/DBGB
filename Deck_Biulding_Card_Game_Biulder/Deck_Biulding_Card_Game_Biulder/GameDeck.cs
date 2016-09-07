using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck_Biulding_Card_Game_Biulder
{
    class GameDeck : DeckBaseClass
    {
        public bool refillAtTurnChange = true;
       
        
        public bool Refill()
        {
            while(AvailableCards.Count < AvailableCards.Count)
            {
                if (Deck.Count == 0)
                {
                    return false;
                }
                
                Card temp = Deck[0];
                Deck.RemoveAt(0);
                AvailableCards.Add(temp);
            }
            return true;
        }

        public void GameStartUp()
        {
            shuffle();
            refillCards();
        }

        public override void drawAttemptFinish()
        {
            //End Game
            throw new NotImplementedException();
        }

        public List<Card> getBuyableCards()
        {
            return AvailableCards;
        }

        public void addToDestroyed(Card card)
        {
            RemovedCards.Add(card);
        }
    }
}
