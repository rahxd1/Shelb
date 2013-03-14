<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Energizer/EnergizerConv/Energizer_Conv.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteExistenciaEnergizerConv.aspx.vb" 
    Inherits="procomlcd.ReporteExistenciaEnergizerConv"
    title="Energizer Conveniencia - Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1EnergizerConv" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
            Reporte existencias por tienda</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr><td style="width: 96px">&nbsp;</td>
                            <td style="width: 237px">&nbsp;</td>
                            <td style="width: 90px; text-align: right">&nbsp;</td>
                            <td style="text-align: right">
                                <img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" runat="server" 
                                        NavigateUrl="~/sitios/Energizer/EnergizerConv/Reportes/ReportesEnergizerConv.aspx">Regresar</asp:HyperLink></td>
                        </tr>
                        <tr><td style="width: 96px">Periodo</td>
                            <td colspan="3"><asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="ddl" AutoPostBack="True" /></td>
                        </tr>
                        <tr><td style="width: 96px">Región</td>
                            <td colspan="3"><asp:DropDownList ID="cmbRegion" runat="server" CssClass="ddl" AutoPostBack="True" /></td>
                        </tr>
                        <tr><td style="width: 96px">Promotor</td>
                            <td colspan="3"><asp:DropDownList ID="cmbPromotor" runat="server" CssClass="ddl" AutoPostBack="True" />
                            </td>
                        </tr>
                        <tr><td style="width: 96px">Tienda</td>
                            <td colspan="3"><asp:DropDownList ID="cmbTienda" runat="server" CssClass="ddl" AutoPostBack="True"/></td></tr>
                        <tr>
                            <td style="text-align: center;" colspan="4">
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
                    <asp:Panel ID="PanelFS" runat="server"/>
                    <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" 
                        Width="100%" ShowFooter="True" CssClass="grid-view">
                            <Columns>
                                <asp:BoundField HeaderText="Region" DataField="nombre_region"/>
                                <asp:BoundField HeaderText="Periodo" DataField="nombre_periodo"/>
                                <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/>
                                <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/> 
                                <asp:BoundField HeaderText="Tienda" DataField="nombre"/> 
                                <asp:BoundField HeaderText="Existencias" DataField="ExistenciasTotales"/>  
                            </Columns>
                        <FooterStyle CssClass="grid-footer" />
                        <EmptyDataTemplate>
                            <h1>Sin información</h1>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbPromotor" EventName="SelectedIndexChanged" />
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