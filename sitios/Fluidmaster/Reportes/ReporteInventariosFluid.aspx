﻿<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Fluidmaster/Fluidmaster.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteInventariosFluid.aspx.vb" 
    Inherits="procomlcd.ReporteInventariosFluid"
    title="Fluidmaster - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoFluid" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte inventarios</div>

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
                    
                    <asp:Panel ID="PanelFS" runat="server" />
                    
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridResumen" runat="server" AutoGenerateColumns="False" 
                            CssClass="grid-view" ShowFooter="True" Width="407px">
                            <Columns>
                                <asp:BoundField DataField="codigo" HeaderText="Código" />
                                <asp:BoundField DataField="nombre_producto" HeaderText="Producto" />
                                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>
                                    Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <br />
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" Width="645px" 
                                    ShowFooter="True" CssClass="grid-view">
                                <Columns>  
                                    <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                    <asp:BoundField HeaderText="Ciudad" DataField="ciudad"/> 
                                    <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/> 
                                    <asp:BoundField HeaderText="Distribuidor" DataField="nombre_cadena"/>  
                                    <asp:BoundField HeaderText="Tienda" DataField="nombre"/> 
                                    <asp:BoundField HeaderText="200AM133" DataField="1" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="200AM133" DataField="101" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="200AM133" DataField="1001" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="200CM135" DataField="2" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="200CM135" DataField="102" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="200CM135" DataField="1002" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="200AK133" DataField="3" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="200AK133" DataField="103" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="200AK133" DataField="1003" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="400A133" DataField="4" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="400A133" DataField="104" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="400A133" DataField="1004" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="400LS133" DataField="5" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="400LS133" DataField="105" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="400LS133" DataField="1005" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="500135" DataField="6" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="500135" DataField="106" ItemStyle-Width="70px"/>
                                    <asp:BoundField HeaderText="500135" DataField="1006" ItemStyle-Width="70px"/>  
                                    <asp:BoundField HeaderText="501135" DataField="7" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="501135" DataField="107" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="501135" DataField="1007" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="502135" DataField="8" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="502135" DataField="108" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="502135" DataField="1008" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="555C135" DataField="9" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="555C135" DataField="109" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="555C135" DataField="1009" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="507A133" DataField="10" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="507A133" DataField="110" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="507A133" DataField="1010" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="681135" DataField="11" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="681135" DataField="111" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="681135" DataField="1011" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="242135" DataField="12" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="242135" DataField="112" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="242135" DataField="1012" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="400LSR133" DataField="13" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="400LSR133" DataField="113" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="400LSR133" DataField="1013" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="400LSCR133" DataField="14" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="400LSCR133" DataField="114" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="400LSCR133" DataField="1014" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="400LSCLR133" DataField="15" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="400LSCLR133" DataField="115" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="400LSCLR133" DataField="1015" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="503" DataField="16" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="503" DataField="116" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="503" DataField="1016" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="747UK" DataField="17" ItemStyle-Width="70px"/>  
                                    <asp:BoundField HeaderText="747UK" DataField="117" ItemStyle-Width="70px"/> 
                                    <asp:BoundField HeaderText="747UK" DataField="1017" ItemStyle-Width="70px"/> 
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
                    <asp:AsyncPostBackTrigger ControlID="cmbTienda" EventName="SelectedIndexChanged" />                    
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