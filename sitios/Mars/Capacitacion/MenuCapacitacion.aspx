<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion_Mars.Master"
    AutoEventWireup="false" 
    CodeBehind="MenuCapacitacion.aspx.vb" 
    Inherits="procomlcd.MenuCapacitacion"
    title="Mars Capacitacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsCapacitacion" runat="Server">
     
    <!--POSTER PHOTO-->
    <div id="poster-photo-container">
        <div id="poster-photo-image">
        </div>
    </div>
    
    <div id="contenido-columna">
        <table style="width: 100%; vertical-align: bottom;" align="center">
            <tr>
                <td style="width: 245px; text-align: center;" valign="bottom">
                    <a href="Sesiones/joinme.aspx">
                    <img src="Img/cap_linea.png" alt="Join.me" 
                        style="border: medium solid #C0C0C0" /></a>
                    </td>
                <td style="width: 235px; text-align: center;">
                        <a href="Manuales/ManualesMars.aspx">
                    <img src="Img/manuales.png" alt="Manuales" 
                        style="border: medium solid #C0C0C0"/></a>
                    </td>
                <td style="width: 226px; text-align: center;">
                    <a href="Certificacion/certificacion.aspx">
                    <img src="Img/logo100min.png" alt="Certificación" 
                        style="border: medium solid #C0C0C0"/></a>
                    </td>
                <td style="width: 226px; text-align: center;">
                    <a href="Cuestionarios/CuestionariosMars.aspx">
                    <img src="Img/cuestionario.jpg" alt="Cuestionarios" 
                        style="border: medium solid #C0C0C0"/></a>
                    </td>
            </tr>
            <tr>
                <td style="width: 245px; text-align: center; height: 30px;" valign="bottom">
                    <h2><a href="Sesiones/joinme.aspx">Join.me</a></h2>
                </td>
                <td style="width: 235px; height: 30px;">
                    <h2><a href="Manuales/ManualesMars.aspx">Manuales</a></h2>
                </td>
                <td style="width: 226px; text-align: center; height: 30px;">
                    <h2><a href="Certificacion/certificacion.aspx">Certificación</a></h2>
                </td>
                <td style="width: 226px; text-align: center; height: 30px;">
                    <h2><a href="Cuestionarios/CuestionariosMars.aspx">Cuestionarios</a></h2>
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