<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="FixedAssetSummary.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.FixedAsset.FixedAssetSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <table cellpadding="3" cellspacing="0" width="100%" border="0">
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" width="100%" border="0">
                    <tr>
                        <td class="tahoma_14_black">
                            <b>
                                <asp:Literal ID="PageTitleLiteral" runat="server" />
                            </b>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" width="0" border="0">
                    <tr class="bgcolor_gray">
                        <td class="tahoma_11_white" align="center" colspan="3">
                            <b>Buy Price Home</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Grand Total
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label runat="server" ID="GrandTotalLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr class="bgcolor_gray">
                        <td style="width: 1px" colspan="10">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
