using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_Cards
{
    public partial class Form2 : Form
    {
        List<int> numbers = new List<int> { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
        PictureBox picA;
        PictureBox picB;
        List<PictureBox> pictures = new List<PictureBox>();

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        private void StartGame()
        {
            var RandomList = numbers.OrderBy(x => Guid.NewGuid()).ToList();
            numbers = RandomList;
            for (int i = 0; i < 16; i++)
            {
                pictures[i].Tag = numbers[i].ToString();
            }
        }
    }
}
