using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.IO;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.ContractTemplate
{
    public partial class ContractTemplateAdd : ContractTemplateBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private ContractTemplateBL _contractBL = new ContractTemplateBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ClearData();
                this.SetAttribute();
            }
        }


        protected void SetAttribute()
        {
            //this.DateTextBox.Attributes.Add("ReadOnly", "True");
            //this.CompanyNameTextBox.Attributes.Add("ReadOnly", "True");
        }

        protected void ClearData()
        {
            this.WarningLabel.Text = "";
            DateTime _now = DateTime.Now;

            this.TemplateNameTextBox.Text = "";
            this.FileNameHiddenField.Value = "";
            this.FgActiveCheckBox.Checked = true;
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.FileNameHiddenField.Value != "")
            {
                BILMsContractTemplateHd _contractTemplate = new BILMsContractTemplateHd();

                _contractTemplate.TemplateID = _contractBL.GetMaxTemplateID();
                _contractTemplate.TemplateName = this.TemplateNameTextBox.Text;
                _contractTemplate.TemplateFileName = this.FileNameHiddenField.Value;
                _contractTemplate.FgActive = this.FgActiveCheckBox.Checked;

                String _result = _contractBL.AddContractTemplateHd(_contractTemplate);

                if (_result == "")
                {
                    Response.Redirect(_homePage);
                }
                else
                {
                    this.WarningLabel.Text = "Your Failed Add Data";
                }
            }
            else
            {
                this.WarningLabel.Text = "File Must Be Specified";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            this.WarningLabel.Text = "";
            this.PathFileLabel.Text = "";

            if (this.FileNameFileUpload.PostedFile.FileName.Trim() != "")
            {
                FileInfo _fileInfo = new FileInfo(this.FileNameFileUpload.PostedFile.FileName);

                if (_fileInfo.Name != "")
                {
                    string[] _validExtension = ApplicationConfig.DocFileExtension.Split(',');

                    foreach (string _temp in _validExtension)
                    {
                        if (_fileInfo.Extension == _temp)
                        {
                            while (true)
                            {
                                String _fileName = "Contract - " + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

                                //validasi size
                                if (Convert.ToDecimal(FileNameFileUpload.PostedFile.ContentLength) < Convert.ToInt32(ApplicationConfig.DocMaxSize))
                                {
                                    this.FileNameFileUpload.PostedFile.SaveAs(ApplicationConfig.ContractTemplateImportPath + _fileName + _fileInfo.Extension);
                                    this.FileNameHiddenField.Value = _fileName + _fileInfo.Extension;

                                    this.PathFileLabel.Text = this.FileNameFileUpload.PostedFile.FileName;

                                    //this.WorksheetDropDownList.Items.Clear();
                                    //string[] _result2 = this.GetExcelSheetNames(ApplicationConfig.SMSImportPath + _fileName + _fileInfo.Extension);
                                    //foreach (string _sheetName in _result2)
                                    //{
                                    //    this.WorksheetDropDownList.Items.Add(_sheetName);
                                    //}
                                    //this.WorksheetDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

                                    this.WarningLabel.Text = "";
                                }
                                else
                                {
                                    this.WarningLabel.Text = "File Only Allow Only 10 MegaBytes";
                                }

                                break;
                            }

                            break;
                        }
                        else
                        {
                            this.WarningLabel.Text = "File Must Be Excel Extension";
                        }
                    }
                }
                else
                {
                    this.WarningLabel.Text = "File Name Not Valid";
                }
            }
            else
            {
                this.WarningLabel.Text = "File Name Must be Filled";
            }
        }
    }
}