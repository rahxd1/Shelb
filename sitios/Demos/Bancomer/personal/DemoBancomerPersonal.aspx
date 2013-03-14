<%@ Page Language="vb"
   MasterPageFile="~/sitios/Demos/Bancomer/DemoBancomer.Master"
  AutoEventWireup="false"
  CodeBehind="DemoBancomerPersonal.aspx.vb" 
  Inherits="procomlcd.DemoBancomerPersonal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DemoBancomerContent" runat="Server">
     
    <!--POSTER PHOTO-->
    
    
    <div id="contenido-columna">
        <table style="width: 100%; vertical-align: bottom;" align="center">
            <tr>
                <td style="width: 245px; text-align: center;" valign="bottom">
                    <a href="DemoBancomerCaptura.aspx">
                    <img src="../Img/captura.jpg" alt="Join.me" 
                        style="border: medium solid #C0C0C0" /></a>
                    </td>
                <td style="width: 235px; text-align: center;">
                        <a href="Manuales/ManualesMars.aspx">
                    <img src="../Img/modificar.jpg" alt="Manuales" 
                        style="border: medium solid #C0C0C0"/></a>
                    </td>
                <td style="width: 226px; text-align: center;">
                    &nbsp;</td>
              
            </tr>
            <tr>
                <td style="width: 245px; text-align: center; height: 30px;" valign="bottom">
                    <h2><a href="DemoBancomerCaptura.aspx">Captura</a></h2>
                </td>
                <td style="width: 235px; height: 30px;">
                    <h2><a href="Manuales/ManualesMars.aspx">Modificar</a></h2>
                </td>
                <td style="width: 226px; text-align: center; height: 30px;">
                    <h2>&nbsp;</h2>
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
