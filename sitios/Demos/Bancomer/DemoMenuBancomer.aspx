<%@ Page Language="vb"
 MasterPageFile="~/sitios/Demos/Bancomer/DemoBancomer.Master"
 AutoEventWireup="false" 
 CodeBehind="DemoMenuBancomer.aspx.vb" 
 Inherits="procomlcd.DemoMenuBancomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DemoBancomerContent" runat="Server">
     
    <!--POSTER PHOTO-->
    <div id="poster-photo-container">
        <div id="poster-photo-image">
        
        </div>
    </div>
    
    <div id="contenido-columna">
        <table style="width: 100%; vertical-align: bottom;" align="center">
            <tr>
                <td style="width: 245px; text-align: center;" valign="bottom">
                    <a href="personal/DemoBancomerPersonal.aspx">
                    <img src="Img/personal.jpg" alt="Join.me" 
                        style="border: medium solid #C0C0C0" /></a>
                    </td>
                <td style="width: 235px; text-align: center;">
                        <a href="consultas/DemoBancomerConsultas.aspx">
                    <img src="Img/consultas.jpg" alt="Manuales" 
                        style="border: medium solid #C0C0C0"/></a>
                    </td>
                <td style="width: 226px; text-align: center;">
                    <a href="DemoBancomerDocs.aspx">
                    <img src="Img/documentos.jpg" alt="Certificación" 
                        style="border: medium solid #C0C0C0"/></a>
                    </td>
              
            </tr>
            <tr>
                <td style="width: 245px; text-align: center; height: 30px;" valign="bottom">
                    <h2><a href="personal/DemoBancomerPersonal.aspx">Personal</a></h2>
                </td>
                <td style="width: 235px; height: 30px;">
                    <h2><a href="consultas/DemoBancomerConsultas.aspx">Consultas</a></h2>
                </td>
                <td style="width: 226px; text-align: center; height: 30px;">
                    <h2><a href="DemoBancomerDocs.aspx">Documentos</a></h2>
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