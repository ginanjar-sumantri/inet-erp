using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

namespace InetGlobalIndo.ERP.MTJ.Common
{
    public sealed class Pager
    {
        //private int _maxPageNumber = 0;
        //private int _minPageNumber = 0;

        public Pager()
        {
        }

        //public string Render(int _prmCurrentPageNumber, int _prmPageSize, int _prmNumberOfRow, string _prmNavURL, string _prmReqPageValue)
        //{
        //    string _result = "";

        //    //double q = this._curr.RowsCount;
        //    this._maxPageNumber = Convert.ToInt32(Math.Ceiling((decimal)_prmNumberOfRow / _prmPageSize));
        //    this._minPageNumber = _prmCurrentPageNumber - Convert.ToInt32(ApplicationConfig.PageNumberRange);

        //    if (this._minPageNumber < 0)
        //    {
        //        this._minPageNumber = 0;
        //    }

        //    if (_prmCurrentPageNumber + Convert.ToInt32(ApplicationConfig.PageNumberRange) < this._maxPageNumber)
        //    {
        //        this._maxPageNumber = _prmCurrentPageNumber + Convert.ToInt32(ApplicationConfig.PageNumberRange);
        //    }

        //    StringBuilder sb = new StringBuilder();

        //    int i = (this._minPageNumber == 0) ? 0 : this._minPageNumber - 1;
        //    for (; i <= this._maxPageNumber; i++)
        //    {
        //        if ((i == this._minPageNumber - 1) || (i == this._maxPageNumber))
        //        {
        //            sb.Append("<a href='" + _prmNavURL.Replace(_prmReqPageValue, i.ToString()) + "'>...</a>&nbsp;");
        //        }
        //        else if (i == _prmCurrentPageNumber)
        //        {
        //            sb.Append("[<b>" + (i + 1) + "</b>]&nbsp;");
        //        }
        //        else
        //        {
        //            sb.Append("<a href='" + _prmNavURL.Replace(_prmReqPageValue, i.ToString()) + "'>" + (i + 1) + "</a>&nbsp;");
        //        }
        //    }

        //    _result = sb.ToString();

        //    return _result;
        //}

        ~Pager()
        {
        }
    }
}
