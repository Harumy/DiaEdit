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
        string[][] Station_List=new string[0][];
        string[][] Train_List=new string[0][];
        int[][][] Data = new int[0][][];
        int Station_Length = 0;
        int Train_Length = 0;
        public void LoadDia()
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.FileName = "大曲-盛岡.oud";
            OFD.InitialDirectory = @"C:\Users\harum\Desktop";
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
                        DataEditStation();
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
            Station_List[a - 1] = new string[2];
            bool A=true;
            while (A)
            {
                string reading = sr.ReadLine();
                if (reading.IndexOf("=")==-1)
                {
                    //sr.ReadLine();
                    Station_Length++;
                    Array.Resize(ref Data, Station_List.Length);
                    //Console.WriteLine("終わった");
                    A = false;
                }
                else
                {
                    string[] read = reading.Split('=');
                    switch (read[0])
                    {
                        case "Ekimei":
                            Station_List[Station_Length][0] = read[1];
                            continue;
                        case "Ekikibo":
                            Station_List[Station_Length][1] = read[1];
                            continue;
                        default:
                            continue;
                    }
                }
            }
        }
        private void Train(StreamReader sr)
        {
            Console.WriteLine("Train");
            int a = Train_List.Length + 1;
            int b = a - 1;
            string UD="";
            Array.Resize(ref Train_List, a);
            Train_List[b] = new string[6];//列車番号,列車名,号数,種別,備考,行先
            bool A =new bool();
            A = true;
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
                            Train_List[Train_Length][0] = read[1];
                            Console.WriteLine(read[1]);
                            continue;
                        case "Ressyamei":
                            Train_List[Train_Length][1] = read[1];
                            continue;
                        case "Gousuu":
                            Train_List[Train_Length][2] = read[1];
                            continue;
                        case "Syubetsu":
                            Train_List[Train_Length][3] = read[1];
                            continue;
                        case "Bikou":
                            Train_List[Train_Length][4] = read[1];
                            continue;
                        case "EkiJikoku":
                            EditDataArray(b,read[1],UD);
                            continue;
                        case "Houkou":
                            UD = read[1];
                            continue;
                        default:
                            sr.ReadLine();
                            continue;
                    }
                }
            }
        }
        private void DataEditStation()
        {
            int length = Station_List.Length;
            for (int i = 0; i < length; i++){
                if(Station_List[i][1]== "Ekikibo_Syuyou")
                {
                    Data[i] = new int[2][];
                    Data[i][0] = new int[0];
                    Data[i][1] = new int[0];
                }
                else
                {
                    Data[i] = new int[1][];
                    Data[i][0] = new int[0];
                }
            }
        }
        private void EditDataArray(int a,string dia,string Houkou)
        {
            Console.WriteLine(dia);
            string[] tmp1 = dia.Split(new String[] { "," }, StringSplitOptions.None);
            if (tmp1.Length != Station_List.Length)
            {
                Array.Resize(ref tmp1, Station_List.Length);
            }
            for(int j = 0; j < Station_List.Length; j++)
            {
                int i=j;
                if (Houkou == "Nobori")
                {
                    i = Station_List.Length - 1 - j;
                }
                if (tmp1[i] != null)
                {
                    string[] tmp = tmp1[i].Split(';');
                    if (tmp[0] == "")
                    {
                        if (Station_List[i][1] == "Ekikibo_Syuyou")
                        {
                            int length0 = Data[i][0].Length;
                            int length1 = Data[i][1].Length;
                            Array.Resize(ref Data[i][0], length0 + 1);
                            Array.Resize(ref Data[i][1], length1 + 1);
                        }
                        else
                        {
                            int length0 = Data[i][0].Length;
                            Array.Resize(ref Data[i][0], length0 + 1);
                        }
                    }
                    else
                    {
                        switch (tmp[0])
                        {
                            case "1":
                                string[] time = tmp;
                                if (Station_List[i][1] == "Ekikibo_Syuyou")
                                {
                                    int length0 = Data[i][0].Length;
                                    int length1 = Data[i][1].Length;
                                    Array.Resize(ref Data[i][0], length0 + 1);
                                    Array.Resize(ref Data[i][1], length1 + 1);
                                    if (time[1].IndexOf('/') != -1)
                                    {
                                        time = time[1].Split('/');
                                        if (time[0] != "")
                                        {
                                            Data[i][0][a] = int.Parse(time[0]);
                                        }
                                        else
                                        {
                                            Data[i][0][a] = 0;
                                        }
                                        if (time[1] != "")
                                        {
                                            Data[i][1][a] = int.Parse(time[1]);
                                        }
                                        else
                                        {
                                            Data[i][1][a] = 0;
                                        }
                                    }
                                    else
                                    {
                                        Data[i][1][a] = int.Parse(time[1]);
                                    }
                                }
                                else
                                {
                                    int length0 = Data[i][0].Length;
                                    Array.Resize(ref Data[i][0], length0 + 1);
                                    if (time[1].IndexOf('/') != -1)
                                    {
                                        time = time[1].Split('/');

                                        if (time[1] != "")
                                        {
                                            Data[i][0][a] = int.Parse(time[1]);
                                        }
                                        else
                                        {
                                            Data[i][0][a] = 0;
                                        }
                                    }
                                    else
                                    {
                                        Data[i][0][a] = int.Parse(time[1]);
                                    }
                                }
                                continue;
                            default:
                                if (Station_List[i][1] == "Ekikibo_Syuyou")
                                {
                                    int length0 = Data[i][0].Length;
                                    int length1 = Data[i][1].Length;
                                    Array.Resize(ref Data[i][0], length0 + 1);
                                    Array.Resize(ref Data[i][1], length1 + 1);
                                }
                                else
                                {
                                    int length0 = Data[i][0].Length;
                                    Array.Resize(ref Data[i][0], length0 + 1);
                                }
                                continue;
                        }
                    }
                }
                else
                {
                    if (Station_List[i][1] == "Ekikibo_Syuyou")
                    {
                        int length0 = Data[i][0].Length;
                        int length1 = Data[i][1].Length;
                        Array.Resize(ref Data[i][0], length0 + 1);
                        Array.Resize(ref Data[i][1], length1 + 1);
                    }
                    else
                    {
                        int length0 = Data[i][0].Length;
                        Array.Resize(ref Data[i][0], length0 + 1);
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