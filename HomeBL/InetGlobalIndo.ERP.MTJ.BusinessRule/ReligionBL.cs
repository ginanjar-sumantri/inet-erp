using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule
{
    public sealed class ReligionBL : Base
    {
        public ReligionBL()
        {

        }

        #region Religion

        public MsReligion GetSingle(string _prmReligionCode)
        {
            MsReligion _result = null;

            try
            {
                _result = this.db.MsReligions.Single(_rlg => _rlg.ReligionCode == _prmReligionCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetReligionNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msReligion in this.db.MsReligions
                                where _msReligion.ReligionCode == _prmCode
                                select new
                                {
                                    ReligionName = _msReligion.ReligionName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.ReligionName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsReligion> GetList()
        {
            List<MsReligion> _result = new List<MsReligion>();

            try
            {
                var _query = (
                                from rlg in this.db.MsReligions
                                orderby rlg.UserDate descending
                                select new
                                {
                                    ReligionCode = rlg.ReligionCode,
                                    ReligionName = rlg.ReligionName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ReligionCode = this._string, ReligionName = this._string });

                    _result.Add(new MsReligion(_row.ReligionCode, _row.ReligionName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(MsReligion _prmMsReligion)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(MsReligion _prmMsReligion)
        {
            bool _result = false;

            try
            {
                this.db.MsReligions.InsertOnSubmit(_prmMsReligion);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmReligionCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmReligionCode.Length; i++)
                {
                    MsReligion _msReligion = this.db.MsReligions.Single(_rlg => _rlg.ReligionCode.Trim().ToLower() == _prmReligionCode[i].Trim().ToLower());

                    this.db.MsReligions.DeleteOnSubmit(_msReligion);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~ReligionBL()
        {

        }
    }
}
