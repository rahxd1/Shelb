<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Herradura/NewMix/NewMix.Master"
    AutoEventWireup="false" 
    CodeBehind="ReportePreciosNM.aspx.vb" 
    Inherits="procomlcd.ReportePreciosNM" 
    Title="New Mix - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderNewMix" runat="Server">

    <!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte precios por cadena</div>

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
                            NavigateUrl="~/sitios/Herradura/NewMix/Reportes/ReportesNewMix.aspx">Regresar</asp:HyperLink><br />
                    </div>
                    
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="cmb" 
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
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both" Height="350px">   
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="false" 
                            Width="100%" ShowFooter="True" CssClass="grid-view">
                            <Columns>
                                <asp:BoundField HeaderText="Region" DataField="nombre_region"/>
                                <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/> 
                                <asp:BoundField HeaderText="JACK DANIEL´S 700" DataField="100" DataFormatString="{0:c2}"/>
                                <asp:BoundField HeaderText="JACK DANIEL´S GINGER 350" DataField="101" DataFormatString="{0:c2}"/>   
                                <asp:BoundField HeaderText="JACK DANIEL´S COLA 350" DataField="102" DataFormatString="{0:c2}"/>
                                <asp:BoundField HeaderText="JACK DANIEL´S AGUA MINERAL" DataField="134" DataFormatString="{0:c2}"/>
                                <asp:BoundField HeaderText="REPOSADO 950" DataField="103" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="REPOSADO 700" DataField="104" DataFormatString="{0:c2}"/>
                                <asp:BoundField HeaderText="REPOSADO 375" DataField="105" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="REPOSADO 200" DataField="106" DataFormatString="{0:c2}"/>
                                <asp:BoundField HeaderText="NEW MIX PALOMA 350" DataField="108" DataFormatString="{0:c2}"/>
                                <asp:BoundField HeaderText="NEW MIX VAMPIRO 350" DataField="109" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="NEW MIX CHARRO NEGRO 350" DataField="110" DataFormatString="{0:c2}"/>
                                <asp:BoundField HeaderText="NEW MIX MARGARITA 350" DataField="111" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="NEW MIX SPICY MANGO 350" DataField="112" DataFormatString="{0:c2}"/>
                                <asp:BoundField HeaderText="NEW MIX PALOMA 2000" DataField="113" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="FINLANDIA FROST LIMÓN" DataField="132" DataFormatString="{0:c2}"/>
                                <asp:BoundField HeaderText="FINLANDIA FROST ARANDANO" DataField="133" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="FINLANDIA FROST MANGO" DataField="135" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="VINO SANTA ELENA" DataField="136" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="RON APPLETON AÑEJO" DataField="137" DataFormatString="{0:c2}"/>
                                <asp:BoundField HeaderText="RON APPLETON BLANCO" DataField="138" DataFormatString="{0:c2}"/> 
                                
                                <asp:BoundField HeaderText="BOONES" DataField="117" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="CABRITO MIX" DataField="118" DataFormatString="{0:c2}"/>
                                <asp:BoundField HeaderText="CARIBE COOLER" DataField="120" DataFormatString="{0:c2}"/>
                                <asp:BoundField HeaderText="CUBARAIMA" DataField="123" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="PRESIDEN COLA" DataField="125" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="SKYY BLUE" DataField="126" DataFormatString="{0:c2}"/>
                                <asp:BoundField HeaderText="VIÑA REAL" DataField="128" DataFormatString="{0:c2}"/>
                                <asp:BoundField HeaderText="KOSACO" DataField="130" DataFormatString="{0:c2}"/>
                                <asp:BoundField HeaderText="VIÑA REAL" DataField="131" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="MOJITO" DataField="139" DataFormatString="{0:c2}"/>
                                <asp:BoundField HeaderText="PARIS DE NOCHE" DataField="140" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="PARIS DE NOCHE 2LTS" DataField="141" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="100 AÑOS" DataField="142" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="MOLOTOV" DataField="145" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="FIXION BLUE" DataField="146" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="C-ICE" DataField="147" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="PERLA NEGRA" DataField="148" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="PZ" DataField="149" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="SUN BEAT" DataField="150" DataFormatString="{0:c2}"/> 
                                <asp:BoundField HeaderText="CITRUS DOUGLAS" DataField="151" DataFormatString="{0:c2}"/> 
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