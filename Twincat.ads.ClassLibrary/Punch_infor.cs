using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwincatAds.ClassLibrary
{


    //########################################
    public class Punch_infor
    {

        //필드정의 
        public bool Use;
        public string position="-";
        public bool Standard;
        public float SectionHigh = 0.0f;
        public float StepHigh = 0.0f;
        public float FillingRatio = 0.0f;


        //internal 연산 필드 
        private float FillingMM = 0.0f;


        //####################
        private float CallFillingMM()
        {
            return FillingMM;
        }

        //####################
        public float parsedot(float targetValue)
        {
            var str = targetValue.ToString("0.000");
            return float.Parse(str);
        }

        //####################
        public void Filling_MM(float SectionHigh, float FillingRatio)
        {
            this.FillingMM = parsedot(SectionHigh * FillingRatio);
        }

            /// <summary>
            /// 축사용(bool), 펀치위치(string) 기준펀치(bool), 제품높이(float), 단차(float),충진비(float)
            /// </summary>
            /// <param name="AxisUse"></param>
            /// <param name="StandardAxis"></param>
            /// <param name="ProductSectionHigh"></param>
            /// <param name="StepHigh"></param>
            /// <param name="OffsetHigh"></param>
            /// <param name="FillingRatio"></param>
            public void Punch_set(bool Use, string position, bool Standard, float SectionHigh, float StepHigh, float FillingRatio)
        {
            this.Use = Use;
            this.position = position;
            this.Standard = Standard;
            this.SectionHigh = SectionHigh;
            this.StepHigh = StepHigh;
            this.FillingRatio = FillingRatio;
        }


    }
}
