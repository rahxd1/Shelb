<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion_Mars.Master"
    AutoEventWireup="false" 
    CodeBehind="AdmincuestionarioMars.aspx.vb" 
    Inherits="procomlcd.AdmincuestionarioMars"
    Title="Administración Newell Rubbermaid" %>

<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsCapacitacion" runat="Server">
  
<!--titulo-pagina -->
    <div id="titulo-pagina">
       Periodos promotor Newell Rubbermaid</div>    

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            
            <asp:ScriptManager ID="ScriptManager1" runat="server"/>
           
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td><asp:Label ID="lblAviso" runat="server" CssClass="aviso"></asp:Label></td>
                        </tr>
                    </table>
                    
                    <asp:Panel ID="pnlNuevo" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: right; width: 148px; height: 19px;">
                                    ID Periodo</td>
                                <td style="height: 19px">
                                    <asp:Label ID="lblIDPeriodo" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 148px">
                                    Nombre Periodo</td>
                                <td>
                                    <asp:TextBox ID="txtRespuesta" runat="server"
                                        Height="199px" MaxLength="50" TextMode="MultiLine" Width="511px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: right; width: 154px">
                                    &nbsp;</td>
                                <td style="width: 215px; text-align: center;">
                                    <asp:Button ID="btnGuardar" runat="server" CssClass="button" Text="Guardar" 
                                        ValidationGroup="Periodo" />
                                </td>
                                <td style="text-align: center">
                                    <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                                        CssClass="button" Text="Cancelar" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>       
        </div>
        
<!--CONTENT SIDE COLUMN-->
            
        <div class="clear">
        </div>
    </div>
</asp:Content>
