<%@ Page Language="vb" 
MasterPageFile="~/sitios/Demos/Penaranda/DemoPenaranda.Master"
AutoEventWireup="false" 
CodeBehind="DemoPenarandaMenu.aspx.vb" 
Inherits="procomlcd.DemoPenarandaMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DemoPenarandaContent" runat="Server">
     
    <!--POSTER PHOTO-->
    <div id="poster-photo-container">
        <div id="poster-photo-image">
        
        </div>
    </div>
    
    <div id="contenido-columna">
        <table style="width: 100%; vertical-align: bottom;" align="center">
            <tr>
                <td style="width: 245px; text-align: center;" valign="bottom">
                    <a href="rh/DemoPenarandaRH.aspx">
                    <img src="Img/personal.jpg" alt="Join.me" 
                        style="border: medium solid #C0C0C0" /></a>
                    </td>
                <td style="width: 235px; text-align: center;">
                        <a href="capacitacion/DemoPenarandaCapacitacion.aspx">
                    <img src="Img/consultas.jpg" alt="Manuales" 
                        style="border: medium solid #C0C0C0"/></a>
                    </td>
                <td style="width: 226px; text-align: center;">
                    <a href="certificacion/DemoPenaraldaCertificacion.aspx">
                    <img src="Img/certificacion.jpg" alt="Certificación" 
                        style="border: medium solid #C0C0C0"/></a>
                    </td>
              
            </tr>
            <tr>
                <td style="width: 245px; text-align: center; height: 30px;" valign="bottom">
                    <h2><a href="rh/DemoPenarandaRH.aspx">Recursos Humanos</a></h2>
                </td>
                <td style="width: 235px; height: 30px;">
                    <h2><a href="capacitacion/DemoPenarandaCapacitacion.aspx">Capacitación</a></h2>
                </td>
                <td style="width: 226px; text-align: center; height: 30px;">
                    <h2><a href="certificacion/DemoPenaraldaCertificacion.aspx">Certificación</a></h2>
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
        <br />
        
        <div class="clear"></div>
        
    </div>
</asp:Content>

