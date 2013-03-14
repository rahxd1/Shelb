<%@ Page Language="vb"
    Culture="es-MX" 
    masterpagefile="~/sitios/Energizer/Schick/Schick.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteBitacoraSchick.aspx.vb" 
    Inherits="procomlcd.ReporteBitacoraSchick"
    title="Schick - Reportes" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1Schick" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
            Reporte Bitácora Captura</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%">
                            <tr><td style="width: 96px">&nbsp;</td>
                                <td style="width: 359px">&nbsp;</td>
                                <td style="text-align: right">
                                    <img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" runat="server" 
                                            NavigateUrl="~/sitios/Energizer/Schick/Reportes/ReportesSchick.aspx">Regresar</asp:HyperLink></td>
                            </tr>
                            <tr><td style="width: 96px">Periodo</td>
                                <td colspan="2"><asp:DropDownList ID="cmbPeriodo" runat="server" AutoPostBack="True" CssClass="ddl"/></td>
                            </tr>
                            <tr><td style="width: 96px">Región</td>
                                <td colspan="2"><asp:DropDownList ID="cmbRegion" runat="server" CssClass="ddl" AutoPostBack="True" /></td>
                            </tr>
                            <tr><td style="width: 96px">Promotor</td>
                                <td colspan="2"><asp:DropDownList ID="cmbPromotor" runat="server" CssClass="ddl" AutoPostBack="True" />
                                    </td>
                            </tr>
                            <tr><td style="width: 96px">Tienda</td>
                                <td colspan="2"><asp:DropDownList ID="cmbTienda" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center;">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                        <ProgressTemplate>
                                            <table id="pnlSave" >
                                            <tr><td>
                                                <br /><p><img alt="Cargando Reporte" src="../../../../Img/loading.gif" /> El Reporte se 
                                                esta generando.</p><br /></td></tr>
                                        </table>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" 
                            CellPadding="3" GridLines="Vertical" ShowFooter="True" 
                            Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="None" 
                            BorderWidth="1px">
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                                <asp:CommandField EditText="Ver detalle" ShowEditButton="True">
                                <ControlStyle ForeColor="Red" />
                                </asp:CommandField>
                                <asp:BoundField DataField="id_usuario" HeaderText="Promotor" />
                                <asp:BoundField DataField="Tiendas" HeaderText="Tiendas" />
                                <asp:BoundField DataField="Capturas" HeaderText="Capturas" />
                                <asp:TemplateField HeaderText="Porcentaje"></asp:TemplateField>
                                
                           </Columns>
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                        </asp:GridView>
                        
                        <asp:Button ID="btnDetalles" runat="server" Text="Ver Detalle General" />
                        <br />
                        <asp:Panel ID="pnlDetalle" runat="server" Visible="False" BorderColor="Black" 
                        BorderStyle="Dashed" style="text-align: center" BorderWidth="2px">
                       
                        <asp:GridView ID="gridDetalle" runat="server" 
                             AutoGenerateColumns="False" CellPadding="3" 
                             GridLines="Vertical" ShowFooter="True" Width="100%" Caption="DETALLES" 
                             Font-Bold="False" BackColor="White" BorderColor="#999999" 
                             BorderStyle="None" BorderWidth="1px">
                             <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                             <Columns>
                                 <asp:BoundField DataField="id_usuario" HeaderText="Promotor" />
                                 <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/>
                                 <asp:BoundField DataField="nombre" HeaderText="Tienda" />
                                 <asp:BoundField DataField="estatus" HeaderText="Capturada" />
                             
                             </Columns>
                             <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                             <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                             <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                             <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                             <AlternatingRowStyle BackColor="#DCDCDC" />
                         </asp:GridView>
                     </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
        </div>
        
        <!--CONTENT SIDE COLUMN-->
        <div id="content-side-two-column">
            
            <asp:LinkButton ID="lnkExportar" runat="server" Font-Bold="True">Exportar Tabla a Excel</asp:LinkButton>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>