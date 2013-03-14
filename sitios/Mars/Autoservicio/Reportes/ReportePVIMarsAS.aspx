<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Autoservicio/Mars_Autoservicio.Master"
    AutoEventWireup="false" 
    CodeBehind="ReportePVIMarsAS.aspx.vb" 
    Inherits="procomlcd.ReportePVIMarsAS"
    title="Mars Autoservicio - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsAutoservicio" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte PVI por ejecutivo</div>

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
                    <asp:Panel ID="PanelFS" runat="server"/>
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridResumen" runat="server" AutoGenerateColumns="False" Width="30%" 
                                    ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:BoundField HeaderText="Tiendas" DataField="Tiendas"/>                                     
                                    <asp:BoundField HeaderText="PVI logradas" DataField="PVI"/>       
                                    <asp:BoundField HeaderText="no logradas" DataField="NoPVI"/>
                                </Columns>
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <br />
                        <asp:GridView ID="gridDetalle" runat="server" AutoGenerateColumns="False" Width="100%" 
                                    ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                    <asp:BoundField HeaderText="Grupo cadena" DataField="nombre_grupo"/>
                                    <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/> 
                                    <asp:BoundField HeaderText="Formato" DataField="nombre_formato"/> 
                                    <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/> 
                                    <asp:BoundField HeaderText="Tienda" DataField="nombre"/>                                     
                                    <asp:BoundField HeaderText="Quincena" DataField="id_quincena"/>       
                                    <asp:BoundField HeaderText="Cumplimiento" DataField="Cumplimiento"/>
                                </Columns>
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <br />
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" Width="100%" 
                                    ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:BoundField HeaderText="Ejecutivo" DataField="ejecutivo"/>  
                                    <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                    <asp:BoundField HeaderText="Grupo cadena" DataField="nombre_grupo"/>
                                    <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/>                                     
                                    <asp:BoundField HeaderText="Formato" DataField="nombre_formato"/> 
                                    <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/> 
                                    <asp:BoundField HeaderText="Tienda" DataField="nombre"/> 
                                    
                                    <asp:BoundField HeaderText="Faltan" DataField="Faltan_A_9" ItemStyle-Width="90px" /> 
                                    <asp:BoundField HeaderText="Faltan" DataField="Faltan_A_10" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="Faltan" DataField="Faltan_A_11" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="Faltan" DataField="Faltan_A_12" ItemStyle-Width="90px"/> 
                                    
                                    <asp:BoundField HeaderText="Faltan" DataField="Faltan_M_1" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="Faltan" DataField="Faltan_M_6" ItemStyle-Width="90px"/>  
                                    <asp:BoundField HeaderText="Faltan" DataField="Faltan_M_15" ItemStyle-Width="90px"/>
                                    
                                    <asp:BoundField HeaderText="Faltan" DataField="Faltan_C_1" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="Faltan" DataField="Faltan_C_3" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="Faltan" DataField="Faltan_C_5" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="Faltan" DataField="Faltan_C_15" ItemStyle-Width="90px"/> 
                                    
                                    <asp:BoundField HeaderText="Faltan" DataField="Faltan_ES_5" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="Faltan" DataField="Faltan_ES_16" ItemStyle-Width="90px"/>  
                                                                       
                                    <asp:BoundField HeaderText="Sobran" DataField="Sobran_A_9" ItemStyle-Width="90px"/>
                                    <asp:BoundField HeaderText="Sobran" DataField="Sobran_A_10" ItemStyle-Width="90px"/>
                                    <asp:BoundField HeaderText="Sobran" DataField="Sobran_A_11" ItemStyle-Width="90px"/>  
                                    <asp:BoundField HeaderText="Sobran" DataField="Sobran_A_12" ItemStyle-Width="90px"/>
                                    
                                    <asp:BoundField HeaderText="Sobran" DataField="Sobran_M_1" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="Sobran" DataField="Sobran_M_6" ItemStyle-Width="90px"/>
                                    <asp:BoundField HeaderText="Sobran" DataField="Sobran_M_15" ItemStyle-Width="90px"/>  
                                    
                                    <asp:BoundField HeaderText="Sobran" DataField="Sobran_C_1" ItemStyle-Width="90px"/> 
                                    <asp:BoundField HeaderText="Sobran" DataField="Sobran_C_3" ItemStyle-Width="90px"/>  
                                    <asp:BoundField HeaderText="Sobran" DataField="Sobran_C_5" ItemStyle-Width="90px"/>
                                    <asp:BoundField HeaderText="Sobran" DataField="Sobran_C_15" ItemStyle-Width="90px"/> 
                                     
                                    <asp:BoundField HeaderText="Sobran" DataField="Sobran_ES_5" ItemStyle-Width="90px"/>
                                    <asp:BoundField HeaderText="Sobran" DataField="Sobran_ES_16" ItemStyle-Width="90px"/> 
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
                    <asp:AsyncPostBackTrigger ControlID="cmbEjecutivo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbSupervisor" EventName="SelectedIndexChanged" />
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