
using ScottPlot.Plottable;
using ScottPlot;
using TwincatAds.ClassLibrary;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Timers;
using Timer = System.Windows.Forms.Timer;
using System.Drawing;
using System.Text;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using MySqlX.XDevAPI.Relational;
using MySqlX.XDevAPI;
using ScottPlot.Statistics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Policy;
using System.Threading;
using System.Windows.Forms;
using TwinCAT.PlcOpen;
using Org.BouncyCastle.Asn1.X509.Qualified;

namespace Graph_WinFormsApp_3
{

    //########################################
    public partial class Form1 : Form
    {
        Twincat_ADS twincat_ads = new Twincat_ADS();
        Timer timer = new System.Windows.Forms.Timer();
        RingBuffer ringbuffer_upper_svpos = new RingBuffer();
        RingBuffer ringbuffer_die_svpos = new RingBuffer();
        RingBuffer feeder_svpos = new RingBuffer();
        private System.Timers.Timer aTimer;
        private int Pointlength = 10000;
        private int PagebufferCounter = 0;
        private int PagebufferCounter_old = 0;
        private int PagebufferCounter_ea = 0;
        private int Timer_Stop = 0;
        private static string strFilePath = @"C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/DataWrite_id.csv";
       // private static string strFilePath = @"DataWrite_id.csv";
        private static string connString = "Server=127.0.0.1;Database=pressdb;port=3306;User=root;Password=tlmysql01";
        //private static string connString = "Server=hongserver.synology.me;Database=pressdb;port=3306;User=tshong;Password=TL1Comport!;AllowLoadLocalInfile=true";


        private string strSeperator = ",";
        private int db_enable = 0;
        private List<double> ADS_list_data = new List<double>();
        private static MySqlConnection conn = new MySqlConnection(connString);

        private int write_csv_load = 0;




        public Form1()
        {
            InitializeComponent();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

            conn.Open();
            //########
            Pointlength = Convert.ToInt16(textBox1.Text)*1000;
            ringbuffer_upper_svpos.GraphPoint = Pointlength;
            ringbuffer_die_svpos.GraphPoint = Pointlength;
            feeder_svpos.GraphPoint = Pointlength;

            ringbuffer_upper_svpos.ListClearAll();
            ringbuffer_die_svpos.ListClearAll();
            feeder_svpos.ListClearAll();


            var now_1 = DateTime.Now;

            twincat_ads.HanbleName("GVL_RING_BUFFER.ADS_MON_DATA");
            twincat_ads.HanbleName("GVL_POSITION.POSITION_UPPER_RAM_LOP");


            //#### ����� buffer�� ��ȿ�� 
            twincat_ads.HanbleName("GVL_RING_BUFFER.PAGE_INDEX");

            for (int i=1; i < 51; i++)
            {

                string ads_no = String.Format("{0:00}", i);
                string ads_name = "GVL_RING_BUFFER.BUFFER_" + ads_no;
                twincat_ads.HanbleName(ads_name);
               // Debug.WriteLine(DateTime.Now.TimeOfDay +"    "+ i);
            }




            //twincat_ads.DeleteHandvalue();
        }





        private void timer_Tick(object sender, EventArgs e)
        {


            //ADS_Read();
            ADS_RingBufferRead();

            //int arry_limit1 = 5; //list�� ���� 
            //double[] xx = DataGen.Consecutive(arry_limit1);
            //double[] yy = new double[arry_limit1];
            //yy[0] = 1;
            //yy[1] = 2;
            //yy[2] = 3;
            //yy[3] = 4;
            //yy[4] = 5;

            //formsPlot1.Reset();
            //formsPlot1.Plot.AddScatter(xx, yy);
            //formsPlot1.Refresh();


            //Thread t1 = new Thread(new ThreadStart(ADS_Read));
            //Thread t2 = new Thread(new ThreadStart(Graph));
            //t1.Start();
            //t2.Start();
            //t1.Join();
            //t2.Join();
        }

        private void ADS_RingBufferRead()
        {

            double[,] ads_ringbuffer = new double[100, 107];

            double[] ads_1array = new double[108];
            string ads_no;
            string ads_name;
            int write_csv = 0;




            PagebufferCounter =  (int) twincat_ads.Int32AdsRead("GVL_RING_BUFFER.PAGE_INDEX");

            if (PagebufferCounter_old <= PagebufferCounter)
            {
                PagebufferCounter_ea = PagebufferCounter - PagebufferCounter_old;
            }
            else
            {
                PagebufferCounter_ea = (49 - PagebufferCounter_old) + PagebufferCounter;
            }

            if ((1 <= PagebufferCounter_ea) && (PagebufferCounter_old != PagebufferCounter))
            {
                int add_no = PagebufferCounter_old + 1;
                for (int y = 0; y < PagebufferCounter_ea; y++)
                {
                    if (51 <= PagebufferCounter )
                    {
                        add_no = 1;
                    }

                    ads_no = String.Format("{0:00}", (int)add_no);
                    ads_name = "GVL_RING_BUFFER.BUFFER_" + ads_no;
                    ads_ringbuffer = twincat_ads.LRealArray2X2AdsRead(ads_name, 100, 107);
                    StringBuilder sbOutput = new StringBuilder();


                    if (cb_DBEnable.Checked)
                    {
                        for (int i = 0; i <100; i++)
                        {
                            ads_1array[0] = 0;
                            for (int x = 0; x < 100; x++)
                            {
                                ads_1array[x+1] = ads_ringbuffer[i, x];
                            }

                            double[][] inaOutput = new double[][] { ads_1array };
                            int ilength = inaOutput.GetLength(0);

                            for (int x1 = 0; x1 < ilength; x1++)
                            {
                                    sbOutput.AppendLine(string.Join(strSeperator, inaOutput[x1]));                                 
                            }

                            try
                            {                               
                                // Create and write the csv file
                                File.WriteAllText(strFilePath, sbOutput.ToString());
                                write_csv = 1;
                            }
                            catch
                            {

                            }

                        }


                    }


                    try
                    {
                        for (int i = 0; i < 100; i++)
                        {

                            if (4000 <= ads_ringbuffer[i, 0])
                            {
                                ringbuffer_upper_svpos.ReadRingBuffer(ads_ringbuffer[i, 0], ads_ringbuffer[i, 9]);
                                ringbuffer_die_svpos.ReadRingBuffer(ads_ringbuffer[i, 0], ads_ringbuffer[i, 45]);
                                feeder_svpos.ReadRingBuffer(ads_ringbuffer[i, 0], ads_ringbuffer[i, 41]);
                            }

                        }
                    }
                    catch
                    {

                    }

                    add_no += 1;

                }



                if (write_csv == 1)
                {

                    //var c1 = DateTime.Now;
                    var b1 = new MySqlBulkLoader(conn);
                    //b1.Local = true;
                    b1.TableName = "testscope04";
                    b1.FieldTerminator = ",";
                    b1.LineTerminator = "\r\n";
                    b1.FileName = strFilePath;
                    b1.NumberOfLinesToSkip = 0;

                    var count = b1.Load();
                    //var c2 = DateTime.Now;

                   // Debug.WriteLine(c2 - c1);

                    //string sql = "SELECT ID,MEAS_DATE_TIME, SOL_VALVE_01_STATUS, SOL_VALVE_02_STATUS FROM testscope04";
                    //MySqlCommand cmd = new MySqlCommand(sql, conn);
                    // MySqlDataReader rdr = cmd.ExecuteReader();
                    // while (rdr.Read())
                    // {
                    //    Debug.WriteLine(rdr[0] + " -- " + rdr[1] + " -- " + rdr[2]);
                    // }
                }

                  Graph();
            }
             
            Debug.WriteLine(DateTime.Now.TimeOfDay + "      " + PagebufferCounter + "     " + PagebufferCounter_ea + "   " );
            PagebufferCounter_old = PagebufferCounter;
        }

 

        private void Graph()
        {
            //  var now_21 = DateTime.Now;
            (double[] upsvxpoint,double[] upsvypoint) = ringbuffer_upper_svpos.ReadGraphFIFO();
            (double[] diesvxpoint, double[] diesvypoint) = ringbuffer_die_svpos.ReadGraphFIFO();
            (double[] feedsvxpoint, double[] feedsvypoint) = feeder_svpos.ReadGraphFIFO();

            Plot plot = new Plot(800, 600);
            formsPlot1.Reset();

            formsPlot1.Plot.Style(Style.Blue1);
            formsPlot1.Plot.Title("Powder Press");
            formsPlot1.Plot.XLabel("Time Base");
            formsPlot1.Plot.YLabel("Axis [mm]");


            //formsPlot1.Plot.AddTooltip(label: "Upper Ram", x: upsvxpoint[100], y: upsvypoint[100]);
            formsPlot1.Plot.XAxis.DateTimeFormat(true);

            formsPlot1.Plot.AddScatter(upsvxpoint, upsvypoint, Color.Red, 3,markerSize: 0);
            formsPlot1.Plot.AddScatter(upsvxpoint, diesvypoint, Color.GreenYellow, 3, markerSize: 0);
            formsPlot1.Plot.AddScatter(upsvxpoint, feedsvypoint, Color.Pink, 3, markerSize: 0);

            formsPlot1.Refresh();

            if (Timer_Stop == 1)
            {
                timer.Stop();
                Timer_Stop = 0;
            }

        }

        //########################################
        public class ADS : Twincat_ADS
        {
            Twincat_ADS twincat_ads = new Twincat_ADS();
        }

        //########################################
        private void formsPlot2_Load(object sender, EventArgs e)
        {


        }

        //########################################
        private void button1_Click(object sender, EventArgs e)
        {

            timer.Interval = 50; // ms
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        //########################################
        private void button2_Click(object sender, EventArgs e)
        {

            Timer_Stop = 1;

        }

        //########################################
        public enum NameOfAxis
        {
            UpperRam = 0,
            Up1Punch,
            Up2Punch,
            Up3Punch,
            Feeder,
            Discharge,
            Die,
            Lp1Punch,
            LP2Punch,
            Lp3Punch,
            LP4Punch,
            CorePunch
        }

        private void formsPlot1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Pointlength = Convert.ToInt16(textBox1.Text) * 1000;
            ringbuffer_upper_svpos.GraphPoint = Pointlength;
            ringbuffer_die_svpos.GraphPoint = Pointlength;
            feeder_svpos.GraphPoint = Pointlength;
        }
    }
}