<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/MasterPage.master" 
    AutoEventWireup="false" 
    CodeBehind="PaginaTiempo.aspx.vb" 
    Inherits="procomlcd.PaginaTiempo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoProcomlcd" runat="Server">

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
                    <div id="three-column-container">    
    
    
    
        <table class="style5" align="center">
             <tr>
                <td class="style12">
                                        <img alt="" src="reloj-loco-gif.gif" 
                                            style="width: 121px; height: 101px" /><br />
                                        <br />
                                        </td>
                <td class="style12" style="height: 64px">
                                        <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" 
                                            Font-Size="XX-Large" style="font-size: small; font-weight: 700;" 
                                            
                                            
                                            Text="Su sesión ha excedido el tiempo límite. Por favor, ingrese de nuevo."></asp:Label>
                    </td>
            </tr>
            
            
             </table>
    
    
    
                    </div>
    </div>
        <!--

CONTENT SIDE COLUMN AVISOS

-->
        
        <div class="clear">
        
        </div>
    </div>
    
</asp:Content>
