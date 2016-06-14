using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck_Biulding_Card_Game_Biulder
{
    public class PlayerDeck : DeckBaseClass
    {
        List<Card> DeckList = new List<Card>();
        List<Card> played = new List<Card>();
        List<Card> RemovedCards = new List<Card>();
        List<Card> AvailableCards = new List<Card>();

        public void discard(Card card)
        {
            AvailableCards.Remove(card);
            RemovedCards.Add(card);
        }
        public void endTurn()
        {
            foreach(Card card in played)
            {
                RemovedCards.Add(card);
            }
            played.Clear();
            foreach (Card card in AvailableCards)
            {
                RemovedCards.Add(card);
            }
            AvailableCards.Clear();
            refillCards();
        }

        public Card playcard(int index)
        {
            Card played = AvailableCards[index];
            AvailableCards.RemoveAt(index);
            return played;
        }

        public override void drawAttemptFinish()
        {
            if (DeckList.Count + RemovedCards.Count != 0)
            {
                shuffle();
                draw();
            }
        }

        public void shuffleDeck()
        {
            DeckList = shuffle(RemovedCards);
            RemovedCards.Clear();
        }
    }
}
