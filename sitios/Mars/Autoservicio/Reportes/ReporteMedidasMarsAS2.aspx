﻿<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Autoservicio/Mars_Autoservicio.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteMedidasMarsAS2.aspx.vb" 
    Inherits="procomlcd.ReporteMedidasMarsAS2"
    title="Mars Autoservicio - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsAutoservicio" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte medidas anaquel</div>

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
                            NavigateUrl="~/sitios/Mars/Autoservicio/Reportes/ReportesMarsAS.aspx">Regresar</asp:HyperLink><br />
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
                        
                    <asp:Label ID="lblTienda" runat="server" Text="Tienda" CssClass="lbl" />
                    <asp:DropDownList ID="cmbTienda" runat="server" CssClass="cmb" 
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
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" Width="645px" 
                                    ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:BoundField HeaderText="Ruta sap" DataField="ejecutivo"/>  
                                    <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                    <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/> 
                                    <asp:BoundField HeaderText="Formato" DataField="nombre_formato"/> 
                                    <asp:BoundField HeaderText="Log in" DataField="id_usuario"/> 
                                    <asp:BoundField HeaderText="Sap" DataField="codigo"/> 
                                    <asp:BoundField HeaderText="Tienda" DataField="nombre"/> 
                                    <asp:BoundField HeaderText="Clasificacion" DataField="clasificacion_tienda"/>
                                    <asp:BoundField HeaderText="Ciudad" DataField="ciudad"/>
                                    <asp:BoundField HeaderText="Estado" DataField="nombre_estado"/>
                                    <asp:BoundField HeaderText="Frecuencia" DataField="frecuencia"/>  
                                    <asp:BoundField HeaderText="Tiempo visita" DataField="nombre_visita"/> 
                                    <asp:BoundField HeaderText="Medida anaquel" DataField="tipo_medida"/> 
                                    <asp:BoundField HeaderText="Domingo" DataField="W1_1" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" /> 
                                    <asp:BoundField HeaderText="Lunes" DataField="W1_2" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Martes" DataField="W1_3" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Miércoles" DataField="W1_4" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Jueves" DataField="W1_5" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Viernes" DataField="W1_6" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Sábado" DataField="W1_7" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Domingo" DataField="W2_1" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" /> 
                                    <asp:BoundField HeaderText="Lunes" DataField="W2_2" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Martes" DataField="W2_3" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Miércoles" DataField="W2_4" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Jueves" DataField="W2_5" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Viernes" DataField="W2_6" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Sábado" DataField="W2_7" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Domingo" DataField="W3_1" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" /> 
                                    <asp:BoundField HeaderText="Lunes" DataField="W3_2" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Martes" DataField="W3_3" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Miércoles" DataField="W3_4" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Jueves" DataField="W3_5" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Viernes" DataField="W3_6" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Sábado" DataField="W3_7" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Domingo" DataField="W4_1" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" /> 
                                    <asp:BoundField HeaderText="Lunes" DataField="W4_2" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Martes" DataField="W4_3" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Miércoles" DataField="W4_4" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Jueves" DataField="W4_5" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Viernes" DataField="W4_6" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Sábado" DataField="W4_7" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
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
                    <asp:AsyncPostBackTrigger ControlID="cmbQuincena" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbEjecutivo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbSupervisor" EventName="SelectedIndexChanged" />
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
        <div class="clear"></div>
    </div>
</asp:Content>