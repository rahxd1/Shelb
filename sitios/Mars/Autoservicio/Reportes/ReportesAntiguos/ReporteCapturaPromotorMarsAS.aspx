<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Autoservicio/Mars_Autoservicio.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteCapturaPromotorMarsAS.aspx.vb" 
    Inherits="procomlcd.ReporteCapturaPromotorMarsAS"
    title="Mars Autoservicio - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsAutoservicio" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte detalle captura promotor por tienda</div>

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
                        
                    <asp:Label ID="lblTienda" runat="server" Text="Tienda" CssClass="lbl" />
                    <asp:DropDownList ID="cmbTienda" runat="server" CssClass="cmb" 
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
                    
                    <asp:Panel ID="PanelFS" runat="server" />
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" Width="645px" 
                                    ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                    <asp:BoundField HeaderText="Ejecutivo" DataField="ejecutivo"/>  
                                    <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/> 
                                    <asp:BoundField HeaderText="Periodo" DataField="nombre_periodo"/> 
                                    <asp:BoundField HeaderText="Quincena" DataField="id_quincena"/> 
                                    <asp:BoundField HeaderText="SAP" DataField="codigo"/> 
                                    <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/> 
                                    <asp:BoundField HeaderText="Tienda" DataField="nombre"/> 
                                    <asp:BoundField HeaderText="Clasificacion" DataField="clasificacion_tienda"/>
                                    <asp:BoundField HeaderText="Area nielsen" DataField="area_nielsen"/>
                                    <asp:BoundField HeaderText="Razas pequeñas (Mars)" DataField="15"/> 
                                    <asp:BoundField HeaderText="Resto de adulto (Mars)" DataField="1"/> 
                                    <asp:BoundField HeaderText="Cachorro seco (Mars)" DataField="2"/>   
                                    <asp:BoundField HeaderText="Perro húmedo (Mars)" DataField="3"/> 
                                    <asp:BoundField HeaderText="Perro botanas (Mars)" DataField="4"/> 
                                    <asp:BoundField HeaderText="Adulto seco (competencia)" DataField="5"/>
                                    <asp:BoundField HeaderText="Cachorro seco (competencia)" DataField="6"/>
                                    <asp:BoundField HeaderText="Perro húmedo (competencia)" DataField="7"/> 
                                    <asp:BoundField HeaderText="Perro botanas (competencia)" DataField="8"/> 
                                    <asp:BoundField HeaderText="Gato seco (Mars)" DataField="9"/> 
                                    <asp:BoundField HeaderText="Gato húmedo (Mars)" DataField="10"/> 
                                    <asp:BoundField HeaderText="Gato botana (Mars)" DataField="11"/> 
                                    <asp:BoundField HeaderText="Gato seco (Competencia)" DataField="12"/> 
                                    <asp:BoundField HeaderText="Gato húmedo (Competencia)" DataField="13"/> 
                                    <asp:BoundField HeaderText="Gato botana (Competencia)" DataField="14"/>  
                                    <asp:BoundField HeaderText="Islas" DataField="B1"/> 
                                    <asp:BoundField HeaderText="Rack de bulto grande" DataField="B2"/> 
                                    <asp:BoundField HeaderText="Mix feeding" DataField="B3"/> 
                                    <asp:BoundField HeaderText="Mega Exhibidor" DataField="B4"/> 
                                    <asp:BoundField HeaderText="Mini rack" DataField="B5"/>
                                    <asp:BoundField HeaderText="Cabecera" DataField="B6"/>
                                    <asp:BoundField HeaderText="Plastival sencillo" DataField="B7"/>
                                    <asp:BoundField HeaderText="Exhibidor Generico" DataField="B8"/>
                                    <asp:BoundField HeaderText="Balcones" DataField="B9"/>
                                    <asp:BoundField HeaderText="Tiras" DataField="B10"/>
                                    <asp:BoundField HeaderText="Lateros" DataField="B11"/>
                                    <asp:BoundField HeaderText="Poucheros" DataField="B12"/>
                                    <asp:BoundField HeaderText="PDQ's" DataField="B13"/>
                                    <asp:BoundField HeaderText="Otros Exhibidores" DataField="B14"/>
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
                    <asp:AsyncPostBackTrigger ControlID="cmbPromotor" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbCadena" EventName="SelectedIndexChanged" />
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