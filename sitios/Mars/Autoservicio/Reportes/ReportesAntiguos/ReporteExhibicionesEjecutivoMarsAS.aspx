﻿<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Autoservicio/Mars_Autoservicio.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteExhibicionesEjecutivoMarsAS.aspx.vb" 
    Inherits="procomlcd.ReporteExhibicionesEjecutivoMarsAS"
    title="Mars Autoservicio - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsAutoservicio" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte exhibiciones por ejecutivo</div>

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
                    
                    <asp:Panel ID="PanelFS" runat="server" />
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" Width="100%" 
                                    ShowFooter="True" CssClass="grid-view" DataKeyNames="region_mars" >
                                <Columns>
                                    <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                    <asp:TemplateField HeaderText="Ejecutivo">
                                        <ItemTemplate>
                                            <a href="ReporteDetalleExhibicionesEjecutivoMarsAS.aspx?id_periodo=<%#Eval("id_periodo")%>&region_mars=<%#Eval("region_mars")%>"><%#Eval("EjecutivoMars")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Tiendas" DataField="Tiendas"/> 
                                    <asp:BoundField HeaderText="con pauta" DataField="Pautas"/> 
                                    <asp:BoundField HeaderText="venta ideal" DataField="PVI"/> 
                                    <asp:BoundField HeaderText="% Porcentaje" DataField="Porcentaje"/>  
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
            </asp:UpdatePanel>
        </div>
        
        <!--CONTENT SIDE COLUMN-->
        <div id="content-side-two-column">
            <asp:LinkButton ID="lnkExportar" runat="server" Font-Bold="True">Exportar a Excel</asp:LinkButton>
        </div>
        <div class="clear"></div>
    </div>
</asp:Content>