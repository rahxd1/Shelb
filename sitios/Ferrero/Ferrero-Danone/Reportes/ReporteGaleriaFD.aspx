<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Ferrero/Ferrero-Danone/FerreroDanone.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteGaleriaFD.aspx.vb" 
    Inherits="procomlcd.ReporteGaleriaFD"
    Title="Ferrero Danone - Galería" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentFerreroDanone" runat="Server">

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
                        
                    <asp:Label ID="lblColonia" runat="server" Text="Colonia" CssClass="lbl" />
                    <asp:DropDownList ID="cmbColonia" runat="server" CssClass="cmb"
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
                    
                    <asp:GridView ID="gridImagenes" runat="server" Width="100%" AutoGenerateColumns="False" 
                        ShowFooter="True" CssClass="grid-view">
                        <RowStyle BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" VerticalAlign="Top" />
                         <Columns>
                             <asp:TemplateField>
                                <ItemTemplate>
                                    <img src="<%#Eval("ruta")%><%#Eval("foto")%>" height="300" width="250" alt="Foto"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="top" Width="0%" />
                             </asp:TemplateField>
                             <asp:TemplateField>
                                <ItemTemplate>
                                    <br />Periodo: <i><%#Eval("nombre_periodo")%></i>
                                    <br />Región: <i><%#Eval("nombre_region")%></i>
                                    <br />Colonia: <i><%#Eval("nombre_colonia")%></i>
                                    <br />Tienda: <i><%#Eval("nombre_tienda")%></i>
                                    <br />
                                    <br />Descripción: <b><font color="black"><%#Eval("descripcion")%></font></b>
                                    <br />
                                    <br />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="top" Width="90%" />
                            </asp:TemplateField>
                         </Columns>
                         <FooterStyle CssClass="grid-footer" />
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:GridView>
                </ContentTemplate>
                 <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbColonia" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbTienda" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

        <!-- END MAIN COLUMN -->
        <!--CONTENT SIDE 2 COLUMN-->
        
        <div class="clear"></div>
    </div>
    </asp:Content>