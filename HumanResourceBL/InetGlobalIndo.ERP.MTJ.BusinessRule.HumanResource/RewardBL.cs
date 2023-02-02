using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Web;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.IO;
using System.Drawing;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class RewardBL : Base
    {
        public RewardBL()
        {

        }

        #region Reward
        public double RowsCountReward(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                (
                    from _msReward in this.db.HRMMsRewards
                    where (SqlMethods.Like(_msReward.RewardCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msReward.RewardName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _msReward.RewardCode

                ).Count();

            _result = _query;
            return _result;
        }

        public HRMMsReward GetSingleReward(string _prmRewardCode)
        {
            HRMMsReward _result = null;

            try
            {
                _result = this.db.HRMMsRewards.Single(_temp => _temp.RewardCode == _prmRewardCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetRewardNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msReward in this.db.HRMMsRewards
                                where _msReward.RewardCode == _prmCode
                                select new
                                {
                                    RewardName = _msReward.RewardName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.RewardName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsReward> GetListReward(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMMsReward> _result = new List<HRMMsReward>();
            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }
            try
            {
                var _query = (
                                from _msReward in this.db.HRMMsRewards
                                where (SqlMethods.Like(_msReward.RewardCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msReward.RewardName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msReward.EditDate descending
                                select new
                                {
                                    RewardCode = _msReward.RewardCode,
                                    RewardName = _msReward.RewardName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsReward(_row.RewardCode, _row.RewardName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsReward> GetListRewardForDDL()
        {
            List<HRMMsReward> _result = new List<HRMMsReward>();

            try
            {
                var _query = (
                                from _msReward in this.db.HRMMsRewards
                                orderby _msReward.RewardCode ascending
                                select new
                                {
                                    RewardCode = _msReward.RewardCode,
                                    RewardName = _msReward.RewardName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsReward(_row.RewardCode, _row.RewardName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiReward(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMMsReward _msReward = this.db.HRMMsRewards.Single(_temp => _temp.RewardCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.HRMMsRewards.DeleteOnSubmit(_msReward);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddReward(HRMMsReward _prmMsReward)
        {
            bool _result = false;

            try
            {
                this.db.HRMMsRewards.InsertOnSubmit(_prmMsReward);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditReward(HRMMsReward _prmMsReward)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        ~RewardBL()
        {

        }
    }
}
