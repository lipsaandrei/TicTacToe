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
    public partial class Form2 : Form
    {
        bool p1_complete = false;
        bool p2_complete = false;
        bool computer_on = false;
        bool switch_x_o = false;
        
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (computer_on && comboBox1.Text.ToLower() == "selectare")
                MessageBox.Show("Va rog, selectati dificultatea computer-ului!");
            else if (p1_complete && p2_complete)
            {   
                Form f1 = new Form1();
                Form1.set_dificulty(comboBox1.Text.ToLower());
                Form1.setXandO(textBox2.Text, textBox1.Text);
                Form1.setPlayerNames(p1.Text, p2.Text);
                Form f2 = new Form2();
                f2.Hide();
                f1.ShowDialog();
                //this.Hide();
                

            }
            else MessageBox.Show("Numele jucatorilor nu pot lipsi!");
        }

        private void p2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
                button1.PerformClick();
        }

        private void p1_TextChanged(object sender, EventArgs e)
        {
            if(p1.Text != "")
                p1_complete = true;
            else p1_complete = false;
        }

        private void p2_TextChanged(object sender, EventArgs e)
        {
            if (p2.Text != "")
                p2_complete = true;
            else p2_complete = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void p1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
                if(!computer_on)
                    p2.Select();
                else button1.PerformClick();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            computer_on = !computer_on;
            if(computer_on)
            {
                p2.Text = "Computer";
                p2.Enabled = false;
                label4.Visible = true;
                comboBox1.Visible = true;
            }
            else
            {
                p2.Text = "";
                p2.Enabled = true;
                label4.Visible = false;
                comboBox1.Visible = false; 
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //if (comboBox1.Text.ToLower() == "selectare")
              //  MessageBox.Show("Va rog, selectati dificultatea computer-ului!");
            //else 
              //  Form1.set_dificulty(comboBox1.Text.ToLower());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch_x_o = !switch_x_o;
            if(switch_x_o)
            {
                textBox2.Text = "O";
                textBox1.Text = "X";
            }
            else
            {
                textBox2.Text = "X";
                textBox1.Text = "O";
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
