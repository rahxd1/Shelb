<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Autoservicio/Mars_Autoservicio.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteDetalleExhibicionesEjecutivoMarsAS.aspx.vb" 
    Inherits="procomlcd.ReporteDetalleExhibicionesEjecutivoMarsAS"
    title="Mars Autoservicio - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsAutoservicio" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte detalle exhibiciones por ejecutivo</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>
                    <div id="menu-regresar">
                        <img alt="" src="../../../../../Img/arrow.gif" />
                        <asp:HyperLink ID="lnkRegresar" runat="server" 
                            NavigateUrl="~/sitios/Mars/Autoservicio/Reportes/ReportesAntiguos/ReportesAntiguosMarsAS.aspx">Regresar</asp:HyperLink><br />
                    </div>
                    
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblQuincena" runat="server" Text="Quincena" CssClass="lbl" />
                    <asp:DropDownList ID="cmbQuincena" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblRegion" runat="server" Text="Región" CssClass="lbl" />
                    <asp:DropDownList ID="cmbRegion" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblEjecutivo" runat="server" Text="Ejecutivo Mars" CssClass="lbl" />
                    <asp:DropDownList ID="cmbEjecutivo" runat="server" CssClass="cmb" 
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

                    <asp:UpdateProgress ID="UpdateProgress" runat="server" 
                        AssociatedUpdatePanelID="updPnl">
                        <ProgressTemplate>
                            <div id="pnlSave" >
                                <img alt="Cargando..." src="../../../../../Img/loading.gif" /> 
                                Cargando información.
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                    <asp:Panel ID="PanelFS" runat="server"/>
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" Width="645px" 
                                    ShowFooter="True" CssClass="grid-view" BackColor="#CCFFCC">
                                <Columns>
                                    <asp:BoundField HeaderText="Ejecutivo" DataField="EjecutivoMars"/>
                                    <asp:BoundField HeaderText="ID Tienda" DataField="codigo"/> 
                                    <asp:BoundField HeaderText="Tipo tienda" DataField="clasificacion_tienda"/> 
                                    <asp:BoundField HeaderText="Región" DataField="nombre_region"/>  
                                    <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/> 
                                    <asp:BoundField HeaderText="Tienda" DataField="nombre"/> 
                                    <asp:BoundField HeaderText="PVI" DataField="PVI"/> 
                                    <asp:BoundField HeaderText="Islas" DataField="1"/> 
                                    <asp:BoundField HeaderText="Rack de bulto grande" DataField="2"/> 
                                    <asp:BoundField HeaderText="Mix feeding" DataField="3"/> 
                                    <asp:BoundField HeaderText="Mega Exhibidor" DataField="4"/> 
                                    <asp:BoundField HeaderText="Mini rack" DataField="5"/>
                                    <asp:BoundField HeaderText="Cabecera" DataField="6"/>
                                    <asp:BoundField HeaderText="Plastival sencillo" DataField="7"/>
                                    <asp:BoundField HeaderText="Exhibidor Generico" DataField="8"/>
                                    <asp:BoundField HeaderText="Totales" DataField="T_12345"/>
                                    <asp:BoundField HeaderText="Objetivo" DataField="O_12345"/>
                                    <asp:BoundField HeaderText="%" DataField="Por_12345"/>
                                    <asp:BoundField HeaderText="Cabecera" DataField="6_B"/>
                                    <asp:BoundField HeaderText="Plastival sencillo" DataField="7_B"/>
                                    <asp:BoundField HeaderText="Exhibidor generico" DataField="8_B"/>
                                    <asp:BoundField HeaderText="Totales" DataField="T_678"/>
                                    <asp:BoundField HeaderText="Objetivo" DataField="O_678"/>
                                    <asp:BoundField HeaderText="%" DataField="Por_678"/>
                                    <asp:BoundField HeaderText="Balcones" DataField="9"/>
                                    <asp:BoundField HeaderText="Objetivo" DataField="O_9"/>
                                    <asp:BoundField HeaderText="%" DataField="Por_9"/>
                                    <asp:BoundField HeaderText="Tiras" DataField="10"/>
                                    <asp:BoundField HeaderText="Objetivo" DataField="O_10"/>
                                    <asp:BoundField HeaderText="%" DataField="Por_10"/>
                                    <asp:BoundField HeaderText="Lateros" DataField="11"/>
                                    <asp:BoundField HeaderText="Objetivo" DataField="O_11"/>
                                    <asp:BoundField HeaderText="%" DataField="Por_11"/>
                                    <asp:BoundField HeaderText="Poucheros" DataField="12"/>
                                    <asp:BoundField HeaderText="Objetivo" DataField="O_12"/>
                                    <asp:BoundField HeaderText="%" DataField="Por_12"/>
                                    <asp:BoundField HeaderText="PDQ's" DataField="13"/>
                                    <asp:BoundField HeaderText="Objetivo" DataField="O_13"/>
                                    <asp:BoundField HeaderText="%" DataField="Por_13"/>
                                    <asp:BoundField HeaderText="Otros Exhibidores" DataField="14"/>
                                    <asp:BoundField HeaderText="Objetivo" DataField="O_14"/>
                                    <asp:BoundField HeaderText="%" DataField="Por_14"/> 
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
                    <asp:AsyncPostBackTrigger ControlID="cmbQuincena" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbEjecutivo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbSupervisor" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbCadena" EventName="SelectedIndexChanged" />
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