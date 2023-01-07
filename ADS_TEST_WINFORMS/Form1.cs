using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwincatAds.ClassLibrary;

namespace ADS_TEST_WINFORMS
{

    //########################################
    public class ADS : Twincat_ADS
    {

    }

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Twincat_ADS twincat_ads = new Twincat_ADS();

            uint handleStructOPSI = ADS.MyAds.CreateVariableHandle("GVL_WIZARD_PARAM.OUTER_PLATE_SAMPLE_INFO");
            uint handleStructMPI = ADS.MyAds.CreateVariableHandle("GVL_WIZARD_PARAM.MIDDLE_PLATE_SAMPLE_INFO");
            uint handleStructIPSI = ADS.MyAds.CreateVariableHandle("GVL_WIZARD_PARAM.INNER_PLATE_SAMPLE_INFO");
            uint handleStructIMPSI = ADS.MyAds.CreateVariableHandle("GVL_WIZARD_PARAM.INNERMOST_PLATE_SAMPLE_INFO");
            uint handleStructCPAI = ADS.MyAds.CreateVariableHandle("GVL_WIZARD_PARAM.CORE_PLATE_SAMPLE_INFO");
            uint handleStructDPSI = ADS.MyAds.CreateVariableHandle("GVL_WIZARD_PARAM.DIE_PLATE_SAMPLE_INFO");
            var now_1 = DateTime.Now;

            twincat_ads.HanbleName("GVL_RING_BUFFER.ADS_MON_DATA");
            twincat_ads.HanbleName("MAIN.TEST_BOOL");
            twincat_ads.HanbleName("MAIN.TEST_BOOL_ARRY");

            twincat_ads.HanbleName("MAIN.test_int_value");
            twincat_ads.HanbleName("MAIN.TEST_INT_VALUE_ARRAY");
            twincat_ads.HanbleName("MAIN.TEST_INT_VALUE_ARRAY22");

            twincat_ads.HanbleName("MAIN.test_uint_value");
            twincat_ads.HanbleName("MAIN.TEST_UINT_VALUE_ARRAY");

            twincat_ads.HanbleName("MAIN.TEST_DINT");
            twincat_ads.HanbleName("MAIN.TEST_DINT_VALUE_ARRAY");

            twincat_ads.HanbleName("MAIN.TEST_UDINT");
            twincat_ads.HanbleName("MAIN.TEST_UDINT_VALUE_ARRAY");

            twincat_ads.HanbleName("MAIN.TEST_REAL");
            twincat_ads.HanbleName("MAIN.TEST_REAL_VALUE_ARRAY");

            twincat_ads.HanbleName("MAIN.TEST_LREAL");
            twincat_ads.HanbleName("MAIN.TEST_LREAL_VALUE_ARRAY");

            twincat_ads.HanbleName("MAIN.TEST_STRING");
            twincat_ads.HanbleName("MAIN.TEST_STRING_ARRAY");


            void ADS_Read()
            {
                var now_21 = DateTime.Now;
                for (int i = 0; i < 1; i++)
                {
                    float[] DATA = twincat_ads.RealArrayAdsRead("GVL_RING_BUFFER.ADS_MON_DATA", 800);
                }
                var now_22 = DateTime.Now;
                Debug.WriteLine((now_22 - now_21));
            }

            void Graph()
            {
                var now_21 = DateTime.Now;
                for (int i = 0; i < 1; i++)
                {
                    float[] DATA = twincat_ads.RealArrayAdsRead("GVL_RING_BUFFER.ADS_MON_DATA", 800);
                }
                var now_22 = DateTime.Now;
                Debug.WriteLine((now_22 - now_21));
            }

            Thread t1 = new Thread(new ThreadStart(ADS_Read));
            Thread t2 = new Thread(new ThreadStart(Graph));
            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();


        }





    }
}