<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="POS.POSInterface.Registration.Registration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
    <asp:Literal ID="javaScriptDeclaration" runat="server"></asp:Literal>

    <script src="../JQuery/jquery-ui.min.js" type="text/javascript"></script>

    <script src="../JQuery/jquery-1.4.3.min.js" type="text/javascript"></script>

    <script src="../JQuery/jquery-1.2.6.min.js" type="text/javascript"></script>

    <script src="../JQuery/jquery.event.drag-1.5.min.js" type="text/javascript"></script>

    <script src="../CSS/orange/jquery.corner.js" type="text/javascript"></script>

    <script src="../CSS/orange/style3.js" type="text/javascript"></script>

    <script src="../CSS/orange/JScript.js" type="text/javascript"></script>

    <link href="../JQuery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/orange/style4.css" media="all" rel="Stylesheet" type="text/css" />
    <asp:Literal ID="JScriptLiteral" runat="server"></asp:Literal>
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script type="text/javascript" language="javascript">
        var dateObject = new Date();
        var Hari = new Array("SUNDAY", "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY");
        var Bulan = new Array("JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER");
        var hour = dateObject.getHours();
        var minute = dateObject.getMinutes();
        var second = dateObject.getSeconds();
        if (hour < 10) {
            hour = "0" + hour;
        }
        if (minute < 10) {
            minute = "0" + minute;
        }
        if (second < 10) {
            second = "0" + second;
        }
        $('.container .header .right .date #tanggal').text(Hari[dateObject.getDay()] + ', ' + dateObject.getDate() + ' ' + Bulan[dateObject.getMonth()] + ' ' + (dateObject.getYear() + 1900));
        $('.container .header .right .date #jam').text(hour + ':' + minute + ':' + second);
        function updateJam() {
            var dateObject = new Date();
            var Hari = new Array("SUNDAY", "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY");
            var Bulan = new Array("JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER");
            var hour = dateObject.getHours();
            var minute = dateObject.getMinutes();
            var second = dateObject.getSeconds();
            if (hour < 10) {
                hour = "0" + hour;
            }
            if (minute < 10) {
                minute = "0" + minute;
            }
            if (second < 10) {
                second = "0" + second;
            }
            $('.container .header .right .date #tanggal').text(Hari[dateObject.getDay()] + ', ' + dateObject.getDate() + ' ' + Bulan[dateObject.getMonth()] + ' ' + (dateObject.getYear() + 1900));
            $('.container .header .right .date #jam').text(hour + ':' + minute + ':' + second);
        }
        var x = setInterval("updateJam();", 1000);
    </script>

    <style type="text/css">
        .td
        {
            width: 230px;
        }
        .textValid
        {
            color: Red;
            font-size: 17px;
            font-family: caption;
        }
        .Highlight
        {
            background-color: Red;
        }
    </style>
</head>
<body id="bodyRegistration">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />
    <asp:UpdatePanel ID="contentupdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Literal runat="server" ID="CSSLiteral"></asp:Literal>
            <asp:Literal runat="server" ID="javascriptReceiver"></asp:Literal>
            <div class="container">
                <div class="header">
                    <div class="left">
                        <div class="title">
                            <div class="text">
                                Registration</div>
                            <div class="sep">
                            </div>
                        </div>
                    </div>
                    <div class="right">
                        <div class="date">
                            <div id="tanggal">
                            </div>
                            <div id="jam">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="warning">
                    <%--<asp:ValidationSummary ID="ValidationSummary" runat="server" />
--%>
                    <asp:Label runat="server" ID="WarningLabel"></asp:Label>
                </div>
                <div class="ContentForm">
                    <div class="formRegister1">
                        <table class="table1">
                            <tr>
                                <td>
                                    MEMBER TYPE
                                </td>
                                <td>
                                    :
                                </td>
                                <td class="textValid">
                                    <asp:DropDownList ID="MemberTypeDropDownList" runat="server" Width="255">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="MemberTypeCustomValidator" runat="server" ErrorMessage="<b>Member Type Must Be Filed.</b> "
                                        Text="*" ControlToValidate="MemberTypeDropDownList" Display="Dynamic" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1"
                                        TargetControlID="MemberTypeCustomValidator" HighlightCssClass="Highlight">
                                        <Animations>
                                 <OnShow>                                    
                                 <Sequence>   
        <HideAction Visible="true" /> 
        <FadeIn Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        </Sequence>
        </OnShow>
        <OnHide>
        <Sequence>    
        <FadeOut Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        <HideAction Visible="false" />
        </Sequence>
        </OnHide></Animations>
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    MEMBER CODE
                                </td>
                                <td>
                                    :
                                </td>
                                <td class="textValid">
                                    <asp:TextBox ID="MemberCodeTextBox" runat="server" CssClass="widthText" MaxLength="50"
                                        onKeyPress="return event.keyCode!=13"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="MemberCodeRequiredFieldValidator"
                                        Text="*" ControlToValidate="MemberCodeTextBox" Display="Dynamic" ErrorMessage="<b>Member Code Must Be Filed</b>" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="NReqE" TargetControlID="MemberCodeRequiredFieldValidator"
                                        HighlightCssClass="Highlight">
                                        <Animations>
                                 <OnShow>                                    
                                 <Sequence>   
        <HideAction Visible="true" /> 
        <FadeIn Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        </Sequence>
        </OnShow>
        <OnHide>
        <Sequence>    
        <FadeOut Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        <HideAction Visible="false" />
        </Sequence>
        </OnHide></Animations>
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    MEMBER BARCODE
                                </td>
                                <td>
                                    :
                                </td>
                                <td class="textValid">
                                    <asp:TextBox ID="BarcodeTextBox" runat="server" CssClass="widthText" MaxLength="13"
                                        onKeyPress="return event.keyCode!=13"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="MemberBarcodeRequiredFieldValidator" runat="server"
                                        ErrorMessage="<b>Member Barcode Must Be Filled.</b>" Text="*" ControlToValidate="BarcodeTextBox"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2"
                                        TargetControlID="MemberBarcodeRequiredFieldValidator" HighlightCssClass="Highlight">
                                        <Animations>
                                 <OnShow>                                    
                                 <Sequence>   
        <HideAction Visible="true" /> 
        <FadeIn Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        </Sequence>
        </OnShow>
        <OnHide>
        <Sequence>    
        <FadeOut Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        <HideAction Visible="false" />
        </Sequence>
        </OnHide></Animations>
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    MEMBER NAME
                                </td>
                                <td>
                                    :
                                </td>
                                <td class="textValid">
                                    <asp:TextBox ID="MemberNameTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="MemberNameRequiredFieldValidator" runat="server"
                                        ErrorMessage="<b>Member Name Must Be Filled</b>" Text="*" ControlToValidate="MemberNameTextBox"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender3"
                                        TargetControlID="MemberNameRequiredFieldValidator" HighlightCssClass="Highlight">
                                        <Animations>
                                 <OnShow>                                    
                                 <Sequence>   
        <HideAction Visible="true" /> 
        <FadeIn Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        </Sequence>
        </OnShow>
        <OnHide>
        <Sequence>    
        <FadeOut Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        <HideAction Visible="false" />
        </Sequence>
        </OnHide></Animations>
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    IDENTITY
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="IdentityRadioButtonList" runat="server" RepeatDirection="Vertical"
                                        RepeatColumns="2" CssClass="SizeRB">
                                        <asp:ListItem Value="0" Text="ID Card" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Driver Lisensi"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Student Card"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Paspor"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    IDENTITY NUMBER
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="IdentityNumberTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    PHONE NUMBER 1
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="Telephone1TextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    MOBILE NUMBER 1
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="HandPhone1TextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    ADDRESS
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox TextMode="MultiLine" Height="110" CssClass="widthText" ID="AddressTextBox"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    COMPANY
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="CompanyTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    JOB TITLE
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:ListBox ID="JobTitleListBox" runat="server" CssClass="widthDDL"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    JOB LEVEL
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:ListBox ID="JobLevelListBox" runat="server" CssClass="widthDDL"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    SALARY
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:ListBox ID="SalaryListBox" runat="server" CssClass="widthDDL">
                                        <asp:ListItem Value="1" Text="< 1 juta" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="1 juta - 1,9 juta"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="2 juta - 3,9 juta"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="4 juta - 5 juta"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="5 juta - 10 juta"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="10 juta - 15 juta"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="> 15 juta"></asp:ListItem>
                                    </asp:ListBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="formRegister2">
                        <table class="table2">
                            <tr>
                                <td>
                                    MEMBER TITLE
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="MemberTitleRadioButtonList" runat="server" RepeatDirection="Horizontal"
                                        CssClass="SizeRB">
                                        <asp:ListItem Value="0" Text="Mr" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Ms"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Mrs"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    GENDER
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="GenderRadioButtonList" runat="server" RepeatDirection="Horizontal"
                                        CssClass="SizeRB">
                                        <asp:ListItem Text="Male" Value="Male" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    RELIGION
                                </td>
                                <td>
                                    :
                                </td>
                                <td class="textValid">
                                    <asp:DropDownList ID="ReligionDDL" runat="server" CssClass="widthDDL">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="ReligionCustomValidator" runat="server" ErrorMessage="Religion Must Be Filled"
                                        Text="*" ControlToValidate="ReligionDDL" Display="Dynamic" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender4"
                                        TargetControlID="ReligionCustomValidator" HighlightCssClass="Highlight">
                                        <Animations>
                                 <OnShow>                                    
                                 <Sequence>   
        <HideAction Visible="true" /> 
        <FadeIn Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        </Sequence>
        </OnShow>
        <OnHide>
        <Sequence>    
        <FadeOut Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        <HideAction Visible="false" />
        </Sequence>
        </OnHide></Animations>
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    DATE OF BIRTH
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="DateOfBirthTextBox" Width="100" MaxLength="30" BackColor="#CCCCCC"></asp:TextBox>
                                    <asp:Literal ID="BirthDateLiteral" runat="server"></asp:Literal>
                                    <%--<input id="DateButton" type="button" onclick="displayCalendar(DateOfBirthTextBox,'dd-mm-yyyy',this)"
                                value="..." />--%>
                                    <%--
                            <script type="text/javascript" language="javascript">
                                Calendar.setup({
                                    inputField: "DateOfBirthTextBox",
                                    trigger: "DateButton",
                                    onSelect: function() { this.hide() },
                                    showTime: 12,
                                    dateFormat: "%d-%m-%Y"
                                });
                            </script>--%>
                                    <%-- <input size="30" id="f_date1" />
                            <button id="f_btn1">
                                ...</button>

                            <script type="text/javascript">
                                Calendar.setup({
                                    inputField: "f_date1",
                                    trigger: "f_btn1",
                                    onSelect: function() { this.hide() },
                                    showTime: 12,
                                    dateFormat: "%Y-%m-%d %I:%M %p"
                                });
                            </script>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    PLACE OF BIRTH
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="PlaceOfBirthTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    REFERENCE ID
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="ReferenceTextBox" runat="server" CssClass="widthText" MaxLength="13"
                                        OnTextChanged="ReferenceTextBox_TextChanged" AutoPostBack="True"></asp:TextBox>
                                    <asp:HiddenField ID="ReferenceCodeHidden" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    REFERENCE NAME
                                </td>
                                <td>
                                    :
                                </td>
                                <td style="color: Yellow">
                                    <asp:Label ID="ReferenceNameLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    PHONE NUMBER 2
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="Telephone2TextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    MOBILE NUMBER 2
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="HandPhone2TextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    CITY
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="CityDropDownList" runat="server" CssClass="widthDDL">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ZIP CODE
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="ZipCodeTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    EMAIL
                                </td>
                                <td>
                                    :
                                </td>
                                <td class="textValid">
                                    <asp:TextBox ID="EmailTextBox" runat="server" CssClass="widthText" MaxLength="50"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                                        Display="Dynamic" ControlToValidate="EmailTextBox" ErrorMessage="Email is not Valid"
                                        Text="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender5"
                                        TargetControlID="EmailRegularExpressionValidator" HighlightCssClass="Highlight">
                                        <Animations>
                                 <OnShow>                                    
                                 <Sequence>   
        <HideAction Visible="true" /> 
        <FadeIn Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        </Sequence>
        </OnShow>
        <OnHide>
        <Sequence>    
        <FadeOut Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        <HideAction Visible="false" />
        </Sequence>
        </OnHide></Animations>
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    HOBBY
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="HobbyTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    EDUCATION
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="EducationDropDownList" runat="server" CssClass="widthDDL">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    SOURCE INFO
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="SourceInfoTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div style="margin-top: 30px;">
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="BackButton" runat="server" Text="search" CausesValidation="false"
                                            OnClick="BackButton_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                                    </td>
                                    <td style="padding-left: 6px;">
                                        <asp:ImageButton ID="ResetButton" runat="server" Text="Back" CausesValidation="false"
                                            OnClick="ResetButton_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--    <asp:UpdatePanel ID="update" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="errorsPanel" runat="server" Style="display: none; border-style: solid;
                border-width: thin; border-color: #FFDBCA" Width="175px" BackColor="White">
                <div style="text-align: left">
                    <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="BulletList" ShowSummary="true"
                        ValidationGroup="valGroup" />
                    <div style="text-align: right">
                        <asp:Button ID="okBtn" runat="server" Text="Ok" /></div>
                </div>
            </asp:Panel>
            <asp:Label ID="invisibleTarget" runat="server" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="modalPopupEx" runat="server" PopupControlID="errorsPanel"
                TargetControlID="invisibleTarget" CancelControlID="okBtn" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
    </form>
</body>
</html>
