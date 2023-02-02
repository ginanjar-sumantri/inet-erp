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
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.Web.UI.WebControls;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class OrganizationStructureBL : Base
    {
        private string _viewPage = "OrganizationUnitView.aspx";
        private string _codeKey = "code";
        private string _codeItem = "CodeItem";
        private string _flag = "Flag";

        public OrganizationStructureBL()
        {

        }

        //#region Department

        //public double RowsCountDept1(string _prmCategory, string _prmKeyword)
        //{
        //    double _result = 0;

        //    string _pattern1 = "%%";
        //    string _pattern2 = "%%";

        //    if (_prmCategory == "Code")
        //    {
        //        _pattern1 = "%" + _prmKeyword + "%";
        //        _pattern2 = "%%";

        //    }
        //    else if (_prmCategory == "Name")
        //    {
        //        _pattern2 = "%" + _prmKeyword + "%";
        //        _pattern1 = "%%";
        //    }

        //    var _query =
        //        (
        //            from _msDepartement1 in this.db.MsDepartment1s
        //            where (SqlMethods.Like(_msDepartement1.Dept1Code.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                   && (SqlMethods.Like(_msDepartement1.Dept1Name.Trim().ToLower(), _pattern2.Trim().ToLower()))
        //            select _msDepartement1.Dept1Code

        //        ).Count();

        //    _result = _query;
        //    return _result;
        //}

        //public MsDepartment1 GetSingleDept1(string _prmDept1Code)
        //{
        //    MsDepartment1 _result = null;

        //    try
        //    {
        //        _result = this.db.MsDepartment1s.Single(_dept1 => _dept1.Dept1Code == _prmDept1Code);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public string GetDept1NameByCode(string _prmCode)
        //{
        //    string _result = "";

        //    try
        //    {
        //        var _query = (
        //                        from _msDepartment in this.db.MsDepartment1s
        //                        where _msDepartment.Dept1Code == _prmCode
        //                        select new
        //                        {
        //                            Dept1Name = _msDepartment.Dept1Name
        //                        }
        //                      );

        //        foreach (var _obj in _query)
        //        {
        //            _result = _obj.Dept1Name;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<MsDepartment1> GetListDept1(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        //{
        //    List<MsDepartment1> _result = new List<MsDepartment1>();
        //    string _pattern1 = "%%";
        //    string _pattern2 = "%%";

        //    if (_prmCategory == "Code")
        //    {
        //        _pattern1 = "%" + _prmKeyword + "%";
        //        _pattern2 = "%%";

        //    }
        //    else if (_prmCategory == "Name")
        //    {
        //        _pattern2 = "%" + _prmKeyword + "%";
        //        _pattern1 = "%%";
        //    }
        //    try
        //    {
        //        var _query = (
        //                        from dept1 in this.db.MsDepartment1s
        //                        where (SqlMethods.Like(dept1.Dept1Code.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                                && (SqlMethods.Like(dept1.Dept1Name.Trim().ToLower(), _pattern2.Trim().ToLower()))
        //                        orderby dept1.UserDate descending
        //                        select new
        //                        {
        //                            Dept1Code = dept1.Dept1Code,
        //                            Dept1Name = dept1.Dept1Name
        //                        }
        //                    ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

        //        foreach (object _obj in _query)
        //        {
        //            var _row = _obj.Template(new { Dept1Code = this._string, Dept1Name = this._string });

        //            _result.Add(new MsDepartment1(_row.Dept1Code, _row.Dept1Name));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<MsDepartment1> GetListDept1()
        //{
        //    List<MsDepartment1> _result = new List<MsDepartment1>();

        //    try
        //    {
        //        var _query = (
        //                        from dept1 in this.db.MsDepartment1s
        //                        orderby dept1.UserDate descending
        //                        select new
        //                        {
        //                            Dept1Code = dept1.Dept1Code,
        //                            Dept1Name = dept1.Dept1Name
        //                        }
        //                    );

        //        foreach (object _obj in _query)
        //        {
        //            var _row = _obj.Template(new { Dept1Code = this._string, Dept1Name = this._string });

        //            _result.Add(new MsDepartment1(_row.Dept1Code, _row.Dept1Name));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<MsDepartment1> GetListDDLByView()
        //{
        //    List<MsDepartment1> _result = new List<MsDepartment1>();

        //    try
        //    {
        //        var _query = (
        //                        from _msDepartment in this.db.V_MsDepartments
        //                        orderby _msDepartment.Dept_Name ascending
        //                        select new
        //                        {
        //                            Dept_Code = _msDepartment.Dept_Code,
        //                            Dept_Name = _msDepartment.Dept_Name
        //                        }
        //                    );

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new MsDepartment1(_row.Dept_Code, _row.Dept_Name));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool EditDept1(MsDepartment1 _prmMsDepartment1)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool AddDept1(MsDepartment1 _prmMsDepartment1)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        this.db.MsDepartment1s.InsertOnSubmit(_prmMsDepartment1);
        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool DeleteMultiDept1(string[] _prmDept1Code)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        for (int i = 0; i < _prmDept1Code.Length; i++)
        //        {
        //            MsDepartment1 _msDepartment1 = this.db.MsDepartment1s.Single(_dept1 => _dept1.Dept1Code.Trim().ToLower() == _prmDept1Code[i].Trim().ToLower());

        //            this.db.MsDepartment1s.DeleteOnSubmit(_msDepartment1);
        //        }

        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //#endregion

        //#region Divison

        //public double RowsCountDept2(string _prmCategory, string _prmKeyword)
        //{
        //    double _result = 0;

        //    string _pattern1 = "%%";
        //    string _pattern2 = "%%";

        //    if (_prmCategory == "Code")
        //    {
        //        _pattern1 = "%" + _prmKeyword + "%";
        //        _pattern2 = "%%";

        //    }
        //    else if (_prmCategory == "Name")
        //    {
        //        _pattern2 = "%" + _prmKeyword + "%";
        //        _pattern1 = "%%";
        //    }

        //    var _query =
        //        (
        //        from _msDepartement2 in this.db.MsDepartment2s
        //        where (SqlMethods.Like(_msDepartement2.Dept2Code.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                   && (SqlMethods.Like(_msDepartement2.Dept2Name.Trim().ToLower(), _pattern2.Trim().ToLower()))
        //        select _msDepartement2.Dept2Code

        //        ).Count();

        //    _result = _query;
        //    return _result;
        //}

        //public MsDepartment2 GetSingleDept2(string _prmDept2Code)
        //{
        //    MsDepartment2 _result = null;

        //    try
        //    {
        //        _result = this.db.MsDepartment2s.Single(_dept2 => _dept2.Dept2Code == _prmDept2Code);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public string GetDept2NameByCode(string _prmCode)
        //{
        //    string _result = "";

        //    try
        //    {
        //        var _query = (
        //                        from _msDivision in this.db.MsDepartment2s
        //                        where _msDivision.Dept2Code == _prmCode
        //                        select new
        //                        {
        //                            Dept2Name = _msDivision.Dept2Name
        //                        }
        //                      );

        //        foreach (var _obj in _query)
        //        {
        //            _result = _obj.Dept2Name;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<MsDepartment2> GetListDept2(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        //{
        //    List<MsDepartment2> _result = new List<MsDepartment2>();

        //    string _pattern1 = "%%";
        //    string _pattern2 = "%%";

        //    if (_prmCategory == "Code")
        //    {
        //        _pattern1 = "%" + _prmKeyword + "%";
        //        _pattern2 = "%%";

        //    }
        //    else if (_prmCategory == "Name")
        //    {
        //        _pattern2 = "%" + _prmKeyword + "%";
        //        _pattern1 = "%%";
        //    }

        //    try
        //    {
        //        var _query = (
        //                        from dept2 in this.db.MsDepartment2s
        //                        join dept1 in this.db.MsDepartment1s
        //                            on dept2.Department1 equals dept1.Dept1Code
        //                        where (SqlMethods.Like(dept2.Dept2Code.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                           && (SqlMethods.Like(dept2.Dept2Name.Trim().ToLower(), _pattern2.Trim().ToLower()))
        //                        orderby dept2.UserDate descending
        //                        select new
        //                        {
        //                            Dept2Code = dept2.Dept2Code,
        //                            Dept2Name = dept2.Dept2Name,
        //                            Department1 = dept1.Dept1Name

        //                        }
        //                    ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

        //        foreach (object _obj in _query)
        //        {
        //            var _row = _obj.Template(new { Dept2Code = this._string, Dept2Name = this._string, Department1 = this._string });

        //            _result.Add(new MsDepartment2(_row.Dept2Code, _row.Dept2Name, _row.Department1));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<MsDepartment2> GetListDept2()
        //{
        //    List<MsDepartment2> _result = new List<MsDepartment2>();

        //    try
        //    {
        //        var _query = (
        //                        from dept2 in this.db.MsDepartment2s
        //                        join dept1 in this.db.MsDepartment1s
        //                            on dept2.Department1 equals dept1.Dept1Code
        //                        orderby dept2.UserDate descending
        //                        select new
        //                        {
        //                            Dept2Code = dept2.Dept2Code,
        //                            Dept2Name = dept2.Dept2Name,
        //                            Department1 = dept1.Dept1Name

        //                        }
        //                    );

        //        foreach (object _obj in _query)
        //        {
        //            var _row = _obj.Template(new { Dept2Code = this._string, Dept2Name = this._string, Department1 = this._string });

        //            _result.Add(new MsDepartment2(_row.Dept2Code, _row.Dept2Name, _row.Department1));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool EditDept2(MsDepartment2 _prmMsDepartment2)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool AddDept2(MsDepartment2 _prmMsDepartment2)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        this.db.MsDepartment2s.InsertOnSubmit(_prmMsDepartment2);
        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool DeleteMultiDept2(string[] _prmDept2Code)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        for (int i = 0; i < _prmDept2Code.Length; i++)
        //        {
        //            MsDepartment2 _msDepartment2 = this.db.MsDepartment2s.Single(_dept2 => _dept2.Dept2Code.Trim().ToLower() == _prmDept2Code[i].Trim().ToLower());

        //            this.db.MsDepartment2s.DeleteOnSubmit(_msDepartment2);
        //        }

        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<MsDepartment1> GetListForDDLDept1()
        //{
        //    List<MsDepartment1> _result = new List<MsDepartment1>();

        //    try
        //    {
        //        var _query =
        //                    (
        //                        from _dept1 in this.db.MsDepartment1s
        //                        orderby _dept1.Dept1Name ascending
        //                        select new
        //                        {
        //                            Dept1Code = _dept1.Dept1Code,
        //                            Dept1Name = _dept1.Dept1Name
        //                        }
        //                    );

        //        foreach (object _obj in _query)
        //        {
        //            var _row = _obj.Template(new { Dept1Code = this._string, Dept1Name = this._string });

        //            _result.Add(new MsDepartment1(_row.Dept1Code, _row.Dept1Name));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}
        //#endregion

        //#region SubDivision

        //public double RowsCountDept3(string _prmCategory, string _prmKeyword)
        //{
        //    double _result = 0;

        //    string _pattern1 = "%%";
        //    string _pattern2 = "%%";

        //    if (_prmCategory == "Code")
        //    {
        //        _pattern1 = "%" + _prmKeyword + "%";
        //        _pattern2 = "%%";

        //    }
        //    else if (_prmCategory == "Name")
        //    {
        //        _pattern2 = "%" + _prmKeyword + "%";
        //        _pattern1 = "%%";
        //    }

        //    var _query =
        //        (
        //            from _msDepartement3 in this.db.MsDepartment3s
        //            where (SqlMethods.Like(_msDepartement3.Dept3Code.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                   && (SqlMethods.Like(_msDepartement3.Dept3Name.Trim().ToLower(), _pattern2.Trim().ToLower()))
        //            select _msDepartement3.Dept3Code

        //        ).Count();

        //    _result = _query;
        //    return _result;
        //}

        //public MsDepartment3 GetSingleDept3(string _prmDept3Code)
        //{
        //    MsDepartment3 _result = null;

        //    try
        //    {
        //        _result = this.db.MsDepartment3s.Single(_dept3 => _dept3.Dept3Code == _prmDept3Code);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public string GetDept3NameByCode(string _prmCode)
        //{
        //    string _result = "";

        //    try
        //    {
        //        var _query = (
        //                        from _msSubDivision in this.db.MsDepartment3s
        //                        where _msSubDivision.Dept3Code == _prmCode
        //                        select new
        //                        {
        //                            Dept3Name = _msSubDivision.Dept3Name
        //                        }
        //                      );

        //        foreach (var _obj in _query)
        //        {
        //            _result = _obj.Dept3Name;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<MsDepartment3> GetListDept3(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        //{
        //    List<MsDepartment3> _result = new List<MsDepartment3>();
        //    string _pattern1 = "%%";
        //    string _pattern2 = "%%";

        //    if (_prmCategory == "Code")
        //    {
        //        _pattern1 = "%" + _prmKeyword + "%";
        //        _pattern2 = "%%";

        //    }
        //    else if (_prmCategory == "Name")
        //    {
        //        _pattern2 = "%" + _prmKeyword + "%";
        //        _pattern1 = "%%";
        //    }
        //    try
        //    {
        //        var _query = (
        //                        from dept3 in this.db.MsDepartment3s
        //                        join dept2 in this.db.MsDepartment2s
        //                            on dept3.Department2 equals dept2.Dept2Code
        //                        where (SqlMethods.Like(dept3.Dept3Code.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                                && (SqlMethods.Like(dept3.Dept3Name.Trim().ToLower(), _pattern2.Trim().ToLower()))
        //                        orderby dept3.UserDate descending
        //                        select new
        //                        {
        //                            Dept3Code = dept3.Dept3Code,
        //                            Dept3Name = dept3.Dept3Name,
        //                            Department2 = dept2.Dept2Name

        //                        }
        //                    ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

        //        foreach (object _obj in _query)
        //        {
        //            var _row = _obj.Template(new { Dept3Code = this._string, Dept3Name = this._string, Department2 = this._string });

        //            _result.Add(new MsDepartment3(_row.Dept3Code, _row.Dept3Name, _row.Department2));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<MsDepartment3> GetListDept3()
        //{
        //    List<MsDepartment3> _result = new List<MsDepartment3>();

        //    try
        //    {
        //        var _query = (
        //                        from dept3 in this.db.MsDepartment3s
        //                        join dept2 in this.db.MsDepartment2s
        //                            on dept3.Department2 equals dept2.Dept2Code
        //                        orderby dept3.UserDate descending
        //                        select new
        //                        {
        //                            Dept3Code = dept3.Dept3Code,
        //                            Dept3Name = dept3.Dept3Name,
        //                            Department2 = dept2.Dept2Name
        //                        }
        //                    );

        //        foreach (object _obj in _query)
        //        {
        //            var _row = _obj.Template(new { Dept3Code = this._string, Dept3Name = this._string, Department2 = this._string });

        //            _result.Add(new MsDepartment3(_row.Dept3Code, _row.Dept3Name, _row.Department2));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool EditDept3(MsDepartment3 _prmMsDepartment3)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool AddDept3(MsDepartment3 _prmMsDepartment3)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        this.db.MsDepartment3s.InsertOnSubmit(_prmMsDepartment3);
        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool DeleteMultiDept3(string[] _prmDept3Code)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        for (int i = 0; i < _prmDept3Code.Length; i++)
        //        {
        //            MsDepartment3 _msDepartment3 = this.db.MsDepartment3s.Single(_dept3 => _dept3.Dept3Code.Trim().ToLower() == _prmDept3Code[i].Trim().ToLower());

        //            this.db.MsDepartment3s.DeleteOnSubmit(_msDepartment3);
        //        }

        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<MsDepartment2> GetListForDDLDept2()
        //{
        //    List<MsDepartment2> _result = new List<MsDepartment2>();

        //    try
        //    {
        //        var _query =
        //                    (
        //                        from _dept2 in this.db.MsDepartment2s
        //                        orderby _dept2.Dept2Name ascending
        //                        select new
        //                        {
        //                            Dept2Code = _dept2.Dept2Code,
        //                            Dept2Name = _dept2.Dept2Name
        //                        }
        //                    );

        //        foreach (object _obj in _query)
        //        {
        //            var _row = _obj.Template(new { Dept2Code = this._string, Dept2Name = this._string });

        //            _result.Add(new MsDepartment2(_row.Dept2Code, _row.Dept2Name));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //#endregion

        #region Master_OrganizationUnit

        public double RowsCountOrganizationUnit(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query =
                            (
                                from _masterOrgUnit in this.db.Master_OrganizationUnits
                                where (SqlMethods.Like(_masterOrgUnit.OrgUnit.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_masterOrgUnit.Description.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select _masterOrgUnit.OrgUnit
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public double RowsCountOrganizationUnit()
        {
            double _result = 0;

            try
            {
                var _query =
                            (
                                from _masterOrgUnit in this.db.Master_OrganizationUnits
                                select _masterOrgUnit.OrgUnit
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Master_OrganizationUnit GetSingleOrganizationUnit(string _prmOrgUnit)
        {
            Master_OrganizationUnit _result = null;

            try
            {
                _result = this.db.Master_OrganizationUnits.Single(_msOrgUnit => _msOrgUnit.OrgUnit == _prmOrgUnit);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetDescriptionByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msOrgUnit in this.db.Master_OrganizationUnits
                                where _msOrgUnit.OrgUnit == _prmCode
                                select new
                                {
                                    Description = _msOrgUnit.Description
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.Description;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_OrganizationUnit> GetListOrganizationUnit(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Master_OrganizationUnit> _result = new List<Master_OrganizationUnit>();
            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }
            try
            {
                var _query = (
                                from _msOrgUnit in this.db.Master_OrganizationUnits
                                where _msOrgUnit.OrgUnit != ApplicationConfig.RootOrganization
                                    && (SqlMethods.Like(_msOrgUnit.OrgUnit.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msOrgUnit.Description.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msOrgUnit.ParentOrgUnit, _msOrgUnit.Description descending
                                select new
                                {
                                    OrgUnit = _msOrgUnit.OrgUnit,
                                    Description = _msOrgUnit.Description,
                                    ParentOrgUnit = _msOrgUnit.ParentOrgUnit,
                                    Address = _msOrgUnit.Address,
                                    ContactInfo = _msOrgUnit.ContactInfo,
                                    Note = _msOrgUnit.Note,
                                    FgActive = _msOrgUnit.FgActive
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_OrganizationUnit(_row.OrgUnit, _row.Description, _row.ParentOrgUnit, _row.Address, _row.ContactInfo, _row.Note, _row.FgActive));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_OrganizationUnit> GetListOrganizationUnit(int _prmReqPage, int _prmPageSize)
        {
            List<Master_OrganizationUnit> _result = new List<Master_OrganizationUnit>();

            try
            {
                var _query = (
                                from _msOrgUnit in this.db.Master_OrganizationUnits
                                where _msOrgUnit.OrgUnit != ApplicationConfig.RootOrganization
                                orderby _msOrgUnit.ParentOrgUnit, _msOrgUnit.Description descending
                                select new
                                {
                                    OrgUnit = _msOrgUnit.OrgUnit,
                                    Description = _msOrgUnit.Description,
                                    ParentOrgUnit = _msOrgUnit.ParentOrgUnit,
                                    Address = _msOrgUnit.Address,
                                    ContactInfo = _msOrgUnit.ContactInfo,
                                    Note = _msOrgUnit.Note,
                                    FgActive = _msOrgUnit.FgActive
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_OrganizationUnit(_row.OrgUnit, _row.Description, _row.ParentOrgUnit, _row.Address, _row.ContactInfo, _row.Note, _row.FgActive));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_OrganizationUnit> GetListOrganizationUnit()
        {
            List<Master_OrganizationUnit> _result = new List<Master_OrganizationUnit>();

            try
            {
                var _query = (
                                from _msOrgUnit in this.db.Master_OrganizationUnits
                                where _msOrgUnit.OrgUnit != ApplicationConfig.RootOrganization
                                orderby _msOrgUnit.EditDate descending
                                select new
                                {
                                    OrgUnit = _msOrgUnit.OrgUnit,
                                    Description = _msOrgUnit.Description,
                                    ParentOrgUnit = _msOrgUnit.ParentOrgUnit,
                                    Address = _msOrgUnit.Address,
                                    ContactInfo = _msOrgUnit.ContactInfo,
                                    Note = _msOrgUnit.Note,
                                    FgActive = _msOrgUnit.FgActive
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_OrganizationUnit(_row.OrgUnit, _row.Description, _row.ParentOrgUnit, _row.Address, _row.ContactInfo, _row.Note, _row.FgActive));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetListOrganizationUnitInTerminationRequest(string _prmEmpNumbRequest, string _prmEmpNumbUser)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _msOrgUnit in this.db.Master_OrgUnit_MsEmployees
                                where _msOrgUnit.EmpNumb == _prmEmpNumbRequest
                                select new
                                {
                                    OrgUnit = _msOrgUnit.OrgUnit
                                }
                            );

                foreach (var _row in _query)
                {
                    var _query2 = (
                                      from _master_OrgUnit_MsEmployees in this.db.Master_OrgUnit_MsEmployees
                                      where _master_OrgUnit_MsEmployees.EmpNumb == _prmEmpNumbUser
                                            && _master_OrgUnit_MsEmployees.OrgUnit == _row.OrgUnit
                                      select new
                                      {
                                          EmpNumb = _master_OrgUnit_MsEmployees.EmpNumb,
                                          IsPIC = _master_OrgUnit_MsEmployees.IsPIC
                                      }
                                    ).FirstOrDefault();

                    if (_query2 != null)
                    {
                        if (_query2.IsPIC == true)
                        {
                            _result = _query2.EmpNumb;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public String GetListOrganizationUnitInTerminationRequestForUser(string _prmEmpNumb, List<string> _prmOrgUnit)
        //{
        //    String _result = "";

        //    try
        //    {



        //        foreach (var _item in _prmOrgUnit)
        //        {
        //            if (_query2 == _item)
        //            {
        //                _result = _query;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<Master_OrganizationUnit> GetListOrganizationUnitForDDL()
        //{
        //    List<Master_OrganizationUnit> _result = new List<Master_OrganizationUnit>();

        //    try
        //    {
        //        var _query = (
        //                        from _msOrgUnit in this.db.Master_OrganizationUnits
        //                        where _msOrgUnit.ParentOrgUnit != ApplicationConfig.RootOrganization
        //                            && _msOrgUnit.FgActive == OrganizationUnitDataMapper.ActiveStatus(OrganizationUnitActiveStatus.Active)
        //                        orderby _msOrgUnit.Description ascending
        //                        select new
        //                        {
        //                            OrgUnit = _msOrgUnit.OrgUnit,
        //                            Description = _msOrgUnit.Description
        //                        }
        //                    );

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new Master_OrganizationUnit(_row.OrgUnit, _row.Description));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public double RowsCountOrgUnitReport(string _prmParent)
        {
            double _result = 0;

            string _pattern1 = "%%";

            if (_prmParent != "")
            {
                _pattern1 = "%" + _prmParent + "%";
            }

            var _query =
                        (
                            from _msOrgUnit in this.db.Master_OrganizationUnits
                            where //_msOrgUnit.ParentOrgUnit != ApplicationConfig.RootOrganization &&
                               (SqlMethods.Like(_msOrgUnit.ParentOrgUnit.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && _msOrgUnit.FgActive == OrganizationUnitDataMapper.ActiveStatus(OrganizationUnitActiveStatus.Active)
                            select _msOrgUnit.OrgUnit
                        ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountOrgUnitReport()
        {
            double _result = 0;

            var _query =
                        (
                            from _msOrgUnit in this.db.Master_OrganizationUnits
                            where //_msOrgUnit.ParentOrgUnit != ApplicationConfig.RootOrganization &&
                            _msOrgUnit.FgActive == OrganizationUnitDataMapper.ActiveStatus(OrganizationUnitActiveStatus.Active)
                            select _msOrgUnit.OrgUnit
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<Master_OrganizationUnit> GetListOrganizationUnitForDDL(string _prmParent, int _prmReqPage, int _prmPageSize)
        {
            List<Master_OrganizationUnit> _result = new List<Master_OrganizationUnit>();

            string _pattern1 = "%%";

            if (_prmParent != "")
            {
                _pattern1 = "%" + _prmParent + "%";
            }

            try
            {
                var _query = (
                                from _msOrgUnit in this.db.Master_OrganizationUnits
                                where _msOrgUnit.ParentOrgUnit != ApplicationConfig.RootOrganization
                                    && (SqlMethods.Like(_msOrgUnit.ParentOrgUnit.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && _msOrgUnit.FgActive == OrganizationUnitDataMapper.ActiveStatus(OrganizationUnitActiveStatus.Active)
                                orderby _msOrgUnit.Description ascending
                                select new
                                {
                                    OrgUnit = _msOrgUnit.OrgUnit,
                                    Description = _msOrgUnit.Description
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_OrganizationUnit(_row.OrgUnit, _row.Description));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_OrganizationUnit> GetListOrganizationUnitForDDL(int _prmReqPage, int _prmPageSize)
        {
            List<Master_OrganizationUnit> _result = new List<Master_OrganizationUnit>();

            try
            {
                var _query = (
                                from _msOrgUnit in this.db.Master_OrganizationUnits
                                where _msOrgUnit.FgActive == OrganizationUnitDataMapper.ActiveStatus(OrganizationUnitActiveStatus.Active)
                                orderby _msOrgUnit.Description ascending
                                select new
                                {
                                    OrgUnit = _msOrgUnit.OrgUnit,
                                    Description = _msOrgUnit.Description
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_OrganizationUnit(_row.OrgUnit, _row.Description));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_OrganizationUnit> GetListOrganizationUnitForDDL()
        {
            List<Master_OrganizationUnit> _result = new List<Master_OrganizationUnit>();

            try
            {
                var _query = (
                                from _msOrgUnit in this.db.Master_OrganizationUnits
                                //where (
                                //        from _msOrgUnit2 in this.db.Master_OrganizationUnits
                                //        select _msOrgUnit2.ParentOrgUnit
                                //      ).Contains(_msOrgUnit.OrgUnit)
                                //    && 
                                where _msOrgUnit.FgActive == OrganizationUnitDataMapper.ActiveStatus(OrganizationUnitActiveStatus.Active)
                                orderby _msOrgUnit.Description ascending
                                select new
                                {
                                    OrgUnit = _msOrgUnit.OrgUnit,
                                    Description = _msOrgUnit.Description
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_OrganizationUnit(_row.OrgUnit, _row.Description));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditOrganizationUnit(Master_OrganizationUnit _prmMaster_OrganizationUnit)
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

        public bool AddOrganizationUnit(Master_OrganizationUnit _prmMaster_OrganizationUnit)
        {
            bool _result = false;

            try
            {
                this.db.Master_OrganizationUnits.InsertOnSubmit(_prmMaster_OrganizationUnit);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiOrganizationUnit(string[] _prmOrgUnit)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmOrgUnit.Length; i++)
                {
                    Master_OrganizationUnit _msOrgUnit = this.db.Master_OrganizationUnits.Single(_temp => _temp.OrgUnit.Trim().ToLower() == _prmOrgUnit[i].Trim().ToLower());

                    var _query = (from _detail in this.db.Master_OrgUnit_MsEmployees
                                  where _detail.OrgUnit.Trim().ToLower() == _prmOrgUnit[i].Trim().ToLower()
                                  select _detail);

                    this.db.Master_OrgUnit_MsEmployees.DeleteAllOnSubmit(_query);

                    this.db.Master_OrganizationUnits.DeleteOnSubmit(_msOrgUnit);
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

        public TreeNode RenderNode(string _prmParentID, string _prmTitle, TreeNode _prmTreeNode)
        {
            TreeNode _result;

            if (_prmTreeNode == null)
            {
                _result = new TreeNode();
                _result.Text = _prmTitle;
                _result.Expanded = true;
            }
            else
            {
                _result = _prmTreeNode;
            }

            var _nodeQuery = from _node in this.db.Master_OrganizationUnits
                             where _node.OrgUnit != ApplicationConfig.RootOrganization
                                && (_node.ParentOrgUnit == _prmParentID)
                             orderby _node.OrgUnit, _node.ParentOrgUnit ascending
                             select _node;

            foreach (Master_OrganizationUnit _nodeRow in _nodeQuery)
            {
                TreeNode _treeNode = new TreeNode(_nodeRow.Description, _nodeRow.OrgUnit, "", this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_nodeRow.OrgUnit, ApplicationConfig.EncryptionKey)), "_self");
                _treeNode.Expanded = false;

                _result.ChildNodes.Add(this.RenderNode(_nodeRow.OrgUnit, _prmTitle, _treeNode));
            }

            return _result;
        }

        public List<Master_OrganizationUnit> GetListOrganizationUnitForChackBox(int _prmReqPage, int _prmPageSize)
        {
            List<Master_OrganizationUnit> _result = new List<Master_OrganizationUnit>();

            try
            {
                var _query = (
                                from _msOrgUnit in this.db.Master_OrganizationUnits
                                where _msOrgUnit.FgActive == OrganizationUnitDataMapper.ActiveStatus(OrganizationUnitActiveStatus.Active)
                                && _msOrgUnit.OrgUnit != "0"
                                orderby _msOrgUnit.Description ascending
                                select new
                                {
                                    OrgUnit = _msOrgUnit.OrgUnit,
                                    Description = _msOrgUnit.Description
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize); 

                foreach (var _row in _query)
                {
                    _result.Add(new Master_OrganizationUnit(_row.OrgUnit, _row.Description));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region Person In Charge

        public double RowsCountEmpOrgUnitForEmployee(string _prmEmpNumb)
        {
            double _result = 0;

            var _query =
                (
                   from _msEmpOrgunit in this.db.Master_OrgUnit_MsEmployees
                   where _msEmpOrgunit.EmpNumb == _prmEmpNumb
                   select _msEmpOrgunit.EmpNumb
                 ).Count();

            _result = _query;
            return _result;

        }

        public double RowsCountEmpOrgUnitForOrgUnit(string _prmOrgUnit)
        {
            double _result = 0;

            var _query =
                (
                   from _msEmpOrgUnit in this.db.Master_OrgUnit_MsEmployees
                   where _msEmpOrgUnit.OrgUnit == _prmOrgUnit
                   select _msEmpOrgUnit.EmpNumb
                 ).Count();

            _result = _query;

            return _result;

        }

        public Master_OrgUnit_MsEmployee GetSingleEmpOrgUnit(string _prmEmpNumb, string _prmOrgUnit)
        {
            Master_OrgUnit_MsEmployee _result = null;

            try
            {
                _result = this.db.Master_OrgUnit_MsEmployees.Single(_temp => _temp.EmpNumb == _prmEmpNumb && _temp.OrgUnit == _prmOrgUnit);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_OrgUnit_MsEmployee> GetListEmpOrgUnitForEmployee(int _prmReqPage, int _prmPageSize, string _prmEmpNumb)
        {
            List<Master_OrgUnit_MsEmployee> _result = new List<Master_OrgUnit_MsEmployee>();

            try
            {
                var _query = (
                                from _msEmpOrgUnit in this.db.Master_OrgUnit_MsEmployees
                                join _msOrgUnit in this.db.Master_OrganizationUnits
                                    on _msEmpOrgUnit.OrgUnit equals _msOrgUnit.OrgUnit
                                join _msEmployee in this.db.MsEmployees
                                    on _msEmpOrgUnit.EmpNumb equals _msEmployee.EmpNumb
                                where _msEmpOrgUnit.EmpNumb == _prmEmpNumb
                                orderby _msEmpOrgUnit.EditDate descending
                                select new
                                {
                                    OrgUnit = _msEmpOrgUnit.OrgUnit,
                                    Description = _msOrgUnit.Description,
                                    EmpNumb = _msEmpOrgUnit.EmpNumb,
                                    EmpName = _msEmployee.EmpName,
                                    IsPIC = _msEmpOrgUnit.IsPIC
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_OrgUnit_MsEmployee(_row.OrgUnit, _row.Description, _row.EmpNumb, _row.EmpName, _row.IsPIC));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_OrgUnit_MsEmployee> GetListEmpOrgUnitForOrgUnit(int _prmReqPage, int _prmPageSize, string _prmorgUnit)
        {
            List<Master_OrgUnit_MsEmployee> _result = new List<Master_OrgUnit_MsEmployee>();

            try
            {
                var _query = (
                                from _msEmpOrgUnit in this.db.Master_OrgUnit_MsEmployees
                                join _msOrgUnit in this.db.Master_OrganizationUnits
                                    on _msEmpOrgUnit.OrgUnit equals _msOrgUnit.OrgUnit
                                join _msEmployee in this.db.MsEmployees
                                    on _msEmpOrgUnit.EmpNumb equals _msEmployee.EmpNumb
                                where _msEmpOrgUnit.OrgUnit == _prmorgUnit
                                orderby _msEmpOrgUnit.EditDate descending
                                select new
                                {
                                    OrgUnit = _msEmpOrgUnit.OrgUnit,
                                    Description = _msOrgUnit.Description,
                                    EmpNumb = _msEmpOrgUnit.EmpNumb,
                                    EmpName = _msEmployee.EmpName,
                                    IsPIC = _msEmpOrgUnit.IsPIC
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_OrgUnit_MsEmployee(_row.OrgUnit, _row.Description, _row.EmpNumb, _row.EmpName, _row.IsPIC));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditEmpOrgUnit(Master_OrgUnit_MsEmployee _prmEmpOrgUnit)
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

        public bool AddEmpOrgUnit(Master_OrgUnit_MsEmployee _prmEmpOrgUnit)
        {
            bool _result = false;

            try
            {
                if (IsOrganizationUnitExist(_prmEmpOrgUnit.EmpNumb, _prmEmpOrgUnit.OrgUnit) == false)
                {
                    this.db.Master_OrgUnit_MsEmployees.InsertOnSubmit(_prmEmpOrgUnit);
                    this.db.SubmitChanges();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiEmpOrgUnitForEmployee(string _prmEmpNmbr, string[] _prmOrgUnit)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmOrgUnit.Length; i++)
                {
                    Master_OrgUnit_MsEmployee _msEmpOrg = this.db.Master_OrgUnit_MsEmployees.Single(_temp => _temp.OrgUnit == _prmOrgUnit[i] && _temp.EmpNumb == _prmEmpNmbr);

                    this.db.Master_OrgUnit_MsEmployees.DeleteOnSubmit(_msEmpOrg);
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

        public bool DeleteMultiEmpOrgUnitFororgUnit(string _prmOrgUnit, string[] _prmEmpNmbr)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmEmpNmbr.Length; i++)
                {
                    Master_OrgUnit_MsEmployee _msEmpOrg = this.db.Master_OrgUnit_MsEmployees.Single(_temp => _temp.OrgUnit == _prmOrgUnit && _temp.EmpNumb == _prmEmpNmbr[i]);

                    this.db.Master_OrgUnit_MsEmployees.DeleteOnSubmit(_msEmpOrg);
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

        private bool IsOrganizationUnitExist(String _prmEmpNumb, String _prmEmpOrgUnit)
        {
            bool _result = false;

            try
            {
                var _query = from _orgUnit in this.db.Master_OrgUnit_MsEmployees
                             where _orgUnit.EmpNumb == _prmEmpNumb && _orgUnit.OrgUnit == _prmEmpOrgUnit
                             select new
                             {
                                 _orgUnit.OrgUnit
                             };

                if (_query.Count() > 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~OrganizationStructureBL()
        {

        }
    }
}
