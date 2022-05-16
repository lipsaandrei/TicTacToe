using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * @autor AndreiLipsa
 */

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        bool turn = true; //true = randul lui X, false = randul lui Y
        bool against_computer = false;
        int turn_count = 0;
        static String player1, player2, dificulty, X = "X", O = "O";
        bool turn_computer = false;

        public Form1()
        {
            InitializeComponent();
        }

        public static void setPlayerNames(String n1, String n2)
        {
            player1 = n1;
            player2 = n2;
        }

        public static void setXandO(String n1, String n2)
        {
            X = n1;
            O = n2;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Acesta este un joc creat de Andrei Lipsa!  Daca doriti mai multe detalii, informatine la urmatoarea adresa de e-mail: lipsaandrei2003@gmail.com", "Despre TicTacToe");
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); //exit buton
        }

        private void button_click(object sender, EventArgs e)
        {
            if (against_computer == false)
            {
                Button b = (Button)sender;
                if (turn)           //schimbarea pe rand in X si O
                    b.Text = X;
                else b.Text = O;
                turn = !turn;
                b.Enabled = false; //dezactivam schimbarea butonului
                turn_count++;

                checkForWinner();
            }
            else if (turn_computer == false)
            {   
                Button b = (Button)sender;
                if (turn)
                    b.Text = X;
                else b.Text = O;
                turn = !turn;
                b.Enabled = false;
                turn_count++;
                //if(p1.Text.ToLower() == "computer")
                   // p1.Focus();
                checkForWinner();
                
                if (!turn)
                    computer_make_move();
            }
           /* else
            {
                if (turn)
                    computer_make_move();
                Button b = (Button)sender;
                if (!turn)
                    b.Text = X;
                else b.Text = O;
                turn = !turn;
                b.Enabled=false;
                turn_count++;
                checkForWinner();
            }*/
        }

        private void computer_make_move()
        {
            //prioritatea 1: sa castige
            //prioritatea 2: sa blocheze
            //prioritatea 3: sa caute cel mai favorabil loc liber
            if(dificulty == "mediu")
            {
                Button move = null;
                move = look_for_win_or_block(O);
                if (move == null)
                {
                    move = look_for_win_or_block(X);
                    if (move == null)
                    {
                        //move = look_for_corner();
                        //if(move == null)
                        //{
                        move = look_for_open_space();
                        //}
                    }
                }

                if (move != null)
                    move.PerformClick();
            }
            else if (dificulty == "usor")
            {
                Button move = null;
                move = look_for_open_space();
                if(move != null)
                {
                    move = look_for_open_space();
                }
                if (move != null)
                    move.PerformClick();
            }
            else
            {
                Button move = null;
                move = look_for_win_or_block(O); //incearca sa castige
                if(move == null)
                {
                    move = look_for_win_or_block(X); //incearca sa blocheze
                    if( move == null)
                    {
                        move = look_for_exceptional_cases(O); //face o mutare sah-mat
                        if( move == null)
                        {
                            move = look_for_exceptional_cases(X); //evita o mutare sah-mat
                            if(move == null)
                            {
                                move = look_for_open_space();
                            }
                        }
                    }
                }
                if (move != null)
                    move.PerformClick();
            }
        }

        private Button look_for_open_space()
        {
            /*Console.WriteLine("Looking for open space");
            Button b = null;
            foreach (Control c in Controls)
            {
                b = c as Button;
                if (b != null)
                {
                    if (b.Text == "")
                        return b;
                }//end if
            }//end if*/
            //Button b = null;
            int pozAleasa;
            bool ok1 = false, ok2 = false, ok3 = false, ok4 = false, ok5 = false, ok6 = false, ok7 = false, ok8 = false , ok9 = false;
            Random rnd = new Random();
            do
            {
                pozAleasa = (int)(rnd.NextDouble() * 10) + 1;
                if (pozAleasa == 1)
                {
                    if (A1.Enabled)
                    {

                        return A1;
                    }
                    else ok1 = true;
                }
                else if (pozAleasa == 2)
                {
                    if(A2.Enabled)
                    {
                        
                        return A2;
                    }
                    else ok2 = true;
                }
                else if (pozAleasa == 3)
                {
                    if (A3.Enabled)
                    {
                        
                        return A3;
                    }
                    else ok3 = true;
                }
                else if (pozAleasa == 4)
                {
                    if (B1.Enabled)
                    {
                        
                        return B1;
                    }
                    else ok4 = true;
                }
                else if (pozAleasa == 5)
                {
                    if (B2.Enabled)
                    {
                        
                        return B2;
                    }
                    else ok5 = true;
                }
                else if (pozAleasa == 6)
                {
                    if (B3.Enabled)
                    {

                        return B3;
                    }
                    else ok6 = true;
                }
                else if (pozAleasa == 7)
                {
                    if (C1.Enabled)
                    {

                        return C1;
                    }
                    else ok7 = true;
                }
                else if (pozAleasa == 8)
                {
                    if (C2.Enabled)
                    {
                        
                        return C2;
                    }
                    else ok8 = true;
                }
                else if (pozAleasa == 9)
                {
                    if (C3.Enabled)
                    {

                        return C3;
                    }
                    else ok9 = true;
                }
            } while(ok1 == false || ok2 == false || ok3 == false || ok4 == false || ok5 == false || ok6 == false || ok7 == false || ok8 == false || ok9 == false);

            return null;
        }

        private Button look_for_win_or_block(string mark)
        {
            Console.WriteLine("Looking for win or block:  " + mark);
            //testele orizontale
            if ((A1.Text == mark) && (A2.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A2.Text == mark) && (A3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (A3.Text == mark) && (A2.Text == ""))
                return A2;

            if ((B1.Text == mark) && (B2.Text == mark) && (B3.Text == ""))
                return B3;
            if ((B2.Text == mark) && (B3.Text == mark) && (B1.Text == ""))
                return B1;
            if ((B1.Text == mark) && (B3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((C1.Text == mark) && (C2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((C2.Text == mark) && (C3.Text == mark) && (C1.Text == ""))
                return C1;
            if ((C1.Text == mark) && (C3.Text == mark) && (C2.Text == ""))
                return C2;

            //testele verticale
            if ((A1.Text == mark) && (B1.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B1.Text == mark) && (C1.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C1.Text == mark) && (B1.Text == ""))
                return B1;

            if ((A2.Text == mark) && (B2.Text == mark) && (C2.Text == ""))
                return C2;
            if ((B2.Text == mark) && (C2.Text == mark) && (A2.Text == ""))
                return A2;
            if ((A2.Text == mark) && (C2.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B3.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B3.Text == mark) && (C3.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C3.Text == mark) && (B3.Text == ""))
                return B3;

            //testele pe diagonala
            if ((A1.Text == mark) && (B2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B2.Text == mark) && (C3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B2.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B2.Text == mark) && (C1.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C1.Text == mark) && (B2.Text == ""))
                return B2;

            return null;
        }

        private Button look_for_exceptional_cases(string mark)
        {
            Console.WriteLine("Look for exceptional cases:  " + mark);
            if (A1.Text == mark && A3.Text == mark && B2.Text == "" && ((A2.Text == "" && (C1.Text == "" || C3.Text == "")) || (C1.Text == "" && C3.Text == "")))
                return A2;

            return null;
        }

        private void checkForWinner()
        {
            bool there_is_a_winner = false;

            //verificarile orizontale
            if ((A1.Text == A2.Text) && (A2.Text == A3.Text) && (!A1.Enabled)) //e suficient sa verific o singura casuta daca exista un X sau O
                there_is_a_winner = true;
            else if ((B1.Text == B2.Text) && (B2.Text == B3.Text) && (!B1.Enabled))
                there_is_a_winner = true;
            else if ((C1.Text == C2.Text) && (C2.Text == C3.Text) && (!C1.Enabled))
                there_is_a_winner = true;

            //verificarile verticale
            else if ((A1.Text == B1.Text) && (B1.Text == C1.Text) && (!A1.Enabled)) 
                there_is_a_winner = true;
            else if ((A2.Text == B2.Text) && (B2.Text == C2.Text) && (!B2.Enabled))
                there_is_a_winner = true;
            else if ((A3.Text == B3.Text) && (B3.Text == C3.Text) && (!C3.Enabled))
                there_is_a_winner = true;

            //verificarile pe diagonale
            else if ((A1.Text == B2.Text) && (B2.Text == C3.Text) && (!A1.Enabled)) 
                there_is_a_winner = true;
            else if ((C1.Text == B2.Text) && (B2.Text == A3.Text) && (!B2.Enabled))
                there_is_a_winner = true;

            if (there_is_a_winner)
            {
                disableButtons();

                String winner = "";
                if (against_computer)
                {
                    if (!turn)
                    {
                        winner = player1;
                        x_win_count.Text = (Int32.Parse(x_win_count.Text) + 1).ToString();
                    }
                    else
                    {
                        winner = player2;
                        o_win_count.Text = (Int32.Parse(o_win_count.Text) + 1).ToString();
                    }
                }
                else if(turn)
                {
                    winner = player2;
                    o_win_count.Text = (Int32.Parse(o_win_count.Text) + 1).ToString();
                }
                else
                {
                    winner = player1;
                    x_win_count.Text = (Int32.Parse(x_win_count.Text) + 1).ToString();
                }

                MessageBox.Show(winner + " a castigat!", "Frumos joc!");
            } //afisare fereastra castigator
            else
            {
                if (turn_count == 9)
                {
                    MessageBox.Show("Egalitate!", "Sfarsit joc");
                    draw_count.Text = (Int32.Parse(draw_count.Text) + 1).ToString();
                }
            }

        }//sfarsit verificare castigator

        private void disableButtons() //dezactiveaza restul casutelor atunci cand se gaseste un castigator
        {
            /*try
            {
                foreach (Control c in Controls)
                {
                    Button b = (Button)c;
                    b.Enabled = false;
                }
            }
            catch { }*/
            if(A1.Text == "")
                A1.Enabled = false;
            if (A2.Text == "")
                A2.Enabled = false;
            if (A3.Text == "")
                A3.Enabled = false;
            if (B1.Text == "")
                B1.Enabled = false;
            if (B2.Text == "")
                B2.Enabled = false;
            if (B3.Text == "")
                B3.Enabled = false;
            if (C1.Text == "")
                C1.Enabled = false;
            if (C2.Text == "")
                C2.Enabled = false;
            if (C3.Text == "")
                C3.Enabled = false;

        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turn = true;
            turn_count = 0;

                foreach (Control c in Controls)
                {
                    try
                        {
                            Button b = (Button)c;
                            b.Enabled = true;
                            b.Text = "";
                        }
                    catch { }
                }
      
        }

        private void button_enter(object sender, EventArgs e) //interactiunea cursor - casuta
        {
            Button b = (Button)sender;
            if (b.Enabled)
            {
                if (turn)
                    b.Text = X;
                else b.Text = O;
            }
        }

        private void button_leave(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
            {
                b.Text = "";
            }
        }

        private void resetScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            o_win_count.Text = "0";
            x_win_count.Text = "0";
            draw_count.Text = "0";
        }

        private void draw_count_Click(object sender, EventArgs e)
        {

        }

        private void p2_TextChanged(object sender, EventArgs e)
        {
            if (p1.Text.ToLower() != "computer")
            {
                if (p2.Text.ToLower() == "computer")
                    against_computer = true;
                else against_computer = false;
            }
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void modificaJucatoriiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
            if (p1.Text != "" && p2.Text != "")
            {
                
                p1.Text = player1;
                p2.Text = player2;
            }
            if (player2.ToLower() == "computer")
                against_computer = true;
            o_win_count.Text = "0";
            x_win_count.Text = "0";
            draw_count.Text = "0";
            turn = true;
            turn_count = 0;

            foreach (Control c in Controls)
            {
                try
                {
                    Button b = (Button)c;
                    b.Enabled = true;
                    b.Text = "";
                }
                catch { }
            }
        }

        private void p1_TextChanged(object sender, EventArgs e)
        {
            if (p1.Text.ToLower() == "computer")
            { 
                against_computer = true;
                turn_computer = true;
                dificulty = "mediu";

            }
            else against_computer = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Form f2 = new Form2();
            //f2.ShowDialog();
            //f2.Hide();
            if (p1.Text != "" && p2.Text != "")
            {
                p1.Text = player1;
                p2.Text = player2;
            }
        }

        public static void set_dificulty(String dif) //setam dificultatea computerului
        {
            dificulty = dif;
        }
    }
}
