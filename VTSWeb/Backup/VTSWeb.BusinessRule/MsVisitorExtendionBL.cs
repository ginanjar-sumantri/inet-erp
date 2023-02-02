using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Web.UI.WebControls;
using System.IO;
using System.Transactions;
using System.Net.Mail;
using System.Web;
using VTSWeb.Common;
using VTSWeb.DataMapping;
using VTSWeb.Enum;
using VTSWeb.Database;
using VTSWeb.SystemConfig;


namespace VTSWeb.BusinessRule
{
    public sealed class MsVisitorExtensionBL : Base
    {
        public MsVisitorExtensionBL ()
        {
        }
        ~MsVisitorExtensionBL()
        {
        }

        #region Area
        public int RowsCount(String _prmCustCode)
        {
            int _result = 0;

            String _pattern = "%%";

            if (_prmCustCode != "")
            {
                _pattern = "%" + _prmCustCode + "%";
            }

            _result = (from _master_CustContactExtension in this.db.master_CustContactExtensions
                       join _msCustomer in this.db.MsCustomers
                       on _master_CustContactExtension.CustCode equals _msCustomer.CustCode
                       where
                      (SqlMethods.Like(_msCustomer.CustName.ToString().Trim().ToLower(), _pattern.Trim().ToLower()))

                       select _master_CustContactExtension.CustCode).Count();
            return _result;
        }


        public List<master_CustContactExtension> GetList(int _prmReqPage, int _prmPageSize, String _prmCustCode)
        {
            List<master_CustContactExtension> _result = new List<master_CustContactExtension>();
            
            String _pattern = "%%";

            if (_prmCustCode != "")
            {
                _pattern = "%" + _prmCustCode + "%";
            }
            try
            {
                var _query = (
                                from _master_CustContactExtension in this.db.master_CustContactExtensions
                                join _msCustomer in this.db.MsCustomers
                                on _master_CustContactExtension.CustCode equals _msCustomer.CustCode
                                where
                                (SqlMethods.Like(_msCustomer.CustCode.ToString().Trim().ToLower(), _pattern.Trim().ToLower()))

                                orderby _msCustomer.CustName ascending
                                select new
                                {
                                    CustCode        = _master_CustContactExtension.CustCode,

                                    CustName        = (from _msCust in this.db.MsCustomers
                                                    where _master_CustContactExtension.CustCode == _msCust.CustCode
                                                    select _msCust.CustName).FirstOrDefault(),

                                    ItemNo          = _master_CustContactExtension.ItemNo,
                                    ContactName     = (from _msCustContact in this.db.MsCustContacts
                                                     where _master_CustContactExtension.CustCode == _msCustContact.CustCode
                                                     && _master_CustContactExtension.ItemNo == _msCustContact.ItemNo
                                                     select _msCustContact.ContactName).FirstOrDefault(),

                                    CustomerPhoto   = _master_CustContactExtension.CustomerPhoto
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new master_CustContactExtension(_row.CustCode, _row.CustName, _row.ItemNo, _row.ContactName, _row.CustomerPhoto));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public List<master_CustContactExtension> GetList()
        {
            List<master_CustContactExtension> _result = new List<master_CustContactExtension>();

            try
            {
                var _query = (
                                from _master_CustContactExtension in this.db.master_CustContactExtensions
                                orderby _master_CustContactExtension.CustCode, _master_CustContactExtension.ItemNo descending
                                select new
                                {
                                    CustCode    = _master_CustContactExtension.CustCode,

                                    CustName    = (from _msCust in this.db.MsCustomers
                                                   where _master_CustContactExtension.CustCode == _msCust.CustCode
                                                   select _msCust.CustName).FirstOrDefault(),

                                    ItemNo      = _master_CustContactExtension.ItemNo,
                                    ContactName = (from _msCustContact in this.db.MsCustContacts
                                                   where _master_CustContactExtension.CustCode == _msCustContact.CustCode
                                                   && _master_CustContactExtension.ItemNo == _msCustContact.ItemNo
                                                   select _msCustContact.ContactName).FirstOrDefault(),

                                    CustomerPhoto = _master_CustContactExtension.CustomerPhoto
                               
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new master_CustContactExtension(_row.CustCode, _row.CustName, _row.ItemNo, _row.ContactName, _row.CustomerPhoto));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public bool DeleteMulti(String[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');
                    master_CustContactExtension _master_CustContactExtension = this.db.master_CustContactExtensions.Single(_temp => _temp.CustCode.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.ItemNo.ToString() == _tempSplit[1].Trim().ToLower());

                    this.db.master_CustContactExtensions.DeleteOnSubmit(_master_CustContactExtension);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public master_CustContactExtension GetSingle(string _prmCode)
        {
            master_CustContactExtension _result = null;

            try
            {
                String[] _tempSplit = _prmCode.Split('-');
                _result = this.db.master_CustContactExtensions.Single(_temp => _temp.CustCode.ToLower() == _tempSplit[0].Trim().ToLower() && _temp.ItemNo.ToString() == _tempSplit[1].Trim().ToLower());
            }
            catch (Exception)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        public master_CustContactExtension GetSingleForClearance(string _prmCustCode, int _prmItemNo)
        {
            master_CustContactExtension _result = null;

            try
            {
                _result = this.db.master_CustContactExtensions.Single(_temp => _temp.CustCode.ToLower() == _prmCustCode.Trim().ToLower() && _temp.ItemNo == _prmItemNo);
            }
            catch (Exception)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        public master_CustContactExtension GetSinglePicture(string _prmCode)
        {
            master_CustContactExtension _result = null;

            try
            {
                String[] _tempSplit = _prmCode.Split('-');
                _result = this.db.master_CustContactExtensions.Single(_temp => _temp.CustCode.ToLower() == _tempSplit[0].Trim().ToLower() && _temp.ItemNo.ToString() == _tempSplit[1].Trim().ToLower());
            }
            catch (Exception)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String Add(master_CustContactExtension _prmCustContactExtension, FileUpload _prmFileUpload, String _imagePath)
        {
            String _result = "";

            try
            {
                String _result2 = "";

                //if (_prmMasterLiveAuctionSetup != null)
                //{
                //    if (_prmMasterLiveAuctionSetup.StartDate < DateTime.Now)
                //    {
                //        _result2 = "Tanggal Awal harus lebih besar dari Tanggal Berakhir dan Tanggal hari ini";
                //    }
                //    if (_prmMasterLiveAuctionSetup.EndDate < _prmMasterLiveAuctionSetup.StartDate)
                //    {
                //        _result2 = "Tanggal Awal harus lebih besar dari Tanggal Berakhir dan Tanggal hari ini";
                //    }
                //}
                //else if (_prmMasterLowestUniqueAuctionSetup != null)
                //{
                //    if (_prmMasterLowestUniqueAuctionSetup.StartDate < DateTime.Now)
                //    {
                //        _result2 = "Tanggal Awal harus lebih besar dari Tanggal Berakhir dan Tanggal hari ini";
                //    }
                //    if (_prmMasterLowestUniqueAuctionSetup.EndDate < _prmMasterLowestUniqueAuctionSetup.StartDate)
                //    {
                //        _result2 = "Tanggal Awal harus lebih besar dari Tanggal Berakhir dan Tanggal hari ini";
                //    }
                //}
                //else if (_prmMasterRapidAuctionSetup != null)
                //{
                //    if (_prmMasterRapidAuctionSetup.StartDate < DateTime.Now)
                //    {
                //        _result2 = "Tanggal Awal harus lebih besar dari Tanggal Berakhir dan Tanggal hari ini";
                //    }
                //    if (_prmMasterRapidAuctionSetup.EndDate < _prmMasterRapidAuctionSetup.StartDate)
                //    {
                //        _result2 = "Tanggal Awal harus lebih besar dari Tanggal Berakhir dan Tanggal hari ini";
                //    }
                //}

                if (_result2 == "")
                {
                     String _prmFotoCode = _prmCustContactExtension.CustCode + "-" + _prmCustContactExtension.ItemNo.ToString();
                     _result = this.UploadProductPicture("", _prmFotoCode, _prmCustContactExtension.CustomerPhoto, _prmFileUpload, "Add");

                    if (_result == "")
                    {
                        //foreach (spBidHaven_ProductAutoNmbrResult _item in this.db.spBidHaven_ProductAutoNmbr(DateTime.Now.Year, DateTime.Now.Month, GameTypeDataMapper.GetGameTypeInitial(_prmMaster_Product.GameType), Convert.ToInt32(_prmMaster_Product.GameType)))
                        //{
                        //    _prmMaster_Product.ProductID = _item.Number;
                        //}

                        String _path = _prmFileUpload.PostedFile.FileName;
                        FileInfo _file = new FileInfo(_path);
                        
                        String _newImagepath = _imagePath + _prmFotoCode.ToString() + _file.Extension;

                        _prmFileUpload.PostedFile.SaveAs(_newImagepath);

                        _prmCustContactExtension.CustomerPhoto = _prmFotoCode.ToString() + _file.Extension;

                        this.db.master_CustContactExtensions.InsertOnSubmit(_prmCustContactExtension);

                        //if (_prmMasterLiveAuctionSetup != null)
                        //{
                        //    this.db.Master_LiveAuctionSetups.InsertOnSubmit(_prmMasterLiveAuctionSetup);
                        //}
                        //else if (_prmMasterLowestUniqueAuctionSetup != null)
                        //{
                        //    this.db.Master_LowestUniqueAuctionSetups.InsertOnSubmit(_prmMasterLowestUniqueAuctionSetup);
                        //}
                        //else if (_prmMasterRapidAuctionSetup != null)
                        //{
                        //    this.db.Master_RapidAuctionSetups.InsertOnSubmit(_prmMasterRapidAuctionSetup);
                        //}

                        //this.db.Master_ProductTimeLefts.InsertOnSubmit(_prmMasterProductTimeLeft);

                        this.db.SubmitChanges();

                        _file.Refresh();
                    }
                }
                else
                {
                    _result = _result2;
                }
            }
            catch (Exception ex)
            {
                //new ErrorLogBL(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name);
                _result = "Anda gagal menambah data";
                // ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String Edit(master_CustContactExtension _prmCustContactExtension, FileUpload _prmFileUpload, String _imagePath)
        {
            String _result = "";

            try
            {

                String _result2 = "";
                String _path = "";

                if (_result == "")
                {
                    _path = _prmFileUpload.PostedFile.FileName;
                    String _prmFotoCode = _prmCustContactExtension.CustCode + "-" + _prmCustContactExtension.ItemNo.ToString();

                    if (_path != "")
                    {
                        _result2 = this.UploadProductPicture("", _prmFotoCode.ToString(), _prmCustContactExtension.CustomerPhoto, _prmFileUpload, "Edit");

                        if (_result2 == "")
                        {
                            FileInfo _file = new FileInfo(_path);
                            //String _imagepath = ApplicationConfig.PhotoPicturePath + _prmFotoCode.ToString() + _file.Extension;
                            
                            String _newImagepath = _imagePath + _prmFotoCode.ToString() + _file.Extension;
                            _prmFileUpload.PostedFile.SaveAs(_newImagepath);

                            _prmCustContactExtension.CustomerPhoto = _prmFotoCode.ToString() + _file.Extension;

                            _file.Refresh();
                        }
                        else
                        {
                            _result = _result2;
                        }
                    }

                    if (_result2 == "")
                    {
                        this.db.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                _result = "Ubah data gagal";
            }

            return _result;
        }


        private string UploadProductPicture(string p, string _prmFotoCode, System.Data.Linq.Binary binary, FileUpload _prmFileUpload, string p_5)
        {
            throw new NotImplementedException();
        }

        public String UploadProductPicture(String _prmCustCode, String _prmItemCode, String _prmPicture, FileUpload _prmFileUpload, string _prmAction)
        {
            String _result = "";

            String _path = _prmFileUpload.PostedFile.FileName;

            if (_path == "" && _prmAction == "Add")
            {
                _result = "Image must be filled";
            }
            //else if (_path == "" && _prmAction == "AddCopy")
            //{
            //    _path = ApplicationConfig.ProductPicturePath + this.GetSingle(_prmProductCodeOld).Picture;

            //    FileInfo _file = new FileInfo(_path);
            //    String _imagepath = ApplicationConfig.ProductPicturePath + _prmProductCode + _file.Extension;

            //    if (PictureHandler.IsPictureFile(_path, ApplicationConfig.ImageExtension) == true)
            //    {
            //        _file.CopyTo(_imagepath, true);
            //    }
            //    else
            //    {
            //        _result = "File type not supported";
            //    }
            //}
            else
            {
                FileInfo _file = new FileInfo(_path);
                String _imagepath = ApplicationConfig.PhotoPicturePath + _prmCustCode+"-"+_prmItemCode + _file.Extension;

                if (_path == "")
                {
                    _result = "Invalid filename supplied";
                }
                if (_prmFileUpload.PostedFile.ContentLength == 0)
                {
                    _result = "Invalid file content";
                }
                if (_result == "")
                {
                    if (PictureHandler.IsPictureFile(_path, ApplicationConfig.ImageExtension) == true)
                    {
                        System.Drawing.Image _uploadedImage = System.Drawing.Image.FromStream(_prmFileUpload.PostedFile.InputStream);

                        Decimal _width = Convert.ToDecimal(_uploadedImage.PhysicalDimension.Width);
                        Decimal _height = Convert.ToDecimal(_uploadedImage.PhysicalDimension.Height);

                        if (_width > Convert.ToDecimal(ApplicationConfig.ImageWidth) || _height > Convert.ToDecimal(ApplicationConfig.ImageHeight))
                        {
                            _result = "This image is too big - please resize it!";
                        }
                        else
                        {
                            if (_prmFileUpload.PostedFile.ContentLength <= Convert.ToDecimal(ApplicationConfig.ImageMaxSize))
                            {
                                if (_prmPicture != ApplicationConfig.ImageDefault)
                                {
                                    if (File.Exists(ApplicationConfig.PhotoPicturePath + _prmPicture) == true)
                                    {
                                        File.Delete(ApplicationConfig.PhotoPicturePath + _prmPicture);
                                    }
                                }
                            }
                            else
                            {
                                _result = "Unable to upload, file exceeds maximum limit";
                            }
                        }
                    }
                    else
                    {
                        _result = "File type not supported";
                    }
                }
            }
            return _result;
        }


        public bool Edit2(master_CustContactExtension _prmmaster_CustContactExtension)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }


        #endregion

    }
}
