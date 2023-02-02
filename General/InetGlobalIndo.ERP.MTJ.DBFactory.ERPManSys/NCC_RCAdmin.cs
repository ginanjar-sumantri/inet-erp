using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class NCC_RCAdmin
    {
        public NCC_RCAdmin(String _prmNomorUrut, String _prmCaseId, String _prmStatus, DateTime _prmTglMasuk,
            String _prmNamaUser, String _prmPhoneUser, DateTime _prmTglTechOut, String _prmActionTaken,
            String _prmType, Decimal _prmMenitServ, DateTime _prmDeadline, String _prmTeknisi,
            String _prmProblem, String _prmCSIn, Char _prmWarrantyRef, String _prmServCode,
            String _prmIMEI, String _prmCSOut, DateTime _prmTglAmbil, String _prmKeteranganKhusus)
        {
            this.NomorUrut = _prmNomorUrut;
            this.CaseId = _prmCaseId;
            this.Status = _prmStatus;
            this.TglMasuk = _prmTglMasuk;
            this.NamaUser = _prmNamaUser;
            this.PhoneUser = _prmPhoneUser;
            this.TglTechOut = _prmTglTechOut;
            this.ActionTaken = _prmActionTaken;
            this.Type = _prmType;
            this.MenitServ = _prmMenitServ;
            this.Deadline = _prmDeadline;
            this.Teknisi = _prmTeknisi;
            this.Problem = _prmProblem;
            this.CSIn = _prmCSIn;
            this.WarrantyRef = _prmWarrantyRef;
            this.ServCode = _prmServCode;
            this.IMEI  = _prmIMEI;
            this.CSOut = _prmCSOut;
            this.TglAmbil = _prmTglAmbil;
            this.KeteranganKhusus = _prmKeteranganKhusus;
        }

        public NCC_RCAdmin(String _prmCaseId, String _prmStatus, DateTime _prmTglMasuk, String _prmNamaUser, String _prmType)
        {
            this.CaseId = _prmCaseId;
            this.Status = _prmStatus;
            this.TglMasuk = _prmTglMasuk;
            this.NamaUser = _prmNamaUser;
            this.Type = _prmType;
        }
    }
}
