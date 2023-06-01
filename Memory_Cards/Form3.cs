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
        bool move_ended=true;
        int moves = 0;
        int points1 = 0;
        int points2 = 0;
        List<PictureBox> pictures = new List<PictureBox>();
        PictureBox picA;
        PictureBox picB;
        public Form3(int size, int players)
        {
            InitializeComponent();
            int FormWidth;
            int FormHeight;
            int rows;
            int i;
            int j;
            int move_counter_pos;
            switch (size)
            {
                case 1:
                    FormWidth = 640;
                    FormHeight = 640;
                    rows = 4;
                    i=9;
                    j=16;
                    move_counter_pos = 455;
                    LoadPictures(players, FormWidth, FormHeight, rows, i, j, move_counter_pos);
                    break;
                case 2:
                    FormWidth = 850;
                    FormHeight = 900;
                    rows = 6;
                    i=19;
                    j=36;
                    move_counter_pos = 685;
                    LoadPictures(players, FormWidth, FormHeight, rows, i, j, move_counter_pos);
                    break;
                case 3:
                    FormWidth = 1300;
                    FormHeight = 900;
                    rows = 10;
                    i=36;
                    j=60;
                    move_counter_pos = 1125;
                    LoadPictures(players, FormWidth, FormHeight, rows, i, j, move_counter_pos);
                    break;
            }
        }
        private void LoadPictures(int players, int FormWidth, int FormHeight, int rows, int j, int cards, int move_counter_pos)
        {
            Size = new Size(FormWidth, FormHeight);
            for (int i=j; i<31; i++ )
            {
                numbers.Remove(i);
                numbers.Remove(i);
            }
            int leftPos = 20;
            int topPos = 20;
            int current_rows = 0;
            for (int i = 0; i < cards; i++)
            {
                PictureBox newPic = new PictureBox();
                newPic.Height = 130;
                newPic.Width = 100;
                newPic.BackColor = Color.LightGray;
                newPic.SizeMode = PictureBoxSizeMode.StretchImage;
                newPic.Click += NewPic_Click;
                pictures.Add(newPic);
                if (current_rows < rows)
                {
                    current_rows++;
                    newPic.Left = leftPos;
                    newPic.Top = topPos;
                    this.Controls.Add(newPic);
                    leftPos = leftPos + 110;
                }
                if (current_rows == rows)
                {
                    leftPos = 20;
                    topPos += 140;
                    current_rows = 0;
                }
            }
            switch (players)
            {
                case 1:
                    SinglePlayer(move_counter_pos);
                    break;
                case 2:
                    {
                        DualPlayers(move_counter_pos);
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
        async private void NewPic_Click(object sender, EventArgs e)
        {
            if (move_ended == false)
            {
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
                    move_ended = false;
                    await Task.Delay(2000);
                    CheckPictures(picA, picB);
                    move_ended = true;
                }
            }
        }
        private void RandomizeCards()
        {
            var randomList = numbers.OrderBy(x => Guid.NewGuid()).ToList();
            numbers = randomList;
            for (int i = 0; i < pictures.Count; i++)
            {
                pictures[i].Image = Properties.Resources.question_mark;
                pictures[i].Tag = numbers[i].ToString();
            }
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
            if (pictures.All(o => o.Tag == pictures[0].Tag))
            {
                GameOver();
            }
        }
        private void GameOver()
        {
            TextBox players = (TextBox)this.Controls["Moves1"];
            if ((string)players.Tag == "1player")
            {
                DialogResult result = MessageBox.Show("Congrats You Won In " + moves + " Moves!!!", "GameOver", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    Form1 form1 = new Form1();
                    form1.Show();
                    form1.Closed += (s, args) => this.Close();
                    this.Hide();
                }
            }
            else
            {
                if (points1 > points2)
                {
                    DialogResult result = MessageBox.Show("Player1 Won With " + points1 + " Points!!!", "GameOver", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        Form1 form1 = new Form1();
                        form1.Show();
                        form1.Closed += (s, args) => this.Close();
                        this.Hide();
                    }
                }
                else if (points2 > points1)
                {
                    DialogResult result = MessageBox.Show("Player2 Won With " + points2 + " Points!!!", "GameOver", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        Form1 form1 = new Form1();
                        form1.Show();
                        form1.Closed += (s, args) => this.Close();
                        this.Hide();
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("It's a Draw!!!", "GameOver", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        Form1 form1 = new Form1();
                        form1.Show();
                        form1.Closed += (s, args) => this.Close();
                        this.Hide();
                    }
                }
            }
        }



        private void Form3_Load(object sender, EventArgs e)
        {

        }

    }
}
