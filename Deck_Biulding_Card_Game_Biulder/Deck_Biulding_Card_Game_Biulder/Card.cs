using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck_Biulding_Card_Game_Biulder
{
    public class Card
    {
        public string Name
        {
            get;
            private set;
        }
        public int Points
        {
            get;
            private set;
        }
        public int ID
        {
            get;
            private set;
        }
        public int Owner
        {
            get;
            private set;
        }

        public List<Tuple<int, int>> Effects
        {
            get;
            private set;
        }

        //something
        public Card(string aName, int aID, int aPoints)
        {
            this.Name = aName;
            this.ID = aID;
            this.Effects = new List<Tuple<int, int>>();
            this.Points = aPoints;
        }
        
        public void AddEffect(int aEffect, int aTimes)
        {
            Effects.Add(new Tuple<int,int>(aEffect, aTimes));
        }

        public void removeEffect(int aEffect)
        {
            Effects.RemoveAll(x => x.Item1 == aEffect);
        }

        public void updateOwner(int aOwner)
        {
            this.Owner = aOwner;
        }
    }
}
