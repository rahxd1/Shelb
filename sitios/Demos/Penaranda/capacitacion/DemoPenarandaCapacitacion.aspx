<%@ Page Language="vb" 
AutoEventWireup="false" 
MasterPagefile="~/sitios/Demos/Penaranda/DemoPenaranda.Master"
CodeBehind="DemoPenarandaCapacitacion.aspx.vb" 
Inherits="procomlcd.DemoPenarandaCapacitacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DemoPenarandaContent" runat="Server">
     
    <!--POSTER PHOTO-->
    <div id="poster-photo-container">
        <div id="poster-photo-image">
        </div>
    </div>
    
    <div id="contenido-columna">
    <h1 style="color:#004020">Capacitación</h1>
    <br />
        <table style="width: 100%; vertical-align: bottom;" align="center">
            <tr>
                <td style="width: 245px; text-align: center;" valign="bottom">
                    <a href="DemoPenarandaSesiones.aspx">
                    <img src="../Img/cap_linea.png" alt="Join.me" 
                        style="border: medium solid #C0C0C0" /></a>
                    </td>
                <td style="width: 245px; text-align: center;">
                        <a href="DemoPenarandaManuales.aspx">
                    <img src="../Img/manuales.png" alt="Manuales" 
                        style="border: medium solid #C0C0C0"/></a>
                    </td>
                <td style="width: 245px; text-align: center;">
                    <a href="dinamicas/DemoPenarandaDinamicaInicio.aspx">
                    <img src="../Img/dinamicas.jpg" alt="Certificación" 
                        style="border: medium solid #C0C0C0"/></a>
                    </td>
               <td style="width: 245px ; text-align: center;">
                    <a href="DemoPenarandaMultimedia.aspx">
                    <img src="../Img/logo100min2.png" alt="Cuestionarios" 
                        style="border: medium solid #C0C0C0"/></a>
                    </td>
            </tr>
            <tr>
                <td style="width: 245px; text-align: center; height: 30px;" valign="bottom">
                    <h2><a href="DemoPenarandaSesiones.aspx">Virtual/Join.me</a></h2>
                </td>
                <td style="width: 235px; height: 30px;">
                    <h2><a href="DemoPenarandaManuales.aspx">Manuales</a></h2>
                </td>
                <td style="width: 226px; text-align: center; height: 30px;">
                    <h2><a href="dinamicas/DemoPenarandaDinamicaInicio.aspx">Dinámicas</a></h2>
                </td>
                <td style="width: 226px; text-align: center; height: 30px;">
                    <h2><a href="DemoPenarandaMultimedia.aspx">Multimedia</a></h2>
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
