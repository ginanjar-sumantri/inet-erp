using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace SMSLibrary
{
    public sealed class SMSMessageBL : SMSLibBase
    {
        public SMSMessageBL()
        {
        }

        ~SMSMessageBL()
        {
        }

        #region SMSGatewayReceive
        public double RowsCountSMSGatewayInbox(String _prmCategory, String _prmKeyword, String _prmCategoryDDL, String _prmRead, String _prmUserID, Int32 _prmOrganization, bool _prmFgAdmin)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "SenderNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "Message")
            {
                _prmKeyword = _prmKeyword.Replace(' ', '%');
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }
            if (_prmRead == "1")
            {
                _pattern3 = "1";
            }
            else if (_prmRead == "2")
            {
                _pattern3 = "0";
            }
            if (_prmFgAdmin)
            {
                var _query =
                               (
                                   from _msSMSGateway in this.db.SMSGatewayReceives
                                   where _msSMSGateway.OrganizationID == _prmOrganization
                                      && (SqlMethods.Like(_msSMSGateway.SenderPhoneNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                      && (SqlMethods.Like(_msSMSGateway.Message.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                      && ((_prmCategoryDDL == "null") ? true : ((_msSMSGateway.Category == _prmCategoryDDL)))
                                      && (SqlMethods.Like(((byte)_msSMSGateway.flagRead).ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                      && !(
                                                   from _blockNo in this.db.MsBlockPhoneBooks
                                                   where _blockNo.OrganizationID == _prmOrganization
                                                       && _blockNo.OrganizationID == (long)_msSMSGateway.OrganizationID
                                                   select _blockNo.phoneNumber
                                             ).Contains(_msSMSGateway.SenderPhoneNo)
                                   select _msSMSGateway
                               ).Count();

                _result = _query;
            }
            else
            {
                var _query =
                            (
                                from _msSMSGateway in this.db.SMSGatewayReceives
                                join _msContact in this.db.MsPhoneBooks
                                on _msSMSGateway.SenderPhoneNo equals _msContact.PhoneNumber
                                where _msContact.UserID == _prmUserID
                                   && _msSMSGateway.OrganizationID == _prmOrganization
                                   && (SqlMethods.Like(_msSMSGateway.SenderPhoneNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSMSGateway.Message.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && ((_prmCategoryDDL == "null") ? true : ((_msSMSGateway.Category == _prmCategoryDDL)))
                                   && (SqlMethods.Like(((byte)_msSMSGateway.flagRead).ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && !(
                                                from _blockNo in this.db.MsBlockPhoneBooks
                                                where _blockNo.UserID == _prmUserID
                                                    && _blockNo.OrganizationID == _prmOrganization
                                                    && _blockNo.OrganizationID == (long)_msSMSGateway.OrganizationID
                                                    && _blockNo.UserID == _msSMSGateway.userID
                                                select _blockNo.phoneNumber
                                          ).Contains(_msSMSGateway.SenderPhoneNo)
                                select _msSMSGateway
                            ).Count();

                _result = _query;
            }


            return _result;
        }

        public double RowsCountSMSGatewayJunk(String _prmCategory, String _prmKeyword, String _prmCategoryDDL, String _prmRead, String _prmUserID, Int32 _prmOrganization, bool _prmFgAdmin)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "SenderNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "Message")
            {
                _prmKeyword = _prmKeyword.Replace(' ', '%');
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }
            if (_prmRead == "1")
            {
                _pattern3 = "1";
            }
            else if (_prmRead == "2")
            {
                _pattern3 = "0";
            }

            if (_prmFgAdmin)
            {
                var _query =
                               (
                                   from _msSMSGateway in this.db.SMSGatewayReceives
                                   where _msSMSGateway.OrganizationID == _prmOrganization
                                      && (SqlMethods.Like(_msSMSGateway.SenderPhoneNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                      && (SqlMethods.Like(_msSMSGateway.Message.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                      && ((_prmCategoryDDL == "null") ? true : ((_msSMSGateway.Category == "InetReg") ? "InetReg" : (_msSMSGateway.Category == "Konf") ? "Konf" : "Other") == _prmCategoryDDL)
                                      && (SqlMethods.Like(((byte)_msSMSGateway.flagRead).ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                      && (
                                                   from _blockNo in this.db.MsBlockPhoneBooks
                                                   where _blockNo.OrganizationID == _prmOrganization
                                                       && _blockNo.OrganizationID == (long)_msSMSGateway.OrganizationID
                                                   select _blockNo.phoneNumber
                                             ).Contains(_msSMSGateway.SenderPhoneNo)
                                   select _msSMSGateway
                               ).Count();

                _result = _query;
            }
            else
            {
                var _query =
                            (
                                from _msSMSGateway in this.db.SMSGatewayReceives
                                join _msContact in this.db.MsPhoneBooks
                                on _msSMSGateway.SenderPhoneNo equals _msContact.PhoneNumber
                                where _msContact.UserID == _prmUserID
                                   && _msSMSGateway.OrganizationID == _prmOrganization
                                   && (SqlMethods.Like(_msSMSGateway.SenderPhoneNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSMSGateway.Message.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && ((_prmCategoryDDL == "null") ? true : ((_msSMSGateway.Category == "InetReg") ? "InetReg" : (_msSMSGateway.Category == "Konf") ? "Konf" : "Other") == _prmCategoryDDL)
                                   && (SqlMethods.Like(((byte)_msSMSGateway.flagRead).ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && (
                                                from _blockNo in this.db.MsBlockPhoneBooks
                                                where _blockNo.OrganizationID == _prmOrganization
                                                    && _blockNo.UserID == _prmUserID
                                                    && _blockNo.OrganizationID == (long)_msSMSGateway.OrganizationID
                                                    && _blockNo.UserID == _msSMSGateway.userID
                                                select _blockNo.phoneNumber
                                          ).Contains(_msSMSGateway.SenderPhoneNo)
                                select _msSMSGateway
                            ).Count();

                _result = _query;
            }
            return _result;
        }

        public List<SMSGatewayReceive> GetListSMSGatewayInbox(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmCategoryDDL, String _prmRead, String _prmUserID, Int32 _prmOrganization, bool _prmFgAdmin)
        {
            List<SMSGatewayReceive> _result = new List<SMSGatewayReceive>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "SenderNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "Message")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }
            if (_prmRead == "1")
            {
                _pattern3 = "1";
            }
            else if (_prmRead == "2")
            {
                _pattern3 = "0";
            }

            try
            {
                if (_prmFgAdmin)
                {
                    var _query = (
                                       from _msSMSGateway in this.db.SMSGatewayReceives
                                       where _msSMSGateway.OrganizationID == _prmOrganization
                                          && (SqlMethods.Like(_msSMSGateway.SenderPhoneNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                          && (SqlMethods.Like(_msSMSGateway.Message.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                          && ((_prmCategoryDDL == "null") ? true : ((_msSMSGateway.Category == _prmCategoryDDL)))
                                          && (SqlMethods.Like(((byte)_msSMSGateway.flagRead).ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                          && !(
                                                   from _blockNo in this.db.MsBlockPhoneBooks
                                                   where _blockNo.OrganizationID == _prmOrganization
                                                       && _blockNo.OrganizationID == (long)_msSMSGateway.OrganizationID
                                                   select _blockNo.phoneNumber
                                             ).Contains(_msSMSGateway.SenderPhoneNo)
                                       orderby _msSMSGateway.flagRead
                                       select new
                                       {
                                           ID = _msSMSGateway.id,
                                           Category = _msSMSGateway.Category,
                                           SenderPhoneNo = _msSMSGateway.SenderPhoneNo,
                                           Message = _msSMSGateway.Message,
                                           flagRead = _msSMSGateway.flagRead,
                                           userID = _msSMSGateway.userID,
                                           ReplyId = _msSMSGateway.ReplyId,
                                           CustID = "",
                                           CustName = "",
                                           DateReceived = _msSMSGateway.DateReceived
                                       }
                                   ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                    foreach (var _row in _query)
                    {
                        _result.Add(new SMSGatewayReceive(_row.ID, _row.Category, _row.SenderPhoneNo, _row.Message, (Byte)_row.flagRead, _row.userID, (long)((_row.ReplyId == null) ? 0 : 1), _row.CustID, _row.CustName, (DateTime)_row.DateReceived));
                    }
                }
                else
                {
                    var _query = (
                                    from _msSMSGateway in this.db.SMSGatewayReceives
                                    join _msContact in this.db.MsPhoneBooks
                                    on _msSMSGateway.SenderPhoneNo equals _msContact.PhoneNumber
                                    where
                                       _msSMSGateway.OrganizationID == _prmOrganization
                                       && _msContact.UserID == _prmUserID
                                       && (SqlMethods.Like(_msSMSGateway.SenderPhoneNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msSMSGateway.Message.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                       && ((_prmCategoryDDL == "null") ? true : ((_msSMSGateway.Category == _prmCategoryDDL)))
                                       && (SqlMethods.Like(((byte)_msSMSGateway.flagRead).ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                       && !(
                                                from _blockNo in this.db.MsBlockPhoneBooks
                                                where _blockNo.UserID == _prmUserID
                                                    && _blockNo.OrganizationID == _prmOrganization
                                                    && _blockNo.OrganizationID == (long)_msSMSGateway.OrganizationID
                                                    && _blockNo.UserID == _msSMSGateway.userID
                                                select _blockNo.phoneNumber
                                          ).Contains(_msSMSGateway.SenderPhoneNo)
                                    orderby _msSMSGateway.flagRead
                                    select new
                                    {
                                        ID = _msSMSGateway.id,
                                        Category = _msSMSGateway.Category,
                                        SenderPhoneNo = _msSMSGateway.SenderPhoneNo,
                                        Message = _msSMSGateway.Message,
                                        flagRead = _msSMSGateway.flagRead,
                                        userID = _msSMSGateway.userID,
                                        ReplyId = _msSMSGateway.ReplyId,
                                        CustID = "",
                                        CustName = "",
                                        DateReceived = _msSMSGateway.DateReceived
                                    }
                                ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                    foreach (var _row in _query)
                    {
                        _result.Add(new SMSGatewayReceive(_row.ID, _row.Category, _row.SenderPhoneNo, _row.Message, (Byte)_row.flagRead, _row.userID, (long)((_row.ReplyId == null) ? 0 : 1), _row.CustID, _row.CustName, (DateTime)_row.DateReceived));
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<SMSGatewayReceive> GetListSMSGatewayJunk(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmCategoryDDL, String _prmRead, String _prmUserID, Int32 _prmOrganization, bool _prmFgAdmin)
        {
            List<SMSGatewayReceive> _result = new List<SMSGatewayReceive>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "SenderNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "Message")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }
            if (_prmRead == "1")
            {
                _pattern3 = "1";
            }
            else if (_prmRead == "2")
            {
                _pattern3 = "0";
            }

            try
            {
                if (_prmFgAdmin)
                {
                    var _query = (
                                        from _msSMSGateway in this.db.SMSGatewayReceives
                                        where _msSMSGateway.OrganizationID == _prmOrganization
                                           && (SqlMethods.Like(_msSMSGateway.SenderPhoneNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           && (SqlMethods.Like(_msSMSGateway.Message.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                           && ((_prmCategoryDDL == "null") ? true : ((_msSMSGateway.Category == "InetReg") ? "InetReg" : (_msSMSGateway.Category == "Konf") ? "Konf" : "Other") == _prmCategoryDDL)
                                           && (SqlMethods.Like(((byte)_msSMSGateway.flagRead).ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                           && (
                                                    from _blockNo in this.db.MsBlockPhoneBooks
                                                    where _blockNo.OrganizationID == _prmOrganization
                                                        && _blockNo.OrganizationID == (long)_msSMSGateway.OrganizationID
                                                    select _blockNo.phoneNumber
                                              ).Contains(_msSMSGateway.SenderPhoneNo)
                                        orderby _msSMSGateway.flagRead
                                        select new
                                        {
                                            ID = _msSMSGateway.id,
                                            Category = _msSMSGateway.Category,
                                            SenderPhoneNo = _msSMSGateway.SenderPhoneNo,
                                            Message = _msSMSGateway.Message,
                                            flagRead = _msSMSGateway.flagRead,
                                            userID = _msSMSGateway.userID,
                                            ReplyId = _msSMSGateway.ReplyId,
                                            CustID = "",
                                            CustName = "",
                                            DateReceived = _msSMSGateway.DateReceived
                                        }
                                    ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                    foreach (var _row in _query)
                    {
                        _result.Add(new SMSGatewayReceive(_row.ID, _row.Category, _row.SenderPhoneNo, _row.Message, (Byte)_row.flagRead, _row.userID, (long)((_row.ReplyId == null) ? 0 : 1), _row.CustID, _row.CustName, (DateTime)_row.DateReceived));
                    }
                }
                else
                {
                    var _query = (
                                    from _msSMSGateway in this.db.SMSGatewayReceives
                                    join _msContact in this.db.MsPhoneBooks
                                    on _msSMSGateway.SenderPhoneNo equals _msContact.PhoneNumber
                                    where _msContact.UserID == _prmUserID
                                       && _msSMSGateway.OrganizationID == _prmOrganization
                                       && (SqlMethods.Like(_msSMSGateway.SenderPhoneNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msSMSGateway.Message.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                       && ((_prmCategoryDDL == "null") ? true : ((_msSMSGateway.Category == "InetReg") ? "InetReg" : (_msSMSGateway.Category == "Konf") ? "Konf" : "Other") == _prmCategoryDDL)
                                       && (SqlMethods.Like(((byte)_msSMSGateway.flagRead).ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                       && (
                                                from _blockNo in this.db.MsBlockPhoneBooks
                                                where _blockNo.UserID == _prmUserID
                                                    && _blockNo.OrganizationID == _prmOrganization
                                                    && _blockNo.OrganizationID == (long)_msSMSGateway.OrganizationID
                                                    && _blockNo.UserID == _msSMSGateway.userID
                                                select _blockNo.phoneNumber
                                          ).Contains(_msSMSGateway.SenderPhoneNo)
                                    orderby _msSMSGateway.flagRead
                                    select new
                                    {
                                        ID = _msSMSGateway.id,
                                        Category = _msSMSGateway.Category,
                                        SenderPhoneNo = _msSMSGateway.SenderPhoneNo,
                                        Message = _msSMSGateway.Message,
                                        flagRead = _msSMSGateway.flagRead,
                                        userID = _msSMSGateway.userID,
                                        ReplyId = _msSMSGateway.ReplyId,
                                        CustID = "",
                                        CustName = "",
                                        DateReceived = _msSMSGateway.DateReceived
                                    }
                                ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                    foreach (var _row in _query)
                    {
                        _result.Add(new SMSGatewayReceive(_row.ID, _row.Category, _row.SenderPhoneNo, _row.Message, (Byte)_row.flagRead, _row.userID, (long)((_row.ReplyId == null) ? 0 : 1), _row.CustID, _row.CustName, (DateTime)_row.DateReceived));
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public SMSGatewayReceive GetSingleSMSGatewayReceive(string _prmCode)
        {
            SMSGatewayReceive _result = null;

            try
            {
                _result = this.db.SMSGatewayReceives.Single(_temp => _temp.id.ToString() == _prmCode);
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public bool AddSMSGatewayReceive(SMSGatewayReceive _prmSMSGatewayReceive)
        {
            bool _result = false;

            try
            {
                this.db.SMSGatewayReceives.InsertOnSubmit(_prmSMSGatewayReceive);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public bool EditSMSGatewayReceive(SMSGatewayReceive _prmSMSGatewayReceive)
        {
            bool _result = false;

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

        public bool DeleteMultiSMSGatewayReceive(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    SMSGatewayReceive _msSMSGateway = this.db.SMSGatewayReceives.Single(_temp => _temp.id.ToString().Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.SMSGatewayReceives.DeleteOnSubmit(_msSMSGateway);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {

            }

            return _result;
        }
        #endregion

        #region SMSGatewaySend
        public double RowsCountSMSGatewaySendOutbox(string _prmCategory, string _prmKeyword, String _prmCategoryDDL, String _prmStatus, String _prmUserID, Int32 _prmOrganization, bool _prmFgAdmin)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "DestinationNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Message")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }
            if (_prmStatus == "1")
            {
                _pattern3 = "0";
            }
            else if (_prmStatus == "2")
            {
                _pattern3 = "2";
            }
            if (_prmFgAdmin)
            {
                var _query =
                           (
                               from _msSMSGateway in this.db.SMSGatewaySends
                               where _msSMSGateway.OrganizationID == _prmOrganization
                                  && (SqlMethods.Like(_msSMSGateway.DestinationPhoneNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                  && (SqlMethods.Like(_msSMSGateway.Message.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                  && (SqlMethods.Like(((byte)_msSMSGateway.flagSend).ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                  && (((byte)_msSMSGateway.flagSend).ToString().Trim().ToLower()) != "1"
                               select _msSMSGateway
                           ).Count();

                _result = _query;
            }
            else
            {
                var _query =
                            (
                                from _msSMSGateway in this.db.SMSGatewaySends
                                where _msSMSGateway.OrganizationID == _prmOrganization
                                   && _msSMSGateway.userID == _prmUserID
                                   && (SqlMethods.Like(_msSMSGateway.DestinationPhoneNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSMSGateway.Message.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like(((byte)_msSMSGateway.flagSend).ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && (((byte)_msSMSGateway.flagSend).ToString().Trim().ToLower()) != "1"
                                select _msSMSGateway
                            ).Count();

                _result = _query;
            }
            return _result;
        }

        public double RowsCountSMSGatewaySendSentItems(string _prmCategory, string _prmKeyword, String _prmCategoryDDL, String _prmUserID, Int32 _prmOrganization, bool _prmFgAdmin)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "DestinationNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "Message")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            if (_prmCategoryDDL != "null")
            {
                _pattern3 = _prmCategoryDDL;
            }

            if (_prmFgAdmin)
            {
                var _query =
                            (
                                from _msSMSGateway in this.db.SMSGatewaySends
                                where _msSMSGateway.OrganizationID == _prmOrganization
                                   && (SqlMethods.Like(_msSMSGateway.DestinationPhoneNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSMSGateway.Message.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSMSGateway.Category.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                       && (((byte)_msSMSGateway.flagSend).ToString().Trim().ToLower()) == "1"
                                select _msSMSGateway
                            ).Count();

                _result = _query;
            }
            else
            {

                var _query =
                            (
                                from _msSMSGateway in this.db.SMSGatewaySends
                                where _msSMSGateway.OrganizationID == _prmOrganization
                                   && _msSMSGateway.userID == _prmUserID
                                   && (SqlMethods.Like(_msSMSGateway.DestinationPhoneNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSMSGateway.Message.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSMSGateway.Category.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                       && (((byte)_msSMSGateway.flagSend).ToString().Trim().ToLower()) == "1"
                                select _msSMSGateway
                            ).Count();

                _result = _query;
            }
            return _result;
        }

        public List<SMSGatewaySend> GetListSMSGatewaySendOutbox(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmCategoryDDL, String _prmStatus, String _prmUserID, Int32 _prmOrganization, bool _prmFgAdmin)
        {
            List<SMSGatewaySend> _result = new List<SMSGatewaySend>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "Message")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "DestinationPhoneNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }
            if (_prmStatus == "1")
            {
                _pattern3 = "0";
            }
            else if (_prmStatus == "2")
            {
                _pattern3 = "2";
            }

            try
            {
                if (_prmFgAdmin)
                {
                    var _query = (
                                    from _msSMSGateway in this.db.SMSGatewaySends
                                    where _msSMSGateway.OrganizationID == _prmOrganization
                                        && (SqlMethods.Like(_msSMSGateway.Message.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(_msSMSGateway.DestinationPhoneNo.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                        && (SqlMethods.Like(((byte)_msSMSGateway.flagSend).ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                        && (((byte)_msSMSGateway.flagSend).ToString().Trim().ToLower()) != "1"
                                    select new
                                    {
                                        ID = _msSMSGateway.id,
                                        Category = _msSMSGateway.Category,
                                        DestinationPhoneNo = _msSMSGateway.DestinationPhoneNo,
                                        Message = _msSMSGateway.Message,
                                        flagSend = _msSMSGateway.flagSend,
                                        userID = _msSMSGateway.userID,
                                        DateSent = _msSMSGateway.DateSent
                                    }
                                ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                    foreach (var _row in _query)
                    {
                        _result.Add(new SMSGatewaySend(_row.ID, _row.Category, _row.DestinationPhoneNo, _row.Message, (Byte)_row.flagSend, _row.userID, _row.DateSent));
                    }
                }
                else
                {
                    var _query = (
                                    from _msSMSGateway in this.db.SMSGatewaySends
                                    where _msSMSGateway.OrganizationID == _prmOrganization
                                        && _msSMSGateway.userID == _prmUserID
                                        && (SqlMethods.Like(_msSMSGateway.Message.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(_msSMSGateway.DestinationPhoneNo.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                        && (SqlMethods.Like(((byte)_msSMSGateway.flagSend).ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                        && (((byte)_msSMSGateway.flagSend).ToString().Trim().ToLower()) != "1"
                                    select new
                                    {
                                        ID = _msSMSGateway.id,
                                        Category = _msSMSGateway.Category,
                                        DestinationPhoneNo = _msSMSGateway.DestinationPhoneNo,
                                        Message = _msSMSGateway.Message,
                                        flagSend = _msSMSGateway.flagSend,
                                        userID = _msSMSGateway.userID,
                                        DateSent = _msSMSGateway.DateSent
                                    }
                                ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                    foreach (var _row in _query)
                    {
                        _result.Add(new SMSGatewaySend(_row.ID, _row.Category, _row.DestinationPhoneNo, _row.Message, (Byte)_row.flagSend, _row.userID, _row.DateSent));
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<SMSGatewaySend> GetListSMSGatewaySendSentItems(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmCategoryDDL, String _prmUserID, Int32 _prmOrganization, bool _prmFgAdmin)
        {
            List<SMSGatewaySend> _result = new List<SMSGatewaySend>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "DestinationNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }
            else if (_prmCategory == "Message")
            {
                _pattern2 = "%" + _prmKeyword + "%";
            }

            if (_prmCategoryDDL != "null")
            {
                _pattern3 = _prmCategoryDDL;
            }

            try
            {
                if (_prmFgAdmin)
                {
                    var _query = (
                                    from _msSMSGateway in this.db.SMSGatewaySends
                                    where _msSMSGateway.OrganizationID == _prmOrganization
                                       && (SqlMethods.Like(_msSMSGateway.DestinationPhoneNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msSMSGateway.Message.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                       && (SqlMethods.Like(_msSMSGateway.Category.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                       && (((byte)_msSMSGateway.flagSend).ToString().Trim().ToLower()) == "1"
                                    orderby _msSMSGateway.DateSent descending
                                    select new
                                    {
                                        ID = _msSMSGateway.id,
                                        Category = _msSMSGateway.Category,
                                        DestinationPhoneNo = _msSMSGateway.DestinationPhoneNo,
                                        Message = _msSMSGateway.Message,
                                        flagSend = _msSMSGateway.flagSend,
                                        userID = _msSMSGateway.userID,
                                        DateSent = _msSMSGateway.DateSent
                                    }
                                ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                    foreach (var _row in _query)
                    {
                        _result.Add(new SMSGatewaySend(_row.ID, _row.Category, _row.DestinationPhoneNo, _row.Message, (Byte)_row.flagSend, _row.userID, (DateTime)_row.DateSent));
                    }
                }
                else
                {
                    var _query = (
                                    from _msSMSGateway in this.db.SMSGatewaySends
                                    where _msSMSGateway.OrganizationID == _prmOrganization
                                       && _msSMSGateway.userID == _prmUserID
                                       && (SqlMethods.Like(_msSMSGateway.DestinationPhoneNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msSMSGateway.Message.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                       && (SqlMethods.Like(_msSMSGateway.Category.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                       && (((byte)_msSMSGateway.flagSend).ToString().Trim().ToLower()) == "1"
                                    select new
                                    {
                                        ID = _msSMSGateway.id,
                                        Category = _msSMSGateway.Category,
                                        DestinationPhoneNo = _msSMSGateway.DestinationPhoneNo,
                                        Message = _msSMSGateway.Message,
                                        flagSend = _msSMSGateway.flagSend,
                                        userID = _msSMSGateway.userID,
                                        DateSent = _msSMSGateway.DateSent
                                    }
                                ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                    foreach (var _row in _query)
                    {
                        _result.Add(new SMSGatewaySend(_row.ID, _row.Category, _row.DestinationPhoneNo, _row.Message, (Byte)_row.flagSend, _row.userID, (DateTime)_row.DateSent));
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public SMSGatewaySend GetSingleSMSGatewaySend(string _prmCode)
        {
            SMSGatewaySend _result = null;

            try
            {
                _result = this.db.SMSGatewaySends.Single(_temp => _temp.id.ToString() == _prmCode);
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool AddSMSGatewaySend(List<SMSGatewaySend> _prmSMSGatewaySend)
        {
            bool _result = false;


            try
            {
                foreach (var _item in _prmSMSGatewaySend)
                {
                    this.db.SMSGatewaySends.InsertOnSubmit(_item);
                }
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public bool AddSMSGatewaySend(SMSGatewaySend _prmSMSGatewaySend, String _prmCode)
        {
            bool _result = false;

            try
            {
                this.db.SMSGatewaySends.InsertOnSubmit(_prmSMSGatewaySend);

                if (_prmCode != "")
                {
                    SMSGatewayReceive _smsRcv = this.db.SMSGatewayReceives.Single(_temp => _temp.id.ToString() == _prmCode);
                    _smsRcv.flagRead = 1;
                    _smsRcv.ReplyId = _prmSMSGatewaySend.id;
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool EditSMSGatewaySend(SMSGatewaySend _prmSMSGatewaySend)
        {
            bool _result = false;

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

        public bool DeleteMultiSMSGatewaySend(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    SMSGatewaySend _msSMSGateway = this.db.SMSGatewaySends.Single(_temp => _temp.id.ToString().Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.SMSGatewaySends.DeleteOnSubmit(_msSMSGateway);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }
        #endregion

        #region Groups
        public double RowsCountPhoneGroup(String _prmOrgID, String _prmUserID, String _fgAdmin)
        {
            double _result = 0;
            var _query = (
                            from _msPhoneBook in this.db.MsPhoneBooks
                            where (_msPhoneBook.PhoneBookGroup ?? "") != ""
                                && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                            select _msPhoneBook
                        );
            if (!Convert.ToBoolean(_fgAdmin))
                _query = _query.Where(a => a.UserID == _prmUserID);

            _result = (from _qry in _query select _qry.PhoneBookGroup).Distinct().Count();

            return _result;
        }

        public List<MsPhoneBook> GetListPhoneGroup(String _prmOrgID, String _prmUserID, String _fgAdmin)
        {
            List<MsPhoneBook> _result = new List<MsPhoneBook>();
            try
            {
                var _query = (
                                from _msPhoneBook in this.db.MsPhoneBooks
                                where (_msPhoneBook.PhoneBookGroup ?? "") != ""
                                    && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                select _msPhoneBook
                            );
                if (!Convert.ToBoolean(_fgAdmin))
                    _query = _query.Where(a => a.UserID == _prmUserID);
                var _qryResult = (from _qry in _query select _qry.PhoneBookGroup).Distinct();
                foreach (var _row in _qryResult)
                    _result.Add(new MsPhoneBook(_row));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public double RowCountPhoneGroupByGroupName(String _prmGroupName, String _prmOrgID, String _prmUserID, String _fgAdmin)
        {
            double _result = 0;

            var _query =
                        (
                            from _msPhoneBook in this.db.MsPhoneBooks
                            where _msPhoneBook.PhoneBookGroup == _prmGroupName
                                && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                            select _msPhoneBook
                        );
            if (!Convert.ToBoolean(_fgAdmin))
                _query = _query.Where(a => a.UserID == _prmUserID);

            _result = _query.Count();

            return _result;
        }

        public List<MsPhoneBook> GetListPhoneGroupByGroupName(int _prmReqPage, int _prmPageSize, String _prmGroupName, String _prmOrgID, String _prmUserID, String _fgAdmin)
        {
            List<MsPhoneBook> _result = new List<MsPhoneBook>();

            try
            {
                var _queryTemp = (
                                from _msPhoneBook in this.db.MsPhoneBooks
                                where _msPhoneBook.PhoneBookGroup == _prmGroupName
                                    && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                select _msPhoneBook
                                );
                if (!Convert.ToBoolean(_fgAdmin))
                    _queryTemp = _queryTemp.Where(a => a.UserID == _prmUserID);
                var _query = (
                                from _msPhoneBook in _queryTemp
                                select new
                                {
                                    id = _msPhoneBook.id,
                                    Name = _msPhoneBook.Name,
                                    PhoneNumber = _msPhoneBook.PhoneNumber,
                                    PhoneBookGroup = _msPhoneBook.PhoneBookGroup
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsPhoneBook(_row.id, _row.Name, _row.PhoneNumber, _row.PhoneBookGroup));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public int RowsCountContactListNotYetAssigned(String _prmCategory, String _prmKeyword, String _prmOrgID, String _prmUserID, String _fgAdmin)
        {
            int _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "Company")
                _pattern1 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "Religion")
                _pattern2 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "City")
                _pattern3 = "%" + _prmKeyword + "%";

            try
            {
                var _query = (
                                from _msPhoneBook in this.db.MsPhoneBooks
                                where (SqlMethods.Like(_msPhoneBook.Name.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msPhoneBook.Religion.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like(_msPhoneBook.City.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && (_msPhoneBook.PhoneBookGroup == "" || _msPhoneBook.PhoneBookGroup == null)
                                   && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                select _msPhoneBook
                               );
                if (!Convert.ToBoolean(_fgAdmin))
                    _query = _query.Where(a => a.UserID == _prmUserID);
                _result = (from _msPhoneBook in _query
                           select _msPhoneBook.id
                            ).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<MsPhoneBook> GetListAllContactListNotYetAssigned(String _prmCategory, String _prmKeyword, String _prmOrgID, String _prmUserID, String _fgAdmin)
        {
            List<MsPhoneBook> _result = new List<MsPhoneBook>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "Company")
                _pattern1 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "Religion")
                _pattern2 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "City")
                _pattern3 = "%" + _prmKeyword + "%";

            try
            {
                var _query = (
                                from _msPhoneBook in this.db.MsPhoneBooks
                                where (SqlMethods.Like(_msPhoneBook.Name.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msPhoneBook.Religion.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like(_msPhoneBook.City.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && (_msPhoneBook.PhoneBookGroup == "" || _msPhoneBook.PhoneBookGroup == null)
                                   && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                select _msPhoneBook
                                );
                if (!Convert.ToBoolean(_fgAdmin))
                    _query = _query.Where(a => a.UserID == _prmUserID);

                foreach (var _row in _query)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<MsPhoneBook> GetListContactListNotYetAssigned(String _prmCategory, String _prmKeyword, int _prmReqPage, int _prmPageSize, String _prmOrgID, String _prmUserID, String _fgAdmin)
        {
            List<MsPhoneBook> _result = new List<MsPhoneBook>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "Company")
                _pattern1 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "Religion")
                _pattern2 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "City")
                _pattern3 = "%" + _prmKeyword + "%";

            try
            {
                var _queryTemp = (
                                from _msPhoneBook in this.db.MsPhoneBooks
                                where (SqlMethods.Like(_msPhoneBook.Name.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msPhoneBook.Religion.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like(_msPhoneBook.City.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && (_msPhoneBook.PhoneBookGroup == "" || _msPhoneBook.PhoneBookGroup == null)
                                   && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                select _msPhoneBook);

                if (!Convert.ToBoolean(_fgAdmin))
                    _queryTemp = _queryTemp.Where(a => a.UserID == _prmUserID);

                var _query = (from _msPhoneBook in _queryTemp
                              select new
                              {
                                  id = _msPhoneBook.id,
                                  Name = _msPhoneBook.Name
                              }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsPhoneBook(_row.id, _row.Name));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public List<MsPhoneBook> GetListContactListNotYetAssigned(String _prmCategory, String _prmKeyword, String _prmOrgID, String _prmUserID, String _fgAdmin)
        {
            List<MsPhoneBook> _result = new List<MsPhoneBook>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "Company")
                _pattern1 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "Religion")
                _pattern2 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "City")
                _pattern3 = "%" + _prmKeyword + "%";

            try
            {
                var _queryTemp = (
                                from _msPhoneBook in this.db.MsPhoneBooks
                                where (SqlMethods.Like(_msPhoneBook.Name.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msPhoneBook.Religion.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like(_msPhoneBook.City.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && (_msPhoneBook.PhoneBookGroup == "" || _msPhoneBook.PhoneBookGroup == null)
                                   && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                select _msPhoneBook);

                if (!Convert.ToBoolean(_fgAdmin))
                    _queryTemp = _queryTemp.Where(a => a.UserID == _prmUserID);

                var _query = (from _msPhoneBook in _queryTemp
                              select new
                              {
                                  id = _msPhoneBook.id,
                                  Name = _msPhoneBook.Name
                              }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsPhoneBook(_row.id, _row.Name));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public int RowsCountContactListNotInPhoneGroup(String _prmCategory, String _prmKeyword, String _prmPhoneGroup, String _prmOrgID, String _prmUserID, String _fgAdmin)
        {
            int _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "Company")
                _pattern1 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "Religion")
                _pattern2 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "City")
                _pattern3 = "%" + _prmKeyword + "%";

            try
            {
                var _query = (
                                from _msPhoneBook in this.db.MsPhoneBooks
                                where (SqlMethods.Like(_msPhoneBook.Name.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msPhoneBook.Religion.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like(_msPhoneBook.City.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && (_msPhoneBook.PhoneBookGroup != _prmPhoneGroup)
                                   && (_msPhoneBook.PhoneBookGroup == "" || _msPhoneBook.PhoneBookGroup == null)
                                   && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                select _msPhoneBook);
                if (!Convert.ToBoolean(_fgAdmin))
                    _query = _query.Where(a => a.UserID == _prmUserID);
                _result = _query.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public List<MsPhoneBook> GetListContactListNotInPhoneGroup(int _prmReqPage, int _prmPageSize, String _prmCategory, String _prmKeyword, String _prmPhoneGroup, String _prmUserID, Int32 _prmOrganization, bool _prmFgAdmin)
        {
            List<MsPhoneBook> _result = new List<MsPhoneBook>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "Company")
                _pattern1 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "Religion")
                _pattern2 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "City")
                _pattern3 = "%" + _prmKeyword + "%";

            try
            {
                if (_prmFgAdmin)
                {
                    var _query = (
                                    from _msPhoneBook in this.db.MsPhoneBooks
                                    where _msPhoneBook.OrganizationID == _prmOrganization
                                       && (SqlMethods.Like(_msPhoneBook.Name.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msPhoneBook.Religion.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                       && (SqlMethods.Like(_msPhoneBook.City.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                       && (_msPhoneBook.PhoneBookGroup != _prmPhoneGroup)
                                       && (_msPhoneBook.PhoneBookGroup == "" || _msPhoneBook.PhoneBookGroup == null)
                                    select new
                                    {
                                        id = _msPhoneBook.id,
                                        Name = _msPhoneBook.Name
                                    }
                                ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                    foreach (var _row in _query)
                    {
                        _result.Add(new MsPhoneBook(_row.id, _row.Name));
                    }
                }
                else
                {

                    var _query = (
                                    from _msPhoneBook in this.db.MsPhoneBooks
                                    where _msPhoneBook.OrganizationID == _prmOrganization
                                       && _msPhoneBook.UserID == _prmUserID
                                       && (SqlMethods.Like(_msPhoneBook.Name.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msPhoneBook.Religion.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                       && (SqlMethods.Like(_msPhoneBook.City.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                       && (_msPhoneBook.PhoneBookGroup != _prmPhoneGroup)
                                       && (_msPhoneBook.PhoneBookGroup == "" || _msPhoneBook.PhoneBookGroup == null)
                                    select new
                                    {
                                        id = _msPhoneBook.id,
                                        Name = _msPhoneBook.Name
                                    }
                                ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                    foreach (var _row in _query)
                    {
                        _result.Add(new MsPhoneBook(_row.id, _row.Name));
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public String AddGroup(String[] _prmContactList, String _prmGroupName)
        {
            String _result = "";

            try
            {
                foreach (var _item in _prmContactList)
                {
                    MsPhoneBook _phoneBook = this.db.MsPhoneBooks.Single(_temp => _temp.id.ToString().Trim().ToLower() == _item.ToString().Trim().ToLower());

                    _phoneBook.PhoneBookGroup = _prmGroupName;

                    this.db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public String EditGroup(String[] _prmContactList, String _prmGroupName)
        {
            String _result = "";

            try
            {
                foreach (var _item in _prmContactList)
                {
                    MsPhoneBook _phoneBook = this.db.MsPhoneBooks.Single(_temp => _temp.id.ToString().Trim().ToLower() == _item.ToString().Trim().ToLower());
                    _phoneBook.PhoneBookGroup = _prmGroupName;

                    this.db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public bool DeleteMultiPhoneGroup(string[] _prmPhoneGroup)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmPhoneGroup.Length; i++)
                {
                    var _query = (
                                    from _phoneBook in this.db.MsPhoneBooks
                                    where _phoneBook.PhoneBookGroup.ToString().Trim().ToLower() == _prmPhoneGroup[i].Trim().ToLower()
                                    select _phoneBook.id
                                 );

                    foreach (var _item in _query)
                    {
                        MsPhoneBook _msPhoneBook = this.db.MsPhoneBooks.Single(_temp => _temp.id.ToString().Trim().ToLower() == _item.ToString().Trim().ToLower());
                        _msPhoneBook.PhoneBookGroup = "";

                        this.db.SubmitChanges();
                    }
                }

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool DeleteMultiContactListPhoneGroup(string[] _prmGroupID)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmGroupID.Length; i++)
                {
                    var _query = (
                                    from _phoneBook in this.db.MsPhoneBooks
                                    where _phoneBook.id.ToString().Trim().ToLower() == _prmGroupID[i].Trim().ToLower()
                                    select _phoneBook.id
                                 );

                    foreach (var _item in _query)
                    {
                        MsPhoneBook _msPhoneBook = this.db.MsPhoneBooks.Single(_temp => _temp.id.ToString().Trim().ToLower() == _item.ToString().Trim().ToLower());
                        _msPhoneBook.PhoneBookGroup = "";

                        this.db.SubmitChanges();
                    }
                }

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool DeleteMultiContactList(string[] _prmGroupID)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmGroupID.Length; i++)
                {
                    var _query = (
                                    from _phoneBook in this.db.MsPhoneBooks
                                    where _phoneBook.id.ToString().Trim().ToLower() == _prmGroupID[i].Trim().ToLower()
                                    select _phoneBook.id
                                 );

                    foreach (var _item in _query)
                    {
                        MsPhoneBook _msPhoneBook = this.db.MsPhoneBooks.Single(_temp => _temp.id.ToString().Trim().ToLower() == _item.ToString().Trim().ToLower());
                        _msPhoneBook.PhoneBookGroup = "";

                        this.db.SubmitChanges();
                    }
                }

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool EditListPhoneGroupName(String _prmUserID, Int32 _prmOrganization, String _prmOldName, String _prmNewName)
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _phoneBook in this.db.MsPhoneBooks
                                where _phoneBook.UserID == _prmUserID
                                && _phoneBook.OrganizationID == _prmOrganization
                                && _phoneBook.PhoneBookGroup == _prmOldName
                                select _phoneBook.id
                             );

                foreach (var _item in _query)
                {
                    MsPhoneBook _msPhoneBook = this.db.MsPhoneBooks.Single(_temp => _temp.id.ToString().Trim().ToLower() == _item.ToString().Trim().ToLower());
                    _msPhoneBook.PhoneBookGroup = _prmNewName;

                    this.db.SubmitChanges();
                    _result = true;
                }

            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        #endregion

        #region Contacts
        public double RowsCountMsPhoneBook(string _prmCategory, string _prmKeyword, String _prmOrgID, String _prmUserID, String _prmFgAdmin)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "Name")
                _pattern1 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "PhoneNo")
                _pattern2 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "PhoneGroup")
                _pattern3 = "%" + _prmKeyword + "%";

            if (Convert.ToBoolean(_prmFgAdmin))
            {
                var _query = (
                                from _msPhoneBook in this.db.MsPhoneBooks
                                where (SqlMethods.Like(_msPhoneBook.Name.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msPhoneBook.PhoneNumber.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like(_msPhoneBook.PhoneBookGroup.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                select _msPhoneBook
                            ).Count();
                _result = _query;
            }
            else
            {
                var _query = (
                                from _msPhoneBook in this.db.MsPhoneBooks
                                where (SqlMethods.Like(_msPhoneBook.Name.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msPhoneBook.PhoneNumber.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like(_msPhoneBook.PhoneBookGroup.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                   && _msPhoneBook.UserID == _prmUserID
                                select _msPhoneBook
                            ).Count();
                _result = _query;
            }

            return _result;
        }

        public double RowsCountAdvanceMsPhoneBook(string _prmKeyword, String _prmOrgID, String _prmUserID, String _prmFgAdmin)
        {
            double _result = 0;

            string _pattern1 = "%" + _prmKeyword + "%";
            //string _pattern2 = "%%";
            //string _pattern3 = "%%";

            //if (_prmCategory == "Name")
            //    _pattern1 = "%" + _prmKeyword + "%";
            //else if (_prmCategory == "PhoneNo")
            //    _pattern2 = "%" + _prmKeyword + "%";
            //else if (_prmCategory == "PhoneGroup")
            //    _pattern3 = "%" + _prmKeyword + "%";

            if (Convert.ToBoolean(_prmFgAdmin))
            {
                var _query = (
                                from _msPhoneBook in this.db.MsPhoneBooks
                                where (SqlMethods.Like(_msPhoneBook.Name.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.PhoneNumber.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.PhoneBookGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.Company.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.Email.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.City.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.BirthdayWishes.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.Remark.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.JobTitle.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.Address.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                select _msPhoneBook
                            ).Count();
                _result = _query;
            }
            else
            {
                var _query = (
                                from _msPhoneBook in this.db.MsPhoneBooks
                                where (SqlMethods.Like(_msPhoneBook.Name.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.PhoneNumber.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.PhoneBookGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.Company.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.Email.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.City.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.BirthdayWishes.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.Remark.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.JobTitle.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.Address.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                           && _msPhoneBook.UserID == _prmUserID
                                select _msPhoneBook
                            ).Count();
                _result = _query;
            }

            return _result;
        }

        //public double RowsCountAdvanceMsPhoneBook(string _prmKeyword, String _prmOrgID, String _prmUserID, String _prmFgAdmin)
        //{
        //    double _result = 0;

        //    string _pattern1 = "%" + _prmKeyword + "%";
        //    //string _pattern2 = "%%";
        //    //string _pattern3 = "%%";

        //    //if (_prmCategory == "Name")
        //    //    _pattern1 = "%" + _prmKeyword + "%";
        //    //else if (_prmCategory == "PhoneNo")
        //    //    _pattern2 = "%" + _prmKeyword + "%";
        //    //else if (_prmCategory == "PhoneGroup")
        //    //    _pattern3 = "%" + _prmKeyword + "%";

        //    if (Convert.ToBoolean(_prmFgAdmin))
        //    {
        //        var _query = (
        //                        from _msPhoneBook in this.db.MsPhoneBooks
        //                        where (SqlMethods.Like(_msPhoneBook.Name.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                               || (SqlMethods.Like(_msPhoneBook.PhoneNumber.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                               || (SqlMethods.Like(_msPhoneBook.PhoneBookGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                               || (SqlMethods.Like(_msPhoneBook.Company.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                               || (SqlMethods.Like(_msPhoneBook.Email.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                               || (SqlMethods.Like(_msPhoneBook.City.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                               || (SqlMethods.Like(_msPhoneBook.BirthdayWishes.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                               || (SqlMethods.Like(_msPhoneBook.Remark.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                               || (SqlMethods.Like(_msPhoneBook.JobTitle.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                               || (SqlMethods.Like(_msPhoneBook.Address.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                               && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
        //                        select _msPhoneBook
        //                    ).Count();
        //        _result = _query;
        //    }
        //    else
        //    {
        //        var _query = (
        //                        from _msPhoneBook in this.db.MsPhoneBooks
        //                        where (SqlMethods.Like(_msPhoneBook.Name.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                                   || (SqlMethods.Like(_msPhoneBook.PhoneNumber.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                                   || (SqlMethods.Like(_msPhoneBook.PhoneBookGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                                   || (SqlMethods.Like(_msPhoneBook.Company.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                                   || (SqlMethods.Like(_msPhoneBook.Email.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                                   || (SqlMethods.Like(_msPhoneBook.City.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                                   || (SqlMethods.Like(_msPhoneBook.BirthdayWishes.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                                   || (SqlMethods.Like(_msPhoneBook.Remark.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                                   || (SqlMethods.Like(_msPhoneBook.JobTitle.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                                   || (SqlMethods.Like(_msPhoneBook.Address.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                                   && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
        //                                   && _msPhoneBook.UserID == _prmUserID
        //                        select _msPhoneBook
        //                    ).Count();
        //        _result = _query;
        //    }

        //    return _result;
        //}

        public List<MsPhoneBook> GetListMsPhoneBook(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrgID, String _prmUserID, bool _prmFgAdmin)
        {
            List<MsPhoneBook> _result = new List<MsPhoneBook>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "Name")
                _pattern1 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "PhoneNo")
                _pattern2 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "PhoneGroup")
                _pattern3 = "%" + _prmKeyword + "%";

            try
            {

                if (_prmFgAdmin)
                {
                    var _query = (
                                    from _msPhoneBook in this.db.MsPhoneBooks
                                    where (SqlMethods.Like(_msPhoneBook.Name.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msPhoneBook.PhoneNumber.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                       && (SqlMethods.Like(_msPhoneBook.PhoneBookGroup.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                       && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                    select new
                                    {
                                        ID = _msPhoneBook.id,
                                        OrganizationID = _msPhoneBook.OrganizationID,
                                        UserID = _msPhoneBook.UserID,
                                        Name = _msPhoneBook.Name,
                                        PhoneNumber = _msPhoneBook.PhoneNumber,
                                        Email = _msPhoneBook.Email,
                                        PhoneBookGroup = _msPhoneBook.PhoneBookGroup
                                    }
                                ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                    foreach (var _row in _query)
                    {
                        _result.Add(new MsPhoneBook(_row.ID, _row.OrganizationID, _row.UserID, _row.Name, _row.PhoneNumber, _row.Email, _row.PhoneBookGroup));
                    }
                }
                else
                {
                    var _query = (
                                        from _msPhoneBook in this.db.MsPhoneBooks
                                        where (SqlMethods.Like(_msPhoneBook.Name.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           && (SqlMethods.Like(_msPhoneBook.PhoneNumber.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                           && (SqlMethods.Like(_msPhoneBook.PhoneBookGroup.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                           && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                           && _msPhoneBook.UserID == _prmUserID
                                        select new
                                        {
                                            ID = _msPhoneBook.id,
                                            OrganizationID = _msPhoneBook.OrganizationID,
                                            UserID = _msPhoneBook.UserID,
                                            Name = _msPhoneBook.Name,
                                            PhoneNumber = _msPhoneBook.PhoneNumber,
                                            Email = _msPhoneBook.Email,
                                            PhoneBookGroup = _msPhoneBook.PhoneBookGroup
                                        }
                                    ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                    foreach (var _row in _query)
                    {
                        _result.Add(new MsPhoneBook(_row.ID, _row.OrganizationID, _row.UserID, _row.Name, _row.PhoneNumber, _row.Email, _row.PhoneBookGroup));
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<MsPhoneBook> GetListMsPhoneBookAdvanceSearch(int _prmReqPage, int _prmPageSize, string _prmKeyword, String _prmOrgID, String _prmUserID, bool _prmFgAdmin)
        {
            List<MsPhoneBook> _result = new List<MsPhoneBook>();

            string _pattern1 = "%" + _prmKeyword + "%";

            //string _pattern2 = "%%";
            //string _pattern3 = "%%";

            //if (_prmCategory == "Name")
            //    _pattern1 = "%" + _prmKeyword + "%";
            //else if (_prmCategory == "PhoneNo")
            //    _pattern2 = "%" + _prmKeyword + "%";
            //else if (_prmCategory == "PhoneGroup")
            //    _pattern3 = "%" + _prmKeyword + "%";

            try
            {

                if (_prmFgAdmin)
                {
                    var _query = (
                                    from _msPhoneBook in this.db.MsPhoneBooks
                                    where (SqlMethods.Like(_msPhoneBook.Name.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.PhoneNumber.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.PhoneBookGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.Company.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.Email.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.City.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.BirthdayWishes.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.Remark.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.JobTitle.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.Address.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                    select new
                                    {
                                        ID = _msPhoneBook.id,
                                        OrganizationID = _msPhoneBook.OrganizationID,
                                        UserID = _msPhoneBook.UserID,
                                        Name = _msPhoneBook.Name,
                                        PhoneNumber = _msPhoneBook.PhoneNumber,
                                        Email = _msPhoneBook.Email,
                                        PhoneBookGroup = _msPhoneBook.PhoneBookGroup
                                    }
                                ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                    foreach (var _row in _query)
                    {
                        _result.Add(new MsPhoneBook(_row.ID, _row.OrganizationID, _row.UserID, _row.Name, _row.PhoneNumber, _row.Email, _row.PhoneBookGroup));
                    }
                }
                else
                {
                    var _query = (
                                        from _msPhoneBook in this.db.MsPhoneBooks
                                        where (SqlMethods.Like(_msPhoneBook.Name.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.PhoneNumber.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.PhoneBookGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.Company.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.Email.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.City.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.BirthdayWishes.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.Remark.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.JobTitle.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.Address.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                           && _msPhoneBook.UserID == _prmUserID
                                        select new
                                        {
                                            ID = _msPhoneBook.id,
                                            OrganizationID = _msPhoneBook.OrganizationID,
                                            UserID = _msPhoneBook.UserID,
                                            Name = _msPhoneBook.Name,
                                            PhoneNumber = _msPhoneBook.PhoneNumber,
                                            Email = _msPhoneBook.Email,
                                            PhoneBookGroup = _msPhoneBook.PhoneBookGroup
                                        }
                                    ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                    foreach (var _row in _query)
                    {
                        _result.Add(new MsPhoneBook(_row.ID, _row.OrganizationID, _row.UserID, _row.Name, _row.PhoneNumber, _row.Email, _row.PhoneBookGroup));
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<MsPhoneBook> GetListMsPhoneBookNameCard(int _prmReqPage, int _prmPageSize, string _prmKeyword, String _prmOrgID, String _prmUserID, bool _prmFgAdmin)
        {
            List<MsPhoneBook> _result = new List<MsPhoneBook>();

            string _pattern1 = "%" + _prmKeyword + "%";

            //string _pattern2 = "%%";
            //string _pattern3 = "%%";

            //if (_prmCategory == "Name")
            //    _pattern1 = "%" + _prmKeyword + "%";
            //else if (_prmCategory == "PhoneNo")
            //    _pattern2 = "%" + _prmKeyword + "%";
            //else if (_prmCategory == "PhoneGroup")
            //    _pattern3 = "%" + _prmKeyword + "%";

            try
            {

                if (_prmFgAdmin)
                {
                    var _query = (
                                    from _msPhoneBook in this.db.MsPhoneBooks
                                    where (SqlMethods.Like(_msPhoneBook.Name.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.PhoneNumber.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.PhoneBookGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.Company.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.Email.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.City.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.BirthdayWishes.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.Remark.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.JobTitle.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       || (SqlMethods.Like(_msPhoneBook.Address.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                    select new
                                    {
                                        ID = _msPhoneBook.id,
                                        NameCardPicture = _msPhoneBook.NameCardPicture,
                                        Remark = _msPhoneBook.Remark,                                        
                                    }
                                ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                    foreach (var _row in _query)
                    {
                        _result.Add(new MsPhoneBook(_row.ID, _row.NameCardPicture, _row.Remark));
                    }
                }
                else
                {
                    var _query = (
                                        from _msPhoneBook in this.db.MsPhoneBooks
                                        where (SqlMethods.Like(_msPhoneBook.Name.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.PhoneNumber.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.PhoneBookGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.Company.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.Email.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.City.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.BirthdayWishes.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.Remark.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.JobTitle.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           || (SqlMethods.Like(_msPhoneBook.Address.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                           && _msPhoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                           && _msPhoneBook.UserID == _prmUserID
                                        select new
                                        {
                                            ID = _msPhoneBook.id,
                                            NameCardPicture = _msPhoneBook.NameCardPicture,
                                            Remark = _msPhoneBook.Remark,
                                        }
                                    ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                    foreach (var _row in _query)
                    {
                        _result.Add(new MsPhoneBook(_row.ID, _row.NameCardPicture, _row.Remark));
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public MsPhoneBook GetSinglePhoneBook(string _prmCode)
        {
            MsPhoneBook _result = null;

            try
            {
                _result = this.db.MsPhoneBooks.Single(_temp => _temp.id.ToString() == _prmCode);
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool AddPhoneBook(MsPhoneBook _prmMsPhoneBook)
        {
            bool _result = false;

            try
            {
                this.db.MsPhoneBooks.InsertOnSubmit(_prmMsPhoneBook);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool EditPhoneBook(MsPhoneBook _prmMsPhoneBook)
        {
            bool _result = false;

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

        public bool DeleteMultiMsPhoneBook(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MsPhoneBook _msPhoneBook = this.db.MsPhoneBooks.Single(_temp => _temp.id.ToString().Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsPhoneBooks.DeleteOnSubmit(_msPhoneBook);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<String> GetListContactFromGroups(String _prmGroupList)
        {
            List<String> _result = new List<String>();

            String[] _phoneGroup = _prmGroupList.Split(',');

            foreach (var _groupName in _phoneGroup)
            {
                var _query = (
                                from _phoneContact in this.db.MsPhoneBooks
                                where _phoneContact.PhoneBookGroup.Trim().ToLower() == _groupName.Trim().ToLower()
                                select _phoneContact.PhoneNumber
                             );

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }

            return _result;
        }
        #endregion

        public List<SMSGatewayReceive> GetListSMSGatewayReceiveForDDL()
        {
            List<SMSGatewayReceive> _result = new List<SMSGatewayReceive>();
            Boolean _update = false;

            var _query = (
                            from _smsReceive in this.db.SMSGatewayReceives
                            orderby _smsReceive.Category ascending
                            select new
                            {
                                Category = _smsReceive.Category
                            }
                         ).Distinct();

            foreach (var _row in _query)
            {
                _update = false;

                if (_result.Count == 0)
                {
                    _update = true;
                }

                foreach (var _item in _result)
                {
                    if (_item.Category != _row.Category)
                    {
                        _update = true;
                    }
                    else
                    {
                        break;
                    }
                }

                if (_update)
                {
                    _result.Add(new SMSGatewayReceive(_row.Category, _row.Category));
                }
            }

            return _result;
        }

        public List<SMSGatewaySend> GetListSMSGatewaySendForDDL(String _prmUserID, Int32 _prmOrganization, bool _prmFgAdmin)
        {
            List<SMSGatewaySend> _result = new List<SMSGatewaySend>();
            Boolean _update = false;

            if (_prmFgAdmin)
            {
                var _query = (
                                from _smsReceive in this.db.SMSGatewaySends
                                where _smsReceive.OrganizationID == _prmOrganization
                                orderby _smsReceive.Category ascending
                                select new
                                {
                                    Category = _smsReceive.Category
                                }
                             ).Distinct();
                foreach (var _row in _query)
                {
                    _update = false;

                    if (_result.Count == 0)
                    {
                        _update = true;
                    }

                    foreach (var _item in _result)
                    {
                        if (_item.Category != _row.Category)
                        {
                            _update = true;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (_update)
                    {
                        //_result.Add(new SMSGatewaySend(_row.Category, SMSCategoryDataMapper.SMSCategoryText(_row.Category)));
                        _result.Add(new SMSGatewaySend(_row.Category, _row.Category));
                    }
                }
            }
            else
            {
                var _query = (
                                   from _smsReceive in this.db.SMSGatewaySends
                                   where _smsReceive.OrganizationID == _prmOrganization
                                   && _smsReceive.userID == _prmUserID
                                   orderby _smsReceive.Category ascending
                                   select new
                                   {
                                       Category = _smsReceive.Category
                                   }
                                ).Distinct();
                foreach (var _row in _query)
                {
                    _update = false;

                    if (_result.Count == 0)
                    {
                        _update = true;
                    }

                    foreach (var _item in _result)
                    {
                        if (_item.Category != _row.Category)
                        {
                            _update = true;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (_update)
                    {
                        //_result.Add(new SMSGatewaySend(_row.Category, SMSCategoryDataMapper.SMSCategoryText(_row.Category)));
                        _result.Add(new SMSGatewaySend(_row.Category, _row.Category));
                    }
                }
            }



            return _result;
        }

        public long GetMaxNoSend()
        {
            long _result = 0;

            long _query = 0;
            var _count = (
                            from _smsSend in this.db.SMSGatewaySends
                            select _smsSend.id
                         ).Count();

            if (_count > 0)
            {
                _query = (
                                from _smsSend in this.db.SMSGatewaySends
                                select _smsSend.id
                             ).Max();
            }
            else
            {
                _query = 0;
            }

            _result = _query + 1;

            return _result;
        }

        public void SetSMSGatewayReceiveToRead(String _prmID, String _prmUserName)
        {
            try
            {
                SMSGatewayReceive _smsRcv = this.db.SMSGatewayReceives.Single(_temp => _temp.id.ToString() == _prmID);
                _smsRcv.flagRead = 1;
                _smsRcv.userID = _prmUserName;
                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public void SetSMSGatewaySendToQueued(String _prmID)
        {
            try
            {
                SMSGatewaySend _smsSend = this.db.SMSGatewaySends.Single(_temp => _temp.id.ToString() == _prmID);
                _smsSend.flagSend = 0;
                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<String> GetListPhoneBook(String _prmOrgID, String _prmUserID, String _prmFgAdmin)
        {
            List<String> _result = new List<String>();
            try
            {
                if (Convert.ToBoolean(_prmFgAdmin))
                {
                    var _qry = (
                            from _phoneBook in this.db.MsPhoneBooks
                            where _phoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                            orderby _phoneBook.Name
                            select _phoneBook
                        );
                    foreach (var _row in _qry)
                        _result.Add(_row.PhoneNumber + "," + _row.Name);
                }
                else
                {
                    var _qry = (
                            from _phoneBook in this.db.MsPhoneBooks
                            where _phoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                && _phoneBook.UserID == _prmUserID
                            orderby _phoneBook.Name
                            select _phoneBook
                        );
                    foreach (var _row in _qry)
                        _result.Add(_row.PhoneNumber + "," + _row.Name);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<String> GetListPhoneBook(String _prmOrgID, String _prmUserID, String _prmFgAdmin, int _requestPage, int _pageSize, String _prmSearch)
        {
            List<String> _result = new List<String>();
            try
            {
                if (Convert.ToBoolean(_prmFgAdmin))
                {
                    var _qry = (
                            from _phoneBook in this.db.MsPhoneBooks
                            where _phoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                && SqlMethods.Like(_phoneBook.Name, "%" + _prmSearch + "%")
                            orderby _phoneBook.Name
                            select _phoneBook
                        ).Skip(_requestPage * _pageSize).Take(_pageSize);
                    foreach (var _row in _qry)
                        _result.Add(_row.PhoneNumber + "," + _row.Name);
                }
                else
                {
                    var _qry = (
                            from _phoneBook in this.db.MsPhoneBooks
                            where _phoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                && SqlMethods.Like(_phoneBook.Name, "%" + _prmSearch + "%")
                                && _phoneBook.UserID == _prmUserID
                            orderby _phoneBook.Name
                            select _phoneBook
                        ).Skip(_requestPage * _pageSize).Take(_pageSize);
                    foreach (var _row in _qry)
                        _result.Add(_row.PhoneNumber + "," + _row.Name);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public double ListPhoneBookCount(String _prmOrgId, String _prmUserID, String _prmFgAdmin, String _prmSearch)
        {
            double _result = 0;
            try
            {
                if (Convert.ToBoolean(_prmFgAdmin))
                {
                    var _qry = (
                            from _phoneBook in this.db.MsPhoneBooks
                            where _phoneBook.OrganizationID == Convert.ToInt32(_prmOrgId)
                                && SqlMethods.Like(_phoneBook.Name, "%" + _prmSearch + "%")
                            orderby _phoneBook.Name
                            select _phoneBook
                        );
                    _result = _qry.Count();
                }
                else
                {
                    var _qry = (
                            from _phoneBook in this.db.MsPhoneBooks
                            where _phoneBook.OrganizationID == Convert.ToInt32(_prmOrgId)
                                && SqlMethods.Like(_phoneBook.Name, "%" + _prmSearch + "%")
                                && _phoneBook.UserID == _prmUserID
                            orderby _phoneBook.Name
                            select _phoneBook
                        );
                    _result = _qry.Count();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public String ConvertPhoneBookContactIDArrayToPhoneNumberArray(String _prmContactIDArray)
        {
            String _result = "";
            try
            {
                String[] splitContactID = _prmContactIDArray.Split(',');
                foreach (String _contactID in splitContactID)
                {
                    String _contactPhoneNumber = this.db.MsPhoneBooks.Single(a => a.id == Convert.ToInt32(_contactID)).PhoneNumber;
                    _result += "," + _contactPhoneNumber;
                }
                _result = _result.Substring(1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public DateTime GetLastLimitReset(String _prmUserID, Int32 _prmOrganization)
        {
            DateTime _result = new DateTime();

            try
            {
                var _query = (from _msUser in this.db.MsUsers
                              where _msUser.UserID == _prmUserID
                              && _msUser.OrganizationID == _prmOrganization
                              select _msUser
                            ).FirstOrDefault();

                _result = Convert.ToDateTime(_query.LastLimitReset);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public Int32 GetSMSLimit(String _prmUserID, Int32 _prmOrganization)
        {
            Int32 _result = 0;
            try
            {
                var _query = (from _msUser in this.db.MsUsers
                              where _msUser.UserID == _prmUserID
                              && _msUser.OrganizationID == _prmOrganization
                              select _msUser
                           ).FirstOrDefault();
                _result = Convert.ToInt32(_query.SMSLimit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Int32 GetSMSPerDay(String _prmPackageName)
        {
            Int32 _result;

            _result = Convert.ToInt32(this.db.MSPackages.Single(_temp => _temp.PackageName == _prmPackageName).SMSPerDay);

            return _result;
        }

        public bool EditSubmit()
        {
            bool _result = false;
            try
            {
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public void DecreaseSMSLimit(String _OrgID, String _UserId, Int32 _prmSMSLimitDecrease)
        {
            MsUser _msUser = this.db.MsUsers.Single(a => a.OrganizationID == Convert.ToInt32(_OrgID) && a.UserID == _UserId);
            _msUser.SMSLimit -= _prmSMSLimitDecrease;
            this.db.SubmitChanges();
        }

        public void ResetLimitSMS(int _organization, string _UserID)
        {
            try
            {
                MsUser _updateData = this.db.MsUsers.Single(a => a.OrganizationID == _organization && a.UserID == _UserID);
                Int32 _resetLimitAmount = Convert.ToInt32(this.db.MSPackages.Single(a => a.PackageName == _updateData.PackageName).SMSPerDay);
                if (_updateData.LastLimitReset < DateTime.Now.Date)
                {
                    _updateData.SMSLimit = _resetLimitAmount;
                    _updateData.LastLimitReset = DateTime.Now.Date;
                }
                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String GetNameFromPhoneBook(String _prmPhoneNumber, Int32 _prmOrgID, String _prmUserID, Boolean _prmFgAdmin)
        {
            String _result = _prmPhoneNumber;
            String _altPhoneNumber = _prmPhoneNumber;
            if (_prmPhoneNumber.Substring(0, 1) == "0")
            {
                _altPhoneNumber = "+62" + _altPhoneNumber.Substring(1, _altPhoneNumber.Length - 1);
            }

            try
            {
                if (_prmFgAdmin)
                {
                    var _qry = (
                            from _phoneBook in this.db.MsPhoneBooks
                            where _phoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                && (_phoneBook.PhoneNumber == _prmPhoneNumber || _phoneBook.PhoneNumber == _altPhoneNumber)
                            select _phoneBook
                        );
                    if (_qry.Count() > 0) _result += " (" + _qry.FirstOrDefault().Name + ")";
                }
                else
                {
                    var _qry = (
                            from _phoneBook in this.db.MsPhoneBooks
                            where _phoneBook.OrganizationID == Convert.ToInt32(_prmOrgID)
                                && _phoneBook.UserID == _prmUserID
                                && (_phoneBook.PhoneNumber == _prmPhoneNumber || _phoneBook.PhoneNumber == _altPhoneNumber)
                            select _phoneBook
                        );
                    if (_qry.Count() > 0) _result += " (" + _qry.FirstOrDefault().Name + ")";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Boolean isAutoReplyExist(String _autoReplyKeyWord, String _autoReplyPhoneNumber)
        {
            Boolean _result = false;
            try
            {
                _result = (this.db.TrAutoReplies.Where(a => a.KeyWord == _autoReplyKeyWord && a.PhoneNumber == _autoReplyPhoneNumber).Count() > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public TrAutoReply GetSingleTrAutoReply(String _autoReplyKeyWord, String _autoReplyPhoneNumber)
        {
            TrAutoReply _result = new TrAutoReply();
            var _qry = (
                from _trAutoreply in this.db.TrAutoReplies
                where _trAutoreply.KeyWord == _autoReplyKeyWord
                    && _trAutoreply.PhoneNumber == _autoReplyPhoneNumber
                select _trAutoreply
            );
            if (_qry.Count() > 0)
                _result = _qry.FirstOrDefault();

            return _result;
        }

        public void AddAutoReply(TrAutoReply _newData)
        {
            try
            {
                this.db.TrAutoReplies.InsertOnSubmit(_newData);
                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String GetFooterAdditionalMessage(Int32 _prmOrganizationID)
        {
            try
            {
                return this.db.MsOrganizations.Where(a => a.OrganizationID == _prmOrganizationID).FirstOrDefault().FooterAdditionalMessage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean decreaseMaskingBalance(int _organization, int _smsCount)
        {
            Boolean _result = false;
            try
            {
                Decimal _maskingPrice = Convert.ToDecimal(this.db.MsBackEndSettings.Single(a => a.Description == "MaskingPrice").Value);
                MsOrganization _updateData = this.db.MsOrganizations.Single(a => a.OrganizationID == _organization);
                if (_updateData.MaskingBalanceAccount < _maskingPrice * _smsCount)
                    return false;
                _updateData.MaskingBalanceAccount -= (_maskingPrice * _smsCount);

                TrBalance _insertTransaction = new TrBalance();
                _insertTransaction.id = this.GetMaxNoBalance() + 1;
                _insertTransaction.Amount = (_maskingPrice * _smsCount);
                _insertTransaction.fgIncrease = false;
                _insertTransaction.OrganizationID = _organization;
                _insertTransaction.TransDate = DateTime.Now;
                _insertTransaction.Description = "Masking Usage";
                this.db.TrBalances.InsertOnSubmit(_insertTransaction);
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        private int GetMaxNoBalance()
        {
            int _result = 0;

            var _query = (
                            from _balance in this.db.TrBalances
                            select _balance.id
                         ).Max();

            if (_result != null)
            {
                _result = Convert.ToInt32(_query);
            }

            return _result;
        }

        public List<MsCountryCode> GetListCountryCode()
        {
            List<MsCountryCode> _result = new List<MsCountryCode>();
            try
            {
                var _qry = (
                        from _countryCode in this.db.MsCountryCodes select _countryCode
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

        public SMSGatewaySend GetSingleSMSGatewaySend(DateTime _prmDateSend, string _prmMessage, string _prmDestinationPhoneNumber)
        {
            SMSGatewaySend _result = new SMSGatewaySend();
            try
            {
                var _qry = (
                        from _smsSend in this.db.SMSGatewaySends
                        where _smsSend.DateSent == _prmDateSend
                            && _smsSend.Message == _prmMessage
                            && _smsSend.DestinationPhoneNo == _prmDestinationPhoneNumber
                        select _smsSend
                    );
                if (_qry.Count() > 0)
                    return this.db.SMSGatewaySends.Single(a => a.id == _qry.FirstOrDefault().id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<String> GetListPhoneGroupForCheckBoxList(string _prmOrgID, string _UserID, string _fgAdmin)
        {
            List<String> _result = new List<String>();
            try
            {
                var _queryTemp = (from _groups in this.db.MsPhoneBooks
                                  where _groups.OrganizationID == Convert.ToInt32(_prmOrgID)
                                   && _groups.PhoneBookGroup != ""
                                  select _groups);
                if (!Convert.ToBoolean(_fgAdmin))
                    _queryTemp = _queryTemp.Where(a => a.UserID == _UserID);
                var _queryGroup = (from _temp in _queryTemp select _temp.PhoneBookGroup).Distinct();
                foreach (var _rowGroup in _queryGroup)
                {
                    String _tempContacts = "";
                    var _queryContacts = (
                                            from _contacts in this.db.MsPhoneBooks
                                            where _contacts.PhoneBookGroup == _rowGroup
                                                && _contacts.PhoneNumber.ToString().Trim() != ""
                                                && _contacts.OrganizationID == Convert.ToInt32(_prmOrgID)
                                                && _contacts.UserID.Trim().ToLower() == _UserID.Trim().ToLower()
                                            select _contacts.PhoneNumber);
                    foreach (var _rowContact in _queryContacts)
                        _tempContacts += "," + _rowContact;
                    if (_tempContacts != "") _tempContacts = _tempContacts.Substring(1);
                    _result.Add(_tempContacts + "|" + _rowGroup);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }
    }
}
