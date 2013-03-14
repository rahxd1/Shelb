<%@ Page Language="vb" 
MASTERPAGEFILE="~/sitios/SYM/Demos2013/SYMDemos2013.Master"
AutoEventWireup="false" 
CodeBehind="RptDemos2013Bitacora.aspx.vb" 
Inherits="procomlcd.RptDemos2013Bitacora" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Contenido_SYMDemos2013" runat="Server">
  <!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte bitácora captura</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%">
                            <tr><td style="width: 96px">&nbsp;</td>
                                <td style="width: 434px">&nbsp;</td>
                                <td style="text-align: right">
                                    <img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" runat="server" 
                                            NavigateUrl="~/sitios/SYM/Demos/Reportes/ReportesSYMDemos.aspx">Regresar</asp:HyperLink></td>
                            </tr>
                            <tr>
                                <td style="width: 96px">Periodo</td>
                                <td colspan="2"><asp:DropDownList ID="cmbPeriodo" runat="server" Height="22px" Width="250px" AutoPostBack="True" /></td>
                            </tr>
                            <tr>
                                <td style="width: 96px">Divisiónes</td>
                                <td colspan="2"><asp:DropDownList ID="cmbRegion" runat="server" Height="22px" Width="250px" AutoPostBack="True" /></td>
                            </tr>
                            <tr><td style="width: 96px">Supervisor</td>
                                <td colspan="2"><asp:DropDownList ID="cmbPromotor" runat="server" Height="22px" Width="250px" AutoPostBack="True" /></td>
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
                            <asp:Panel ID="PnlGrid" runat="server" ScrollBars="Both" CssClass="panel-grid" Height="500">
                            <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" 
                                Width="100%" ShowFooter="True" CssClass="grid-view">
                                    <Columns>
                                        <asp:BoundField HeaderText="REGION" DataField="REGION"/> 
                                        <asp:BoundField HeaderText="USUARIOS" DataField="id_usuario"/> 
                                        <asp:BoundField HeaderText="TIENDAS" DataField="TIENDAS"/> 
                                        <asp:BoundField HeaderText="CAPTURA VENTAS" DataField="CAPTURA_VENTAS"/>
                                        <asp:BoundField HeaderText="CAPTURA IMAGENES" DataField="CAPTURA_IMAGENES"/>
                                        <asp:BoundField HeaderText="% CAPTURA" DataField="PORCENTAJE"/>
                                    </Columns>   
                                    <FooterStyle CssClass="grid-footer" />                   
                                <EmptyDataTemplate>
                                    <h1>Sin información</h1>
                                </EmptyDataTemplate>
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

