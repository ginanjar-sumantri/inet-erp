using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common;
using System.IO;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.ContractTemplate
{
    public partial class ContractTemplateEdit : ContractTemplateBase
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

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/Save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.SetAttribute();

                this.ShowData();
            }
        }

        protected void SetAttribute()
        {
            //this.DateTextBox.Attributes.Add("ReadOnly", "True");
        }

        public void ShowData()
        {
            BILMsContractTemplateHd _bilMsContractTemplateHd = _contractBL.GetSingleContractTemplateHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TemplateNameTextBox.Text = _bilMsContractTemplateHd.TemplateName;
            this.FileNameHiddenField.Value = _bilMsContractTemplateHd.TemplateFileName;
            this.PathFileLabel.Text = _bilMsContractTemplateHd.TemplateFileName;
            this.FgActiveCheckBox.Checked = _bilMsContractTemplateHd.FgActive;
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            BILMsContractTemplateHd _bilMsContractTemplateHd = _contractBL.GetSingleContractTemplateHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _bilMsContractTemplateHd.TemplateName = this.TemplateNameTextBox.Text;
            _bilMsContractTemplateHd.TemplateFileName = this.FileNameHiddenField.Value;
            _bilMsContractTemplateHd.FgActive = this.FgActiveCheckBox.Checked;

            bool _result = _contractBL.EditContractTemplateHd(_bilMsContractTemplateHd);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.Page.IsValid == true)
            {
                BILMsContractTemplateHd _bilMsContractTemplateHd = _contractBL.GetSingleContractTemplateHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _bilMsContractTemplateHd.TemplateName = this.TemplateNameTextBox.Text;
                _bilMsContractTemplateHd.TemplateFileName = this.FileNameHiddenField.Value;
                _bilMsContractTemplateHd.FgActive = this.FgActiveCheckBox.Checked;

                bool _result = _contractBL.EditContractTemplateHd(_bilMsContractTemplateHd);

                if (_result == true)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.WarningLabel.Text = "Your Failed Edit Data";
                }
            }
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
                                String _fileName = "Contract - " + DateTime.Now.Year.ToString().PadLeft(4, '0') + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0') + DateTime.Now.Millisecond.ToString().PadLeft(2, '0');

                                //validasi size
                                if (Convert.ToDecimal(FileNameFileUpload.PostedFile.ContentLength) < Convert.ToInt32(ApplicationConfig.DocMaxSize))
                                {
                                    //delete old doc
                                    if (this.FileNameHiddenField.Value != _fileName + _fileInfo.Extension)
                                    {
                                        File.Delete(ApplicationConfig.ContractTemplateImportPath + this.FileNameHiddenField.Value);                                        
                                    }
                                    this.FileNameFileUpload.PostedFile.SaveAs(ApplicationConfig.ContractTemplateImportPath + _fileName + _fileInfo.Extension);
                                    this.FileNameHiddenField.Value = _fileName + _fileInfo.Extension;

                                    this.PathFileLabel.Text = this.FileNameFileUpload.PostedFile.FileName;

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