using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class SalaryDataMapper
    {
        public static string GetStatusText(Char _prmSalaryStatus)
        {
            string _result = "";

            switch (_prmSalaryStatus)
            {
                case 'H':
                    _result = "On Hold";
                    break;
                case 'G':
                    _result = "Waiting For Approval";
                    break;
                case 'A':
                    _result = "Approved";
                    break;
                case 'P':
                    _result = "Posted";
                    break;
                case 'C':
                    _result = "Compute";
                    break;
                case 'F':
                    _result = "Complete";
                    break;
                case 'D':
                    _result = "Deleted";
                    break;
                default:
                    _result = "On Hold";
                    break;
            }

            return _result;
        }

        public static SalaryStatus GetStatus(Char _prmSalaryStatus)
        {
            SalaryStatus _result;

            switch (_prmSalaryStatus)
            {
                case 'H':
                    _result = SalaryStatus.OnHold;
                    break;
                case 'G':
                    _result = SalaryStatus.WaitingForApproval;
                    break;
                case 'A':
                    _result = SalaryStatus.Approved;
                    break;
                case 'P':
                    _result = SalaryStatus.Posted;
                    break;
                case 'C':
                    _result = SalaryStatus.Compute;
                    break;
                case 'F':
                    _result = SalaryStatus.Complete;
                    break;
                case 'D':
                    _result = SalaryStatus.Deleted;
                    break;
                default:
                    _result = SalaryStatus.OnHold;
                    break;
            }

            return _result;
        }

        public static Char GetStatus(SalaryStatus _prmSalaryStatus)
        {
            Char _result;

            switch (_prmSalaryStatus)
            {
                case SalaryStatus.OnHold:
                    _result = 'H';
                    break;
                case SalaryStatus.WaitingForApproval:
                    _result = 'G';
                    break;
                case SalaryStatus.Approved:
                    _result = 'A';
                    break;
                case SalaryStatus.Posted:
                    _result = 'P';
                    break;
                case SalaryStatus.Compute:
                    _result = 'C';
                    break;
                case SalaryStatus.Complete:
                    _result = 'F';
                    break;
                case SalaryStatus.Deleted:
                    _result = 'D';
                    break;
                default:
                    _result = 'H';
                    break;
            }

            return _result;
        }
    }
}
