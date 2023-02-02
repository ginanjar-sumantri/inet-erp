using System;
using System.Linq;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Web;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Billing
{
    public sealed partial class TermAndConditionSetupBL : Base
    {
        public TermAndConditionSetupBL()
        {
        }

        #region TermAndConditionSetup
        public BILMsTermAndConditionSetup GetSingle(Byte _prmID)
        {
            BILMsTermAndConditionSetup _result = null;

            try
            {
                _result = db.BILMsTermAndConditionSetups.Single(_temp => _temp.ID == _prmID);
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<BILMsTermAndConditionSetup> GetList()
        {
            List<BILMsTermAndConditionSetup> _result = new List<BILMsTermAndConditionSetup>();

            try
            {
                var _query = (
                                 from _setup in this.db.BILMsTermAndConditionSetups
                                 orderby _setup.ID
                                 select new
                                 {
                                     ID = _setup.ID,
                                     Type = _setup.Type,
                                     Body = _setup.Body
                                 }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new BILMsTermAndConditionSetup(_row.ID, _row.Type, _row.Body));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public Boolean Edit(BILMsTermAndConditionSetup _prmBILMsTermAndConditionSetup)
        {
            Boolean _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        #endregion

        ~TermAndConditionSetupBL()
        {
        }
    }
}
