using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule.POSInterface;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Threading;

namespace POS.POSInterface.Cafe
{
    public partial class JoinTable : POSCafeBase
    {
        private POSCafeBL _posCafeBL = new POSCafeBL();
        private POSTrCafeBookingBL _posCafeBookingBL = new POSTrCafeBookingBL();
        private List<POSMsInternetTable> _list1 = new List<POSMsInternetTable>();
        private List<POSMsInternetTable> _list2 = new List<POSMsInternetTable>();
        private POSInternetBL _internetBL = new POSInternetBL();
            
        private int _no = 0;
        //private int _nomor = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack == true)
                {
                    //this.ClearData();
                    this.FloorButtonRepeater.DataSource = this._posCafeBL.GetList(Convert.ToInt16(this.floorPagingPageHiddenField.Value), 4);
                    this.FloorButtonRepeater.DataBind();
                    this.ClearListDetail2();
                    this.ShowDetail("CLantai 1-1");
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        //private void ClearData()
        //{
        //    this.ReferencesNumberTextBox.Text = "";
        //}

        private void ClearListDetail2()
        {
            this.ListRepeater2.DataSource = null;
            this.ListRepeater2.DataBind();
        }

        private void DisplayData()
        {
            this._list1.Sort((x, y) => (x.TableIDPerRoom.CompareTo(y.TableIDPerRoom)));

            this.ListRepeater.DataSource = this._list1;
            this.ListRepeater.DataBind();

            //this._list2.Sort((x, y) => (x.TableIDPerRoom.CompareTo(y.TableIDPerRoom)));

            this.ListRepeater2.DataSource = this._list2;
            this.ListRepeater2.DataBind();
        }

        private void FillListFromHiddenField()
        {
            if (this.List1HiddenField.Value != "")
            {
                String[] _dataRow = this.List1HiddenField.Value.Split('^');
                foreach (var _item in _dataRow)
                {
                    String[] _field = _item.Split(',');
                    this._list1.Add(new POSMsInternetTable(_field[0], Convert.ToInt32(_field[1]), Convert.ToInt32(_field[2])));
                }
            }

            if (this.List2HiddenField.Value != "")
            {
                String[] _dataRow2 = this.List2HiddenField.Value.Split('^');
                foreach (var _item2 in _dataRow2)
                {
                    String[] _field2 = _item2.Split(',');
                    this._list2.Add(new POSMsInternetTable(_field2[0], Convert.ToInt32(_field2[1]), Convert.ToInt32(_field2[2])));
                }
            }
        }

        private void AssignHiddenField()
        {
            String _strAccumData1 = "";
            foreach (var _item in this._list1)
            {
                if (_strAccumData1 == "")
                {
                    _strAccumData1 = _item.FloorType + "," + _item.FloorNmbr + "," + _item.TableIDPerRoom;
                }
                else
                {
                    _strAccumData1 += "^" + _item.FloorType + "," + _item.FloorNmbr + "," + _item.TableIDPerRoom;
                }
            }
            this.List1HiddenField.Value = _strAccumData1;

            String _strAccumData2 = "";
            foreach (var _item in this._list2)
            {
                if (_strAccumData2 == "")
                {
                    _strAccumData2 = _item.FloorType + "," + _item.FloorNmbr + "," + _item.TableIDPerRoom;
                }
                else
                {
                    _strAccumData2 += "^" + _item.FloorType + "," + _item.FloorNmbr + "," + _item.TableIDPerRoom;
                }
            }
            this.List2HiddenField.Value = _strAccumData2;
        }

        protected void FloorButtonRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            POSMsInternetFloor _temp = (POSMsInternetFloor)e.Item.DataItem;

            Button _floorButton = (Button)e.Item.FindControl("FloorButton");
            _floorButton.Text = _temp.FloorName;
            _floorButton.ToolTip = _temp.roomCode;
            _floorButton.CommandArgument = _temp.FloorName + "-" + _temp.FloorNmbr.ToString();
            _floorButton.CommandName = "CafeLantai";
        }

        protected void Floorbutton_onclick(object sender, EventArgs e)
        {
            Button _temp = (Button)sender;
            //String[] _splitButton = _temp.CommandArgument.Split('-');
            //this.currFloorHiddenField.Value = _splitButton[1];
            //this.selectedtablehiddenfield.value = _temp.tooltip;
            //this.currfloornamehiddenfield.value = _temp.text;
            this.ShowDetail(_temp.CommandArgument);
        }

        protected void ShowDetail(String _prmArgument)
        {
            try
            {
                string[] _tempSplit = _prmArgument.Split('-');
                DateTime _date = DateTime.Now;
                DateTime _date2 = _date.Date;
                int _hour = _date.Hour;
                int _minute = _date.Minute;

                this._list1 = _posCafeBookingBL.GetListUnReservedTableJoinTable(_date2, Convert.ToInt32(_tempSplit[1]), _hour, _minute, _hour + 1, _minute);
                this.ListRepeater.DataSource = this._list1;
                this.ListRepeater.DataBind();

                this.AssignHiddenField();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                POSMsInternetTable _temp = (POSMsInternetTable)e.Item.DataItem;

                string _code = _temp.FloorType;

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no += 1;
                //_no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                //_nomor += 1;

                Literal _floorLiteral = (Literal)e.Item.FindControl("FloorLiteral");
                _floorLiteral.Text = HttpUtility.HtmlEncode(_temp.FloorNmbr.ToString());

                Literal _divisiLiteral = (Literal)e.Item.FindControl("TableLiteral");
                _divisiLiteral.Text = HttpUtility.HtmlEncode(_temp.TableIDPerRoom.ToString());

                ImageButton _pickButton = (ImageButton)e.Item.FindControl("PickImageButton");
                //_pickButton.ImageUrl = ApplicationConfig.POSInterfaceWebAppURL + "images/arrowright.gif";
                _pickButton.CommandArgument = _code + "," + _temp.TableIDPerRoom + "," + _temp.FloorNmbr + "," + e.Item.ItemIndex;
                _pickButton.CommandName = "PickDetail";
            }
        }

        protected void ListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "PickDetail")
            {
                String[] _break = e.CommandArgument.ToString().Split(',');
                String _floorType = _break[0];
                String _tableIdPerRoom = _break[1];
                String _floorNmbr = _break[2];
                String _index = _break[3];

                this.FillListFromHiddenField();

                this._list2.Add(this._list1.Find(_temp => _temp.FloorType == _floorType && _temp.TableIDPerRoom == Convert.ToInt32(_tableIdPerRoom) && _temp.FloorNmbr == Convert.ToInt32(_floorNmbr)));

                this._list1.RemoveAt(Convert.ToInt32(_index));

                this.AssignHiddenField();

                this.DisplayData();
            }
        }

        protected void ListRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                POSMsInternetTable _temp = (POSMsInternetTable)e.Item.DataItem;

                string _code = _temp.FloorType.ToString();

                //HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate2");
                //_tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                //_tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

                Literal _floorLiteral = (Literal)e.Item.FindControl("FloorLiteral");
                _floorLiteral.Text = HttpUtility.HtmlEncode(_temp.FloorNmbr.ToString());

                Literal _transNmbrLiteral = (Literal)e.Item.FindControl("TableLiteral");
                _transNmbrLiteral.Text = HttpUtility.HtmlEncode(_temp.TableIDPerRoom.ToString());

                ImageButton _pickButton = (ImageButton)e.Item.FindControl("ResetPickImageButton");
                //_pickButton.ImageUrl = ApplicationConfig.POSInterfaceWebAppURL + "images/arrowleft.gif";
                _pickButton.CommandArgument = _code + "," + _temp.TableIDPerRoom + "," + _temp.FloorNmbr + "," + e.Item.ItemIndex;
                _pickButton.CommandName = "ResetPick";
            }
        }

        protected void ListRepeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ResetPick")
            {
                String[] _break = e.CommandArgument.ToString().Split(',');
                String _floorType = _break[0];
                String _tableIdPerRoom = _break[1];
                String _floorNmbr = _break[2];
                String _index = _break[3];

                this.FillListFromHiddenField();

                this._list1.Add(this._list2.Find(_temp => _temp.FloorType == _floorType && _temp.TableIDPerRoom == Convert.ToInt32(_tableIdPerRoom) && _temp.FloorNmbr == Convert.ToInt32(_floorNmbr)));

                this._list2.RemoveAt(Convert.ToInt32(_index));

                //this.AssignHiddenField();

                this.DisplayData();
            }
        }

        protected void CancelImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearListDetail2();
            this.ShowDetail("CLantai 1-1");
        }

        protected void BackImageButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                String _floor = _internetBL.GetSinglePOSMsInternetFloor("Cafe");
                Response.Redirect("POSCafe.aspx" + "?" + this._selectedFloorKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_floor, ApplicationConfig.EncryptionKey)));
                //Response.Redirect(this._homePage + "?selectedFloor=");
            }
            catch (ThreadAbortException) { throw; }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void SaveImageButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (this.List2HiddenField.Value != "")
                {
                    String _tableJoin = "";
                    String _floorJoin = "";
                    String[] _break = this.List2HiddenField.Value.ToString().Split('^');
                    int num = 0;
                    foreach (var _item in _break)
                    {
                        String _table = _break[num];
                        String[] _break2 = _table.ToString().Split(',');
                        if (_tableJoin == "")
                        {
                            _tableJoin = _break2[2];
                            _floorJoin = this._posCafeBL.GetRoomCode(Convert.ToInt32(_break2[1]));
                        }
                        else
                        {
                            _tableJoin = _tableJoin + "," + _break2[2];
                        }
                        num++;
                    }

                    Response.Redirect(this._chooseTablePage + "?" + this._selectedTableKey + "=" +
                            HttpUtility.UrlEncode(Rijndael.Encrypt(_tableJoin, ApplicationConfig.EncryptionKey)) +
                            "&" + this._selectedFloorKey + "=" +
                            HttpUtility.UrlEncode(Rijndael.Encrypt(_floorJoin, ApplicationConfig.EncryptionKey)) +
                            "&" + this._referenceNo + "=" +
                            HttpUtility.UrlEncode(Rijndael.Encrypt("", ApplicationConfig.EncryptionKey))
                            );
                }
            }
            catch (ThreadAbortException) { throw; }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void errorhandler(Exception ex)
        {
            //ErrorHandler.Record(ex, EventLogEntryType.Error);
            String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "CAFE");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
        }
    }
}