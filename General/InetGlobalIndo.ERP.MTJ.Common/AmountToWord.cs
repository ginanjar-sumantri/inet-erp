using System;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.Common
{
    public sealed class AmountToWord
    {
        public AmountToWord()
        {
        }

        private static int SearchStr(string sStr, string cStr)
        {
            return sStr.IndexOf(cStr);
        }

        private static string Left(string numStr, int index)
        {
            return numStr.Substring(0, index);
        }

        private static string Right(string numStr, int index)
        {
            if (numStr.Length < index)
                index = numStr.Length;

            return numStr.Substring(numStr.Length - index);
        }

        private static string Mid(string numStr, int start, int length)
        {
            return numStr.Substring(start, length);
        }

        private static string ConvertHundreds(string strNum, Language _prmLanguage)
        {
            string result = "";

            if (System.Convert.ToDecimal(strNum) == 0)
                return "";

            strNum = Right(string.Concat("000", strNum), 3);

            if (string.Compare(Left(strNum, 1), "0") != 0)
            {
                if (_prmLanguage == Language.English)
                {
                    result = string.Concat(ConvertDigit(Left(strNum, 1), _prmLanguage), " Hundred ");
                }
                else if (_prmLanguage == Language.Indonesia)
                {
                    result = string.Concat(ConvertDigit(Left(strNum, 1), _prmLanguage), " Ratus ");
                }
            }

            if (string.Compare(Mid(strNum, 1, 1), "0") != 0)
            {
                result += ConvertTens(Mid(strNum, 1, 2), _prmLanguage);
            }
            else
            {
                result += ConvertDigit(Mid(strNum, 2, 1), _prmLanguage);
            }

            return result;
        }

        private static string ConvertTens(string strTens, Language _prmLanguage)
        {
            string sTens = "";
            if (System.Convert.ToInt16(Left(strTens, 1)) == 1)
            {
                int nTens = System.Convert.ToInt16(strTens);

                if (_prmLanguage == Language.English)
                {
                    switch (nTens)
                    {
                        case 10: sTens = "Ten"; break;
                        case 11: sTens = "Eleven"; break;
                        case 12: sTens = "Twelve"; break;
                        case 13: sTens = "Thirteen"; break;
                        case 14: sTens = "Fourteen"; break;
                        case 15: sTens = "Fifteen"; break;
                        case 16: sTens = "Sixteen"; break;
                        case 17: sTens = "Seventeen"; break;
                        case 18: sTens = "Eighteen"; break;
                        case 19: sTens = "Nineteen"; break;
                    }
                }
                else if (_prmLanguage == Language.Indonesia)
                {
                    switch (nTens)
                    {
                        case 10: sTens = "Sepuluh"; break;
                        case 11: sTens = "Sebelas"; break;
                        case 12: sTens = "Dua belas"; break;
                        case 13: sTens = "Tiga belas"; break;
                        case 14: sTens = "Empat belas"; break;
                        case 15: sTens = "Lima belas"; break;
                        case 16: sTens = "Enam belas"; break;
                        case 17: sTens = "Tujuh belas"; break;
                        case 18: sTens = "Delapan belas"; break;
                        case 19: sTens = "Sembilan belas"; break;
                    }
                }
            }
            else
            {
                if (_prmLanguage == Language.English)
                {
                    switch (System.Convert.ToInt16(Left(strTens, 1)))
                    {
                        case 2: sTens = "Twenty "; break;
                        case 3: sTens = "Thirty "; break;
                        case 4: sTens = "Fourty "; break;
                        case 5: sTens = "Fifty "; break;
                        case 6: sTens = "Sixty "; break;
                        case 7: sTens = "Seventy "; break;
                        case 8: sTens = "Eighty "; break;
                        case 9: sTens = "Ninety "; break;
                    }
                }
                else if (_prmLanguage == Language.Indonesia)
                {
                    switch (System.Convert.ToInt16(Left(strTens, 1)))
                    {
                        case 2: sTens = "Dua puluh "; break;
                        case 3: sTens = "Tiga puluh "; break;
                        case 4: sTens = "Empat puluh "; break;
                        case 5: sTens = "Lima puluh "; break;
                        case 6: sTens = "Enam puluh "; break;
                        case 7: sTens = "Tujuh puluh "; break;
                        case 8: sTens = "Delapan puluh "; break;
                        case 9: sTens = "Sembilan puluh "; break;
                    }
                }

                sTens = string.Concat(sTens, ConvertDigit(Right(strTens, 1), _prmLanguage));
            }
            return sTens;
        }

        private static string ConvertDigit(string strDigit, Language _prmLanguage)
        {
            string sDigit = "";
            int nDigit = System.Convert.ToInt16(strDigit);

            if (_prmLanguage == Language.English)
            {
                switch (nDigit)
                {
                    case 1: sDigit = "One"; break;
                    case 2: sDigit = "Two"; break;
                    case 3: sDigit = "Three"; break;
                    case 4: sDigit = "Four"; break;
                    case 5: sDigit = "Five"; break;
                    case 6: sDigit = "Six"; break;
                    case 7: sDigit = "Seven"; break;
                    case 8: sDigit = "Eight"; break;
                    case 9: sDigit = "Nine"; break;
                    default: sDigit = ""; break;
                }
            }
            else if (_prmLanguage == Language.Indonesia)
            {
                switch (nDigit)
                {
                    case 1: sDigit = "Satu"; break;
                    case 2: sDigit = "Dua"; break;
                    case 3: sDigit = "Tiga"; break;
                    case 4: sDigit = "Empat"; break;
                    case 5: sDigit = "Lima"; break;
                    case 6: sDigit = "Enam"; break;
                    case 7: sDigit = "Tujuh"; break;
                    case 8: sDigit = "Delapan"; break;
                    case 9: sDigit = "Sembilan"; break;
                    default: sDigit = ""; break;
                }
            }

            return sDigit;
        }

        public static string ConvertCurrencyToWord(Decimal amount, Boolean capitalize, Language _prmLanguage, Boolean includeDollarVerbiage, Boolean allCaps)
        {
            string temp = "";
            string dollars = "";
            string cents = "";
            int count = 1;
            int decimalPlace;
            string number = amount.ToString();

            decimalPlace = SearchStr(number, ".");

            if (decimalPlace > 0 && number.Length == decimalPlace + 1)
                number = Left(number, decimalPlace);

            if (decimalPlace > 0 && number.Length > decimalPlace + 1)
            {
                temp = Left(string.Concat(Mid(number, decimalPlace + 1, number.Length - (decimalPlace + 1)), "00"), 2);
                cents = ConvertTens(temp, _prmLanguage);
                number = Left(number, decimalPlace);
            }

            //string[] place = { "", "", " Thousand ", " Million ", " Billion ", " Trillion ", " Quadrillion ", " Quintillion ", " Sextillion " };
            string[] place = new string[9];

            if (_prmLanguage == Language.English)
            {
                place[0] = "";
                place[1] = "";
                place[2] = " Thousand ";
                place[3] = " Million ";
                place[4] = " Billion ";
                place[5] = " Trillion ";
                place[6] = " Quadrillion ";
                place[7] = " Quintillion ";
                place[8] = " Sextillion ";
            }
            else if (_prmLanguage == Language.Indonesia)
            {
                place[0] = "";
                place[1] = "";
                place[2] = " Ribu ";
                place[3] = " Juta ";
                place[4] = " Miliar ";
                place[5] = " Triliyun ";
                place[6] = " Kuadriliyun ";
                place[7] = " Kuintiliyun ";
                place[8] = " Sektiliyun ";
            }

            while (number != "")
            {
                temp = ConvertHundreds(Right(number, 3), _prmLanguage);

                if (temp != "")
                {
                    dollars = string.Concat(string.Concat(temp, place[count]), dollars);
                }

                number = number.Length > 3 ? Left(number, number.Length - 3) : "";
                count++;
            }

            if (includeDollarVerbiage)
            {
                if (dollars.Length == 0)
                {
                    if (_prmLanguage == Language.English)
                        dollars = "Zero"; //"Zero Dollars";
                    else if (_prmLanguage == Language.Indonesia)
                        dollars = "Nol"; //"Nol Rupiah"
                }
                else if (dollars == "One" || dollars == "Satu")
                {
                    if (_prmLanguage == Language.English)
                        dollars = "One"; //"One Dollar";
                    else if (_prmLanguage == Language.Indonesia)
                        dollars = "Satu"; //"Satu Rupiah"
                }
                else
                {
                    if (_prmLanguage == Language.English)
                        dollars += " "; //" Dollars";
                    else if (_prmLanguage == Language.Indonesia)
                        dollars += " ";//" Rupiah"
                }

                if (cents.Length == 0)
                {
                    if (_prmLanguage == Language.English)
                        cents = " And Zero "; //" And Zero Cents";
                    else if (_prmLanguage == Language.Indonesia)
                        cents = " Koma Nol "; //" Koma Nol Rupiah";
                }
                else if (cents == "One")
                {
                    if (_prmLanguage == Language.English)
                        cents = " And One "; //" And One Cent";
                    else if (_prmLanguage == Language.Indonesia)
                        cents = " Koma Satu "; //" Koma Satu Rupiah";
                }
                else
                {
                    if (_prmLanguage == Language.English)
                        //cents = string.Concat(" And ", cents, " Cents ");
                        cents = string.Concat(" And ", cents, " ");
                    else if (_prmLanguage == Language.Indonesia)
                        //cents = string.Concat(" Koma ", cents, " Rupiah ");
                        cents = string.Concat(" Koma ", cents, " ");
                }
            }
            else
            {
                if (cents.Length > 0)
                    cents = " Point " + cents;
            }

            string result = dollars + cents;
            result = result.Replace("   ", " ");
            if (allCaps)
                result = result.ToUpper();
            else
            {
                if (capitalize)
                    result = string.Concat(result.Substring(0, 1).ToUpper(), result.Substring(1, result.Length - 1).ToLower());
                else
                    result = result.ToLower();
            }
            return result;
        }

        public static string ConvertCurrencyToWord(decimal amount, bool capitalize, Language _prmLanguage)
        {
            return ConvertCurrencyToWord(amount, capitalize, _prmLanguage, true, false);
        }

        ~AmountToWord()
        {
        }
    }
}