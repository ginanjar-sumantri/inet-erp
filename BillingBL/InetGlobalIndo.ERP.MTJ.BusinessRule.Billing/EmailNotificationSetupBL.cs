using System;
using System.Linq;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Web;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Billing
{
    public sealed partial class EmailNotificationSetupBL : Base
    {
        public EmailNotificationSetupBL()
        {
        }

        #region EmailNotificationSetup
        public BILMsEmailNotificationSetup GetSingle(Byte _prmID)
        {
            BILMsEmailNotificationSetup _result = null;

            try
            {
                _result = db.BILMsEmailNotificationSetups.Single(_temp => _temp.ID == _prmID);
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<BILMsEmailNotificationSetup> GetList(String _prmType)
        {
            String _pattern1 = "";
            //String _pattern2 = "";

            if (_prmType.Trim() == "99")
            {
                _pattern1 = "%";
            }
            else
            {
                _pattern1 = _prmType;
            }

            //if (_prmSubType.Trim() == "null")
            //{
            //    _pattern2 = "%";
            //}
            //else
            //{
            //    _pattern2 = _prmSubType;
            //}

            List<BILMsEmailNotificationSetup> _result = new List<BILMsEmailNotificationSetup>();

            try
            {
                var _query = (
                                 from _emailSetup in this.db.BILMsEmailNotificationSetups
                                 where (SqlMethods.Like(_emailSetup.NotificationType.ToString().Trim(), _pattern1.ToString().Trim()))                                    
                                 orderby _emailSetup.ID
                                 select new
                                 {
                                     ID = _emailSetup.ID,
                                     NotificationType = _emailSetup.NotificationType,
                                     EmailFrom = _emailSetup.EmailFrom,
                                     EmailTo = _emailSetup.EmailTo,
                                     Subject = _emailSetup.Subject,
                                     BodyMessage = _emailSetup.BodyMessage
                                 }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new BILMsEmailNotificationSetup(_row.ID, _row.NotificationType, _row.EmailFrom, _row.EmailTo, _row.Subject, _row.BodyMessage));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public Boolean Edit(BILMsEmailNotificationSetup _prmBILMsEmailNotificationSetup)
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

        ~EmailNotificationSetupBL()
        {
        }
    }
}
