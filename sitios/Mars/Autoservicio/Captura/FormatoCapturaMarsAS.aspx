<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Autoservicio/Mars_Autoservicio.Master" 
    AutoEventWireup="false" 
    CodeBehind="FormatoCapturaMarsAS.aspx.vb" 
    Inherits="procomlcd.FormatoCapturaMarsAS"
    title="Mars Autoservicio - Captura" %>
    
<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsAutoservicio" runat="Server">

 
<!--titulo-pagina-->
    <div id="titulo-pagina">Formato captura información</div>

<!--CONTENT CONTAINER-->
    <div id="contenido">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-one-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: right;">
                            <img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink 
                            ID="lnkRegresar" runat="server" 
                            NavigateUrl="~/sitios/Mars/Autoservicio/Captura/RutaMarsAS.aspx">Regresar</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 54px">
                                        Cadena:</td>
                                    <td>
                            <asp:Label ID="lblCadena" runat="server" Font-Bold="True" Width="500px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 54px">
                                        Tienda:</td>
                                    <td>
                            <asp:Label ID="lblTienda" runat="server" Font-Bold="True" Width="500px"></asp:Label>
                                    </td>
                                </tr>
                                </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Recuerda que debes de capturar el número de frentes y exhibiciones en todos 
                            los campos, ninguno debe quedar en blanco.</b><br />
                            <br />
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <%--<asp:Panel ID="pnlAnaquel" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <table>
                                        <tr>
                                            <td bgcolor="#333399" style="color: #FFFFFF; text-align: center;" colspan="2">
                                                <b style="text-align: center">Medida anaquel</b></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                Fecha medición (dd/mm/aaaa):
                                                <asp:TextBox runat="server" ID="txtFechaMedicion" MaxLength="10" Width="100px"></asp:TextBox>
                                                <asp:RegularExpressionValidator id="rvaFechaMedicion" runat="server" 
                                                    ErrorMessage="La fecha de medicion no tiene el formato correcto, el formato debe ser: 'dd/mm/aaaa'" 
                                                    Display="Dynamic" 
                                                    ValidationExpression="\d{2}/\d{2}/\d{4}" ControlToValidate="txtFechaMedicion" 
                                                    ValidationGroup="Mars" >*</asp:RegularExpressionValidator>
                                                <asp:RangeValidator ID="rvFechaMedicion" runat="server" 
                                                    ErrorMessage="La fecha indicada no es valida" 
                                                    ValidationGroup="Mars" ControlToValidate="txtFechaMedicion"
                                                    MaximumValue="31/12/2012" MinimumValue="01/11/2012" Type="Date">*</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfvFechaMedicion" runat="server" 
                                                        ControlToValidate="txtFechaMedicion" 
                                                        ErrorMessage="Completa la información de fecha de medicion del anaquel" 
                                                        ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                            <tr>
                                                <td>
                                                    <b>El anaquel mide:</b>
                                                    <asp:RadioButtonList ID="rdMedidas" runat="server">
                                                        <asp:ListItem Value="1">Menos de 3 metros</asp:ListItem>
                                                        <asp:ListItem Value="2">Entre 3 metros y 9 metros</asp:ListItem>
                                                        <asp:ListItem Value="3">Entre 9 metros y 18 metros</asp:ListItem>
                                                        <asp:ListItem Value="4">Más de 18 metros </asp:ListItem>
                                                        <asp:ListItem Value="5">Tienda inactiva</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="rfvMedidas" runat="server" 
                                                        ControlToValidate="rdMedidas" 
                                                        ErrorMessage="Completa la información de medida de anaquel" 
                                                        ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                    </table>
                                </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>                                
                            </asp:Panel>--%>
                        </td>
                    </tr>
            </table>
            
            <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    
                    <asp:Panel ID="Panel1" runat="server" BackColor="#4F81BD" BorderStyle="Solid" 
                        BorderWidth="1px">
                <table style="width: 100%">
                    <tr>
                        <td style="font-weight: bold; color: #FFFFFF; vertical-align: middle; text-align: center">
                            ANAQUEL</td>
                    </tr>
                </table>
                <br />
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td colspan="2" 
                                style="background-color: #FFC000; text-align: center; font-weight: bold">
                                PERRO</td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" style="width: 101%">
                                    <tr>
                                        <td colspan="5" 
                                            style="background-color: #FFCC66; text-align: center; font-weight: bold;">
                                            MARS</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" 
                                            style="background-color: #FFE07E; font-weight: bold; text-align: center">
                                            Seco</td>
                                        <td rowspan="3" 
                                            style="background-color: #FFE07E; font-weight: bold; text-align: center; width: 57px;">
                                            Húmedo</td>
                                        <td rowspan="3" 
                                            style="background-color: #FFE07E; text-align: center; font-weight: bold; width: 52px;">
                                            Botana</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" 
                                            style="background-color: #FFEEB3; text-align: center; font-weight: bold">
                                            Adulto</td>
                                        <td rowspan="2" 
                                            style="background-color: #FFEEB3; text-align: center; font-weight: bold">
                                            Cachorro</td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #F9F19A; text-align: center; font-weight: bold">
                                            Razas
                                            <br />
                                            pequeñas</td>
                                        <td style="background-color: #F9F19A; text-align: center; font-weight: bold">
                                            Resto
                                            <br />
                                            de adulto</td>
                                    </tr>
                                    <tr>
                                        <td height="50" 
                                            style="background-color: #FFFFCC; text-align: center; font-weight: bold; vertical-align: middle;">
                                            <asp:TextBox ID="txtFrentes15" runat="server" MaxLength="3" 
                                                ValidationGroup="Mars" Width="30px"></asp:TextBox>
                                            <asp:RangeValidator ID="rvFrentes15" runat="server" 
                                                ControlToValidate="txtFrentes15" Display="Dynamic" 
                                                ErrorMessage="El campo Perro adulto seco razas pequeñas(Mars) no contiene números" 
                                                MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                            <asp:RequiredFieldValidator ID="rfvFrentes15" runat="server" 
                                                ControlToValidate="txtFrentes15" 
                                                ErrorMessage="Completa la información de Perro seco adulto razas pequeñas(Mars)" 
                                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td height="50" 
                                            style="background-color: #FFFFCC; text-align: center; font-weight: bold; vertical-align: middle;">
                                            <asp:TextBox ID="txtFrentes1" runat="server" MaxLength="3" 
                                                ValidationGroup="Mars" Width="30px"></asp:TextBox>
                                            <asp:RangeValidator ID="rvaFrentes1" runat="server" 
                                                ControlToValidate="txtFrentes1" Display="Dynamic" 
                                                ErrorMessage="El campo Perro resto de adulto(Mars) no contiene números" 
                                                MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                ControlToValidate="txtFrentes1" 
                                                ErrorMessage="Completa la información de Perro resto de adulto(Mars)" 
                                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td height="50" 
                                            style="background-color: #FFFFCC; text-align: center; font-weight: bold; vertical-align: middle;">
                                            <asp:TextBox ID="txtFrentes2" runat="server" MaxLength="3" 
                                                ValidationGroup="Mars" Width="30px"></asp:TextBox>
                                            <asp:RangeValidator ID="rvFrentes2" runat="server" 
                                                ControlToValidate="txtFrentes2" Display="Dynamic" 
                                                ErrorMessage="El campo Perro cachorro(Mars) no contiene números" 
                                                MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                            <asp:RequiredFieldValidator ID="rfvFrentes2" runat="server" 
                                                ControlToValidate="txtFrentes2" 
                                                ErrorMessage="Completa la información de Perro cachorro(Mars)" 
                                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td height="50" 
                                            style="background-color: #FFFFCC; text-align: center; font-weight: bold; vertical-align: middle; width: 57px;">
                                            <asp:TextBox ID="txtFrentes3" runat="server" MaxLength="3" 
                                                ValidationGroup="Mars" Width="30px"></asp:TextBox>
                                            <asp:RangeValidator ID="rvFrentes3" runat="server" 
                                                ControlToValidate="txtFrentes3" Display="Dynamic" 
                                                ErrorMessage="El campo Perro húmedo(Mars) no contiene números" 
                                                MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                            <asp:RequiredFieldValidator ID="rfvFrentes3" runat="server" 
                                                ControlToValidate="txtFrentes3" 
                                                ErrorMessage="Completa la información de Perro húmedo(Mars)" 
                                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td height="50" 
                                            style="background-color: #FFFFCC; text-align: center; font-weight: bold; vertical-align: middle; width: 52px;">
                                            <asp:TextBox ID="txtFrentes4" runat="server" MaxLength="3" 
                                                ValidationGroup="Mars" Width="30px"></asp:TextBox>
                                            <asp:RangeValidator ID="rvFrentes4" runat="server" 
                                                ControlToValidate="txtFrentes4" Display="Dynamic" 
                                                ErrorMessage="El campo Perro botana(Mars) no contiene números" 
                                                MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                            <asp:RequiredFieldValidator ID="rfvFrentes4" runat="server" 
                                                ControlToValidate="txtFrentes4" 
                                                ErrorMessage="Completa la información de Perro botana(Mars)" 
                                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td colspan="5" 
                                            style="text-align: center; background-color: #7F7F7F; font-weight: bold;">
                                            COMPETENCIA</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" 
                                            style="font-weight: bold; text-align: center; background-color: #A6A5A6">
                                            Seco</td>
                                        <td rowspan="3" 
                                            style="background-color: #A6A5A6; text-align: center; font-weight: bold; width: 59px;">
                                            Húmedo</td>
                                        <td rowspan="3" 
                                            style="background-color: #A6A5A6; text-align: center; font-weight: bold; width: 55px;">
                                            Botana</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" 
                                            style="background-color: #BFBEBF; text-align: center; font-weight: bold">
                                            Adulto</td>
                                        <td rowspan="2" 
                                            style="font-weight: bold; text-align: center; background-color: #BFBEBF">
                                            Cachorro</td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #D9D9D9; text-align: center; font-weight: bold">
                                            Razas
                                            <br />
                                            pequeñas</td>
                                        <td style="background-color: #D9D9D9; text-align: center; font-weight: bold">
                                            Resto
                                            <br />
                                            de adulto</td>
                                    </tr>
                                    <tr>
                                        <td height="50" 
                                            style="background-color: #F2F2F2; text-align: center; font-weight: bold; vertical-align: middle;">
                                            <asp:TextBox ID="txtFrentes16" runat="server" MaxLength="3" 
                                                ValidationGroup="Mars" Width="30px"></asp:TextBox>
                                            <asp:RangeValidator ID="rvFrentes16" runat="server" 
                                                ControlToValidate="txtFrentes16" Display="Dynamic" 
                                                ErrorMessage="El campo Perro adulto seco razas pequeñas(Competencia) no contiene números" 
                                                MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                            <asp:RequiredFieldValidator ID="rfvFrentes16" runat="server" 
                                                ControlToValidate="txtFrentes16" 
                                                ErrorMessage="Completa la información de Perro seco adulto razas pequeñas(Competencia)" 
                                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td height="50" 
                                            style="background-color: #F2F2F2; text-align: center; font-weight: bold; vertical-align: middle;">
                                            <asp:TextBox ID="txtFrentes5" runat="server" MaxLength="3" 
                                                ValidationGroup="Mars" Width="30px"></asp:TextBox>
                                            <asp:RangeValidator ID="rvFrentes5" runat="server" 
                                                ControlToValidate="txtFrentes5" Display="Dynamic" 
                                                ErrorMessage="El campo Perro resto de adulto(Competencia) no contiene números" 
                                                MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                            <asp:RequiredFieldValidator ID="rfvFrentes5" runat="server" 
                                                ControlToValidate="txtFrentes5" 
                                                ErrorMessage="Completa la información de Perro resto de adulto(Competencia)" 
                                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td height="50" 
                                            style="background-color: #F2F2F2; text-align: center; font-weight: bold; vertical-align: middle;">
                                            <asp:TextBox ID="txtFrentes6" runat="server" MaxLength="3" 
                                                ValidationGroup="Mars" Width="30px"></asp:TextBox>
                                            <asp:RangeValidator ID="rvFrentes6" runat="server" 
                                                ControlToValidate="txtFrentes6" Display="Dynamic" 
                                                ErrorMessage="El campo Perro seco cachorro(Competencia) no contiene números" 
                                                MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                            <asp:RequiredFieldValidator ID="rfvFrentes6" runat="server" 
                                                ControlToValidate="txtFrentes6" 
                                                ErrorMessage="Completa la información de Perro seco cachorro(Competencia)" 
                                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td height="50" 
                                            style="background-color: #F2F2F2; text-align: center; font-weight: bold; vertical-align: middle; width: 59px;">
                                            <asp:TextBox ID="txtFrentes7" runat="server" MaxLength="3" 
                                                ValidationGroup="Mars" Width="30px"></asp:TextBox>
                                            <asp:RangeValidator ID="rvFrentes7" runat="server" 
                                                ControlToValidate="txtFrentes7" Display="Dynamic" 
                                                ErrorMessage="El campo Perro húmedo(Competencia) no contiene números" 
                                                MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                            <asp:RequiredFieldValidator ID="rfvFrentes7" runat="server" 
                                                ControlToValidate="txtFrentes7" 
                                                ErrorMessage="Completa la información de Perro húmedo(Competencia)" 
                                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td height="50" 
                                            style="background-color: #F2F2F2; text-align: center; font-weight: bold; vertical-align: middle; width: 55px;">
                                            <asp:TextBox ID="txtFrentes8" runat="server" MaxLength="3" 
                                                ValidationGroup="Mars" Width="30px"></asp:TextBox>
                                            <asp:RangeValidator ID="rvFrentes8" runat="server" 
                                                ControlToValidate="txtFrentes8" Display="Dynamic" 
                                                ErrorMessage="El campo Perro botana(Competencia) no contiene números" 
                                                MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                            <asp:RequiredFieldValidator ID="rfvFrentes8" runat="server" 
                                                ControlToValidate="txtFrentes8" 
                                                ErrorMessage="Completa la información de Perro botana(Competencia)" 
                                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2" 
                                style="background-color: #7030A0; text-align: center; font-weight: bold; color: #FFFFFF">
                                GATO</td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td colspan="3" 
                                            style="text-align: center; background-color: #604A7B; font-weight: bold;">
                                            MARS</td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold; text-align: center; background-color: #B3A2C7">
                                            Seco</td>
                                        <td style="background-color: #B3A2C7; text-align: center; font-weight: bold">
                                            Húmedo</td>
                                        <td style="background-color: #B3A2C7; text-align: center; font-weight: bold">
                                            Botana</td>
                                    </tr>
                                    <tr>
                                        <td height="50" 
                                            style="background-color: #E6E0EC; text-align: center; font-weight: bold; vertical-align: middle;">
                                            <asp:TextBox ID="txtFrentes9" runat="server" MaxLength="3" 
                                                ValidationGroup="Mars" Width="30px"></asp:TextBox>
                                            <asp:RangeValidator ID="rvFrentes9" runat="server" 
                                                ControlToValidate="txtFrentes9" Display="Dynamic" 
                                                ErrorMessage="El campo Gato seco(Mars) no contiene números" MaximumValue="300" 
                                                MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                            <asp:RequiredFieldValidator ID="rfvFrentes9" runat="server" 
                                                ControlToValidate="txtFrentes9" 
                                                ErrorMessage="Completa la información de Gato seco(Mars)" 
                                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td height="50" 
                                            style="background-color: #E6E0EC; text-align: center; font-weight: bold; vertical-align: middle;">
                                            <asp:TextBox ID="txtFrentes10" runat="server" MaxLength="3" 
                                                ValidationGroup="Mars" Width="30px"></asp:TextBox>
                                            <asp:RangeValidator ID="rvFrentes10" runat="server" 
                                                ControlToValidate="txtFrentes10" Display="Dynamic" 
                                                ErrorMessage="El campo Gato húmedo(Mars) no contiene números" 
                                                MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                            <asp:RequiredFieldValidator ID="rfvFrentes10" runat="server" 
                                                ControlToValidate="txtFrentes10" 
                                                ErrorMessage="Completa la información de Gato húmedo(Mars)" 
                                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td height="50" 
                                            style="background-color: #E6E0EC; text-align: center; font-weight: bold; vertical-align: middle;">
                                            <asp:TextBox ID="txtFrentes11" runat="server" MaxLength="3" 
                                                ValidationGroup="Mars" Width="30px"></asp:TextBox>
                                            <asp:RangeValidator ID="rvFrentes11" runat="server" 
                                                ControlToValidate="txtFrentes11" Display="Dynamic" 
                                                ErrorMessage="El campo Gato botana(Mars) no contiene números" 
                                                MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                            <asp:RequiredFieldValidator ID="rfvFrentes11" runat="server" 
                                                ControlToValidate="txtFrentes11" 
                                                ErrorMessage="Completa la información de Gato botana(Mars)" 
                                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td colspan="3" 
                                            style="text-align: center; background-color: #7F7F7F; font-weight: bold;">
                                            COMPETENCIA</td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold; text-align: center; background-color: #A6A5A6">
                                            Seco</td>
                                        <td style="background-color: #A6A5A6; text-align: center; font-weight: bold">
                                            Húmedo</td>
                                        <td style="background-color: #A6A5A6; text-align: center; font-weight: bold">
                                            Botana</td>
                                    </tr>
                                    <tr>
                                        <td height="50" 
                                            style="background-color: #F2F2F2; text-align: center; font-weight: bold; vertical-align: middle;">
                                            <asp:TextBox ID="txtFrentes12" runat="server" MaxLength="3" 
                                                ValidationGroup="Mars" Width="30px"></asp:TextBox>
                                            <asp:RangeValidator ID="rvFrentes12" runat="server" 
                                                ControlToValidate="txtFrentes12" Display="Dynamic" 
                                                ErrorMessage="El campo Gato seco(Competencia) no contiene números" 
                                                MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                            <asp:RequiredFieldValidator ID="rfvFrentes12" runat="server" 
                                                ControlToValidate="txtFrentes12" 
                                                ErrorMessage="Completa la información de Gato seco(Competencia)" 
                                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td height="50" 
                                            style="background-color: #F2F2F2; text-align: center; font-weight: bold; vertical-align: middle;">
                                            <asp:TextBox ID="txtFrentes13" runat="server" MaxLength="3" 
                                                ValidationGroup="Mars" Width="30px"></asp:TextBox>
                                            <asp:RangeValidator ID="rvFrentes13" runat="server" 
                                                ControlToValidate="txtFrentes13" Display="Dynamic" 
                                                ErrorMessage="El campo Gato húmedo(Competencia) no contiene números" 
                                                MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                            <asp:RequiredFieldValidator ID="rfvFrentes13" runat="server" 
                                                ControlToValidate="txtFrentes13" 
                                                ErrorMessage="Completa la información de Gato húmedo(Competencia)" 
                                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td height="50" 
                                            style="background-color: #F2F2F2; text-align: center; font-weight: bold; vertical-align: middle;">
                                            <asp:TextBox ID="txtFrentes14" runat="server" MaxLength="3" 
                                                ValidationGroup="Mars" Width="30px"></asp:TextBox>
                                            <asp:RangeValidator ID="rvFrentes14" runat="server" 
                                                ControlToValidate="txtFrentes14" Display="Dynamic" 
                                                ErrorMessage="El campo Gato botana(Competencia) no contiene números" 
                                                MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                            <asp:RequiredFieldValidator ID="rfvFrentes14" runat="server" 
                                                ControlToValidate="txtFrentes14" 
                                                ErrorMessage="Completa la información de Gato botana(Competencia)" 
                                                ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                </table>
                <br />
                    </asp:Panel>
                    <br />
                  
                    <asp:Panel ID="pnlImagenes" runat="server" 
                    ScrollBars="Horizontal" BorderStyle="Solid" BorderWidth="1px">
                    <table style="width: 100%">
                    <tr>
                        <td colspan="10" 
                            style="background-color: #4BACC6; text-align: center; color: #FFFFFF; font-weight: bold">
                            IDENTIFICA TUS EXHIBICIONES                    </tr>
                    <tr>
                        <td style="background-color: #D0E3EA; text-align: center; font-weight: bold; vertical-align: middle;">
                            Pouchero</td>
                        <td style="background-color: #D0E3EA; text-align: center; font-weight: bold; vertical-align: middle;">
                            Latero</td>
                        <td style="background-color: #D0E3EA; text-align: center; font-weight: bold; vertical-align: middle;">
                            Tira</td>
                        <td style="background-color: #D0E3EA; text-align: center; font-weight: bold; vertical-align: middle;">
                            Balcón</td>
                        <td style="background-color: #D0E3EA; text-align: center; font-weight: bold; vertical-align: middle;">
                            Cabecera</td>
                        <td style="background-color: #D0E3EA; text-align: center; font-weight: bold; vertical-align: middle;">
                            Isla</td>
                        <td style="background-color: #D0E3EA; text-align: center; font-weight: bold; vertical-align: middle;">
                            Botadero</td>
                        <td style="background-color: #D0E3EA; text-align: center; font-weight: bold; vertical-align: middle;">
                            Mix
                            <br />
                            feeding</td>
                        <td style="background-color: #D0E3EA; text-align: center; font-weight: bold; vertical-align: middle;">
                            Mini
                            <br />
                            rack</td>
                        <td style="background-color: #D0E3EA; text-align: center; font-weight: bold; vertical-align: middle;">
                            Wet<br />
                            movil</td>
                    </tr>
                    <tr>
                        <td>
                            <img alt="" src="Img/pouchero.jpg" /></td>
                        <td>
                            <img alt="" src="Img/latero.jpg" /></td>
                        <td>
                            <img alt="" src="Img/Tiras.jpg" /></td>
                        <td>
                            <img alt="" src="Img/balcones.jpg" /></td>
                        <td>
                            <img alt="" src="Img/cabeceras.jpg" /></td>
                        <td>
                            <img alt="" src="Img/Isla.jpg" width="100" /></td>
                        <td>
                            <img alt="" src="Img/Botadero.jpg" width="100"/></td>
                        <td>
                            <img alt="" src="Img/mixfeeding.jpg" width="100"/></td>
                        <td>
                            <img alt="" src="Img/minirack.jpg" width="100"/></td>
                        <td>
                            <img alt="" src="Img/Wet movil.jpg" width="100"/></td>
                    </tr>
            </table>
                </asp:Panel>   
                    <br />
                
                    <asp:Panel ID="Panel2" runat="server" BackColor="#4F81BD" 
                ScrollBars="Both" BorderStyle="Solid" BorderWidth="1px" Height="350px">
                <table style="width: 100%">
                    <tr>
                        <td style="font-weight: bold; color: #FFFFFF; vertical-align: middle; text-align: center">
                            PUNTOS DE INTERRUPCIÓN</td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td colspan="4" 
                                        style="background-color: #F79646; text-align: center; font-weight: bold; color: #FFFFFF;">
                                        ANAQUEL</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td style="background-color: #FCDDCF; text-align: center; font-weight: bold">
                                                    Pouchero</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #FDEFE9; text-align: center;">
                                                    <asp:TextBox ID="txtAnaquel12" runat="server" MaxLength="3" 
                                                        ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvAnaquel12" runat="server" 
                                                        ControlToValidate="txtAnaquel12" 
                                                        ErrorMessage="Completa la información de anaquel Pouchero" 
                                                        ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="rvAnaquel12" runat="server" 
                                                        ControlToValidate="txtAnaquel12" Display="Dynamic" 
                                                        ErrorMessage="El campo anaquel Pouchero no contiene números" MaximumValue="300" 
                                                        MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #FCDDCF; text-align: center; font-weight: bold">
                                                    Productos</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #FDEFE9">
                                                    <asp:DropDownList ID="cmbProductoAnaquel12A" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #FDEFE9">
                                                    <asp:DropDownList ID="cmbProductoAnaquel12B" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #FDEFE9">
                                                    <asp:DropDownList ID="cmbProductoAnaquel12C" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #FDEFE9">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #FDEFE9">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #FDEFE9">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #FDEFE9">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td style="background-color: #FCDDCF; text-align: center; font-weight: bold">
                                                    Latero</td>
                                            </tr>
                                            <tr>
                                                <td height="35" 
                                                    style="background-color: #FDEFE9; text-align: center; font-weight: bold">
                                                    <asp:TextBox ID="txtAnaquel11" runat="server" MaxLength="3" 
                                                        ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvAnaquel11" runat="server" 
                                                        ControlToValidate="txtAnaquel11" 
                                                        ErrorMessage="Completa la información de anaquel Latero" ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="rvanaquel11" runat="server" 
                                                        ControlToValidate="txtAnaquel11" Display="Dynamic" 
                                                        ErrorMessage="El campo anaquel Latero no contiene números" MaximumValue="300" 
                                                        MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #FCDDCF; text-align: center; font-weight: bold">
                                                    Productos</td>
                                            </tr>
                                            <tr>
                                                <td height="35" 
                                                    style="background-color: #FDEFE9; text-align: center; font-weight: bold">
                                                    <asp:DropDownList ID="cmbProductoAnaquel11A" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" 
                                                    style="background-color: #FDEFE9; text-align: center; font-weight: bold">
                                                    <asp:DropDownList ID="cmbProductoAnaquel11B" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" 
                                                    style="background-color: #FDEFE9; text-align: center; font-weight: bold">
                                                    <asp:DropDownList ID="cmbProductoAnaquel11C" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" 
                                                    style="background-color: #FDEFE9; text-align: center; font-weight: bold">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35" 
                                                    style="background-color: #FDEFE9; text-align: center; font-weight: bold">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35" 
                                                    style="background-color: #FDEFE9; text-align: center; font-weight: bold">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35" 
                                                    style="background-color: #FDEFE9; text-align: center; font-weight: bold">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" 
                                            style="width: 100%; background-color: #FDEFE9; text-align: center; font-weight: bold;">
                                            <tr>
                                                <td style="background-color: #FCDDCF; height: 15px;">
                                                    Tira</td>
                                            </tr>
                                            <tr>
                                                <td height="35">
                                                    <asp:TextBox ID="txtAnaquel10" runat="server" MaxLength="3" 
                                                        ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvAnaquel10" runat="server" 
                                                        ControlToValidate="txtAnaquel10" 
                                                        ErrorMessage="Completa la información de anaquel Tira" ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="rvAnaquel10" runat="server" 
                                                        ControlToValidate="txtAnaquel10" Display="Dynamic" 
                                                        ErrorMessage="El campo anaquel Tira no contiene números" MaximumValue="300" 
                                                        MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #FCDDCF">
                                                    Productos</td>
                                            </tr>
                                            <tr>
                                                <td height="35">
                                                    <asp:DropDownList ID="cmbProductoAnaquel10A" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35">
                                                    <asp:DropDownList ID="cmbProductoAnaquel10B" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35">
                                                    <asp:DropDownList ID="cmbProductoAnaquel10C" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" 
                                            style="width: 100%; background-color: #FDEFE9; text-align: center; font-weight: bold;">
                                            <tr>
                                                <td style="background-color: #FCDDCF; height: 15px;">
                                                    Balcón</td>
                                            </tr>
                                            <tr>
                                                <td height="35">
                                                    <asp:TextBox ID="txtAnaquel9" runat="server" MaxLength="3" 
                                                        ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvAnaquel9" runat="server" 
                                                        ControlToValidate="txtAnaquel9" 
                                                        ErrorMessage="Completa la información de anaquel Balcón" ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="rvAnaquel9" runat="server" 
                                                        ControlToValidate="txtAnaquel9" Display="Dynamic" 
                                                        ErrorMessage="El campo anaquel Balcón no contiene números" MaximumValue="300" 
                                                        MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #FCDDCF">
                                                    Productos</td>
                                            </tr>
                                            <tr>
                                                <td height="35">
                                                    <asp:DropDownList ID="cmbProductoAnaquel9A" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35">
                                                    <asp:DropDownList ID="cmbProductoAnaquel9B" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35">
                                                    <asp:DropDownList ID="cmbProductoAnaquel9C" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td colspan="4" 
                                        style="background-color: #4F81BD; text-align: center; font-weight: bold; color: #FFFFFF;">
                                        PASILLO MASCOTA</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" 
                                            
                                            style="width: 100%; background-color: #FDEFE9; text-align: center; font-weight: bold; height: 100%;">
                                            <tr>
                                                <td style="background-color: #D0D8E8">
                                                    Cabecera</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    <asp:TextBox ID="txtPasillo6" runat="server" MaxLength="3" 
                                                        ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvPasillo6" runat="server" 
                                                        ControlToValidate="txtPasillo6" 
                                                        ErrorMessage="Completa la información de pasillo mascota Cabecera" 
                                                        ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="rvPasillo6" runat="server" 
                                                        ControlToValidate="txtPasillo6" Display="Dynamic" 
                                                        ErrorMessage="El campo pasillo mascota Cabecera no contiene números" 
                                                        MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #D0D8E8">
                                                    Productos</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    <asp:DropDownList ID="cmbProductoPasillo6A" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    <asp:DropDownList ID="cmbProductoPasillo6B" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    <asp:DropDownList ID="cmbProductoPasillo6C" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" 
                                            
                                            style="width: 100%; background-color: #FDEFE9; text-align: center; font-weight: bold; height: 256px;">
                                            <tr>
                                                <td style="background-color: #D0D8E8">
                                                    Isla</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    <asp:TextBox ID="txtPasillo1" runat="server" MaxLength="3" 
                                                        ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvPasillo1" runat="server" 
                                                        ControlToValidate="txtPasillo1" 
                                                        ErrorMessage="Completa la información de pasillo mascota Isla" 
                                                        ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="rvPasillo1" runat="server" 
                                                        ControlToValidate="txtPasillo1" Display="Dynamic" 
                                                        ErrorMessage="El campo pasillo mascota Isla no contiene números" 
                                                        MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #D0D8E8">
                                                    Productos</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    <asp:DropDownList ID="cmbProductoPasillo1A" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    <asp:DropDownList ID="cmbProductoPasillo1B" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    <asp:DropDownList ID="cmbProductoPasillo1C" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" 
                                            
                                            style="width: 100%; background-color: #FDEFE9; text-align: center; font-weight: bold; height: 100%;" 
                                            width="100%">
                                            <tr>
                                                <td style="background-color: #D0D8E8">
                                                    Botadero</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    <asp:TextBox ID="txtPasillo15" runat="server" MaxLength="3" 
                                                        ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvPasillo15" runat="server" 
                                                        ControlToValidate="txtPasillo15" 
                                                        ErrorMessage="Completa la información de pasillo mascota Botadero" 
                                                        ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="rvPasillo15" runat="server" 
                                                        ControlToValidate="txtPasillo15" Display="Dynamic" 
                                                        ErrorMessage="El campo pasillo mascota Botadero no contiene números" 
                                                        MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #D0D8E8">
                                                    Productos</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    <asp:DropDownList ID="cmbProductoPasillo15A" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    <asp:DropDownList ID="cmbProductoPasillo15B" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    <asp:DropDownList ID="cmbProductoPasillo15C" runat="server" Height="22px" 
                                                        style="margin-left: 0px" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" 
                                            
                                            style="width: 100%; background-color: #FDEFE9; text-align: center; font-weight: bold; height: 311px;">
                                            <tr>
                                                <td style="background-color: #D0D8E8; height: 15px;">
                                                    Otras exhibiciones</td>
                                                <td style="background-color: #D0D8E8; height: 15px;">
                                                    Cantidad</td>
                                                <td style="background-color: #D0D8E8; height: 15px;">
                                                    Productos</td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    <asp:DropDownList ID="cmbExhibicionesPasillo1" runat="server" Height="21px" 
                                                        style="margin-left: 0px" Width="114px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    <asp:TextBox ID="txtExhibicionesPasillo1" runat="server" MaxLength="3" 
                                                        ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                    <asp:RangeValidator ID="rvExhibicionesPasillo1" runat="server" 
                                                        ControlToValidate="txtExhibicionesPasillo1" Display="Dynamic" 
                                                        ErrorMessage="El campo pasillo mascota Otras exhibiciones no contiene números" 
                                                        MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                    <asp:RequiredFieldValidator ID="rfvExhibicionesPasillo1" runat="server" 
                                                        ControlToValidate="txtExhibicionesPasillo1" 
                                                        ErrorMessage="Completa la información de pasillo mascota Otras exhibiciones" 
                                                        ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td style="background-color: #E9EDF4">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td height="30">
                                                                <asp:DropDownList ID="cmbProductoExhibicionesPasillo1A" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="30">
                                                                <asp:DropDownList ID="cmbProductoExhibicionesPasillo1B" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="30">
                                                                <asp:DropDownList ID="cmbProductoExhibicionesPasillo1C" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    <asp:DropDownList ID="cmbExhibicionesPasillo2" runat="server" Height="21px" 
                                                        style="margin-left: 0px" Width="114px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    <asp:TextBox ID="txtExhibicionesPasillo2" runat="server" MaxLength="3" 
                                                        ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                    <asp:RangeValidator ID="rvExhibicionesPasillo2" runat="server" 
                                                        ControlToValidate="txtExhibicionesPasillo2" Display="Dynamic" 
                                                        ErrorMessage="El campo pasillo mascota Otras exhibiciones no contiene números" 
                                                        MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                    <asp:RequiredFieldValidator ID="rfvExhibicionesPasillo2" runat="server" 
                                                        ControlToValidate="txtExhibicionesPasillo2" 
                                                        ErrorMessage="Completa la información de pasillo mascota Otras exhibiciones" 
                                                        ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td style="background-color: #E9EDF4">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td height="30">
                                                                <asp:DropDownList ID="cmbProductoExhibicionesPasillo2A" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="30">
                                                                <asp:DropDownList ID="cmbProductoExhibicionesPasillo2B" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="30">
                                                                <asp:DropDownList ID="cmbProductoExhibicionesPasillo2C" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    <asp:DropDownList ID="cmbExhibicionesPasillo3" runat="server" Height="21px" 
                                                        style="margin-left: 0px" Width="114px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td height="35" style="background-color: #E9EDF4">
                                                    <asp:TextBox ID="txtExhibicionesPasillo3" runat="server" MaxLength="3" 
                                                        ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                    <asp:RangeValidator ID="rvExhibicionesPasillo3" runat="server" 
                                                        ControlToValidate="txtExhibicionesPasillo3" Display="Dynamic" 
                                                        ErrorMessage="El campo pasillo mascota Otras exhibiciones no contiene números" 
                                                        MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                    <asp:RequiredFieldValidator ID="rfvExhibicionesPasillo3" runat="server" 
                                                        ControlToValidate="txtExhibicionesPasillo3" 
                                                        ErrorMessage="Completa la información de pasillo mascota Otras exhibiciones" 
                                                        ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td style="background-color: #E9EDF4">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td height="30">
                                                                <asp:DropDownList ID="cmbProductoExhibicionesPasillo3A" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="30">
                                                                <asp:DropDownList ID="cmbProductoExhibicionesPasillo3B" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="30">
                                                                <asp:DropDownList ID="cmbProductoExhibicionesPasillo3C" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td colspan="5" 
                                                    style="background-color: #FF0000; text-align: center; font-weight: bold; color: #FFFFFF;">
                                                    ZONA CALIENTE</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; font-weight: bold; color: #000000;">
                                                    <table border="0" cellpadding="0" cellspacing="0" 
                                                        style="width: 100%; background-color: #FDEFE9; text-align: center; font-weight: bold;">
                                                        <tr>
                                                            <td style="background-color: #FF9797; height: 35px;">
                                                                Isla</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                <asp:TextBox ID="txtCaliente1" runat="server" MaxLength="3" 
                                                                    ValidationGroup="Mars" Width="40px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvCaliente1" runat="server" 
                                                                    ControlToValidate="txtCaliente1" 
                                                                    ErrorMessage="Completa la información de zona caliente Isla" 
                                                                    ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                                <asp:RangeValidator ID="rvCaliente1" runat="server" 
                                                                    ControlToValidate="txtCaliente1" Display="Dynamic" 
                                                                    ErrorMessage="El campo zona caliente Isla no contiene números" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="background-color: #FF9797">
                                                                Productos</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                <asp:DropDownList ID="cmbProductoCaliente1A" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                <asp:DropDownList ID="cmbProductoCaliente1B" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                <asp:DropDownList ID="cmbProductoCaliente1C" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="text-align: center; font-weight: bold; color: #000000;">
                                                    <table border="0" cellpadding="0" cellspacing="0" 
                                                        style="width: 100%; background-color: #FDEFE9; text-align: center; font-weight: bold;">
                                                        <tr>
                                                            <td style="background-color: #FF9797; height: 35px;">
                                                                Botadero</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                <asp:TextBox ID="txtCaliente15" runat="server" MaxLength="3" 
                                                                    ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvCaliente15" runat="server" 
                                                                    ControlToValidate="txtCaliente15" 
                                                                    ErrorMessage="Completa la información de zona caliente Botadero" 
                                                                    ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                                <asp:RangeValidator ID="rvCaliente15" runat="server" 
                                                                    ControlToValidate="txtCaliente15" Display="Dynamic" 
                                                                    ErrorMessage="El campo zona caliente Botadero no contiene números" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="background-color: #FF9797">
                                                                Productos</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                <asp:DropDownList ID="cmbProductoCaliente15A" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                <asp:DropDownList ID="cmbProductoCaliente15B" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                <asp:DropDownList ID="cmbProductoCaliente15C" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="text-align: center; font-weight: bold; color: #000000;">
                                                    <table border="0" cellpadding="0" cellspacing="0" 
                                                        style="width: 100%; background-color: #FDEFE9; text-align: center; font-weight: bold;">
                                                        <tr>
                                                            <td style="background-color: #FF9797; height: 35px;">
                                                                Mix feeding</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                <asp:TextBox ID="txtCaliente3" runat="server" MaxLength="3" 
                                                                    ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvCaliente3" runat="server" 
                                                                    ControlToValidate="txtCaliente3" 
                                                                    ErrorMessage="Completa la información de zona caliente Mix feeding" 
                                                                    ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                                <asp:RangeValidator ID="rvCaliente3" runat="server" 
                                                                    ControlToValidate="txtCaliente3" Display="Dynamic" 
                                                                    ErrorMessage="El campo zona caliente Mix feeding" MaximumValue="300" 
                                                                    MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="background-color: #FF9797">
                                                                Productos</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                <asp:DropDownList ID="cmbProductoCaliente3A" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                <asp:DropDownList ID="cmbProductoCaliente3B" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                <asp:DropDownList ID="cmbProductoCaliente3C" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="text-align: center; font-weight: bold; color: #000000;">
                                                    <table border="0" cellpadding="0" cellspacing="0" 
                                                        style="width: 100%; background-color: #FDEFE9; text-align: center; font-weight: bold;">
                                                        <tr>
                                                            <td style="background-color: #FF9797; height: 35px;">
                                                                Mini rack</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                <asp:TextBox ID="txtCaliente5" runat="server" MaxLength="3" 
                                                                    ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvCaliente5" runat="server" 
                                                                    ControlToValidate="txtCaliente5" 
                                                                    ErrorMessage="Completa la información de zona caliente Mini rack" 
                                                                    ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                                <asp:RangeValidator ID="rvCaliente5" runat="server" 
                                                                    ControlToValidate="txtCaliente5" Display="Dynamic" 
                                                                    ErrorMessage="El campo zona caliente Mini rack no contiene números" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="background-color: #FF9797">
                                                                Productos</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                <asp:DropDownList ID="cmbProductoCaliente5A" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                <asp:DropDownList ID="cmbProductoCaliente5B" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                <asp:DropDownList ID="cmbProductoCaliente5C" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="text-align: center; font-weight: bold; color: #000000; background-color: #FDEFE9;">
                                                    <table border="0" cellpadding="0" cellspacing="0" 
                                                        
                                                        
                                                        
                                                        style="width: 100%; background-color: #FDEFE9; text-align: center; font-weight: bold; height: 330px;">
                                                        <tr>
                                                            <td style="background-color: #FF9797; height: 34px;" height="35">
                                                                </td>
                                                            <td style="background-color: #FF9797; height: 34px;" height="35">
                                                                Otras exhibiciones</td>
                                                            <td style="background-color: #FF9797; height: 34px;" height="35">
                                                                Cantidad</td>
                                                            <td style="background-color: #FF9797; height: 34px;" height="35">
                                                                Productos</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9; width: 125px;">
                                                                &nbsp;</td>
                                                            <td height="35" style="background-color: #FDEFE9; width: 125px;">
                                                                <asp:DropDownList ID="cmbExhibicionesCaliente1" runat="server" Height="21px" 
                                                                    style="margin-left: 0px" Width="114px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                <asp:TextBox ID="txtExhibicionesCaliente1" runat="server" MaxLength="3" 
                                                                    ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                                <asp:RangeValidator ID="rvExhibicionesCaliente1" runat="server" 
                                                                    ControlToValidate="txtExhibicionesCaliente1" Display="Dynamic" 
                                                                    ErrorMessage="El campo de zona caliente Otras exhibiciones no contiene números" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                                <asp:RequiredFieldValidator ID="rfvExhibicionesCaliente1" runat="server" 
                                                                    ControlToValidate="txtExhibicionesCaliente1" 
                                                                    ErrorMessage="Completa la información de zona caliente Otras exhibiciones" 
                                                                    ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="background-color: #FDEFE9">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td height="30">
                                                                            <asp:DropDownList ID="cmbProductoExhibicionesCaliente1A" runat="server" Height="22px" 
                                                                                style="margin-left: 0px" Width="250px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="30">
                                                                            <asp:DropDownList ID="cmbProductoExhibicionesCaliente1B" runat="server" Height="22px" 
                                                                                style="margin-left: 0px" Width="250px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="30">
                                                                            <asp:DropDownList ID="cmbProductoExhibicionesCaliente1C" runat="server" Height="22px" 
                                                                                style="margin-left: 0px" Width="250px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9; width: 125px;">
                                                                &nbsp;</td>
                                                            <td height="35" style="background-color: #FDEFE9; width: 125px;">
                                                                <asp:DropDownList ID="cmbExhibicionesCaliente2" runat="server" Height="21px" 
                                                                    style="margin-left: 0px" Width="114px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                <asp:TextBox ID="txtExhibicionesCaliente2" runat="server" MaxLength="3" 
                                                                    ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                                <asp:RangeValidator ID="rvExhibicionesCaliente2" runat="server" 
                                                                    ControlToValidate="txtExhibicionesCaliente2" Display="Dynamic" 
                                                                    ErrorMessage="El campo de zona caliente Otras exhibiciones no contiene números" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                                <asp:RequiredFieldValidator ID="rfvExhibicionesCaliente2" runat="server" 
                                                                    ControlToValidate="txtExhibicionesCaliente2" 
                                                                    ErrorMessage="Completa la información de zona caliente Otras exhibiciones" 
                                                                    ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="background-color: #FDEFE9">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td height="30">
                                                                            <asp:DropDownList ID="cmbProductoExhibicionesCaliente2A" runat="server" Height="22px" 
                                                                                style="margin-left: 0px" Width="250px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="30">
                                                                            <asp:DropDownList ID="cmbProductoExhibicionesCaliente2B" runat="server" Height="22px" 
                                                                                style="margin-left: 0px" Width="250px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="30">
                                                                            <asp:DropDownList ID="cmbProductoExhibicionesCaliente2C" runat="server" Height="22px" 
                                                                                style="margin-left: 0px" Width="250px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #FDEFE9; width: 125px;">
                                                                &nbsp;</td>
                                                            <td height="35" style="background-color: #FDEFE9; width: 125px;">
                                                                <asp:DropDownList ID="cmbExhibicionesCaliente3" runat="server" Height="21px" 
                                                                    style="margin-left: 0px" Width="114px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td height="35" style="background-color: #FDEFE9">
                                                                <asp:TextBox ID="txtExhibicionesCaliente3" runat="server" MaxLength="3" 
                                                                    ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                                <asp:RangeValidator ID="rvExhibicionesCaliente3" runat="server" 
                                                                    ControlToValidate="txtExhibicionesCaliente3" Display="Dynamic" 
                                                                    ErrorMessage="El campo de zona caliente Otras exhibiciones no contiene números" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                                <asp:RequiredFieldValidator ID="rfvExhibicionesCaliente3" runat="server" 
                                                                    ControlToValidate="txtExhibicionesCaliente3" 
                                                                    ErrorMessage="Completa la información de zona caliente Otras exhibiciones" 
                                                                    ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="background-color: #FDEFE9">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td height="30">
                                                                            <asp:DropDownList ID="cmbProductoExhibicionesCaliente3A" runat="server" Height="22px" 
                                                                                style="margin-left: 0px" Width="250px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="30">
                                                                            <asp:DropDownList ID="cmbProductoExhibicionesCaliente3B" runat="server" Height="22px" 
                                                                                style="margin-left: 0px" Width="250px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="30">
                                                                            <asp:DropDownList ID="cmbProductoExhibicionesCaliente3C" runat="server" Height="22px" 
                                                                                style="margin-left: 0px" Width="250px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td colspan="3" 
                                                    style="background-color: #00B050; text-align: center; font-weight: bold; color: #FFFFFF;">
                                                    ENTRADA O SALIDA</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; font-weight: bold; color: #000000;">
                                                    <table border="0" cellpadding="0" cellspacing="0" 
                                                        style="width: 100%; background-color: #FDEFE9; text-align: center; font-weight: bold;">
                                                        <tr>
                                                            <td style="background-color: #00DE64; height: 35px;">
                                                                Mini rack</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #E9F1F5">
                                                                <asp:TextBox ID="txtEntrada5" runat="server" MaxLength="3" 
                                                                    ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvEntrada5" runat="server" 
                                                                    ControlToValidate="txtEntrada5" 
                                                                    ErrorMessage="Completa la información de entrada o salida Mini rack" 
                                                                    ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                                <asp:RangeValidator ID="rvEntrada5" runat="server" 
                                                                    ControlToValidate="txtEntrada5" Display="Dynamic" 
                                                                    ErrorMessage="El campo entrada o salida Mini rack no contiene números" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="background-color: #00DE64">
                                                                Productos</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #E9F1F5">
                                                                <asp:DropDownList ID="cmbProductoEntrada5A" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #E9F1F5">
                                                                <asp:DropDownList ID="cmbProductoEntrada5B" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #E9F1F5">
                                                                <asp:DropDownList ID="cmbProductoEntrada5C" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #E9F1F5">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #E9F1F5">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #E9F1F5">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #E9F1F5">
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="text-align: center; font-weight: bold; color: #000000;">
                                                    <table border="0" cellpadding="0" cellspacing="0" 
                                                        style="width: 100%; background-color: #FDEFE9; text-align: center; font-weight: bold;">
                                                        <tr>
                                                            <td style="background-color: #00DE64; height: 35px;">
                                                                Wet móvil</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #E9F1F5">
                                                                <asp:TextBox ID="txtEntrada16" runat="server" MaxLength="3" 
                                                                    ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                                <asp:RangeValidator ID="rvEntrada16" runat="server" 
                                                                    ControlToValidate="txtEntrada16" Display="Dynamic" 
                                                                    ErrorMessage="El campo entrada o salida Wet móvil no contiene números" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                                <asp:RequiredFieldValidator ID="rfvEntrada16" runat="server" 
                                                                    ControlToValidate="txtEntrada16" 
                                                                    ErrorMessage="Completa la información de entrada o salida Wet móvil" 
                                                                    ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="background-color: #00DE64">
                                                                Productos</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #E9F1F5">
                                                                <asp:DropDownList ID="cmbProductoEntrada16A" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #E9F1F5">
                                                                <asp:DropDownList ID="cmbProductoEntrada16B" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #E9F1F5">
                                                                <asp:DropDownList ID="cmbProductoEntrada16C" runat="server" Height="22px" 
                                                                    style="margin-left: 0px" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #E9F1F5">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #E9F1F5">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #E9F1F5">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #E9F1F5">
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="text-align: center; font-weight: bold; color: #000000;">
                                                    <table border="0" cellpadding="0" cellspacing="0" 
                                                        
                                                        
                                                        
                                                        style="width: 100%; background-color: #FDEFE9; text-align: center; font-weight: bold; height: 330px;">
                                                        <tr>
                                                            <td style="background-color: #00DE64; height: 17px; width: 144px;">
                                                                &nbsp;</td>
                                                            <td style="background-color: #00DE64; height: 34px;">
                                                                Otras exhibiciones</td>
                                                            <td style="background-color: #00DE64; height: 34px;">
                                                                Cantidad</td>
                                                            <td style="background-color: #00DE64; height: 34px;">
                                                                Productos</td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #E9F1F5; width: 144px;">
                                                                &nbsp;</td>
                                                            <td height="35" style="background-color: #E9F1F5; width: 132px;">
                                                                <asp:DropDownList ID="cmbExhibicionesEntrada1" runat="server" Height="21px" 
                                                                    style="margin-left: 0px" Width="114px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td height="35" style="background-color: #E9EDF4">
                                                                <asp:TextBox ID="txtExhibicionesEntrada1" runat="server" MaxLength="3" 
                                                                    ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                                <asp:RangeValidator ID="rvExhibicionesEntrada1" runat="server" 
                                                                    ControlToValidate="txtExhibicionesEntrada1" Display="Dynamic" 
                                                                    ErrorMessage="El campo de entrada o salida Otras exhibiciones no contiene números" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                                <asp:RequiredFieldValidator ID="rfvExhibicionesEntrada1" runat="server" 
                                                                    ControlToValidate="txtExhibicionesEntrada1" 
                                                                    ErrorMessage="Completa la información de entrada o salida Otras exhibiciones" 
                                                                    ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="background-color: #E9EDF4">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td height="30">
                                                                            <asp:DropDownList ID="cmbProductoExhibicionesEntrada1A" runat="server" Height="22px" 
                                                                                style="margin-left: 0px" Width="250px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="30">
                                                                            <asp:DropDownList ID="cmbProductoExhibicionesEntrada1B" runat="server" Height="22px" 
                                                                                style="margin-left: 0px" Width="250px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="30">
                                                                            <asp:DropDownList ID="cmbProductoExhibicionesEntrada1C" runat="server" Height="22px" 
                                                                                style="margin-left: 0px" Width="250px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #E9EDF4; width: 144px;">
                                                                &nbsp;</td>
                                                            <td height="35" style="background-color: #E9EDF4; width: 132px;">
                                                                <asp:DropDownList ID="cmbExhibicionesEntrada2" runat="server" Height="21px" 
                                                                    style="margin-left: 0px" Width="114px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td height="35" style="background-color: #E9EDF4">
                                                                <asp:TextBox ID="txtExhibicionesEntrada2" runat="server" MaxLength="3" 
                                                                    ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                                <asp:RangeValidator ID="rvExhibicionesEntrada2" runat="server" 
                                                                    ControlToValidate="txtExhibicionesEntrada2" Display="Dynamic" 
                                                                    ErrorMessage="El campo de entrada o salida Otras exhibiciones no contiene números" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                                <asp:RequiredFieldValidator ID="rfvExhibicionesEntrada2" runat="server" 
                                                                    ControlToValidate="txtExhibicionesEntrada2" 
                                                                    ErrorMessage="Completa la información de entrada o salida Otras exhibiciones" 
                                                                    ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="background-color: #E9EDF4">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td height="30">
                                                                            <asp:DropDownList ID="cmbProductoExhibicionesEntrada2A" runat="server" Height="22px" 
                                                                                style="margin-left: 0px" Width="250px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="30">
                                                                            <asp:DropDownList ID="cmbProductoExhibicionesEntrada2B" runat="server" Height="22px" 
                                                                                style="margin-left: 0px" Width="250px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="30">
                                                                            <asp:DropDownList ID="cmbProductoExhibicionesEntrada2C" runat="server" Height="22px" 
                                                                                style="margin-left: 0px" Width="250px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="35" style="background-color: #E9EDF4; width: 144px;">
                                                                &nbsp;</td>
                                                            <td height="35" style="background-color: #E9EDF4; width: 132px;">
                                                                <asp:DropDownList ID="cmbExhibicionesEntrada3" runat="server" Height="21px" 
                                                                    style="margin-left: 0px" Width="114px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td height="35" style="background-color: #E9EDF4">
                                                                <asp:TextBox ID="txtExhibicionesEntrada3" runat="server" MaxLength="3" 
                                                                    ValidationGroup="Mars" Width="50"></asp:TextBox>
                                                                <asp:RangeValidator ID="rvExhibicionesEntrada3" runat="server" 
                                                                    ControlToValidate="txtExhibicionesEntrada3" Display="Dynamic" 
                                                                    ErrorMessage="El campo de entrada o salida Otras exhibiciones no contiene números" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Double" ValidationGroup="Mars">*</asp:RangeValidator>
                                                                <asp:RequiredFieldValidator ID="rfvExhibicionesEntrada3" runat="server" 
                                                                    ControlToValidate="txtExhibicionesEntrada3" 
                                                                    ErrorMessage="Completa la información de entrada o salida Otras exhibiciones" 
                                                                    ValidationGroup="Mars">*</asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="background-color: #E9EDF4">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td height="30">
                                                                            <asp:DropDownList ID="cmbProductoExhibicionesEntrada3A" runat="server" Height="22px" 
                                                                                style="margin-left: 0px" Width="250px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="30">
                                                                            <asp:DropDownList ID="cmbProductoExhibicionesEntrada3B" runat="server" Height="22px" 
                                                                                style="margin-left: 0px" Width="250px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="30">
                                                                            <asp:DropDownList ID="cmbProductoExhibicionesEntrada3C" runat="server" Height="22px" 
                                                                                style="margin-left: 0px" Width="250px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="Panel3" runat="server" BorderStyle="Solid" BorderWidth="1px">
                    
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="background-color: #4F81BD; text-align: center; font-weight: bold; color: #FFFFFF;">
                            IMPLEMENTACION </td>
                         <td style="background-color: #4F81BD; text-align: center; font-weight: bold; color: #FFFFFF;" 
                            colspan="2">
                             EXHIBIDORES      </td>       </tr>
                    <tr>
                        <td rowspan="3">
                            <table style="width: 100%; height: 197px;">
                                <tr>
                                    <td style="background-color: #D0D8E8; text-align: right; vertical-align: middle; height: 47px;">
                                        Marca de la implementación    
                                        <asp:RequiredFieldValidator ID="rfvImplementacion" runat="server" 
                                            ControlToValidate="lstImplementacion" 
                                            ErrorMessage="Selecciona la implementacion" 
                                            ValidationGroup="Implementacion">*</asp:RequiredFieldValidator>
                                    <td style="background-color: #D0D8E8; text-align: left; vertical-align: middle; height: 47px;">
                                        <asp:ListBox ID="lstImplementacion" runat="server" Height="101px" Width="250px" 
                                            AutoPostBack="True">
                                        </asp:ListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: #D0D8E8; text-align: right; vertical-align: middle; height: 41px;">
                                        Fecha implementación (dd/mm/yyyy):</td>
                                    <td style="background-color: #D0D8E8; text-align: left; vertical-align: middle; height: 41px;">
                                        <asp:TextBox ID="txtFechaImplementacion" runat="server" 
                                            DataFormatString="{0:dd-MM-yyyy}" style="text-align: left" 
                                            Width="96px" Enabled="False"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFechaImplementacion" runat="server" 
                                            ControlToValidate="txtFechaImplementacion" 
                                            ErrorMessage="Indica la fecha de implementacion" 
                                            ValidationGroup="Implementacion">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" 
                                        style="background-color: #D0D8E8; text-align: center; vertical-align: middle;">
                                        <b>Material POP<br />
                                        (Selecciona el material que tienes implementado)</b></td>
                                </tr>
                                <tr>
                                    <td colspan="2" 
                                        style="background-color: #D0D8E8; text-align: center; vertical-align: middle;">
                                        <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                                <asp:CheckBox ID="chkMaterialPOP1" runat="server" Text="Cenefas" 
                                                                    TextAlign="Left" Enabled="False" />
                                                            </td>
                                                <td>
                                                                <asp:CheckBox ID="chkMaterialPOP5" runat="server" Text="Mini stopper" 
                                                                    TextAlign="Left" Enabled="False" />
                                                            </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                                <asp:CheckBox ID="chkMaterialPOP2" runat="server" Text="Cenefas copete" 
                                                                    TextAlign="Left" Enabled="False" />
                                                            </td>
                                                <td>
                                                                <asp:CheckBox ID="chkMaterialPOP6" runat="server" Text="Dangler" 
                                                                    TextAlign="Left" Enabled="False" />
                                                            </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                                <asp:CheckBox ID="chkMaterialPOP3" runat="server" Text="Stopper" 
                                                                    TextAlign="Left" Enabled="False" />
                                                            </td>
                                                <td>
                                                                <asp:CheckBox ID="chkMaterialPOP7" runat="server" Text="Teatritos" 
                                                                    TextAlign="Left" Enabled="False" />
                                                            </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                                <asp:CheckBox ID="chkMaterialPOP4" runat="server" Text="Maxi stopper" 
                                                                    TextAlign="Left" Enabled="False" />
                                                            </td>
                                                <td>
                                                                <asp:CheckBox ID="chkMaterialPOP8" runat="server" Text="Charolas" 
                                                                    TextAlign="Left" Enabled="False" />
                                                            </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    Otros materiales :                                                     
                                                    <asp:TextBox ID="txtOtros" runat="server" style="text-align: left" 
                                                            Width="150px" Enabled="False"></asp:TextBox>
                                                                <asp:CheckBox ID="chkMaterialPOP9" runat="server" 
                                                                    TextAlign="Left" Enabled="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                        <asp:ValidationSummary runat="server" ID="vsImplementacion" ValidationGroup="Implementacion" HeaderText="Por favor corrige los datos de implementacion" 
                                                            ShowMessageBox="True" ShowSummary="False" />
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                        <asp:Button ID="btnGuardarImplementacion" runat="server" Text="Guardar" 
                                            ValidationGroup="Implementacion" CssClass="button" Enabled="False" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                </table>
                        </td>
                        <td colspan="2" style="background-color: #E9EDF4; text-align: center;">
                            <b>Selecciona la exhibición que tengas implementada</b></td>
                    </tr>
                    <tr>
                        <td style="background-color: #E9EDF4; text-align: center;">
                                                                <asp:CheckBox ID="chkExhibidores13" 
                                runat="server" Text="PDQ" 
                                                                    TextAlign="Left" />
                        </td>
                        <td style="background-color: #E9EDF4; text-align: center;">
                                                                <asp:CheckBox ID="chkExhibidores17" 
                                runat="server" Text="Floor display" 
                                                                    TextAlign="Left" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="width: 100%; background-color: #E9EDF4; text-align: left; vertical-align: bottom; height: 197px;">
                                <tr>
                         <td style="background-color: #4F81BD; text-align: center; font-weight: bold; color: #FFFFFF;" 
                                        colspan="2">
                            COMENTARIOS      </td>       
                                </tr>
                                <tr>
                                    <td>
                                        Tipo comentarios:</td>
                                    <td style="vertical-align: top">
                                        <asp:DropDownList ID="cmbComentarios" runat="server" Width="175px" 
                                            Height="21px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top">
                                        Comentario:</td>
                                    <td style="vertical-align: top">
                                 
                                   <asp:TextBox ID="txtComentarios" runat="server" Width="231px" Height="130px" 
                                       TextMode="MultiLine" MaxLength="300"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
            </table>
            </asp:Panel>
                    <br />
                </ContentTemplate> 
            </asp:UpdatePanel> 
                    
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="width: 100%">
                       <tr>
                           <td style="text-align: center; height: 31px; " colspan="2">
                           
                               <asp:ValidationSummary ID="vsMars" runat="server" 
                                   ValidationGroup="Mars" HeaderText="Por favor corrige los siguientes datos" 
                                   ShowMessageBox="True" ShowSummary="False" />
                                <asp:UpdateProgress ID="UpdateProgress" runat="server" 
                                   AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
                                   <ProgressTemplate>
                                      <div id="pnlSave" >
                                           <img alt="" src="../../../../Img/loading.gif" />La información se esta guardando<br />
                                               Por favor espera...<br />
                                               <br />
                                               <br />
                                        </div>
                                   </ProgressTemplate>
                               </asp:UpdateProgress>
                           </td>
                       </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" 
                                    ValidationGroup="Mars" CssClass="button" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                                    Text="Cancelar" CssClass="button" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate> 
            </asp:UpdatePanel> 
            </div>
        
        <!--CONTENT SIDE COLUMN-->
        <div id="content-side-two-column">
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>
