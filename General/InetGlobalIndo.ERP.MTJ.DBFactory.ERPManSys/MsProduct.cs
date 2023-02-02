using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsProduct
    {
        string _prodSubGrpName = "";
        string _prodTypeName = "";        

        public MsProduct(string _prmProductCode, string _prmProductName, string _prmProdSubGrp, string _prmProdSubGrpName, string _prmProdType, string _prmProdTypeName)
        {
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.ProductSubGroup = _prmProdSubGrp;
            this.ProductSubGroupName = _prmProdSubGrpName;
            this.ProductType = _prmProdType;
            this.ProductTypeName = _prmProdTypeName;
        }

        public MsProduct(string _prmProductCode, string _prmProductName, string _prmProdSubGrp, string _prmProdType, string _prmSpecification1,
            string _prmSpecification2, string _prmSpecification3, string _prmSpecification4, decimal _prmMinQty, decimal _prmMaxQty, string _prmUnit,
            string _prmUnitOrder, decimal _prmLength, decimal _prmWidth, decimal _prmHeight, decimal _prmVolume, decimal _prmWeight, string _prmPurchaseCurr,
            String _prmPriceGroupCode, decimal _prmBuyingPrice, decimal _prmSellingPrice, char _prmFgActive, string _prmUserId, DateTime _prmUserDate, string _prmBarcode)
        {
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.ProductSubGroup = _prmProdSubGrp;
            this.ProductType = _prmProdType;
            this.Specification1 = _prmSpecification1;
            this.Specification2 = _prmSpecification2;
            this.Specification3 = _prmSpecification3;
            this.Specification4 = _prmSpecification4;
            this.MinQty = _prmMinQty;
            this.MaxQty = _prmMaxQty;
            this.Unit = _prmUnit;
            this.UnitOrder = _prmUnitOrder;
            this.Length = _prmLength;
            this.Width = _prmWidth;
            this.Height = _prmHeight;
            this.Volume = _prmVolume;
            this.Weight = _prmWeight;
            this.PurchaseCurr = _prmPurchaseCurr;
            this.PriceGroupCode = _prmPriceGroupCode;
            this.BuyingPrice = _prmBuyingPrice;
            this.SellingPrice = _prmSellingPrice;
            this.FgActive = _prmFgActive;
            this.CreatedBy = _prmUserId;
            this.CreatedDate = _prmUserDate;
            this.Barcode = _prmBarcode;
        }

        public MsProduct(string _prmProductCode, string _prmProductName, string _prmProdSubGrp, string _prmProdType, string _prmSpecification1,
            string _prmSpecification2, string _prmSpecification3, string _prmSpecification4, decimal _prmMinQty, decimal _prmMaxQty, string _prmUnit,
            string _prmUnitOrder, decimal _prmLength, decimal _prmWidth, decimal _prmHeight, decimal _prmVolume, decimal _prmWeight, string _prmPurchaseCurr,
            String _prmPriceGroupCode, decimal _prmBuyingPrice, decimal _prmSellingPrice, char _prmFgActive, string _prmUserId, DateTime _prmUserDate, 
            string _prmBarcode, int _prmItemDuration)
        {
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.ProductSubGroup = _prmProdSubGrp;
            this.ProductType = _prmProdType;
            this.Specification1 = _prmSpecification1;
            this.Specification2 = _prmSpecification2;
            this.Specification3 = _prmSpecification3;
            this.Specification4 = _prmSpecification4;
            this.MinQty = _prmMinQty;
            this.MaxQty = _prmMaxQty;
            this.Unit = _prmUnit;
            this.UnitOrder = _prmUnitOrder;
            this.Length = _prmLength;
            this.Width = _prmWidth;
            this.Height = _prmHeight;
            this.Volume = _prmVolume;
            this.Weight = _prmWeight;
            this.PurchaseCurr = _prmPurchaseCurr;
            this.PriceGroupCode = _prmPriceGroupCode;
            this.BuyingPrice = _prmBuyingPrice;
            this.SellingPrice = _prmSellingPrice;
            this.FgActive = _prmFgActive;
            this.CreatedBy = _prmUserId;
            this.CreatedDate = _prmUserDate;
            this.Barcode = _prmBarcode;
            this.ItemDuration = _prmItemDuration;
        }

        public MsProduct(string _prmProductCode, string _prmProductName, string _prmProdSubGrp, string _prmProdType, string _prmSpecification1,
            string _prmSpecification2, string _prmSpecification3, string _prmSpecification4, decimal _prmMinQty, decimal _prmMaxQty, string _prmUnit,
            string _prmUnitOrder, decimal _prmLength, decimal _prmWidth, decimal _prmHeight, decimal _prmVolume, decimal _prmWeight, string _prmPurchaseCurr,
            String _prmPriceGroupCode, decimal _prmBuyingPrice, decimal _prmSellingPrice, char _prmFgActive, string _prmUserId, DateTime _prmUserDate, string _prmBarcode, Boolean _prmFgConsignment)
        {
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.ProductSubGroup = _prmProdSubGrp;
            this.ProductType = _prmProdType;
            this.Specification1 = _prmSpecification1;
            this.Specification2 = _prmSpecification2;
            this.Specification3 = _prmSpecification3;
            this.Specification4 = _prmSpecification4;
            this.MinQty = _prmMinQty;
            this.MaxQty = _prmMaxQty;
            this.Unit = _prmUnit;
            this.UnitOrder = _prmUnitOrder;
            this.Length = _prmLength;
            this.Width = _prmWidth;
            this.Height = _prmHeight;
            this.Volume = _prmVolume;
            this.Weight = _prmWeight;
            this.PurchaseCurr = _prmPurchaseCurr;
            this.PriceGroupCode = _prmPriceGroupCode;
            this.BuyingPrice = _prmBuyingPrice;
            this.SellingPrice = _prmSellingPrice;
            this.FgActive = _prmFgActive;
            this.CreatedBy = _prmUserId;
            this.CreatedDate = _prmUserDate;
            this.Barcode = _prmBarcode;
            this.FgConsignment = _prmFgConsignment;
        }

        public MsProduct(string _prmProductCode, string _prmProductName, string _prmProdSubGrp, string _prmProdType, string _prmSpecification1,
            string _prmSpecification2, string _prmSpecification3, string _prmSpecification4, decimal _prmMinQty, decimal _prmMaxQty, string _prmUnit,
            string _prmUnitOrder, decimal _prmLength, decimal _prmWidth, decimal _prmHeight, decimal _prmVolume, decimal _prmWeight, string _prmPurchaseCurr,
            String _prmPriceGroupCode, decimal _prmBuyingPrice, decimal _prmSellingPrice, char _prmFgActive, string _prmUserId, DateTime _prmUserDate, string _prmBarcode, Boolean _prmFgConsignment
            , String _prmConsignmentSuppCode)
        {
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.ProductSubGroup = _prmProdSubGrp;
            this.ProductType = _prmProdType;
            this.Specification1 = _prmSpecification1;
            this.Specification2 = _prmSpecification2;
            this.Specification3 = _prmSpecification3;
            this.Specification4 = _prmSpecification4;
            this.MinQty = _prmMinQty;
            this.MaxQty = _prmMaxQty;
            this.Unit = _prmUnit;
            this.UnitOrder = _prmUnitOrder;
            this.Length = _prmLength;
            this.Width = _prmWidth;
            this.Height = _prmHeight;
            this.Volume = _prmVolume;
            this.Weight = _prmWeight;
            this.PurchaseCurr = _prmPurchaseCurr;
            this.PriceGroupCode = _prmPriceGroupCode;
            this.BuyingPrice = _prmBuyingPrice;
            this.SellingPrice = _prmSellingPrice;
            this.FgActive = _prmFgActive;
            this.CreatedBy = _prmUserId;
            this.CreatedDate = _prmUserDate;
            this.Barcode = _prmBarcode;
            this.FgConsignment = _prmFgConsignment;
            this.ConsignmentSuppCode = _prmConsignmentSuppCode;
        }

        public MsProduct(string _prmProductCode, string _prmProductName)
        {
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
        }

        public string ProductSubGroupName
        {
            get
            {
                return this._prodSubGrpName;
            }
            set
            {
                this._prodSubGrpName = value;
            }
        }

        public string ProductTypeName
        {
            get
            {
                return this._prodTypeName;
            }
            set
            {
                this._prodTypeName = value;
            }
        }        
    }
}
