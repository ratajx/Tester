using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace tester
{
    public partial class Form1 : Form
    {
        int[] tab;
        int lenght;
    
        double[] pojedyncze()
        {
            int n0=0,n1=0;
            double[] t=new double[3];

            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i] == 0)
                    n0++;
                else
                    n1++;
            }

            double res;
            res = (double)((n0 - n1) * (n0 - n1)) /(double) tab.Length;


            t[0] = n0;
            t[1] = n1;
            t[2] = res;
            return t;
        }

        void pary()
        {
            double[] t = pojedyncze();
            double n0 = t[0], n1 = t[1];

            int[,] tabk = new int[,] 
            {{ 00, 0 }, { 01, 0 }, { 10, 0 }, { 11, 0 },};

            int number = 0;

            for (int i = 0; tab.Length - i > 1; i++)
            {
                number = tab[i] * 10 + tab[i + 1];
                for (int j = 0; j < 4; j++)
                    if (number == tabk[j, 0])
                    {
                        tabk[j, 1]++;
                        break;
                    }
            }
            double suma=0;

            for(int i=0;i<4;i++)
                suma+=tabk[i,1]*tabk[i,1];

            double a = (double)4 / (double)(tab.Length - 1);
            double b = (double)2 / (double)tab.Length;
           
            double res;
            res = (a* suma) - (b * (n0 * n0 + n1 * n1)) + 1;
           
            textBox2.Text += checkBox2.Text + " S2 równe " + Math.Round(res, 4) + Environment.NewLine;

        }

        void dlugie()
        {
            int max = 0;

            int[] v = new int[2];
            v[0] = 0;
            v[1] = 0;

            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i] == 0 && i == 0)
                    v[0]++;
                if (tab[i] == 1 && i == 0)
                    v[1]++;

                if (tab[i] == 0 && i > 0)
                    if (tab[i - 1] == 0)
                        v[0]++;
                    else
                    {
                        if (v[1] > max)
                            max = v[1];
                        v[0] = 1;
                        v[1] = 0;
                    }

                if (tab[i] == 1 && i > 0)
                    if (tab[i - 1] == 1)
                        v[1]++;
                    else
                    {
                        if (v[0] > max)
                            max = v[0];
                        v[0] = 0;
                        v[1] = 1;
                    }
             }
            if (max > 25)
                textBox3.Text += checkBox3.Text + " niezaliczony najdłuższa seria równa " + max + Environment.NewLine; 
            else
                textBox3.Text += checkBox3.Text + " zaliczony najdłuższa seria równa " + max + Environment.NewLine;
        }

        void poker()
        {
            int[,] tabk = new int[,] { 
            { 0000, 0 }, { 0001, 0 }, { 0010, 0 }, { 0011, 0 },
            { 0100, 0 }, { 0101, 0 }, { 0110, 0 }, { 0111, 0 }, 
            { 1000, 0 }, { 1001, 0 }, { 1010, 0 }, { 1011, 0 }, 
            { 1100, 0 }, { 1101, 0 }, { 1110, 0 }, { 1111, 0 },};
            int number=0;
            
            for(int i=0;tab.Length-i>3;i+=4)
            {
                number = tab[i] * 1000 + tab[i + 1] * 100 + tab[i + 2] * 10 + tab[i + 3];
                for(int j=0;j<16;j++)
                    if (number == tabk[j,0])
                    {
                        tabk[j, 1]++;
                        break;
                    }
            }

            double temp,si=0;

            for (int i = 0; i < 16; i++)
            {
                temp = (double)tabk[i, 1] * (double)tabk[i, 1];
                si += temp;
            }

            double res;
            res= (double)16 / ((double)tab.Length / 4) * si - 5000;

            if (res > 2.16 && res < 46.17)
                textBox3.Text += checkBox4.Text + " zaliczony X=" +Math.Round(res,4) + Environment.NewLine;
            else
                textBox3.Text += checkBox4.Text + " niezaliczony X=" + Math.Round(res, 4) + Environment.NewLine;
        }




        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          /* OpenFileDialog okienko = new OpenFileDialog();
            okienko.Filter = "Pliki textowe (txt)|*.txt";
            if (okienko.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                string path = okienko.FileName;
                StreamReader sr = new StreamReader(path, Encoding.Default);
                string s,txt="";
                textBox1.Clear();
                do
                {
                    s = sr.ReadLine();
                    sb.AppendLine(s);
                    txt+=s;
                 } while (s != null);
                textBox1.Text = textBox1.Text + txt;
                label1.Text = txt.Length.ToString();
                label2.Text = path.ToString();
                sr.Close();*/

            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            String txt = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            txt = File.ReadAllText(openFileDialog1.FileName);

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd. Nie można odczytać wskazenego pliku! \n " + ex.Message);
                }

               textBox1.AppendText(txt);

                label1.Text = txt.Length.ToString();
                label2.Text = openFileDialog1.FileName;

                char[] tabb;
                tabb = txt.ToCharArray();
                int i=0;
                tab = new int[tabb.Length];
                foreach (char a in tabb)
                {
                    tab[i] = (int)a-48;
                    i++;
                }
                lenght = tabb.Length;
            }
            else MessageBox.Show("Nie wczytano  pliku.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.Text += checkBox1.Text + " S1 równe " + Math.Round(pojedyncze()[2], 4) + Environment.NewLine;
            }

            if (checkBox2.Checked == true)
            {
                pary();
            }
        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            if (lenght >= 20000)
            {
                if (checkBox3.Checked == true)
                {
                    dlugie();
                }

                if (checkBox4.Checked == true)
                {
                    poker();
                }
            }
            else
                MessageBox.Show("Ciąg jest za krótki, minimalna długość to 20000");
        }

      

    
    }
}
