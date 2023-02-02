using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Collections.Generic;
using System.IO;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using Excel = Microsoft.Office.Interop.Excel;


namespace InetGlobalIndo.ERP.MTJ.UI.Billing.GenerateVoucher
{
    public partial class GenerateVoucherView : GenerateVoucherBase
    {
        private Int16 _nomor = 1;
        private GenerateVoucherBL _generateVoucherBL = new GenerateVoucherBL();
        private OrganizationStructureBL _organizationStructureBL = new OrganizationStructureBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);
            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
                this.BackButton.PostBackUrl = this._homePage;
                this.CreateXMLButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/viewinexcel.jpg";
                this.BackButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
                this.BackButton2.PostBackUrl = this._homePage;
                this.CreateXMLButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/viewinexcel.jpg";

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowData()
        {
            String _transNumberCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            this.ListRepeater.DataSource = this._generateVoucherBL.GetVoucherSerialNumberList(_transNumberCode);
            this.ListRepeater.DataBind();
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                MsProduct_SerialNumber _temp = (MsProduct_SerialNumber)e.Item.DataItem;

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
                _tableRow.Attributes.Add("OnMouseOver", "this.style.backgroundColor='" + this._rowColorHover + "';");
                if (e.Item.ItemType == ListItemType.Item)
                {
                    _tableRow.Attributes.Add("style", "background-color:" + this._rowColor);
                    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColor + "';");
                }
                if (e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    _tableRow.Attributes.Add("style", "background-color:" + this._rowColorAlternate);
                    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColorAlternate + "';");
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _noLiteral.Text = _nomor.ToString();
                _nomor += 1;

                Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                Literal _SNLiteral = (Literal)e.Item.FindControl("SNLiteral");
                _SNLiteral.Text = HttpUtility.HtmlEncode(_temp.SerialNumber);

                Literal _PINLiteral = (Literal)e.Item.FindControl("PINLiteral");
                _PINLiteral.Text = HttpUtility.HtmlEncode(_temp.PIN);

                Literal _PINAuthenticationLiteral = (Literal)e.Item.FindControl("PINAuthenticationLiteral");
                _PINAuthenticationLiteral.Text = _temp.PINAuthentication;

                Literal _expireDateLiteral = (Literal)e.Item.FindControl("ExpireDateLiteral");
                _expireDateLiteral.Text = HttpUtility.HtmlEncode(_temp.ExpirationDate.ToString());

                Literal _countryLiteral = (Literal)e.Item.FindControl("CountryLiteral");
                _countryLiteral.Text = _temp.Country;
            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void CreateXMLButton_Click(object sender, ImageClickEventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.ApplicationClass();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Cells[1, 1] = "Product Code";
            xlWorkSheet.Cells[1, 2] = "Serial Number";
            xlWorkSheet.Cells[1, 3] = "PIN";
            xlWorkSheet.Cells[1, 4] = "PIN Authentication";
            xlWorkSheet.Cells[1, 5] = "Expire Date";
            xlWorkSheet.Cells[1, 6] = "Country";

            Int16 _currCol = 1;
            Int16 _currRow = 2;

            List<MsProduct_SerialNumber> _dataVoucher = _generateVoucherBL.GetVoucherSerialNumberList(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            foreach (MsProduct_SerialNumber _rowVoucher in _dataVoucher)
            {
                _currCol = 1;
                xlWorkSheet.Cells[_currRow, _currCol++] = _rowVoucher.ProductCode;
                xlWorkSheet.Cells[_currRow, _currCol++] = "'" + _rowVoucher.SerialNumber;
                xlWorkSheet.Cells[_currRow, _currCol++] = "'" + ("000000000000000" + _rowVoucher.PIN).Substring(("000000000000000" + _rowVoucher.PIN).Length - 16, 16);
                xlWorkSheet.Cells[_currRow, _currCol++] = "'" + ("000000" + _rowVoucher.PINAuthentication).Substring(("000000" + _rowVoucher.PINAuthentication).Length - 6, 6);
                xlWorkSheet.Cells[_currRow, _currCol++] = "'" + ("000000" + _rowVoucher.ExpirationDate).Substring(("000000" + _rowVoucher.ExpirationDate).Length - 6, 6);
                xlWorkSheet.Cells[_currRow++, _currCol++] = _rowVoucher.Country;
            }

            xlWorkSheet.Columns.AutoFit();
            //xlWorkSheet.Columns.NumberFormat = "0";
            String _tempFile = "voucher" + DateTime.Now.ToString("ddMMyyhhmmss") + ".xls";
            xlWorkBook.SaveAs(ApplicationConfig.SalesFormatPath + _tempFile, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            Response.Redirect(ApplicationConfig.SalesFormatVirDirPath + _tempFile);
        }
    }
}