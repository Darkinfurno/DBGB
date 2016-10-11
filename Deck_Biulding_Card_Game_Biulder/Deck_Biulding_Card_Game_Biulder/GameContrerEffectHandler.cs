using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck_Biulding_Card_Game_Biulder
{
    public partial class GameController
    {

        public void cardEventDraw(CardEffect effect)
        {
            for (int i = 0; i < effect.NumberOfEffects; i++)
            {
                playerList[player].draw();
            }
        }


        public void cardEventTypeBasedDraw(CardEffect effect, string[] Args)
        {

            if (Args.Distinct().Count() == Args.Count() && effect.EffectMatchCondition == MatchCondition.different ||
                Args.Distinct().Count() == 1 && effect.EffectMatchCondition == MatchCondition.same)
            {
                cardEventDraw(effect);
            }
            else if (Args.Count() == 1 && effect.EffectConditionText == Args[0])
            {
                cardEventDraw(effect);
            }

        }

        public void processPeek(CardEffect effect)
        {
            List<Card> Temp = new List<Card>();
            //main deck, my deck, all other players, player left, player right, (not all players)
            if (effect.EffectConditionsTarget == Target.self || effect.EffectConditionsTarget == Target.allPlayers)
            {
                Temp = playerList[player].Show(effect.NumberOfEffects);
                foreach (Card c in Temp)
                {
                    selectFromCards.Add(c);
                }
            }
            else if (effect.EffectConditionsTarget == Target.others || effect.EffectConditionsTarget == Target.allPlayers)
            {
                for (int i = 0; i < playerList.Count(); i++)
                {
                    if (player == i) i++;
                    Temp = playerList[i].Show(effect.NumberOfEffects);
                }
                foreach (Card c in Temp)
                {
                    selectFromCards.Add(c);
                }
            }
            else if (effect.EffectConditionsTarget == Target.playerLeft)
            {
                int numPlayers = playerList.Count();
                Temp = playerList[(player + numPlayers - 1) % numPlayers].Show(effect.NumberOfEffects);
                foreach (Card c in Temp)
                {
                    selectFromCards.Add(c);
                }
            }
            else if (effect.EffectConditionsTarget == Target.playerRight)
            {
                int numPlayers = playerList.Count();
                Temp = playerList[(player + numPlayers + 1) % numPlayers].Show(effect.NumberOfEffects);
                foreach (Card c in Temp)
                {
                    selectFromCards.Add(c);
                }
            }
        }

        public List<Card> getFreeCardByValue(CardEffect effect)
        {

            List<Card> aquired = new List<Card>();
            int value = effect.Value;
            int numCards = effect.NumberOfEffects;
            //Get free cards from main deck 1,2,3

            for (int i = 0; i < effect.NumberOfEffects || (effect.FreeByTotalValue && value > 0); i++)
            {
                List<Card> available = mainDeckList[effect.targetIndexLocation].getBuyableCards();
                available.RemoveAll(x => x.Cost[effect.TargetIndex] > value);
                if (available.Count == 0) break;
                selectFromCards = available;
                Card selected = selectCard(false, player);
                if (selected == null) break;
                if (effect.FreeByTotalValue) value -= selected.Cost[effect.TargetIndex];
                aquired.Add(selected);
            }

            return aquired;
        }

        public List<Card> getFreeCardByType(CardEffect effect)
        {
            List<Card> aquired = new List<Card>();
            string value = effect.TargetCardType;
            int numCards = effect.NumberOfEffects;
            //Get free cards from main deck 1,2,3

            for (int i = 0; i < effect.NumberOfEffects || effect.EffectConditionsValue == ValueEvenOdd.all; i++)
            {
                List<Card> available = mainDeckList[effect.targetIndexLocation].getBuyableCards();
                available.RemoveAll(x => x.Type == value);
                if (available.Count == 0) break;
                selectFromCards = available;
                Card selected = selectCard(false, player);
                if (selected == null) break;
                aquired.Add(selected);
            }

            return aquired;
        }

        public void cardEventDestroyCard(CardEffect effect, bool random)
        {
            int[] players = getEffectedPlayers(effect);

            for (int i = 0; i < players.Length; i++)
            {

                for (int j = 0; j < effect.NumberOfEffects; j++)
                {
                    if (playerList[players[i]].AvailableCards.Count == 0) break;
                    selectFromCards = playerList[players[i]].AvailableCards;
                    Card destroyed = (random == true) ? playerList[players[i]].destroy(true) : playerList[players[i]].destroy(selectCardIndex(true, players[i]));
                    mainDeckList[0].addToDestroyed(destroyed);
                }
            }
        }

        private void cardEventDiscard(CardEffect effect)
        {
            int[] players = getEffectedPlayers(effect);

            for (int i = 0; i < players.Length; i++)
            {
                selectFromCards = playerList[players[i]].AvailableCards;
                for (int j = 0; j < effect.NumberOfEffects; j++)
                {
                    if (selectFromCards.Count == 0) break;
                    playerList[players[i]].RemovedCards.Add(playerList[players[i]].destroy(selectCardIndex(true, players[i])));
                }
            }
        }

        public void cardEventValueBasedDiscard(CardEffect effect)
        {
            int value = effect.Value;

            int[] players = getEffectedPlayers(effect);

            for (int i = 0; i < players.Length; i++)
            {
                if (effect.ValueTarget == ValueRangeAboveBelow.none) selectFromCards = playerList[players[i]].AvailableCards.FindAll(x => x.Cost[effect.TargetIndex] == value);
                else if (effect.ValueTarget == ValueRangeAboveBelow.above) selectFromCards = playerList[players[i]].AvailableCards.FindAll(x => x.Cost[effect.TargetIndex] > value);
                else selectFromCards = playerList[players[i]].AvailableCards.FindAll(x => x.Cost[effect.TargetIndex] < value);
                for (int j = 0; j < effect.NumberOfEffects; j++)
                {
                    if (selectFromCards.Count == 0) break;
                    Card discard = selectCard(true, i);
                    playerList[players[i]].AvailableCards.Remove(discard); // may not need this
                    playerList[players[i]].RemovedCards.Add(discard);
                }
            }
        }

        public void cardEventTypeBasedDiscard(CardEffect effect)
        {
            int[] players = getEffectedPlayers(effect);

            for (int i = 0; i < players.Length; i++)
            {
                selectFromCards = playerList[players[i]].AvailableCards.FindAll(x => x.Type == effect.TargetCardType);
                for (int j = 0; j < effect.NumberOfEffects; j++)
                {
                    if (playerList[i].AvailableCards.Count == 0) break;
                    Card discard = selectCard(true, i);
                    playerList[players[i]].AvailableCards.Remove(discard); // may not need this
                    playerList[players[i]].RemovedCards.Add(discard);
                }
            }
        }

        public void cardEventEvenOddBasedDraw(CardEffect effect)
        {
            int[] players = getEffectedPlayers(effect);

            for (int i = 0; i < players.Length; i++)
            {

                List<Card> drawn = playerList[players[i]].Show(effect.NumberOfEffects);
                List<Card> match = drawn.FindAll(x => (x.Cost[effect.TargetIndex] % 2 == 0));
                if (effect.EffectConditionsValue == ValueEvenOdd.even && drawn.Count() == match.Count()) playerList[players[i]].addCardsTo(drawn, TargetDeckType.availableCards);
                else if (effect.EffectConditionsValue == ValueEvenOdd.odd && match.Count == 0) playerList[players[i]].addCardsTo(drawn, TargetDeckType.availableCards);
                else playerList[players[i]].ReturnShown();

            }
        }

        public void cardEventValueBasedDraw(CardEffect effect)
        {
            int[] players = getEffectedPlayers(effect);

            for (int i = 0; i < players.Length; i++)
            {

                List<Card> drawn = playerList[players[i]].Show(effect.NumberOfEffects);
                List<Card> match = drawn.FindAll(x => (effect.ValueTarget == ValueRangeAboveBelow.above ? x.Cost[effect.TargetIndex] > effect.Value : x.Cost[effect.TargetIndex] < effect.Value));
                if (effect.EffectCondition == CondidionToExecute.allMustMatch && drawn.Count == match.Count) playerList[players[i]].addCardsTo(drawn, TargetDeckType.availableCards);
                else if (effect.EffectCondition == CondidionToExecute.ifAnyMatch)
                {
                    drawn.RemoveAll(x => match.Exists(y => y.Name == x.Name)); //Hope this logic works right
                    playerList[players[i]].addCardsTo(match, TargetDeckType.availableCards);
                    playerList[players[i]].addCardsTo(drawn, TargetDeckType.deck);
                }
                else playerList[players[i]].ReturnShown();

            }
        }

        public void cardEventValueBasedRetrieveDiscarded(CardEffect effect)
        {
            int[] players = getEffectedPlayers(effect);

            for (int i = 0; i < players.Length; i++)
            {

                selectFromCards = playerList[players[i]].RemovedCards.FindAll(x => effect.ValueTarget == ValueRangeAboveBelow.above ? x.Cost[effect.TargetIndex] > effect.Value : x.Cost[effect.TargetIndex] < effect.Value);
                if (effect.SelectionType == CardSelectionType.forceAllCards) playerList[players[i]].addCardsTo(selectFromCards, TargetDeckType.availableCards);
                else
                {
                    int inumberation = effect.SelectionType == CardSelectionType.upToAllCards ? selectFromCards.Count() : effect.NumberOfEffects;
                    for (int j = 0; j < inumberation; j++)
                    {
                        if (selectFromCards.Count == 0) break;
                        Card selected = selectCard(false, i);
                        if (selected == null) break;
                        playerList[players[i]].AvailableCards.Add(selected);
                        playerList[players[i]].RemovedCards.Remove(selected);
                    }
                }
            }
        }

        public void cardEventsTypeBasedDiscardedDraw(CardEffect effect)
        {
            int[] players = getEffectedPlayers(effect);

            for (int i = 0; i < players.Length; i++)
            {

                selectFromCards = playerList[players[i]].RemovedCards.FindAll(x => x.Type == effect.TargetCardType);
                if (effect.SelectionType == CardSelectionType.forceAllCards) playerList[players[i]].addCardsTo(selectFromCards, TargetDeckType.availableCards);
                else
                {
                    int inumberation = effect.SelectionType == CardSelectionType.upToAllCards ? selectFromCards.Count() : effect.NumberOfEffects;
                    for (int j = 0; j < inumberation; j++)
                    {
                        if (selectFromCards.Count == 0) break;
                        Card selected = selectCard(false, i);
                        if (selected == null) break;
                        playerList[players[i]].AvailableCards.Add(selected);
                        playerList[players[i]].RemovedCards.Remove(selected);
                    }
                }
            }
        }

        public void cardEventsValueAndTypeBasedDiscard(CardEffect effect)
        {
            int[] players = getEffectedPlayers(effect);

            for (int i = 0; i < players.Length; i++)
            {

                selectFromCards = playerList[players[i]].RemovedCards.FindAll(x => x.Type == effect.TargetCardType && (effect.ValueTarget == ValueRangeAboveBelow.above ? x.Cost[effect.TargetIndex] > effect.Value : x.Cost[effect.TargetIndex] < effect.Value));
                if (effect.SelectionType == CardSelectionType.forceAllCards) playerList[players[i]].addCardsTo(selectFromCards, TargetDeckType.availableCards);
                else
                {
                    int inumberation = effect.SelectionType == CardSelectionType.upToAllCards ? selectFromCards.Count() : effect.NumberOfEffects;
                    for (int j = 0; j < inumberation; j++)
                    {
                        if (selectFromCards.Count == 0) break;
                        Card selected = selectCard(false, i);
                        if (selected == null) break;
                        playerList[players[i]].AvailableCards.Add(selected);
                        playerList[players[i]].RemovedCards.Remove(selected);
                    }
                }
            }
        }

        public void passCardToPlayer(CardEffect effect)
        {
            int playerCount = playerList.Count;
            List<Card>[] toMove = new List<Card>[playerCount];
            int shiftToPlayer = effect.EffectConditionsTarget == Target.playerLeft ? effect.TargetIndex * 1 : effect.TargetIndex * -1;

            int[] players = getEffectedPlayers(effect);

            for (int i = 0; i < players.Length; i++)
            {
                selectFromCards = playerList[players[i]].AvailableCards;
                for (int j = 0; j < effect.NumberOfEffects; j++)
                {
                    if (selectFromCards.Count == 0) break;
                    toMove[players[i]].Add(selectCard(true, i));

                }
            }
            for (int i = 0; i < playerCount; i++)
            {
                playerList[(players[i] + playerCount + shiftToPlayer) % playerCount].addCardsTo(toMove[players[i]], effect.SelfTargetedDeckType);
            }
        }
    }
}

