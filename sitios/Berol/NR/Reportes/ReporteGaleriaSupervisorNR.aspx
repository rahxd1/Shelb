<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Berol/NR/Rubbermaid.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteGaleriaSupervisorNR.aspx.vb" 
    Inherits="procomlcd.ReporteGaleriaSupervisorNR"
    Title="Newell Rubbermaid - Galer�a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoNR" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
            Galer�a fotografica</div>

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
                            
                    <asp:Label ID="lblRegion" runat="server" Text="Regi�n" CssClass="lbl" />
                    <asp:DropDownList ID="cmbRegion" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                    
                    <asp:Label ID="lblEstado" runat="server" Text="Estado" CssClass="lbl" />
                    <asp:DropDownList ID="cmbEstado" runat="server" CssClass="cmb" 
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
                                Cargando informaci�n.
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both" CssClass="panel-grid">
                    <asp:GridView ID="gridImagenes" runat="server" Width="100%" AutoGenerateColumns="False" 
                        ShowFooter="True" CssClass="grid-view" BorderWidth="0px">
                        <RowStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" VerticalAlign="Top" />
                             <Columns>
                                 <asp:TemplateField>
                                    <ItemTemplate>
                                        <img src="<%#Eval("ruta")%><%#Eval("foto")%>" width="100" alt="<%#Eval("ruta")%><%#Eval("foto")%>"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="top" Width="0%" />
                                 </asp:TemplateField>
                                 <asp:TemplateField>
                                    <ItemTemplate>
                                        <br />Regi�n: <i><%#Eval("nombre_region")%></i>
                                        <br />Cadena: <i><%#Eval("nombre_cadena")%></i>
                                        <br />Formato: <i><%#Eval("nombre_formato")%></i>
                                        <br />Tienda: <i><%#Eval("nombre")%></i>
                                        <br /><%#Eval("ciudad")%>, <%#Eval("nombre_estado")%>
                                        <br />
                                        <br />Promotor: <%#Eval("id_usuario")%>
                                        <br />
                                        <br /> <%#Eval("descripcion")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="top" Width="90%" />
                                </asp:TemplateField>
                             </Columns>
                             <FooterStyle CssClass="grid-footer" />
                        <FooterStyle 
                            HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>

        <!-- END MAIN COLUMN -->
        <!--CONTENT SIDE 2 COLUMN-->
        
        <div class="clear"></div>
    </div>
    </asp:Content>