<%@ Page Language="vb" 
    Culture="es-MX" 
    AutoEventWireup="false"
    masterpagefile="~/sitios/Energizer/Schick/Schick.Master"
    CodeBehind="ReporteProdyPromSchick.aspx.vb" 
    Inherits="procomlcd.ReporteProdyPromSchick"
    title="Schick - Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1Schick" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
            Reporte Productos y promocionales por tienda</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr><td style="width: 73px; height: 18px;"></td>
                            <td style="width: 351px; height: 18px;"></td>
                            <td style="text-align: right; height: 18px;"><img alt="Regresar" src="../../../../Img/arrow.gif" />
                            <asp:HyperLink ID="lnkRegresar" runat="server" NavigateUrl="~/sitios/Energizer/Schick/Reportes/ReportesSchick.aspx">Regresar</asp:HyperLink></td>
                        </tr>
                        <tr>
                            <td style="width: 73px">
                                Periodo</td>
                            <td colspan="2">
                                <asp:DropDownList ID="cmbPeriodo" runat="server" AutoPostBack="True" CssClass="ddl" />
                            </td>
                        </tr>
                        <tr><td style="width: 73px">Región</td>
                            <td colspan="2"><asp:DropDownList ID="cmbRegion" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr><td style="width: 73px">Promotor</td>
                            <td colspan="2"><asp:DropDownList ID="cmbPromotor" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr><td style="width: 73px">Cadena</td>
                            <td colspan="2"><asp:DropDownList ID="cmbCadena" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr>
                            <td style="width: 73px">Tienda</td>
                            <td colspan="2"><asp:DropDownList ID="cmbTienda" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr><td colspan="3" style="text-align: center;">
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
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" 
                        CellPadding="3" 
                        GridLines="Vertical" Width="100%" ShowFooter="True" BackColor="White" 
                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px">
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                                <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/> 
                                <asp:BoundField HeaderText="Tienda" DataField="nombre"/> 
                                <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/>
                                <asp:BoundField HeaderText="Schick Schick2 Bolsa x5 MB" DataField="1"/>
                                <asp:BoundField HeaderText="SCHICK EXACTAPSx3" DataField="2"/>
                                <asp:BoundField HeaderText="SCHICK EXACTAPDELx3" DataField="3"/>
                                <asp:BoundField HeaderText="SCHICK EXACTAPS 5x4" DataField="4"/>
                                <asp:BoundField HeaderText="SCHICK EXACTAPDEL 5x4" DataField="5"/>
                                <asp:BoundField HeaderText="SCHICK EXACTAPS 10x7" DataField="6"/>
                                <asp:BoundField HeaderText="SCHICK EXACTAPD 10x7" DataField="7"/>
                                <asp:BoundField HeaderText="Schick Exacta3 PS Bolsa x6 (6X5) MB" DataField="8"/>
                                <asp:BoundField HeaderText="SCHICK XTREMEPSx2" DataField="9"/>
                                <asp:BoundField HeaderText="Schick Xtreme3 PD BP x2 HT" DataField="10"/>
                                <asp:BoundField HeaderText="Schick Xtreme3 PS BP x4 (4x3) MB" DataField="11"/>
                                <asp:BoundField HeaderText="Schick Quattro Des PS BP x2 MB" DataField="12"/>
                                <asp:BoundField HeaderText="Schick Quattro Des PD BP x2 MB" DataField="13"/>
                                <asp:BoundField HeaderText="Schick Quattro Titanium Maq BP x1 MB2" DataField="14"/>
                                <asp:BoundField HeaderText="Schick Quattro Titanium Maq BP x1 s/b MB" DataField="15"/>
                                <asp:BoundField HeaderText="Schick Quattro Titanium Cart BP x2 MB2" DataField="16"/>
                                <asp:BoundField HeaderText="Schick Quattro Titanium Cart BP x4 MB2" DataField="17"/>
                                <asp:BoundField HeaderText="Schick Quattro Precision Maq BP x1 MB2" DataField="18"/>
                                <asp:BoundField HeaderText="Schick QuattroFW Cart BP x2 MB" DataField="19"/>
                                <asp:BoundField HeaderText="Schick QuattroFW Maq BP x1 Cosm MB" DataField="20"/>
                                <asp:BoundField HeaderText="Schick Lady Prot Maq BP x1 MB" DataField="21"/>
                                <asp:BoundField HeaderText="SCHICK LADYPROTCARTx3NE" DataField="22"/>
                                <asp:BoundField HeaderText="Schick Intuition Maq BP x1 MB" DataField="23"/>
                                <asp:BoundField HeaderText="SCHICK INTUITION CARTX3" DataField="24"/>
                                <asp:BoundField HeaderText="XTREME3 X - MEN C/2" DataField="25"/>
                                <asp:BoundField HeaderText="PLAYERA (promocional)" DataField="100"/>
                                <asp:BoundField HeaderText="MORRAL (promocional)" DataField="200"/>
                                <asp:BoundField HeaderText="GORRA (promocional)" DataField="300"/>
                                <asp:BoundField HeaderText="QUATTRO TIT (promocional)" DataField="400"/>
                                <asp:BoundField HeaderText="QUATTRO ENERGY (promocional)" DataField="500"/>
                                <asp:BoundField HeaderText="COSMETIQUERA (promocional)" DataField="600"/>
                            </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            <h1>Sin información</h1>
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                    </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbPromotor" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbCadena" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbTienda" EventName="SelectedIndexChanged" />
                </Triggers>
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