using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck_Biulding_Card_Game_Biulder
{
    public partial class GameController
    {
        public List<PlayerDeck> playerList { get; private set; }
        public List<GameDeck> mainDeckList{ get; private set; }
        public List<Card> removedCards{ get; private set; }
        public List<Card> startingDeck{ get; private set; }
        public List<Card> selectFromCards{ get; private set; }
        public List<int> buyPower{ get; private set; }
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

        //Returns the array of players to be effected if the effect is an attack it resolves the defences of the players first then returns the list of the non-defeneded players
        public int[] getEffectedPlayers(CardEffect effect)
        {

            bool fullLoop = true;
            bool otherOnly = effect.EffectConditionsTarget == Target.others;
            bool selfOnly = effect.EffectConditionsTarget == Target.self;
            if (otherOnly) fullLoop = false;
            int prePlayer = (player + playerList.Count - 1) % playerList.Count;
            List<int> Result = new List<int>();
            for (int i = prePlayer; !fullLoop; i++)
            {
                if (selfOnly) i = player;
                if (i == playerList.Count) i = 0;
                if (i == player || (i == prePlayer && otherOnly))
                {
                    fullLoop = false;
                }
                if (effect.EffectType == Special.attack && !processDefence(i)) Result.Add(i);
                else Result.Add(i);
            }
            return Result.ToArray();
        }

        public bool processDefence(int defendingPlayer)
        {
            List<Card> available = findAllOfEffectType(defendingPlayer, Special.defend);
            if (available.Count == 0) return false;
            selectFromCards = available;
            Card playedDefence = selectCard(false, defendingPlayer)[0];
            if (playedDefence == null) return false;
            int currentPlayer = player;
            player = defendingPlayer;
            ManageEffects(playedDefence, true);
            player = currentPlayer;
            return true;
        }

        public void PlayCardAt(int aIndex)
        {

            Card lPlayedCard = playerList[player].playcard(aIndex);
            for(int i = 0; i < lPlayedCard.Power.Count(); i++)
            {
                buyPower[i] += lPlayedCard.Power[i];
            }

        }

        public void ManageEffects(Card aCard, bool isDefence = false)
        {
            
            foreach (CardEffect cef in aCard.Effects)
            {
                if ((cef.EffectType != Special.defend && !isDefence || (cef.EffectType == Special.defend && isDefence)))
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
                            playerList[player].addCardsTo(getFreeCardByType(cef), cef.SelfTargetedDeckType);
                            break;
                        case events.Discard:
                            cardEventDiscard(cef);
                            break;
                        case events.DiscardOfAboveBelowValue:
                            cardEventValueBasedDiscard(cef);
                            break;
                        case events.DiscardOfType:
                            cardEventTypeBasedDiscard(cef);
                            break;
                        case events.DrawIfEvenOdd:
                            cardEventEvenOddBasedDraw(cef);
                            break;
                        case events.DrawIfAboveBelow:
                            cardEventValueBasedDraw(cef);
                            break;
                        case events.DrawIfTypesMatch:
                            cardEventTypeBasedDraw(cef);
                            break;
                        case events.Peek:
                            processPeek(cef);
                            break;
                        case events.Destroy:
                            cardEventDestroyCard(cef, false);
                            break;
                        case events.DestroyRandom:
                            cardEventDestroyCard(cef, true);
                            break;
                        case events.PassCard:
                            passCardToPlayer(cef);
                            break;
                        case events.DrawDiscardedAboveBelowValue:
                            cardEventValueBasedRetrieveDiscarded(cef);
                            break;
                        case events.DrawDiscardedType:
                            cardEventsTypeBasedDiscardedDraw(cef);
                            break;
                        case events.DrawDiscardedTypeAboveBelowValue:
                            cardEventsValueAndTypeBasedDiscard(cef);
                            break;
                        case events.endEffect:
                            selectFromCards.Clear();
                            break;
                        case events.AddPowerEvenOddPeek:
                            cardEventAddPowerEvenOddPeek(cef);
                            break;
                        case events.AddPowerTypePlayed:
                            cardEventTypeBasedAddPower(cef);
                            break;
                        case events.AddPowerTypePeek:
                            cardEventTypeBasedAddPowerPeek(cef);
                            break;
                        case events.AddPowerTypeInDiscard:
                            cardEventDiscardedTypeBasedAddPower(cef);
                            break;
                        case events.AddPowerIfDestroy:
                            cardEventAddPowerIfDestroy(cef);
                            break;

                    }
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

        public List<Card> findAllOfEffectType(int player, Special Type)
        {
            return playerList[player].AvailableCards.FindAll(x => x.Effects.FindAll(y => y.EffectType == Type).Count > 0);
        }

        public void createCard()
        {

        }

        //Must Select is a determinating factor if the cancel button is active on the form.
        public List<Card> selectCard(bool mustSelect, int targetPlayer, int selectNumber = 1)
        {
            int selectedIndex = 0;//get index from select card display window
            bool cancel = false; // if selection is canceled then no card is returned or in this case a null card
            //Call to display selectable cards Form
            List<Card> returned = new List<Card>();
            for (int i = 0; i < selectNumber; i++)
            {
                if (cancel)
                {
                    return null;
                }
                returned.Add(selectFromCards[selectedIndex]);
                selectFromCards.RemoveAt(selectedIndex);
            }
            return returned;
        }

        public int selectCardIndex(bool mustSelect, int targetPlayer)
        {
            int selectedIndex = 0;//get index from select card display window
            bool cancel = false; // if selection is canceled then no card is returned or in this case a null card
             //Call to display selectable cards Form


            if (cancel) return -1;
            return selectedIndex;
        }
    }
}
