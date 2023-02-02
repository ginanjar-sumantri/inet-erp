using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class BILTrContract
    {
        public BILTrContract(String _prmTransNmbr, String _prmFileNmbr, DateTime _prmTransDate, String _prmSalesConfirmationNoRef, String _prmCompanyName,
            String _prmResponsibleName, String _prmTitleName, string _prmLetterProviderInformation, string _prmLetterCustomerInformation,
            String _prmFinanceCustomerPIC, String _prmFinanceCustomerPhone, String _prmFinanceCustomerFax, String _prmFinanceCustomerEmail, Byte _prmStatus)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.SalesConfirmationNoRef = _prmSalesConfirmationNoRef;
            this.CompanyName = _prmCompanyName;
            this.ResponsibleName = _prmResponsibleName;
            this.TitleName = _prmTitleName;
            this.LetterProviderInformation = _prmLetterProviderInformation;
            this.LetterCustomerInformation = _prmLetterCustomerInformation;
            this.FinanceCustomerPIC = _prmFinanceCustomerPIC;
            this.FinanceCustomerPhone = _prmFinanceCustomerPhone;
            this.FinanceCustomerFax = _prmFinanceCustomerFax;
            this.FinanceCustomerEmail = _prmFinanceCustomerEmail;
            this.Status = _prmStatus;
        }
    }
}
