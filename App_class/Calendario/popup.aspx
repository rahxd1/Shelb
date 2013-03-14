<%@ Page Language="vb" 
    Culture="es-MX" 
    AutoEventWireup="false"
     Src="PopUp.aspx.vb" 
    Inherits="DotNetJohn.PopUp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<script runat="server">
    Protected Sub Change_Date(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strScript As String
        strScript = "<script>window.opener.document.forms(0)." + control.Value + ".value = '"
        strScript += calDate.SelectedDate.ToString("dd/MMM/yyyy")
        strScript += "';self.close()"
        strScript += "</" + "script>"
        RegisterClientScriptBlock("anything", strScript)
    End Sub
    
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        control.Value = Request.QueryString("textbox").ToString()
    End Sub
</script>

<html>
<head>
<title>PopUp</title>
<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
<meta name=vs_defaultClientScript content="JavaScript">
<meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body MS_POSITIONING="GridLayout">
<form id="Form1" method="post" runat="server">
<asp:Calendar ID="calDate" OnSelectionChanged="Change_Date" Runat="server" />
<input type="hidden" id="control" runat="server" />
</form>
</body>
</html>