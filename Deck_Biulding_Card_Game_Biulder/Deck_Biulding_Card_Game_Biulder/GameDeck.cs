using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck_Biulding_Card_Game_Biulder
{
    class GameDeck : DeckBaseClass
    {
        int GameDeckNum;
        int LineUpCound;
        List<Card> DeckList = new List<Card>();
        List<Card> ActiveLineUp = new List<Card>();
        List<Card> ShowList = new List<Card>();
        public bool Refill()
        {
            while(ActiveLineUp.Count < LineUpCound)
            {
                if (DeckList.Count == 0)
                {
                    return false;
                }
                Card temp = new Card("",2);
                temp = DeckList[0];
                DeckList.RemoveAt(0);
                ActiveLineUp.Add(temp);
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

        public override void drawAttemptFinish()
        {
            throw new NotImplementedException();
        }
    }
}
