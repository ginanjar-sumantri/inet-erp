using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class CompanyConfigureDataMapper
    {
        public static String GetCompanyConfigure(CompanyConfigure _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case CompanyConfigure.FACodeAutoNumber:
                    _result = "FACodeAutoNumber";
                    break;
                case CompanyConfigure.FACodeDigitNumber:
                    _result = "FACodeDigitNumber";
                    break;
                case CompanyConfigure.COGSMethod:
                    _result = "COGSMethod";
                    break;
                case CompanyConfigure.FADepreciationMethod:
                    _result = "FADepreciationMethod";
                    break;
                case CompanyConfigure.BOLReferenceType:
                    _result = "BOLReferenceType";
                    break;
                case CompanyConfigure.ViewJobTitlePrintReport:
                    _result = "ViewJobTitlePrintReport";
                    break;
                case CompanyConfigure.ActivePGYear:
                    _result = "ActivePGYear";
                    break;
                case CompanyConfigure.FlyerEmail:
                    _result = "FlyerEmail";
                    break;
                case CompanyConfigure.BillingAuthorizedSignImage:
                    _result = "BillingAuthorizedSignImage";
                    break;
                case CompanyConfigure.LeaveTypeProcess:
                    _result = "LeaveTypeProcess";
                    break;
                case CompanyConfigure.PremiGroupBy:
                    _result = "PremiGroupBy";
                    break;
                case CompanyConfigure.BillingHeaderImage:
                    _result = "BillingHeaderImage";
                    break;
                case CompanyConfigure.BillingLeftImage:
                    _result = "BillingLeftImage";
                    break;
                case CompanyConfigure.BillingFooterImage:
                    _result = "BillingFooterImage";
                    break;
                case CompanyConfigure.RDLCBillingInvoiceSendEmail:
                    _result = "RDLCBillingInvoiceSendEmail";
                    break;
                case CompanyConfigure.SalaryEncryptionKey:
                    _result = "SalaryEncryptionKey";
                    break;
                case CompanyConfigure.SalaryEncryptionKeyValidation:
                    _result = "SalaryEncryptionKeyValidation";
                    break;
                case CompanyConfigure.SalaryEncryption:
                    _result = "SalaryEncryption";
                    break;
                case CompanyConfigure.Theme:
                    _result = "Theme";
                    break;
                case CompanyConfigure.PosisiLogo:
                    _result = "PosisiLogo";
                    break;
                case CompanyConfigure.PayrollAuthorizedSignImage:
                    _result = "PayrollAuthorizedSignImage";
                    break;
                case CompanyConfigure.IgnoreItemDiscount:
                    _result = "IgnoreItemDiscount";
                    break;
                case CompanyConfigure.POSServiceCharge:
                    _result = "POSServiceCharge";
                    break;
                case CompanyConfigure.POSTaxCharge:
                    _result = "POSTaxCharge";
                    break;
                case CompanyConfigure.POSRounding:
                    _result = "POSRounding";
                    break;
                case CompanyConfigure.AutoNmbrLocForFA:
                    _result = "AutoNmbrLocForFA";
                    break;
                case CompanyConfigure.AutoNmbrPeriodForFA:
                    _result = "AutoNmbrPeriodForFA";
                    break;
                case CompanyConfigure.HaveProductItemDuration:
                    _result = "HaveProductItemDuration";
                    break;
                case CompanyConfigure.POSBookingTimeLimitAfter:
                    _result = "POSBookingTimeLimitAfter";
                    break;
                case CompanyConfigure.POSBookingTimeLimitBefore:
                    _result = "POSBookingTimeLimitBefore";
                    break;
                case CompanyConfigure.EmailFromSlipPayment:
                    _result = "EmailFromSlipPayment";
                    break;
                case CompanyConfigure.EmailBodySlipPayment:
                    _result = "EmailBodySlipPayment";
                    break;
                case CompanyConfigure.LeaveCanTakeLeaveAfterWorkTime:
                    _result = "LeaveCanTakeLeaveAfterWorkTime";
                    break;
                case CompanyConfigure.POSInternetTimeLimitAfter:
                    _result = "POSInternetTimeLimitAfter";
                    break;
                case CompanyConfigure.POSCafeTimeLimitAfter:
                    _result = "POSCafeTimeLimitAfter";
                    break;
                case CompanyConfigure.BillingRadiusToleranceDay:
                    _result = "BillingRadiusToleranceDay";
                    break;
            }

            return _result;
        }

        public static CompanyConfigure GetCompanyConfigure(String _prmValue)
        {
            CompanyConfigure _result;

            switch (_prmValue)
            {
                case "FACodeAutoNumber":
                    _result = CompanyConfigure.FACodeAutoNumber;
                    break;
                case "FACodeDigitNumber":
                    _result = CompanyConfigure.FACodeDigitNumber;
                    break;
                case "COGSMethod":
                    _result = CompanyConfigure.COGSMethod;
                    break;
                case "FADepreciationMethod":
                    _result = CompanyConfigure.FADepreciationMethod;
                    break;
                case "BOLReferenceType":
                    _result = CompanyConfigure.BOLReferenceType;
                    break;
                case "ViewJobTitlePrintReport":
                    _result = CompanyConfigure.ViewJobTitlePrintReport;
                    break;
                case "ActivePGYear":
                    _result = CompanyConfigure.ActivePGYear;
                    break;
                case "FlyerEmail":
                    _result = CompanyConfigure.FlyerEmail;
                    break;
                case "LeaveTypeProcess":
                    _result = CompanyConfigure.LeaveTypeProcess;
                    break;
                case "RDLCBillingInvoiceSendEmail":
                    _result = CompanyConfigure.RDLCBillingInvoiceSendEmail;
                    break;
                case "PayrollAuthorizedSignImage":
                    _result = CompanyConfigure.PayrollAuthorizedSignImage;
                    break;
                case "IgnoreItemDiscount":
                    _result = CompanyConfigure.IgnoreItemDiscount;
                    break;
                case "POSServiceCharge":
                    _result = CompanyConfigure.POSServiceCharge;
                    break;
                case "POSTaxCharge":
                    _result = CompanyConfigure.POSTaxCharge;
                    break;
                case "POSRounding":
                    _result = CompanyConfigure.POSRounding;
                    break;
                case "AutoNmbrLocForFA":
                    _result = CompanyConfigure.AutoNmbrLocForFA;
                    break;
                case "AutoNmbrPeriodForFA":
                    _result = CompanyConfigure.AutoNmbrPeriodForFA;
                    break;
                case "HaveProductItemDuration":
                    _result = CompanyConfigure.HaveProductItemDuration;
                    break;
                case "POSBookingTimeLimitAfter":
                    _result = CompanyConfigure.POSBookingTimeLimitAfter;
                    break;
                case "POSBookingTimeLimitBefore":
                    _result = CompanyConfigure.POSBookingTimeLimitBefore;
                    break;
                case "EmailFromSlipPayment":
                    _result = CompanyConfigure.EmailFromSlipPayment;
                    break;
                case "EmailBodySlipPayment":
                    _result = CompanyConfigure.EmailBodySlipPayment;
                    break;
                case "LeaveCanTakeLeaveAfterWorkTime":
                    _result = CompanyConfigure.LeaveCanTakeLeaveAfterWorkTime;
                    break;
                case "POSInternetTimeLimitAfter":
                    _result = CompanyConfigure.POSInternetTimeLimitAfter;
                    break;
                case "POSCafeTimeLimitAfter":
                    _result = CompanyConfigure.POSCafeTimeLimitAfter;
                    break;
                default:
                    _result = CompanyConfigure.FACodeAutoNumber;
                    break;
            }

            return _result;
        }

        public static String GetFACodeAutoNumber(FACodeAutoNumber _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case FACodeAutoNumber.False:
                    _result = "0";
                    break;
                case FACodeAutoNumber.True:
                    _result = "1";
                    break;
                default:
                    _result = "0";
                    break;
            }

            return _result;
        }

        public static FACodeAutoNumber GetFACodeAutoNumber(String _prmValue)
        {
            FACodeAutoNumber _result;

            switch (_prmValue)
            {
                case "0":
                    _result = FACodeAutoNumber.False;
                    break;
                case "1":
                    _result = FACodeAutoNumber.True;
                    break;
                default:
                    _result = FACodeAutoNumber.False;
                    break;
            }

            return _result;
        }

        public static String GetLeaveTypeProcess(LeaveTypeProcessType _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case LeaveTypeProcessType.Monthly:
                    _result = "Monthly";
                    break;
                case LeaveTypeProcessType.Yearly:
                    _result = "Yearly";
                    break;
                default:
                    _result = "Monthly";
                    break;
            }

            return _result;
        }

        public static LeaveTypeProcessType GetLeaveTypeProcess(String _prmValue)
        {
            LeaveTypeProcessType _result;

            switch (_prmValue)
            {
                case "Monthly":
                    _result = LeaveTypeProcessType.Monthly;
                    break;
                case "Yearly":
                    _result = LeaveTypeProcessType.Yearly;
                    break;
                default:
                    _result = LeaveTypeProcessType.Monthly;
                    break;
            }

            return _result;
        }
    }
}