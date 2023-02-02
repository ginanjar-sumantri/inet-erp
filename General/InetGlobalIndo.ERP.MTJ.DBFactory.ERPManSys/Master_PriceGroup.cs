using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_PriceGroup
    {
        public Master_PriceGroup(String _priceGroupCode, int _year, string _currCode, decimal _amountForex, Boolean _fgActive, DateTime _prmStartDate, DateTime _prmEndDate)
        {
            this.PriceGroupCode = _priceGroupCode;
            this.Year = _year;
            this.CurrCode = _currCode;
            this.AmountForex = _amountForex;
            this.FgActive = _fgActive;
            this.StartDate = _prmStartDate;
            this.EndDate = _prmEndDate;
        }

        public Master_PriceGroup(String _priceGroupCode, int _year, string _currCode, decimal _amountForex, Boolean _fgActive, String _pgDesc, DateTime _prmStartDate, DateTime _prmEndDate, string _prmInsertBy, DateTime _prmInsertDate, string _prmEditBy, DateTime _prmEditDate)
        {
            this.PriceGroupCode = _priceGroupCode;
            this.Year = _year;
            this.CurrCode = _currCode;
            this.AmountForex = _amountForex;
            this.PGDesc = _pgDesc;
            this.FgActive = _fgActive;
            this.StartDate = _prmStartDate;
            this.EndDate = _prmEndDate;
            this.InsertBy = _prmInsertBy;
            this.InsertDate = _prmInsertDate;
            this.EditBy = _prmEditBy;
            this.EditDate = _prmEditDate;
        }

        public Master_PriceGroup(String _priceGroupCode)
        {
            this.PriceGroupCode = _priceGroupCode;
        }

    }
}
