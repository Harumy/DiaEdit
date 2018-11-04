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
        string[] Station_List;
        string[] Train_List;
        public void LoadDia()
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.FileName = "List.txt";
            OFD.InitialDirectory = @"M:\";
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(OFD.FileName);
            }
            StreamReader sr = new StreamReader(OFD.FileName);
            int i = 0;
            while (sr.Peek() != -1)
            {
                i++;
                string read = sr.ReadLine();
                //Console.WriteLine(read);
                switch (read)
                {
                    case "Rosen":
                        continue;
                    case "Eki":
                        Station();
                        continue;
                    case "Ressyasyubetsu":
                        continue;
                    case "Dia":
                        continue;
                    case "Ressya":
                        continue;
                    default:
                        continue;
                }
            }
        }
        private void Station()
        {
            Console.WriteLine("Station");
        }
        private void Train()
        {
            Console.WriteLine("Train");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDia();
        }
    }
}