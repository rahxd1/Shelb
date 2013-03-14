<%@ Page Language="vb" 
 masterpagefile="~/sitios/Mars/Autoservicio/Mars_Autoservicio.Master"
AutoEventWireup="false" 
CodeBehind="tutorial.aspx.vb" 
Inherits="procomlcd.tutorial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsAutoservicio" runat="Server">
   <!--

POSTER PHOTO

-->
    <div id="poster-photo-container">
    
    <div id="poster-photo-image"></div>
         
          </div>
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


                         <asp:HyperLink ID="HyperLink1" runat="server" 
                                NavigateUrl='~/ARCHIVOS/CLIENTES/MARS/Dinamica/manual_autoservicio.wmv'>Manual Autoservicio</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
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
                    <br />
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center; background-color: #FFFFFF;">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        
        <div class="clear" style="color: #FFFFFF">
            .<br />
            .<br />
            .<br />
            .<br />
            .<br />
            .<br />
            .<br />
            .<br />
            .<br />
            .<br />
            .<br />
        
        </div>
        
    </div>
</asp:Content>