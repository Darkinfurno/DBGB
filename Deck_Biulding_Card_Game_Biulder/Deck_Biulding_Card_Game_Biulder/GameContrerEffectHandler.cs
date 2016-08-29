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
            List<Card> Temp;
            //main deck, my deck, all other players, player left, player right, (not all players)
            if (effect.EffectConditionsTarget == Target.self || effect.EffectConditionsTarget == Target.allPlayers)
            {
                Temp = playerList[player].Show(effect.NumberOfEffects);
                foreach (Card c in Temp)
                {
                    selectedOrTempCards.Add(c);
                }
            }
            else if(effect.EffectConditionsTarget == Target.others)
            {
                for(int i = 0; i < playerList.Count(); i++)
                {
                    if (player == i) i++;
                    Temp = playerList[i].Show(effect.NumberOfEffects);
                }
                foreach (Card c in Temp)
                {
                    selectedOrTempCards.Add(c);
                }
            }

        }

    }

}

