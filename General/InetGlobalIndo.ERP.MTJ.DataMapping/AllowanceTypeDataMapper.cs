using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class AllowanceTypeDataMapper
    {
        public static String GetAllowanceTypeText(AllowanceType _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case AllowanceType.MasaKerja:
                    _result = "Working Period";
                    break;
                case AllowanceType.THR:
                    _result = "THR";
                    break;
                case AllowanceType.Penghargaan:
                    _result = "Reward";
                    break;
                case AllowanceType.Pesangon:
                    _result = "Pesangon";
                    break;
            }

            return _result;
        }

        public static String GetAllowanceText(String _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case "MasaKerja":
                    _result = "Working Period";
                    break;
                case "THR":
                    _result = "THR";
                    break;
                case "Penghargaan":
                    _result = "Reward";
                    break;
                case "Pesangon":
                    _result = "Pesangon";
                    break;
            }

            return _result;
        }

        public static String GetAllowanceValue(AllowanceType _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case AllowanceType.MasaKerja:
                    _result = "MasaKerja";
                    break;
                case AllowanceType.THR:
                    _result = "THR";
                    break;
                case AllowanceType.Penghargaan:
                    _result = "Penghargaan";
                    break;
                case AllowanceType.Pesangon:
                    _result = "Pesangon";
                    break;
            }

            return _result;
        }
    }
}