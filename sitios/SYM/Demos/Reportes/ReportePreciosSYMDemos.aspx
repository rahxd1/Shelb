<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/SYM/Demos/SYMDemos.Master"
    AutoEventWireup="false" 
    CodeBehind="ReportePreciosSYMDemos.aspx.vb" 
    Inherits="procomlcd.ReportePreciosSYMDemos" 
    Title="SYM Demos - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Contenido_SYMDemos" runat="Server">

    <!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte precios</div>

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
                            NavigateUrl="~/sitios/SYM/Demos/Reportes/ReportesSYMDemos.aspx">Regresar</asp:HyperLink><br />
                    </div>
                    
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />    
                        
                    <asp:Label ID="lblRegion" runat="server" Text="División" CssClass="lbl" />
                    <asp:DropDownList ID="cmbRegion" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblCiudad" runat="server" Text="Ciudad" CssClass="lbl" />
                    <asp:DropDownList ID="cmbCiudad" runat="server" CssClass="cmb" 
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
                                <img alt="Cargando..." src="../../../../Img/loading.gif" /> 
                                Cargando información.
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                    <br /> 
                    <asp:Panel ID="PnlGrid" runat="server" ScrollBars="Both" CssClass="panel-grid" Height="500">
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" 
                            Width="100%" ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                    <asp:BoundField HeaderText="Plaza" DataField="plaza"/> 
                                    <asp:BoundField HeaderText="Supervisor" DataField="id_usuario"/> 
                                    <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/>
                                    <asp:BoundField HeaderText="Tienda" DataField="nombre_tienda"/>
                                    <asp:BoundField HeaderText="Jabón Liquido Manos Dermatológico 221ml" DataField="1" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón Liquido Manos Neutro 221ml" DataField="2" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón Liquido Corporal Dermatológico 400ml" DataField="3" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón Liquido Corporal Neutro 400ml" DataField="4" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador Dermatológico 150grs" DataField="5" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador Neutro 150grs" DataField="6" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador Natura 150grs" DataField="7" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador Cosmético 150 grs" DataField="8" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador Antibacterial 150 grs" DataField="9" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador Dermatológico 100grs" DataField="10" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón Lirio Neutro 100grs" DataField="11" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador Neutro 200grs" DataField="12" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador Dermatológico 200grs" DataField="13" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador Cosmético 200 grs" DataField="14" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador  Lavanda 200grs" DataField="15" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador Natura 200grs" DataField="16" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador Antibacterial 200 grs" DataField="17" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador Neutro 4 pack 600grs" DataField="18" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador Dermatológico 4 Pack 600grs" DataField="19" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador Cosmético 4 Pack 600grs" DataField="20" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador Antibacterial 4 Pack 600grs" DataField="21" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador Mix 4 Pack" DataField="22" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador Natura 4 Pack 600grs" DataField="23" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador Lavanda 4 Pack 600grs" DataField="24" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador Neutro 3 Pack 270grs" DataField="25" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón de Tocador Dermatológico 3 Pack 270grs" DataField="26" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón LaCrem de Karité 125grs" DataField="27" DataFormatString="{0:c2}"/>
                                    <asp:BoundField HeaderText="Jabón LaCrem de Olive 125grs" DataField="28" DataFormatString="{0:c2}"/>
                                </Columns>   
                                <FooterStyle CssClass="grid-footer" />                   
                            <EmptyDataTemplate>
                                <h1>Sin comentarios</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbCiudad" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbPromotor" EventName="SelectedIndexChanged" />
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