<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Mayoreo/MayoreoMars.Master"
    AutoEventWireup="false" 
    CodeBehind="ReportePlazasPromedioMarsMay.aspx.vb" 
    Inherits="procomlcd.ReportePlazasPromedioMarsMay"
    title="Mars Mayoreo - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoMayoreoMars" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte promedio por plazas</div>

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
                            NavigateUrl="~/sitios/Mars/Mayoreo/Reportes/ReportesMayoreoMars.aspx">Regresar</asp:HyperLink><br />
                    </div>
                    
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo" CssClass="lbl" />
                    <asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblQuincena" runat="server" Text="Quincena" CssClass="lbl" />
                    <asp:DropDownList ID="cmbQuincena" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />
                        
                    <asp:Label ID="lblSemana" runat="server" Text="Semana" CssClass="lbl" />
                    <asp:DropDownList ID="cmbSemana" runat="server" CssClass="cmb" 
                        AutoPostBack="True" /><br />         
                        
                    <asp:Label ID="lblRegion" runat="server" Text="Región" CssClass="lbl" />
                    <asp:DropDownList ID="cmbRegion" runat="server" CssClass="cmb" 
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
                <asp:Panel ID="pnlGrid" runat="server">
                    <asp:Panel ID="pnlResumen" runat="server">
                        <asp:GridView ID="gridResumen" runat="server" AutoGenerateColumns="False" Width="645px" 
                                    ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                    <asp:BoundField HeaderText="Plaza" DataField="ciudad"/> 
                                    <asp:BoundField HeaderText="PAL ® PERRO 1/25 KG" DataField="11" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="PEDIGREE ® CACHORRO 1/20 KG" DataField="12" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="PEDIGREE ® ADULTO NUTRICION COMPLETA 1/25 KG" DataField="15" DataFormatString="{0:c2}"/>  
                                    <asp:BoundField HeaderText="WHISKAS ® RECETA ORIGINAL 1/20 KG" DataField="17" DataFormatString="{0:c2}"/> 
                                </Columns>
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                     </asp:Panel>
                     <asp:Panel ID="pnlReporte" runat="server" ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridReporteAutoservicio" Caption="Autoservicio" runat="server" AutoGenerateColumns="False" Width="645px" 
                                    ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                    <asp:BoundField HeaderText="Plaza" DataField="ciudad"/>  
                                    
                                    <asp:BoundField HeaderText="mínimo" DataField="1" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="PAL ® PERRO 1/25 KG" DataField="11" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="máximo" DataField="M_1" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="NutresCan Original Adulto 25 kg" DataField="46" DataFormatString="{0:c2}" 
                                        HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" /> 
                                    <asp:BoundField HeaderText="NutresCan Natural Adulto 25 kg" DataField="47" DataFormatString="{0:c2}" 
                                        HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"/> 
                                    
                                    <asp:BoundField HeaderText="mínimo" DataField="2" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="PEDIGREE ® CACHORRO 1/20 KG" DataField="12" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="máximo" DataField="M_2" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="Dog Chow Cachorro 25 kg" DataField="25" DataFormatString="{0:c2}"   
                                        HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" />
                                    <asp:BoundField HeaderText="NutresCan Cachorro 20 kg" DataField="48" DataFormatString="{0:c2}"
                                        HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"/>
                                    
                                    <asp:BoundField HeaderText="mínimo" DataField="5" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="PEDIGREE ® ADULTO NUTRICION COMPLETA 1/25 KG" DataField="15" DataFormatString="{0:c2}"/>  
                                    <asp:BoundField HeaderText="máximo" DataField="M_5" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="Dog Chow Adulto 25kg" DataField="24" DataFormatString="{0:c2}"
                                        HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" />  
                                    <asp:BoundField HeaderText="Ganador adulto 25kg" DataField="32" DataFormatString="{0:c2}"
                                        HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" />  
                                    <asp:BoundField HeaderText="Ganador Premuim 25Kg" DataField="34" DataFormatString="{0:c2}"
                                        HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"/>  
                                    
                                    <asp:BoundField HeaderText="mínimo" DataField="7" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="WHISKAS ® RECETA ORIGINAL 1/20 KG" DataField="17" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="máximo" DataField="M_7" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="Cat Chow 20kg" DataField="39" DataFormatString="{0:c2}"
                                        HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"/> 
                                    <asp:BoundField HeaderText="Gatina 20kg" DataField="40" DataFormatString="{0:c2}"
                                        HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"/> 
                                </Columns>
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <table style="width: 100%">
                            <tr><td><asp:Panel ID="pnl_1" runat="server" /></td>
                                <td><asp:Panel ID="pnl_2" runat="server" /></td>
                                <td><asp:Panel ID="pnl_3" runat="server" /></td>
                                <td><asp:Panel ID="pnl_4" runat="server" /></td>
                            </tr>
                        </table>
                        <br />
                        <asp:GridView ID="gridReporteMostrador" Caption="Mostrador" runat="server" AutoGenerateColumns="False" Width="645px" 
                                    ShowFooter="True" CssClass="grid-view">
                                <Columns>
                                    <asp:BoundField HeaderText="Región" DataField="nombre_region"/> 
                                    <asp:BoundField HeaderText="Plaza" DataField="ciudad"/>  
                                    
                                    
                                    <asp:BoundField HeaderText="mínimo" DataField="1" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="PAL ® PERRO 1/25 KG" DataField="11" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="máximo" DataField="M_1" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="NutresCan Original Adulto 25 kg" DataField="46" DataFormatString="{0:c2}"
                                        HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"/> 
                                    <asp:BoundField HeaderText="NutresCan Natural Adulto 25 kg" DataField="47" DataFormatString="{0:c2}"
                                        HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"/> 
                                    
                                    <asp:BoundField HeaderText="mínimo" DataField="2" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="PEDIGREE ® CACHORRO 1/20 KG" DataField="12" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="máximo" DataField="M_2" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="Dog Chow Cachorro 25 kg" DataField="25" DataFormatString="{0:c2}"
                                        HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"/>
                                    <asp:BoundField HeaderText="NutresCan Cachorro 20 kg" DataField="48" DataFormatString="{0:c2}"
                                        HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"/>
                                    
                                    <asp:BoundField HeaderText="mínimo" DataField="5" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="PEDIGREE ® ADULTO NUTRICION COMPLETA 1/25 KG" DataField="15" DataFormatString="{0:c2}"/>  
                                    <asp:BoundField HeaderText="máximo" DataField="M_5" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="Dog Chow Adulto 25kg" DataField="24" DataFormatString="{0:c2}"
                                        HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"/>  
                                    <asp:BoundField HeaderText="Ganador adulto 25kg" DataField="32" DataFormatString="{0:c2}"
                                        HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"/>  
                                    <asp:BoundField HeaderText="Ganador Premuim 25Kg" DataField="34" DataFormatString="{0:c2}"
                                        HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"/>  
                                    
                                    <asp:BoundField HeaderText="mínimo" DataField="7" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="WHISKAS ® RECETA ORIGINAL 1/20 KG" DataField="17" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="máximo" DataField="M_7" DataFormatString="{0:c2}"/> 
                                    <asp:BoundField HeaderText="Cat Chow 20kg" DataField="39" DataFormatString="{0:c2}"
                                        HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"/> 
                                    <asp:BoundField HeaderText="Gatina 20kg" DataField="40" DataFormatString="{0:c2}"
                                        HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"/> 
                                </Columns>
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <table style="width: 100%">
                            <tr><td><asp:Panel ID="pnl_5" runat="server" /></td>
                                <td><asp:Panel ID="pnl_6" runat="server" /></td>
                                <td><asp:Panel ID="pnl_7" runat="server" /></td>
                                <td><asp:Panel ID="pnl_8" runat="server" /></td>
                            </tr>
                        </table>
                    </asp:Panel>
                 </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbQuincena" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbSemana" EventName="SelectedIndexChanged" />
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