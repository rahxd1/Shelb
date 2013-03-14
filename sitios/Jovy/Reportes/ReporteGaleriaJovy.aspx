<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Jovy/Jovy.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteGaleriaJovy.aspx.vb" 
    Inherits="procomlcd.ReporteGaleriaJovy"
    Title="Jovy - Galería" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderJovy" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
            Galería de imagenes</div>

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
                            NavigateUrl="~/sitios/Jovy/Reportes/ReportesJovy.aspx">Regresar</asp:HyperLink><br />
                    </div>
                    
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="cmb" 
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
                                <img alt="Cargando..." src="../../../Img/loading.gif" /> 
                                Cargando información.
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                    <br />
                    <asp:GridView ID="gridImagenes" runat="server" Width="100%" AutoGenerateColumns="False" 
                        ShowFooter="True" CssClass="grid-view" BorderWidth="0px">
                        <RowStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" VerticalAlign="Top" />
                             <Columns>
                                 <asp:TemplateField>
                                    <ItemTemplate>
                                        <img src="<%#Eval("ruta")%><%#Eval("foto")%>" width="250" alt="<%#Eval("ruta")%><%#Eval("foto")%>"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="top" Width="0%" />
                                 </asp:TemplateField>
                                 <asp:TemplateField>
                                    <ItemTemplate>
                                        <br />Periodo: <i><%#Eval("nombre_periodo")%></i>
                                        <br />Plaza: <i><%#Eval("nombre_region")%></i>
                                        <br />Cadena: <i><%#Eval("nombre_cadena")%></i>
                                        <br />Tienda: <i><%#Eval("nombre")%></i>
                                        <br />
                                        <br />Descripción: <b><font color="black"><%#Eval("descripcion")%></font></b>
                                        <br />
                                        <br />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="top" Width="90%" />
                                </asp:TemplateField>
                             </Columns>
                             <FooterStyle CssClass="grid-footer" />
                        <FooterStyle 
                            HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbPromotor" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbCadena" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbTienda" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

        <!-- END MAIN COLUMN -->
        <!--CONTENT SIDE 2 COLUMN-->
        
        <div class="clear"></div>
    </div>
</asp:Content>