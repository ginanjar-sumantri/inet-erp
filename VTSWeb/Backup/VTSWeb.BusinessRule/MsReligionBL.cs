using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Data;
using VTSWeb.Database;
using VTSWeb.SystemConfig;

namespace VTSWeb.BusinessRule
{
    public sealed class MsReligionBL : Base
    {

        public MsReligionBL()
        {
        }
        ~MsReligionBL()
        {
        }

        #region Religion
        public int RowsCount
        {
            get
            {
                return this.db.MsReligions.Count();
            }
        }

        public List<MsReligion> GetList(int _prmReqPage, int _prmPageSize)
        {
            List<MsReligion> _result = new List<MsReligion>();

            try
            {
                var _query = (
                                from _msReligion in this.db.MsReligions
                                orderby _msReligion.ReligionCode ascending
                                select new
                                {
                                    ReligionCode = _msReligion.ReligionCode,
                                    ReligionName = _msReligion.ReligionName,

                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsReligion(_row.ReligionCode, _row.ReligionName));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public List<MsReligion> GetList()
        {
            List<MsReligion> _result = new List<MsReligion>();

            try
            {
                var _query = (
                                from _msReligion in this.db.MsReligions
                                orderby _msReligion.ReligionCode ascending
                                select new
                                {
                                    ReligionCode = _msReligion.ReligionCode,
                                    ReligionName = _msReligion.ReligionName,
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsReligion(_row.ReligionCode, _row.ReligionName));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public MsReligion GetSingle(String _prmCode)
        {
            MsReligion _result = null;

            try
            {
                _result = this.db.MsReligions.Single(_temp => _temp.ReligionCode == _prmCode);
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public String GetReligionNameByCode(String _prmCode)
        {
            String _result = "";

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
            catch (Exception)
            {
            }

            return _result;
        }

        public bool DeleteMulti(String[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MsReligion _msReligion = this.db.MsReligions.Single(_temp => _temp.ReligionCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsReligions.DeleteOnSubmit(_msReligion);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
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
            catch (Exception)
            {
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
            catch (Exception)
            {
            }

            return _result;
        }
        public List<MsReligion> GetReligonForDDL()
        {
            List<MsReligion> _result = new List<MsReligion>();

            try
            {
                var _query = (
                                from _msReligion in this.db.MsReligions
                                //where _msRegional.RegionalCode == _prmCode
                                select new
                                {
                                    ReligionCode = _msReligion.ReligionCode,
                                    ReligionName = _msReligion.ReligionName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsReligion(_row.ReligionCode, _row.ReligionName));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        #endregion


        }
    }
}
