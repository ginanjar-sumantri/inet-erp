using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
   
    public partial class MsFixedAsset
    {
        private string _faStatusName = "";

        public MsFixedAsset(string _prmFACode, string _prmFAName, string _prmFAStatusName, string _prmCurrCode, decimal _prmForexRate, decimal _prmAmountForex, decimal _prmAmountHome)
        {
            this.FACode = _prmFACode;
            this.FAName = _prmFAName;
            this.FAStatusName = _prmFAStatusName;
            this.CurrCode = _prmCurrCode;
            this.ForexRate = _prmForexRate;
            this.AmountForex = _prmAmountForex;
            this.AmountHome = _prmAmountHome;
        }

        public MsFixedAsset(string _prmFACode, string _prmFAName, string _prmFAStatusName, string _prmCurrCode, decimal _prmForexRate, decimal _prmAmountForex, decimal _prmAmountHome, char _prmCreatedFrom, String _prmFALocationCode)
        {
            this.FACode = _prmFACode;
            this.FAName = _prmFAName;
            this.FAStatusName = _prmFAStatusName;
            this.CurrCode = _prmCurrCode;
            this.ForexRate = _prmForexRate;
            this.AmountForex = _prmAmountForex;
            this.AmountHome = _prmAmountHome;
            this.CreatedFrom = _prmCreatedFrom;
            this.FALocationCode = _prmFALocationCode;
        }

        public MsFixedAsset(String _prmFACode, String _prmFAName, String _prmFAStatus, char _prmFAOwner, String _prmFASubGroup,
            DateTime _prmBuyingDate, String _prmFALocationType, String _prmFALocationCode, String _prmCurrCode,
            Decimal _prmForexRate, Decimal _prmAmountForex, Decimal _prmAmountHome, int _prmTotalLifeMonth,
            int _prmLifeDepr, int _prmLifeProcess, int _prmTotalLifeDepr, Decimal _prmAmountDepr,
            Decimal _prmAmountProcess, Decimal _prmTotalAmountDepr, Decimal _prmAmountCurrent,
            char _prmFgActive, char _prmFgSold, char _prmFgProcess, char _prmCreatedFrom,
            Boolean _prmCreateJournal, String _prmPhoto)
        {
            this.FACode = _prmFACode;
            this.FAName = _prmFAName;
            this.FAStatus = _prmFAStatus;
            this.FAOwner = _prmFAOwner;
            this.FASubGroup = _prmFASubGroup;
            this.BuyingDate = _prmBuyingDate;
            this.FALocationType = _prmFALocationType;
            this.FALocationCode = _prmFALocationCode;
            this.CurrCode = _prmCurrCode;
            this.ForexRate = _prmForexRate;
            this.AmountForex = _prmAmountForex;
            this.AmountHome = _prmAmountHome;
            this.TotalLifeMonth = _prmTotalLifeMonth;
            this.LifeDepr = _prmLifeDepr;
            this.LifeProcess = _prmLifeProcess;
            this.TotalLifeDepr = _prmTotalLifeDepr;
            this.AmountDepr = _prmAmountDepr;
            this.AmountProcess = _prmAmountProcess;
            this.TotalAmountDepr = _prmTotalAmountDepr;
            this.AmountCurrent = _prmAmountCurrent;
            this.FgActive = _prmFgActive;
            this.FgSold = _prmFgSold;
            this.FgProcess = _prmFgProcess;
            this.CreatedFrom = _prmCreatedFrom;
            this.CreateJournal = _prmCreateJournal;
            this.Photo = _prmPhoto;
        }

        public MsFixedAsset(string _prmFACode, string _prmFAName)
        {
            this.FACode = _prmFACode;
            this.FAName = _prmFAName;
        }

        public string FAStatusName
        {
            get
            {
                return this._faStatusName;
            }

            set
            {
                this._faStatusName = value;
            }
        }

    }
}
