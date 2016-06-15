using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck_Biulding_Card_Game_Biulder
{
    public class GameController
    {
        List<PlayerDeck> playerList;
        List<GameDeck> mainDeckList;
        List<Card> removedCards;
        List<Card> startingHand;
        int player = 0;

        //Events need to be able to link together Exp. peek at top 2 cards draw if different types draw = peekSelf * 2 + typeDifDraw maybe have events take arguments of type object and cast to type expected?
        

        public GameController()
        {
            playerList = new List<PlayerDeck>();
            mainDeckList = new List<GameDeck>();
            removedCards = new List<Card>();
            startingHand = new List<Card>();
        }

        public void addStarterCard(Card card, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                startingHand.Add(card);
            }
        }

        public void playCard(Card cd)
        {
           
        }


        public object cardEvent(events EventName)
        {

        }

        public object cardEvent(events EventName, int[] Args)
        {
            switch (EventName)
            {
                case events.draw:
                    {
                        int times = 0;
                        do
                        {

                            playerList[player].draw();

                        } while (Args != null || Args[0] != ++times);
                    }
                    return null;
            }

            return null;
        }

        public object cardEvent(events EventName, string[] Args)
        {
            switch (EventName)
            {
                case events.TypeDraw:
                    {
                        int indexEnd = 1;
                        var newArray = new int[Args.Length - 1];
                        Array.Copy(Args, 1, newArray, 0, newArray.Length);
                        
                        if (newArray.Distinct().Count() == newArray.Count() && Args[0] == "Different" ||
                            newArray.Distinct().Count() == 1 && Args[0] == "Same")
                        {
                            playerList[player].draw();
                        }
{
                    }
            }

        }

        public object cardEvent(events EventName, Card[] Args)
        {
            switch (EventName)
            {
            }

        }

        public void createCard()
        {
        }
    }
}
