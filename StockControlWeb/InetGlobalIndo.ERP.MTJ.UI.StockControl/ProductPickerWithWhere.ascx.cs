using System;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.CustomControl;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;

public partial class ProductPickerWithWhere : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            StockBeginningBL _stockBeginBL = new StockBeginningBL();
            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);
            STCAdjustHd _stcAdjustHd = _stockBeginBL.GetSingleSTCAdjustHd(Rijndael.Decrypt(_nvcExtractor.GetValue("code"), ApplicationConfig.EncryptionKey));

            this.btnSearch.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProduct&configCode=" + this.productPickerMode.Value + "&paramWhere=A.PurchaseCurrsamadenganpetik" + _stcAdjustHd.CurrCode + "petik','_popSearch','width=500,height=300,toolbar=0,location=0,status=0,scrollbars=1');return false;";
            String spawnJS = "<script language='JavaScript'>\n";
            ////////////////////DECLARE FUNCTION FOR CATCHING PRODUCT SEARCH
            spawnJS += "function findProduct(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.productCode.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.getElementById('" + this.productName.ClientID + "').value = dataArray [1];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            ///////////////////FUNCTION FOR KEYPRESS ENTER ON PRODUCTCODE
            spawnJS += "function enterProductCode() {\n";
            spawnJS += "document.getElementById('" + this.productName.ClientID + "').focus();\n";
            spawnJS += "return false;\n";
            spawnJS += "}\n";

            spawnJS += "</script>\n";
            this.javascriptReceiver.Text = spawnJS;

            this.productCode.Attributes.Add("onkeydown", "if(event.keyCode==13){return enterProductCode();}");

        }
    }
    protected void productCode_TextChanged(object sender, EventArgs e)
    {
        SearchBL _searchBL = new SearchBL();
        this.productName.Text = _searchBL.getProductData(this.productCode.Text);
    }

    public String ProductCode
    {
        get
        {
            return this.productCode.Text;
        }
        set
        {
            this.productCode.Text = value;
        }
    }

    public String ProductName
    {
        get
        {
            return this.productName.Text;
        }
        set
        {
            this.productName.Text = value;
        }
    }

}
