<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Mayoreo/MayoreoMars.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteBitacoraMarsVP.aspx.vb" 
    Inherits="procomlcd.ReporteBitacoraMarsVP"
    title="Mars Verificadores de Precio Mayoreo - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoMayoreoMars" runat="Server">

<!--titulo-pagina-->
    <div id="titulo-pagina">Reporte bitácora captura</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>
                    <div id="menu-regresar">
                        <img alt="" src="../../../../Img/arrow.gif" />
                        <asp:HyperLink ID="lnkRegresar" runat="server" 
                            NavigateUrl="~/sitios/Mars/Mayoreo/Reportes/ReportesMayoreoMars.aspx">Regresar</asp:HyperLink><br />
                    </div>
                    
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblSemana" runat="server" Text="Semana" CssClass="lbl" />
                    <asp:DropDownList ID="cmbSemana" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />     
                        
                    <asp:Label ID="lblRegion" runat="server" Text="Región" CssClass="lbl" />
                    <asp:DropDownList ID="cmbRegion" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />  
                        
                    <asp:UpdateProgress ID="UpdateProgress" runat="server" 
                        AssociatedUpdatePanelID="updPnl">
                        <ProgressTemplate>
                            <div id="pnlSave" >
                                <img alt="Cargando..." src="../../../../Img/loading.gif" /> 
                                Cargando información.
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                    <br />
                    <asp:Panel ID="pnlGrillas" runat="server">
                        <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                            CssClass="panel-grid">
                            <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" 
                                CssClass="grid-view" ShowFooter="True" Width="644px">
                                <Columns>
                                    <asp:CommandField EditText="Ver detalle" ShowEditButton="True">
                                    <ControlStyle ForeColor="Red" />
                                    </asp:CommandField>
                                    <asp:BoundField DataField="id_usuario" HeaderText="Promotor" />
                                    <asp:BoundField DataField="Tiendas" HeaderText="Tiendas" />
                                    <asp:BoundField DataField="Capturas" HeaderText="Capturas" />
                                    <asp:BoundField DataField="Incompletas" HeaderText="Incompletas" />
                                    <asp:BoundField DataField="Porcentaje" HeaderText="%" />
                                </Columns>
                                <FooterStyle CssClass="grid-footer" />
                                <EmptyDataTemplate>
                                    <h1>
                                        Sin información</h1>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="pnlDetalle" runat="server" Visible="False" BorderColor="Black" 
                            style="text-align: center">
                             <asp:GridView ID="gridDetalle" runat="server" 
                                 AutoGenerateColumns="False" ShowFooter="True" Width="638px" Caption="DETALLES" 
                                 Font-Bold="False" CssClass="grid-view">
                                 <Columns>
                                     <asp:BoundField DataField="id_usuario" HeaderText="Promotor" />
                                     <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/>
                                     <asp:BoundField DataField="nombre" HeaderText="Tienda" />
                                     <asp:BoundField DataField="estatus" HeaderText="Capturada" />
                                 </Columns>
                                 <FooterStyle CssClass="grid-footer" />
                             </asp:GridView>
                         </asp:Panel>
                    </asp:Panel> 
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbSemana" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        
        <!--CONTENT SIDE COLUMN-->
        <div id="content-side-two-column">
            <asp:LinkButton ID="lnkExportar" runat="server" Font-Bold="True">Exportar a Excel</asp:LinkButton>
        </div>
        <div class="clear">
                        <asp:Button ID="btnDetalles" runat="server" Text="Ver Detalle General" 
                            CssClass="button" Width="175px" />
        </div>
    </div>
</asp:Content>