<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Autoservicio/Mars_Autoservicio.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteDetalleAnaquelEjecutivoMarsAS2.aspx.vb" 
    Inherits="procomlcd.ReporteDetalleAnaquelEjecutivoMarsAS2"
    title="Mars Autoservicio - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsAutoservicio" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte detalle por tiendas anaquel</div>

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
                            NavigateUrl="~/sitios/Mars/Autoservicio/Reportes/ReportesMarsAS.aspx">Regresar</asp:HyperLink><br />
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
                                <img alt="Cargando..." src="../../../../Img/loading.gif" /> 
                                Cargando información.
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                    <br />
                    <asp:GridView ID="gridAreaNielsen" runat="server" AutoGenerateColumns="False" 
                        ShowFooter="True" Width="489px" CssClass="grid-view-AN">
                        <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundField DataField="Area" HeaderText="Area nielsen" ItemStyle-Width="39px" />
                            <asp:BoundField DataField="O_ps" HeaderText="Adulto Seco" ItemStyle-Width="82px" />
                            <asp:BoundField DataField="O_pc" HeaderText="Cachorro Seco" ItemStyle-Width="95px" />
                            <asp:BoundField DataField="O_ph" HeaderText="Perro Húmedo" ItemStyle-Width="63px" />
                            <asp:BoundField DataField="O_pb" HeaderText="Perro Botanas" ItemStyle-Width="59px" />
                            <asp:BoundField DataField="O_gs" HeaderText="Gato Seco" ItemStyle-Width="53px" />
                            <asp:BoundField DataField="O_gh" HeaderText="Gato Húmedo" ItemStyle-Width="63px" />
                            <asp:BoundField DataField="O_gb" HeaderText="Gato Botana" ItemStyle-Width="59px" />
                        </Columns>
                        <FooterStyle CssClass="grid-view-AN-footer" />
                        <EmptyDataTemplate>
                            <h1>Sin información</h1>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    
                    <br />
                    <asp:Panel ID="PanelFS" runat="server"/>
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" Width="645px" 
                                    ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:BoundField HeaderText="Ejecutivo" DataField="ejecutivo"/> 
                                    <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/> 
                                    <asp:BoundField HeaderText="Area nielsen" DataField="area_nielsen"/> 
                                    <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                    <asp:BoundField HeaderText="ID Tienda" DataField="codigo"/> 
                                    <asp:BoundField HeaderText="Tipo tienda" DataField="clasificacion_tienda"/> 
                                    <asp:BoundField HeaderText="Grupo cadena" DataField="nombre_grupo"/>
                                    <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/>  
                                    <asp:BoundField HeaderText="Formato" DataField="nombre_formato"/> 
                                    <asp:BoundField HeaderText="Tienda" DataField="nombre"/> 
                                    <asp:BoundField HeaderText="Mars Perro seco" DataField="1"/> 
                                    <asp:BoundField HeaderText="Competencia Perro seco" DataField="5"/>  
                                    <asp:BoundField HeaderText="Resultado" DataField="Porcentaje_PS"/> 
                                    <asp:BoundField HeaderText="Objetivo" DataField="Objetivo_PS"/>   
                                    <asp:BoundField HeaderText="Mars Cachorro" DataField="2"/> 
                                    <asp:BoundField HeaderText="Competencia Cachorro" DataField="6"/>  
                                    <asp:BoundField HeaderText="Resultado" DataField="Porcentaje_PC"/> 
                                    <asp:BoundField HeaderText="Objetivo" DataField="Objetivo_PC"/> 
                                    <asp:BoundField HeaderText="Mars Perro húmedo" DataField="3"/> 
                                    <asp:BoundField HeaderText="Competencia Perro húmedo" DataField="7"/> 
                                    <asp:BoundField HeaderText="Resultado" DataField="Porcentaje_PH"/>  
                                    <asp:BoundField HeaderText="Objetivo" DataField="Objetivo_PH"/> 
                                    <asp:BoundField HeaderText="Mars Perro botana" DataField="4"/> 
                                    <asp:BoundField HeaderText="Competencia Perro botana" DataField="8"/>  
                                    <asp:BoundField HeaderText="Resultado" DataField="Porcentaje_PB"/> 
                                    <asp:BoundField HeaderText="Objetivo" DataField="Objetivo_PB"/> 
                                    <asp:BoundField HeaderText="Mars Gato seco" DataField="9"/> 
                                    <asp:BoundField HeaderText="Competencia Gato seco" DataField="12"/> 
                                    <asp:BoundField HeaderText="Resultado" DataField="Porcentaje_GS"/>  
                                    <asp:BoundField HeaderText="Objetivo" DataField="Objetivo_GS"/> 
                                    <asp:BoundField HeaderText="Mars Gato húmedo" DataField="10"/> 
                                    <asp:BoundField HeaderText="Competencia Gato húmedo" DataField="13"/> 
                                    <asp:BoundField HeaderText="Resultado" DataField="Porcentaje_GH"/>  
                                    <asp:BoundField HeaderText="Objetivo" DataField="Objetivo_GH"/> 
                                    <asp:BoundField HeaderText="Mars Gato botana" DataField="11"/> 
                                    <asp:BoundField HeaderText="Competencia Gato botana" DataField="14"/> 
                                    <asp:BoundField HeaderText="Resultado" DataField="Porcentaje_GB"/>  
                                    <asp:BoundField HeaderText="Objetivo" DataField="Objetivo_GB"/> 
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