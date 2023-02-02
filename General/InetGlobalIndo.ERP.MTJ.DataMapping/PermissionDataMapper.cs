using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class PermissionDataMapper
    {
        public static bool GetStatus(string _prmStatus)
        {
            bool _result = false;

            switch (_prmStatus)
            {
                case "Allow":
                    _result = true;
                    break;
                case "Deny":
                    _result = false;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }

        public static string GetStatus(bool _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case true:
                    _result = "Allow";
                    break;
                case false:
                    _result = "Deny";
                    break;
                default:
                    _result = "Deny";
                    break;
            }

            return _result;
        }

        public static string GetText(Common.Enum.Action _prmAction)
        {
            string _result = "";

            switch (_prmAction)
            {
                case Common.Enum.Action.Add:
                    _result = "Add";
                    break;
                case Common.Enum.Action.Edit:
                    _result = "Edit";
                    break;
                case Common.Enum.Action.Delete:
                    _result = "Delete";
                    break;
                case Common.Enum.Action.View:
                    _result = "View";
                    break;
                case Common.Enum.Action.GetApproval:
                    _result = "GetApproval";
                    break;
                case Common.Enum.Action.Posting:
                    _result = "Posting";
                    break;
                case Common.Enum.Action.Approve:
                    _result = "Approve";
                    break;
                case Common.Enum.Action.Unposting:
                    _result = "Unposting";
                    break;
                case Common.Enum.Action.Close:
                    _result = "Close";
                    break;
                case Common.Enum.Action.Access:
                    _result = "Access";
                    break;
                case Common.Enum.Action.PrintPreview:
                    _result = "PrintPreview";
                    break;
                case Common.Enum.Action.TaxPreview:
                    _result = "TaxPreview";
                    break;
                case Common.Enum.Action.Generate:
                    _result = "Generate";
                    break;
                case Common.Enum.Action.Revisi:
                    _result = "Revisi";
                    break;
            }

            return _result;
        }

        //new permission
        public static byte GetStatusLevel(PermissionLevel _prmLevel)
        {
            byte _result = 0;

            switch (_prmLevel)
            {
                case PermissionLevel.NoAccess:
                    _result = 0;
                    break;
                case PermissionLevel.Self:
                    _result = 1;
                    break;
                case PermissionLevel.SelfOU:
                    _result = 2;
                    break;
                case PermissionLevel.InheritOU:
                    _result = 3;
                    break;
                case PermissionLevel.EntireOU:
                    _result = 4;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static PermissionLevel GetStatusLevel(byte _prmLevel)
        {
            PermissionLevel _result;

            switch (_prmLevel)
            {
                case 0:
                    _result = PermissionLevel.NoAccess;
                    break;
                case 1:
                    _result = PermissionLevel.Self;
                    break;
                case 2:
                    _result = PermissionLevel.SelfOU;
                    break;
                case 3:
                    _result = PermissionLevel.InheritOU;
                    break;
                case 4:
                    _result = PermissionLevel.EntireOU;
                    break;
                default:
                    _result = PermissionLevel.NoAccess;
                    break;
            }

            return _result;
        }
    }
}