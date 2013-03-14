<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Berol/NR/Rubbermaid.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteBitacoraNR.aspx.vb" 
    Inherits="procomlcd.ReporteBitacoraNR"
    Title="Newell Rubbermaid - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoNR" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
            Reporte bitácora captura</div>

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
                            
                    <asp:Label ID="lblRegion" runat="server" Text="Región" CssClass="lbl" />
                    <asp:DropDownList ID="cmbRegion" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />

                    <asp:Label ID="lblSupervisor" runat="server" Text="Supervisor" CssClass="lbl" />
                    <asp:DropDownList ID="cmbSupervisor" runat="server" CssClass="cmb" 
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
                    
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both" Height="350px">
                    <asp:GridView ID="gridResumen" runat="server" AutoGenerateColumns="False" 
                        Width="50%" ShowFooter="True" CssClass="grid-view">
                        <Columns>
                            <asp:BoundField HeaderText="Región" DataField="nombre_region"/>
                            <asp:BoundField HeaderText="fotografias" DataField="Fotos"/> 
                            <asp:BoundField HeaderText="frentes y exhibiciones" DataField="Frentes"/>  
                            <asp:BoundField HeaderText="inventarios" DataField="Inventarios"/>
                        </Columns>
                        <FooterStyle CssClass="grid-footer" />
                        <EmptyDataTemplate>
                            <h1>Sin información</h1>
                        </EmptyDataTemplate>
                    </asp:GridView>
                     <br />
                    <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" 
                        Width="100%" ShowFooter="True" CssClass="grid-view">
                        <Columns>
                            <asp:CommandField EditText="Ver detalle" ShowEditButton="True" >
                            <ControlStyle ForeColor="Red" />
                            </asp:CommandField>
                            <asp:BoundField HeaderText="Usuario" DataField="id_usuario"/>
                            <asp:BoundField HeaderText="Tiendas" DataField="Tiendas"/>
                            <asp:BoundField HeaderText="% fotografias" DataField="PorFotos"/> 
                            <asp:BoundField HeaderText="% frentes y exhibiciones" DataField="PorFrentes"/>  
                            <asp:BoundField HeaderText="% inventarios" DataField="PorInventarios"/>
                        </Columns>
                        <FooterStyle CssClass="grid-footer" />
                        <EmptyDataTemplate>
                            <h1>Sin información</h1>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="pnlDetalle" runat="server" Visible="False" BorderColor="Black" 
                        ScrollBars="Both" Height="350px"
                        BorderStyle="Dotted">
                         <a name="DETALLES" style="text-align: center">DETALLES</a>
                         <asp:GridView ID="gridDetalle" runat="server" 
                             AutoGenerateColumns="False" CellPadding="4" ForeColor="Black" 
                             GridLines="Horizontal" ShowFooter="True" Width="100%" BackColor="White" 
                             BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                             <Columns>
                                 <asp:BoundField DataField="id_usuario" HeaderText="Login" />
                                 <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/>
                                 <asp:BoundField DataField="nombre" HeaderText="Tienda" />
                                 <asp:BoundField DataField="estatus_fotos" HeaderText="Fotos" />
                                 <asp:BoundField DataField="estatus_frentes" HeaderText="Frentes" />
                                 <asp:BoundField DataField="estatus_inventarios" HeaderText="Inventarios" />
                             </Columns>
                             <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                             <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                             <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                             <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                         </asp:GridView>
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