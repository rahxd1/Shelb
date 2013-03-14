<%@ Page Language="vb" 
Masterpagefile="~/sitios/Demos/Bancomer/DemoBancomer.Master"
AutoEventWireup="false" 
CodeBehind="DemoBancomerDocs.aspx.vb" 
Inherits="procomlcd.DemoBancomerDocs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DemoBancomerContent" runat="Server">
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
                            NavigateUrl="DemoMenuBancomer.aspx">&lt;-- Regresar</asp:HyperLink>

                        
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; color: #FFFFFF; background-color: #336699">
                        Documentos</td>
                </tr>
                <tr>
                    <td style="text-align: left">
                         
                         <asp:HyperLink ID="lnkDoc" runat="server" 
                             NavigateUrl="~/sitios/Demos/Bancomer/Docs/test.txt">Documento de Prueba</asp:HyperLink>

                         

                    </td>
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
                    <b>&quot;Sección de Documentos&quot;</b></p>
                    <br />
        </div>
        
        <div class="clear" style="color: #FFFFFF">
            .<br />
      
        
        </div>
        
    </div>
</asp:Content>