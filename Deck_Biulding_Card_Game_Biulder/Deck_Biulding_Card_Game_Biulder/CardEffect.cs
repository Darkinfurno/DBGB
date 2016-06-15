﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck_Biulding_Card_Game_Biulder
{
    /// <summary>
    /// Class to encapsulate card effects and number of times card effect is applied for current card object
    /// </summary>
    public class CardEffect
    {
        public events Effect
        {
            get;
            private set;
        }

        public int NumberOfEffects
        {
            get;
            private set;
        }

        public string 
        {
            get;
            private set;
        }

        public CardEffect(events ev, int num)
        {
            Effect = ev;
            NumberOfEffects = num;
        }
    }
}