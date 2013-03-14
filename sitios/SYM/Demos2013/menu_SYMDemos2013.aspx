<%@ Page Language="vb" 
MASTERPAGEFILE="~/sitios/SYM/Demos2013/SYMDemos2013.Master"
AutoEventWireup="false" 
CodeBehind="menu_SYMDemos2013.aspx.vb" 
Inherits="procomlcd.menu_SYMDemos2013" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Contenido_SYMDemos2013" runat="Server">
  
     
    <!--POSTER PHOTO-->
    <div id="poster-photo-container">
        <div id="poster-photo-image">
        
        </div>
    </div>
    
    <div id="contenido-columna">
        <table style="width: 100%; vertical-align: bottom;" align="center">
            <tr>
                <td style="width: 150px; text-align: center;" valign="bottom">
                    <a href="Captura/RutasSYMDemos2013.aspx">
                    <img src="Img/captura.jpg" alt="Capturas" 
                        style="border: medium solid #C0C0C0" /></a>
                    </td>
                <td style="width: 150px; text-align: center;">
                        <a href="Reportes/ReportesSYMDemos2013.aspx">
                    <img src="Img/reportes.jpg" alt="Reportes" 
                        style="border: medium solid #C0C0C0"/></a>
                    </td>
                <td style="width: 150px; text-align: center;">
                    <a href="Docs/DocsSYMDemos2013.aspx">
                    <img src="Img/documentos.jpg" alt="Docs" 
                        style="border: medium solid #C0C0C0"/></a>
                    </td>
              
            </tr>
            <tr>
                <td style="width: 245px; text-align: center; height: 30px;" valign="bottom">
                    <h2><a href="Captura/RutasSYMDemos2013.aspx">Captura</a></h2>
                </td>
                <td style="width: 235px; height: 30px;">
                    <h2><a href="Reportes/ReportesSYMDemos2013.aspx">Reportes</a></h2>
                </td>
                <td style="width: 226px; text-align: center; height: 30px;">
                    <h2><a href="Docs/DocsSYMDemos2013.aspx">Documentos</a></h2>
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

