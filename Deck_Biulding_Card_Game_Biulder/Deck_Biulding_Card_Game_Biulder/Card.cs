using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck_Biulding_Card_Game_Biulder
{
    public class Card
    {
        private string Name
        {
            get;
            private set;
        }
        private int ID
        {
            get;
            private set;
        }
        private int Owner
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
        public void Card(string aName, int aID)
        {
            this.Name = aName;
            this.ID = aID;
            this.Effects = new List<Tuple<int, int>>();
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
