using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BlackJack
{
    class SplitHand
    {

        //Fields

        private Card[] splithand;
        private int cards;

        //Constructor

        public SplitHand()
        {
            splithand = new Card[52];
            this.cards = 0;
        }

        //Properties


        //Methods

        public void AddCard2(Card card)
        {
            splithand[cards] = card;

            cards++;
        }

        //Draw players hand
        public void DrawHand3(Graphics g)
        {
            //loop through each card in the hand 
            //and calculate the x and y position
            //and call DrawCard

            //card width is 73

            int x = 450;
            int y = 100;
  
            for (int i = 0; i < cards; i++)
            {
           splithand[i].DrawCard(g, x, y);
            x = x + 73;
            }           
        }


        public int getScore2()
        {
            //loop through each card and 
            //add up the value of the card
            int value = 0;
            for (int i = 0; i < cards; i++)
            {
                value = value + splithand[i].CardValue;          
            }
            return value;
        }


    }
}
