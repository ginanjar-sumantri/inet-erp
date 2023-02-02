using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsPetty
    {
        private string _AccountName = "";


        //public MsPetty(string _prmPettyCode, string _prmPettyName, byte _prmFgType, string _prmAccount, string _prmAccountName)
        //{
        //    this.PettyCode = _prmPettyCode;
        //    this.PettyName = _prmPettyName;
        //    this.FgType = _prmFgType;
        //    this.Account = _prmAccount;
        //    this.AccountName = _prmAccountName;
        //}

        public MsPetty(string _prmPettyCode, string _prmPettyName, string _prmAccount, string _prmAccountName)
        {
            this.PettyCode = _prmPettyCode;
            this.PettyName = _prmPettyName;
            //this.FgType = _prmFgType;
            this.Account = _prmAccount;
            this.AccountName = _prmAccountName;
        }

        public MsPetty(string _prmPettyCode, string _prmPettyName)
        {
            this.PettyCode = _prmPettyCode;
            this.PettyName = _prmPettyName;

        }



        public string AccountName
        {
            get
            {
                return this._AccountName;
            }
            set
            {
                this._AccountName = value;
            }
        }


    }
}
