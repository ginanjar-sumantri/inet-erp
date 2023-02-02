using System;
using System.Linq;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Web;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Billing
{
    public sealed partial class ContractTemplateBL : Base
    {
        public ContractTemplateBL()
        {
        }

        #region ContractTemplateHd
        public int RowsCountContractTemplateHd(String _prmCategory, String _prmKeyword)
        {
            int _result = 0;

            string _pattern1 = "%%";

            if (_prmCategory == "TemplateName")
            {
                _pattern1 = "%" + _prmKeyword.Trim().ToLower() + "%";
            }

            _result =
                       (
                           from _radius in this.db.BILMsContractTemplateHds
                           where (SqlMethods.Like((_radius.TemplateName ?? "").Trim().ToLower(), _pattern1.Trim().ToLower()))
                           orderby _radius.TemplateName
                           select _radius.TemplateID
                       ).Count();

            return _result;
        }

        public BILMsContractTemplateHd GetSingleContractTemplateHd(String _prmContractTemplateCode)
        {
            BILMsContractTemplateHd _result = null;

            try
            {
                _result = db.BILMsContractTemplateHds.Single(_temp => _temp.TemplateID.ToString().Trim().ToLower() == _prmContractTemplateCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<BILMsContractTemplateHd> GetListContractTemplateHd(int _prmReqPage, int _prmPageSize, String _prmCategory, String _prmKeyword)
        {
            List<BILMsContractTemplateHd> _result = new List<BILMsContractTemplateHd>();

            string _pattern1 = "%%";

            if (_prmCategory == "TemplateName")
            {
                _pattern1 = "%" + _prmKeyword.Trim().ToLower() + "%";
            }

            try
            {
                var _query = (
                                 from _radius in this.db.BILMsContractTemplateHds
                                 where (SqlMethods.Like((_radius.TemplateName ?? "").Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 orderby _radius.TemplateID
                                 select new
                                 {
                                     TemplateID = _radius.TemplateID,
                                     TemplateName = _radius.TemplateName,
                                     TemplateFileName = _radius.TemplateFileName,
                                     FgActive = _radius.FgActive
                                 }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new BILMsContractTemplateHd(_row.TemplateID, _row.TemplateName, _row.TemplateFileName, _row.FgActive));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<BILMsContractTemplateHd> GetListContractTemplateForDDL()
        {
            List<BILMsContractTemplateHd> _result = new List<BILMsContractTemplateHd>();

            try
            {
                var _query = (
                                 from _radius in this.db.BILMsContractTemplateHds
                                 orderby _radius.TemplateName
                                 select new
                                 {
                                     TemplateID = _radius.TemplateID,
                                     TemplateName = _radius.TemplateName
                                 }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new BILMsContractTemplateHd(_row.TemplateID, _row.TemplateName));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public String GetTemplateNameByCode(String _prmContractTemplateCode)
        {
            String _result = "";

            try
            {
                var _query = (
                                 from _radius in this.db.BILMsContractTemplateHds
                                 where _radius.TemplateID.ToString().Trim().ToLower() == _prmContractTemplateCode.Trim().ToLower()
                                 select _radius.TemplateName
                            ).FirstOrDefault();

                _result = _query;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool DeleteMultiContractTemplateHd(String[] _prmContractTemplateCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmContractTemplateCode.Length; i++)
                {
                    BILMsContractTemplateHd _bilMsContractTemplate = this.db.BILMsContractTemplateHds.Single(_temp => _temp.TemplateID.ToString().Trim().ToLower() == _prmContractTemplateCode[i].Trim().ToLower());

                    this.db.BILMsContractTemplateHds.DeleteOnSubmit(_bilMsContractTemplate);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public String AddContractTemplateHd(BILMsContractTemplateHd _prmBILMsContractTemplate)
        {
            String _result = "";

            try
            {
                this.db.BILMsContractTemplateHds.InsertOnSubmit(_prmBILMsContractTemplate);

                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public Boolean EditContractTemplateHd(BILMsContractTemplateHd _prmBILMsContractTemplate)
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

        public int GetMaxTemplateID()
        {
            int _result = 1;

            int _count = (
                            from _counter in this.db.BILMsContractTemplateHds
                            select _counter
                         ).Count();

            if (_count > 0)
            {
                int _maxNo = (
                            from _counter in this.db.BILMsContractTemplateHds
                            select _counter.TemplateID
                         ).Max();

                _result = _maxNo + 1;
            }

            return _result;
        }

        #endregion

        #region ContractTemplateDt
        public int RowsCountContractTemplateDt(String _prmVariable)
        {
            int _result = 0;

            try
            {
                _result = this.db.BILMsContractTemplateDts.Where(_row => _row.TemplateID.ToString() == _prmVariable).Count();
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<BILMsContractTemplateDt> GetListContractTemplateDt(String _prmCode)
        {
            List<BILMsContractTemplateDt> _result = new List<BILMsContractTemplateDt>();

            try
            {
                var _query = (
                                from _contractTemDt in this.db.BILMsContractTemplateDts
                                where _contractTemDt.TemplateID.ToString() == _prmCode
                                orderby _contractTemDt.Variable ascending
                                select new
                                {
                                    TemplateID = _contractTemDt.TemplateID,
                                    Variable = _contractTemDt.Variable
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new BILMsContractTemplateDt(_row.TemplateID, _row.Variable));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool DeleteMultiContractTemplateDt(string[] _prmVariable, String _prmTemplateID)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmVariable.Length; i++)
                {
                    BILMsContractTemplateDt _contractTemDt = this.db.BILMsContractTemplateDts.Single(_temp => _temp.Variable == _prmVariable[i] && _temp.TemplateID.ToString() == _prmTemplateID);

                    this.db.BILMsContractTemplateDts.DeleteOnSubmit(_contractTemDt);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool AddContractTemplateDt(BILMsContractTemplateDt _prmContractTemplateDt)
        {
            bool _result = false;

            try
            {
                this.db.BILMsContractTemplateDts.InsertOnSubmit(_prmContractTemplateDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool EditContractTemplateDt(BILMsContractTemplateDt _prmContractTemplateDt)
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

        //public BILMsContractTemplateDt GetSingleContractTemplateDt(String _prmTransNmbr, String _prmProductCode)
        //{
        //    BILMsContractTemplateDt _result = null;

        //    try
        //    {
        //        _result = this.db.BILMsContractTemplateDts.Single(_temp => _temp.TransNmbr == _prmTransNmbr && _temp.ProductCode == _prmProductCode);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}
        #endregion

        ~ContractTemplateBL()
        {
        }
    }
}
