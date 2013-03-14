<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/SYM/AC/SYMAC.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteBitacoraAC.aspx.vb" 
    Inherits="procomlcd.ReporteBitacoraAC" 
    Title="SYM Anaquel y Catalogación - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSYMAC" runat="Server">

    <!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte bitácora captura</div>

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
                            NavigateUrl="~/sitios/SYM/AC/Reportes/ReportesSYMAC.aspx">Regresar</asp:HyperLink><br />
                    </div>
                    
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />    
                        
                    <asp:Label ID="lblRegion" runat="server" Text="División" CssClass="lbl" />
                    <asp:DropDownList ID="cmbRegion" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblEstado" runat="server" Text="Estado" CssClass="lbl" />
                    <asp:DropDownList ID="cmbEstado" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />    
                        
                    <asp:Label ID="lblPromotor" runat="server" Text="Promotor" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPromotor" runat="server" CssClass="cmb" 
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
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" 
                            Width="100%" ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:CommandField EditText="Ver detalle" ShowEditButton="True">
                                        <ControlStyle ForeColor="Red" />
                                    </asp:CommandField>
                                    <asp:BoundField HeaderText="División" DataField="nombre_region"/> 
                                    <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/>  
                                    <asp:BoundField HeaderText="Tiendas" DataField="tiendas"/> 
                                    <asp:BoundField HeaderText="Capturas Anaquel" DataField="CapturasA"/>
                                    <asp:BoundField HeaderText="Incompletas Anaquel" DataField="IncompletasA"/>
                                    <asp:BoundField HeaderText="Capturas Catalogación" DataField="CapturasC"/>
                                    <asp:BoundField HeaderText="Incompletas Catalogación" DataField="IncompletasC"/>
                                    <asp:BoundField HeaderText="Porcentaje" DataField="Total"/>
                                </Columns>   
                                <FooterStyle CssClass="grid-footer" />                   
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                    <asp:Panel ID="pnlDetalle" runat="server" BorderColor="Black" 
                        BorderStyle="Dashed" BorderWidth="2px" style="text-align: center" 
                        Visible="False">
                        <asp:GridView ID="gridDetalle" runat="server" AutoGenerateColumns="False" 
                            Caption="DETALLES" CssClass="grid-view" Font-Bold="False" ShowFooter="True" 
                            Width="100%">
                            <Columns>
                                <asp:BoundField DataField="id_usuario" HeaderText="Promotor" />
                                <asp:BoundField DataField="nombre_cadena" HeaderText="Cadena" />
                                <asp:BoundField DataField="nombre" HeaderText="Tienda" />
                                <asp:BoundField DataField="estatus_anaquel" HeaderText="Anaquel" />
                                <asp:BoundField DataField="estatus_catalogacion" HeaderText="Catalogación" />
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbEstado" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbPromotor" EventName="SelectedIndexChanged" />
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