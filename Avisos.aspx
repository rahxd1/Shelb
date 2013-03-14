<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/MasterPage.master" 
    AutoEventWireup="false" 
    CodeBehind="Avisos.aspx.vb" 
    Inherits="procomlcd.Avisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoProcomlcd" runat="Server">
    <!--

CONTENT CONTAINER

-->
    <div id="titulo-pagina">
        Avisos</div>

    <div id="contenido-columna-derecha">
        <!--
CONTENT MAIN COLUMN
-->
        <div id="content-main-two-column">
        
                <table style="width: 100%">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: right">
                            <img alt="" src="Imagenes/arrow.gif" />
                            <a href="default.aspx">Regresar</a></td>
                    </tr>
                    </table>
                
        <br>
            <h2>
                Avisos</h2>
            <asp:Panel ID="pnlDatos" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td>
                            Titulo</td>
                        <td colspan="2">
                            <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Fecha</td>
                        <td colspan="2">
                            <asp:Label ID="lblFecha" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Descripción</td>
                        <td colspan="2">
                            <asp:Label ID="lblDescripcion" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 110px">
                            Comentario</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtComentario" runat="server" Height="115px" MaxLength="300" 
                                TextMode="MultiLine" Width="378px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Seguimiento</td>
                        <td colspan="2">
                            <asp:DropDownList ID="cmbSeguimiento" runat="server" AutoPostBack="True" 
                                Height="22px" Width="250px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="pnlSeguimiento" runat="server" Visible="False">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 108px">
                                            Estatus</td>
                                        <td>
                                            <asp:DropDownList ID="cmbEstatus" runat="server" AutoPostBack="True" 
                                                Height="22px" Width="250px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: center">
                            <asp:Button ID="btnEnviar" runat="server" CssClass="button" 
                                style="text-align: center" Text="Enviar" ValidationGroup="" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td colspan="2">
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
            
            
        </div>
        
        <div class="clear">
        
        </div>
    </div>
    
</asp:Content>
