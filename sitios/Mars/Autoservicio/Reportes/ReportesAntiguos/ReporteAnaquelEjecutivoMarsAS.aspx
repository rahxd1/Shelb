<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Autoservicio/Mars_Autoservicio.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteAnaquelEjecutivoMarsAS.aspx.vb" 
    Inherits="procomlcd.ReporteAnaquelEjecutivoMarsAS"
    title="Mars Autoservicio - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsAutoservicio" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte anaquel por ejecutivo</div>

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
                    
                    <br />
                    <asp:GridView ID="gridAreaNielsen" runat="server" AutoGenerateColumns="False" 
                        ShowFooter="True" Width="50%" BackColor="#FFFF99"  
                        CaptionAlign="Top" HorizontalAlign="Center">
                        <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundField DataField="Area" HeaderText="Area nielsen" 
                                ItemStyle-Width="39px" />
                            <asp:BoundField DataField="O_ps" HeaderText="Adulto Seco" 
                                ItemStyle-Width="83px" />
                            <asp:BoundField DataField="O_pc" HeaderText="Cachorro Seco"
                                ItemStyle-Width="95px" />
                            <asp:BoundField DataField="O_ph" HeaderText="Perro Húmedo" 
                                ItemStyle-Width="60px" />
                            <asp:BoundField DataField="O_pb" HeaderText="Perro Botanas"
                                ItemStyle-Width="56px" />
                            <asp:BoundField DataField="O_gs" HeaderText="Gato Seco" 
                                ItemStyle-Width="50px" />
                            <asp:BoundField DataField="O_gh" HeaderText="Gato Húmedo"
                                ItemStyle-Width="60px" />
                            <asp:BoundField DataField="O_gb" HeaderText="Gato Botana"
                                ItemStyle-Width="56px" />
                        </Columns>
                        <FooterStyle BackColor="#17375D" />
                        <EmptyDataTemplate>
                            <h1>
                                Sin información</h1>
                        </EmptyDataTemplate>
                        <HeaderStyle BackColor="#02456F" ForeColor="White" />
                    </asp:GridView>
                    
                    <asp:Panel ID="PanelFS" runat="server" />
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" Width="645px" 
                            ShowFooter="True" CssClass="grid-view">
                            <Columns>
                                <asp:TemplateField HeaderText="Ejecutivo">
                                    <ItemTemplate>
                                        <a href="ReporteDetalleAnaquelEjecutivoMarsAS.aspx?orden=<%#Eval("orden")%>&region_mars=<%#Eval("region_mars")%>"><%#Eval("Ejecutivo")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                <asp:BoundField HeaderText="Adulto Seco" DataField="ASS"/> 
                                <asp:BoundField HeaderText="Cachorro Seco" DataField="CS"/> 
                                <asp:BoundField HeaderText="Perro Húmedo" DataField="PH"/> 
                                <asp:BoundField HeaderText="Perro Botanas" DataField="PB"/>  
                                <asp:BoundField HeaderText="Gato Seco" DataField="GS"/>  
                                <asp:BoundField HeaderText="Gato Húmedo" DataField="GH"/>  
                                <asp:BoundField HeaderText="Gato Botana" DataField="GB"/>  
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <br />
                        <asp:LinkButton ID="lnkVerTodo" runat="server" Font-Bold="True">Detalle todas las tiendas</asp:LinkButton>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbQuincena" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbEjecutivo" EventName="SelectedIndexChanged" />
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