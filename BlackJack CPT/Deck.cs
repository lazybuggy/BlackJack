using System;
using System.Drawing;
using System.Drawing.Drawing2D;
//Header Comments

namespace BlackJack
{
    class Deck
    {
        //Fields

        private Card[] deck;
        private int nextCard;
        public int length;

        //Constructors

        public Deck()
        {
            //create all 52 cards and assign them to the deck variable
            deck = new Card[52];
            nextCard = 0;
            length = deck.Length;

            //store the values and facevalues in an array
            int[] values = {11,2,3,4,5,6,7,8,9,10,10,10,10};
            string[] facevalues = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

            // store all the values into the card deck
            for (int i = 0; i < (deck.Length/4); i++)
            {               
                  deck[i] = new Card(values[i], "Card"+(i+1),facevalues[i]);
                  deck[i + 13] = new Card(values[i], "Card" + (i + 14), facevalues[i]);
                  deck[i + 26] = new Card(values[i], "Card" + (i + 27), facevalues[i]);
                  deck[i + 39] = new Card(values[i], "Card" + (i + 40), facevalues[i]);
            }

        }

        //Properties


        //Methods

        public void Shuffle()
        {
            //This shuffles the deck
            //use Random class to generate the shuffling
            Random rand = new Random();

            for (int i = 0; i < deck.Length; i++)
            {
                 int num = rand.Next(0, 51);

                Card Card = deck[i];
                
                deck[i] = deck[num];
                deck[num] = Card;
            }
            
        }

        public Card Deal()
        {
            //Deal the next card in the deck array
                Card card = deck[nextCard];
                nextCard++;


                return card;
        }

        public Card Deal2()
        {
            //Deal the 2nd card in the deck array 
            //this is used for the split
            Card card = deck[nextCard - 2];
            return card;
        }

    }
}
