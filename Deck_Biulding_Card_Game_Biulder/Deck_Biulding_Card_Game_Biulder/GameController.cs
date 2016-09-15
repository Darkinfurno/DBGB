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
        List<Card> selectFromCards;
        List<int> buyPower;
        int player = 0;
        //Select Cards from effect could work where all cards that are valid to be selected from show up in a list and 1 is selected, if multiple then the form will display again.


        //Events need to be able to link together Exp. peek at top 2 cards draw if different types draw = peekSelf * 2 + typeDifDraw maybe have events take arguments of type object and cast to type expected?


        public GameController()
        {
            playerList = new List<PlayerDeck>();
            mainDeckList = new List<GameDeck>();
            removedCards = new List<Card>();
            startingDeck = new List<Card>();
            selectFromCards = new List<Card>(); //May need to move this gets reset lots ;)
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
                    case events.AquireFreeCardByValue:
                        playerList[player].addCardsTo(getFreeCardByValue(cef), cef.SelfTargetedDeckType);
                        break;
                    case events.AquireFreeCardByType:
                        //playerList[player].addCardsTo(getFreeCardByType(cef), cef.SelfTargetedDeckType);
                        break;
                    case events.Discard:
                        cardEventDiscard(cef);
                        break;
                    case events.DiscardIfCostNotZero:
                        //cardEventValueBasedDiscard(cef);
                        break;
                    case events.DiscardIfCostZero:
                        //cardEventValueBasedDiscard(cef);
                        break;
                    case events.DiscardIfEvenOdd:
                        //cardEventValueBasedDiscard(cef);
                        break;
                    case events.DiscardOfType:
                        //cardEventTypeBasedDiscard(cef);
                        break;
                    case events.DrawIfEvenOdd:
                        //cardEventValuebasedDraw(cef);
                        break;
                    case events.DrawIfType:
                        cardEventTypeBasedDraw(cef, getAllTypes(selectFromCards));
                        break;
                    case events.DrawIfTypesMatch:
                        cardEventTypeBasedDraw(cef, getAllTypes(selectFromCards));
                        break;
                    case events.Peek:
                        processPeek(cef);
                        break;
                    case events.Destroy:
                        //cardEventDestroy;
                        break;
                    case events.DestroyRandom:
                        cardEventdestroyRandomCard(cef);
                        break;
                    case events.PassCard:

                        break;
                    case events.winIf:

                        break;
                    case events.DrawDiscardedAboveBelowValue:
                        //cardEventValueBasedRetrieveDiscarded(cef);
                        break;
                    case events.DrawDiscardedType:
                        //cardEventsTypeBasedDiscard(cef);
                        break;
                    case events.DrawDiscardedTypeAboveBelowValue:
                        //cardEventsValueAndTypeBasedDiscard(cef);
                        break;
                    case events.endEffect:
                        selectFromCards.Clear();
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

        public List<int>[] getAllCosts(List<Card> cards)
        {
            List<List<int>> temp = new List<List<int>>();
            foreach (Card card in cards)
            {
                temp.Add(card.Cost);
            }
            return temp.ToArray();
        }

        public void createCard()
        {

        }

        //Must Select is a determinating factor if the cancel button is active on the form.
        public Card selectCard(bool mustSelect, int targetPlayer)
        {
            int selectedIndex = 0;//get index from select card display window
            bool cancel = false; // if selection is canceled then no card is returned or in this case a null card
            //Call to display selectable cards Form


            if (cancel) return null;
            Card selected = selectFromCards[selectedIndex];
            selectFromCards.RemoveAt(selectedIndex);
            return selected;
        }

        public int selectCardIndex(bool mustSelect, int targetPlayer)
        {
            int selectedIndex = 0;//get index from select card display window
            bool cancel = false; // if selection is canceled then no card is returned or in this case a null card
             //Call to display selectable cards Form


            if (cancel) return null;
            return selectedIndex;
        }
    }
}
