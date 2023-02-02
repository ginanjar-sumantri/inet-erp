using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTSWeb.Enum;
using VTSWeb.DataMapping;
using VTSWeb.Database;

namespace VTSWeb.BusinessRule
{
    public sealed class PermissionBL : Base
    {
        public PermissionBL()
        {
        }

        //public bool PermissionValidation(String _prmUserName, String _prmRequestBy)
        //{
        //    var _query = (
        //        from _msEmployee in this.db.MsEmployees
        //        join _msUser_msEmployee in this.db.MsUser_MsEmployees
        //        on _msEmployee.EmpNumb equals _msUser_msEmployee.EmpNumb
        //        where _msUser_msEmployee.UserName == _prmUserName
        //        select _msEmployee.JobLevel
        //        ).FirstOrDefault();

        //    var _query1 = (
        //        from _msEmployee in this.db.MsEmployees
        //        join _hrmTrAssignment in this.db.HRMTrAssignments
        //        on _msEmployee.EmpNumb equals _hrmTrAssignment.EmpNumb
        //        where _hrmTrAssignment.AssignmentBy == _prmRequestBy
        //        select _msEmployee.JobLevel
        //        ).FirstOrDefault();

        //    if (Convert.ToInt32(_query) <= Convert.ToInt32(_query1))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}

        //public bool PermissionValidationAssignTo(String _prmUserName, String _prmAssignTo)
        //{
        //    var _query = (
        //        from _msEmployee in this.db.MsEmployees
        //        join _msUser_msEmployee in this.db.MsUser_MsEmployees
        //        on _msEmployee.EmpNumb equals _msUser_msEmployee.EmpNumb
        //        where _msUser_msEmployee.UserName == _prmUserName
        //        select _msEmployee.JobLevel
        //        ).FirstOrDefault();

        //    var _query1 = (
        //        from _msEmployee in this.db.MsEmployees
        //        join _hrmTrAssignment in this.db.HRMTrAssignments
        //        on _msEmployee.EmpNumb equals _hrmTrAssignment.EmpNumb
        //        where _hrmTrAssignment.EmpNumb == _prmAssignTo
        //        select _msEmployee.JobLevel
        //        ).FirstOrDefault();

        //    if (Convert.ToInt32(_query) <= Convert.ToInt32(_query1))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}

        //public bool PermissionDone(String _prmUserName, int _prmID)
        //{
        //    var EmpNumbLogin = (
        //        from _MsUser_MsEmployee in this.db.MsUser_MsEmployees
        //        join _MsUser in this.db.MsUsers
        //            on _MsUser_MsEmployee.UserName equals _MsUser.UserName
        //        where _MsUser.UserName == _prmUserName
        //        select _MsUser_MsEmployee.EmpNumb
        //        ).FirstOrDefault();

        //    var EmpNumbAssignTo = (
        //        from _TaskList in this.db.TrTaskLists
        //        join _AssignmentDt in this.db.HRMTrAssignmentDts
        //            on _TaskList.ID equals _AssignmentDt.TaskListID
        //        join _AssignmentHd in this.db.HRMTrAssignments
        //            on _AssignmentDt.AssignmentID equals _AssignmentHd.AssignmentID
        //        where _TaskList.ID == _prmID
        //        select _AssignmentHd.EmpNumb
        //        ).FirstOrDefault();

        //    if (Convert.ToInt32(EmpNumbLogin) == Convert.ToInt32(EmpNumbAssignTo))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public bool PermissionAccept(String _prmUserName, String _prmKey)
        //{
        //    var EmpNumbLogin = (
        //        from _MsUser_MsEmployee in this.db.MsUser_MsEmployees
        //        join _MsUser in this.db.MsUsers
        //            on _MsUser_MsEmployee.UserName equals _MsUser.UserName
        //        where _MsUser.UserName == _prmUserName
        //        select _MsUser_MsEmployee.EmpNumb
        //        ).FirstOrDefault();

        //    var EmpNumbAssignTo = (
        //        from _AssignmentHd in this.db.HRMTrAssignments
        //        where _AssignmentHd.AssignmentID == _prmKey
        //        select _AssignmentHd.EmpNumb
        //        ).FirstOrDefault();

        //    if (Convert.ToInt32(EmpNumbLogin) == Convert.ToInt32(EmpNumbAssignTo))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public bool Permission(String _prmUserName, String _prmKey)
        //{
        //    var EmpNameLogin = (
        //        from _MsUser_MsEmployee in this.db.MsUser_MsEmployees
        //        join _MsUser in this.db.MsUsers
        //            on _MsUser_MsEmployee.UserName equals _MsUser.UserName
        //        where _MsUser.UserName == _prmUserName
        //        select _MsUser_MsEmployee.UserName
        //        ).FirstOrDefault();

        //    var EmpNameAssignmentBy = (
        //        from _AssignmentHd in this.db.HRMTrAssignments
        //        where _AssignmentHd.AssignmentID == _prmKey
        //        select _AssignmentHd.AssignmentBy
        //        ).FirstOrDefault();

        //    if (EmpNameLogin.Trim().ToLower() == EmpNameAssignmentBy.Trim().ToLower())
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public int GetEmployeeLevelByUserName(String _prmUserName)
        {
            int _result = 0;

            try
            {
                var _query = (
                                from _msUserEmp in this.db.MsUser_MsEmployees
                                where _msUserEmp.UserName.ToLower() == _prmUserName.ToLower()
                                join _msEmp in this.db.MsEmployees
                                    on _msUserEmp.EmpNumb equals _msEmp.EmpNumb
                                select new
                                {
                                    JobLevel = _msEmp.JobLevel
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = Convert.ToInt32(_obj.JobLevel);
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        ~PermissionBL()
        {
        }
    }
}
