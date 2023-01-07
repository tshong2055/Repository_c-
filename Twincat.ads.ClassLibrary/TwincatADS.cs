
//TwinCAT.ADS 6.0.216

using System.Drawing;
using System.Reflection.Metadata;
using System.Collections.Generic;
using TwinCAT.Ads;
using TwinCAT.TypeSystem;
using System.Xml.Linq;
using System.Drawing.Drawing2D;
using System;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

//########### ads를 class로 접근하도록 만든 프로그램
//########### nuget에서 TwinCAT.ADS 6.0.216를 받아 설치하고 시험 

namespace TwincatAds.ClassLibrary
{

    ///////////////////////////////////////////////////////////////////////////////////
    public class Twincat_ADS
    {
        //##############################
        public static AdsClient MyAds = new AdsClient();
        public static Dictionary<string, uint> dict = new Dictionary<string, uint>();
        //public static uint handlevalue = 0;
        public static string handlename;
        public bool Connected_status;
        public bool HanbleMakeError = false;
        public bool DictMakeError = false;
        public uint handlevalue = 0;

        //##############################
        public Twincat_ADS()  //클래스 명과 동일하게 선언하면 샌성자가 동작할때 동작함 
        {
            MyAds.Connect(AmsNetId.Local, 851);//default port of Twincat 3 is 851 //Twincat 2 is 801
            do
            {
                Connected_status = false;
                //연결을 기다려 주면 핸들값을 바로 가져오고 
                //연결을 기다려 주지않으면 연결후(연결까지 시간이 흐름) 핸들을 가져온다. -> error로 처리하지 않고 좌측처럼한다.
            } while (MyAds.IsConnected == false);

            Connected_status = true;
        }
        //##############################
        public bool Connected()
        {
            return Connected_status;
        }

        //##############################
        public void ResetError()
        {
            DictMakeError = false;
            HanbleMakeError = false;
        }

        //##############################
        public void HanbleName(string name)
        {
            try
            {
                uint handle_value = MyAds.CreateVariableHandle(name);
                try
                {
                    dict.Add(name, handle_value);// int
                }
                catch
                {
                    Console.WriteLine("-----  변수를 추가할수 없음 (중복선언 확인필요)  -----");
                    DictMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Twincat.asd접근이상 -----");
                HanbleMakeError = true;
            }

        }

        //#########################################################################################
        //#########################################################################################
        //#########################################################################################

        //##############################
        public bool BoolAdsRead(string HandleName)
        {
            bool valueToBool = false;
            //var now_1 = DateTime.Now;
            try
            {
                uint handlevalue = (uint)dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);  //호출이후 여러번의 처리하고 닫은다음 다시 크레이트를 해야함 
                //this.handlevalue = handlevalue;
                try
                {
                    valueToBool = (bool)MyAds.ReadAny(handlevalue, typeof(bool));// Bool
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }

            //Console.WriteLine($"Bool handle= {now_2 - now_1}, class = {now_3 - now_2}, class = {now_4 - now_3}, class = {now_5 - now_4}");
            return valueToBool;

        }

        //##############################
        public void BoolAdsWrite(string HandleName, bool boolvalue)
        {
            try
            {
                uint handlevalue = dict[HandleName]; // uint
               // handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, boolvalue);
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
            }

        }

        //##############################
        public bool[] BoolArrayAdsRead(string HandleName, int ArryC)
        {
            bool[] IntArr = new bool[ArryC];
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    IntArr = (bool[])MyAds.ReadAny(handlevalue, typeof(bool[]), new int[] { ArryC });
                   // MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
            return IntArr;
        }

        //##############################
        public void BoolArrayAdsWrite(string HandleName, bool[] value)
        {
            int ArryC_len = value.Length; //길이를 구하여 사용 
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, value, new int[] { ArryC_len });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
        }
        //}

        /////////////////////////////////////////////////////////////////////////////////////
        //public class Int16ADS
        //{
        //    //##############################
        //    public static AdsClient MyAds = new AdsClient();
        //    public static uint handlevalue = 0;
        //    public static string handlename;

        //    public Int16ADS()  //클래스 명과 동일하게 선언하면 샌성자가 동작할때 동작함 
        //    {
        //        MyAds.Connect(AmsNetId.Local, 851);//default port of Twincat 3 is 851 //Twincat 2 is 801
        //    }


        //    //##############################
        //    public void HanbleName(string name)
        //    {
        //        handlename = name;
        //        //MyAds.Connect(AmsNetId.Local, 851);//default port of Twincat 3 is 851 //Twincat 2 is 801
        //        handlevalue = MyAds.CreateVariableHandle(handlename); // int
        //    }

        //##############################
        public Int16 Int16AdsRead(string HandleName)
        {
            Int16 valueToRead = 0;
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    valueToRead = (Int16)MyAds.ReadAny(handlevalue, typeof(Int16));
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
            return valueToRead;
        }

        //##############################
        public void Int16AdsWrite(string HandleName, Int16 intvalue)
        {
            try
            {
                uint handlevalue = dict[HandleName]; // uint
               // handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, intvalue);
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
        }

        //##############################
        public Int16[] Int16ArrayAdsRead(string HandleName, int ArryC)
        {
            Int16[] IntArr = new Int16[ArryC];
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    IntArr = (Int16[])MyAds.ReadAny(handlevalue, typeof(Int16[]), new int[] { ArryC });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
            return IntArr;
        }

        //##############################
        public void Int16ArrayAdsWrite(string HandleName, Int16[] value)
        {
            try
            {
                int ArryC_len = value.Length; //길이를 구하여 사용 
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, value, new int[] { ArryC_len });
                   // MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
        }

        //##############################
        public Int16[,] Int16Array2X2AdsRead(string HandleName, int ArryC, int ArryB)
        {
            Int16[,] IntArr = new short[ArryC, ArryB];
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //uint handlevalue = 218107708;
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    IntArr = (Int16[,])MyAds.ReadAny(handlevalue, typeof(Int16[,]), new int[] { ArryC, ArryB });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
            return IntArr;
        }

        //##############################
        public void Int16Array2X2AdsWrite(string HandleName, Int16[,] value)
        {
            try
            {
                int ArryC_len = value.Length; //길이를 구하여 사용 
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, value, new int[] { ArryC_len });
                   // MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }

        }


        //}

        /////////////////////////////////////////////////////////////////////////////////////
        //public class UInt16ADS
        //{
        //    //##############################
        //    public static AdsClient MyAds = new AdsClient();
        //    public static uint handlevalue = 0;
        //    public static string handlename;

        //    public UInt16ADS()  //클래스 명과 동일하게 선언하면 샌성자가 동작할때 동작함 
        //    {
        //        MyAds.Connect(AmsNetId.Local, 851);//default port of Twincat 3 is 851 //Twincat 2 is 801
        //    }


        //    //##############################
        //    public void HanbleName(string name)
        //    {
        //        handlename = name;
        //       //MyAds.Connect(AmsNetId.Local, 851);//default port of Twincat 3 is 851 //Twincat 2 is 801
        //        handlevalue = MyAds.CreateVariableHandle(handlename); // int
        //    }

        //##############################
        public UInt16 UInt16AdsRead(string HandleName)
        {
            UInt16 valueToRead = 0;
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    valueToRead = (UInt16)MyAds.ReadAny(handlevalue, typeof(UInt16));
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }



            return valueToRead;
        }

        //##############################
        public void UInt16AdsWrite(string HandleName, UInt16 intvalue)
        {
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, intvalue);
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }


        }

        //##############################
        public UInt16[] UInt16ArrayAdsRead(string HandleName, int ArryC)
        {
            UInt16[] IntArr = new UInt16[ArryC];
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    IntArr = (UInt16[])MyAds.ReadAny(handlevalue, typeof(UInt16[]), new int[] { ArryC });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
            //##########
            return IntArr;
        }

        //##############################
        public void UInt16ArrayAdsWrite(string HandleName, UInt16[] value)
        {
            try
            {
                int ArryC_len = value.Length; //길이를 구하여 사용 
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, value, new int[] { ArryC_len });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
        }

        //##############################
        public UInt16[,] UInt16Array2X2AdsRead(string HandleName, int ArryC, int ArryB)
        {
            UInt16[,] IntArr = new UInt16[ArryC, ArryB];
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    IntArr = (UInt16[,])MyAds.ReadAny(handlevalue, typeof(UInt16[,]), new int[] { ArryC, ArryB });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
            return IntArr;
        }

        //##############################
        public void UInt16Array2X2AdsWrite(string HandleName, UInt16[,] value)
        {
            try
            {
                int ArryC_len = value.Length; //길이를 구하여 사용 
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, value, new int[] { ArryC_len });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }

        }


        //}


        /////////////////////////////////////////////////////////////////////////////////////
        //public class Int32ADS
        //{
        //    //##############################
        //    public static AdsClient MyAds = new AdsClient();
        //    public static uint handlevalue = 0;
        //    public static string handlename;


        //    public Int32ADS()  //클래스 명과 동일하게 선언하면 샌성자가 동작할때 동작함 
        //    {
        //        MyAds.Connect(AmsNetId.Local, 851);//default port of Twincat 3 is 851 //Twincat 2 is 801
        //    }


        ////##############################
        //public void HanbleName(string name)
        //{
        //    handlename = name;
        //    //MyAds.Connect(AmsNetId.Local, 851);//default port of Twincat 3 is 851 //Twincat 2 is 801
        //    handlevalue = MyAds.CreateVariableHandle(handlename); // int
        //}

        //##############################
        public Int32 Int32AdsRead(string HandleName)
        {
            Int32 valueToRead = 0;
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    valueToRead = (Int32)MyAds.ReadAny(handlevalue, typeof(Int32));
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }

            return valueToRead;
        }

        //##############################
        public void Int32AdsWrite(string HandleName, Int32 intvalue)
        {
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, intvalue);
                   // MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
        }

        //##############################
        public Int32[] Int32ArrayAdsRead(string HandleName, int ArryC)
        {
            Int32[] IntArr = new Int32[ArryC];
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    IntArr = (Int32[])MyAds.ReadAny(handlevalue, typeof(Int32[]), new int[] { ArryC });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
            return IntArr;
        }

        //##############################
        public void Int32ArrayAdsWrite(string HandleName, Int32[] value)
        {
            try
            {
                int ArryC_len = value.Length; //길이를 구하여 사용 
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, value, new int[] { ArryC_len });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
        }

        //##############################
        public Int32[,] Int32Array2X2AdsRead(string HandleName, int ArryC, int ArryB)
        {
            Int32[,] IntArr = new int[ArryC, ArryB];

            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    IntArr = (Int32[,])MyAds.ReadAny(handlevalue, typeof(Int32[,]), new int[] { ArryC, ArryB });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }

            return IntArr;
        }

        //##############################
        public void Int32Array2X2AdsWrite(string HandleName, Int32[,] value)
        {
            int ArryC_len = value.Length; //길이를 구하여 사용 
            uint handlevalue = dict[HandleName]; // uint
            //handlevalue = MyAds.CreateVariableHandle(HandleName);
            this.handlevalue = handlevalue;
            MyAds.WriteAny(handlevalue, value, new int[] { ArryC_len });
            //MyAds.DeleteVariableHandle(handlevalue);
        }


        //}


        /////////////////////////////////////////////////////////////////////////////////////
        //public class UInt32ADS
        //{
        //    //##############################
        //    public static AdsClient MyAds = new AdsClient();
        //    public static uint handlevalue = 0;
        //    public static string handlename;

        //    public UInt32ADS()  //클래스 명과 동일하게 선언하면 샌성자가 동작할때 동작함 
        //    {
        //        MyAds.Connect(AmsNetId.Local, 851);//default port of Twincat 3 is 851 //Twincat 2 is 801
        //    }



        ////##############################
        //public void HanbleName(string name)
        //{
        //    handlename = name;
        //    //MyAds.Connect(AmsNetId.Local, 851);//default port of Twincat 3 is 851 //Twincat 2 is 801
        //    handlevalue = MyAds.CreateVariableHandle(handlename); // int
        //}

        //##############################
        public UInt32 UInt32AdsRead(string HandleName)
        {
            UInt32 valueToRead = 0;
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    valueToRead = (UInt32)MyAds.ReadAny(handlevalue, typeof(UInt32));
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
            return valueToRead;
        }

        //##############################
        public void UInt32AdsWrite(string HandleName, UInt32 intvalue)
        {
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, intvalue);
                   // MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
        }

        //##############################
        public UInt32[] UInt32ArrayAdsRead(string HandleName, int ArryC)
        {
            UInt32[] UIntArr = new UInt32[ArryC];
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    UIntArr = (UInt32[])MyAds.ReadAny(handlevalue, typeof(UInt32[]), new int[] { ArryC });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
            return UIntArr;
        }

        //##############################
        public void UInt32ArrayAdsWrite(string HandleName, UInt32[] value)
        {
            try
            {
                int ArryC_len = value.Length; //길이를 구하여 사용 
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, value, new int[] { ArryC_len });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
        }

        //##############################
        public UInt32[,] UInt32Array2X2AdsRead(string HandleName, int ArryC, int ArryB)
        {
            UInt32[,] UIntArr = new UInt32[ArryC, ArryB];
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    UIntArr = (UInt32[,])MyAds.ReadAny(handlevalue, typeof(UInt32[,]), new int[] { ArryC, ArryB });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
            //##########
            return UIntArr;
        }

        //##############################
        public void UInt32Array2X2AdsWrite(string HandleName, UInt32[,] value)
        {
            try
            {
                int ArryC_len = value.Length; //길이를 구하여 사용 
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, value, new int[] { ArryC_len });
                   // MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
        }

        //}

        /////////////////////////////////////////////////////////////////////////////////////
        //public class RealADS
        //{
        //    //##############################
        //    public static AdsClient MyAds = new AdsClient();
        //    public static uint handlevalue = 0;
        //    public static string handlename;

        //    public RealADS()  //클래스 명과 동일하게 선언하면 샌성자가 동작할때 동작함 
        //    {
        //        MyAds.Connect(AmsNetId.Local, 851);//default port of Twincat 3 is 851 //Twincat 2 is 801
        //    }


        ////##############################
        //public void HanbleName(string name)
        //{
        //    handlename = name;
        //    //MyAds.Connect(AmsNetId.Local, 851);//default port of Twincat 3 is 851 //Twincat 2 is 801
        //    handlevalue = MyAds.CreateVariableHandle(handlename); // int
        //}

        //##############################
        public float RealAdsRead(string HandleName)
        {
            float valueToRead = 0.0F;
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    valueToRead = (float)MyAds.ReadAny(handlevalue, typeof(float));
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }

            return valueToRead;
        }

        //##############################
        public void RealAdsWrite(string HandleName, float intvalue)
        {
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, intvalue);
                   // MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
        }

        //##############################
        public float[] RealArrayAdsRead(string HandleName, int ArryC)
        {
            float[] IntArr = new float[ArryC];
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    IntArr = (float[])MyAds.ReadAny(handlevalue, typeof(float[]), new int[] { ArryC });
                   // MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
            //##########
            return IntArr;
        }

        //##############################
        public void RealArrayAdsWrite(string HandleName, float[] value)
        {
            try
            {
                int ArryC_len = value.Length; //길이를 구하여 사용 
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, value, new int[] { ArryC_len });
                   // MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
        }

        //##############################
        public float[,] RealArray2X2AdsRead(string HandleName, int ArryC, int ArryB)
        {
            float[,] IntArr = new float[ArryC, ArryB];
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    IntArr = (float[,])MyAds.ReadAny(handlevalue, typeof(float[,]), new int[] { ArryC, ArryB });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }

            //##########
            return IntArr;
        }

        //##############################
        public void RealArray2X2AdsWrite(string HandleName, float[,] value)
        {
            try
            {
                int ArryC_len = value.Length; //길이를 구하여 사용 
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, value, new int[] { ArryC_len });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
        }



        //}

        /////////////////////////////////////////////////////////////////////////////////////
        //public class LRealADS
        //{
        //    //##############################
        //    public static AdsClient MyAds = new AdsClient();
        //    public static uint handlevalue = 0;
        //    public static string handlename;


        //    public LRealADS()  //클래스 명과 동일하게 선언하면 샌성자가 동작할때 동작함 
        //    {
        //        MyAds.Connect(AmsNetId.Local, 851);//default port of Twincat 3 is 851 //Twincat 2 is 801
        //    }

        ////##############################
        //public void HanbleName(string name)
        //{
        //    handlename = name;
        //    //MyAds.Connect(AmsNetId.Local, 851);//default port of Twincat 3 is 851 //Twincat 2 is 801
        //    handlevalue = MyAds.CreateVariableHandle(handlename); // int
        //}

        //##############################
        public double LRealAdsRead(string HandleName)
        {
            double valueToRead = 0;
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    valueToRead = (double)MyAds.ReadAny(handlevalue, typeof(double));
                    //MyAds.DeleteVariableHandle(handlevalue);
                    return valueToRead;
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
            return valueToRead;
        }

        //##############################
        public void LRealAdsWrite(string HandleName, double intvalue)
        {
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, intvalue);
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
        }

        //##############################
        public double[] LRealArrayAdsRead(string HandleName, int ArryC)
        {
            double[] IntArr = new double[ArryC];
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    IntArr = (double[])MyAds.ReadAny(handlevalue, typeof(double[]), new int[] { ArryC });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
            //########## 
            return IntArr;
        }

        //##############################
        public void LRealArrayAdsWrite(string HandleName, double[] value)
        {
            try
            {
                int ArryC_len = value.Length; //길이를 구하여 사용 
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
               //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, value, new int[] { ArryC_len });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
        }

        //##############################
        public double[,] LRealArray2X2AdsRead(string HandleName, int ArryC, int ArryB)
        {
            double[,] IntArr = new double[ArryC, ArryB];
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    IntArr = (double[,])MyAds.ReadAny(handlevalue, typeof(double[,]), new int[] { ArryC, ArryB });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
            //##########
            return IntArr;
        }

        //##############################
        public void LRealArray2X2AdsWrite(string HandleName, double[,] value)
        {
            try
            {
                int ArryC_len = value.Length; //길이를 구하여 사용 
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, value, new int[] { ArryC_len });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
        }

        //}

        /////////////////////////////////////////////////////////////////////////////////////
        //public class StringADS
        //{
        //    //##############################
        //    public static AdsClient MyAds = new AdsClient();
        //    public static uint handlevalue = 0;
        //    public static string handlename;

        //    public StringADS()  //클래스 명과 동일하게 선언하면 샌성자가 동작할때 동작함 
        //    {
        //        MyAds.Connect(AmsNetId.Local, 851);//default port of Twincat 3 is 851 //Twincat 2 is 801
        //    }

        //    //##############################
        //    public void HanbleName(string name)
        //    {
        //        handlename = name;
        //        //MyAds.Connect(AmsNetId.Local, 851);//default port of Twincat 3 is 851 //Twincat 2 is 801
        //        handlevalue = MyAds.CreateVariableHandle(handlename); // int
        //    }


        //##############################
        public string StringAdsRead(string HandleName, int s_length)
        {
            string stringValue = null;
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    stringValue = (string)MyAds.ReadAny(handlevalue, typeof(string), new int[] { s_length }); // Needs additional para for strlen
                   // MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }

            return stringValue;
        }

        //##############################
        public void StringAdsWrite(string HandleName, string svalue)
        {
            try
            {
                int s_length = svalue.Length; //길이를 구하여 사용 
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, svalue, new int[] { s_length });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
        }



        //##############################
        public string[] StringArrayAdsRead(string HandleName, int ArryC, int svalue)
        {
            string[] stringArr = new string[ArryC];
            try
            {
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    stringArr = (string[])MyAds.ReadAny(handlevalue, typeof(string[]), new int[] { svalue, ArryC });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
            //##########
            return stringArr;
        }

        //##############################
        public void StringArrayAdsWrite(string HandleName, int ArryC, string[] svalue)
        {

            try
            {
                //전달된 배열에 Null이 있으면 에러가 발생하므로 우선 읽고서 상황에 따라 
                //가공하여 Null이 없도록 할것 
                int ArryC_len = svalue.Length; //길이를 구하여 사용 
                uint handlevalue = dict[HandleName]; // uint
                //handlevalue = MyAds.CreateVariableHandle(HandleName);
                //this.handlevalue = handlevalue;
                try
                {
                    MyAds.WriteAny(handlevalue, svalue, new int[] { ArryC, ArryC_len });
                    //MyAds.DeleteVariableHandle(handlevalue);
                }
                catch
                {
                    Console.WriteLine("-----  Twincat.asd접근이상 -----");
                    HanbleMakeError = true;
                }
            }
            catch
            {
                Console.WriteLine("-----  Dictionary접근이상 , Twincat.asd접근이상 -----");
                DictMakeError = true;
                HanbleMakeError = true;
            }
        }

        //#################  ST_WIZARD_PARAM Struct  ##################
        //#################  ST_WIZARD_PARAM Struct  ##################
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]  //정해진 길이를 명시할때 
        public struct Wizard_Struct
        {
            //#########
            [MarshalAs(UnmanagedType.I4)]
            public Int32 STEP_USE;// : DINT; //스텝 사용유무	
            public Int32 SYNC_AXIS;// : DINT; //동기축 번호(1:시작신호, 2:자동기, 3:상램, 4:상3, 5:상2, 6:상1, 7:피더, 8:배출, 9:다이, 10:하1, 11:하2, 12:하3, 13:하4, 14:코어)
            public Int32 SYNC_STEP;// : DINT; //동기축 스텝번호(동기축 번호가 자동기인경우: 자기 스텝번호, 아닌경우 동기축의 스텝번호)    
            public Int32 SYNC_FACTOR;// : DINT; //동기축 성분(1:거리, 2:압력, 3:kN, 4:시간) 

            //#########
            [MarshalAs(UnmanagedType.R4)]
            public float SYNC_RATIO;// : REAL; //동기스텝 진행률(진행률 타입에따른 거리값, +인경우 동기축 이동 시작시점에서 부터의 이동거리, -인경우 동기축 이동 종점에서 부터의 이동거리)  

            //#########
            [MarshalAs(UnmanagedType.I4)]
            public Int32 SYNC_RATIO_TYPE;// : DINT; //동기스텝 진행률 타입(-1:동기축이 시작신호, 자동기인경우, 0:+값 진행률, 1:+% 진행률, 2:-값 진행률, 3:-% 진행률)

            //#########
            [MarshalAs(UnmanagedType.R4)]
            public float SYNC_VALUE;// : REAL; //동기스텝 설정값   

            //#########
            [MarshalAs(UnmanagedType.I4)]
            public Int32 SYNC_IF;// : DINT; //동기스텝 비교인자 (0:자동계산, 1:<=, 2:>=, 3:<, 4:>)

            //#########
            [MarshalAs(UnmanagedType.R4)]
            public float POSITION_SV_VALUE;// : REAL; //설정위치 
            public float POSITION_VELOCITY;// : REAL; //위치속도 	
            public float PRESSURE_SV_VALUE;// : REAL; //설정압력
            public float PRESSURE_VELOCITY;// : REAL; //압력속도	
            public float STEP_DELAY_TIME;// : REAL; //딜레이타임 

            //#########
            [MarshalAs(UnmanagedType.I4)]
            public Int32 POSITION_ERROR_USE;// : DINT; //위치오차 판정 사용유무(0:미사용, 1:사용)

            //#########
            [MarshalAs(UnmanagedType.R4)]
            public float POSITION_ERROR_BAND;// : REAL; //위치오차 밴드값

            //#########
            [MarshalAs(UnmanagedType.I4)]
            public Int32 PRESSURE_ERROR_USE;// : DINT; //압력오차 판정 사용유무(0:미사용, 1:사용)

            //#########
            [MarshalAs(UnmanagedType.R4)]
            public float PRESSURE_ERROR_BAND;// : REAL;	//압력오차 밴드값

            //#########
            [MarshalAs(UnmanagedType.I4)]
            public Int32 PRESSURE_POSITION_CONTROL_MODE;// : DINT; //압력위치 제어 모드(0:위치제어, 1:압력제어)

            //#########
            [MarshalAs(UnmanagedType.R4)]
            public float PRESSURE_CONTROL_DISTANCE_LIMIT;// : REAL; //압력제어 모드 이동거리제한 리미트	

        }

        //#################  ST_SAMPLE_INFO Struct  ##################
        //#################  ST_SAMPLE_INFO Struct  ##################
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]  //정해진 길이를 명시할때 
        public struct SampleInfo_Struct
        {
            //#########
            [MarshalAs(UnmanagedType.I4)]
            public Int32 USE;// : DINT; //플레이트 사용 유무

            [MarshalAs(UnmanagedType.R4)]
            //#########
            public float FILLING_RATIO;// : REAL; //충진비 
            public float POWDER_DENSITY;// : REAL; //분말밀도
            public float DEPTH_HEIGHT;// : REAL; //단차	
            public float SAMPLE_HEIGHT;// : REAL; //제품높이 
            public float SAMPLE_AREA;// : REAL; //단면적
            public float SAMPLE_WIDTH;// : REAL; //제품너비
            public float TARGET_DENSITY;// : REAL; //목표밀도
            public float REAL_DENSITY;// : REAL; //실측밀도	

            public float TARGET_UPPER_DEPTH_HEIGHT;// : REAL; //상부 목표단차
            public float REAL_UPPER_DEPTH_HEIGHT;// : REAL; //상부 실측단차
            public float TARGET_LOWER_DEPTH_HEIGHT;// : REAL; //상부 목표단차
            public float REAL_LOWER_DEPTH_HEIGHT;// : REAL; //하부 실측단차

            //#########
            [MarshalAs(UnmanagedType.I4)]
            public Int32 UPPER_PUNCH_REF_ENABLE;// : DINT; //상기준펀치 선택(1), 미선택(0)  
            public Int32 UPPER_PUNCH_NO;// : DINT; //상펀치 번호(1~4)
            public Int32 LOWER_PUNCH_REF_ENABLE;// : DINT; //하기준펀치 선택(1), 미선택(0)
            public Int32 LOWER_PUNCH_NO;// : DINT; //하펀치 번호(1~4)
            public Int32 PUNCH_TYPE;// : DINT; //하펀치타입 (0:일반펀치, 1:스텝펀치, 2:코어펀치)

            [MarshalAs(UnmanagedType.R4)]
            //#########
            public float STEP_HEIGHT;// : REAL; //금형깊이
            public float CALIB_WEIGHT;// : REAL; //보정중량

        }
        //#######################
        public void DeleteHandvalue()
        {
            
            foreach(uint i in dict.Values)
            {                
                MyAds.DeleteVariableHandle(i);
            }      



        }


    }
}
        

