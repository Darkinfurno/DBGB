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
        List<Card> startingDeck;
        List<Card> selectedOrTempCards;
        int player = 0;

        //Events need to be able to link together Exp. peek at top 2 cards draw if different types draw = peekSelf * 2 + typeDifDraw maybe have events take arguments of type object and cast to type expected?


        public GameController()
        {
            playerList = new List<PlayerDeck>();
            mainDeckList = new List<GameDeck>();
            removedCards = new List<Card>();
            startingDeck = new List<Card>();
            selectedOrTempCards = new List<Card>(); //May need to move this gets reset lots ;)
        }

        public void addStarterCard(Card card, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                startingDeck.Add(card);
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
                        playerList[player].addCardsTo(getFreeCard(cef),cef.SelfTargetedDeckType);
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
                        cardEventTypeBasedDraw(cef, getAllTypes(selectedOrTempCards));
                        break;
                    case events.DrawIfTypesMatch:
                        cardEventTypeBasedDraw(cef, getAllTypes(selectedOrTempCards));
                        break;
                    case events.Peek:
                        processPeek(cef);
                        break;
                    case events.Destroy:

                        break;
                    case events.DestroyRandom:

                        break;
                    case events.PassCard:

                        break;
                    case events.winIf:

                        break;
                    case events.DrawDiscardedAboveBelowValue:

                        break;
                    case events.DrawDiscardedType:

                        break;
                    case events.endEffect:
                        selectedOrTempCards.Clear();
                        break;

                }

            }

        }

        public string[] getAllTypes(List<Card> cards)
        {
            List<string> temp = new List<string>();
            foreach (Card card in cards)
            {
                temp.Add(card.Type);
            }
            return temp.ToArray();
        }


        public void createCard()
        {
        }
    }
}
