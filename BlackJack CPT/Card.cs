using System.Drawing;
//Header comments

namespace BlackJack
{
    class Card
    {
        //Fields

        //value of the card 2-11
        int mvalue;  
        //file name for the card to draw it
        string mfileName;  
        //facevalue of the card from A-K
        string mfaceValue; 

        //Constructors
      
        public Card(int value, string fileName, string faceValue)
        {
            this.mvalue = value;
            this.mfileName = fileName;
            this.mfaceValue = faceValue;
        }

        //Properties (accessor and mutators)

        //create a property to get access to the card value
        public int CardValue
        {
            get { return mvalue; }
            set { mvalue = value; }
        }
        public string FileName
        {
            get { return mfileName; }
            set { mfileName = value; }
        }
        public string FaceValue
        {
            get { return mfaceValue; }
            set { mfaceValue = value; }
        }

        //Methods

        public void DrawCard(Graphics g, int x, int y)
        {
            //Draw the card 
           Bitmap card = (Bitmap)Resource1.ResourceManager.GetObject(FileName);
           g.DrawImage(card, x, y);

        }


    }
}
