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
            for(int i = 0; i< effect.NumberOfEffects;i++)
            {
                playerList[player].draw();

            }
        }


        public void cardEventTypeBasedDraw(CardEffect effect, string[] Args)
        {

            if (Args.Distinct().Count() == Args.Count() && effect.EffectConditionsText == Text.different ||
                Args.Distinct().Count() == 1 && effect.EffectConditionsText == Text.same)
            {
                cardEventDraw(effect);
            }

        }

    }

}

