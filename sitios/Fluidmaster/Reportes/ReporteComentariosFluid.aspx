﻿<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Fluidmaster/Fluidmaster.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteComentariosFluid.aspx.vb" 
    Inherits="procomlcd.ReporteComentariosFluid"
    title="Fluidmaster - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoFluid" runat="Server">

<!--titulo-pagina-->
    <div id="titulo-pagina">Reporte comentarios</div>

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
                        
                    <asp:Label ID="lblTipoComentario" runat="server" Text="Tipo comentario" CssClass="lbl" />
                    <asp:DropDownList ID="cmbTipoComentario" runat="server" CssClass="cmb"
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
                    
                    <asp:Panel ID="PanelFS" runat="server" />
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" Width="645px" 
                                    ShowFooter="True" CssClass="grid-view">
                                <Columns>  
                                    <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                    <asp:BoundField HeaderText="Ciudad" DataField="ciudad"/> 
                                    <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/> 
                                    <asp:BoundField HeaderText="Distribuidor" DataField="nombre_cadena"/>  
                                    <asp:BoundField HeaderText="Tipo tienda" DataField="tipo_tienda"/>  
                                    <asp:BoundField HeaderText="Tienda" DataField="nombre"/> 
                                    <asp:BoundField HeaderText="Tipo comentario" DataField="descripcion_comentario"/>
                                    <asp:BoundField HeaderText="Comentario" DataField="comentarios" ItemStyle-Width="1650px"/>  
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
                    <asp:AsyncPostBackTrigger ControlID="cmbEstado" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbPromotor" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbDistribuidor" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbTipoComentario" EventName="SelectedIndexChanged" />
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