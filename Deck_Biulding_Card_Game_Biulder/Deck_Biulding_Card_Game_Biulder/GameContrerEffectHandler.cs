﻿using System;
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
            bool fullLoop = true;
            bool otherOnly = effect.EffectConditionsTarget == Target.others;
            bool selfOnly = effect.EffectConditionsTarget == Target.self;
            if (otherOnly) fullLoop = false;
            int prePlayer = (player + playerList.Count - 1) % playerList.Count;

            for (int i = (player + playerList.Count + 1) % playerList.Count; !fullLoop; i++)
            {
                if (selfOnly) i = player;
                if (i < playerList.Count) i = 0;
                if (i == player || (i == prePlayer && otherOnly))
                {
                    fullLoop = false;
                }
                for (int j = 0; j < effect.NumberOfEffects; j++)
                {
                    if (playerList[i].AvailableCards.Count == 0) break;
                    selectFromCards = playerList[i].AvailableCards;
                    Card destroyed = (random == true) ? playerList[i].destroy(true) : playerList[i].destroy(selectCardIndex(true, i));
                    mainDeckList[0].addToDestroyed(destroyed);
                }
            }
        }

        private void cardEventDiscard(CardEffect effect)
        {
            bool fullLoop = true;
            bool otherOnly = effect.EffectConditionsTarget == Target.others;
            bool selfOnly = effect.EffectConditionsTarget == Target.self;
            if (otherOnly) fullLoop = false;
            int prePlayer = (player + playerList.Count - 1) % playerList.Count;

            for (int i = (player + playerList.Count + 1) % playerList.Count; !fullLoop; i++)
            {
                if (selfOnly) i = player;
                if (i < playerList.Count) i = 0;
                if (i == player || (i == prePlayer && otherOnly))
                {
                    fullLoop = false;
                }
                selectFromCards = playerList[i].AvailableCards;
                for (int j = 0; j < effect.NumberOfEffects; j++)
                {
                    if (selectFromCards.Count == 0) break;
                    playerList[i].RemovedCards.Add(playerList[i].destroy(selectCardIndex(true, i)));
                }
            }
        }

        public void cardEventValueBasedDiscard(CardEffect effect)
        {
            int value = effect.Value;

            bool fullLoop = true;
            bool otherOnly = effect.EffectConditionsTarget == Target.others;
            bool selfOnly = effect.EffectConditionsTarget == Target.self;
            if (otherOnly) fullLoop = false;
            int prePlayer = (player + playerList.Count - 1) % playerList.Count;

            for (int i = (player + playerList.Count + 1) % playerList.Count; !fullLoop; i++)
            {
                if (selfOnly) i = player;
                if (i < playerList.Count) i = 0;
                if (i == player || (i == prePlayer && otherOnly))
                {
                    fullLoop = false;
                }
                if (effect.ValueTarget == ValueRangeAboveBelow.none) selectFromCards = playerList[i].AvailableCards.FindAll(x => x.Cost[effect.TargetIndex] == value);
                else if (effect.ValueTarget == ValueRangeAboveBelow.above) selectFromCards = playerList[i].AvailableCards.FindAll(x => x.Cost[effect.TargetIndex] > value);
                else selectFromCards = playerList[i].AvailableCards.FindAll(x => x.Cost[effect.TargetIndex] < value);
                for (int j = 0; j < effect.NumberOfEffects; j++)
                {
                    if (selectFromCards.Count == 0) break;
                    Card discard = selectCard(true, i);
                    playerList[i].AvailableCards.Remove(discard); // may not need this
                    playerList[i].RemovedCards.Add(discard);
                }
            }
        }

        public void cardEventTypeBasedDiscard(CardEffect effect)
        {
            bool fullLoop = true;
            bool otherOnly = effect.EffectConditionsTarget == Target.others;
            bool selfOnly = effect.EffectConditionsTarget == Target.self;
            if (otherOnly) fullLoop = false;
            int prePlayer = (player + playerList.Count - 1) % playerList.Count;

            for (int i = (player + playerList.Count + 1) % playerList.Count; !fullLoop; i++)
            {
                if (selfOnly) i = player;
                if (i < playerList.Count) i = 0;
                if (i == player || (i == prePlayer && otherOnly))
                {
                    fullLoop = false;
                }
                selectFromCards = playerList[i].AvailableCards.FindAll(x => x.Type == effect.TargetCardType);
                for (int j = 0; j < effect.NumberOfEffects; j++)
                {
                    if (playerList[i].AvailableCards.Count == 0) break;
                    Card discard = selectCard(true, i);
                    playerList[i].AvailableCards.Remove(discard); // may not need this
                    playerList[i].RemovedCards.Add(discard);
                }
            }
        }

        public void cardEventEvenOddBasedDraw(CardEffect effect)
        {
            bool fullLoop = true;
            bool otherOnly = effect.EffectConditionsTarget == Target.others;
            bool selfOnly = effect.EffectConditionsTarget == Target.self;
            if (otherOnly) fullLoop = false;
            int prePlayer = (player + playerList.Count - 1) % playerList.Count;

            for (int i = (player + playerList.Count + 1) % playerList.Count; !fullLoop; i++)
            {
                if (selfOnly) i = player;
                if (i < playerList.Count) i = 0;
                if (i == player || (i == prePlayer && otherOnly))
                {
                    fullLoop = false;
                }

                List<Card> drawn = playerList[i].Show(effect.NumberOfEffects);
                List<Card> match = drawn.FindAll(x => (x.Cost[effect.TargetIndex] % 2 == 0));
                if (effect.EffectConditionsValue == ValueEvenOdd.even && drawn.Count() == match.Count()) playerList[i].addCardsTo(drawn, TargetDeckType.availableCards);
                else if (effect.EffectConditionsValue == ValueEvenOdd.odd && match.Count == 0) playerList[i].addCardsTo(drawn, TargetDeckType.availableCards);
                else playerList[i].ReturnShown();
                
            }
        }

        public void cardEventValueBasedDraw(CardEffect effect)
        {
            bool fullLoop = true;
            bool otherOnly = effect.EffectConditionsTarget == Target.others;
            bool selfOnly = effect.EffectConditionsTarget == Target.self;
            if (otherOnly) fullLoop = false;
            int prePlayer = (player + playerList.Count - 1) % playerList.Count;

            for (int i = (player + playerList.Count + 1) % playerList.Count; !fullLoop; i++)
            {
                if (selfOnly) i = player;
                if (i < playerList.Count) i = 0;
                if (i == player || (i == prePlayer && otherOnly))
                {
                    fullLoop = false;
                }

                List<Card> drawn = playerList[i].Show(effect.NumberOfEffects);
                List<Card> match = drawn.FindAll(x => (effect.ValueTarget == ValueRangeAboveBelow.above ? x.Cost[effect.TargetIndex] > effect.Value : x.Cost[effect.TargetIndex] < effect.Value));
                if (effect.EffectCondition == CondidionToExecute.allMustMatch && drawn.Count == match.Count) playerList[i].addCardsTo(drawn, TargetDeckType.availableCards);
                else if (effect.EffectCondition == CondidionToExecute.ifAnyMatch)
                {
                    drawn.RemoveAll(x => match.Exists(y => y.Name == x.Name)); //Hope this logic works right
                    playerList[i].addCardsTo(match, TargetDeckType.availableCards);
                    playerList[i].addCardsTo(drawn, TargetDeckType.deck);
                }
                else playerList[i].ReturnShown();

            }
        }

        public void cardEventValueBasedRetrieveDiscarded(CardEffect effect)
        {
            bool fullLoop = true;
            bool otherOnly = effect.EffectConditionsTarget == Target.others;
            bool selfOnly = effect.EffectConditionsTarget == Target.self;
            if (otherOnly) fullLoop = false;
            int prePlayer = (player + playerList.Count - 1) % playerList.Count;

            for (int i = (player + playerList.Count + 1) % playerList.Count; !fullLoop; i++)
            {
                if (selfOnly) i = player;
                if (i < playerList.Count) i = 0;
                if (i == player || (i == prePlayer && otherOnly))
                {
                    fullLoop = false;
                }

                selectFromCards = playerList[i].RemovedCards.FindAll(x => effect.ValueTarget == ValueRangeAboveBelow.above ? x.Cost[effect.TargetIndex] > effect.Value : x.Cost[effect.TargetIndex] < effect.Value);
                if (effect.SelectionType == CardSelectionType.forceAllCards) playerList[i].addCardsTo(selectFromCards, TargetDeckType.availableCards);
                else
                {
                    int inumberation = effect.SelectionType == CardSelectionType.upToAllCards ? selectFromCards.Count() : effect.NumberOfEffects;
                    for (int j = 0; j < inumberation; j++)
                    {
                        if (selectFromCards.Count == 0) break;
                        Card selected = selectCard(false, i);
                        if (selected == null) break;
                        playerList[i].AvailableCards.Add(selected);
                        playerList[i].RemovedCards.Remove(selected);
                    }
                }
            }
        }

        public void cardEventsTypeBasedDiscardedDraw(CardEffect effect)
        {
            bool fullLoop = true;
            bool otherOnly = effect.EffectConditionsTarget == Target.others;
            bool selfOnly = effect.EffectConditionsTarget == Target.self;
            if (otherOnly) fullLoop = false;
            int prePlayer = (player + playerList.Count - 1) % playerList.Count;

            for (int i = (player + playerList.Count + 1) % playerList.Count; !fullLoop; i++)
            {
                if (selfOnly) i = player;
                if (i < playerList.Count) i = 0;
                if (i == player || (i == prePlayer && otherOnly))
                {
                    fullLoop = false;
                }

                selectFromCards = playerList[i].RemovedCards.FindAll(x => x.Type == effect.TargetCardType);
                if (effect.SelectionType == CardSelectionType.forceAllCards) playerList[i].addCardsTo(selectFromCards, TargetDeckType.availableCards);
                else
                {
                    int inumberation = effect.SelectionType == CardSelectionType.upToAllCards ? selectFromCards.Count() : effect.NumberOfEffects;
                    for (int j = 0; j < inumberation; j++)
                    {
                        if (selectFromCards.Count == 0) break;
                        Card selected = selectCard(false, i);
                        if (selected == null) break;
                        playerList[i].AvailableCards.Add(selected);
                        playerList[i].RemovedCards.Remove(selected);
                    }
                }
            }
        }

        public void cardEventsValueAndTypeBasedDiscard(CardEffect effect)
        {
            bool fullLoop = true;
            bool otherOnly = effect.EffectConditionsTarget == Target.others;
            bool selfOnly = effect.EffectConditionsTarget == Target.self;
            if (otherOnly) fullLoop = false;
            int prePlayer = (player + playerList.Count - 1) % playerList.Count;

            for (int i = (player + playerList.Count + 1) % playerList.Count; !fullLoop; i++)
            {
                if (selfOnly) i = player;
                if (i < playerList.Count) i = 0;
                if (i == player || (i == prePlayer && otherOnly))
                {
                    fullLoop = false;
                }

                selectFromCards = playerList[i].RemovedCards.FindAll(x => x.Type == effect.TargetCardType && (effect.ValueTarget == ValueRangeAboveBelow.above ? x.Cost[effect.TargetIndex] > effect.Value : x.Cost[effect.TargetIndex] < effect.Value));
                if (effect.SelectionType == CardSelectionType.forceAllCards) playerList[i].addCardsTo(selectFromCards, TargetDeckType.availableCards);
                else
                {
                    int inumberation = effect.SelectionType == CardSelectionType.upToAllCards ? selectFromCards.Count() : effect.NumberOfEffects;
                    for (int j = 0; j < inumberation; j++)
                    {
                        if (selectFromCards.Count == 0) break;
                        Card selected = selectCard(false, i);
                        if (selected == null) break;
                        playerList[i].AvailableCards.Add(selected); 
                        playerList[i].RemovedCards.Remove(selected);
                    }
                }
            }
        }

        public void passCardToPlayer(CardEffect effect)
        {
            bool fullLoop = true;
            bool otherOnly = effect.EffectConditionsTarget == Target.others;
            bool selfOnly = effect.EffectConditionsTarget == Target.self;
            int playerCount = playerList.Count;
            if (otherOnly) fullLoop = false;
            int prePlayer = (player + playerCount - 1) % playerList.Count;
            int shiftToPlayer = effect.EffectConditionsTarget == Target.playerLeft ? effect.TargetIndex * 1 : effect.TargetIndex * -1;
            List <Card>[] toMove = new List<Card>[playerCount];
            for (int i = (player + playerCount + 1) % playerCount; !fullLoop; i++)
            {
                if (selfOnly) i = player;
                if (i < playerList.Count) i = 0;
                if (i == player || (i == prePlayer && otherOnly))
                {
                    fullLoop = false;
                }
                selectFromCards = playerList[i].AvailableCards;
                for(int j = 0; j< effect.NumberOfEffects; j++)
                {
                    if (selectFromCards.Count == 0) break;
                    toMove[i].Add(selectCard(true, i));

                }
            }
            for(int i = 0; i < playerCount; i++)
            {
                playerList[(i + playerCount + shiftToPlayer) % playerCount].addCardsTo(toMove[i], effect.SelfTargetedDeckType);
            }
        }
    }
}

