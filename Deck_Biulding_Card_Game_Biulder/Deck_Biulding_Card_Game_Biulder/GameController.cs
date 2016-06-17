using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck_Biulding_Card_Game_Biulder
{
    public partial class GameController
    {
        List<PlayerDeck> playerList;
        List<GameDeck> mainDeckList;
        List<Card> removedCards;
        List<Card> startingHand;
        int player = 0;

        //Events need to be able to link together Exp. peek at top 2 cards draw if different types draw = peekSelf * 2 + typeDifDraw maybe have events take arguments of type object and cast to type expected?
        

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

        public void PlayCardAt(int aIndex)
        {
            Card lPlayedCard = playerList[player].playcard(aIndex);
            
            foreach (CardEffect cef in lPlayedCard.Effects)
            {
                
                switch (cef.Effect)
                {
                    case events.Draw:
                        cardEventDraw(cef);
                        break;
                    case events.AquireFreeCard:

                        break;
                    case events.Discard:

                        break;
                    case events.DiscardIfCostNotZero:

                        break;
                    case events.DiscardIfCostZero:

                        break;
                    case events.DiscardIfEvenOdd:

                        break;
                    case events.DiscardOfType:

                        break;
                    case events.DrawIfEvenOdd:

                        break;
                    case events.DrawIfType:

                        break;
                    case events.DrawIfTypesMatch:

                        break;
                    case events.Peek:

                        break;
                     
                }
                
            }

            playerList[player].

        }


        public void createCard()
        {
        }
    }
}
