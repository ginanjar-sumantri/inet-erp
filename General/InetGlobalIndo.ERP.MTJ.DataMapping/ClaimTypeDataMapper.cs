using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ClaimTypeDataMapper 
    {
        public static String GetClaimType(CheckPlafonType _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case CheckPlafonType.All:
                    _result = "All";
                    break;
                case CheckPlafonType.EmployeeID:
                    _result = "Employee ID";
                    break;
                case CheckPlafonType.JobLevel:
                    _result = "Job Level";
                    break;
                case CheckPlafonType.JobTitle:
                    _result = "Job Title";
                    break;
                case CheckPlafonType.WorkPlace:
                    _result = "Work Place";
                    break;
                case CheckPlafonType.EmployeeType:
                    _result = "Employee Type";
                    break;
            }

            return _result;
        }
    }
}