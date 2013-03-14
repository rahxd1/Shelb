<%@ Page Language="vb"
masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion_Mars.Master"
AutoEventWireup="false" 
CodeBehind="evaluar.aspx.vb" 
Inherits="procomlcd.evaluar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsCapacitacion" runat="Server">
   <!--

POSTER PHOTO

-->
    <div id="poster-photo-container">
    
    <div id="poster-photo-image"></div>
         
          </div>
    <!--

CONTENT CONTAINER

-->
    <div id="contenido-columna-derecha">
        <!--

CONTENT MAIN COLUMN

-->
        <div id="content-main-two-column">

            <table style="width: 100%">
                <tr>
                    <td colspan="4" 
                        style="text-align: center; color: #FFFFFF; background-color: #FFFFFF">
                        
                        &nbsp;</td>
                    <td colspan="4" 
                        style="text-align: right; color: #FFFFFF; background-color: #FFFFFF">
                        
                                <asp:HyperLink ID="lnkRegresar" runat="server" 
                                    
                            NavigateUrl="~/sitios/Mars/Capacitacion/Dinamicas/Autoservicio/PuntosInterrupcion/menu_logros.aspx">Regresar</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" 
                        style="text-align: center; color: #FFFFFF; background-color: #FFFFFF">
                        
                        <asp:Image ID="imgFormato" runat="server" />
                    </td>
                    <td colspan="4" 
                        style="text-align: center; color: #FFFFFF; background-color: #FFFFFF">
                        
                        <asp:Image ID="imgTipo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="8" 
                        
                        
                        
                        
                        style="text-align: center; color: #336699; background-color: #FFFFFF; height: 19px;">
                        
                        </td>
                </tr>
                <tr>
                    <td colspan="8" 
                        
                        
                        
                        style="text-align: center; color: #336699; background-color: #FFFFFF; height: 19px;">
                        
                        |Juego de conocimiento | deberás de anotar la cantidad de exhibiciones que se te 
                        pide</td>
                </tr>
                <tr>
                    <td style="text-align: center; background-color: #FFFFFF; color: #336699; height: 19px;" 
                        colspan="8">
                        en la pa<span style="background-color: #FFFFFF">uta de
                        </span>
                        <span style="background-color: #336699">
                        <span style="background-color: #FFFFFF">
                        <asp:Label ID="lblFORMATO" runat="server" 
                            style="color: #336699; font-weight: 700; background-color: #FFFFFF" 
                            ForeColor="#003366"></asp:Label>
                        </span><span style="color: #336699; background-color: #FFFFFF">&nbsp;y tipo de tienda 
                        </span> </span><span style="background-color: #FFFFFF">
                        <span style="background-color: #336699">
                        <asp:Label ID="lblTIPO" runat="server" 
                            style="color: #336699; font-weight: 700; background-color: #FFFFFF;" 
                            ForeColor="#003366"></asp:Label>
                        </span></span></td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF; color: #336699; height: 34px; text-align: center;" 
                        colspan="8">
                        Por cada una de las zonas, al terminar darás clic en el botón evaluar para saber 
                        si esta correcta.</td>
                </tr>
                <tr>
                    <td style="text-align: center; background-color: #FFFF00; height: 20px;" 
                        colspan="8">
                        AVISO: Si al dar clic en el boton EVALUAR, los cuadros cambian de color a rojo 
                        significa que la cantidad</td>
                </tr>
                <tr>
                    <td style="text-align: center; background-color: #FFFF00; height: 20px;" 
                        colspan="8">
                        esta equivocada, si tus cuadros cambian a verde la cantidad estará correcta.</td>
                </tr>
                <tr>
                    <td style="text-align: center; background-color: #006699; height: 20px;" 
                        colspan="8">
                        <span style="background-color: #336699">
                        <span style="background-color: #FFFFFF">
                        <asp:Label ID="lblAvisoGral" runat="server" 
                            style="color: #FF0000; font-weight: 700; background-color: #336699" 
                            Font-Size="Medium"></asp:Label>
                        </span> </span>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F79646; height: 18px; text-align: center;" 
                        colspan="8">
                        <b>ZONA ANAQUEL</b></td>
                </tr>
                <tr>
                    <td style="height: 51px; background-color: #F79646; text-align: center;">
                        <b>POUCHERO<br />
                        </b>
                        <asp:TextBox ID="txtAnaquelPouchero" runat="server" style="text-align: center" 
                            Width="80px" Font-Bold="True" ForeColor="#003366"  v  > 
                             
                            
                            </asp:TextBox>
                        &nbsp;<br />
                        <asp:RangeValidator ID="RVAnaquelPouchero" runat="server" 
                            ControlToValidate="txtAnaquelPouchero" MaximumValue="9" 
                            MinimumValue="0" style="font-size: x-small">Solo Numeros</asp:RangeValidator>
                        &nbsp;<asp:RequiredFieldValidator ID="RFVAnaquelPouchero" runat="server" 
                            ControlToValidate="txtAnaquelPouchero" style="font-size: x-small">Introduce un valor</asp:RequiredFieldValidator>
                    </td>
                    <td style="height: 51px; background-color: #F79646; text-align: center;" 
                        colspan="3">
                        <b>LATERO<br />
                        </b>
                        <asp:TextBox ID="txtAnaquelLatero" runat="server" style="text-align: center" 
                            Width="80px" Font-Bold="True" ForeColor="#003366"></asp:TextBox>
                        &nbsp;<br />
                        <asp:RangeValidator ID="RVAnaquelLatero" runat="server" 
                            ControlToValidate="txtAnaquelLatero" MaximumValue="9" 
                            MinimumValue="0" style="font-size: x-small">Solo Numeros</asp:RangeValidator>
                        &nbsp;<asp:RequiredFieldValidator ID="RFVAnaquel" runat="server" 
                            ControlToValidate="txtAnaquelLatero" style="font-size: x-small">Introduce un valor</asp:RequiredFieldValidator>
                    </td>
                    <td style="height: 51px; background-color: #F79646; text-align: center;" 
                        colspan="3">
                        <b>TIRA<br />
                        </b>
                        <asp:TextBox ID="txtAnaquelTira" runat="server" Font-Bold="True" 
                            Font-Italic="False" style="text-align: center" Width="80px" 
                            ForeColor="#003366"></asp:TextBox>
                        &nbsp;<br />
                        <asp:RangeValidator ID="RVAnaquelLatero0" runat="server" 
                            ControlToValidate="txtAnaquelTira" MaximumValue="9" 
                            MinimumValue="0" style="font-size: x-small">Solo Numeros</asp:RangeValidator>
                        &nbsp;<asp:RequiredFieldValidator ID="RFVAnaquel0" runat="server" 
                            ControlToValidate="txtAnaquelTira" style="font-size: x-small">Introduce un valor</asp:RequiredFieldValidator>
                    </td>
                    <td style="height: 51px; background-color: #F79646; text-align: center;">
                        <b>BALCON<br />
                        </b>
                        <asp:TextBox ID="txtAnaquelBalcon" runat="server" style="text-align: center" 
                            Width="80px" Font-Bold="True" ForeColor="#003366"></asp:TextBox>
                        &nbsp;<br />
                        <asp:RangeValidator ID="RVAnaquelLatero1" runat="server" 
                            ControlToValidate="txtAnaquelBalcon" MaximumValue="9" 
                            MinimumValue="0" style="font-size: x-small">Solo numeros</asp:RangeValidator>
                        &nbsp;<asp:RequiredFieldValidator ID="RFVAnaquel1" runat="server" 
                            ControlToValidate="txtAnaquelBalcon" style="font-size: x-small">Introduce un valor</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="height: 19px; background-color: #F79646; text-align: center;" 
                        colspan="8">
                        <span style="background-color: #336699">
                        <span style="background-color: #FFFFFF">
                        <asp:Label ID="lblAvisoAnaquel" runat="server" 
                            style="color: #FF0000; font-weight: 700; background-color: #F79646"></asp:Label>
                        </span> </span>
                        </td>
                </tr>
                <tr>
                    <td style="height: 20px; background-color: #006699" colspan="8">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="height: 19px; background-color: #4F81BD; text-align: center;" 
                        colspan="8">
                        <b>ZONA PASILLO MASCOTA</b></td>
                </tr>
                <tr>
                    <td style="height: 51px; background-color: #4F81BD; text-align: center;" 
                        colspan="3">
                        <b>CABECERA<br />
                        </b>
                        <asp:TextBox ID="txtMascotaCabecera" runat="server" style="text-align: center" 
                            Width="80px" Font-Bold="True" ForeColor="#003366"></asp:TextBox>
                        &nbsp;<br />
                        <asp:RangeValidator ID="RVMascota" runat="server" 
                            ControlToValidate="txtMascotaCabecera" MaximumValue="9" 
                            MinimumValue="0" style="font-size: x-small">Solo numeros</asp:RangeValidator>
                        &nbsp;<asp:RequiredFieldValidator ID="RFVMascota" runat="server" 
                            ControlToValidate="txtMascotaCabecera" style="font-size: x-small">Introduce un valor</asp:RequiredFieldValidator>
                    </td>
                    <td style="height: 51px; background-color: #4F81BD; text-align: center;" 
                        colspan="2">
                        <b>ISLA<br />
                        </b>
                        <asp:TextBox ID="txtMascotaIsla" runat="server" style="text-align: center" 
                            Width="80px" Font-Bold="True" ForeColor="#003366"></asp:TextBox>
                        &nbsp;<br />
                        <asp:RangeValidator ID="RVMascota0" runat="server" 
                            ControlToValidate="txtMascotaIsla" MaximumValue="9" 
                            MinimumValue="0" style="font-size: x-small">Solo numeros</asp:RangeValidator>
                        &nbsp;<asp:RequiredFieldValidator ID="RFVMascota0" runat="server" 
                            ControlToValidate="txtMascotaIsla" style="font-size: x-small">Introduce un valor</asp:RequiredFieldValidator>
                    </td>
                    <td style="height: 51px; background-color: #4F81BD; text-align: center;" 
                        colspan="3">
                        <b>BOTADERO<br />
                        </b>
                        <asp:TextBox ID="txtMascotaBotadero" runat="server" style="text-align: center" 
                            Width="80px" Font-Bold="True" ForeColor="#003366"></asp:TextBox>
                        &nbsp;<br />
                        <asp:RangeValidator ID="RVMascota1" runat="server" 
                            ControlToValidate="txtMascotaBotadero" MaximumValue="9" 
                            MinimumValue="0" style="font-size: x-small">Solo numeros</asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="RFVMascota1" runat="server" 
                            ControlToValidate="txtMascotaBotadero" style="font-size: x-small">Introduce un valor</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #4F81BD; height: 26px; text-align: center;" 
                        colspan="8">
                        <span style="background-color: #336699">
                        <span style="background-color: #FFFFFF">
                        <asp:Label ID="lblAvisoMascota" runat="server" 
                            style="color: #FF0000; font-weight: 700; background-color: #4F81BD"></asp:Label>
                        </span> </span>
                        </td>
                </tr>
                <tr>
                    <td style="height: 20px; background-color: #006699" colspan="8">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="height: 19px; background-color: #FF0000; font-weight: 700; text-align: center;" 
                        colspan="8">
                        ZONA CALIENTE</td>
                </tr>
                <tr>
                    <td style="height: 49px; background-color: #FF9797; font-weight: 700; text-align: center;" 
                        colspan="2">
                        ISLA<br />
                        <asp:TextBox ID="txtCalienteIsla" runat="server" style="text-align: center" 
                            Width="80px" Font-Bold="True" ForeColor="#003366"></asp:TextBox>
                        &nbsp;<br />
                        <asp:RangeValidator ID="RVCaliente" runat="server" 
                            ControlToValidate="txtCalienteIsla" Font-Bold="False" 
                            MaximumValue="9" MinimumValue="0" style="font-size: x-small">Solo numeros</asp:RangeValidator>
                        &nbsp;<asp:RequiredFieldValidator ID="RFVCaliente" runat="server" 
                            ControlToValidate="txtCalienteIsla" 
                            Font-Bold="False" style="font-size: x-small">Introduce un valor</asp:RequiredFieldValidator>
                        <br />
                    </td>
                    <td style="background-color: #FF9797; font-weight: 700; height: 49px; text-align: center;" 
                        colspan="2">
                        BOTADEROS<br />
                        <asp:TextBox ID="txtCalienteBotadero" runat="server" style="text-align: center" 
                            Width="80px" Font-Bold="True" ForeColor="#003366"></asp:TextBox>
                        <br />
                        <asp:RangeValidator ID="RVCaliente0" runat="server" 
                            ControlToValidate="txtCalienteBotadero" Font-Bold="False" 
                            MaximumValue="9" MinimumValue="0" style="font-size: x-small">Solo numeros</asp:RangeValidator>
                        &nbsp;<asp:RequiredFieldValidator ID="RFVCaliente0" runat="server" 
                            ControlToValidate="txtCalienteBotadero" 
                            Font-Bold="False" style="font-size: x-small">Introduce un valor</asp:RequiredFieldValidator>
                    </td>
                    <td style="background-color: #FF9797; font-weight: 700; height: 49px; text-align: center;" 
                        colspan="2">
                        MIX FEEDING<br />
                        <asp:TextBox ID="txtCalienteMixfeeding" runat="server" style="text-align: center" 
                            Width="80px" Font-Bold="True" ForeColor="#003366"></asp:TextBox>
                        <br />
                        <asp:RangeValidator ID="RVCaliente1" runat="server" 
                            ControlToValidate="txtCalienteMixfeeding" Font-Bold="False" 
                            MaximumValue="9" MinimumValue="0" style="font-size: x-small">Solo numeros</asp:RangeValidator>
                        &nbsp;<asp:RequiredFieldValidator ID="RFVCaliente1" runat="server" 
                            ControlToValidate="txtCalienteMixfeeding" 
                            Font-Bold="False" style="font-size: x-small">Introduce un valor</asp:RequiredFieldValidator>
                    </td>
                    <td style="background-color: #FF9797; font-weight: 700; height: 49px; text-align: center;" 
                        colspan="2">
                        MINI RACK<br />
                        <asp:TextBox ID="txtCalienteMiniRack" runat="server" style="text-align: center" 
                            Width="80px" Font-Bold="True" ForeColor="#003366"></asp:TextBox>
                        &nbsp;<br />
                        <asp:RangeValidator ID="RVCaliente2" runat="server" 
                            ControlToValidate="txtCalienteMiniRack" Font-Bold="False" 
                            MaximumValue="9" MinimumValue="0" style="font-size: x-small">Solo numeros</asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="RFVCaliente2" runat="server" 
                            ControlToValidate="txtCalienteMiniRack" 
                            Font-Bold="False" style="font-size: x-small">Introduce un valor</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="height: 19px; background-color: #FF9797; font-weight: 700; text-align: center;" 
                        colspan="8">
                        <span style="background-color: #336699">
                        <span style="background-color: #FFFFFF">
                        <asp:Label ID="lblAvisoCaliente" runat="server" 
                            style="color: #FF0000; font-weight: 700; background-color: #FFFFFF"></asp:Label>
                        </span> </span></td>
                </tr>
                <tr>
                    <td style="height: 19px; background-color: #006699; font-weight: 700;" 
                        colspan="8">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="height: 19px; background-color: #00B050; font-weight: 700; text-align: center;" 
                        colspan="8">
                        ZONA ENTRADA / SALIDA</td>
                </tr>
                <tr>
                    <td style="height: 51px; background-color: #00DE64; font-weight: 700; text-align: center;" 
                        colspan="4">
                        MINI RACK<br />
                        <asp:TextBox ID="txtESMinirack" runat="server" style="text-align: center" 
                            Width="80px" Font-Bold="True" ForeColor="#003366"></asp:TextBox>
                        <br />
                        &nbsp;<asp:RangeValidator ID="RVES" runat="server" 
                            ControlToValidate="txtESMinirack" Font-Bold="False" 
                            MaximumValue="9" MinimumValue="0" style="font-size: x-small">Solo numeros</asp:RangeValidator>
                        &nbsp;<asp:RequiredFieldValidator ID="RFVES" runat="server" 
                            ControlToValidate="txtESMinirack" 
                            Font-Bold="False" style="font-size: x-small">Introduce un valor</asp:RequiredFieldValidator>
                        <br />
                    </td>
                    <td style="height: 51px; background-color: #00DE64; font-weight: 700; text-align: center;" 
                        colspan="4">
                        WET MOVIL<br />
                        <asp:TextBox ID="txtESWetMovil" runat="server" style="text-align: center" 
                            Width="80px" Font-Bold="True" ForeColor="#003366"></asp:TextBox>
                        &nbsp;<br />
                        <asp:RangeValidator ID="RVES0" runat="server" ControlToValidate="txtESWetMovil" Font-Bold="False" 
                            MaximumValue="9" MinimumValue="0" style="font-size: x-small">Solo numeros</asp:RangeValidator>
                        &nbsp;<asp:RequiredFieldValidator ID="RFVES0" runat="server" 
                            ControlToValidate="txtESWetMovil" 
                            Font-Bold="False" style="font-size: x-small">Introduce un valor</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="height: 19px; background-color: #00B050; font-weight: 700; text-align: center;" 
                        colspan="8">
                        <span style="background-color: #336699">
                        <span style="background-color: #FFFFFF">
                        <asp:Label ID="lblAvisoES" runat="server" 
                            style="color: #FF0000; font-weight: 700; background-color: #00B050"></asp:Label>
                        </span> </span></td>
                </tr>
                <tr>
                    <td style="height: 19px; background-color: #FFFF00; font-weight: 700; text-align: center;" 
                        colspan="8">
                        AVISOS DE ERROR:</td>
                </tr>
                <tr>
                    <td style="height: 19px; background-color: #FFFF00; font-weight: 700; text-align: center;" 
                        colspan="8">
                        Si en tu evaluación aparece un *, significa que debes de anotar solo números, si 
                        aparece un +, significa que tienes que anotar un valor.</td>
                </tr>
                <tr>
                    <td style="height: 22px; background-color: #FFFFFF; font-weight: 700; text-align: center;" 
                        colspan="8">
                        <asp:Button ID="btnEvaluar" runat="server" Text="Evaluar" />
                    </td>
                </tr>
            </table>

        </div>
        <!--

CONTENT SIDE COLUMN AVISOS

-->
        <div id="content-side-two-column">
            
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center; color: #FF0066;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center; color: #009933; height: 19px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center; height: 19px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <object classid="perro" 
                             codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0" 
                             title="MARS" style="width: 150px; height: 150px">
                             <param name="movie" value="imagenes/perroexh.swf" />
                             <param name="quality" value="high" />
                             <param name="wmode" value="opaque" />
                             <embed src="imagenes/Perroexh.swf" quality="high" wmode="opaque" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash" type="application/x-shockwave-flash" width="150" height="150"></embed>
                          </object>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
            </table>
        
        </div>
        
        <div class="clear">
        
       
            <br />
            <br />
        
        </div>
        
    </div>
</asp:Content>
