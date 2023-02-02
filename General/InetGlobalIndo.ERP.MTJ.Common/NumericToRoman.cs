using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.Common
{
    public sealed class NumericToRoman
    {
        public NumericToRoman()
        {
        }

        public static String ConvertNumericToRoman(int nr)
        {
            String sNumericToRoman = "";
            try
            {
                if (nr >= 889)
                {
                    sNumericToRoman = "M" + ConvertNumericToRoman(nr - 1000);
                }
                else if (nr >= 389)
                {
                    sNumericToRoman = "D" + ConvertNumericToRoman((nr - 500));
                }
                else if (nr >= 89)
                {
                    sNumericToRoman = "C" + ConvertNumericToRoman((nr - 100));
                }
                else if (nr >= 39)
                {
                    sNumericToRoman = "L" + ConvertNumericToRoman((nr - 50));
                }
                else if (nr >= 9)
                {
                    sNumericToRoman = "X" + ConvertNumericToRoman((nr - 10));
                }
                else if (nr >= 4)
                {
                    sNumericToRoman = "V" + ConvertNumericToRoman((nr - 5));
                }
                else if (nr >= 1)
                {
                    sNumericToRoman = "I" + ConvertNumericToRoman((nr - 1));
                }
                else if (nr <= -889)
                {
                    sNumericToRoman = "M" + ConvertNumericToRoman(nr + 1000);
                }
                else if (nr <= -389)
                {
                    sNumericToRoman = "D" + ConvertNumericToRoman(nr + 500);
                }
                else if (nr <= -89)
                {
                    sNumericToRoman = "C" + ConvertNumericToRoman(nr + 100);
                }
                else if (nr <= -39)
                {
                    sNumericToRoman = "L" + ConvertNumericToRoman(nr + 50);
                }
                else if (nr <= -9)
                {
                    sNumericToRoman = "X" + ConvertNumericToRoman(nr + 10);
                }
                else if (nr <= -4)
                {
                    sNumericToRoman = "V" + ConvertNumericToRoman(nr + 5);
                }
                else if (nr <= -1)
                {
                    sNumericToRoman = "I" + ConvertNumericToRoman(nr + 1);
                }
            }
            catch (Exception ex)
            {
               
            }
            return sNumericToRoman;

        }

        ~NumericToRoman()
        {

        }
    }
}
