using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Web;
using System.Data.Linq.SqlClient;

namespace SMSLibrary
{
    public class StatisticBL : SMSLibBase
    {
        public StatisticBL()
        {
        }

        ~StatisticBL()
        {
        }

        public int GetTodaySentSMS(String _organization, String _userID)
        {
            int _result = 0;
            try
            {
                _result = ( 
                    from _smsSend in this.db.SMSGatewaySends 
                    where _smsSend.OrganizationID == Convert.ToInt32(_organization)
                        && _smsSend.userID == _userID
                        && _smsSend.DateSent.Value.Year  == DateTime.Now.Year 
                        && _smsSend.DateSent.Value.Month == DateTime.Now.Month
                        && _smsSend.DateSent.Value.Date == DateTime.Now.Date
                    select _smsSend
                    ).Count();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public int GetTodayIncomingSMS(string _organization, string _userID)
        {
            int _result = 0;
            try
            {
                _result = ( 
                    from _smsReceived in this.db.SMSGatewayReceives 
                    where _smsReceived.OrganizationID == Convert.ToInt32(_organization)
                        && _smsReceived.userID == _userID
                        && _smsReceived.DateReceived.Value.Year == DateTime.Now.Year
                        && _smsReceived.DateReceived.Value.Month == DateTime.Now.Month
                        && _smsReceived.DateReceived.Value.Date == DateTime.Now.Date
                    select _smsReceived
                    ).Count();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public int GetTodayJunkSMS(string _organization, string _userID)
        {
            int _result = 0;
            try
            {
                _result = (
                    from _smsReceived in this.db.SMSGatewayReceives
                    where _smsReceived.OrganizationID == Convert.ToInt32(_organization)
                        && _smsReceived.userID == _userID
                        && (
                            from _blockedPhone in this.db.MsBlockPhoneBooks 
                            where _blockedPhone.UserID == _userID
                                && _blockedPhone.OrganizationID == Convert.ToInt32(_organization)
                            select _blockedPhone.phoneNumber
                        ).Contains ( _smsReceived.SenderPhoneNo )
                    select _smsReceived
                    ).Count();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public List<String> GetListSentSMSByCategory(string _prmMonth, string _prmYear, string _organization)
        {
            List<String> _result = new List<String>();
            try
            {
                var _qry = (
                        from _smsSent in this.db.SMSGatewaySends
                        where _smsSent.DateSent.Value.Month == Convert.ToInt32(_prmMonth )
                            && _smsSent.DateSent.Value.Year == Convert.ToInt32(_prmYear )
                            && _smsSent.OrganizationID == Convert.ToInt32(_organization)

                        group _smsSent by _smsSent.Category into _group

                        select new { Category = _group.Key, Count = _group.Count() }

                    );
                foreach (var _row in _qry)
                    _result.Add(_row.Category + "|" + _row.Count);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public List<String> GetListSentSMSByUser(string _prmMonth, string _prmYear, string _organization)
        {
            List<String> _result = new List<String>();
            try
            {
                var _qry = (
                        from _sentSMS in this.db.SMSGatewaySends
                        where _sentSMS.DateSent.Value.Month == Convert.ToInt32(_prmMonth)
                            && _sentSMS.DateSent.Value.Year == Convert.ToInt32(_prmYear)
                            && _sentSMS.OrganizationID == Convert.ToInt32(_organization)

                        group _sentSMS by _sentSMS.userID into _group

                        select new { UserID = _group.Key, Count = _group.Count() }

                    );
                foreach (var _row in _qry)
                    _result.Add(_row.UserID + "|" + _row.Count);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public int GetThisMonthSentSMS(string _organization, string _userID)
        {
            int _result = 0;
            try
            {
                _result = (
                        from _smsSent in this.db.SMSGatewaySends 
                        where _smsSent.DateSent.Value.Month == DateTime.Now.Month 
                            && _smsSent.DateSent.Value.Year == DateTime.Now.Year
                            && _smsSent.OrganizationID == Convert.ToInt32(_organization)
                            && _smsSent.userID == _userID
                        select _smsSent.id
                    ).Count();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public int GetThisMonthIncomingSMS(string _organization, string _userID)
        {
            int _result = 0;
            try
            {
                _result = (
                        from _smsIncoming in this.db.SMSGatewayReceives 
                        where _smsIncoming.DateReceived.Value.Month == DateTime.Now.Month 
                            && _smsIncoming.DateReceived.Value.Year == DateTime.Now.Year 
                            && _smsIncoming.OrganizationID == Convert.ToInt32(_organization)
                            && _smsIncoming.userID == _userID
                        select _smsIncoming.id
                    ).Count();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public decimal GetIncomingBalanceTotal(string _organization)
        {
            decimal _result = 0;
            try
            {
                var _qry = (
                        from _balanceIncoming in this.db.TrBalances 
                        where _balanceIncoming.OrganizationID == Convert.ToInt32(_organization)
                            && _balanceIncoming.fgIncrease == true
                        select _balanceIncoming
                    ).Sum(a=>a.Amount);
                _result = Convert.ToDecimal(_qry);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public List<String> GetListTransactionIncoming(string _organization)
        {
            List<String> _result = new List<String>();
            try
            {
                var _qry = (
                        from _incomingTrans in this.db.TrBalances
                        where _incomingTrans.OrganizationID == Convert.ToInt32( _organization)
                            && _incomingTrans.fgIncrease == true
                        select _incomingTrans
                    );
                foreach (var _row in _qry)
                    _result.Add( Convert.ToDateTime( _row.TransDate ).ToString("dd MMM yyyy") + '|' + Convert.ToDecimal( _row.Amount).ToString("#,##0.00"));
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public List<String> GetListOutgoingMasking(string _organization, string _prmYear)
        {
            List<String> _result = new List<String>();
            String[] _monthList = new String[12] {"January","February","March","April"
            ,"May","June","July","August","September","October","November","December"};
            try
            {
                var _qry = (
                        from _outGoingBalance in this.db.TrBalances
                        where _outGoingBalance.OrganizationID == Convert.ToInt32(_organization)
                            && _outGoingBalance.TransDate.Value.Year == Convert.ToInt32 (_prmYear )
                            && _outGoingBalance.fgIncrease == false
                        group _outGoingBalance by _outGoingBalance.TransDate.Value.Month into _group
                        select new { Month = _group.Key ,
                            Amount = _group.Sum(a=>a.Amount) }
                    );
                for (int i = 0; i < 12; i++) {
                    Decimal _currMonthAmount = 0;
                    if ( _qry.Where(a => a.Month == i+1).Count()>0 )
                        _currMonthAmount = Convert.ToDecimal( _qry.Single(a => a.Month == i+1).Amount) ;
                    _result.Add(_monthList[i] + '|' + _currMonthAmount.ToString("#,##0.00"));
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public List<TrBalance> GetListAllTransaction(string _organization)
        {
            List<TrBalance> _result = new List<TrBalance>();
            try
            {
                var _qry = (
                        from _trBalance in this.db.TrBalances
                        where _trBalance.OrganizationID == Convert.ToInt32(_organization)
                        orderby _trBalance.TransDate
                        select _trBalance
                    );
                foreach (var _row in _qry)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }
    }
}
