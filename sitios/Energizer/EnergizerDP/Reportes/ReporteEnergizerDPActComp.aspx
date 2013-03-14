<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Energizer/EnergizerDP/Energizer_DP.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteEnergizerDPActComp.aspx.vb" 
    Inherits="procomlcd.ReporteEnergizerDPActComp"
    title="Energizer Pilas Demo 2010 - Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1EnergizerDP" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
            Reporte actividad competencia</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr><td style="width: 67px">&nbsp;</td>
                            <td style="width: 345px">&nbsp;</td>
                            <td style="text-align: right"><img alt="Regresar" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" runat="server" NavigateUrl="~/sitios/Energizer/EnergizerDP/Reportes/ReportesEnergizerDP.aspx">Regresar</asp:HyperLink></td>
                        </tr>
                        <tr><td style="width: 67px">Periodo</td>
                            <td colspan="2"><asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr><td style="width: 67px">Región</td>
                            <td colspan="2"><asp:DropDownList ID="cmbRegion" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr><td style="width: 67px">Promotor</td>
                            <td colspan="2"><asp:DropDownList ID="cmbPromotor" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr><td style="width: 67px">Cadena</td>
                            <td colspan="2"> <asp:DropDownList ID="cmbCadena" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr><td style="width: 67px">Tienda</td>
                            <td colspan="2"> <asp:DropDownList ID="cmbTienda" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
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
                    <asp:Panel ID="PanelFS" runat="server"/>
                    <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" 
                        Width="100%" ShowFooter="True" CssClass="grid-view">
                            <Columns>
                                <asp:BoundField HeaderText="Region" DataField="nombre_region"/>
                                <asp:BoundField HeaderText="Periodo" DataField="nombre_periodo"/>
                                <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/>
                                <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/> 
                                <asp:BoundField HeaderText="Tienda" DataField="nombre"/> 
                                <asp:BoundField HeaderText="Actividad Competencia" DataField="act_competencia"/>  
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