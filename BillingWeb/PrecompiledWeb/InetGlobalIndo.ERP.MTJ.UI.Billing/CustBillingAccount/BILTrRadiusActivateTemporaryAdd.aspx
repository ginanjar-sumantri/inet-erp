<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.CustBillingAccountBase.BILTrRadiusActivateTemporaryAdd, App_Web_cajrmfjr" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <%--    <script type="text/javascript">             
    function AsyncFileUpload1_UploadedComplete(sender, args) {
    try {
        var fileExtension = args.get_fileName();
        var mp3 = fileExtension.indexOf('.mp3');
        var filesizeuploaded = parseInt(args.get_length());
        if (mp3 > 0 && (filesizeuploaded < 2097152)) {
        $get("dvFileInfo").style.display = 'block';
        $get("dvFileErrorInfo").style.display = 'none';
        $get("<%=uploadsuccess.ClientID%>").innerHTML = "File Uploaded Successfully";
        $get("<%=uploadfiledisplay.ClientID%>").innerHTML = args.get_fileName();
        $get("<%=uploadfilesizedisplay.ClientID%>").innerHTML = args.get_length();
        $get("<%=uploadcontenttypedisplay.ClientID%>").innerHTML = args.get_contentType();
        }
        else {
        $get("dvFileErrorInfo").style.display = 'block';
        $get("<%=uploaderror.ClientID%>").innerHTML = "File NOT uploaded.Allowed file extension are mp3 file type only and should be less than 2 MB.";
        $get("dvFileInfo").style.display = 'none';
        return;
        }
        }
        catch (e) 
        {
        alert(e.message);
        }
        }  
    </script>--%>
    <%--VALIDATION FILE EXTENTION--%>
    <%--    <script type="text/javascript">
        
        function uploadError(sender, args) {
            addToClientTable(args.get_fileName(), "<span style='color:red;'>" + args.get_errorMessage() + "</span>");
        }
        function uploadComplete(sender, args) 
        {
            //            var contentType = args.get_contentType();
            //            var text = args.get_length() + " bytes";
            //            if (contentType.length > 0) {
            //                text += ", '" + contentType + "'";
            //            }
            //            addToClientTable(args.get_fileName(), text);

            try {
                    var fileExtension = args.get_fileName();
                    var jpg = fileExtension.indexOf('.jpg');
                    var gif = fileExtension.indexOf('.gif');
                    var png = fileExtension.indexOf('.png');
                    var JPG = fileExtension.indexOf('.JPG');
                    var GIF = fileExtension.indexOf('.GIF');
                    var PNG = fileExtension.indexOf('.PNG');
                    var filesizeuploaded = parseInt(args.get_length());
                    if (jpg > 0 || gif > 0 || png > 0 || JPG > 0 || GIF > 0 || PNG > 0) {
                    $get("dvFileInfo").style.display = 'block';
                    $get("dvFileErrorInfo").style.display = 'none';
                    $get("<%=uploadsuccess.ClientID%>").innerHTML = "File Uploaded Successfully";
                    $get("<%=uploadfiledisplay.ClientID%>").innerHTML = args.get_fileName();
                    $get("<%=uploadfilesizedisplay.ClientID%>").innerHTML = args.get_length();
                    $get("<%=uploadcontenttypedisplay.ClientID%>").innerHTML = args.get_contentType();
                    }
                    else {
                    $get("dvFileErrorInfo").style.display = 'block';
                    $get("<%=uploaderror.ClientID%>").innerHTML = "File NOT uploaded.Allowed file extension are .jpg,.gif,.png file type only.";
                    $get("dvFileInfo").style.display = 'none';
                    return;
                    }
                    }
                    catch (e) 
                    {
                    alert(e.message);
                    }
                    }  
    </script>--%>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                        <td>
                            Attachment File
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="UploadLabel" runat="server" CssClass="labelLimit"></asp:Label>
                            <%--<br />
                            <cc1:AsyncFileUpload ID="AsyncFileUpload1" OnClientUploadError="uploadError"
                                OnClientUploadComplete="uploadComplete" runat="server" Width="400px" UploaderStyle="Modern"
                                UploadingBackColor="#CCFFFF" ThrobberID="myThrobber" />
                            <br />
                            <br />
                            <div>
                                <strong>Your Upload Detail:</strong></div>
                            <div style="display: none; width: 600px" id="dvFileInfo">
                                Upload Status:
                                <asp:Label ID="uploadsuccess" ForeColor="Blue" runat="server" /><br />
                                Uploaded FileName:
                                <asp:Label ID="uploadfiledisplay" ForeColor="Blue" runat="server" /><br />
                                Uploaded File Size :
                                <asp:Label ID="uploadfilesizedisplay" ForeColor="Blue" runat="server" /><br />
                                Uploaded Content Type :<asp:Label ID="uploadcontenttypedisplay" ForeColor="Blue"
                                    runat="server" /><br />
                            </div>
                            <div style="display: none; width: 800px" id="dvFileErrorInfo">
                                Upload Status:
                                <asp:Label ID="uploaderror" ForeColor="Red" runat="server" /><br />
                            </div>
                            <asp:Label runat="server" ID="myThrobber" Style="display: none;"><img align="absmiddle" alt="" src="../images/ajax-loader.gif" /></asp:Label>
                            <%--   <div>
                                <strong>The latest Server-side event:</strong></div>
                            <asp:Label runat="server" Text="&nbsp;" ID="uploadResult" />
                            <br />
                            <br />--%>
                            <%--                            <table style="border-collapse: collapse; border-left: solid 1px #aaaaff; border-top: solid 1px #aaaaff;"
                                runat="server" cellpadding="3" id="clientSide" />
                            <br />
                            <br />--%>
                            <asp:Label ID="lblStatus" runat="server" Style="font-family: Arial; font-size: small;"></asp:Label>
                            <br />
                            <asp:FileUpload ID="FotoUpLoad" runat="server" />
                            <asp:RequiredFieldValidator ID="FotoUpLoadRequiredFieldValidator" runat="server"
                                ErrorMessage="Attachment File Must Be Filled" Text="*" ControlToValidate="FotoUpLoad"
                                Display="Dynamic"></asp:RequiredFieldValidator><br />
                            <asp:CustomValidator ID="CustomValidator1" ClientValidationFunction="ValidateFile"
                                runat="server" ControlToValidate="FotoUpLoad" Display="dynamic" ErrorMessage="Upload Jpg, Png Or Gif only"
                                Text="*">
                            </asp:CustomValidator>
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
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" border="0" width="0">
                    <tr>
                        <td>
                            <asp:ImageButton ID="NextButton" runat="server" OnClick="NextButton_Click" />
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
