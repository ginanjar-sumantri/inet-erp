using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DataMapping;

public partial class POS_POS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) {
            this.TransDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.transDate.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

            this.StyleSheetLiteral.Text = "<link type=\"text/css\" href=\"" + ApplicationConfig.HomeWebAppURL + "StyleSheet.css\" rel=\"Stylesheet\" />";
            
            this.btnSearchCustomer.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findCustomer&configCode=customer','_popSearch','width=1200,height=1000,toolbar=0,location=0,status=0,scrollbars=1')";            
            this.btnSrcEmployee.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findEmployee&configCode=employee','_popSearch','width=950,height=700,toolbar=0,location=0,status=0,scrollbars=1')";
            String spawnJS = "<script language='JavaScript'>\n";
            ////////////////////DECLARE FUNCTION FOR CATCHING PRODUCT SEARCH
            spawnJS += "function findProduct(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.productCode.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.getElementById('" + this.productName.ClientID + "').value = dataArray [1];\n";
            spawnJS += "priceVal = parseFloat(dataArray[2]);\n";
            spawnJS += "document.getElementById('" + this.price.ClientID + "').value = priceVal.toFixed(2);\n";
            spawnJS += "document.getElementById('" + this.uom.ClientID + "').value = dataArray [3];\n";            
            spawnJS += "document.getElementById('" + this.txtProductName.ClientID + "').value = dataArray [1];\n";
            spawnJS += "document.getElementById('" + this.txtPrice.ClientID + "').value = priceVal.toFixed(2);\n";
            spawnJS += "document.getElementById('" + this.txtUom.ClientID + "').value = dataArray [3];\n";
            spawnJS += "document.getElementById('" + this.qty.ClientID + "').focus();\n";
            spawnJS += "}\n";

            ////////////////////DECLARE FUNCTION FOR CATCHING CUSTOMER SEARCH
            spawnJS += "function findCustomer(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.custCode.ClientID + "').value = dataArray[0];\n";
            spawnJS += "document.getElementById('" + this.customer.ClientID + "').value = dataArray[1];\n";
            spawnJS += "}\n";

            ////////////////////DECLARE FUNCTION FOR CATCHING EMPLOYEE SEARCH
            spawnJS += "function findEmployee(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.namaSales.ClientID + "').value = dataArray[0];\n";
            spawnJS += "document.getElementById('" + this.empNumb.ClientID + "').value = dataArray[1];\n";
            spawnJS += "}\n";
            
            ////////////////////DECLARE FUNCTION FOR CALCULATING LINETOTAL
            spawnJS += "function calculateLineTotal(x) {\n";
            spawnJS += "if ( x.value != '' ) {\n";
            spawnJS += "document.getElementById('" + this.lineTotal.ClientID + "').value = (document.getElementById('" + this.qty.ClientID + "').value * document.getElementById('" + this.price.ClientID + "').value).toFixed(2);\n";
            spawnJS += "document.getElementById('" + this.txtLineTotal.ClientID + "').value = (document.getElementById('" + this.qty.ClientID + "').value * document.getElementById('" + this.price.ClientID + "').value).toFixed(2);\n";
            spawnJS += "}\n}\n";

            ///////////////////FUNCTION FOR KEYPRESS ENTER ON PRODUCTCODE
            spawnJS += "function enterProductCode() {\n";
            spawnJS += "document.getElementById('" + this.qty.ClientID + "').focus();\n" ;
            spawnJS += "return false;\n" ;
            spawnJS += "}\n";
            ///////////////////FUNCTION FOR KEYPRESS ENTER ON QTY
            spawnJS += "function enterQty() {\n";
            spawnJS += "document.getElementById('" + this.btnAddLine.ClientID + "').focus();\n";
            spawnJS += "return false;\n";
            spawnJS += "}\n";

            ////////////////////ONCHANGE DISCOUNT AMOUNT
            spawnJS += "function setDiscAmount() {\n";
            spawnJS += "document.getElementById('" + this.discountValue.ClientID + "').value = Math.round(";
            spawnJS += "document.getElementById('" + this.discount.ClientID + "').value * ";
            spawnJS += "document.getElementById('" + this.subTotal.ClientID + "').value / 100 ) ;";
            spawnJS += "}\n";

            spawnJS += "</script>\n";
            this.javascriptReceiver.Text = spawnJS;

            this.transDate.Text = DateTime.Now.ToString("yyyy-MM-dd");


            CurrencyBL _currBL = new CurrencyBL();
            //this.currency.Text = _currBL.GetCurrDefault();
            this.currencyX.DataSource = _currBL.GetListAll();
            this.currencyX.DataTextField = "CurrCode";
            this.currencyX.DataValueField = "CurrCode";
            this.currencyX.DataBind();
            this.currencyX.SelectedValue = _currBL.GetCurrDefault();

            POSBL _posBL = new POSBL();
            this.paymentDDL.DataSource = _posBL.getPaymentType();
            this.paymentDDL.DataValueField = "Key";
            this.paymentDDL.DataTextField = "Value";
            this.paymentDDL.DataBind();

            this.qty.Attributes.Add("onkeyup", "numericInput(this);calculateLineTotal(this)");
            this.qty.Attributes.Add("onchange", "numericInput(this);calculateLineTotal(this)");
            this.discount.Attributes.Add("onkeyup", "numericInput(this)");
            this.discount.Attributes.Add("onchange", "numericInput(this);setDiscAmount();document.forms[0].submit()");
            this.discountValue.Attributes.Add("onkeyup", "numericInput(this)");
            this.discountValue.Attributes.Add("onchange", "numericInput(this);document.forms[0].submit()");
            this.tax.Attributes.Add("onkeyup", "numericInput(this)");
            this.tax.Attributes.Add("onchange", "numericInput(this);document.forms[0].submit()");
            this.cashAmount.Attributes.Add("onkeyup", "numericInput(this)");
            this.cashAmount.Attributes.Add("onchange", "numericInput(this);document.forms[0].submit()");
            this.additionalFee.Attributes.Add("onkeyup", "numericInput(this)");
            this.additionalFee.Attributes.Add("onchange", "numericInput(this);document.forms[0].submit()");

            this.productCode.Attributes.Add("onkeydown", "if(event.keyCode==13 && this.value != ''){return enterProductCode();}");
            this.qty.Attributes.Add("onkeydown", "if(event.keyCode==13){return enterQty();}");

            this.customer.Attributes.Add("ReadOnly", "true");
            this.namaSales.Attributes.Add("ReadOnly", "true");
            this.forexRateTextBox.Attributes.Add("ReadOnly", "true");
        }

        this.btnSearchProductCode.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProduct&configCode=productWithSalePrice&paramWhere=B.CurrCodesamadenganpetik" + this.currencyX.SelectedValue + "petik','_popSearch','width=500,height=300,toolbar=0,location=0,status=0,scrollbars=1')";

        this.productCode.Focus();

        if (this.boughtItems.Value != "")
            this.showBoughtItem();
        else
            this.perulanganDataDibeli.Text = "";

        if (this.postingState.Value == "1")
        {
            this.btnPrint.Enabled = true;
            this.btnPosting.Enabled = false;
            this.panelAddRow.Visible = false;
        }
        else {
            this.btnPrint.Enabled = false;
            this.btnPosting.Enabled = true;
            this.panelAddRow.Visible = true;
        }
    }
    protected void btnAddLine_Click(object sender, EventArgs e)
    {
        if (this.productName.Value != "" && this.qty.Text != "")
        {
            this.itemCount.Value = (Convert.ToInt16(this.itemCount.Value) + 1).ToString();
            Decimal _subTotal = Convert.ToDecimal(this.subTotal.Value);
            if (this.itemCount.Value == "1")
            {
                this.boughtItems.Value = this.itemCount.Value + "|" + this.productCode.Text + "|";
                this.boughtItems.Value += this.productName.Value + "|" + this.qty.Text + "|" + this.uom.Value + "|";
                this.boughtItems.Value += this.price.Value + "|" + this.lineTotal.Value;
                _subTotal += Convert.ToDecimal(this.lineTotal.Value);
            }
            else
            {
                this.boughtItems.Value += "^" + this.itemCount.Value + "|" + this.productCode.Text + "|";
                this.boughtItems.Value += this.productName.Value + "|" + this.qty.Text + "|" + this.uom.Value + "|";
                this.boughtItems.Value += this.price.Value + "|" + this.lineTotal.Value;
                _subTotal += Convert.ToDecimal(this.lineTotal.Value);
            }
            this.productCode.Text = "";
            this.productCode.Focus();
            this.productName.Value = "";
            this.txtProductName.Text = "";
            this.price.Value = "0";
            this.txtPrice.Text = "";
            this.qty.Text = "";
            this.uom.Value = "";
            this.txtUom.Text = "";
            this.lineTotal.Value = "0";
            this.subTotal.Value = _subTotal.ToString();
            this.txtSubTotal.Text = _subTotal.ToString();
            this.showBoughtItem();
        }
    }
    protected void showBoughtItem() 
    {
        String _strGenerateTable = "" ;
        String[] _dataItem = this.boughtItems.Value.Split('^');
        Decimal _subTotal = 0 ;
        foreach (String _dataRow in _dataItem) {
            _strGenerateTable += "<tr height='20px'>" ;
            String[] _dataField = _dataRow.Split('|');
            foreach (String _data in _dataField) {
                _strGenerateTable += "<td>" + _data + "</td>" ;
            }
            if (this.postingState.Value == "1")
            {
                _strGenerateTable += "<td></td></tr>";
            } 
            else 
            {
                _strGenerateTable += "<td><input type='button' onClick='deleteItem(\"" + this.boughtItems.ClientID + "\"," + _dataField[0] + ",\"" + this.itemCount.ClientID + "\");' value='Delete'></td></tr>";
            }
            _subTotal += Convert.ToDecimal(_dataField[6]);
        }
        this.subTotal.Value = _subTotal.ToString();
        this.txtSubTotal.Text = this.subTotal.Value;
        this.perulanganDataDibeli.Text = _strGenerateTable;
        //this.discountValue.Text = ( Convert.ToDecimal(this.subTotal.Value) * Convert.ToDecimal(this.discount.Text) / 100).ToString();
        this.discount.Text = Math.Round(Convert.ToDecimal(this.discountValue.Text) / Convert.ToDecimal(this.subTotal.Value) * 100 , 2).ToString();
        this.taxValue.Text = ((Convert.ToDecimal(this.subTotal.Value) - (Convert.ToDecimal(this.discountValue.Text)) ) * Convert.ToDecimal(this.tax.Text) / 100 ).ToString() ;
        this.totalValue.Text = ((Convert.ToDecimal(this.subTotal.Value) - (Convert.ToDecimal(this.discountValue.Text))) + Convert.ToDecimal(this.taxValue.Text) + Convert.ToDecimal(this.additionalFee.Text) ).ToString();
        this.totalPriceBesar.Text = Convert.ToDecimal(this.totalValue.Text).ToString("#,##0.00");
        if (Convert.ToDecimal(this.cashAmount.Text) > Convert.ToDecimal(this.totalValue.Text))
            this.returnCash.Text = (Convert.ToDecimal(this.cashAmount.Text) - Convert.ToDecimal(this.totalValue.Text) ).ToString();
    }
    protected void productCode_TextChanged(object sender, EventArgs e)
    {
        POSBL _posBL = new POSBL() ;
        String[] _productData = _posBL.getProductData(this.productCode.Text,this.currencyX.SelectedValue).Split('|') ;
        if (_productData.Length > 0 && _productData[0] != "")
        {
            this.txtProductName.Text = _productData[0];
            this.productName.Value = _productData[0];
            this.txtUom.Text = _productData[1];
            this.uom.Value = _productData[1];
            this.txtPrice.Text = Convert.ToDecimal( _productData[2]).ToString("0.00");
            this.price.Value = Convert.ToDecimal( _productData[2] ).ToString("0.00");
            this.qty.Focus();
        }
    }
    protected void btnPosting_Click(object sender, EventArgs e)
    {
        if ((Convert.ToDecimal(this.cashAmount.Text) >= Convert.ToDecimal(this.totalValue.Text)) && this.boughtItems.Value != "")
        {
            POSBL _posBL = new POSBL();
            SAL_TrRetail _addSAL_TrRetail = new SAL_TrRetail();
            _addSAL_TrRetail.TransDate = Convert.ToDateTime(this.transDate.Text);
            _addSAL_TrRetail.EmpName = this.namaSales.Text;
            _addSAL_TrRetail.EmpNumb = this.empNumb.Value;
            _addSAL_TrRetail.CustName = this.custCode.Value;
            //_addSAL_TrRetail.PaymentType = this.paymentType.Text;
            _addSAL_TrRetail.PaymentType = this.paymentDDL.SelectedValue;
            _addSAL_TrRetail.BankName = this.bankName.Text;
            //_addSAL_TrRetail.CurrCode = this.currency.Text;
            _addSAL_TrRetail.CurrCode = this.currencyX.SelectedValue;
            _addSAL_TrRetail.TotalAmount = Convert.ToDecimal(this.totalValue.Text);
            _addSAL_TrRetail.AdditionalFee = Convert.ToDecimal(this.additionalFee.Text);
            _addSAL_TrRetail.DiscPercent = Convert.ToDecimal(this.discount.Text);
            _addSAL_TrRetail.DiscAmount = Convert.ToDecimal(this.discountValue.Text);
            _addSAL_TrRetail.PPNPercent = Convert.ToByte(this.tax.Text);
            _addSAL_TrRetail.PPNAmount = Convert.ToDecimal(this.taxValue.Text);
            _addSAL_TrRetail.BaseForex = Convert.ToDecimal(this.subTotal.Value);
            _addSAL_TrRetail.ForexRate = Convert.ToDecimal(this.forexRateHiddenField.Value);
            if (this.note.Text != "")
                _addSAL_TrRetail.Remark = this.note.Text;

            String _generatedTransNmbr = _posBL.postingTransaksi(_addSAL_TrRetail, this.boughtItems.Value);
            if (_generatedTransNmbr != "")
            {
                String[] _transFileNmbr = _generatedTransNmbr.Split('|');
                this.transNo.Text = _transFileNmbr[0];
                this.fileNo.Text = _transFileNmbr[1];

                this.postingState.Value = "1";
                this.btnPrint.Enabled = true;
                this.btnPosting.Enabled = false;
                this.panelAddRow.Visible = false;
                this.discount.Enabled = false;
                this.tax.Enabled = false;
                this.cashAmount.Enabled = false;
                this.showBoughtItem();
                this.hiddenTransNumber.Value = _transFileNmbr[0];
                if (_transFileNmbr[2] != "")
                    this.CashWarningLabel.Text = _transFileNmbr[2];
                this.bottomSpanNota.Visible = true;
            }
        }
        else {
            this.CashWarningLabel.Attributes.Add ( "style" , "color:RED" );
            this.CashWarningLabel.Text = "Uang Pembayaran Tidak Cukup.";
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<script>");
        sb.Append("window.open('POSPrint.aspx?datacount=" + this.itemCount.Value + "&transNmbr=" + this.hiddenTransNumber.Value + "' , '_printSetruk', 'width=700,height=600,toolbar=0,location=0,status=0,scrollbars=1');");
        sb.Append("</script>");
        Page.RegisterStartupScript("printSetruk", sb.ToString());
        //String _dataPayment = "";
        //_dataPayment += this.subTotal.Value + ":" + this.discount.Text + ":" + this.discountValue.Text + ":" ;
        //_dataPayment += this.tax.Text + ":" + this.taxValue.Text + ":" + this.totalValue.Text + ":" ;
        //_dataPayment += this.cashAmount.Text + ":" + this.returnCash.Text ;
        //String _dataAct = "";
        //_dataAct += this.transDate.Text + ":" + this.customer.Text + ":" + this.namaSales.Text;
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<script>");
        //sb.Append("window.open('POSPrint.aspx?dataAct=" + _dataAct + "&dataChunk=" + this.boughtItems.Value + "&dataPayment=" + _dataPayment + "' , '_printSetruk', 'width=320,toolbar=0,location=0,status=0,scrollbars=1');");
        //sb.Append("</script>");
        //Page.RegisterStartupScript("test", sb.ToString());
    }
    protected void btnNewTrans_Click(object sender, EventArgs e)
    {
        Response.Redirect("POS.aspx");
    }
    protected void currencyX_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.SetCurrRate();
        this.itemCount.Value = "0";
        this.boughtItems.Value = "" ;
        this.subTotal.Value = "0";
        this.txtSubTotal.Text = "0";
        this.perulanganDataDibeli.Text = "";
        this.discountValue.Text = "0" ;
        this.taxValue.Text = "0";
        this.totalValue.Text = "0";
        this.totalPriceBesar.Text = "0";
        this.cashAmount.Text = "0" ;
        this.returnCash.Text = "0" ;
    }

    private void SetCurrRate()
    {
        CurrencyBL _currencyBL = new CurrencyBL();
        CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        byte _decimalPlace = _currencyBL.GetDecimalPlace(this.currencyX.SelectedValue);
        this.forexRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.currencyX.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
        if (this.forexRateTextBox.Text == "0") this.forexRateTextBox.Text = "1";
        this.forexRateHiddenField.Value = this.forexRateTextBox.Text ;
    }

}
