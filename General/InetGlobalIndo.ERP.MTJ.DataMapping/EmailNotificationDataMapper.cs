using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class EmailNotificationDataMapper
    {
        public static String GetTypeText(Byte _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "Internal";
                    break;
                case 1:
                    _result = "External";
                    break;
                default:
                    _result = "Internal";
                    break;
            }

            return _result;
        }

        public static EmailNotificationType GetType(Byte _prmValue)
        {
            EmailNotificationType _result;

            switch (_prmValue)
            {
                case 0:
                    _result = EmailNotificationType.Internal;
                    break;
                case 1:
                    _result = EmailNotificationType.External;
                    break;
                default:
                    _result = EmailNotificationType.Internal;
                    break;
            }

            return _result;
        }

        public static Byte GetType(EmailNotificationType _prmStatus)
        {
            Byte _result;

            switch (_prmStatus)
            {
                case EmailNotificationType.Internal:
                    _result = 0;
                    break;
                case EmailNotificationType.External:
                    _result = 1;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }
                
        public static String GetIDText(Byte _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "SPK Installasi";
                    break;
                case 1:
                    _result = "Eksternal Notifikasi - Berita Acara belum di tanda tangani";
                    break;
                case 2:
                    _result = "Internal Notifikasi - Berita Acara belum di tanda tangani";
                    break;
                case 3:
                    _result = "Eksternal Notifikasi - Soft Block Layanan karena Berita Acara belum di tanda tangani";
                    break;
                case 4:
                    _result = "Internal Notifikasi - Soft Block Layanan karena Berita Acara belum di tanda tangani";
                    break;
                case 5:
                    _result = "Eksternal Notifikasi – Open Soft Block Layanan karena Berita Acara sudah di tanda tangani";
                    break;
                case 6:
                    _result = "Internal Notifikasi – Open Soft Block Layanan karena Berita Acara sudah di tanda tangani";
                    break;
                case 7:
                    _result = "Eksternal Notifikasi - Kontrak belum di tanda tangani";
                    break;
                case 8:
                    _result = "Internal Notifikasi - Kontrak belum di tanda tangani";
                    break;
                case 9:
                    _result = "Eksternal Notifikasi - Soft Block Layanan karena Kontrak belum di tanda tangani";
                    break;
                case 10:
                    _result = "Internal Notifikasi - Soft Block Layanan karena Kontrak belum di tanda tangani";
                    break;
                case 11:
                    _result = "Eksternal Notifikasi – Open Soft Block Layanan karena Kontrak sudah di tanda tangani";
                    break;
                case 12:
                    _result = "Internal Notifikasi – Open Soft Block Layanan karena Kontrak sudah di tanda tangani";
                    break;
                case 13:
                    _result = "NOTPAY";
                    break;
                case 14:
                    _result = "Eks SB NOTPAY";
                    break;
                case 15:
                    _result = "Intern SB NOTPAY";
                    break;
                case 16:
                    _result = "Eks Open SB NOTPAY";
                    break;
                case 17:
                    _result = "Intern Open SB NOTPAY";
                    break;
                case 18:
                    _result = "Billing Invoice Not Yet Notified";
                    break;
                default:
                    _result = "SPK Installasi";
                    break;
            }

            return _result;
        }

        public static EmailNotificationID GetID(Byte _prmValue)
        {
            EmailNotificationID _result;

            switch (_prmValue)
            {
                case 0:
                    _result = EmailNotificationID.SPKInstallasi;
                    break;
                case 1:
                    _result = EmailNotificationID.BANotYetApproved;
                    break;
                case 2:
                    _result = EmailNotificationID.BANotYetApprovedInternal;
                    break;
                case 3:
                    _result = EmailNotificationID.BASoftBlock;
                    break;
                case 4:
                    _result = EmailNotificationID.SPKBASoftBlock;
                    break;
                case 5:
                    _result = EmailNotificationID.BAOpenSoftBlock;
                    break;
                case 6:
                    _result = EmailNotificationID.SPKBAOpenSoftBlock;
                    break;
                case 7:
                    _result = EmailNotificationID.ContractLetterNotYetApproved;
                    break;
                case 8:
                    _result = EmailNotificationID.ContractLetterNotYetApprovedInternal;
                    break;
                case 9:
                    _result = EmailNotificationID.CLSoftBlock;
                    break;
                case 10:
                    _result = EmailNotificationID.SPKCLSoftBlock;
                    break;
                case 11:
                    _result = EmailNotificationID.CLOpenSoftBlock;
                    break;
                case 12:
                    _result = EmailNotificationID.SPKCLOpenSoftBlock;
                    break;
                case 13:
                    _result = EmailNotificationID.NotPay;
                    break;
                case 14:
                    _result = EmailNotificationID.NotPaySoftBlock;
                    break;
                case 15:
                    _result = EmailNotificationID.SPKNotPaySoftBlock;
                    break;
                case 16:
                    _result = EmailNotificationID.NotPayOpenSoftBlock;
                    break;
                case 17:
                    _result = EmailNotificationID.SPKNotPayOpenSoftBlock;
                    break;
                case 18:
                    _result = EmailNotificationID.BillingInvoiceEmailNotYetSent;
                    break;
                default:
                    _result = EmailNotificationID.SPKInstallasi;
                    break;
            }

            return _result;
        }

        public static Byte GetID(EmailNotificationID _prmStatus)
        {
            Byte _result;

            switch (_prmStatus)
            {
                case EmailNotificationID.SPKInstallasi:
                    _result = 0;
                    break;
                case EmailNotificationID.BANotYetApproved:
                    _result = 1;
                    break;
                case EmailNotificationID.BANotYetApprovedInternal:
                    _result = 2;
                    break;
                case EmailNotificationID.BASoftBlock:
                    _result = 3;
                    break;
                case EmailNotificationID.SPKBASoftBlock:
                    _result = 4;
                    break;
                case EmailNotificationID.BAOpenSoftBlock:
                    _result = 5;
                    break;
                case EmailNotificationID.SPKBAOpenSoftBlock:
                    _result = 6;
                    break;
                case EmailNotificationID.ContractLetterNotYetApproved:
                    _result = 7;
                    break;
                case EmailNotificationID.ContractLetterNotYetApprovedInternal:
                    _result = 8;
                    break;
                case EmailNotificationID.CLSoftBlock:
                    _result = 9;
                    break;
                case EmailNotificationID.SPKCLSoftBlock:
                    _result = 10;
                    break;
                case EmailNotificationID.CLOpenSoftBlock:
                    _result = 11;
                    break;
                case EmailNotificationID.SPKCLOpenSoftBlock:
                    _result = 12;
                    break;
                case EmailNotificationID.NotPay:
                    _result = 13;
                    break;
                case EmailNotificationID.NotPaySoftBlock:
                    _result = 14;
                    break;
                case EmailNotificationID.SPKNotPaySoftBlock:
                    _result = 15;
                    break;
                case EmailNotificationID.NotPayOpenSoftBlock:
                    _result = 16;
                    break;
                case EmailNotificationID.SPKNotPayOpenSoftBlock:
                    _result = 17;
                    break;
                case EmailNotificationID.BillingInvoiceEmailNotYetSent:
                    _result = 18;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }
    }
}