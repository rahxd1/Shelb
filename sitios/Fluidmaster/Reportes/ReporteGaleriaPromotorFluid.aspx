<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Fluidmaster/Fluidmaster.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteGaleriaPromotorFluid.aspx.vb" 
    Inherits="procomlcd.ReporteGaleriaPromotorFluid"
    Title="Fluidmaster - Galería" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoFluid" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
            Galería fotografica</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>
                    <div id="menu-regresar">
                        <img alt="" src="../../../Img/arrow.gif" />
                        <asp:HyperLink ID="lnkRegresar" runat="server" 
                            NavigateUrl="~/sitios/Fluidmaster/Reportes/ReportesFluid.aspx">Regresar</asp:HyperLink><br />
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
                        
                    <asp:Label ID="lblPromotor" runat="server" Text="Promotor" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPromotor" runat="server" CssClass="cmb"
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblDistribuidor" runat="server" Text="Distribuidor" CssClass="lbl" />
                    <asp:DropDownList ID="cmbDistribuidor" runat="server" CssClass="cmb"
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblTienda" runat="server" Text="Tienda" CssClass="lbl" />
                    <asp:DropDownList ID="cmbTienda" runat="server" CssClass="cmb"
                        AutoPostBack="True" /><br />

                    <asp:UpdateProgress ID="UpdateProgress" runat="server" 
                        AssociatedUpdatePanelID="updPnl">
                        <ProgressTemplate>
                            <div id="pnlSave" >
                                <img alt="Cargando..." src="../../../Img/loading.gif" /> 
                                Cargando información.
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                    <br />
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                                CssClass="panel-grid">
                    <asp:GridView ID="gridImagenes" runat="server" AutoGenerateColumns ="False" 
                            Width="130%" GridLines="Horizontal" ShowHeader="False" BorderStyle="None">
                        <RowStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" VerticalAlign="Top" />
                             <Columns>
                                 <%--<asp:ImageField DataImageUrlField="foto"
                                    DataImageUrlFormatString="/ARCHIVOS/CLIENTES/BEROL/NR/IMAGENES/ANAQUEL/PROMOTOR/{0}">
                                    <ControlStyle Height="100px" Width="100px" />
                                 </asp:ImageField>--%>
                                 <asp:TemplateField>
                                    <ItemTemplate>
                                        <br /><i><%#Eval("nombre")%></i>
                                        <br />(<i><%#Eval("nombre_cadena")%></i>)
                                        <br />
                                        
                                        <br /><font color="blue"><%#Eval("ciudad")%>, <%#Eval("nombre_estado")%></font>
                                        <br /><i><font color="red"><%#Eval("nombre_region")%></font> </i>
                                        <br />    
                                        <br /><font size=1>Promotor: <%#Eval("id_usuario")%></font>
                                    </ItemTemplate>
                                    <ItemStyle Width="2500px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="descarga" runat="server" target="_blank"
                                            NavigateUrl='<%# Eval("foto1", "/ARCHIVOS/CLIENTES/FLUIDMASTER/IMAGENES/{0}") %>'>
                                            <img src="<%#Eval("foto1", "/ARCHIVOS/CLIENTES/FLUIDMASTER/IMAGENES/{0}")%>" alt="" width="100px" style="border-width:0px;" />
                                        </asp:HyperLink>
                                        <br /><%#Eval("descripcion_fotografia_1")%></i>
                                        <br /><%#Eval("descripcion_1")%></i>
                                    </ItemTemplate>
                                    <ItemStyle Width="1500px" />
                                </asp:TemplateField>
                                
                                 <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="descarga" runat="server" target="_blank"
                                            NavigateUrl='<%# Eval("foto2", "/ARCHIVOS/CLIENTES/FLUIDMASTER/IMAGENES/{0}") %>'>
                                            <img src="<%#Eval("foto2", "/ARCHIVOS/CLIENTES/FLUIDMASTER/IMAGENES/{0}")%>" alt="" width="100px" style="border-width:0px;" />
                                        </asp:HyperLink>
                                        <br /><%#Eval("descripcion_fotografia_2")%></i>
                                        <br /><%#Eval("descripcion_2")%></i>
                                    </ItemTemplate>
                                    <ItemStyle Width="1500px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="descarga" runat="server" target="_blank"
                                            NavigateUrl='<%# Eval("foto3", "/ARCHIVOS/CLIENTES/FLUIDMASTER/IMAGENES/{0}") %>'>
                                            <img src="<%#Eval("foto3", "/ARCHIVOS/CLIENTES/FLUIDMASTER/IMAGENES/{0}")%>" alt="" width="100px" style="border-width:0px;" />
                                        </asp:HyperLink>
                                        <br /><%#Eval("descripcion_fotografia_3")%></i>
                                        <br /><%#Eval("descripcion_3")%></i>
                                    </ItemTemplate>
                                    <ItemStyle Width="1500px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="descarga" runat="server" target="_blank"
                                            NavigateUrl='<%# Eval("foto4", "/ARCHIVOS/CLIENTES/FLUIDMASTER/IMAGENES/{0}") %>'>
                                            <img src="<%#Eval("foto4", "/ARCHIVOS/CLIENTES/FLUIDMASTER/IMAGENES/{0}")%>" alt="" width="100px" style="border-width:0px;" />
                                        </asp:HyperLink>
                                        <br /><%#Eval("descripcion_fotografia_4")%></i>
                                        <br /><%#Eval("descripcion_4")%></i>
                                    </ItemTemplate>
                                    <ItemStyle Width="1500px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="descarga" runat="server" target="_blank"
                                            NavigateUrl='<%# Eval("foto5", "/ARCHIVOS/CLIENTES/FLUIDMASTER/IMAGENES/{0}") %>'>
                                            <img src="<%#Eval("foto5", "/ARCHIVOS/CLIENTES/FLUIDMASTER/IMAGENES/{0}")%>" alt="" width="100px" style="border-width:0px;" />
                                        </asp:HyperLink>
                                        <br /><%#Eval("descripcion_fotografia_5")%></i>
                                        <br /><%#Eval("descripcion_5")%></i>
                                    </ItemTemplate>
                                    <ItemStyle Width="1500px" />
                                </asp:TemplateField>
                                
                             </Columns>
                             <FooterStyle CssClass="grid-footer" />
                        <FooterStyle 
                            HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbEstado" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbPromotor" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbDistribuidor" EventName="SelectedIndexChanged" />                    
                    <asp:AsyncPostBackTrigger ControlID="cmbTienda" EventName="SelectedIndexChanged" />                    
                </Triggers>
                
            </asp:UpdatePanel>
        </div>

        <!-- END MAIN COLUMN -->
        <!--CONTENT SIDE COLUMN-->
        
        <div class="clear"></div>
    </div>
</asp:Content>