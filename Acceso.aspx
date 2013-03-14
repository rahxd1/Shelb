<%@ Page Language="vb" 
    Culture="es-MX" 
    AutoEventWireup="false" 
    CodeBehind="Acceso.aspx.vb" 
    Inherits="procomlcd.Acceso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Acceso al Sistema</title>
    <style type="text/css">
            
#menu-navegacion ul {
	list-style-type: disc;
	text-align: left;
	vertical-align: middle;
	margin: 0px;
	padding-top: 11px;
	padding-right: 15px;
	padding-bottom: 15px;
	padding-left: 15px;
	height: 30px;
	}
	
ul {
	margin:0;
	padding:0;
	}

#menu-navegacion li {
	list-style:none;
	background-image: none;
	display: inline;
	margin: 0px;
	}

li {
	list-style:none;
	background:url('Imagenes/list-bullet-02.gif') no-repeat 0 .8em;
	padding:.2em 0 .2em 1em;
	margin-left:0.4em;
	
}

#menu-navegacion a:link,
#menu-navegacion a:visited {
	color:#FFFFFF;
	text-decoration:none;
	height: 30px;
	margin: 0px;
	padding-top: 12px;
	padding-right: 15px;
	padding-bottom: 12px;
	padding-left: 15px;
}


a:link {
	color:#d61719;
	text-decoration:none;
}

        .style5
        {
            width: 751px;
        }
        .style12
        {
            text-align: center;
            font-family: "Eras Medium ITC";
            color: #0099FF;
            height: 55px;
        }
        .style7
        {
            text-align: center;
            font-family: "Eras Medium ITC";
            color: #0099FF;
        }
        .style10
        {
            text-align: center;
            height: 63px;
        }
        .style11
        {
            text-align: center;
            color: #0099FF;
            height: 23px;
        }
        .style6
        {
            text-align: center;
            color: #0099FF;
        }
        .style8
        {
            text-align: right;
            font-family: "Eras Medium ITC";
            color: #0099FF;
            height: 23px;
            width: 346px;
        }
        .style9
        {
            text-align: left;
            height: 23px;
            width: 533px;
        }
        .style13
        {
            text-align: center;
            font-family: "Eras Medium ITC";
            color: #000000;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    
    
        <table align="center" class="style5">
            <tr>
                <td class="style12" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="style7" colspan="2" 
                    style="border-top-style: double; border-width: medium; border-color: #0099FF">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style10" colspan="2">
                    <img alt="" src="logolcd.png" style="width: 189px; height: 76px" /></td>
            </tr>
            <tr>
                <td class="style13" colspan="2">
                    Bienvenido a la Plataforma Web de LCD Logística de Control de Datos.</td>
            </tr>
            <tr>
                <td class="style11" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style6" colspan="2">
                    <asp:Label ID="lblaviso" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style8">
                    <asp:Label ID="lblUsuario" runat="server" style="color: #000000" 
                        Text="Usuario:"></asp:Label>
                </td>
                <td class="style9">
                    <asp:TextBox ID="txtUsuario" runat="server" BackColor="White" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style8">
                    <asp:Label ID="lblContraseña" runat="server" 
                        style="font-family: 'Eras Medium ITC'; color: #000000;" Text="Contraseña:"></asp:Label>
                </td>
                <td class="style9">
                    <asp:TextBox ID="txtContraseña" runat="server" BackColor="White" 
                        style="text-align: left" TextMode="Password" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style6" colspan="2">
                    <asp:Button ID="btnEntrar" runat="server" Text="Entrar" />
                </td>
            </tr>
            <tr>
                <td class="style6" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style6" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style6" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style13" colspan="2">
                    Derechos Reservados © 2013 Shelby Corporativo</td>
            </tr>
            <tr>
                <td class="style7" colspan="2">
                    <asp:Label ID="lblError" runat="server" style="color: #CC0000"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style7" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7" colspan="2" 
                    style="border-top-style: double; border-width: medium; border-color: #0099FF">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style13" colspan="2">
                    Este sitio funciona mejor con los siguientes navegadores:</td>
            </tr>
            <tr>
                <td class="style7" colspan="2">
                    <img alt="" src="firefox.jpg" style="width: 91px; height: 85px" /><img alt="" 
                        src="Internet-Explorer.jpg" style="width: 138px; height: 93px" /></td>
            </tr>
        </table>
    
    
    
    </div>
    </form>
    
 
</body>
</html>
