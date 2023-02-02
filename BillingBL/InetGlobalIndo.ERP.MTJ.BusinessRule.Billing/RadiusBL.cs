using System;
using System.Linq;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Web;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Billing
{
    public sealed partial class RadiusBL : Base
    {
        public RadiusBL()
        {
        }
        ~RadiusBL()
        {
        }

        #region Radius
        public int RowsCountBILMsRadius(String _prmCategory, String _prmKeyword)
        {
            int _result = 0;

            string _pattern1 = "%%";
            //string _pattern2 = "%%";

            if (_prmCategory == "RadiusName")
            {
                _pattern1 = "%" + _prmKeyword.Trim().ToLower() + "%";
                //_pattern2 = "%%";
            }

            _result =
                       (
                           from _radius in this.db.BILMsRadius
                           where (SqlMethods.Like((_radius.RadiusName ?? "").Trim().ToLower(), _pattern1.Trim().ToLower()))
                           orderby _radius.RadiusCode
                           select new
                           {
                               RadiusCode = _radius.RadiusCode
                           }
                       ).Count();

            return _result;
        }

        public BILMsRadius GetSingleRadius(String _prmRadiusCode)
        {
            BILMsRadius _result = null;

            try
            {
                _result = db.BILMsRadius.Single(_temp => _temp.RadiusCode.Trim().ToLower() == _prmRadiusCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<BILMsRadius> GetListRadius(int _prmReqPage, int _prmPageSize, String _prmCategory, String _prmKeyword)
        {
            List<BILMsRadius> _result = new List<BILMsRadius>();

            string _pattern1 = "%%";

            if (_prmCategory == "RadiusName")
            {
                _pattern1 = "%" + _prmKeyword.Trim().ToLower() + "%";
            }

            try
            {
                var _query = (
                                 from _radius in this.db.BILMsRadius
                                 where (SqlMethods.Like((_radius.RadiusName ?? "").Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 orderby _radius.RadiusCode
                                 select new
                                 {
                                     RadiusCode = _radius.RadiusCode,
                                     RadiusName = _radius.RadiusName,
                                     CustCode = _radius.CustCode,
                                     CustName = (
                                                    from _cust in this.db.MsCustomers
                                                    where _cust.CustCode == _radius.CustCode
                                                    select _cust.CustName
                                                ).FirstOrDefault(),
                                     RadiusIP = _radius.RadiusIP,
                                     RadiusUserName = _radius.RadiusUserName,
                                     RadiusPwd = _radius.RadiusPwd,
                                     RadiusDbName = _radius.RadiusDbName
                                 }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new BILMsRadius(_row.RadiusCode, _row.RadiusName, _row.CustCode, _row.CustName, _row.RadiusIP, _row.RadiusUserName, _row.RadiusPwd, _row.RadiusDbName));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<BILMsRadius> GetListRadiusForDDL()
        {
            List<BILMsRadius> _result = new List<BILMsRadius>();

            try
            {
                var _query = (
                                 from _radius in this.db.BILMsRadius
                                 orderby _radius.RadiusCode
                                 select new
                                 {
                                     RadiusCode = _radius.RadiusCode,
                                     RadiusName = _radius.RadiusName
                                 }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new BILMsRadius(_row.RadiusCode, _row.RadiusName));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public String GetRadiusNameByCode(String _prmRadiusCode)
        {
            String _result = "";

            try
            {
                var _query = (
                                 from _radius in this.db.BILMsRadius
                                 where _radius.RadiusCode.Trim().ToLower() == _prmRadiusCode.Trim().ToLower()
                                 select _radius.RadiusName
                            ).FirstOrDefault();

                _result = _query;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool DeleteMultiRadius(String[] _prmRadiusCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmRadiusCode.Length; i++)
                {
                    BILMsRadius _bilMsRadius = this.db.BILMsRadius.Single(_temp => _temp.RadiusCode.Trim().ToLower() == _prmRadiusCode[i].Trim().ToLower());

                    this.db.BILMsRadius.DeleteOnSubmit(_bilMsRadius);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public String Add(BILMsRadius _prmBILMsRadius)
        {
            String _result = "";

            try
            {
                this.db.BILMsRadius.InsertOnSubmit(_prmBILMsRadius);

                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public Boolean Edit(BILMsRadius _prmBILMsRadius)
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
    }
}
