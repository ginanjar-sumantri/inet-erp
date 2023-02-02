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

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class GroupByBL : Base
    {
        public GroupByBL()
        {
        }

        #region GroupBy
        public string GetGroupByNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msGroupBy in this.db.V_GroupBies
                                where _msGroupBy.GroupCode == _prmCode
                                select new
                                {
                                    GroupName = _msGroupBy.GroupName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.GroupName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public List<V_GroupBy> GetListGroupBy(String _prmGroupBy)
        {
            List<V_GroupBy> _result = new List<V_GroupBy>();

            try
            {
                var _query =
                            (
                                from _groupBy in this.db.V_GroupBies
                                where _groupBy.GroupBy == _prmGroupBy
                                orderby _groupBy.GroupName
                                select new
                                {
                                    GroupCode = _groupBy.GroupCode,
                                    GroupName = _groupBy.GroupName + " - " + _groupBy.GroupCode
                                }
                            );

                foreach (var _obj in _query)
                {
                    _result.Add(new V_GroupBy(_obj.GroupCode, _obj.GroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~GroupByBL()
        {
        }

    }
}
