﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck_Biulding_Card_Game_Biulder
{
    public abstract class DeckBaseClass
    {
        protected List<Card> Deck = new List<Card>();           //Deck      Deck
        public List<Card> AvailableCards = new List<Card>(); //Hand      Buyable
        public List<Card> RemovedCards = new List<Card>();   //Discard   Destroyed
        protected List<Card> ShowList = new List<Card>();       //peeking   peeking
        protected int availableNum;
        public void draw()
        {
            if (Deck.Count > 0)
            {
                AvailableCards.Add(Deck[0]);
                Deck.RemoveAt(0);
                return;
            }
            drawAttemptFinish();

        }

        public void addCardsTo(List<Card> cards, TargetDeckType target)
        {
            List<Card> addTo;
            switch (target)
            {
                case TargetDeckType.availableCards:
                    addTo = AvailableCards;
                    break;
                case TargetDeckType.deck:
                    addTo = Deck;
                    break;
                case TargetDeckType.removedCards:
                    addTo = RemovedCards;
                    break;
                default:
                    addTo = new List<Card>();
                    break;
            }
            addTo.InsertRange(0, cards);
        }

        public void refillCards()
        {
            while (AvailableCards.Count < availableNum)
            {
                draw();
            }
        }

        public int AvailableNum
        {
            get;
            set;
        }
        public abstract void drawAttemptFinish();

        public void shuffle()
        {
            List<Card> source = RemovedCards;
            var rnd = new Random();
            var result = source.OrderBy(item => rnd.Next());
            Deck = (List<Card>)result;
            RemovedCards.Clear();
        }

        public List<Card> Show(int number = 1)
        {
            ShowList.Clear();
            bool checkEmpty = true;
            for (int i = 0; i < number; i++)
            {
                if (Deck.Count == 0 && checkEmpty)
                {
                    shuffle();
                }
                else if (Deck.Count == 0)
                {

                    return ShowList;
                }

                ShowList.Add(Deck[0]);
                Deck.RemoveAt(0);
            }
            return ShowList;
        }
        public void ReturnShown()
        {
            foreach (Card c in ShowList)
            {
                Deck.Insert(0, c);
            }
            ShowList.Clear();
        }

        public Card destroy(bool random)
        {

            int numOfAvailable = AvailableCards.Count();
            Random rdm = new Random();
            int index = rdm.Next(0, numOfAvailable);
            Card destroyed = AvailableCards[index];
            AvailableCards.RemoveAt(index);
            return destroyed;

        }
        public Card destroy(int location)
        {
            Card destroyed = AvailableCards[location];
            AvailableCards.RemoveAt(location);
            return destroyed;
        }
    }
}
