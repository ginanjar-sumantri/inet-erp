using System;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting
{
    public class TransferEndingBalanceBL : Base
    {
        public bool TransferEndingBalance(int year)
        {
            bool _result = false;

            try
            {
                this.db.S_GLTransferYear(year);

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
            
        }
    }
}
