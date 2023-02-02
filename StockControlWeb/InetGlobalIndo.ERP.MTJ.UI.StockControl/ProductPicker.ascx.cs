using System;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.CustomControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;

public partial class ProductPicker : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.btnSearch.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProduct&configCode=" + this.productPickerMode.Value + "','_popSearch','width=500,height=300,toolbar=0,location=0,status=0,scrollbars=1');return false;";
            String spawnJS = "<script language='JavaScript'>\n";
            ////////////////////DECLARE FUNCTION FOR CATCHING PRODUCT SEARCH
            spawnJS += "function findProduct(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.productCode.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.getElementById('" + this.productName.ClientID + "').value = dataArray [1];\n";
            spawnJS += "document.getElementById('" + this.UnitHiddenField.ClientID + "').value = dataArray [3];\n";
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
        if (this.productCode.Text != "")
        {
            this.productName.Text = _searchBL.getProductData(this.productCode.Text);
            this.UnitHiddenField.Value = new ProductBL().GetSingleProduct(this.productCode.Text).Unit;
        }
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

    public String UnitCode
    {
        get
        {
            return this.UnitHiddenField.Value;
        }
        set
        {
            this.UnitHiddenField.Value = value;
        }
    }
}
