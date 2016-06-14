using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck_Biulding_Card_Game_Biulder
{
    class Card
    {
        List<Tuple<int, int>> Effects = new List<Tuple<int, int>>();
        string name;
        int id;
        Player owner;

        public void Card(string Name, int ID)
        {

        }
        
        public void AddEffect(int effect, int times)
        {
            Effects.Add(new Tuple<int,int>(effect,times));
        }

        public void removeEffect(int effect)
        {
            Effects.RemoveAll(x => x.Item1 == effect);
        }

        public void updateOwner(Player Owner)
        {

        }

        public List<Tuple<int,int>> _effects
        {
            get { return Effects; }
        }
    }
}
