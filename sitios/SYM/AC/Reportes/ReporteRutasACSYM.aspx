<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/SYM/AC/SYMAC.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteRutasACSYM.aspx.vb" 
    Inherits="procomlcd.ReporteRutasACSYM" 
    Title="SYM Anaquel y Catalogaci�n - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSYMAC" runat="Server">

    <!--titulo-pagina-->
        <div id="titulo-pagina">
                        Rutas Anaquel y Catalogaci�n</div>

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
                            NavigateUrl="~/sitios/SYM/AC/Reportes/ReportesSYMAC.aspx">Regresar</asp:HyperLink><br />
                    </div>
                        
                    <asp:Label ID="lblRegion" runat="server" Text="Divisi�n" CssClass="lbl" />
                    <asp:DropDownList ID="cmbRegion" runat="server" CssClass="cmb" 
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
                                Cargando informaci�n.
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                    <br />
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both" Height="350px">   
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" 
                            Width="100%" ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:BoundField HeaderText="Divisi�n" DataField="nombre_region"/> 
                                    <asp:BoundField HeaderText="Supervisor" DataField="id_supervisor"/> 
                                    <asp:BoundField HeaderText="Supervisor" DataField="nombre_sup"/> 
                                    <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/> 
                                    <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/> 
                                    <asp:BoundField HeaderText="ID Tienda" DataField="id_tienda"/> 
                                    <asp:BoundField HeaderText="Tienda" DataField="nombre"/> 
                                    <asp:BoundField HeaderText="Ciudad" DataField="ciudad"/> 
                                    <asp:BoundField HeaderText="Estado" DataField="nombre_estado"/> 
                                </Columns>   
                                <FooterStyle CssClass="grid-footer" />                   
                            <EmptyDataTemplate>
                                <h1>Sin informaci�n</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbSupervisor" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbPromotor" EventName="SelectedIndexChanged" />
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