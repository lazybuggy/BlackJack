using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//Lucia Okeh
//BlackJack
//January __, 2014
// The objective of this card game is to score “21” and obtain BLACKJACK. If the player scores 
//over “21” they are BUST. The players aim is to get better than the dealer in order to win their bet.
namespace BlackJack
{
    public partial class frmMain : Form
    {
        //Global variables 
        Deck deck;
        Hand player1;
        Hand dealer;
        SplitHand splithand;
        int Bet;

        //True and False to check if buttons can be clicked
        bool StandClick;
        bool GameClicked;
        bool Split;

        //true and false to show facedown card
        bool BlankCard;

        //set inital bet and money amount
        int money = 5000;
       

        public frmMain()
        {
            InitializeComponent();
        }
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            //check if the text box isnt empty
            if (txtBet.Text != string.Empty)
            {
                //load in users bet
                Bet = int.Parse(txtBet.Text);

                //set restrictions on the bet amount
                if (Bet < 2 || Bet > 500 || Bet > money)
                {
                    MessageBox.Show("The bet amount is invalid. \n Please enter an amount from $2 to $500 \n and less than your money.");
                    Bet = 0;
                }
                if (Bet != 0)
                {

                    //create a new deck and hand
                    deck = new Deck();
                    player1 = new Hand();
                    dealer = new Hand();
                    splithand = new SplitHand();

                    //create the user and dealers score
                    int score1 = 0;
                    int score2 = 0;

                    //output the users bet
                    lblBet.Text = ("Bet: $" + Bet);
                    
                    //set all the clicks equal to false
                    StandClick = false;
                    GameClicked = false;
                    Split = false;

                    //allow the blankcard to show
                    BlankCard = true;

                    //Shuffle deck and add 2 cards to the deck
                    deck.Shuffle();
                    deck.Shuffle();
                    deck.Shuffle();

                    //add 2 cards into the players hand and one card into the dealers
                    player1.AddCard(deck.Deal());
                    player1.AddCard(deck.Deal());

                    dealer.AddCard(deck.Deal());

                    //add up all the scores
                    score1 = score1 + player1.getScore();
                    score2 = score2 + dealer.getScore();

                    lblWinLose.Visible = false;

                    //if the player gets blackjack
                    if (score1 == 21)
                    {
                        money = money + Bet;
                        lblDollarValue.Text = ("Current Money: $" + money);
                        GameClicked = true;

                        lblWinLose.Visible = true;
                        lblWinLose.Text = ("AUTO BLACKJACK");
                    }
                    if (score1 > 21)
                    {
                        money = money - Bet;
                        lblDollarValue.Text = ("Current Money: $" + money);
                        GameClicked = true;

                        lblWinLose.Visible = true;
                        lblWinLose.Text = ("AUTO BUST");
                    }

                    //display output 
                    lblScore.Visible = true;
                    lblScore2.Visible = true;
                    lblDollarValue.Visible = true;
                    lblSplitScore.Visible = false;
                   
                    lblDollarValue.Text = ("Current Money: $" + money);
                    lblScore.Text = ("Player: " + "\n" + score1);
                    lblScore2.Text = ("Dealer: " + "\n" + score2);
                   

                    //what happens if the player has no more money left
                    if (money <= 0)
                    {
                        MessageBox.Show("You have no more money. " + "\n" + "The game will now close.");
                        this.Close();
                    }

                    //cause form to repaint itself
                    this.Invalidate();
                }
            }

            //if the user has not entered any bet amount
            else
            {
                MessageBox.Show("Please enter in a bet");
            }
            
        }


        private void btnHit_Click(object sender, EventArgs e)
        {

            //if the game is not over, enable the code to run
            if (GameClicked == false)
            {
                //set the scores
                int score1 = 0;
                //set a score3 in case the hand is split
                int score3 = 0;

                //add a card to the players hand and calculate the new score
                player1.AddCard(deck.Deal());
                score1 = score1 + player1.getScore();

                lblScore.Text = ("Player: " + "\n" + score1);

               //if the hand has been split 
               if (Split == true)
                {
                   //add a card into the new hand aswell
                    splithand.AddCard2(deck.Deal());

                   //calculate the 2nd hands score and output
                    score3 = score3 + splithand.getScore2();
                    lblSplitScore.Visible = true;
                    lblSplitScore.Text = ("Player \n Hand2: \n" + score3);

                    if (score3 == 21)
                    {
                        money = money + Bet;
                        lblDollarValue.Text = ("Current Money: $" + money);

                        lblWinLose.Visible = true;
                        lblWinLose.Text = ("WIN");
                    }
                    if (score3 > 21)
                    {
                        money = money - Bet;
                        lblDollarValue.Text = ("Current Money: $" + money);

                        lblWinLose.Visible = true;
                        lblWinLose.Text = ("LOSE");
                    }
                } 

                //compare the scores and distribute the money evenly    
                if (score1 > 21 || score3 > 21 )
                {
                    money = money - Bet;
                    lblDollarValue.Text = ("Current Money: $" + money);
                    GameClicked = true;

                    lblWinLose.Visible = true;
                    lblWinLose.Text = ("BUST");
                }
                if (score1 == 21 || score3 ==21)
                {
                    money = money + Bet;
                    lblDollarValue.Text = ("Current Money: $" + money);
                    GameClicked = true;

                    lblWinLose.Visible = true;
                    lblWinLose.Text = ("BLACKJACK");
                }

                lblBet.Text = ("Bet: $" + Bet);

                //what happens if the player has no more money left
                if (money <= 0)
                {
                    MessageBox.Show("You have no more money. " + "\n" + "The game will now close.");
                    this.Close();
                }

                // form refreshs itself
                this.Invalidate();

            }
        }

        private void btnStand_Click(object sender, EventArgs e)
        {
            //if stand hasnt been clicked before and the game is not over
            if (StandClick == false && GameClicked == false)
            {
                //add a card to the dealers deck and get rid of the blank card
                dealer.AddCard(deck.Deal());
                BlankCard = false;

                //set the scores
                int score1 = 0;
                int score2 = 0;
                int score3 = 0;

                //calculate the scores
                score1 = score1 + player1.getScore();

                //if the dealer has a score less than 17 allow him to get more cards
                if (score2 < 17)
                {
                    dealer.AddCard(deck.Deal());
                    score2 = score2 + dealer.getScore();
                }

                //if a split happened, calculate the score of the players 2nd hand
                if (Split == true)
                {
                    score3 = score3 + splithand.getScore2();

                    if (score3 > score2)
                    {
                        money = money + Bet;
                        lblDollarValue.Text = ("Current Money: $" + money);

                        lblWinLose.Visible = true;
                        lblWinLose.Text = ("WIN");
                    }
                    if (score3 < score2 && score2 < 21)
                    {
                        money = money - Bet;
                        lblDollarValue.Text = ("Current Money: $" + money);

                        lblWinLose.Visible = true;
                        lblWinLose.Text = ("LOSE");
                    }

                }


                //output scores
                lblScore.Text = ("Player: " + "\n" + score1);
                lblScore2.Text = ("Dealer: " + "\n" + score2);
                lblSplitScore.Text = ("Player \n Hand2: \n" + score3);


                //check the card value and add/subtract money as told
                if (score1 > score2)
                {
                    money = money + Bet;
                    lblDollarValue.Text = ("Current Money: $" + money);

                    lblWinLose.Visible = true;
                    lblWinLose.Text = ("WIN");
                }
                if (score1 < score2 && score2 < 21)
                {
                    money = money - Bet;
                    lblDollarValue.Text = ("Current Money: $" + money);

                    lblWinLose.Visible = true;
                    lblWinLose.Text = ("LOSE");
                }
                if (score1 < 21 && score2 > 21)
                {
                    money = money + Bet;
                    lblDollarValue.Text = ("Current Money: $" + money);

                    lblWinLose.Visible = true;
                    lblWinLose.Text = ("WIN");
                }
                if (score2 == 21)
                {
                    money = money - Bet;
                    lblDollarValue.Text = ("Current Money: $" + money);

                    lblWinLose.Visible = true;
                    lblWinLose.Text = ("LOSE \n DEALER GOT \n BLACKJACK");
                }

                //what happens if the player has no more money left
                if (money <= 0)
                {
                    MessageBox.Show("You have no more money. " + "\n" + "The game will now close.");
                    this.Close();
                }

                //set that stand has been clicked
                StandClick = true;
                GameClicked = true;

                //cause form to repaint itself
                this.Invalidate();
            }
        
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            
            if (player1 != null )
            {
                //Draw Hand
                player1.DrawHand(e.Graphics);
            }

            if (BlankCard == true)
            {
                Graphics g = this.CreateGraphics();
                Bitmap bm = Resource1.blankcard;
                g.DrawImage(bm, 143, 199);
            }
          
            if (dealer != null)
            {
                //Draw Hand
                dealer.DrawHand2(e.Graphics);

            }
            if (splithand != null)
            {
                //Draw Hand
                splithand.DrawHand3(e.Graphics);
            }
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            //End application   
            Application.Exit();    
        }

        private void btnDouble_Click(object sender, EventArgs e)
        {
            //If the game is not over
            if (GameClicked == false)
            {
                //Double the bet amount
                Bet = Bet * 2;

                //set the scores
                int score1 = 0;

                //add a card into the players hand and calculate the score
                player1.AddCard(deck.Deal());
                score1 = score1 + player1.getScore();

                //output the score and the new bet
                lblScore.Text = ("Player: " + "\n" + score1);
                lblBet.Text = ("Bet: $" + Bet);

                //Conditions for the score and add/subtract money as told
                if (score1 > 21)
                {
                    money = money - Bet;
                    lblDollarValue.Text = ("Current Money: $" + money);
                    GameClicked = true;

                    lblWinLose.Visible = true;
                    lblWinLose.Text = ("BUST");
                }
                if (score1 == 21)
                {
                    money = money + Bet;
                    lblDollarValue.Text = ("Current Money: $" + money);
                    GameClicked = true;

                    lblWinLose.Visible = true;
                    lblWinLose.Text = ("BLACKJACK");
                }

                //cause form to repaint itself
                this.Invalidate();
            }
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            //if the first 2 cards of the players hand are the same
            if (player1.Hand1[0].FaceValue == player1.Hand1[1].FaceValue && player1.Cards == 2)
            {
                //set the hands score
                int score1 = 0;
                int score3 = 0;

                //double the players bet and output
      //          ////Bet = Bet * 2;
        //        ////lblBet.Text = ("Bet: $" + Bet);

                //add a card into the 2nd hand and delete that card from the 1st hand
                splithand.AddCard2(deck.Deal2());
                player1.DeleteCard();

                //set and output the 2nd hands score
                score1 = score1 + player1.getScore();
                score3 = score3 + splithand.getScore2();
                lblSplitScore.Visible = true;
                lblSplitScore.Text = ("Player \n Hand2: \n" + score3);
                lblScore.Text = ("Player: " + "\n" + score1);

                //Set that a split has occured
                Split = true;

                //cause form to repaint itself
                this.Invalidate();
            }
        }

        private void mnuFileHelp_Click(object sender, EventArgs e)
        {
            //Help the user
            MessageBox.Show("To play this game you must score 21 or higher than the dealer"+"\n" +
            
                " \n \n If your score is over 21, or if the dealer scores higher than you, your bet amount will be deducted" + "\n" +
                " If your score is lower than 21 BUT the dealers score is HIGHER than 21, you will win your bet amount"+"\n \n"+
                "There are 4 buttons that you can choose from, they include"+"\n"+ "\n"+
                " Hit - deals you another card \n Stand - The dealer plays \n Double Down - double your bet and get another card" +"\n"+
                "Split - ONLY IF your first 2 cards are the same then you can crete a new hand and play with both" + "\n \n"+
                "**NOTE you can bet from $2 to $500 and ONLY less than the money you posses", "Blackjack Game Help", MessageBoxButtons.OK);
        }


    }
}
