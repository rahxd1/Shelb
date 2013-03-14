<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Ferrero/Ferrero-Danone/FerreroDanone.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteCaducidadFD.aspx.vb" 
    Inherits="procomlcd.ReporteCaducidadFD" 
    Title="Ferrero- Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentFerreroDanone" runat="Server">

    <!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte fecha caducidad</div>

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
                            NavigateUrl="~/sitios/Ferrero/Ferrero-Danone/Reportes/ReportesFerreroDanone.aspx">Regresar</asp:HyperLink><br />
                    </div>
                    
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                            
                    <asp:Label ID="lblRegion" runat="server" Text="Región" CssClass="lbl" />
                    <asp:DropDownList ID="cmbRegion" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblSupervisor" runat="server" Text="Supervisor" CssClass="lbl" />
                    <asp:DropDownList ID="cmbSupervisor" runat="server" CssClass="cmb"
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblColonia" runat="server" Text="Colonia" CssClass="lbl" />
                    <asp:DropDownList ID="cmbColonia" runat="server" CssClass="cmb"
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblCaducidad" runat="server" Text="Caducidad" CssClass="lbl" />
                    <asp:DropDownList ID="cmbCaducidad" runat="server" CssClass="cmb"
                        AutoPostBack="True">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="1" >en 45 días</asp:ListItem>
                        <asp:ListItem Value="2">menos de 90 días</asp:ListItem>
                        <asp:ListItem Value="3" >mas de 90 días</asp:ListItem></asp:DropDownList>
                    <br />

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
                    <asp:Panel ID="pnlGrid" runat="server" Height="350px" ScrollBars="Both">
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" 
                            Width="100%" ShowFooter="True" CssClass="grid-view">
                            <Columns>
                                <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                <asp:BoundField HeaderText="Supervisor" DataField="id_usuario"/> 
                                <asp:BoundField HeaderText="Colonia" DataField="nombre_colonia"/> 
                                <asp:BoundField HeaderText="Tienda" DataField="nombre_tienda"/> 
                                <asp:BoundField HeaderText="Dirección" DataField="direccion"/> 
                                <asp:BoundField HeaderText="Producto" DataField="nombre_producto"/> 
                                <asp:BoundField HeaderText="Caducidad" DataField="caducidad" DataFormatString="{0:d}"/> 
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
                    <asp:AsyncPostBackTrigger ControlID="cmbSupervisor" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbColinia" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbCaducidad" EventName="SelectedIndexChanged" />
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