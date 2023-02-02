<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="NCPImport.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.ReceivingPO.NCPImport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="WarningLabel" CssClass="warning" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                File Excel
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:FileUpload ID="FileNameFileUpload" runat="server" />&nbsp;
                                <asp:Button ID="UploadButton" runat="server" OnClick="UploadButton_Click" Text="Upload"
                                    CausesValidation="false" />
                                <asp:HiddenField runat="server" ID="FileNameHiddenField" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Path Upload File
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:Label runat="server" ID="PathFileLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Worksheet Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="WorksheetDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="WorksheetCustomValidator" runat="server" ErrorMessage="Worksheet Name Must Be Filled"
                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="WorksheetDropDownList"></asp:CustomValidator>
                            </td>
                        </tr>
                        <asp:ScriptManager ID="scriptMgr" runat="server" />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <tr>
                                    <td>
                                        Currency
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CurrDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CurrDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Forex Rate
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ForexRateTextBox" runat="server" MaxLength="23"></asp:TextBox>
                                    </td>
                                </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />&nbsp;
                    <asp:ImageButton ID="CancelButton" runat="server" OnClick="CancelButton_Click" CausesValidation="false" />&nbsp;
                    <asp:ImageButton ID="ResetButton" runat="server" OnClick="ResetButton_Click" CausesValidation="false" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="LogPanel">
        <table>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>NCP Import Log</legend>
                        <asp:Label runat="server" ID="ErrorLogLabel"></asp:Label>
                    </fieldset>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
