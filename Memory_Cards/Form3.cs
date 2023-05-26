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
        List<int> numbers = new List<int> { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17, 18, 18, 19, 19, 20, 20, 21, 21, 22, 22, 23, 23, 24, 24, 25, 25, 26, 26, 27, 27, 28, 28, 29, 29, 30, 30};
        string firstChoice;
        string secondChoice;
        bool turn=true;
        int moves = 0;
        int points1 = 0;
        int points2 = 0;
        List<PictureBox> pictures = new List<PictureBox>();
        PictureBox picA;
        PictureBox picB;
        bool gameOver = false;
        public Form3(int size, int players)
        {
            InitializeComponent();
            switch (size)
            {
                case 1:
                    LoadPictures4x4(players);
                    break;
                case 2:
                    LoadPictures6x6(players);
                    break;
                case 3:
                    LoadPictures6x10(players);
                    break;
            }
        }
        private void LoadPictures4x4(int players)
        {
            Size = new Size(640, 640);
            for (int i=9; i<31; i++ )
            {
                numbers.Remove(i);
                numbers.Remove(i);
            }
            int leftPos = 20;
            int topPos = 20;
            int rows = 0;
            for (int i = 0; i < 16; i++)
            {
                PictureBox newPic = new PictureBox();
                newPic.Height = 130;
                newPic.Width = 100;
                newPic.BackColor = Color.LightGray;
                newPic.SizeMode = PictureBoxSizeMode.StretchImage;
                newPic.Click += NewPic_Click;
                pictures.Add(newPic);
                if (rows < 4)
                {
                    rows++;
                    newPic.Left = leftPos;
                    newPic.Top = topPos;
                    this.Controls.Add(newPic);
                    leftPos = leftPos + 110;
                }
                if (rows == 4)
                {
                    leftPos = 20;
                    topPos += 140;
                    rows = 0;
                }
            }
            switch (players)
            {
                case 1:
                    leftPos = 455;
                    SinglePlayer(leftPos);
                    break;
                case 2:
                    {
                        leftPos = 455;
                        DualPlayers(leftPos);
                        break;
                    }

            }
            
            RandomizeCards();
        }
        private void LoadPictures6x6(int players)
        {
            Size = new Size(850, 900);
            int leftPos = 20;
            int topPos = 20;
            int rows = 0;
            for (int i = 19; i < 31; i++)
            {
                numbers.Remove(i);
                numbers.Remove(i);
            }
            for (int i = 0; i < 36; i++)
            {
                PictureBox newPic = new PictureBox();
                newPic.Height = 130;
                newPic.Width = 100;
                newPic.BackColor = Color.LightGray;
                newPic.SizeMode = PictureBoxSizeMode.StretchImage;
                newPic.Click += NewPic_Click;
                pictures.Add(newPic);
                if (rows < 6)
                {
                    rows++;
                    newPic.Left = leftPos;
                    newPic.Top = topPos;
                    this.Controls.Add(newPic);
                    leftPos = leftPos + 110;
                }
                if (rows == 6)
                {
                    leftPos = 20;
                    topPos += 140;
                    rows = 0;
                }
            }
            switch (players)
            {
                case 1:
                    leftPos = 685;
                    SinglePlayer(leftPos);
                    break;
                case 2:
                    {
                        leftPos = 685;
                        DualPlayers(leftPos);
                        break;
                    }
            }
            RandomizeCards();
        }
        private void LoadPictures6x10(int players)
        {
            Size = new Size(1300, 900);
            int leftPos = 20;
            int topPos = 20;
            int rows = 0;
            for (int i = 0; i < 60; i++)
            {
                PictureBox newPic = new PictureBox();
                newPic.Height = 130;
                newPic.Width = 100;
                newPic.BackColor = Color.LightGray;
                newPic.SizeMode = PictureBoxSizeMode.StretchImage;
                newPic.Click += NewPic_Click;
                pictures.Add(newPic);
                if (rows < 10)
                {
                    rows++;
                    newPic.Left = leftPos;
                    newPic.Top = topPos;
                    this.Controls.Add(newPic);
                    leftPos = leftPos + 110;
                }
                if (rows == 10)
                {
                    leftPos = 20;
                    topPos += 140;
                    rows = 0;
                }
            }
            switch (players)
            {
                case 1:
                    leftPos = 1125;
                    SinglePlayer(leftPos);
                    break;
                case 2:
                    {
                        leftPos = 1125;
                        DualPlayers(leftPos);
                        break;
                    }
            }
            RandomizeCards();
        }
        private void SinglePlayer(int leftPos)
        {
            TextBox moves = new TextBox();
            moves.Tag = "1player";
            moves.Name = "Moves1";
            moves.Height = 40;
            moves.Width = 150;
            moves.Text = "Moves: 0";
            moves.Left = leftPos;
            moves.Top = 20;
            moves.BorderStyle = BorderStyle.None;
            moves.Font = new Font("Microsoft Sans Serif", 24);
            moves.ReadOnly = true;
            this.Controls.Add(moves);
        }
        private void DualPlayers(int leftPos)
        {
            int topPos = 20;
            TextBox player_turn = new TextBox();
            player_turn.Name = "Player_turn";
            player_turn.Height = 40;
            player_turn.Width = 140;
            player_turn.Text = "1st player";
            player_turn.Left = leftPos;
            player_turn.Top = topPos;
            player_turn.ReadOnly = true;
            player_turn.BorderStyle = BorderStyle.None;
            player_turn.Font = new Font("Microsoft Sans Serif", 24);
            this.Controls.Add(player_turn);
            for (int i = 1; i < 3; i++)
            {
                topPos += 50;
                TextBox moves1 = new TextBox();
                moves1.Name = "Moves" + i;
                moves1.Tag = "2player";
                moves1.Height = 40;
                moves1.Width = 150;
                moves1.Text = "Player" + i + ": 0";
                moves1.Left = leftPos;
                moves1.Top = topPos;
                moves1.BorderStyle = BorderStyle.None;
                moves1.Font = new Font("Microsoft Sans Serif", 24);
                moves1.ReadOnly = true;
                this.Controls.Add(moves1);
            }
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
        }
        private void RandomizeCards()
        {
            foreach (int i in numbers)
                {
                Console.WriteLine(i);
            }
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
            TextBox move_counter = (TextBox)this.Controls["Moves1"];
            if (firstChoice == secondChoice)
            {
                A.Tag = null;
                B.Tag = null;
                if((string)move_counter.Tag == "2player")
                {
                    if(turn)
                    {
                        TextBox move1 = (TextBox)this.Controls["Moves1"];
                        points1++;
                        move1.Text = "Player1: " + points1;
                    }
                    else
                    {
                        TextBox move1 = (TextBox)this.Controls["Moves2"];
                        points2++;
                        move1.Text = "Player2: " + points2;
                    }
                }
            }
            else if((string)move_counter.Tag=="1player")
            {
                moves++;
                string text = "Moves: " + moves;
                move_counter.Text = text;
            }
            else
            {
                TextBox pturn = (TextBox)this.Controls["Player_turn"];
                if(turn)
                {
                    turn = !turn;
                    pturn.Text = "2nd player";
                }
                else
                {
                    turn = !turn;
                    pturn.Text = "1st player";
                }
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
            MessageBox.Show(msg + " Click Restart to Play Again.");
        }



        private void Form3_Load(object sender, EventArgs e)
        {

        }

    }
}
