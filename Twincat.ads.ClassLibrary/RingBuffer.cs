using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.TypeSystem;

namespace TwincatAds.ClassLibrary
{

    public class RingBuffer    {

        public int GraphPoint;        
        public List<double> XValue = new List<double>();
        public List<double> YVlaue = new List<double>();

        private int GraphPointstep = 0;


        //###########################################################
        public RingBuffer()
        {
            GraphPointstep = 0;
            GraphPoint = 10000;
        }


        //###########################################################
        public void ListClearAll()
        {
            XValue.Clear();
            YVlaue.Clear();
        }



        ////###########################################################
        //public double Timestamp(double time)
        //{
        //    return (ShowTimeSpanProperties(TimeSpan.FromDays(time)));

        //}



        ////###########################################################
        //private double ShowTimeSpanProperties(TimeSpan interval)
        //{

        //    Debug.WriteLine("{0,21}", interval);
        //    Debug.WriteLine("{0,-12}{1,8}       {2,-18}{3,21:N3}", "Days",
        //                      interval.Days, "TotalDays", interval.TotalDays);
        //    Debug.WriteLine("{0,-12}{1,8}       {2,-18}{3,21:N3}", "Hours",
        //                      interval.Hours, "TotalHours", interval.TotalHours);
        //    Debug.WriteLine("{0,-12}{1,8}       {2,-18}{3,21:N3}", "Minutes",
        //                      interval.Minutes, "TotalMinutes", interval.TotalMinutes);
        //    Debug.WriteLine("{0,-12}{1,8}       {2,-18}{3,21:N3}", "Seconds",
        //                      interval.Seconds, "TotalSeconds", interval.TotalSeconds);
        //    Debug.WriteLine("{0,-12}{1,8}       {2,-18}{3,21:N3}", "Milliseconds",
        //                      interval.Milliseconds, "TotalMilliseconds",
        //                      interval.TotalMilliseconds);
        //    Debug.WriteLine("{0,-12}{1,8}       {2,-18}{3,21:N0}", null, null,
        //        "Ticks", interval.Ticks);

        //    return ((interval.Hours * 60*60*1000) + (interval.Minutes * 60*1000) + (interval.Seconds*1000) + interval.Milliseconds);


        //}



        //###########################################################
        public void ListAdd(double xvalue, double yvalue )
        {  

            if (GraphPointstep < GraphPoint)
            {
                 XValue.Add(xvalue);
                 //YVlaue.Add(DateTime.Now);
                 YVlaue.Add(yvalue);
            }
            else
            {
                if (1 <= XValue.Count)
                {
                   XValue.Add(xvalue);
                   XValue.RemoveAt(0);

                }
                if (1 <= YVlaue.Count)
                {
                    YVlaue.Add(yvalue);
                    YVlaue.RemoveAt(0);

                }
            }                
         
            if (GraphPointstep < GraphPoint )
            {
                GraphPointstep += 1;
            }

           // Debug.WriteLine(DateTime.Now.TimeOfDay + "   " + XValue.Count + "    " + YVlaue.Count);

        }


        //###########################################################
        //public (int ALength ,double[] Xvalue, double[] Yvalue) ReadGraphFIFO()
        public (double[] Xvalue, double[] Yvalue) ReadGraphFIFO()
        {

            double[] xarray = new double[XValue.Count];
            double[] yarray = new double[YVlaue.Count];
            int arraycount = 0;

            xarray = XValue.ToArray();
            yarray = YVlaue.ToArray();
            arraycount = xarray.Length;

            //return (arraycount,xarray, yarray);
            return (xarray, yarray);
        }


        //buffercounter 1=> 1ms 
        //ads_ch 성분 

        public void ReadRingBuffer(double xvlaue, double yvalue)
        {

            ListAdd(xvlaue, yvalue);
        }
            //    int i = 0;
            //    foreach (var loop in ads_ch) {
            //        switch (i)
            //        {
            //            case 0:
            //                Timestamp[buffercounter] = ads_ch[i];
            //                break;
            //            case 1:
            //                SolState_1[buffercounter] = ads_ch[i];
            //                break;
            //            case 2:
            //                SolState_2[buffercounter] = ads_ch[i];
            //                break;
            //            case 3:
            //                UpperRunStep[buffercounter] = ads_ch[i];
            //                break;
            //            case 4:
            //                LowerRunStep[buffercounter] = ads_ch[i];
            //                break;

            //            //##################### 상램고속 
            //            case 5:
            //                UpperRamHiSpeed[buffercounter,4] = ads_ch[i];
            //                break;
            //            case 6:
            //                UpperRamHiSpeed[buffercounter, 5] = ads_ch[i];
            //                break;
            //            case 7:
            //                UpperRamHiSpeed[buffercounter, 6] = ads_ch[i];
            //                break;
            //            case 8:
            //                UpperRamHiSpeed[buffercounter, 7] = ads_ch[i];
            //                break;

            //            //##################### 상램 저속 
            //            case 9:
            //                UpperRamLowSpee[buffercounter, 0] = ads_ch[i];
            //                ListAdd(ads_ch[0], ads_ch[i]);
            //                break;
            //            case 10:
            //                UpperRamLowSpee[buffercounter, 1] = ads_ch[i];
            //                break;
            //            case 11:
            //                UpperRamLowSpee[buffercounter, 2] = ads_ch[i];
            //                break;
            //            case 12:
            //                UpperRamLowSpee[buffercounter, 3] = ads_ch[i];
            //                break;
            //            case 13:
            //                UpperRamLowSpee[buffercounter, 4] = ads_ch[i];
            //                break;
            //            case 14:
            //                UpperRamLowSpee[buffercounter, 5] = ads_ch[i];
            //                break;
            //            case 15:
            //                UpperRamLowSpee[buffercounter, 6] = ads_ch[i];
            //                break;
            //            case 16:
            //                UpperRamLowSpee[buffercounter, 7] = ads_ch[i];
            //                break;

            //            //##################### 상 펀치 1
            //            case 17:
            //                UpperPunch_1[buffercounter, 0] = ads_ch[i];
            //                break;
            //            case 18:
            //                UpperPunch_1[buffercounter, 1] = ads_ch[i];
            //                break;
            //            case 19:
            //                UpperPunch_1[buffercounter, 2] = ads_ch[i];
            //                break;
            //            case 20:
            //                UpperPunch_1[buffercounter, 3] = ads_ch[i];
            //                break;
            //            case 21:
            //                UpperPunch_1[buffercounter, 4] = ads_ch[i];
            //                break;
            //            case 22:
            //                UpperPunch_1[buffercounter, 5] = ads_ch[i];
            //                break;
            //            case 23:
            //                UpperPunch_1[buffercounter, 6] = ads_ch[i];
            //                break;
            //            case 24:
            //                UpperPunch_1[buffercounter, 7] = ads_ch[i];
            //                break;
            //            //##################### 상 펀치 2
            //            case 25:
            //                UpperPunch_2[buffercounter, 0] = ads_ch[i];
            //                break;
            //            case 26:
            //                UpperPunch_2[buffercounter, 1] = ads_ch[i];
            //                break;
            //            case 27:
            //                UpperPunch_2[buffercounter, 2] = ads_ch[i];
            //                break;
            //            case 28:
            //                UpperPunch_2[buffercounter, 3] = ads_ch[i];
            //                break;
            //            case 29:
            //                UpperPunch_2[buffercounter, 4] = ads_ch[i];
            //                break;
            //            case 30:
            //                UpperPunch_2[buffercounter, 5] = ads_ch[i];
            //                break;
            //            case 31:
            //                UpperPunch_2[buffercounter, 6] = ads_ch[i];
            //                break;
            //            case 32:
            //                UpperPunch_2[buffercounter, 7] = ads_ch[i];
            //                break;
            //            //##################### 상 펀치 3
            //            case 33:
            //                UpperPunch_3[buffercounter, 0] = ads_ch[i];
            //                break;
            //            case 34:
            //                UpperPunch_3[buffercounter, 1] = ads_ch[i];
            //                break;
            //            case 35:
            //                UpperPunch_3[buffercounter, 2] = ads_ch[i];
            //                break;
            //            case 36:
            //                UpperPunch_3[buffercounter, 3] = ads_ch[i];
            //                break;
            //            case 37:
            //                UpperPunch_3[buffercounter, 4] = ads_ch[i];
            //                break;
            //            case 38:
            //                UpperPunch_3[buffercounter, 5] = ads_ch[i];
            //                break;
            //            case 39:
            //                UpperPunch_3[buffercounter, 6] = ads_ch[i];
            //                break;
            //            case 40:
            //                UpperPunch_3[buffercounter, 7] = ads_ch[i];
            //                break;
            //            //###################### 피더 
            //            case 41:
            //                Feeder[buffercounter, 0] = ads_ch[i];
            //                break;
            //            case 42:
            //                Feeder[buffercounter, 1] = ads_ch[i];
            //                break;
            //            //###################### 배출 
            //            case 43:
            //                Discharge[buffercounter, 0] = ads_ch[i];
            //                break;
            //            case 44:
            //                Discharge[buffercounter, 1] = ads_ch[i];
            //                break;
            //            //##################### 다이 
            //            case 45:
            //                Die_ram[buffercounter, 0] = ads_ch[i];
            //                ListAdd(ads_ch[0], ads_ch[i]);
            //                break;
            //            case 46:
            //                Die_ram[buffercounter, 1] = ads_ch[i];
            //                break;
            //            case 47:
            //                Die_ram[buffercounter, 2] = ads_ch[i];
            //                break;
            //            case 48:
            //                Die_ram[buffercounter, 3] = ads_ch[i];
            //                break;
            //            case 49:
            //                Die_ram[buffercounter, 4] = ads_ch[i];
            //                break;
            //            case 50:
            //                Die_ram[buffercounter, 5] = ads_ch[i];
            //                break;
            //            case 51:
            //                Die_ram[buffercounter, 6] = ads_ch[i];
            //                break;
            //            case 52:
            //                Die_ram[buffercounter, 7] = ads_ch[i];
            //                break;
            //            //##################### 하1 
            //            case 53:
            //                LowerPunch_1[buffercounter, 0] = ads_ch[i];
            //                break;
            //            case 54:
            //                LowerPunch_1[buffercounter, 1] = ads_ch[i];
            //                break;
            //            case 55:
            //                LowerPunch_1[buffercounter, 2] = ads_ch[i];
            //                break;
            //            case 56:
            //                LowerPunch_1[buffercounter, 3] = ads_ch[i];
            //                break;
            //            case 57:
            //                LowerPunch_1[buffercounter, 4] = ads_ch[i];
            //                break;
            //            case 58:
            //                LowerPunch_1[buffercounter, 5] = ads_ch[i];
            //                break;
            //            case 59:
            //                LowerPunch_1[buffercounter, 6] = ads_ch[i];
            //                break;
            //            case 60:
            //                LowerPunch_1[buffercounter, 7] = ads_ch[i];
            //                break;
            //            //##################### 하2
            //            case 61:
            //                LowerPunch_2[buffercounter, 0] = ads_ch[i];
            //                break;
            //            case 62:
            //                LowerPunch_2[buffercounter, 1] = ads_ch[i];
            //                break;
            //            case 63:
            //                LowerPunch_2[buffercounter, 2] = ads_ch[i];
            //                break;
            //            case 64:
            //                LowerPunch_2[buffercounter, 3] = ads_ch[i];
            //                break;
            //            case 65:
            //                LowerPunch_2[buffercounter, 4] = ads_ch[i];
            //                break;
            //            case 66:
            //                LowerPunch_2[buffercounter, 5] = ads_ch[i];
            //                break;
            //            case 67:
            //                LowerPunch_2[buffercounter, 6] = ads_ch[i];
            //                break;
            //            case 68:
            //                LowerPunch_2[buffercounter, 7] = ads_ch[i];
            //                break;

            //            //##################### 하3
            //            case 69:
            //                LowerPunch_3[buffercounter, 0] = ads_ch[i];
            //                break;
            //            case 70:
            //                LowerPunch_3[buffercounter, 1] = ads_ch[i];
            //                break;
            //            case 71:
            //                LowerPunch_3[buffercounter, 2] = ads_ch[i];
            //                break;
            //            case 72:
            //                LowerPunch_3[buffercounter, 3] = ads_ch[i];
            //                break;
            //            case 73:
            //                LowerPunch_3[buffercounter, 4] = ads_ch[i];
            //                break;
            //            case 74:
            //                LowerPunch_3[buffercounter, 5] = ads_ch[i];
            //                break;
            //            case 75:
            //                LowerPunch_3[buffercounter, 6] = ads_ch[i];
            //                break;
            //            case 76:
            //                LowerPunch_3[buffercounter, 7] = ads_ch[i];
            //                break;

            //            //##################### 하4
            //            case 77:
            //                LowerPunch_4[buffercounter, 0] = ads_ch[i];
            //                break;
            //            case 78:
            //                LowerPunch_4[buffercounter, 1] = ads_ch[i];
            //                break;
            //            case 79:
            //                LowerPunch_4[buffercounter, 2] = ads_ch[i];
            //                break;
            //            case 80:
            //                LowerPunch_4[buffercounter, 3] = ads_ch[i];
            //                break;
            //            case 81:
            //                LowerPunch_4[buffercounter, 4] = ads_ch[i];
            //                break;
            //            case 82:
            //                LowerPunch_4[buffercounter, 5] = ads_ch[i];
            //                break;
            //            case 83:
            //                LowerPunch_4[buffercounter, 6] = ads_ch[i];
            //                break;
            //            case 84:
            //                LowerPunch_4[buffercounter, 7] = ads_ch[i];
            //                break;

            //            //##################### 코어
            //            case 85:
            //                core_punch[buffercounter, 0] = ads_ch[i];
            //                break;
            //            case 86:
            //                core_punch[buffercounter, 1] = ads_ch[i];
            //                break;
            //            case 87:
            //                core_punch[buffercounter, 2] = ads_ch[i];
            //                break;
            //            case 88:
            //                core_punch[buffercounter, 3] = ads_ch[i];
            //                break;
            //            case 89:
            //                core_punch[buffercounter, 4] = ads_ch[i];
            //                break;
            //            case 90:
            //                core_punch[buffercounter, 5] = ads_ch[i];
            //                break;
            //            case 91:
            //                core_punch[buffercounter, 6] = ads_ch[i];
            //                break;
            //            case 92:
            //                core_punch[buffercounter, 7] = ads_ch[i];
            //                break;

            //            //##################### pump
            //            case 93:
            //                HydUtil[buffercounter, 0] = ads_ch[i];
            //                break;
            //            case 94:
            //                HydUtil[buffercounter, 1] = ads_ch[i];
            //                break;
            //            case 95:
            //                HydUtil[buffercounter, 2] = ads_ch[i];
            //                break;
            //            case 96:
            //                HydUtil[buffercounter, 3] = ads_ch[i];
            //                break;
            //            case 97:
            //                HydUtil[buffercounter, 4] = ads_ch[i];
            //                break;
            //            case 98:
            //                HydUtil[buffercounter, 5] = ads_ch[i];
            //                break;
            //            case 99:
            //                HydUtil[buffercounter, 6] = ads_ch[i];
            //                break;

            //            //##################### 제품정보 
            //            case 100:
            //                Product[buffercounter, 0] = ads_ch[i];
            //                break;
            //            case 101:
            //                Product[buffercounter, 1] = ads_ch[i];
            //                break;

            //            //##################### 운전모드 
            //            case 102:
            //                Mechine[buffercounter, 0] = ads_ch[i];
            //                break;

            //            //##################### 알람코드 
            //            case 103:
            //                AlarmCode[buffercounter, 0] = ads_ch[i];
            //                break;
            //            case 104:
            //                AlarmCode[buffercounter, 1] = ads_ch[i];
            //                break;
            //            case 105:
            //                AlarmCode[buffercounter, 2] = ads_ch[i];
            //                break;
            //            case 106:
            //                AlarmCode[buffercounter, 3] = ads_ch[i];
            //                break;
            //        }
            //        i += 1;
            //    }

            //}



        }
}
