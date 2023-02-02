<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%
    SMSLibrary.LoginBL _loginBL = new SMSLibrary.LoginBL();
    if ( _loginBL.SweepSound() )
        Response.Write("<EMBED src='ding.wav' autostart=true loop=false volume=100 hidden=true>" +
            "<script language='javascript'>setTimeout ( 'parent.document.forms[0].submit();',3000);</script>");
%>