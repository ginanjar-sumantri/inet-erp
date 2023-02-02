using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule
{
    public sealed class GLYearBL : Base
    {
        private int _top = 10;

        public GLYearBL()
        {
        }

        public int RowsCountYear
        {
            get
            {
                return this.db.GLYears.Count();
            }
        }

        public List<GLYear> GetListYearForDDL()
        {
            List<GLYear> _result = new List<GLYear>();

            try
            {
                var _query = (
                                from _glYear in this.db.GLYears
                                orderby _glYear.Year descending
                                select new
                                {
                                    Year = _glYear.Year
                                }
                            ).Take(this._top);

                foreach (var _item in _query)
                {
                    //var _row = _obj.Template(new { Year = this._int });

                    _result.Add(new GLYear(_item.Year));
                }
            }
            catch (Exception ex)
            {
                // ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        ~GLYearBL()
        {
        }
    }
}