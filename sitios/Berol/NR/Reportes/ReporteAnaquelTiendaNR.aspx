<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Berol/NR/Rubbermaid.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteAnaquelTiendaNR.aspx.vb" 
    Inherits="procomlcd.ReporteAnaquelTiendaNR"
    title="Newell Rubbermaid - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoNR" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte anaquel por tienda por categoria</div>

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
                            NavigateUrl="~/sitios/Berol/NR/Reportes/ReportesNR.aspx">Regresar</asp:HyperLink><br />
                    </div>
                    
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                            
                    <asp:Label ID="lblRegion" runat="server" Text="Región" CssClass="lbl" />
                    <asp:DropDownList ID="cmbRegion" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                    
                    <asp:Label ID="lblEstado" runat="server" Text="Estado" CssClass="lbl" />
                    <asp:DropDownList ID="cmbEstado" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblSupervisor" runat="server" Text="Supervisor" CssClass="lbl" />
                    <asp:DropDownList ID="cmbSupervisor" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblPromotor" runat="server" Text="Promotor" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPromotor" runat="server" CssClass="cmb"
                        AutoPostBack="True" /><br />
                    
                    <asp:Label ID="lblCadena" runat="server" Text="Cadena" CssClass="lbl" />
                    <asp:DropDownList ID="cmbCadena" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                    
                    <asp:Label ID="lblFormato" runat="server" Text="Formato" CssClass="lbl" />
                    <asp:DropDownList ID="cmbFormato" runat="server" CssClass="cmb"  
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblTienda" runat="server" Text="Tienda" CssClass="lbl" />
                    <asp:DropDownList ID="cmbTienda" runat="server" CssClass="cmb"
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
                    
                    <asp:Panel ID="PanelFS" runat="server" />
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" Width="645px" 
                            ShowFooter="True" CssClass="grid-view">
                            <Columns>  
                                <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                <asp:BoundField HeaderText="Ciudad" DataField="ciudad"/> 
                                <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/> 
                                <asp:BoundField HeaderText="Supervisor" DataField="supervisor"/> 
                                <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/> 
                                <asp:BoundField HeaderText="Formato" DataField="nombre_formato"/> 
                                <asp:BoundField HeaderText="ID" DataField="codigo"/> 
                                <asp:BoundField HeaderText="Tienda" DataField="nombre"/> 
                                <asp:BoundField HeaderText="Lapices (NR)" DataField="1"/>
                                <asp:BoundField HeaderText="Lapices (Competencia)" DataField="101"/>  
                                <asp:BoundField HeaderText="Bolígrafos (NR)" DataField="2"/>
                                <asp:BoundField HeaderText="Bolígrafos (Competencia)" DataField="102"/>  
                                <asp:BoundField HeaderText="Roller y gel (NR)" DataField="3"/>
                                <asp:BoundField HeaderText="Roller y gel (Competencia)" DataField="103"/>  
                                <asp:BoundField HeaderText="Correctores (NR)" DataField="4"/>
                                <asp:BoundField HeaderText="Correctores (Competencia)" DataField="104"/>  
                                <asp:BoundField HeaderText="Plumones (NR)" DataField="5"/>
                                <asp:BoundField HeaderText="Plumones (Competencia)" DataField="105"/>  
                                <asp:BoundField HeaderText="Marcadores (NR)" DataField="6"/>
                                <asp:BoundField HeaderText="Marcadores (Competencia)" DataField="106"/>  
                                <asp:BoundField HeaderText="Resaltadores (NR)" DataField="7"/>
                                <asp:BoundField HeaderText="Resaltadores (Competencia)" DataField="107"/>  
                                <asp:BoundField HeaderText="Crayones (NR)" DataField="8"/>
                                <asp:BoundField HeaderText="Crayones (Competencia)" DataField="108"/>  
                                <asp:BoundField HeaderText="Lapices de color (NR)" DataField="9"/>
                                <asp:BoundField HeaderText="Lapices de color (Competencia)" DataField="109"/>  
                                <asp:BoundField HeaderText="Lapiceros (NR)" DataField="10"/>
                                <asp:BoundField HeaderText="Lapiceros (Competencia)" DataField="110"/>  
                                <asp:BoundField HeaderText="Marcador base agua (NR)" DataField="11"/>
                                <asp:BoundField HeaderText="Marcador base agua (Competencia)" DataField="111"/>  
                                <asp:BoundField HeaderText="Marcadores para pizarrón (NR)" DataField="12"/>
                                <asp:BoundField HeaderText="Marcadores para pizarrón (Competencia)" DataField="112"/>  
                                <asp:BoundField HeaderText="Producto propio SAM'S (NR)" DataField="13"/>
                                <asp:BoundField HeaderText="Producto propio SAM'S (Competencia)" DataField="113"/>  
                                <asp:BoundField HeaderText="Puntillas (NR)" DataField="14"/>
                                <asp:BoundField HeaderText="Puntillas (Competencia)" DataField="114"/>  
                                <asp:BoundField HeaderText="Gomas (NR)" DataField="15"/>
                                <asp:BoundField HeaderText="Gomas (Competencia)" DataField="115"/>  
                                <asp:BoundField HeaderText="Checadores (NR)" DataField="16"/>
                                <asp:BoundField HeaderText="Checadores (Competencia)" DataField="116"/>  
                                <asp:BoundField HeaderText="Repuestos (NR)" DataField="17"/>
                                <asp:BoundField HeaderText="Repuestos (Competencia)" DataField="117"/>  
                                <asp:BoundField HeaderText="Rotuladores (NR)" DataField="18"/>
                                <asp:BoundField HeaderText="Rotuladores (Competencia)" DataField="118"/>   
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
                
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbEstado" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbSupervisor" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbPromotor" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbCadena" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbFormato" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbTienda" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        
        <!--CONTENT SIDE COLUMN-->
        <div id="content-side-two-column">
            <asp:LinkButton ID="lnkExportar" runat="server" Font-Bold="True">Exportar a Excel</asp:LinkButton>
        </div>
        
        <div class="clear"></div>
        
    </div>
</asp:Content>