using System;
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

        public int FreeValue
        {
            get;
            private set;
        }

        public string FreeCardType
        {
            get;
            private set;
        }

        //Cost Type i.e. Cost[type 0, type 1, ...]
        public int FreeValueCostType
        {
            get;
            private set;
        }
        public int targetIndexLocation { get; private set; }

        public bool FreeByTotalValue // true = any number of cards : false = only one card
        {
            get;
            private set;
        }

        public string EffectConditionText
        {
            get;
            private set; 
        }

        public MatchCondition EffectMatchCondition
        {
            get;
            private set;
        }

        public Value EffectConditionsValue
        {
            get;
            private set;
        }

        public Target EffectConditionsTarget
        {
            get;
            private set;
        }

        public TargetDeckType EffectConditionsTargetedDeckType
        {
            get;
            private set;
        }

        public TargetDeckType SelfTargetedDeckType
        {
            get;
            private set;
        }

        public CardEffect(events ev, int num,
            MatchCondition ect = MatchCondition.none,
            Value ecv = Value.none)
        {
            Effect = ev;
            NumberOfEffects = num;
            EffectMatchCondition = ect;
            EffectConditionsValue = ecv;
        }
    }
}
