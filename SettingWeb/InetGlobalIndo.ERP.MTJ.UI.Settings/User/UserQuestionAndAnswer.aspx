<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="UserQuestionAndAnswer.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Settings.User.UserQuestionAndAnswer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveQuestionButton">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
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
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="0">
                        <tr style="height: 20px">
                            <td>
                                User Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:Label runat="server" ID="UserNameLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                               Password
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PassTextBox" Width="300" MaxLength="256" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Password Must Be Filled"
                                    Text="*" ControlToValidate="PassTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Security Question
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="QuestionTextBox" Width="300" MaxLength="256"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QuestionRequiredFieldValidator" runat="server" ErrorMessage="Security Question Must Be Filled"
                                    Text="*" ControlToValidate="QuestionTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Answer
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AnswerTextBox" Width="300" MaxLength="256"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AnswerRequiredFieldValidator" runat="server" ErrorMessage="Answer Must Be Filled"
                                    Text="*" ControlToValidate="AnswerTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table cellpadding="3" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="SaveQuestionButton" runat="server" CausesValidation="true" OnClick="SaveQuestionButton_Click" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ResetButton" runat="server" CausesValidation="False" OnClick="ResetButton_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
