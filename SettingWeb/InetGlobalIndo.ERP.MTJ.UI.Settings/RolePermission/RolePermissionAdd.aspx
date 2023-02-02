<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="RolePermissionAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Settings.RolePermission.RolePermissionAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function CheckBoxChangePermission(_prmHidden, _prmImage, _prmMenuID) {
            var _str = _prmHidden.value;
            var _strSplit = _str.split(",");
            var _strMenu = _prmMenuID;
            var _strSplitMenu = _strMenu.split("-");
            var _permission = "";
            var _imgNoAccess = false;
            var _imgEntireOU = true;

            if (_prmImage.checked == true) {
                if (_prmHidden.value.search(_strSplitMenu[0] + "-" + _strSplitMenu[1])) {
                    _prmHidden.value = (_prmHidden.value).replace(_strSplitMenu[0] + "-" + _strSplitMenu[1] + "-0", _strSplitMenu[0] + "-" + _strSplitMenu[1] + "-1");
                }
                _permission = "4";
            }
            else if (_prmImage.checked == false) {
                if (_prmHidden.value.search(_strSplitMenu[0] + "-" + _strSplitMenu[1])) {
                    _prmHidden.value = (_prmHidden.value).replace(_strSplitMenu[0] + "-" + _strSplitMenu[1] + "-4", _strSplitMenu[0] + "-" + _strSplitMenu[1] + "-0");
                }
                _permission = "0";
            }

            if (_strSplit[0] != "") {
                var _first = _strSplit[0].split("-");
                if (_permission == "0") {
                    _prmHidden.value = (_prmHidden.value).replace(_strSplitMenu[0] + "-" + _strSplitMenu[1] + "-4", _strSplitMenu[0] + "-" + _strSplitMenu[1] + "-" + _permission);
                }
                else if (_permission == "4") {
                    _prmHidden.value = (_prmHidden.value).replace(_strSplitMenu[0] + "-" + _strSplitMenu[1] + "-0", _strSplitMenu[0] + "-" + _strSplitMenu[1] + "-" + _permission);
                }
            }

            if (_prmHidden.value == "") {
                _prmHidden.value = _prmMenuID + "-" + _permission;
            }
            else {
                if (_prmHidden.value.match(_strSplitMenu[0] + "-" + _strSplitMenu[1] + "-" + _permission) == null) {
                    _prmHidden.value = _prmHidden.value + "," + _prmMenuID + "-" + _permission;
                }
            }
        }        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="SaveButton" runat="server">
        <table cellpadding="3" cellspacing="0" width="1200px" border="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="Label1" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                Role
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="RoleDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RoleDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="RoleCustomValidator" runat="server" ErrorMessage="Role Must Be Choose"
                                    ClientValidationFunction="DropDownValidation" ControlToValidate="RoleDropDownList"
                                    Text="*"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Module
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="ModuleDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ModuleDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="CheckHidden" runat="server" />
                    <asp:HiddenField ID="TempHidden" runat="server" />
                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                        <tr class="bgcolor_gray">
                            <%--<td style="width: 5px">
                                <asp:CheckBox runat="server" ID="AllCheckBox" />
                            </td>--%>
                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                <b>No.</b>
                            </td>
                            <td style="width: 80px" class="tahoma_11_white" align="center">
                                <b>Module</b>
                            </td>
                            <td style="width: 250px" class="tahoma_11_white" align="center">
                                <b>Menu</b>
                            </td>
                            <%--<td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Fill All</b>
                            </td>--%>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Access</b>
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Add</b>
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Edit</b>
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Delete</b>
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>View</b>
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Get Approval</b>
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Approve</b>
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Posting</b>
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Unposting</b>
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Print Preview</b>
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Tax Preview</b>
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Closing</b>
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Generate</b>
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Revisi</b>
                            </td>
                        </tr>
                        <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                            <ItemTemplate>
                                <tr id="RepeaterItemTemplate" runat="server">
                                    <%--<td align="center">
                                        <asp:CheckBox runat="server" ID="ListCheckBox" />
                                    </td>--%>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                    </td>
                                    <%--<td align="center">
                                        <table cellpadding="0" cellspacing="0" width="0" border="0">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="ViewButton" />
                                                </td>
                                                <td style="padding-left: 4px">
                                                    <asp:ImageButton runat="server" ID="EditButton" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>--%>
                                    <td align="left">
                                        <asp:Literal runat="server" ID="ModuleLiteral"></asp:Literal>
                                    </td>
                                    <td align="left">
                                        <table cellpadding="0" cellspacing="0" border="0" width="0">
                                            <tr>
                                                <asp:Literal runat="server" ID="MenuLiteral"></asp:Literal>
                                            </tr>
                                        </table>
                                    </td>
                                    <%--<td align="center">
                                        <asp:Image runat="server" ID="HorizontalPermission" />
                                    </td>--%>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="AccessPermission" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="AddPermission" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="EditPermission" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="DeletePermission" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="ViewPermission" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="GetApprovalPermission" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="ApprovePermission" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="PostingPermission" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="UnpostingPermission" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="PrintPreviewPermission" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="TaxPreviewPermission" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="ClosePermission" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="GeneratePermission" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="RevisiPermission" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td style="width: 50px">
                                <asp:ImageButton runat="server" ID="SaveButton" OnClick="SaveButton_Click" />
                            </td>
                            <td style="width: 50px">
                                <asp:ImageButton runat="server" ID="CancelButton" CausesValidation="false" OnClick="CancelButton_Click" />
                            </td>
                            <td style="width: 50px">
                                <asp:ImageButton runat="server" ID="SaveAndEditButton" OnClick="SaveAndEditButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
