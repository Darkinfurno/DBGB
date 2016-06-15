using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck_Biulding_Card_Game_Biulder
{
    public class GameController
    {
        List<PlayerDeck> playerList;
        List<GameDeck> mainDeckList;
        List<Card> removedCards;
        List<Card> startingHand;

        public GameController()
        {
            playerList = new List<PlayerDeck>();
            mainDeckList = new List<GameDeck>();
            removedCards = new List<Card>();
            startingHand = new List<Card>();
        }

        public void addStarterCard(Card card, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                startingHand.Add(card);
            }
        }

        public void cardEvent(int EventName)
        {
            switch (EventName)
            {
                case 0:
                    break;
            }



        }

        public void createCard()
        {
        }
    }
}
