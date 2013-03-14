<%@ Page Language="vb"
Masterpagefile="~/sitios/Demos/Penaranda/DemoPenaranda.Master"
 AutoEventWireup="false" 
 CodeBehind="DemoPenaraldaCertificacion.aspx.vb"
  Inherits="procomlcd.DemoPenaraldaCertificacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DemoPenarandaContent" runat="Server">
    
 
<!--POSTER PHOTO-->
    <div id="poster-photo-container">
         <div id="poster-photo-image"></div>
    </div>
    
    <!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
    <!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">

            <table style="width: 100%">
                <tr>
                    <td style="height: 19px; text-align: right;">
                        <img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="HyperLink7" runat="server" 
                            
                            NavigateUrl="~/sitios/Demos/Penaranda/DemoPenarandaMenu.aspx">Regresar</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td style="height: 19px">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; color: #FFFFFF; height: 19px;" class="style1">
                        Certificaciones Activas</td>
                </tr>
                <tr>
                    <td style="text-align: left">
                         
                         <asp:HyperLink ID="HyperLink8" runat="server" 
                             NavigateUrl="~/sitios/Demos/Penaranda/certificacion/DemoPenaraldaCertificacionExamen.aspx">Demo Certificacion </asp:HyperLink>

                         

                    </td>
                </tr>
                <tr>
                    <td style="background-color: #004020">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <!--

CONTENT SIDE COLUMN AVISOS

-->
        <div id="content-side-two-column">
            <h3>
                &nbsp;</h3>
            <p style="text-align: center; color: #CC0000;">
                    &nbsp;</p>
                    <br />
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center">
                        &quot;Da clic en Acceder para </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                                            Iniciar el examen de</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        Certificacion&quot;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        
        <div class="clear" style="color: #FFFFFF">
            .<br />
      
        
        </div>
        
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .style1
        {
            background-color: #004020;
        }
    </style>

</asp:Content>

