using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITV.DataAccess;
using ITV.DataAccess.ITVDatabase;

namespace ITV.BusinessRule
{
    public class MenuBL : Base
    {
        public String GetMenu(short _prmParentID, String _prmText)
        {
            String _result = _prmText;

            var _query = (
                            from _menu in this.db.MsMenu
                            where _menu.ParentID == _prmParentID
                            orderby _menu.Priority
                            select new
                            {
                                MenuID = _menu.MenuID,
                                Text = _menu.Text,
                                Parent = _menu.ParentID
                            }

                         );
            foreach (var _row in _query)
            {
                if (_result == "")
                {
                    _result = _row.Text;
                    this.GetMenu(_row.MenuID, _row.Text);
                }
                else
                {
                    _result += "<br>" + _row.Text;
                    this.GetMenu(_row.MenuID, _result);
                    _result += "<br>" + _result;
                }
            }

            //UsRMSUsers _user = this.db.UsRMSUsers.Single(_temp => _temp.UserName == _username && _temp.Password == _password);
            //if (_user != null) return true;

            return _result;
        }
    }
}
