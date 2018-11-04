using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiaEdit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[] Station_List=new string[0];
        string[] Train_List=new string[0];
        int Station_Length = 0;
        int Train_Length = 0;
        public void LoadDia()
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.FileName = "List.txt";
            OFD.InitialDirectory = @"M:\";
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(OFD.FileName);
            }
            StreamReader sr = new StreamReader(OFD.FileName, System.Text.Encoding.GetEncoding("shift_jis"));
            while (sr.Peek() != -1)
            {
                string read = sr.ReadLine();
                //Console.WriteLine(read);
                switch (read)
                {
                    case "Rosen.":
                        continue;
                    case "Eki.":
                        Station(sr);
                        continue;
                    case "Ressyasyubetsu.":
                        continue;
                    case "Dia.":
                        continue;
                    case "Ressya.":
                        Train(sr);
                        continue;
                    default:
                        continue;
                }
            }
        }
        private void Station(StreamReader sr)
        {
            Console.WriteLine("Station");
            int a = Station_List.Length + 1;
            Array.Resize(ref Station_List, a);
            bool A=true;
            while (A)
            {
                string reading = sr.ReadLine();
                if (reading.IndexOf("=")==-1)
                {
                    //sr.ReadLine();
                    Station_Length++;
                    //Console.WriteLine("終わった");
                    A = false;
                }
                else
                {
                    string[] read = reading.Split('=');
                    switch (read[0])
                    {
                        case "Ekimei":
                            Station_List[Station_Length] = read[1];
                            Console.WriteLine(Station_List[Station_Length]);
                            continue;
                        default:
                            sr.ReadLine();
                            continue;
                    }
                }
            }
        }
        private void Train(StreamReader sr)
        {
            int a = Train_List.Length + 1;
            Array.Resize(ref Train_List, a);
            bool A = true;
            while (A)
            {
                string reading = sr.ReadLine();
                if (reading.IndexOf("=") == -1)
                {
                    //sr.ReadLine();
                    Station_Length++;
                    A = false;
                }
                else
                {
                    string[] read = reading.Split('=');
                    switch (read[0])
                    {
                        case "Ressyabangou":
                            Train_List[Train_Length] = read[1];
                            Console.WriteLine(Train_List[Train_Length]);
                            continue;
                        default:
                            sr.ReadLine();
                            continue;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDia();
        }
    }
}