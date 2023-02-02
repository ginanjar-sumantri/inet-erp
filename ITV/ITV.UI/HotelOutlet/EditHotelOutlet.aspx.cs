using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITV.UI.ApplicationClass;
using ITV.Common;
using ITV.BusinessRule;
using ITV.DataAccess.ITVDatabase;

namespace ITV.UI.HotelOutlet
{
    public partial class EditHotelOutlet : HotelOutletBase
    {
        HotelOutletBL _hotelOutletBL = new HotelOutletBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.ClearLabel();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ClearData()
        {
            this.HotelOutletNameTextBox.Text = "";
            this.DescTextBox.Text = "";
        }

        protected void ShowData()
        {
            MsHotelOutlet _msHotelOutlet = this._hotelOutletBL.GetSingleHotelOutlet(Convert.ToInt64(this._nvcExtractor.GetValue(_codeKey)));

            this.HotelOutletNameTextBox.Text = _msHotelOutlet.HotelOutletName;
            this.DescTextBox.Text = _msHotelOutlet.HotelOutletDesc;            
        }

        protected void SaveImageButton_Click(object sender, ImageClickEventArgs e)
        {
            MsHotelOutlet _msHotelOutlet = this._hotelOutletBL.GetSingleHotelOutlet(Convert.ToInt64(this._nvcExtractor.GetValue(_codeKey)));

            _msHotelOutlet.HotelOutletName = this.HotelOutletNameTextBox.Text;
            _msHotelOutlet.HotelOutletDesc = this.DescTextBox.Text;
            _msHotelOutlet.VideoFile = this.VideoFileUpload.FileName;
            _msHotelOutlet.ImageFile = this.ImageFileUpload.FileName;

            Boolean _result = this._hotelOutletBL.EditHotelOutlet(_msHotelOutlet);

            if (_result == true)
            {
                this.WarningLabel.Text = "You Successfully Changed Data";
            }
            else
            {
                this.WarningLabel.Text = "You Failed Changed Data";
            }
        }

        protected void ResetImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }

        protected void BackImageButton_Click(object sender, ImageClickEventArgs e)
        {

        }
    }
}