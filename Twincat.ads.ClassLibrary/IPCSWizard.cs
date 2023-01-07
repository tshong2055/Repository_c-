using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;
using TwinCAT.Ads.TypeSystem;
using static System.Formats.Asn1.AsnWriter;

namespace TwincatAds.ClassLibrary
{

    public  enum AxisName
    {
        UpOutter = 1,
        UpMiddle,
        UpInner,
        UpInnerMost,
        UpCenterCore,

        LpOutter,
        LpMiddle,
        LpInner,
        LpInnerMost,
        LpCenterCore,
        Die
    }


    public class IPCSWizard
    {

        //###### 상펀치 분말이동 정렬위치 sort거리 
        public float[] UpperPunchSort = { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F };


        //####### 분말이동시의 펀치위치 PowderTransfer
        public float[] LpPowderTransfer = { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F };
        public float[] UpPowderTransfer = { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F };

        //####### 피더충진 펀치의 위치
        public float[] Lowerpunchfillingmm = { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F };
        public int NonFillingLpAxis = 0;    //하펀치 중 충진량이 가장 많아서 충진을 하지 않는 펀치의 배열번호 
        public int NonFillingLP_UpAxis = 0;  //NonFillingLpAxis 위에 있는 축의 위치

        public float DieFillingmm = 0;  //다이충진값 

        //####### 충진량 (제품높이*충진비)
        private float[] ProductSectionfilling = { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F };

        public float ProductOutSectionfilling = 0.0F;
        public float ProductMiddleSectionfilling = 0.0F;
        public float ProductInnerSectionfilling = 0.0F;
        public float ProductCoreSectionfilling = 0.0F;
        public float ProductSideCoreSectionfilling = 0.0F;
        public float ProductDieSectionfilling = 0.0F;

        //######## 충진비율 
        private float[] ProductSectionfillingratio = { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F };
        public float ProductOutSectionfillingratio = 0.0F;
        public float ProductMiddleSectionfillingratio = 0.0F;
        public float ProductInnerSectionfillingratio = 0.0F;
        public float ProductCoreSectionfillingratio = 0.0F;
        public float ProductSideCoreSectionfillingratio = 0.0F;
        public float ProductDieSectionfillingratio = 0.0F;

        //######## 제품높이 
        private float[] ProductSectionHigh = { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F };
        public float ProductOutSectionHigh = 0.0F;
        public float ProductMiddleSectionHigh = 0.0F;
        public float ProductInnerSectionHigh = 0.0F;
        public float ProductCoreSectionHigh = 0.0F;
        public float ProductSideCoreSectionHigh = 0.0F;
        public float ProductDieSectionHigh = 0.0F;


        //######## 상 제품단차 
        public float[] ProductUpStepHigh = { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F };
        public float ProductUpOutStepHigh = 0.0F;
        public float ProductUpMiddleStepHigh = 0.0F;
        public float ProductUpInnerStepHigh = 0.0F;

        public float ProductUpCoreStepHigh = 0.0F;
        public float ProductUpSideCoreStepHigh = 0.0F;
        public float ProductUpDieStepHigh = 0.0F;

        //######## 하제품단차 
        private float[] ProductStepHigh = { 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F };
        public float ProductOutStepHigh = 0.0F;
        public float ProductMiddleStepHigh = 0.0F;
        public float ProductInnerStepHigh = 0.0F;
        public float ProductCoreStepHigh = 0.0F;
        public float ProductSideCoreStepHigh = 0.0F;
        public float ProductDieStepHigh = 0.0F;


        //##### 
        private int ProductStandardUpPunch = 0;
        private string ProductStandardUpPunchname = "-";

        private int ProductStandardLowPunch = 0;
        private string ProductStandardLowPunchname = "-";

        //##### 상부펀치의 최외부 사용축
        //##### 상부펀치의 최외부 펀치 
        private int Up_OutAxisNo = 0;
        private int Up_OutPunchNo = 0;

        ///##### 하부펀치의 최외각 사용축
        ///##### 하부펀치의 최외각 펀치 
        private int Lp_OutAxisNo = 0;
        private int Lp_OutPunchNo = 0;


        //##### 상부펀치의 최내부 사용축
        //##### 상부펀치의 최내부 펀치 
        private int Up_InAxisNo = 0;
        private int Up_InPunchNo = 0;

        //##### 하부펀치의 최내부 사용축
        //##### 하부펀치의 최내부 펀치 
        private int Lp_InAxisNo = 0;
        private int Lp_InPunchNo = 0;

        /// <summary>
        /// 사용펀치 순서대로 정렬 0부터시작 
        /// </summary>
        private int[] UPunchUseNo = new int[6];


        /// <summary>
        /// 사용펀치 순서대로 정렬 0부터시작 
        /// </summary>
        private int[] LPunchUseNo = new int[7];

        private string[] UpStruct = { "-", "-", "-", "-", "-","-"};
        private string[] LowStruct =   { "-", "-", "-", "-", "-","-" };


        //###### step punc
        public float[,] PunchType = new float[6, 2]; //step punch 설정 
        public int[] UPunchUse = new int[6]; //상부 사용펀치 punch 설정 
        public int[] PunchUse = new int[7]; //하부 사용펀치 punch 설정 



        public int[,] LPunchPosition = new int[5, 2]; //x:Axis y:좌측,우측 펀치번호 
        public int[,] UPunchPosition = new int[5, 2]; //x:Axis y:좌측,우측 펀치번호 


        //##################################################### 
        //#####################################################
        public void UsePunchPosition()
        {
            //##########################
            int Lp_outpunch = 0; //하부의 최외각 펀치 
            int Up_outpunch = 0; //상부의 최외각 펀치 
            int Up_outpunch_Axis = 0; //상부의 최외각 펀치의 위치 
            int Lp_outpunch_Axis = 0; //하부의 최외각 펀치의 위치 

            int Up_inpunch = 0; //상부의 최내각 펀치 
            int Lp_inpunch = 0; //하부의 최내각 펀치 
            int Up_inpunch_Axis = 0; //상부의 최내각 펀치의 위치  
            int Lp_inpunch_Axis = 0; //하부의 최내각 의 위치  



        int a1 = 0;
        int a2 = 0;
            foreach (var i in UPunchUse)  //상펀치 사용축을 0번부터 기록 
            {
                
                if (UPunchUse[a1] != 0)
                {                    
                    UPunchUseNo[a2] = UPunchUse[a1];
                    a2 += 1;
                }
                a1 += 1;
            }

        int b1 = 0;
        int b2 = 0;
            foreach (var i in PunchUse) //하펀치 사용축을 0번부터 기록 
            {
                if (PunchUse[b1] != 0)
                {
                    LPunchUseNo[b2] = PunchUse[b1];
                    b2 += 1;
                }

                b1 += 1;
            }

            int x = 1;

        }


        //#####################################################
        public void UpPunchUse(Int32 LOutter, Int32 LMiddle, Int32 LInner, Int32 LInnerMost, Int32 LCenterCore)
        {
            UPunchUse[0] = 0;
            UPunchUse[1] = LOutter;
            UPunchUse[2] = LMiddle;
            UPunchUse[3] = LInner;
            UPunchUse[4] = LInnerMost;
            UPunchUse[5] = LCenterCore;
        }

        //#####################################################
        public void LpPunchUse(Int32 LOutter, Int32 LMiddle, Int32 LInner , Int32 LInnerMost, Int32 LCenterCore, Int32 LDie)
        {

            PunchUse[0] = 0;
            PunchUse[1] = LOutter; 
            PunchUse[2] = LMiddle;
            PunchUse[3] = LInner;
            PunchUse[4] = LInnerMost;
            PunchUse[5] = LCenterCore;
            PunchUse[6] = LDie;
        }

        //#####################################################
        public void Lp1PunchType(Int32 Punch_Type, float StepHight)
        {
            PunchType[0, 0] = (float)Punch_Type;
            PunchType[0, 1] = StepHight;
        }

        public void Lp2PunchType(Int32 Punch_Type, float StepHight)
        {
            PunchType[1, 0] = (float)Punch_Type;
            PunchType[1, 1] = StepHight;
        }
        public void Lp3PunchType(Int32 Punch_Type, float StepHight)
        {
            PunchType[2, 0] = (float)Punch_Type;
            PunchType[2, 1] = StepHight;
        }
        public void Lp4PunchType(Int32 Punch_Type, float StepHight)
        {
            PunchType[3, 0] = (float)Punch_Type;
            PunchType[3, 1] = StepHight;
        }
        public void CorePunchType (Int32 Punch_Type, float StepHight )
        {
            PunchType[4, 0] = (float)Punch_Type;
            PunchType[4, 1] = StepHight;
        }
        public void DiePunchType(Int32 Punch_Type, float StepHight)
        {
            PunchType[5, 0] = (float)Punch_Type;
            PunchType[5, 1] = StepHight;
        }

        //#####################################################
        public void PowerTransfer()
        {

            ///////////////////////////////////////////// 하펀치 ///////////////////////////////            
            //############
            float[] NonFillingStepHeigh = new float[ProductSectionHigh.Length];  //충진이 가장많은 펀치에서의 완제품단차 (스스로는 0이됨)
            int a = 0;
            foreach(var y in NonFillingStepHeigh)
            {
                NonFillingStepHeigh[a] = ProductStepHigh[a] - ProductStepHigh[NonFillingLpAxis];
                a += 1;
            }


            //############
            float[] PriductCenter = new float[ProductSectionHigh.Length]; //충진이 없는 펀치에서의 부분형상의 중앙값 위치
            int a1 = 0;
            foreach (var y in PriductCenter)
            {
                PriductCenter[a1] = ((ProductSectionHigh[a1] / 2) + NonFillingStepHeigh[a1]);
                a1 += 1;
            }

            //############
            float[] PriductCenterDeviation = new float[ProductSectionHigh.Length]; //a1 - 충진이 없는 펀치에서의 중앙값 = 완제품중앙값의 엇갈린 높이 
            int a2 = 0; 
            foreach (var y in PriductCenterDeviation)
            {
                if (PriductCenter[a2] != 0 )
                {
                    PriductCenterDeviation[a2] =  PriductCenter[a2] - (ProductSectionHigh[NonFillingLpAxis]/2);
                }
                else
                {
                    PriductCenterDeviation[a2] = 0;
                }
                a2 += 1;
            }

            //############
            float[] PriductCenterDeviationratio = new float[ProductSectionHigh.Length]; //완제품의 중앙값 분말이동 엇갈린비 
            int a3 = 0;
            foreach (var y in PriductCenterDeviation)
            {
                PriductCenterDeviationratio[a3] = PriductCenterDeviation[a3] * ProductSectionfillingratio[a3];
                a3 += 1;
            }

            //############
            float[] FillingtransferCenter = new float[ProductSectionHigh.Length]; //분말이동을 하지 않는 펀치를 기준으로하는 각 분말이동의 중앙값 
            int a4 = 0;
            foreach (var y in FillingtransferCenter)
            {
                if (a4 == NonFillingLpAxis)
                {
                    FillingtransferCenter[a4] = ProductSectionfilling[NonFillingLpAxis] / 2;
                }
                else
                {
                    FillingtransferCenter[a4] = PriductCenterDeviationratio[a4] + ProductSectionfilling[NonFillingLpAxis] / 2;
                }
                a4 += 1;
            }
            FillingtransferCenter[0]=0;  //배열0은 사용하지 않음 
            FillingtransferCenter[6]=0;  //다이는 중앙을 따지지 않음 
            //############
            float[] Fillingtransfer = new float[ProductSectionHigh.Length]; //충진이 없는 펀치에서의 분말이동 시 하펀치위치 
            int a5 = 0;
            foreach (var y in FillingtransferCenter)
            {

                if (PriductCenterDeviation[NonFillingLpAxis] <= PriductCenterDeviation[a5])
                {
                    Fillingtransfer[a5] = parsedot(FillingtransferCenter[a5] - NonFillingStepHeigh[a5] - (ProductSectionfilling[a5] / 2));
                }
                else
                {
                    Fillingtransfer[a5] = parsedot(FillingtransferCenter[a5] + NonFillingStepHeigh[a5] + (ProductSectionfilling[a5] /2));

                }



                a5 += 1;
            }


            //##### 사용유무에 따른 데이터 임의 수정 
            if (PunchUse[6] == 0)  //다이 
            {
                Fillingtransfer[6] = DieFillingmm;
            }
            if (PunchUse[5] == 0)  //코어 
            {
                Fillingtransfer[5] = 0;
            }

            //이 위치에 프로그램은 단차가 있는지 확인하여
           // LpPunchUse[]


            Fillingtransfer.CopyTo(LpPowderTransfer, 0); //배열복사 


            //////////////////////////////////////////// 분말이동 //////////////////////////////
            PowderTransferNew();



            ///////////////////////////////////////////// 상펀치 ///////////////////////////////

            float[] array_3 = new float[ProductSectionHigh.Length];  //충진이 없는 펀치를 기준으로하는 상펀치의 단차값  (하단차가 +, 상단차가 -)
            int d = 0;
            foreach (var y in array_3)
            {
                array_3[d] = ProductUpStepHigh[d] - ProductUpStepHigh[NonFillingLpAxis];
                d += 1;
            }

            float[] array_4 = new float[ProductSectionHigh.Length];  //충진이 없는 펀치를 기준으로 한 분말이동 상대높이 
            int e = 0;
            foreach (var y in array_4)
            {
                array_4[e] = Lowerpunchfillingmm[e] - LpPowderTransfer[e];
                e += 1;
            }

            float[] array_5 = new float[ProductSectionHigh.Length]; //충진이 없는 펀치를 기준으로 한 분말이동 상대높에서 단차를 고려한 값 
            int f = 0;
            foreach (var y in array_5)
            {
                array_5[f] = parsedot(array_4[f] - array_3[f]);
                f += 1;
            }

            float NonFillingUpSortMax = array_4.Max();
            int e1 = 0;
            foreach (var y in array_4)
            {
                UpperPunchSort[e1] = array_5[e1];  //상단차에서 분말이동을 위하여 정렬하는 경우에 상펀치 각각의 이동값 
                e1 += 1;
            }
            UpperPunchSort[NonFillingLpAxis] = NonFillingUpSortMax;

            //##### 사용유무에 따른 데이터 임의 수정 

            if (PunchUse[5] == 0) //코어 
            {
                array_5[5] = 0;
                UpperPunchSort[5] = 0;
            }

            if (PunchUse[6] == 0) //다이
            {
                array_5[6] = 0;
                UpperPunchSort[6] = 0;
            }




            array_5.CopyTo(UpPowderTransfer, 0); //배열복사 


 

        }

        //####################################################
        public void PowderTransferNew()
        {

            //####### 상펀치의 동일축과 그 수를 카운트 
            int[,] array_6 = new int[UPunchUse.Length, UPunchUse.Length];  //비교기준축위치,동일축위치 =>  축의 번호 계산 
            int c3 = 0;
            int u1 = 0;
            int u2 = 0;
            int u3 = 0;
            int u4= 0;
            int u5 = 0;
            for (int c1 = 0; c1 < UPunchUse.Length - 2; c1++)
            {
                c3 = 0;
                for (int c2 = c1 + 1; c2 < UPunchUse.Length - 1; c2++)
                {
                    if (UPunchUse[c1] == UPunchUse[c2] && UPunchUse[c2] != 0)
                    {
                        array_6[c1, c1] = UPunchUse[c2];
                        switch (UPunchUse[c1])
                        {
                            case 1:
                                if (u1 != 1)
                                {                                    
                                    array_6[c1, c2] = UPunchUse[c2];
                                }
                                break;
                            case 2:
                                if (u2 != 1)
                                {
                                    array_6[c1, c2] = UPunchUse[c2];
                                }
                                break;
                            case 3:
                                if (u3 != 1)
                                {
                                    array_6[c1, c2] = UPunchUse[c2];
                                }
                                break;
                            case 4:
                                if (u4 != 1)
                                {
                                    array_6[c1, c2] = UPunchUse[c2];
                                }
                                break;
                            case 5:
                                if (u5 != 1)
                                {
                                    array_6[c1, c2] = UPunchUse[c2];
                                }
                                break;
                        }
                    }
                }

                switch (UPunchUse[c1])
                {
                    case 1:
                        u1 = 1;
                        break;
                    case 2:
                        u2 = 1;
                        break;
                    case 3:
                        u3 = 1;
                        break;
                    case 4:
                        u4 = 1;
                        break;
                    case 5:
                        u5 = 1;
                        break;
                }  
            }

            //####### 우선순위 1 -> 분말이동이 없는 축과 붙어있는(가까운) 축 고정, 나머지 축 이동 
            //####### 우선순위 2 -> 분말충진량이 많은 축 고정 . 나머지 축 이동 
            int[] rankings =  Enumerable.Repeat(0, UPunchUse.Length).ToArray(); //모두 1로 초기화
            int[,] axis_counter = new int[UPunchUse.Length, UPunchUse.Length];
            int[] array_7 = new int[UPunchUse.Length];
            int[,] array_7rankings = new int[UPunchUse.Length, UPunchUse.Length];
            for (int d1 =0; d1<UPunchUse.Length-1; d1++)
            {                
                for (int d2 = 0; d2 < UPunchUse.Length - 1; d2++)
                {
                    if (array_6[d1, d2] != 0)
                    {
                        axis_counter[d1, d2] = Math.Abs(d2 - NonFillingLP_UpAxis);
                    }
                }
            }

            //#################################
            int[] array_8 = new int[UPunchUse.Length];
            int[] UpAxisNorank1 = new int[UPunchUse.Length];
            int[] UpAxisNorank2 = new int[UPunchUse.Length];
            int[] UpAxisNorank3 = new int[UPunchUse.Length];
            int[] UpAxisNorank4 = new int[UPunchUse.Length];
            int[] UpAxisNorank5 = new int[UPunchUse.Length];
            for (int d1 = 0; d1 < UPunchUse.Length - 1; d1++)        
            {

                for (int d2 = 0; d2 < UPunchUse.Length - 1; d2++)
                {
                    array_8[d2] = axis_counter[d1, d2];
                }
                switch (d1)
                {
                    case 1:
                        UpAxisNorank1 = Punchrank(array_8);   
                        break;
                    case 2:
                        UpAxisNorank2 = Punchrank(array_8);
                        break;
                    case 3:
                        UpAxisNorank3 = Punchrank(array_8);
                        break;
                    case 4:
                        UpAxisNorank4 = Punchrank(array_8);
                        break;
                    case 5:
                        UpAxisNorank5 = Punchrank(array_8);
                        break;
                } 
            }               
                                                                     

        }

        //#####################################################
        public int[] Punchrank(int[] rank)
        {

            //####### 우선순위 1 -> 분말이동이 없는 축과 붙어있는(가까운) 축 고정, 나머지 축 이동 
            //####### 우선순위 2 -> 분말충진량이 많은 축 고정 . 나머지 축 이동 
            int[] rankings = Enumerable.Repeat(0, rank.Length).ToArray(); //모두 1로 초기화
            for (int i = 0; i < rank.Length; i++)
            {

                rankings[i] = 0;

                for (int j = 0; j < rank.Length; j++)
                {
                    if (rank[i] < rank[j] ) //현재(i)와 나머지(j) 비교
                    {
                        rankings[i]++;         //RANK: 나보다 큰 점수가 나오면 순위 1증가
                    }
                }
            }
            int max = rankings.Max();

            for (int i = 0; i < rank.Length; i++)
            {
                rankings[i] = max - rankings[i];
            }

                return rankings;
        }



        //#####################################################
        public void UpperPunchStepHigh()
        {
            float upUpStandardPunch = 0;

            float[] UpStandard = new float[6];
            for (int x =0; x<5 ; x++)
            {
                UpStandard[x] = ProductSectionHigh[x] + ProductStepHigh[x];
            }
            upUpStandardPunch = UpStandard[ProductStandardUpPunch];

            for (int y = 1; y < 6; y++)
            {
                ProductUpStepHigh[y] =  upUpStandardPunch - UpStandard[y];  //위에 있으면 -mm임 가압방향이 +mm
                switch (y)
                {
                    case 1:
                        ProductOutStepHigh = ProductUpStepHigh[y];
                        break;
                    case 2:
                        ProductMiddleStepHigh = ProductUpStepHigh[y];
                        break;
                    case 3:
                        ProductInnerStepHigh = ProductUpStepHigh[y];
                        break;
                    case 4:
                        ProductCoreStepHigh = ProductUpStepHigh[y];
                        break;
                    case 5:
                        ProductSideCoreStepHigh = ProductUpStepHigh[y];
                        break;
                    case 6:
                        ProductDieStepHigh = ProductUpStepHigh[y];
                        break;
                }
            }     

        }


        //#####################################################
        public void LowerPunchFilling()
        {
            float[] filling = new float[ProductSectionfilling.Length];
            float fillingMax = ProductSectionfilling.Max();
            float fillingMin = ProductSectionfilling.Min();
            int fillingMaxindex = Array.IndexOf(ProductSectionfilling,fillingMax);
            NonFillingLpAxis = fillingMaxindex;                //충진이 가장많은 펀치 
            NonFillingLP_UpAxis = NonFillingLpAxis; //NonFillingLpAxis 위에 있는 축  // UPunchUse[NonFillingLpAxis];     
            int fillingMinindex = Array.IndexOf(ProductSectionfilling, fillingMin);
            //filling = ProductSectionfilling;        //배열복사를 이렇게 하면 안됨 
            ProductSectionfilling.CopyTo(filling, 0); //배열복사 

            filling[fillingMaxindex] = 0;

            for (int i = 0; i < 5; i++)
            {
                if ( 0< filling[i])
                {
                    if (PunchType[i,0] == 0)
                    {
                       filling[i] = parsedot((fillingMax - filling[i]) - ProductStepHigh[i] + ProductStepHigh[fillingMaxindex]);
                    }
                }
            }


            filling[fillingMaxindex] = 0;
            //Lowerpunchfillingmm = filling;
            filling.CopyTo(Lowerpunchfillingmm, 0); //배열복사 

            //Lowerpunchfillingmm[5] = filling[ProductStandardLowPunch]+ (ProductSectionfillingratio[ProductStandardLowPunch] * ProductSectionHigh[ProductStandardLowPunch]);  //다이충진값 
            Lowerpunchfillingmm[5] = parsedot(fillingMax);
            DieFillingmm = Lowerpunchfillingmm[5];
            UpperPunchStepHigh();  //상단차 
        }



        //#####################################################
        public (string lposition, string uposition, float filling) WizardReturn(AxisName PositionName){
            string lposition="-";
            string uposition = "-";
            float filling=0.0F;

            int ch = 0;
            switch (PositionName)
            {
                case AxisName.LpOutter:
                    ch = 1;
                    break;

                case AxisName.LpMiddle:
                    ch = 2;
                    break;

                case AxisName.LpInner:
                    ch = 3;
                    break;

                case AxisName.LpInnerMost:
                    ch = 4;

                    break;

                case AxisName.LpCenterCore:
                    ch = 5;
                    break;

                case AxisName.Die:
                    ch = 6;
                    break;
            }

            uposition = UpStruct[ch];
            lposition = LowStruct[ch];
            filling = ProductSectionfilling[ch];

            return (lposition, uposition, filling);
        }



        //#####################################################
        public bool ProductStandard(AxisName UPunchName, AxisName LPunchName)
        {
            bool up = false;
            bool lp = false;

            switch (UPunchName)
            {
                case AxisName.UpOutter:
                    ProductStandardUpPunch = 1;
                    ProductStandardUpPunchname = AxisName.UpOutter.ToString();
                    break;
                case AxisName.UpMiddle:
                    ProductStandardUpPunch = 2;
                    ProductStandardUpPunchname = AxisName.UpMiddle.ToString();
                    break;
                case AxisName.UpInner:
                    ProductStandardUpPunch = 3;
                    ProductStandardUpPunchname = AxisName.UpInner.ToString();
                    break;
                case AxisName.UpInnerMost:
                    ProductStandardUpPunch = 4;
                    ProductStandardUpPunchname = AxisName.UpInnerMost.ToString();
                    break;
                case AxisName.UpCenterCore:
                    ProductStandardUpPunch = 5;
                    ProductStandardUpPunchname = AxisName.UpCenterCore.ToString();
                    break;
                default: return false;

            }

            switch (LPunchName)
            {
                case AxisName.LpOutter:
                    ProductStandardLowPunch = 1;
                    ProductStandardLowPunchname = AxisName.LpOutter.ToString();
                    break;
                case AxisName.LpMiddle:
                    ProductStandardLowPunch = 2;
                    ProductStandardLowPunchname = AxisName.LpMiddle.ToString();
                    break;
                case AxisName.LpInner:
                    ProductStandardLowPunch = 3;
                    ProductStandardLowPunchname = AxisName.LpInner.ToString();
                    break;
                case AxisName.LpInnerMost:
                    ProductStandardLowPunch = 4;
                    ProductStandardLowPunchname = AxisName.LpInnerMost.ToString();
                    break;
                case AxisName.LpCenterCore:
                    ProductStandardLowPunch = 5;
                    ProductStandardLowPunchname = AxisName.LpCenterCore.ToString();
                    break;
               default: return false;
            }
            //###############
            for (int i = 0; i < 4; i++) {
                if (UpStruct[i] == ProductStandardUpPunchname)
                {
                    up =  true;
                }
            }
            //###############
            for (int i = 0; i < 4; i++)
            {
                if (LowStruct[i] == ProductStandardLowPunchname)
                {
                    lp = true;
                }
            }

            bool ups = ScanStandardUpperPunch(UPunchName);
            bool lps = ScanStandardLowerPunch(LPunchName);

            //###############
            if (ups == false)
            {
                return false;
            }

            //###############
            if (lps == false)
            {
                return false;
            }

            //###############
            if (up == true && lp == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //#####################################################
        public void  ProductStructure(AxisName PunchName, float high, float stephigh, float Filling_ratio)
        {
            int ch = 0;
            switch (PunchName)
            {

                case AxisName.LpOutter:
                    ch = 1;
                    ProductOutSectionHigh = high;
                    ProductSectionHigh[ch] = high;
                    ProductOutStepHigh = stephigh;
                    ProductStepHigh[ch] = stephigh;
                    ProductOutSectionfilling = Filling_ratio;
                    ProductSectionfillingratio[ch] = ProductOutSectionfilling;
                    ProductOutSectionfillingratio = parsedot(high * Filling_ratio);
                    ProductSectionfilling[ch] = ProductOutSectionfillingratio;
                    break;

                case AxisName.LpMiddle:
                    ch = 2;
                    ProductMiddleSectionHigh = high;
                    ProductSectionHigh[ch] = high;
                    ProductMiddleStepHigh = stephigh;
                    ProductStepHigh[ch] = stephigh;
                    ProductMiddleSectionfilling = Filling_ratio;
                    ProductSectionfillingratio[ch] = ProductMiddleSectionfilling;
                    ProductMiddleSectionfillingratio = parsedot(high * Filling_ratio);
                    ProductSectionfilling[ch] = ProductMiddleSectionfillingratio;
                    break;

                case AxisName.LpInner:
                    ch = 3;
                    ProductInnerSectionHigh = high;
                    ProductSectionHigh[ch] = high;
                    ProductInnerStepHigh = stephigh;
                    ProductStepHigh[ch] = stephigh;
                    ProductInnerSectionfilling = Filling_ratio;
                    ProductSectionfillingratio[ch] = ProductInnerSectionfilling;
                    ProductInnerSectionfillingratio = parsedot(high * Filling_ratio);
                    ProductSectionfilling[ch] = ProductInnerSectionfillingratio;
                    break;

                case AxisName.LpInnerMost:
                    ch = 4;
                    ProductCoreSectionHigh = high;
                    ProductSectionHigh[ch] = high;
                    ProductCoreStepHigh = stephigh;
                    ProductStepHigh[ch] = stephigh;
                    ProductCoreSectionfilling = Filling_ratio;
                    ProductSectionfillingratio[ch] = ProductCoreSectionfilling;
                    ProductCoreSectionfillingratio = parsedot(high * Filling_ratio);
                    ProductSectionfilling[ch] = ProductCoreSectionfillingratio;
                    break;

                case AxisName.LpCenterCore:
                    ch = 5;
                    ProductSideCoreSectionHigh = high;
                    ProductSectionHigh[ch] = high;
                    ProductSideCoreStepHigh = stephigh;
                    ProductStepHigh[ch] = stephigh;
                    ProductSideCoreSectionfilling = Filling_ratio;
                    ProductSectionfillingratio[ch] = ProductSideCoreSectionfilling;
                    ProductSideCoreSectionfillingratio = parsedot(high * Filling_ratio);
                    ProductSectionfilling[ch] = ProductSideCoreSectionfillingratio;
                    break;

                case AxisName.Die:
                    ch = 6;
                    ProductDieSectionHigh = high;
                    ProductSectionHigh[ch] = high;
                    ProductDieStepHigh = stephigh;
                    ProductStepHigh[ch] = stephigh;
                    ProductDieSectionfilling = Filling_ratio;
                    ProductSectionfillingratio[ch] = ProductDieSectionfilling;
                    ProductDieSectionfillingratio = parsedot(high * Filling_ratio);
                    ProductSectionfilling[ch] = ProductDieSectionfillingratio;
                    break;
            }
            
            
        }


        //#####################################################
        public bool ScanStandardUpperPunch(AxisName PunchName)
        {
            int IndexNo = 0;
            for(int i=0; i<4; i++)
            {
               if (UpStruct[i] == PunchName.ToString())
                {
                    IndexNo = i;
                    return true;
                }
            }
            return false;
        }

        //#####################################################
        public bool ScanStandardLowerPunch(AxisName PunchName)
        {
            int IndexNo = 0;
            for (int i = 0; i < 4; i++)
            {
                if (LowStruct[i] == PunchName.ToString())
                {
                    IndexNo = i;
                    return true;
                }
            }
            return false;
        }

        //#####################################################
        public String UP_Axis(Int32 axis)
        {
            String SAxis = "-";
            switch (axis)
            {
                case 1:
                    SAxis = AxisName.UpOutter.ToString();
                    break;
                case 2:
                    SAxis = AxisName.UpMiddle.ToString();
                    break;
                    break;
                case 3:
                    SAxis = AxisName.UpInner.ToString();
                    break;
                case 4:
                    SAxis = AxisName.UpInnerMost.ToString();
                    break;
                    break;
                case 5:
                    SAxis = AxisName.UpCenterCore.ToString();
                    break;
                    break;
            }
            return SAxis;
        }

        //#####################################################
        public String LP_Axis(Int32 axis)
        {
            String SAxis = "-";
            switch (axis)
            {
                case 1:
                    SAxis = AxisName.LpOutter.ToString();
                    break;
                case 2:
                    SAxis = AxisName.LpMiddle.ToString();
                    break;
                case 3:
                    SAxis = AxisName.LpInner.ToString();
                    break;
                case 4:
                    SAxis = AxisName.LpInnerMost.ToString();
                    break;
                case 5:
                    SAxis = AxisName.LpCenterCore.ToString();
                    break;
            }
            return SAxis;
        }


        //#####################################################
        public void PunchStructure(AxisName PunchName, Int32 Punch)
        {
            switch (PunchName)
            {
                case AxisName.UpOutter:
                    UpStruct[1] = AxisName.UpOutter.ToString();
                    break;
                case AxisName.UpMiddle:
                    UpStruct[2] = AxisName.UpMiddle.ToString();
                    break;
                case AxisName.UpInner:
                    UpStruct[3] = AxisName.UpInner.ToString();
                    break;
                case AxisName.UpInnerMost:
                    UpStruct[4] = AxisName.UpInnerMost.ToString();
                    break;
                case AxisName.UpCenterCore:
                    UpStruct[5] = AxisName.UpCenterCore.ToString();
                    break;

                case AxisName.LpOutter:
                    LowStruct[1] = AxisName.LpOutter.ToString();
                    break;
                case AxisName.LpMiddle:
                    LowStruct[2] = AxisName.LpMiddle.ToString();
                    break;
                case AxisName.LpInner:
                    LowStruct[3] = AxisName.LpInner.ToString();
                    break;
                case AxisName.LpInnerMost:
                    LowStruct[4] = AxisName.LpInnerMost.ToString();
                    break;
                case AxisName.LpCenterCore:
                    LowStruct[5] = AxisName.LpCenterCore.ToString();
                    break;
            }
        }


        //#####################################################
        /// <summary>
        ///  상펀치의 수를 리턴함 
        /// </summary>
        /// <returns></returns>
        public float UpperPunchEA()
        {
            int uppunchea = 0;
            string[] distArray = UpStruct.Distinct().ToArray();            
            int i = 0;
            foreach (var x in distArray)
            {
               if (distArray [i] != "-")
                {
                    uppunchea += 1;   
                }

                i += 1;
            }
            return uppunchea;
        }

        //#####################################################
        public float LowerPunchEA()
        {
            int lowpunchea = 0;
            string[] distArray = LowStruct.Distinct().ToArray();
            int i = 0;
            foreach (var x in distArray)
            {
                if (distArray[i] != "-")
                {
                    lowpunchea += 1;
                }
                i += 1;
            }
            return lowpunchea;
        }
        //#####################################################
        public float parsedot(float targetValue)
        {
            var str = targetValue.ToString("0.000");
            return float.Parse(str);
        }

    }
}
