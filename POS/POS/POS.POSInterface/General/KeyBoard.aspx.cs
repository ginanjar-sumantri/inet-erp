using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

public partial class General_JoinJobOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack == true)
        {
            String spawnJS = "<script language='JavaScript'>\n";
            spawnJS += "function returnValue (val) {\n";
            spawnJS += "window.opener." + Request.QueryString["valueCatcher"] + "(val);\n";
            spawnJS += "window.close();\n";
            spawnJS += "}\n";

            spawnJS += "function allownumbers(e){\n";
            spawnJS += "var key = window.event ? e.keyCode : e.which;\n";
            spawnJS += "var keychar = String.fromCharCode(key);\n";
            spawnJS += "var reg = new RegExp('[0-9.]');\n";
            spawnJS += "if (key == 8){\n";
            spawnJS += "keychar = String.fromCharCode(key);\n";
            spawnJS += "}\n";
            spawnJS += "if (key == 13){\n";
            spawnJS += "key=8;\n";
            spawnJS += "keychar = String.fromCharCode(key);\n";
            spawnJS += "}\n";
            spawnJS += "return reg.test(keychar);\n";
            spawnJS += "}\n";


            spawnJS += "</script>";
            this.javaScriptDeclaration.Text = spawnJS;

            this.CancelButton.OnClientClick = "window.close()";
            this.SetData();
        }
    }

    public void SetData()
    {
        if (Request.QueryString["titleinput"].ToString() != "")
        {
            if (Request.QueryString["titleinput"].ToString() == "Password")
            {
                this.InputTextBox.Attributes.Add("onfocus", "this.type='password';");
                this.InputTextBox.Attributes.Add("onblur", "this.type='password';");
            }
            this.InputTextLiteral.Text = Request.QueryString["titleinput"].ToString();

            if (this.InputTextLiteral.Text == "Phone" | this.InputTextLiteral.Text == "PAX" | this.InputTextLiteral.Text == "Cash In Hand" | this.InputTextLiteral.Text == "Weight" | this.InputTextLiteral.Text == "Discount" | this.InputTextLiteral.Text == "TRS" | this.InputTextLiteral.Text == "Other Surcharge" | this.InputTextLiteral.Text == "Other Fee" | this.InputTextLiteral.Text == "Insurance Cost" | this.InputTextLiteral.Text == "Packaging Cost" | this.InputTextLiteral.Text == "PPN" | this.InputTextLiteral.Text == "Total Guest" | this.InputTextLiteral.Text == "Basic Fare" | this.InputTextLiteral.Text == "Buying Price" | this.InputTextLiteral.Text == "Counter")
            {
                this.ChangeVisiblePanel(1);
                this.InputTextBox.Attributes.Add("onkeypress", "javascript:return allownumbers(event);");
                this.InputTextBox.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id);");
            }
            else
            {
                this.ChangeVisiblePanel(0);
            }
        }
        else
            this.InputTextLiteral.Text = "Input Text :";

        this.InputTextBox.Text = "";
        this.InputTextBox.Focus();
    }

    protected void errorhandler(Exception ex)
    {
        //ErrorHandler.Record(ex, EventLogEntryType.Error);
        String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "JOINJOBORDER");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
    }

    protected void OKButton_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "returnValue('" + this.InputTextBox.Text.Replace("'", "\\'") + "');", true);
    }

    protected void ChangeVisiblePanel(int _prmValue)
    {
        this.Keyboard.Visible = false;
        this.KeyboardNumberPanel.Visible = false;

        if (_prmValue == 0)
        {
            this.Keyboard.Visible = true;
        }
        else if (_prmValue == 1)
        {
            this.KeyboardNumberPanel.Visible = true;
        }
    }

}
