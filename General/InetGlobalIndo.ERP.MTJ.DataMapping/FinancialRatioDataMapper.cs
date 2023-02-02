using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class FinancialRatioDataMapper 
    {
        public static string GetFinancialRatioType(FinancialRatioType _prmFinancialRatioType)
        {
            string _result = "";

            switch(_prmFinancialRatioType)
            {
                case FinancialRatioType.CurrentRatio:
                    _result = "CRT";
                    break;
                case FinancialRatioType.QuickRatio:
                    _result = "QRT";
                    break;
                case FinancialRatioType.AssetTurnover:
                    _result = "ATO";
                    break;
                case FinancialRatioType.InventoryTurnover:
                    _result = "ITO";
                    break;
                case FinancialRatioType.AccountsReceivableTurnover:
                    _result = "ART";
                    break;
                case FinancialRatioType.AverageCollectionPeriod:
                    _result = "ACP";
                    break;
                case FinancialRatioType.GrossProfitMargin:
                    _result = "GPM";
                    break;
                case FinancialRatioType.NetProfitMargin:
                    _result = "ROE";
                    break;
                case FinancialRatioType.ReturnOnAsset:
                    _result = "ROA";
                    break;
                case FinancialRatioType.DividendPayoutRatio:
                    _result = "DPR";
                    break;
                case FinancialRatioType.DebtToAssetRatio:
                    _result = "DAR";
                    break;
                case FinancialRatioType.DebtToEquityRatio:
                    _result = "DER";
                    break;
                case FinancialRatioType.NetWorkingCapital:
                    _result = "NWC";
                    break;
                case FinancialRatioType.DaysSalesOutstandingRatio:
                    _result = "DSO";
                    break;
                case FinancialRatioType.TimesInterestEarnedRatio:
                    _result = "TIE";
                    break;
                default:
                    _result = "CRT";
                    break;
            }

            return _result;
        }

        public static FinancialRatioType GetFinancialRatioType(string _prmFinancialRatioType)
        {
            FinancialRatioType _result = FinancialRatioType.CurrentRatio;

            switch (_prmFinancialRatioType)
            {
                case "CRT":
                    _result = FinancialRatioType.CurrentRatio;
                    break;
                case "QRT":
                    _result = FinancialRatioType.QuickRatio;
                    break;
                case "ATO":
                    _result = FinancialRatioType.AssetTurnover;
                    break;
                case "ITO":
                    _result = FinancialRatioType.InventoryTurnover;
                    break;
                case "ART":
                    _result = FinancialRatioType.AccountsReceivableTurnover;
                    break;
                case "ACP":
                    _result = FinancialRatioType.AverageCollectionPeriod;
                    break;
                case "GPM":
                    _result = FinancialRatioType.GrossProfitMargin;
                    break;
                case "ROE":
                    _result = FinancialRatioType.NetProfitMargin;
                    break;
                case "ROA":
                    _result = FinancialRatioType.ReturnOnAsset;
                    break;
                case "DPR":
                    _result = FinancialRatioType.DividendPayoutRatio;
                    break;
                case "DAR":
                    _result = FinancialRatioType.DebtToAssetRatio;
                    break;
                case "DER":
                    _result = FinancialRatioType.DebtToEquityRatio;
                    break;
                case "NWC":
                    _result = FinancialRatioType.NetWorkingCapital;
                    break;
                case "DSO":
                    _result = FinancialRatioType.DaysSalesOutstandingRatio;
                    break;
                case "TIE":
                    _result = FinancialRatioType.TimesInterestEarnedRatio;
                    break;
                default:
                    _result = FinancialRatioType.CurrentRatio;
                    break;
            }

            return _result;
        }

        public static string GetFinancialRatio(FinancialRatioAcc _prmFinancialRatioAcc)
        {
            string _result = "";

            switch (_prmFinancialRatioAcc)
            {
                case FinancialRatioAcc.CurrentAssets:
                    _result = "001";
                    break;
                case FinancialRatioAcc.CurrentLiabilities:
                    _result = "002";
                    break;
                case FinancialRatioAcc.TotalAssets:
                    _result = "003";
                    break;
                case FinancialRatioAcc.TotalLiabilities:
                    _result = "004";
                    break;
                case FinancialRatioAcc.AccountReceiveable:
                    _result = "005";
                    break;
                case FinancialRatioAcc.Inventories:
                    _result = "006";
                    break;
                case FinancialRatioAcc.Prepayments:
                    _result = "007";
                    break;
                case FinancialRatioAcc.LongTermDebt:
                    _result = "008";
                    break;
                case FinancialRatioAcc.ValueOfLeases:
                    _result = "009";
                    break;
                case FinancialRatioAcc.ShareholdersEquity:
                    _result = "010";
                    break;
                case FinancialRatioAcc.Sales:
                    _result = "011";
                    break;
                case FinancialRatioAcc.COGS:
                    _result = "012";
                    break;
                case FinancialRatioAcc.Devidend:
                    _result = "013";
                    break;
                case FinancialRatioAcc.InterestExpense:
                    _result = "014";
                    break;
                default:
                    _result = "001";
                    break;
            }

            return _result;
        }

        public static string GetGroupLevel(byte _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "Account Type";
                    break;
                case 1:
                    _result = "Account Group";
                    break;
                case 2:
                    _result = "Account Sub Group";
                    break;
                case 3:
                    _result = "Account Class";
                    break;
                case 4:
                    _result = "Account";
                    break;
                default:
                    _result = "Account";
                    break;
            }

            return _result;
        }

        public static byte GetGroupLevel(string _prmValue)
        {
            byte _result = 0;

            switch (_prmValue)
            {
                case "Account Type":
                    _result = 0;
                    break;
                case "Account Group":
                    _result = 1;
                    break;
                case "Account Sub Group":
                    _result = 2;
                    break;
                case "Account Class":
                    _result = 3;
                    break;
                case "Account":
                    _result = 4;
                    break;
                default:
                    _result = 4;
                    break;
            }

            return _result;
        }

        public static byte GetGroupLevel(GroupLevel _prmValue)
        {
            byte _result = 0;

            switch (_prmValue)
            {
                case GroupLevel.AccountType:
                    _result = 0;
                    break;
                case GroupLevel.AccountGroup:
                    _result = 1;
                    break;
                case GroupLevel.AccountSubGroup:
                    _result = 2;
                    break;
                case GroupLevel.AccountClass:
                    _result = 3;
                    break;
                case GroupLevel.Account:
                    _result = 4;
                    break;
                default:
                    _result = 4;
                    break;
            }

            return _result;
        }
    }
}