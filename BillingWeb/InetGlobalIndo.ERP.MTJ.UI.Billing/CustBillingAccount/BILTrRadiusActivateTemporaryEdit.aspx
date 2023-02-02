<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="BILTrRadiusActivateTemporaryEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.CustBillingAccountBase.BILTrRadiusActivateTemporaryEdit"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
    function ValidatePeriod(_prmPeriod) 
    { 
    var _tempPeriod = _prmPeriod.value; 
    if (parseInt(_tempPeriod)< 1 || parseInt(_tempPeriod) > 12) 
        {
         _prmPeriod.value = ""; 
        } 
    } 
    
    function ValidateYear(_prmYear)
    { var _tempYear = _prmYear.value; 
    if (parseInt(_tempYear) < 1 || parseInt(_tempYear) > 9999) 
        { 
          _prmYear.value = ""; 
        } 
    }
    </script>

    <script language="javascript" type="text/javascript">
     function ValidateFile(source, args)
     {
        try
         {       
        var fileAndPath=
           document.getElementById(source.controltovalidate).value;
        var lastPathDelimiter=fileAndPath.lastIndexOf("\\");
        var fileNameOnly=fileAndPath.substring(lastPathDelimiter+1);       
        var file_extDelimiter=fileNameOnly.lastIndexOf(".");
        var file_ext=fileNameOnly.substring(file_extDelimiter+1).toLowerCase();
        if(file_ext!="jpg")
             {
             args.IsValid = false;
             if(file_ext!="gif")
               args.IsValid = false;
                  if(file_ext!="png")
                  {
                    args.IsValid = false;
                     return;
                  }
                }
        }catch(err)
        {
          txt="There was an error on this page.\n\n";
          txt+="Error description: " + err.description + "\n\n";
          txt+="Click OK to continue.\n\n";
          txt+=document.getElementById(source.controltovalidate).value;
          alert(txt);
          }
         
           args.IsValid = true;
    }
    </script>

    <style type="text/css">
        .labelLimit
        {
            font-size: 10.5px;
            font-style: italic;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
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
                <table cellpadding="3" cellspacing="0" width="0" border="0">
                    <tr>
                        <td width="180px">
                            Trans No.
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="TransNoTextBox" Width="160" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="10px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            File No.
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="FileNmbrTextBox" Width="160" MaxLength="20" BackColor="#CCCCCC"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Trans Date
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="TransDateTextBox" Width="100" BackColor="#CCCCCC"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Customer Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="CustNameDropDownList" runat="server">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CurrCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                ControlToValidate="CustNameDropDownList" ErrorMessage="Customer Name Must Be Choosed"
                                Text="*"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Period
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="PeriodTextBox" Width="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PeriodTextBoxRequiredFieldValidator" runat="server"
                                ErrorMessage="Period Must Be Filled" Text="*" ControlToValidate="PeriodTextBox"
                                Display="Dynamic"></asp:RequiredFieldValidator><br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Year
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="YearTextBox" Width="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="YearTextBoxRequiredFieldValidator" runat="server"
                                ErrorMessage="Year Must Be Filled" Text="*" ControlToValidate="YearTextBox" Display="Dynamic"></asp:RequiredFieldValidator><br />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Attachment File
                        </td>
                        <td valign="top">
                            :
                        </td>
                        <td>
                            <asp:FileUpload ID="FotoUpLoad" runat="server" />
                            <%--         <asp:RequiredFieldValidator ID="FotoUpLoadRequiredFieldValidator" runat="server"
                                ErrorMessage="Attachment File Must Be Filled" Text="*" ControlToValidate="FotoUpLoad"
                                Display="Dynamic"></asp:RequiredFieldValidator><br />--%>
                            <asp:CustomValidator ID="CustomValidator1" ClientValidationFunction="ValidateFile"
                                runat="server" ControlToValidate="FotoUpLoad" Display="dynamic" ErrorMessage="Upload Jpg, Png Or Gif only"
                                Text="*">
                            </asp:CustomValidator>
                            <br />
                            <asp:Label ID="UploadLabel" runat="server" CssClass="labelLimit"></asp:Label>
                            <br />
                            <asp:Image ID="PictureImage" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Reason
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox TextMode="MultiLine" runat="server" ID="ReasonTextBox" Height="60px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Status
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="StatusLabel" runat="server"></asp:Label>
                            <asp:HiddenField ID="StatusHiddenField" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" border="0" width="0">
                    <tr>
                        <td>
                            <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
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
</asp:Content>
