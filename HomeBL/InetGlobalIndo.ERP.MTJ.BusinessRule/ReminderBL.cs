using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule
{
    public class ReminderBL : Base
    {
        public ReminderBL() { }

        public String getComposedRoleIdList(String _prmUserId) {
            String _result = "" ;
            try
            {
                var _query = (
                        from _roleID in this.dbMembership.aspnet_UsersInRoles 
                        where _roleID.UserId == new Guid ( _prmUserId )
                        select _roleID.RoleId 
                    );
                foreach (var _rs in _query) 
                {
                    if ( _result == "" ) 
                        _result += _rs ;
                    else
                        _result += "," + _rs ;
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        private int GetReminderCount ( String _prmTableName, String _prmFgCondition ) {
            int _result = 0 ;
            try 
	            {                    
                    foreach (var _item in this.db.spHOME_Reminder(_prmTableName, _prmFgCondition))
                    {
                        _result = _item.ReminderCount;
                    } 
	            }
	            catch (Exception ex)
	            {		
		            throw ex;
	            }
            return _result ;
        }

        public List<HOME_Reminder> getReminderFrontList(String _prmComposedRoleIdList)
        {
            List<HOME_Reminder> _result = new List<HOME_Reminder>();
            try
            {
                String [] RoleList = _prmComposedRoleIdList.Split (',') ;
                foreach ( String _role in RoleList ) {
                    var _query1 = (
                            from _msReminderMapping in this.dbMembership.MsReminderMappings
                            join _msReminder in this.dbMembership.MsReminders
                            on _msReminderMapping.ReminderCode equals _msReminder.ReminderCode
                            where _msReminderMapping.RoleId.ToString() == _role
                            select new {
                                ReminderName = _msReminder.ReminderName ,
                                ReminderCode = _msReminder.ReminderCode ,
                                TableName = _msReminder.TableName ,
                                FgCondition = _msReminder.FgCondition
                            }
                        ).Distinct();
                    foreach (var _rs1 in _query1) {
                        _result.Add ( new HOME_Reminder ( _rs1.ReminderName, this.GetReminderCount ( _rs1.TableName, _rs1.FgCondition) , _rs1.ReminderCode ));
                    }
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public Int32 RowsCount( String _prmReminderCode ) {
            Int32 _result = 0;
            try
            {
                var _query = (
                        from _reminderMapping in this.dbMembership.MsReminders 
                        where _reminderMapping.ReminderCode == _prmReminderCode 
                        select _reminderMapping
                    ).FirstOrDefault () ;
                _result = this.GetReminderCount(_query.TableName, _query.FgCondition);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public List<HOME_Reminder> GetListTransactionApproval(int _prmCurrPage, int _prmMaxRow, String _prmReminderCode)
        {
            List<HOME_Reminder> _result = new List<HOME_Reminder>();
            try
            {
                HOME_Reminder _addData = new HOME_Reminder();
                var _query = (
                        from _reminderMapping in this.dbMembership.MsReminders
                        where _reminderMapping.ReminderCode == _prmReminderCode
                        select _reminderMapping
                    ).FirstOrDefault();
                _addData.ReminderPath = _query.Path;
                _addData.MenuID = Convert.ToInt16( _query.MenuID );
                foreach (var _item in this.db.spHOME_ReminderTransactionApproval(_query.TableName, _query.FgCondition))
                {
                    _addData.TransNmbr = _item.TransNmbr;
                    _addData.FileNmbr = _item.FileNmbr;
                    _addData.Status = _item.Status;
                    _result.Add(_addData);
                }                
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }
        

        ~ReminderBL() { }

        public string GetReminderPath(string p)
        {
            string _result = "";
            try
            {
                MsReminder _dataReminder = this.dbMembership.MsReminders.Single ( a=> a.ReminderCode == p ) ;
                _result = _dataReminder.Path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public string getMenuId(String p)
        {
            string _result = "";
            try
            {
                MsReminder _dataReminder = this.dbMembership.MsReminders.Single ( a=> a.ReminderCode == p ) ;
                _result = _dataReminder.MenuID.ToString();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }
    }
}
