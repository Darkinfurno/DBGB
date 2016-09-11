using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck_Biulding_Card_Game_Biulder
{
    /// <summary>
    /// Class to represent a CardObject
    /// </summary>
    public class Card
    {
        // Represents the name of the Card
        public string Name
        {
            get;
            private set;
        }

        // Represents the type or some other designation of the Card
        public string Type
        {
            get;
            private set;
        }

        // Represents how many points the card is worth at the end of the game to determine winner
        public int Points
        {
            get;
            private set;
        }

        // Identification number for Card Object
        //public int ID
        //{
        //    get;
        //    private set;
        //}

        // Integer to represent the current owner of the card
        public int Owner
        {
            get;
            private set;
        }

        // List that represents the card effects that the card object contains
        public List<CardEffect> Effects
        {
            get;
            private set;
        }

        public List<CardEffect> SpecialEffects
        {
            get;
            private set;
        }

        // Card Constructor
        //public Card(string aName, string aType,int aPoints, int aID, int aOwner, List<CardEffect> aCardEffects = null, List<CardEffect> aCardSpecialEffects = null)
        public Card(string aName, string aType, int aPoints, int aOwner, List<CardEffect> aCardEffects = null, List<CardEffect> aCardSpecialEffects = null)
        {
            this.Name = aName;
            this.Type = aType;
            this.Points = aPoints;
            //this.ID = aID;
            this.Owner = aOwner;
            if (aCardEffects == null)
            {
                this.Effects = new List<CardEffect>();
            }
            else this.Effects = aCardEffects;

            if (aCardSpecialEffects == null)
            {
                this.SpecialEffects = new List<CardEffect>();
            }
            else this.SpecialEffects = aCardSpecialEffects;


        }

        public void addEffect(events aEffect, int aTimes)
        {
            Effects.Add(new CardEffect(aEffect, aTimes));
        }

        public void addSpecialEffect(events aEffect, int aTimes)
        {
            Effects.Add(new CardEffect(aEffect, aTimes));
        }


        public void removeEffect(events aEffect)
        {
            Effects.RemoveAll(x => x.Effect == aEffect);
        }

        public void updateOwner(int aOwner)
        {
            this.Owner = aOwner;
        }
    }
}
