<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Mayoreo/MayoreoMars.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteResumenMarsMay.aspx.vb" 
    Inherits="procomlcd.ReporteResumenMarsMay"
    title="Mars Verificadores de Precio Mayoreo - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoMayoreoMars" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte resumen por plazas</div>

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
                        
                    <asp:Label ID="lblQuincena" runat="server" Text="Quincena" CssClass="lbl" />
                    <asp:DropDownList ID="cmbQuincena" runat="server" CssClass="cmb" 
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
                    <asp:Panel ID="PanelFS" runat="server" />
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridResumenAutoservicio" Caption="Autoservicio por regiones" runat="server" AutoGenerateColumns="False" Width="645px" 
                                    ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:BoundField HeaderText="Región" HeaderStyle-BackColor="#02456F" HeaderStyle-ForeColor="White" DataField="nombre_region"/> 
                                    
                                    <asp:BoundField HeaderText="Minimas" DataField="min_1" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Maximas" DataField="max_1" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje minimas" DataField="t_min_1" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje maximas" DataField="t_max_1" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    
                                    <asp:BoundField HeaderText="Minimas" DataField="min_2" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Maximas" DataField="max_2" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje minimas" DataField="t_min_2" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje maximas" DataField="t_max_2" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    
                                    <asp:BoundField HeaderText="Minimas" DataField="min_5" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Maximas" DataField="max_5" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje minimas" DataField="t_min_5" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje maximas" DataField="t_max_5" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    
                                    <asp:BoundField HeaderText="Minimas" DataField="min_7" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Maximas" DataField="max_7" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje minimas" DataField="t_min_7" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje maximas" DataField="t_max_7" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/>
                                </Columns>
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <br />
                        <asp:GridView ID="gridResumenMostrador" Caption="Mostrador por regiones" runat="server" AutoGenerateColumns="False" Width="645px" 
                                    ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:BoundField HeaderText="Región" HeaderStyle-BackColor="#02456F" HeaderStyle-ForeColor="White" DataField="nombre_region"/> 
                                    
                                    <asp:BoundField HeaderText="Minimas" DataField="min_1" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Maximas" DataField="max_1" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje minimas" DataField="t_min_1" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje maximas" DataField="t_max_1" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    
                                    <asp:BoundField HeaderText="Minimas" DataField="min_2" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Maximas" DataField="max_2" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje minimas" DataField="t_min_2" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje maximas" DataField="t_max_2" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    
                                    <asp:BoundField HeaderText="Minimas" DataField="min_5" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Maximas" DataField="max_5" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje minimas" DataField="t_min_5" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje maximas" DataField="t_max_5" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    
                                    <asp:BoundField HeaderText="Minimas" DataField="min_7" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Maximas" DataField="max_7" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje minimas" DataField="t_min_7" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje maximas" DataField="t_max_7" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/>
                                </Columns>
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <br />
                        
                        <asp:GridView ID="gridReporteAutoservicio" caption="Autoservicio por plazas" runat="server" AutoGenerateColumns="False" Width="645px" 
                                    ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:BoundField HeaderText="Región" HeaderStyle-BackColor="#02456F" HeaderStyle-ForeColor="White" DataField="nombre_region"/> 
                                    <asp:BoundField HeaderText="Ciudad" HeaderStyle-BackColor="#02456F" HeaderStyle-ForeColor="White" DataField="ciudad"/> 
                                    
                                    <asp:BoundField HeaderText="Minimas" DataField="min_1" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Maximas" DataField="max_1" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje minimas" DataField="t_min_1" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje maximas" DataField="t_max_1" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    
                                    <asp:BoundField HeaderText="Minimas" DataField="min_2" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Maximas" DataField="max_2" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje minimas" DataField="t_min_2" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje maximas" DataField="t_max_2" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    
                                    <asp:BoundField HeaderText="Minimas" DataField="min_5" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Maximas" DataField="max_5" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje minimas" DataField="t_min_5" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje maximas" DataField="t_max_5" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    
                                    <asp:BoundField HeaderText="Minimas" DataField="min_7" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Maximas" DataField="max_7" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje minimas" DataField="t_min_7" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje maximas" DataField="t_max_7" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/>
                                </Columns>
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <br />
                        
                        <asp:GridView ID="gridReporteMostrador" caption="Mostrador por plazas" runat="server" AutoGenerateColumns="False" Width="645px" 
                                    ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:BoundField HeaderText="Región" HeaderStyle-BackColor="#02456F" HeaderStyle-ForeColor="White" DataField="nombre_region"/> 
                                    <asp:BoundField HeaderText="Ciudad" HeaderStyle-BackColor="#02456F" HeaderStyle-ForeColor="White" DataField="ciudad"/> 
                                    
                                    <asp:BoundField HeaderText="Minimas" DataField="min_1" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Maximas" DataField="max_1" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje minimas" DataField="t_min_1" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje maximas" DataField="t_max_1" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    
                                    <asp:BoundField HeaderText="Minimas" DataField="min_2" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Maximas" DataField="max_2" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje minimas" DataField="t_min_2" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje maximas" DataField="t_max_2" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    
                                    <asp:BoundField HeaderText="Minimas" DataField="min_5" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Maximas" DataField="max_5" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje minimas" DataField="t_min_5" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje maximas" DataField="t_max_5" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    
                                    <asp:BoundField HeaderText="Minimas" DataField="min_7" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Maximas" DataField="max_7" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje minimas" DataField="t_min_7" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/> 
                                    <asp:BoundField HeaderText="Porcentaje maximas" DataField="t_max_7" ItemStyle-Width="106px" ItemStyle-HorizontalAlign="Center"/>
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
                </Triggers>
            </asp:UpdatePanel>
        </div>
        
        <!--CONTENT SIDE COLUMN-->
        <div id="content-side-two-column">
            <asp:LinkButton ID="lnkExportar" runat="server" Font-Bold="True">Exportar a Excel</asp:LinkButton>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>