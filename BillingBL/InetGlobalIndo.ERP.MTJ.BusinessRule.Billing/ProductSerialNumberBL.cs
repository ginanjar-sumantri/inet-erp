using System;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Billing
{
    public sealed partial class ProductSerialNumberBL : Base
    {
        public ProductSerialNumberBL()
        {
        }
        ~ProductSerialNumberBL()
        {
        }

        #region ProductSerialNumber
        public MsProduct_SerialNumber GetSingleProductSerialNumber(String _prmSerialNo)
        {
            MsProduct_SerialNumber _result = null;

            try
            {
                _result = db.MsProduct_SerialNumbers.Single(_temp => _temp.SerialNumber.Trim().ToLower() == _prmSerialNo.Trim().ToLower());
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public V_MsProduct_VoucherSerialNumber GetSingleV_ProductVoucherSerialNumber(String _prmSerialNo)
        {
            V_MsProduct_VoucherSerialNumber _result = null;

            try
            {
                _result = db.V_MsProduct_VoucherSerialNumbers.Single(_temp => _temp.SerialNumber.Trim().ToLower() == _prmSerialNo.Trim().ToLower());
            }
            catch (Exception ex)
            {
            }

            return _result;
        }
        #endregion
    }
}
