<%@ Page Language="vb"
MasterPageFile="~/sitios/Demos/Penaranda/DemoPenaranda.Master"
AutoEventWireup="false"
CodeBehind="DemoPenarandaRH.aspx.vb" 
Inherits="procomlcd.DemoPenarandaRH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DemoPenarandaContent" runat="Server">
     
    <!--POSTER PHOTO-->
    
    
    <div id="contenido-columna">
    <h1 style="color:#004020">Recursos Humanos</h1>
    <br />
        <table style="width: 100%; vertical-align: bottom;" align="center">
            <tr>
                <td style="width: 245px; text-align: center;" valign="bottom">
                    <a href="DemoPenarandaRHReclutamiento.aspx">
                    <img src="../Img/reclutamiento.jpg" alt="Join.me" 
                        style="border: medium solid #C0C0C0" /></a>
                    </td>
                <td style="width: 235px; text-align: center;">
                        <a href="DemoPenarandaAltas.aspx">
                    <img src="../Img/seleccion.jpg" alt="Manuales" r
                        style="border: medium solid #C0C0C0"/></a>
                    </td>
                <td style="width: 226px; text-align: center;">
                    &nbsp;</td>
              
            </tr>
            <tr>
                <td style="width: 245px; text-align: center; height: 30px;" valign="bottom">
                    <h2><a href="DemoPenarandaRHReclutamiento.aspx">Reclutamiento</a></h2>
                </td>
                <td style="width: 235px; height: 30px;">
                    <h2><a href="DemoPenarandaAltas.aspx">Selección</a></h2>
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

