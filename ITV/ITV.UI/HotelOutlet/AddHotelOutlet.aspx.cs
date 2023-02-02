using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITV.UI.ApplicationClass;
using ITV.DataAccess.ITVDatabase;
using ITV.BusinessRule;

namespace ITV.UI.HotelOutlet
{
    public partial class AddHotelOutlet : HotelOutletBase
    {
        HotelOutletBL _hotelOutletBL = new HotelOutletBL();

        protected void Page_Load(object sender, EventArgs e)
        {
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

        protected void SaveImageButton_Click(object sender, ImageClickEventArgs e)
        {
            MsHotelOutlet _msHotelOutlet = new MsHotelOutlet();

            _msHotelOutlet.HotelOutletName = this.HotelOutletNameTextBox.Text;
            _msHotelOutlet.HotelOutletDesc = this.DescTextBox.Text;
            _msHotelOutlet.VideoFile = this.VideoFileUpload.FileName;
            _msHotelOutlet.ImageFile = this.ImageFileUpload.FileName;
            _msHotelOutlet.RowStatus = 0;
            _msHotelOutlet.CreatedBy = "Anjar";
            _msHotelOutlet.CreatedDate = DateTime.Now;
            _msHotelOutlet.ModifiedBy = "Anjar";
            _msHotelOutlet.ModifiedDate = DateTime.Now;

            Boolean _result = this._hotelOutletBL.AddHotelOutlet(_msHotelOutlet);

            if (_result == true)
            {
                this.WarningLabel.Text = "You Successfully Insert Data";
            }
            else
            {
                this.WarningLabel.Text = "You Failed Insert Data";
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