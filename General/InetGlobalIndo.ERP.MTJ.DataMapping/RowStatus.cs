using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class RowStatus 
    {
        public static bool Deleted(IsDeleted _prmValue)
        {
            bool _result = false;

            switch (_prmValue)
            {
                case IsDeleted.Yes:
                    _result = true;
                    break;
                case IsDeleted.No:
                    _result = false;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }

        public static IsDeleted Deleted(bool _prmValue)
        {
            IsDeleted _result;

            switch (_prmValue)
            {
                case false:
                    _result = IsDeleted.No;
                    break;
                case true:
                    _result = IsDeleted.Yes;
                    break;
                default:
                    _result = IsDeleted.No;
                    break;
            }

            return _result;
        }
    }
}