<%@ Page Language="vb" 
Masterpagefile="~/sitios/Demos/Penaranda/DemoPenaranda.Master"
AutoEventWireup="false" 
CodeBehind="DemoPenarandaAltas.aspx.vb" 
Inherits="procomlcd.DemoPenarandaAltas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DemoPenarandaContent" runat="Server">
  <div id="contenido-columna">
        <table style="width: 100%; vertical-align: bottom;" align="center">
            <tr>
                <td style="text-align: center; color: #FFFFFF; background-color: #004020;" 
                    valign="bottom" colspan="3">
                    &nbsp;Sección de Captura de datos del personal</td>
              
            </tr>
            <tr>
                <td style="text-align: center;" valign="bottom" class="style4" colspan="2">
                    <asp:Label ID="lblAviso" runat="server" 
                        style="font-weight: 700; color: #FF0000"></asp:Label>
                    </td>
                <td style="text-align: center;" class="style5">
                    </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    Nombre (S)</td>
                <td style="text-align: center; margin-left: 40px;" class="style2">
                    <asp:TextBox ID="txtNombre" runat="server" MaxLength="100" Width="367px"></asp:TextBox>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    Apellido Paterno</td>
                <td style="text-align: center; margin-left: 40px;" class="style2">
                    <asp:TextBox ID="txtApPaterno" runat="server" MaxLength="100" Width="367px"></asp:TextBox>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    Apellido Materno</td>
                <td style="text-align: center; margin-left: 40px;" class="style2">
                    <asp:TextBox ID="txtApMaterno" runat="server" MaxLength="100" Width="367px"></asp:TextBox>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    RFC</td>
                <td style="text-align: center; margin-left: 40px;" class="style2">
                    <asp:TextBox ID="txtRFC" runat="server" MaxLength="100" Width="367px"></asp:TextBox>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    CURP</td>
                <td style="text-align: center; margin-left: 40px;" class="style2">
                    <asp:TextBox ID="txtCURP" runat="server" MaxLength="100" Width="367px"></asp:TextBox>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    IMSS</td>
                <td style="text-align: center; margin-left: 40px;" class="style2">
                    <asp:TextBox ID="txtIMSS" runat="server" MaxLength="100" Width="367px"></asp:TextBox>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    PUESTO</td>
                <td style="text-align: left; margin-left: 40px;" class="style2">
                    <asp:DropDownList ID="doPUESTO" runat="server">
                        <asp:ListItem Value="1">PROMOTOR</asp:ListItem>
                    </asp:DropDownList>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    SUCURSAL</td>
                <td style="text-align: left; margin-left: 40px;" class="style2">
                    <asp:DropDownList ID="doSUCURSAL" runat="server">
                        <asp:ListItem Value="1">GDL</asp:ListItem>
                        <asp:ListItem Value="2">DF</asp:ListItem>
                        <asp:ListItem Value="3">MTY</asp:ListItem>
                    </asp:DropDownList>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    FECHA NACIMIENTO(DD/MM/AAAA)</td>
                <td style="text-align: left; margin-left: 40px;" class="style2">
                    <asp:TextBox ID="txtFechaNac" runat="server" MaxLength="100" Width="367px"></asp:TextBox>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    CIUDAD NACIMIENTO</td>
                <td style="text-align: left; margin-left: 40px;" class="style2">
                    <asp:TextBox ID="txtFechaNac0" runat="server" MaxLength="100" Width="367px"></asp:TextBox>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    ESTADO NACIMIENTO</td>
                <td style="text-align: left; margin-left: 40px;" class="style2">
                    <asp:DropDownList ID="doEstadoNac" runat="server">
                        <asp:ListItem Value="0">JALISCO</asp:ListItem>
                        <asp:ListItem Value="2">DF</asp:ListItem>
                        <asp:ListItem Value="2">NUEVO LEON</asp:ListItem>
                    </asp:DropDownList>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    ESCOLARIDAD</td>
                <td style="text-align: left; margin-left: 40px;" class="style2">
                    <asp:DropDownList ID="doSUCURSAL1" runat="server">
                        <asp:ListItem Value="1">BACHILLERATO</asp:ListItem>
                        <asp:ListItem Value="2">UNIVERSIDAD</asp:ListItem>
                    </asp:DropDownList>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    ESTADO CIVIL</td>
                <td style="text-align: left; margin-left: 40px;" class="style2">
                    <asp:DropDownList ID="doSUCURSAL2" runat="server">
                        <asp:ListItem Value="1">SOLTERO</asp:ListItem>
                        <asp:ListItem Value="2">CASADO</asp:ListItem>
                        <asp:ListItem Value="3">UNION LIBRE</asp:ListItem>
                        <asp:ListItem Value="4">VIUDO</asp:ListItem>
                        <asp:ListItem Value="5">DIVORCIADO</asp:ListItem>
                    </asp:DropDownList>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    SEXO</td>
                <td style="text-align: left; margin-left: 40px;" class="style2">
                    <asp:DropDownList ID="doSUCURSAL3" runat="server">
                        <asp:ListItem Value="1">HOMBRE</asp:ListItem>
                        <asp:ListItem Value="2">MUJER</asp:ListItem>
                    </asp:DropDownList>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: center;" valign="bottom" class="style6" colspan="2">
                    DOMICILIO ACTUAL</td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    CALLE</td>
                <td style="text-align: left; margin-left: 40px;" class="style2">
                    <asp:TextBox ID="txtCalle" runat="server" MaxLength="100" Width="367px"></asp:TextBox>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    COLONIA</td>
                <td style="text-align: left; margin-left: 40px;" class="style2">
                    <asp:TextBox ID="txtColonia" runat="server" MaxLength="100" Width="367px"></asp:TextBox>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    CIUDAD</td>
                <td style="text-align: left; margin-left: 40px;" class="style2">
                    <asp:TextBox ID="txtCalle0" runat="server" MaxLength="100" Width="367px"></asp:TextBox>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    ESTADO</td>
                <td style="text-align: left; margin-left: 40px;" class="style2">
                    <asp:DropDownList ID="doEstadoActual" runat="server">
                        <asp:ListItem Value="0">JALISCO</asp:ListItem>
                        <asp:ListItem Value="1">DF</asp:ListItem>
                        <asp:ListItem Value="3">NUEVO LEON</asp:ListItem>
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    CODIGO POSTAL</td>
                <td style="text-align: left; margin-left: 40px;" class="style2">
                    <asp:TextBox ID="txtEstado" runat="server" MaxLength="10" Width="177px"></asp:TextBox>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    TELEFONO</td>
                <td style="text-align: left; margin-left: 40px;" class="style2">
                    <asp:TextBox ID="txtTelefono" runat="server" MaxLength="20" Width="177px"></asp:TextBox>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    CELULAR</td>
                <td style="text-align: left; margin-left: 40px;" class="style2">
                    <asp:TextBox ID="txtCelular" runat="server" MaxLength="20" Width="177px"></asp:TextBox>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    CREDITO INFONAVIT</td>
                <td style="text-align: left; margin-left: 40px;" class="style2">
                    <asp:DropDownList ID="doCredito" runat="server">
                        <asp:ListItem Value="1">SI</asp:ListItem>
                        <asp:ListItem Value="0">NO</asp:ListItem>
                    </asp:DropDownList>
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style6">
                    FOTOGRAFIA</td>
                <td style="text-align: left; margin-left: 40px;" class="style2">
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
              
            </tr>
            <tr>
                <td style="text-align: right;" valign="bottom" class="style1">
                    <asp:Button ID="Button1" runat="server" style="width: 56px" Text="Guardar" />
                    </td>
                <td style="text-align: left; margin-left: 40px;" class="style2">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                        style="height: 26px" />
                </td>
              
            </tr>
            </table>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        
        <div class="clear"></div>
        
    </div>
    </asp:Content> 
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
    {
        height: 2px;
        color: #FFFFFF;
        background-color: #004020;
    }
    .style2
    {
        width: 337px;
        height: 2px;
    }
    .style3
    {
        width: 337px;
            height: 19px;
        }
        .style4
        {
            height: 19px;
        }
        .style5
        {
            width: 226px;
            height: 19px;
        }
        .style6
        {
            height: 2px;
            color: #FFFFFF;
            background-color: #004020;
        }
    </style>
</asp:Content>
