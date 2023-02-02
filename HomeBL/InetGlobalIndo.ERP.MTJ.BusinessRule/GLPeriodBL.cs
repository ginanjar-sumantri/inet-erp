using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule
{
    public sealed class GLPeriodBL : Base
    {
        public GLPeriodBL()
        {

        }

        public int RowsCountPeriod
        {
            get
            {
                return this.db.GLPeriods.Count();
            }
        }

        public List<GLPeriod> GetListPeriodForDDL()
        {
            List<GLPeriod> _result = new List<GLPeriod>();

            try
            {
                var _query = (
                                from _glPeriod in this.db.GLPeriods
                                where (_glPeriod.Period >= 1 && _glPeriod.Period <= 12)
                                select new
                                {
                                    Period = _glPeriod.Period,
                                    Description = _glPeriod.Description

                                }
                            );

                foreach (var _item in _query)
                {
                    //var _row = _obj.Template(new { Period = this._int, Description = this._string });

                    _result.Add(new GLPeriod(_item.Period, _item.Description));
                }
            }
            catch (Exception ex)
            {
                // ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetPeriodByDesc(string _prmDesc)
        {
            int _result = 0;

            try
            {
                _result = this.db.GLPeriods.Single(abc => abc.Description.ToLower().Trim() == _prmDesc.ToLower().Trim()).Period;
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public string GetDescByPeriod(int _prmPeriod)
        {
            string _result = "";

            try
            {
                _result = this.db.GLPeriods.Single(abc => abc.Period == _prmPeriod).Description;
            }
            catch (Exception ex)
            {
                                
            }

            return _result;
        }

        ~GLPeriodBL()
        {

        }
    }


}
