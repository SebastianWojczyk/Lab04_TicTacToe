using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab04_TicTacToe
{
    public partial class Form1 : Form
    {
        private Button[,] game;
        private int boardSize;
        private const int buttonSize = 40;
        private int turnCounter;
        public Form1()
        {
            InitializeComponent();
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            boardSize = 3;
            turnCounter = 0;

            game = new Button[boardSize, boardSize];

            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    game[x, y] = new Button();
                    game[x, y].Size = new Size(buttonSize, buttonSize);
                    game[x, y].Location = new Point(x * buttonSize + this.DefaultMargin.Left,
                                                    y * buttonSize + this.DefaultMargin.Top);

                    game[x, y].Font = new Font(game[x, y].Font.FontFamily, buttonSize / 2);
                    game[x, y].Click += button_Click;

                    this.Controls.Add(game[x, y]);
                }
            }


        }

        private void button_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button b = (sender as Button);

                if (b.Text == "")
                {
                    //b.BackColor = Color.Red;
                    if (turnCounter % 2 == 0)
                    {
                        b.Text = "X";
                    }
                    else
                    {
                        b.Text = "O";
                    }
                    turnCounter++;

                    String message = "";
                    if (isWinner(b.Text))
                    {
                        message = $"The winner is {b.Text}";
                    }
                    else if (turnCounter == boardSize * boardSize)
                    {
                        message = $"The winner is :-(";
                    }

                    if (message != "")
                    {
                        if (MessageBox.Show($"{message}\nAgain?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            turnCounter = 0;
                            foreach (Control c in this.Controls)
                            {
                                if (c is Button)
                                {
                                    c.Text = "";
                                }
                            }
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                }
            }
        }

        private bool isWinner(string player)
        {
            //cols
            for (int x = 0; x < boardSize; x++)
            {
                int count = 0;
                for (int y = 0; y < boardSize; y++)
                {
                    if (game[x, y].Text == player)
                    {
                        count++;
                    }
                }
                if (count == boardSize)
                {
                    return true;
                }
            }
            //rows
            for (int y = 0; y < boardSize; y++)
            {
                int count = 0;
                for (int x = 0; x < boardSize; x++)
                {
                    if (game[x, y].Text == player)
                    {
                        count++;
                    }
                }
                if (count == boardSize)
                {
                    return true;
                }
            }
            {
                //diag
                int count = 0;
                for (int i = 0; i < boardSize; i++)
                {
                    if (game[i, i].Text == player)
                    {
                        count++;
                    }
                }
                if (count == boardSize)
                {
                    return true;
                }
            }
            {
                //diag
                int count = 0;
                for (int i = 0; i < boardSize; i++)
                {
                    if (game[i, boardSize - 1 - i].Text == player)
                    {
                        count++;
                    }
                }
                if (count == boardSize)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
