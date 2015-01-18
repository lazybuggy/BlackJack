using System;
using System.Drawing;
using System.Drawing.Drawing2D;
//Header Comments

namespace BlackJack
{
    class Hand
    {
        //Fields
        
        private Card[] hand;

        private int cards;

       
        //Constructor

        public Hand()
        {
            hand = new Card[52];
            this.cards = 0;
        }



        //Properties
 //       //allows the hand to be accesable 
        public Card[] Hand1
        {
            get { return hand; }
        }

        //makes the array length accessable 
        public int Cards
        {
            get { return cards; }
        }

        //Methods

        public void AddCard(Card card)
        {
            //assign the card to hand

            hand[cards] = card;
            cards++;

        }

        public void DeleteCard()
        {        
    //        //delete this card from the hand and irretate backwards
                hand[1] = null;
                cards--;           
        }

           

        //Draw players hand
        public void DrawHand(Graphics g)
        {
            //loop through each card in the hand 
            //and calculate the x and y position
            //and call DrawCard

            //card width is 73

            int x = 70;
            int y = 100;
  
            for (int i = 0; i < cards; i++)
            {
           hand[i].DrawCard(g, x, y);
            x = x + 73;
            }           
        }

        //Draw dealers hand
        public void DrawHand2(Graphics g)
        {
            //loop through each card in the hand 
            //and calculate the x and y position
            //and call DrawCard

            //card width is 73

            int x = 70;
            int y = 200;

            for (int i = 0; i < cards; i++)
            {
                hand[i].DrawCard(g, x, y);
                x = x + 73;
            }
        }


        public int getScore()
        {
            //loop through each card and 
            //add up the value of the card
            int value = 0;
            for (int i = 0; i < cards; i++)
            {
                value = value + hand[i].CardValue;          
            }
            return value;
        }

    }
}
