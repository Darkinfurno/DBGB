using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck_Biulding_Card_Game_Biulder
{
    public class PlayerDeck : DeckBaseClass
    {
        List<Card> playedCards = new List<Card>();

        public void discard(Card card)
        {
            AvailableCards.Remove(card);
            RemovedCards.Add(card);
        }
        public void endTurn()
        {
            foreach (Card card in playedCards)
            {
                RemovedCards.Add(card);
            }
            playedCards.Clear();
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
            playedCards.Add(played);
            return played;
        }

        public override void drawAttemptFinish()
        {
            if (Deck.Count + RemovedCards.Count != 0)
            {
                shuffleDeck();
                draw();
            }
        }

        public void shuffleDeck()
        {
            shuffle();
            RemovedCards.Clear();
        }

    }
}
