<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Mayoreo/MayoreoMars.Master"
    AutoEventWireup="false" 
    CodeBehind="ReportePreciosCadenas4fantasticosMarsVP.aspx.vb" 
    Inherits="procomlcd.ReportePreciosCadenas4fantasticosMarsVP"
    title="Mars Verificadores de Precio Mayoreo - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoMayoreoMars" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
            Reporte precios 4 fantasticos por cadena</div>

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
                            NavigateUrl="~/sitios/Mars/Conveniencia/Reportes/ReportesMarsConv.aspx">Regresar</asp:HyperLink><br />
                    </div>
                    
                    <asp:Label ID="lblPeriodoA" runat="server" Text="Periodo" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPeriodoA" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                    
                    <asp:Label ID="lblPeriodoB" runat="server" Text="al Periodo" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPeriodoB" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />    
                        
                    <asp:Label ID="lblQuincena" runat="server" Text="Quincena" CssClass="lbl" />
                    <asp:DropDownList ID="cmbQuincena" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblPeriodo2A" runat="server" Text="Periodo" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPeriodo2A" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                    
                    <asp:Label ID="lblPeriodo2B" runat="server" Text="al Periodo" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPeriodo2B" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />      
                        
                    <asp:Label ID="lblSemana" runat="server" Text="Semana" CssClass="lbl" />
                    <asp:DropDownList ID="cmbSemana" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />      
                        
                    <asp:Label ID="lblRegion" runat="server" Text="Región" CssClass="lbl" />
                    <asp:DropDownList ID="cmbRegion" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblPromotor" runat="server" Text="Promotor" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPromotor" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblCadena" runat="server" Text="Cadena" CssClass="lbl" />
                    <asp:DropDownList ID="cmbCadena" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblTienda" runat="server" Text="Tienda" CssClass="lbl" />
                    <asp:DropDownList ID="cmbTienda" runat="server" CssClass="cmb" 
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
                        <asp:GridView ID="gridDetalle1" runat="server" AutoGenerateColumns="False" 
                            Width="537px" CssClass="grid-view" Caption="Banda Precios Mostrador">
                                <Columns>  
                                    <asp:BoundField HeaderText="PAL ® PERRO 1/25 KG" DataFormatString="{0:c2}" DataField="1"/>
                                    <asp:BoundField HeaderText="" DataFormatString="{0:c2}" DataField="11"/>
                                    <asp:BoundField HeaderText="PEDIGREE ® CACHORRO 1/20 KG" DataFormatString="{0:c2}" DataField="2"/>
                                    <asp:BoundField HeaderText="" DataFormatString="{0:c2}" DataField="12"/>
                                    <asp:BoundField HeaderText="PEDIGREE ® ADULTO NUTRICION COMPLETA 1/25 KG" DataFormatString="{0:c2}" DataField="5"/>
                                    <asp:BoundField HeaderText="" DataFormatString="{0:c2}" DataField="15"/>
                                    <asp:BoundField HeaderText="WHISKAS ® RECETA ORIGINAL 1/20 KG" DataFormatString="{0:c2}" DataField="7"/> 
                                    <asp:BoundField HeaderText="" DataFormatString="{0:c2}" DataField="17"/>
                                </Columns>
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        
                        <br />
                        <asp:GridView ID="gridDetalle2" runat="server" AutoGenerateColumns="False" 
                            Width="537px" CssClass="grid-view" Caption="Banda Precios Autoservicio">
                                <Columns>  
                                    <asp:BoundField HeaderText="PAL ® PERRO 1/25 KG" DataField="1" 
                                        DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="" DataField="11" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="PEDIGREE ® CACHORRO 1/20 KG" DataField="2" 
                                        DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="" DataFormatString="{0:c2}" DataField="12"/>
                                    <asp:BoundField HeaderText="PEDIGREE ® ADULTO NUTRICION COMPLETA 1/25 KG" 
                                        DataFormatString="{0:c2}" DataField="5"/>
                                    <asp:BoundField HeaderText="" DataFormatString="{0:c2}" DataField="15"/>
                                    <asp:BoundField HeaderText="WHISKAS ® RECETA ORIGINAL 1/20 KG" 
                                        DataFormatString="{0:c2}" DataField="7"/>
                                    <asp:BoundField HeaderText="" DataFormatString="{0:c2}" DataField="17"/>
                                </Columns>
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <br />
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" 
                            Width="537px" ShowFooter="True" CssClass="grid-view">
                                <Columns>  
                                    <asp:BoundField HeaderText="TOP/RC" DataField="top_rc"/> 
                                    <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/> 
                                    <asp:BoundField HeaderText="Tipo Tienda" DataField="TipoTienda"/> 
                                    <asp:BoundField HeaderText="Precio" DataFormatString="{0:c2}" 
                                        DataField="precio1"/>
                                    <asp:BoundField HeaderText="PAL ® PERRO 1/25 KG" DataFormatString="{0:c2}" 
                                        DataField="producto1"/> 
                                    <asp:BoundField HeaderText="Precio" DataFormatString="{0:c2}" 
                                        DataField="precio2"/>
                                    <asp:BoundField HeaderText="PEDIGREE ® CACHORRO 1/20 KG" 
                                        DataFormatString="{0:c2}" DataField="producto2"/> 
                                    <asp:BoundField HeaderText="Precio" DataFormatString="{0:c2}" 
                                        DataField="precio3"/>
                                    <asp:BoundField HeaderText="PEDIGREE ® ADULTO NUTRICION COMPLETA 1/25 KG" 
                                        DataFormatString="{0:c2}" DataField="producto3"/> 
                                    <asp:BoundField HeaderText="Precio" DataFormatString="{0:c2}" 
                                        DataField="precio4"/>
                                    <asp:BoundField HeaderText="WHISKAS ® RECETA ORIGINAL 1/20 KG" 
                                        DataFormatString="{0:c2}" DataField="producto4"/> 
                                    
                                    <asp:BoundField HeaderText="PAL ® PERRO 1/25 KG" DataField="Estatus1"/> 
                                    <asp:BoundField HeaderText="PEDIGREE ® CACHORRO 1/20 KG" DataField="Estatus2"/> 
                                    <asp:BoundField HeaderText="PEDIGREE ® ADULTO NUTRICION COMPLETA 1/25 KG" 
                                        DataField="Estatus3"/> 
                                    <asp:BoundField HeaderText="WHISKAS ® RECETA ORIGINAL 1/20 KG" 
                                        DataField="Estatus4"/> 
                                </Columns>
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodoA" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodoB" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbQuincena" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbSemana" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbPromotor" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbCadena" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbTienda" EventName="SelectedIndexChanged" />
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