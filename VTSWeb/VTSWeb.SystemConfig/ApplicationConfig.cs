using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace VTSWeb.SystemConfig
{
    public static class ApplicationConfig
    {
        private static String _encryptionKey = Properties.Settings.Default.Encryptionkey;
        private static String _connString = Properties.Settings.Default.ConnString;
        private static String _dateForm = Properties.Settings.Default.DateForm;
        private static String _requestPageKey = Properties.Settings.Default.RequestPageKey;
        private static String _listPageSize = Properties.Settings.Default.ListPageSize;
        private static String _dataPageRange = Properties.Settings.Default.DataPagerRange;
        private static String _actionResult = Properties.Settings.Default.ActionResult;

        private static String _companyName = Properties.Settings.Default.CompanyName;
        private static String _forgotPasswordPage = Properties.Settings.Default.ForgotPasswordPage;
        private static String _cookieName = Properties.Settings.Default.CookieName;
        private static String _cookiePassword = Properties.Settings.Default.CookiePassword;
        private static String _cookieInstance = Properties.Settings.Default.CookieInstance;
        private static String _loginLifeTimeExpired = Properties.Settings.Default.LoginLifeTimeExpired;
        private static String _cookiesPreferences = Properties.Settings.Default.CookiesPreferences;
        private static String _stringSeparatorPublish = Properties.Settings.Default.StringSeparatorPublish;
        private static String _customerConnString = Properties.Settings.Default.CustomerConnString;
        private static String _rootOrganization = Properties.Settings.Default.RootOrganization;
        private static String _imageExtension = Properties.Settings.Default.ImageExtension;
        private static String _productPictureVirDirPath = Properties.Settings.Default.PhotoPicturePath;
        private static String _imageWidth = Properties.Settings.Default.ImageWidth;
        private static String _imageHeight = Properties.Settings.Default.ImageHeight;
        private static String _imageMaxSize = Properties.Settings.Default.ImageMaxSize;
        private static String _imageDefault = Properties.Settings.Default.ImageDefault;
        private static String _photoPicturePath = Properties.Settings.Default.PhotoPicturePath;
        private static String _photoPictureVirDirPath = Properties.Settings.Default.PhotoPictureVirDirPath;


        public static String RootOrganization
        {
            get
            {
                return _rootOrganization;
            }
        }
        public static String CustomerConnString
        {
            get
            {
                return _customerConnString;
            }
        }
        public static String DataPagerRange
        {
            get
            {
                return _dataPageRange;
            }
        }
        public static String ListPageSize
        {
            get
            {
                return _listPageSize;
            }
        }
        public static String RequestPageKey
        {
            get
            {
                return _requestPageKey;
            }
        }
        public static String DateForm
        {
            get
            {
                return _dateForm;
            }
        }
        public static String ConnString
        {
            get
            {
                return _connString;
            }
        }
        public static String EncryptionKey
        {
            get
            {
                return _encryptionKey;
            }
        }
        public static String ActionResult
        {
            get
            {
                return _actionResult;
            }
        }

        public static String CompanyName
        {
            get
            {
                return _companyName;
            }
        }

        public static String ForgotPassword
        {
            get
            {
                return _forgotPasswordPage;
            }
        }

        public static String CookieName
        {
            get
            {
                return _cookieName;
            }
        }

        public static String CookiePassword
        {
            get
            {
                return _cookiePassword;
            }
        }

        public static String CookieInstance
        {
            get
            {
                return _cookieInstance;
            }
        }

        public static String LoginLifeTimeExpired
        {
            get
            {
                return _loginLifeTimeExpired;
            }
        }

        public static String CookiesPreferences
        {
            get
            {
                return _cookiesPreferences;
            }
        }

        public static String StringSeparatorPublish
        {
            get
            {
                return _stringSeparatorPublish;
            }
        }
        public static String ImageExtension
        {
            get
            {
                return _imageExtension;
            }
        }
        public static String ProductPictureVirDirPath
        {
            get
            {
                return _productPictureVirDirPath;
            }
        }
        public static String ImageWidth
        {
            get
            {
                return _imageWidth;
            }
        }
        public static String ImageHeight
        {
            get
            {
                return _imageHeight;
            }
        }
        public static String ImageMaxSize
        {
            get
            {
                return _imageMaxSize;
            }
        }
        public static String ImageDefault
        {
            get
            {
                return _imageDefault;
            }
        }
        public static String PhotoPicturePath
        {
            get
            {
                return _photoPicturePath;
            }
        }
        public static String PhotoPictureVirDirPath
        {
            get
            {
                return _photoPictureVirDirPath;
            }
        }

    }
}