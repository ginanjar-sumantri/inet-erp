using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;

namespace SMSLibrary
{
    public class PackageBL : SMSLibBase
    {
        public PackageBL() { }

        public void GetSingle(String _prmPackageName)
        {
            MSPackage _msPackage = this.db.MSPackages.Single(a => a.PackageName == _prmPackageName);
        }

        public int GetSMSPerDayByCode(String _prmPackageName)
        {
            int _result = 0;
            var _query = (
                            from _package in this.db.MSPackages
                            where _package.PackageName == _prmPackageName
                            select _package.SMSPerDay
                         );

            if (_query.Count() > 0)
            {
                _result = Convert.ToInt32(_query.FirstOrDefault().Value);
            }

            return _result;
        }

        ~PackageBL() { }

        public Double RowsCountPackage(String _prmCategory, String _prmKeyword)
        {
            Double _result = 0;
            try
            {
                String _pattern1 = "%%", _pattern2 = "%%";

                if (_prmCategory == "PackageName")
                {
                    _pattern1 = "%" + _prmKeyword + "%";
                }
                else if (_prmCategory == "Description")
                {
                    _pattern2 = "%" + _prmKeyword + "%" ;
                }

                _result = (
                           from _list in this.db.MSPackages
                           where
                                (SqlMethods.Like((_list.PackageName ?? "").Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_list.Description ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                           select _list
                       ).Count();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public List<MSPackage> getListPackage(int _prmReqPage, int _prmPageSize, String _prmCategory, String _prmKeyword)
        {
            List<MSPackage> _result = new List<MSPackage>();

            String _pattern1 = "%%", _pattern2 = "%%";

            if (_prmCategory == "PackageName")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }
            else if (_prmCategory == "Description")
            {
                _pattern2 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _list in this.db.MSPackages
                                where (SqlMethods.Like((_list.PackageName ?? "").Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_list.Description ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select _list
                            ).Skip(_prmPageSize * _prmReqPage).Take(_prmPageSize);
                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Boolean isPackageExist(String _prmPackageName)
        {
            Boolean _result = false ;
            var _qry = (
                    from _msPackage in this.db.MSPackages 
                    where _msPackage.PackageName == _prmPackageName
                    select _msPackage
                );
            if (_qry.Count() > 0)
                _result = true;
            return _result;
        }

        public Boolean AddPackage(MSPackage _newData)
        {
            Boolean _result = false;
            try
            {
                this.db.MSPackages.InsertOnSubmit(_newData);
                this.db.SubmitChanges();
                _result = true ;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public Boolean EditSubmit() {
            Boolean _result = false;
            try
            {
                this.db.SubmitChanges();
                _result = true ;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public MSPackage getSinglePackage(String _prmPackageName)
        {
            MSPackage _selectedDate = null;
            try
            {
                _selectedDate = this.db.MSPackages.Single(a => a.PackageName == _prmPackageName);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _selectedDate;
        }

        public Boolean DeleteMultiPackage(String[] _tempSplit)
        {
            Boolean _result = false;
            try
            {
                foreach (String _packageToDelete in _tempSplit) { 
                    var _qry = (
                            from _msUser in this.db.MsUsers
                            where _msUser.PackageName == _packageToDelete 
                            select _msUser 
                        );
                    if (_qry.Count() > 0 ) return false ;
                    this.db.MSPackages.DeleteOnSubmit(this.db.MSPackages.Single(a => a.PackageName == _packageToDelete));
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
    }
}
