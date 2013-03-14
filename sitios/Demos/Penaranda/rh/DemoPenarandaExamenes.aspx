<%@ Page Language="vb" 
Masterpagefile="~/sitios/Demos/Penaranda/DemoPenaranda.Master"
AutoEventWireup="false" 
CodeBehind="DemoPenarandaExamenes.aspx.vb" 
Inherits="procomlcd.DemoPenarandaExamenes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DemoPenarandaContent" runat="Server">
    <!--

POSTER PHOTO

-->
    
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
                <!-- celda animacion flash-->
                    <td style="height: 19px">

                        
                        <asp:HyperLink ID="HyperLink6" runat="server" 
                            NavigateUrl="~/sitios/Demos/Penaranda/rh/DemoPenarandaRH.aspx">&lt;-- Regresar</asp:HyperLink>

                        
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; color: #FFFFFF; background-color: #336699">
                        Examenes</td>
                </tr>
                <tr>
                    <td style="text-align: left">
                         
                         Ningun Examen disponible.</td>
                </tr>
                <tr>
                    <td style="background-color: #FF9900">
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
               Aviso</h3>
            <p style="text-align: center; color: #CC0000;">
                    <b>&quot;Sección de Examenes&quot;</b></p>
                    <br />
        </div>
        
        <div class="clear" style="color: #FFFFFF">
            .<br />
      
        
        </div>
        
    </div>
</asp:Content>

