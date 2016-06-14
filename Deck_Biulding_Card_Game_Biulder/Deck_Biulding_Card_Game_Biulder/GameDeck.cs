using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck_Biulding_Card_Game_Biulder
{
    class GameDeck : DeckBaseClass
    {
        List<Card> ShowList = new List<Card>();
        public bool Refill()
        {
            while(AvailableCards.Count < AvailableCards.Count)
            {
                if (DeckList.Count == 0)
                {
                    return false;
                }
                
                Card temp = DeckList[0];
                DeckList.RemoveAt(0);
                AvailableCards.Add(temp);
            }
            return true;
        }

        public bool Show(int number = 1)
        {
            
            for(int i = 0;i < number; i++)
            {
                if (DeckList.Count == 0)
                {
                    return false;
                }

                ShowList.Add(DeckList[0]);
                DeckList.RemoveAt(0);
            }
            return true;
        }
        public void ReturnShown()
        {
            int number = ShowList.Count;
            for (int i = 0; i < number; i++)
            {
                DeckList.Add(ShowList[0]);
                ShowList.RemoveAt(0);
            }
        }

        public void GameStartUp()
        {
            DeckList = shuffle(DeckList);
        }

        public override void drawAttemptFinish()
        {
            //End Game
            throw new NotImplementedException();
        }
    }
}
