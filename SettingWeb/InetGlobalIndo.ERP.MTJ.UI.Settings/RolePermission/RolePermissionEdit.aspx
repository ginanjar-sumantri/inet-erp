<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="RolePermissionEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Settings.RolePermission.RolePermissionEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script type="text/javascript" src="../jquery-1.4.2.min.js"></script>

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

        //ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl00_OneRowCheckBox
        //var a = $("input: OneRowCheckBox: checked").val();

        //        $("input").attr("id","") {
        //            alert("asdaweq");
        //        };



        //var a = $("input[id$='OneRowCheckBox']").val();
        //alert(a);
        
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="SaveButton" runat="server">
        <table cellpadding="3" cellspacing="0" width="1000" border="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                Role Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="RoleNameTextBox" runat="server" Width="560" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                    <asp:HiddenField ID="CheckHidden2" runat="server" />
                    <asp:HiddenField ID="CheckHidden3" runat="server" />
                    <asp:HiddenField ID="TempHidden" runat="server" />
                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                        <tr class="bgcolor_gray">
                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                <b>No.</b>
                            </td>
                            <td style="width: 250px" class="tahoma_11_white" align="center">
                                <b>Menu</b>
                            </td>
                            <td style="width: 25px" class="tahoma_11_white" align="center">
                                <b>One Row</b>
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Access</b><br />
                                <asp:CheckBox runat="server" ID="AccessCheckBox" />
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Add</b><br />
                                <asp:CheckBox runat="server" ID="AddCheckBox" />
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Edit</b><br />
                                <asp:CheckBox runat="server" ID="EditCheckBox" />
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Delete</b><br />
                                <asp:CheckBox runat="server" ID="DeleteCheckBox" />
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>View</b><br />
                                <asp:CheckBox runat="server" ID="ViewCheckBox" />
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Get Approval</b><br />
                                <asp:CheckBox runat="server" ID="GetApprovalCheckBox" />
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Approve</b><br />
                                <asp:CheckBox runat="server" ID="ApproveCheckBox" />
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Posting</b><br />
                                <asp:CheckBox runat="server" ID="PostingCheckBox" />
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Unposting</b><br />
                                <asp:CheckBox runat="server" ID="UnpostingCheckBox" />
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Print Preview</b><br />
                                <asp:CheckBox runat="server" ID="PrintPreviewCheckBox" />
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Tax Preview</b><br />
                                <asp:CheckBox runat="server" ID="TaxPreviewCheckBox" />
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Closing</b><br />
                                <asp:CheckBox runat="server" ID="ClosingCheckBox" />
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Generate</b><br />
                                <asp:CheckBox runat="server" ID="GenerateCheckBox" />
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Revisi</b><br />
                                <asp:CheckBox runat="server" ID="RevisiCheckBox" />
                            </td>
                        </tr>
                        <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                            <ItemTemplate>
                                <tr id="RepeaterItemTemplate" runat="server">
                                    <td align="center">
                                        <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                    </td>
                                    <td align="left">
                                        <table cellpadding="0" cellspacing="0" border="0" width="0">
                                            <tr>
                                                <asp:Literal runat="server" ID="MenuLiteral"></asp:Literal>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="OneRowCheckBox" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="AccessPermission" class="CheckBox" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="AddPermission" class="CheckBox" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="EditPermission" class="CheckBox" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="DeletePermission" class="CheckBox" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="ViewPermission" class="CheckBox" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="GetApprovalPermission" class="CheckBox" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="ApprovePermission" class="CheckBox" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="PostingPermission" class="CheckBox" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="UnpostingPermission" class="CheckBox" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="PrintPreviewPermission" class="CheckBox" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="TaxPreviewPermission" class="CheckBox" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="ClosePermission" class="CheckBox" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="GeneratePermission" class="CheckBox" />
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="RevisiPermission" class="CheckBox" />
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
                                <asp:ImageButton runat="server" CausesValidation="false" ID="CancelButton" OnClick="CancelButton_Click" />
                            </td>
                            <td style="width: 50px">
                                <asp:ImageButton runat="server" ID="SaveAndEditButton" OnClick="SaveAndEditButton_Click" />
                            </td>
                            <td style="width: 50px">
                                <asp:ImageButton runat="server" ID="GrantAllPermissionButton" OnClick="GrantAllPermissionButton_Click" />
                            </td>
                            <td style="width: 50px">
                                <asp:ImageButton runat="server" ID="DenyAllPermissionButton" OnClick="DenyAllPermissionButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <script language="javascript">



        var z = $("input[id$='OneRowCheckBox']").attr("id");
        var x = z.substring(0, z.length - 15);
        //alert(x);
        if ($("input[id*='" + x + "']").attr("checked") == true) {
            $("input[id$='OneRowCheckBox']").attr("checked", "true")
        }
        else {
            $("input[id$='OneRowCheckBox']").attr("checked", "")
            //alert("masuk");
        }

        if ($(".CheckBox").attr("checked") == true) {
            //alert("coba");
            $("input[id$='OneRowCheckBox']").attr("checked", "true")
        }
        
        ////////////////////// + Mulai + ///////////////////////////////////////
        $("input[id$='AccessCheckBox']").click(function() {// untuk checkbox kebawah accountPermision
            var o = $(this).attr("id");
            if ($(this).attr("checked") == true) {
                $("input[id*='AccessPermission']").attr("checked", "true");
            }
            else 
            {
                $("input[id*='AccessPermission']").attr("checked", "");
            }
        });

        $("input[id$='AddCheckBox']").click(function() {// untuk checkbox kebawah AddPermision
            var o = $(this).attr("id");
            if ($(this).attr("checked") == true) {
                $("input[id*='AddPermission']").attr("checked", "true");
            }
            else {
                $("input[id*='AddPermission']").attr("checked", "");
            }
        });

        $("input[id$='EditCheckBox']").click(function() {// untuk checkbox kebawah EditPermision
            var o = $(this).attr("id");
            if ($(this).attr("checked") == true) {
                $("input[id*='EditPermission']").attr("checked", "true");
            }
            else {
                $("input[id*='EditPermission']").attr("checked", "");
            }
        });

        $("input[id$='DeleteCheckBox']").click(function() {// untuk checkbox kebawah DeletePermision
            var o = $(this).attr("id");
            if ($(this).attr("checked") == true) {
                $("input[id*='DeletePermission']").attr("checked", "true");
            }
            else {
                $("input[id*='DeletePermission']").attr("checked", "");
            }
        });

        $("input[id$='ViewCheckBox']").click(function() {// untuk checkbox kebawah ViewPermision
            var o = $(this).attr("id");
            if ($(this).attr("checked") == true) {
                $("input[id*='ViewPermission']").attr("checked", "true");
            }
            else {
                $("input[id*='ViewPermission']").attr("checked", "");
            }
        });

        $("input[id$='GetApprovalCheckBox']").click(function() {// untuk checkbox kebawah GetApprovalPermision
            var o = $(this).attr("id");
            if ($(this).attr("checked") == true) {
                $("input[id*='GetApprovalPermission']").attr("checked", "true");
            }
            else {
                $("input[id*='GetApprovalPermission']").attr("checked", "");
            }
        });

        $("input[id$='ApproveCheckBox']").click(function() {// untuk checkbox kebawah ApprovePermision
            var o = $(this).attr("id");
            if ($(this).attr("checked") == true) {
                $("input[id*='ApprovePermission']").attr("checked", "true");
            }
            else {
                $("input[id*='ApprovePermission']").attr("checked", "");
            }
        });

        $("input[id$='PostingCheckBox']").click(function() {// untuk checkbox kebawah PostingPermision
            var o = $(this).attr("id");
            if ($(this).attr("checked") == true) {
                $("input[id*='PostingPermission']").attr("checked", "true");
            }
            else {
                $("input[id*='PostingPermission']").attr("checked", "");
            }
        });

        $("input[id$='UnpostingCheckBox']").click(function() {// untuk checkbox kebawah UnpostingPermision
            var o = $(this).attr("id");
            if ($(this).attr("checked") == true) {
                $("input[id*='UnpostingPermission']").attr("checked", "true");
            }
            else {
                $("input[id*='UnpostingPermission']").attr("checked", "");
            }
        });


        $("input[id$='PrintPreviewCheckBox']").click(function() {// untuk checkbox kebawah PrintPreviewPermision
            var o = $(this).attr("id");
            if ($(this).attr("checked") == true) {
                $("input[id*='PrintPreviewPermission']").attr("checked", "true");
            }
            else {
                $("input[id*='PrintPreviewPermission']").attr("checked", "");
            }
        });

        $("input[id$='TaxPreviewCheckBox']").click(function() {// untuk checkbox kebawah TaxPreviewPermision
            var o = $(this).attr("id");
            if ($(this).attr("checked") == true) {
                $("input[id*='TaxPreviewPermission']").attr("checked", "true");
            }
            else {
                $("input[id*='TaxPreviewPermission']").attr("checked", "");
            }
        });


        /////////////////// + Row + ///////////////////////
        $("input[id$='OneRowCheckBox']").click(function() {// untuk checkbox per baris
            var a = $(this).attr("id");

            //00_OneRowCheckBox
            //16
            //            alert(a.substring(0, a.length - 14));
            var b = $("input[id$='AccessPermission']").attr("id");
            var d = (a.substring(0, a.length - 15));
            //alert(a.substring(0, a.length - 15));
            //            alert(b.substring(b.length, 55));
            //            alert((a.substring(0, a.length - 14)) + (b.substring(b.length, 55)));
            var c = ((a.substring(0, a.length - 14)) + (b.substring(b.length, 55)));

            if ($(this).attr("checked") == true) {
                //                alert($("input[id$='AccessPermission']"));
                //                alert(c);
                $("input[id*='" + d + "']").attr("checked", "true");
            }
            else if ($(this).attr("checked") == false) {
                //                alert($(c + ":checked"));
                //alert(c);
                $("input[id*=" + d + "]").attr("checked", "");
            }



            //            $(this).toggle(
            //                  function() {
            //                      $(c).attr("Checked") = false;
            //                      alert((a.substring(0, a.length - 14)) + (b.substring(b.length, 55)));
            //                  },
            //                  function() {
            //                      $(c).attr("Checked") = true;
            //                  }
            //             );


            //alert($(this).attr("id").substring(length($(this).attr("id")) - 16, 2));
            //alert(a);
            //alert("check!");
        });
    </script>

</asp:Content>
