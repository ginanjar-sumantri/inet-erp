using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class TermAndConditionDataMapper
    {
        public static String GetTypeText(Byte _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "Sales Confirmation";
                    break;
                case 1:
                    _result = "Contract";
                    break;
            }

            return _result;
        }

        public static TermAndConditionType GetType(Byte _prmValue)
        {
            TermAndConditionType _result = TermAndConditionType.SalesConfirmation;

            switch (_prmValue)
            {
                case 0:
                    _result = TermAndConditionType.SalesConfirmation;
                    break;
                case 1:
                    _result = TermAndConditionType.Contract;
                    break;
            }

            return _result;
        }

        public static Byte GetType(TermAndConditionType _prmStatus)
        {
            Byte _result = 0;

            switch (_prmStatus)
            {
                case TermAndConditionType.SalesConfirmation:
                    _result = 0;
                    break;
                case TermAndConditionType.Contract:
                    _result = 1;
                    break;
            }

            return _result;
        }
    }
}