using System;
using System.Drawing;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Xml.Linq;
using TwinCAT;
using TwinCAT.Ads;
using TwinCAT.Ads.TypeSystem;
using TwinCAT.TypeSystem;


//########### ads를 만드는 시험과정에서 부분동작을 시험한 프로그램 
//########### nuget에서 TwinCAT.ADS 6.0.216를 받아 설치하고 시험 

namespace AdsClient_app
{ 

    public class Program
    {
        //#################
        public static AdsClient MyAds = new AdsClient();
        public static uint handleint =0;


        public static uint handleReal = 0;
        public static uint handleulint = 0;

        //#################  read  ##################
        //#################  read  ##################
        public static bool BoolAdsRead(String name)
        {
            handleint = MyAds.CreateVariableHandle(name); // int
            bool valueToRead = (bool)MyAds.ReadAny(handleint, typeof(bool));
            MyAds.DeleteVariableHandle(handleint);
            return valueToRead;
        }
        public static Int16 IntAdsRead(String name)
        {
            uint handleint = MyAds.CreateVariableHandle(name); // int
            Int16 valueToRead = (Int16)MyAds.ReadAny(handleint, typeof(Int16));
            MyAds.DeleteVariableHandle(handleint);
            return valueToRead;
        }
        public static UInt16 UintAdsRead(String name)
        {
            uint handuint = MyAds.CreateVariableHandle(name); // int
            UInt16 valueToRead = (UInt16)MyAds.ReadAny(handuint, typeof(UInt16));
            MyAds.DeleteVariableHandle(handuint);
            return valueToRead;
        }
        public static Int32 DintAdsRead(String name)
        {
            uint handleint = MyAds.CreateVariableHandle(name); // dint
            Int32 valueToRead = (Int32)MyAds.ReadAny(handleint, typeof(Int32));
            MyAds.DeleteVariableHandle(handleint);
            return valueToRead;
        }
        public static UInt32 UdintAdsRead(String name)
        {
            uint handuint = MyAds.CreateVariableHandle(name); // uint
            UInt32 valueToRead = (UInt32)MyAds.ReadAny(handuint, typeof(UInt32));
            MyAds.DeleteVariableHandle(handuint);
            return valueToRead;
        }
        public static Int64 LintAdsRead(String name)
        {
            uint handleint = MyAds.CreateVariableHandle(name); // lint
            Int64 valueToRead = (Int64)MyAds.ReadAny(handleint, typeof(Int64));
            MyAds.DeleteVariableHandle(handleint);
            return valueToRead;
        }
        public static UInt64 UlintAdsRead(String name)
        {
            handleulint = MyAds.CreateVariableHandle(name); // ulint
            UInt64 valueToRead = (UInt64)MyAds.ReadAny(handleulint, typeof(UInt64));
            MyAds.DeleteVariableHandle(handleulint);
            return valueToRead;
        }

        public static float RealAdsRead(String name)
        {
            handleReal = MyAds.CreateVariableHandle(name); // REAL
            float realValue = (float)MyAds.ReadAny(handleReal, typeof(float));
            MyAds.DeleteVariableHandle(handleReal);
            return realValue;
        }

        public static Int16[] IntArrayAdsRead (String name)
        {
            //##########
            uint handleURUS = MyAds.CreateVariableHandle(name); // ARRAY[0..10] OF INT;
            Int16[] IntArr = (Int16[])MyAds.ReadAny(handleURUS, typeof(Int16[]), new int[] { 11 });
            MyAds.WriteAny(handleURUS, IntArr, new int[] { 11 });
            MyAds.DeleteVariableHandle(handleURUS);
            return IntArr;
        }





        //#################  write  ##################
        //#################  write  ##################
        public static void BoolAdsWrite(String name, bool boolvalue)
        {
            handleint = MyAds.CreateVariableHandle(name); // Bool
            MyAds.WriteAny(handleint, boolvalue);
            MyAds.DeleteVariableHandle(handleint);
        }
        public static void IntAdsWrite(String name, Int16 intvalue)
        {            
            uint handleint = MyAds.CreateVariableHandle(name); // int
            MyAds.WriteAny(handleint, intvalue);
            MyAds.DeleteVariableHandle(handleint);
        }
        public static void UintAdsWrite(String name, UInt16 uintvalue)
        {            
            uint handleuint = MyAds.CreateVariableHandle(name); // uint
            MyAds.WriteAny(handleuint, uintvalue);
            MyAds.DeleteVariableHandle(handleuint);
        }
        public static void DintAdsWrite(String name, Int32 dintvalue)
        { 
            uint handleint = MyAds.CreateVariableHandle(name); // dint
            MyAds.WriteAny(handleint, dintvalue);
            MyAds.DeleteVariableHandle(handleint);
        }
        public static void UdintAdsWrite(String name, UInt32 udintvalue)
        {
            uint handleuint = MyAds.CreateVariableHandle(name); // udint
            MyAds.WriteAny(handleuint, udintvalue);
            MyAds.DeleteVariableHandle(handleuint);
        }
        public static void LintAdsWrite(String name, Int64 lintvalue)
        {
            uint handleint = MyAds.CreateVariableHandle(name); // lint
            MyAds.WriteAny(handleint, lintvalue);
            MyAds.DeleteVariableHandle(handleint);
        }
        public static void UlintAdsWrite(String name, UInt64 ulintvalue)
        {
            //var now_1 = DateTime.Now;
            handleulint = MyAds.CreateVariableHandle(name); // ulint
            //var now_2 = DateTime.Now;
            MyAds.WriteAny(handleulint, ulintvalue);
            //var now_3 = DateTime.Now;
            MyAds.DeleteVariableHandle(handleulint);
            //var now_4 = DateTime.Now;
           // Console.WriteLine($"Uint64   = 핸들가져오기:{now_2 - now_1}, 쓰기:{now_3 - now_2}, 핸들해제:{now_4 - now_3}");

        }
        public static void RealAdsWrite(String name, float realValue)
        {
  
             //var now_1 = DateTime.Now;
             handleReal = MyAds.CreateVariableHandle("MAIN.real1"); // REAL
             //var now_2 = DateTime.Now;
             MyAds.WriteAny(handleReal, realValue);
             //var now_3 = DateTime.Now;
             MyAds.DeleteVariableHandle(handleReal);
             //var now_4 = DateTime.Now;
            // Console.WriteLine($"REAL   = 핸들가져오기:{now_2 - now_1}, 쓰기:{now_3 - now_2}, 핸들해제:{now_4 - now_3}");
    

        }

        //#################  StepStruct  ##################
        //#################  StepStruct  ##################
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]  //정해진 길이를 명시할때 
        public struct Step_Struct
        {
            [MarshalAs(UnmanagedType.I4)]
            public Int32 STEP_TOTAL;          
            public Int32 RUN_STEP ;   
            public Int32 NEXT_STEP ;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public Int32[] STEP_FLOW;

            [MarshalAs(UnmanagedType.I4)]
            public Int32 RUN_STEP_COUNT;
            public Int32 LAST_RUN_STEP;
        }


        //#################  StepStruct  ##################
        //#################  StepStruct  ##################
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]  //정해진 길이를 명시할때 
        public struct Compressing_line_Struct
        {
            [MarshalAs(UnmanagedType.R4)]
            //#########
            public float STEP_USE;

            //#########
            public float SYNC_AXIS;
            public float SYNC_STEP;
            public float SYNC_FACTOR;
            public float SYNC_RATIO;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 SYNC_RATIO_TYPE;
            [MarshalAs(UnmanagedType.R4)]
            public float SYNC_VALUE;
            public float SYNC_IF;

            //#########
            [MarshalAs(UnmanagedType.I4)]
            public Int32 POSITION_CONTROL_ENABLE;
            [MarshalAs(UnmanagedType.R4)]
            public float POSITION_SV_VALUE;
            public float POSITION_VELOCITY_VALUE;
            public float POSITION_SYNC_RATIO;

            //#########
            [MarshalAs(UnmanagedType.I4)]
            public Int32 PRESSURE_CONTROL_ENABLE;
            [MarshalAs(UnmanagedType.R4)]
            public float PRESSURE_SV_VALUE;

            //#########
            public float STEP_DELAY_TIME;

            //#########
            public float POSITION_ERROR_USE;
            public float POSITION_ERROR_BAND;

            //#########
            public float PRESSURE_ERROR_USE;
            public float PRESSURE_ERROR_BAND;

            //#########
            public float TIME_DELAY;

            //#########
            public float PRESSURE_VELOCITY;
            public float PRESSURE_POSITION_CONTROL_MODE;
            public float PRESSURE_CONTROL_DISTANCE_LIMIT;
        }



        //#################  Connect  ##################
        //#################  Connect  ##################
        public static void ReadAdsConnect()
        {
            MyAds.Connect(AmsNetId.Local, 851);//default port of Twincat 3 is 851 //Twincat 2 is 801
        }



        //#################  ReadWrite  ##################
        //#################  ReadWrite  ##################
        public static void ReadWrite()
        {

            handleint = MyAds.CreateVariableHandle("MAIN.test_int_value"); // int
            handleReal = MyAds.CreateVariableHandle("MAIN.real1"); // REAL
            handleulint = MyAds.CreateVariableHandle("MAIN.TEST_ULINT"); // ulint

            //##########
            var now_98 = DateTime.Now;
            BoolAdsWrite("MAIN.TEST_BOOL", true);
            var now_99 = DateTime.Now;
            bool b = BoolAdsRead("MAIN.TEST_BOOL");

            var now_100 = DateTime.Now;

            //##########
            var now_2 = DateTime.Now;
            RealAdsWrite("MAIN.real1", 12345.2F);


            var now_3 = DateTime.Now;
            float r = RealAdsRead("MAIN.real1");


            //##########
            var now_4 = DateTime.Now;
            IntAdsWrite("MAIN.test_int_value", -32_768);
            var now_5 = DateTime.Now;
            int i16 = IntAdsRead("MAIN.test_int_value");

            //##########
            var now_6 = DateTime.Now;
            UintAdsWrite("MAIN.test_uint_value", 65_535);
            var now_7 = DateTime.Now;
            uint ui16 = UintAdsRead("MAIN.test_uint_value");


            //##########
            var now_8 = DateTime.Now;
            DintAdsWrite("MAIN.TEST_DINT", -2_147_483_648);
            var now_9 = DateTime.Now;
            Int32 i32 = DintAdsRead("MAIN.TEST_DINT");

            //##########
            var now_10 = DateTime.Now;
            UdintAdsWrite("MAIN.TEST_UDINT", 4294967295);
            var now_11 = DateTime.Now;
            UInt32 ui32 = UdintAdsRead("MAIN.TEST_UDINT");

            //##########
            var now_12 = DateTime.Now;
            LintAdsWrite("MAIN.TEST_LINT", -9_223_372_036_854_775_808);
            var now_13 = DateTime.Now;
            Int64 i64 = LintAdsRead("MAIN.TEST_LINT");

            //##########
            var now_14 = DateTime.Now;
            UlintAdsWrite("MAIN.TEST_ULINT", 1844674403709551613);
            var now_15 = DateTime.Now;
            UInt64 ui64 = UlintAdsRead("MAIN.TEST_ULINT");


            Int16[] intarray = IntArrayAdsRead("GVL_USE_STEP.UPPER_RAM_USE_STEP");
            int ita = 0;
            foreach (var i in intarray)
            {
                Console.WriteLine("intarray {0} => {1}", ita, i);
                ita += 1;
            }





            //##########
            var now_16 = DateTime.Now;
            uint handleStruct_URS = MyAds.CreateVariableHandle("GVL_RAM_STEP.UPPER_RAM_STEP");
            Step_Struct structValue = (Step_Struct)MyAds.ReadAny(handleStruct_URS, typeof(Step_Struct));
            // MyAds.WriteAny(handleStruct_URS, structValue);
            MyAds.DeleteVariableHandle(handleStruct_URS);
            

            //##########
            var now_17 = DateTime.Now;
            uint handleStructURPL = MyAds.CreateVariableHandle("GVL_COMPRESSING.UPPER_RAM_PRESS_LINE_C4_TO_BF[1]");
            //#### 읽기
            Compressing_line_Struct structValueURPL = (Compressing_line_Struct)MyAds.ReadAny(handleStructURPL, typeof(Compressing_line_Struct));
            //#### 쓰기 
            structValueURPL.SYNC_AXIS = 32.1F;

            MyAds.WriteAny(handleStructURPL, structValueURPL);
            MyAds.DeleteVariableHandle(handleStructURPL);
            var now_18 = DateTime.Now;








                        
            //##########
            Console.WriteLine("STEP_TOTAL = " + structValue.STEP_TOTAL.ToString());
            Console.WriteLine("RUN_STEP = " + structValue.RUN_STEP.ToString());
            Console.WriteLine("NEXT_STEP = " + structValue.NEXT_STEP.ToString());
            int it = 0;
            foreach(var i in structValue.STEP_FLOW)
            {
                Console.WriteLine("STEP_FLOW {0} => {1}", it,i);
                it += 1;
            }
            Console.WriteLine("RUN_STEP_COUNT = " + structValue.RUN_STEP_COUNT.ToString());
            Console.WriteLine("LAST_RUN_STEP = " + structValue.LAST_RUN_STEP.ToString());

            Console.WriteLine("#############################################");
            Console.WriteLine("STEP_TOTAL = " + structValueURPL.STEP_USE.ToString());
            Console.WriteLine("STEP_TOTAL = " + structValueURPL.SYNC_AXIS.ToString());
            Console.WriteLine("STEP_TOTAL = " + structValueURPL.SYNC_STEP.ToString());
            Console.WriteLine("STEP_TOTAL = " + structValueURPL.SYNC_FACTOR.ToString());





            //##########
            Console.WriteLine($"real = write:{now_3 - now_2}, read{now_4 - now_3} => " + r.ToString());
            Console.WriteLine($"int16  = write:{now_5 - now_4}, read{now_6 - now_5} => " + i16.ToString());
            Console.WriteLine($"uint16 = write:{now_7 - now_6}, read{now_8 - now_7} => " + ui16.ToString());
            Console.WriteLine($"dint32  = write:{now_9 - now_8}, read{now_10 - now_9} => " + i32.ToString());
            Console.WriteLine($"udint32 = write:{now_11 - now_10}, read{now_12 - now_11} => " + ui32.ToString());
            Console.WriteLine($"lint64  = write:{now_13 - now_12}, read{now_14 - now_13} => " + i64.ToString());
            Console.WriteLine($"ulint64 = write:{now_15 - now_14}, read{now_16 - now_15} => " + ui64.ToString());
            Console.WriteLine($"bool = write:{now_99 - now_98}, read{now_100 - now_99} => " + b.ToString());
            Console.WriteLine($"struct = dint16:{now_17 - now_16}, dint3_real20{now_18 - now_17} ");



        }


        //#################  Main  ##################
        //#################  Main  ##################
        static void Main()
        {
            
            var now_1 = DateTime.Now;
            ReadAdsConnect();
            do
            {
                //연결을 기다려 주면 핸들값을 바로 가져오고 
                //연결을 기다려 주지않으면 연결후(연결까지 시간이 흐름) 핸들을 가져온다. -> error로 처리하지 않고 좌측처럼한다.
            } while (MyAds.IsConnected==false);

            Console.WriteLine("#############################################");
            ReadWrite();
            Console.WriteLine("#############################################");
            ReadWrite();

        }
    }

}