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
                    selectedOrTempCards.Add(c);
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
                    selectedOrTempCards.Add(c);
                }
            }
            else if (effect.EffectConditionsTarget == Target.playerLeft)
            {
                int numPlayers = playerList.Count();
                Temp = playerList[(player + numPlayers - 1) % numPlayers].Show(effect.NumberOfEffects);
                foreach (Card c in Temp)
                {
                    selectedOrTempCards.Add(c);
                }
            }
            else if (effect.EffectConditionsTarget == Target.playerRight)
            {
                int numPlayers = playerList.Count();
                Temp = playerList[(player + numPlayers + 1) % numPlayers].Show(effect.NumberOfEffects);
                foreach (Card c in Temp)
                {
                    selectedOrTempCards.Add(c);
                }
            }
        }

        public List<Card> getFreeCard(CardEffect effect)
        {
            List<Card> aquired = new List<Card>();
            int value = effect.FreeValue;
            int numCards = effect.NumberOfEffects;
            //Get free cards from main deck 1,2,3

            List<Card> available = mainDeckList[effect.targetIndexLocation].getBuyableCards();
            Card selected;
            List<int> selectedIndex;

            //TODO
            //Need interface for buying cards

            if (effect.NumberOfEffects > 1)
            {

            }
            else
            {

            }


            return aquired;
        }

        public void destroyRandomCard(CardEffect effect)
        {
            bool fullLoop = true;
            bool otherOnly = effect.EffectConditionsTarget == Target.others;
            bool selfOnly = effect.EffectConditionsTarget == Target.self;
            if (otherOnly) fullLoop = false;
            int prePlayer = (player + playerList.Count - 1) % playerList.Count;

            for (int i = (player + playerList.Count + 1) % playerList.Count; !fullLoop ; i++)
            {
                if (selfOnly) i = player;
                if (i < playerList.Count) i = 0;
                if (i == player || (i == prePlayer && otherOnly))
                {
                    fullLoop = false;
                }
                for (int j = 0; j < effect.NumberOfEffects; j++)
                {
                    Card destroyed = playerList[i].destroy(true);
                    mainDeckList[0].addToDestroyed(destroyed);
                }

            }

        }
    }
}

