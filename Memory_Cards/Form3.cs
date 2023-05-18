using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_Cards
{
    public partial class Form3 : Form
    {
        List<int> numbers = new List<int> { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6 };
        string firstChoice;
        string secondChoice;
        int tries;
        List<PictureBox> pictures = new List<PictureBox>();
        PictureBox picA;
        PictureBox picB;
        int totalTime = 30;
        int countDownTime;
        bool gameOver = false;
        public Form3()
        {
            InitializeComponent();
            LoadPictures();
        }
        private void RestartGameEvent(object sender, EventArgs e)
        {
            RandomizeCards();
        }
        private void LoadPictures()
        {
            int leftPos = 20;
            int topPos = 20;
            int rows = 0;
            for (int i = 0; i < 12; i++)
            {
                PictureBox newPic = new PictureBox();
                newPic.Height = 50;
                newPic.Width = 50;
                newPic.BackColor = Color.LightGray;
                newPic.SizeMode = PictureBoxSizeMode.StretchImage;
                newPic.Click += NewPic_Click;
                pictures.Add(newPic);
                if (rows < 3)
                {
                    rows++;
                    newPic.Left = leftPos;
                    newPic.Top = topPos;
                    this.Controls.Add(newPic);
                    leftPos = leftPos + 60;
                }
                if (rows == 3)
                {
                    leftPos = 20;
                    topPos += 60;
                    rows = 0;
                }
            }
            RandomizeCards();
        }
        private void NewPic_Click(object sender, EventArgs e)
        {
            if (gameOver)
            {
                // dont register a click if the game is over
                return;
            }
            if (firstChoice == null)
            {
                picA = sender as PictureBox;
                if (picA.Tag != null && picA.Enabled==true)
                {
                    string a = @"C:\Users\Trupek\Documents\Labolatoria\Memory_Cards\images\1.jpeg";
                    picA.Image = Image.FromFile(@"..\..\..\images\" + (string)picA.Tag + ".jpeg");
                    picA.Enabled = false;
                    firstChoice = (string)picA.Tag;
                }
            }
            else if (secondChoice == null)
            {
                picB = sender as PictureBox;
                if (picB.Tag != null && picB.Enabled == true)
                {
                    picB.Image = Image.FromFile(@"..\..\..\images\" + (string)picB.Tag + ".jpeg");
                    picB.Enabled = false;
                    secondChoice = (string)picB.Tag;
                    Thread.Sleep(2000);
                    CheckPictures(picA, picB);
                }
            }
            else
            {
                CheckPictures(picA, picB);
            }
        }
        private void RandomizeCards()
        {
            // randomise the original list
            var randomList = numbers.OrderBy(x => Guid.NewGuid()).ToList();
            // assign the random list to the original
            numbers = randomList;
            for (int i = 0; i < pictures.Count; i++)
            {
                pictures[i].Image = Properties.Resources.question_mark;
                pictures[i].Tag = numbers[i].ToString();
            }
            gameOver = false;
        }
        private void CheckPictures(PictureBox A, PictureBox B)
        {
            if (firstChoice == secondChoice)
            {
                A.Tag = null;
                B.Tag = null;
            }
            A.Enabled = true;
            B.Enabled = true;
            A.Image = null;
            B.Image = null;
            firstChoice = null;
            secondChoice = null;
            foreach (PictureBox pics in pictures.ToList())
            {
                if (pics.Tag != null)
                {
                    pics.Image = Properties.Resources.question_mark;
                }
            }
            // now lets check if all of the items have been solved
            if (pictures.All(o => o.Tag == pictures[0].Tag))
            {
                GameOver("Great Work, You Win!!!!");
            }
        }
        private void GameOver(string msg)
        {
            gameOver = true;
            MessageBox.Show(msg + " Click Restart to Play Again.", "Moo Says: ");
        }



        private void Form3_Load(object sender, EventArgs e)
        {

        }

    }
}
