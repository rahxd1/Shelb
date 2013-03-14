<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/SYM/AC/SYMAC.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteShareNuevoACSYM.aspx.vb" 
    Inherits="procomlcd.ReporteShareNuevoACSYM" 
    Title="SYM Anaquel y Catalogación - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSYMAC" runat="Server">

    <!--titulo-pagina-->
        <div id="titulo-pagina">
                        Reporte Share (Nuevo)</div>

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
                            NavigateUrl="~/sitios/SYM/AC/Reportes/ReportesSYMAC.aspx">Regresar</asp:HyperLink><br />
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
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Both" Height="350px" 
                        Visible="False">   
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: center">
                                    NACIONAL</td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:GridView ID="gridReporte1" runat="server" AutoGenerateColumns="False" 
                                        CssClass="grid-view" DataKeyNames="id_marca" ShowFooter="True" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="nombre_marca" HeaderText="Tocador" />
                                            <asp:BoundField DataField="1" HeaderText="Casa ley" />
                                            <asp:BoundField DataField="2" HeaderText="Chedraui" />
                                            <asp:BoundField DataField="3" HeaderText="Comercial mexicana" />
                                            <asp:BoundField DataField="4" HeaderText="ISSTE" />
                                            <asp:BoundField DataField="5" HeaderText="Soriana" />
                                            <asp:BoundField DataField="6" HeaderText="Bodega aurrera" />
                                            <asp:BoundField DataField="7" HeaderText="Walmart" />
                                            <asp:BoundField DataField="100" HeaderText="Total" />
                                        </Columns>
                                        <FooterStyle CssClass="grid-footer" />
                                        <EmptyDataTemplate>
                                            <h1>
                                                Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:GridView ID="gridReporte2" runat="server" AutoGenerateColumns="False" 
                                        CssClass="grid-view" DataKeyNames="id_marca" ShowFooter="True" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="nombre_marca" HeaderText="Detergentes" />
                                            <asp:BoundField DataField="1" HeaderText="Casa ley" />
                                            <asp:BoundField DataField="2" HeaderText="Chedraui" />
                                            <asp:BoundField DataField="3" HeaderText="Comercial mexicana" />
                                            <asp:BoundField DataField="4" HeaderText="ISSTE" />
                                            <asp:BoundField DataField="5" HeaderText="Soriana" />
                                            <asp:BoundField DataField="6" HeaderText="Bodega aurrera" />
                                            <asp:BoundField DataField="7" HeaderText="Walmart" />
                                            <asp:BoundField DataField="100" HeaderText="Total" />
                                        </Columns>
                                        <FooterStyle CssClass="grid-footer" />
                                        <EmptyDataTemplate>
                                            <h1>
                                                Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:GridView ID="gridReporte3" runat="server" AutoGenerateColumns="False" DataKeyNames="id_marca"
                                        Width="100%" ShowFooter="True" CssClass="grid-view">
                                            <Columns>
                                                <asp:BoundField HeaderText="Jabón de lavandería" DataField="nombre_marca"/> 
                                                <asp:BoundField HeaderText="Casa ley" DataField="1"/> 
                                                <asp:BoundField HeaderText="Chedraui" DataField="2"/>
                                                <asp:BoundField HeaderText="Comercial mexicana" DataField="3"/>  
                                                <asp:BoundField HeaderText="ISSTE" DataField="4"/> 
                                                <asp:BoundField HeaderText="Soriana" DataField="5"/>  
                                                <asp:BoundField HeaderText="Bodega aurrera" DataField="6"/>  
                                                <asp:BoundField HeaderText="Walmart" DataField="7"/>  
                                                <asp:BoundField HeaderText="Total" DataField="100"/>  
                                            </Columns>   
                                            <FooterStyle CssClass="grid-footer" />                   
                                        <EmptyDataTemplate>
                                            <h1>Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView>    
                                </td>
                            </tr>
                        </table>
                        
                        <br />
                        <br />
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: center">
                                    CENTRO VALLE</td>
                                 
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:GridView ID="gridReporte1_R1" runat="server" AutoGenerateColumns="False" DataKeyNames="id_marca"
                                        Width="100%" ShowFooter="True" CssClass="grid-view">
                                            <Columns>
                                                <asp:BoundField HeaderText="Tocador" DataField="nombre_marca"/> 
                                                <asp:BoundField HeaderText="Casa ley" DataField="1"/> 
                                                <asp:BoundField HeaderText="Chedraui" DataField="2"/>
                                                <asp:BoundField HeaderText="Comercial mexicana" DataField="3"/>  
                                                <asp:BoundField HeaderText="ISSTE" DataField="4"/> 
                                                <asp:BoundField HeaderText="Soriana" DataField="5"/>  
                                                <asp:BoundField HeaderText="Bodega aurrera" DataField="6"/>  
                                                <asp:BoundField HeaderText="Walmart" DataField="7"/>  
                                                <asp:BoundField HeaderText="Total" DataField="100"/>  
                                            </Columns>   
                                            <FooterStyle CssClass="grid-footer" />                   
                                        <EmptyDataTemplate>
                                            <h1>Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView></td>
                                </tr>
                                <tr>
                                <td style="text-align: center">
                                    <asp:GridView ID="gridReporte2_R1" runat="server" AutoGenerateColumns="False" 
                                        CssClass="grid-view" DataKeyNames="id_marca" ShowFooter="True" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="nombre_marca" HeaderText="Detergentes" />
                                            <asp:BoundField DataField="1" HeaderText="Casa ley" />
                                            <asp:BoundField DataField="2" HeaderText="Chedraui" />
                                            <asp:BoundField DataField="3" HeaderText="Comercial mexicana" />
                                            <asp:BoundField DataField="4" HeaderText="ISSTE" />
                                            <asp:BoundField DataField="5" HeaderText="Soriana" />
                                            <asp:BoundField DataField="6" HeaderText="Bodega aurrera" />
                                            <asp:BoundField DataField="7" HeaderText="Walmart" />
                                            <asp:BoundField DataField="100" HeaderText="Total" />
                                        </Columns>
                                        <FooterStyle CssClass="grid-footer" />
                                        <EmptyDataTemplate>
                                            <h1>
                                                Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:GridView ID="gridReporte3_R1" runat="server" AutoGenerateColumns="False" DataKeyNames="id_marca"
                                        Width="100%" ShowFooter="True" CssClass="grid-view">
                                            <Columns>
                                                <asp:BoundField HeaderText="Jabón de lavandería" DataField="nombre_marca"/> 
                                                <asp:BoundField HeaderText="Casa ley" DataField="1"/> 
                                                <asp:BoundField HeaderText="Chedraui" DataField="2"/>
                                                <asp:BoundField HeaderText="Comercial mexicana" DataField="3"/>  
                                                <asp:BoundField HeaderText="ISSTE" DataField="4"/> 
                                                <asp:BoundField HeaderText="Soriana" DataField="5"/>  
                                                <asp:BoundField HeaderText="Bodega aurrera" DataField="6"/>  
                                                <asp:BoundField HeaderText="Walmart" DataField="7"/>  
                                                <asp:BoundField HeaderText="Total" DataField="100"/>  
                                            </Columns>   
                                            <FooterStyle CssClass="grid-footer" />                   
                                        <EmptyDataTemplate>
                                            <h1>Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView>    
                                </td>
                            </tr>
                        </table>
                        
                        <br />
                        <br />
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: center">
                                    SURESTE</td>
                                 
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:GridView ID="gridReporte1_R2" runat="server" AutoGenerateColumns="False" DataKeyNames="id_marca"
                                        Width="100%" ShowFooter="True" CssClass="grid-view">
                                            <Columns>
                                                <asp:BoundField HeaderText="Tocador" DataField="nombre_marca"/> 
                                                <asp:BoundField HeaderText="Casa ley" DataField="1"/> 
                                                <asp:BoundField HeaderText="Chedraui" DataField="2"/>
                                                <asp:BoundField HeaderText="Comercial mexicana" DataField="3"/>  
                                                <asp:BoundField HeaderText="ISSTE" DataField="4"/> 
                                                <asp:BoundField HeaderText="Soriana" DataField="5"/>  
                                                <asp:BoundField HeaderText="Bodega aurrera" DataField="6"/>  
                                                <asp:BoundField HeaderText="Walmart" DataField="7"/>  
                                                <asp:BoundField HeaderText="Total" DataField="100"/>  
                                            </Columns>   
                                            <FooterStyle CssClass="grid-footer" />                   
                                        <EmptyDataTemplate>
                                            <h1>Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView></td>
                                </tr>
                                <tr>
                                <td style="text-align: center">
                                    <asp:GridView ID="gridReporte2_R2" runat="server" AutoGenerateColumns="False" 
                                        CssClass="grid-view" DataKeyNames="id_marca" ShowFooter="True" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="nombre_marca" HeaderText="Detergentes" />
                                            <asp:BoundField DataField="1" HeaderText="Casa ley" />
                                            <asp:BoundField DataField="2" HeaderText="Chedraui" />
                                            <asp:BoundField DataField="3" HeaderText="Comercial mexicana" />
                                            <asp:BoundField DataField="4" HeaderText="ISSTE" />
                                            <asp:BoundField DataField="5" HeaderText="Soriana" />
                                            <asp:BoundField DataField="6" HeaderText="Bodega aurrera" />
                                            <asp:BoundField DataField="7" HeaderText="Walmart" />
                                            <asp:BoundField DataField="100" HeaderText="Total" />
                                        </Columns>
                                        <FooterStyle CssClass="grid-footer" />
                                        <EmptyDataTemplate>
                                            <h1>
                                                Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:GridView ID="gridReporte3_R2" runat="server" AutoGenerateColumns="False" DataKeyNames="id_marca"
                                        Width="100%" ShowFooter="True" CssClass="grid-view">
                                            <Columns>
                                                <asp:BoundField HeaderText="Jabón de lavandería" DataField="nombre_marca"/> 
                                                <asp:BoundField HeaderText="Casa ley" DataField="1"/> 
                                                <asp:BoundField HeaderText="Chedraui" DataField="2"/>
                                                <asp:BoundField HeaderText="Comercial mexicana" DataField="3"/>  
                                                <asp:BoundField HeaderText="ISSTE" DataField="4"/> 
                                                <asp:BoundField HeaderText="Soriana" DataField="5"/>  
                                                <asp:BoundField HeaderText="Bodega aurrera" DataField="6"/>  
                                                <asp:BoundField HeaderText="Walmart" DataField="7"/>  
                                                <asp:BoundField HeaderText="Total" DataField="100"/>  
                                            </Columns>   
                                            <FooterStyle CssClass="grid-footer" />                   
                                        <EmptyDataTemplate>
                                            <h1>Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView>    
                                </td>
                            </tr>
                        </table>
                        
                        <br />
                        <br />
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: center">
                                    BAJÍO</td>
                                 
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:GridView ID="gridReporte1_R3" runat="server" AutoGenerateColumns="False" DataKeyNames="id_marca"
                                        Width="100%" ShowFooter="True" CssClass="grid-view">
                                            <Columns>
                                                <asp:BoundField HeaderText="Tocador" DataField="nombre_marca"/> 
                                                <asp:BoundField HeaderText="Casa ley" DataField="1"/> 
                                                <asp:BoundField HeaderText="Chedraui" DataField="2"/>
                                                <asp:BoundField HeaderText="Comercial mexicana" DataField="3"/>  
                                                <asp:BoundField HeaderText="ISSTE" DataField="4"/> 
                                                <asp:BoundField HeaderText="Soriana" DataField="5"/>  
                                                <asp:BoundField HeaderText="Bodega aurrera" DataField="6"/>  
                                                <asp:BoundField HeaderText="Walmart" DataField="7"/>  
                                                <asp:BoundField HeaderText="Total" DataField="100"/>  
                                            </Columns>   
                                            <FooterStyle CssClass="grid-footer" />                   
                                        <EmptyDataTemplate>
                                            <h1>Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView></td>
                                </tr>
                                <tr>
                                <td style="text-align: center">
                                    <asp:GridView ID="gridReporte2_R3" runat="server" AutoGenerateColumns="False" 
                                        CssClass="grid-view" DataKeyNames="id_marca" ShowFooter="True" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="nombre_marca" HeaderText="Detergentes" />
                                            <asp:BoundField DataField="1" HeaderText="Casa ley" />
                                            <asp:BoundField DataField="2" HeaderText="Chedraui" />
                                            <asp:BoundField DataField="3" HeaderText="Comercial mexicana" />
                                            <asp:BoundField DataField="4" HeaderText="ISSTE" />
                                            <asp:BoundField DataField="5" HeaderText="Soriana" />
                                            <asp:BoundField DataField="6" HeaderText="Bodega aurrera" />
                                            <asp:BoundField DataField="7" HeaderText="Walmart" />
                                            <asp:BoundField DataField="100" HeaderText="Total" />
                                        </Columns>
                                        <FooterStyle CssClass="grid-footer" />
                                        <EmptyDataTemplate>
                                            <h1>
                                                Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:GridView ID="gridReporte3_R3" runat="server" AutoGenerateColumns="False" DataKeyNames="id_marca"
                                        Width="100%" ShowFooter="True" CssClass="grid-view">
                                            <Columns>
                                                <asp:BoundField HeaderText="Jabón de lavandería" DataField="nombre_marca"/> 
                                                <asp:BoundField HeaderText="Casa ley" DataField="1"/> 
                                                <asp:BoundField HeaderText="Chedraui" DataField="2"/>
                                                <asp:BoundField HeaderText="Comercial mexicana" DataField="3"/>  
                                                <asp:BoundField HeaderText="ISSTE" DataField="4"/> 
                                                <asp:BoundField HeaderText="Soriana" DataField="5"/>  
                                                <asp:BoundField HeaderText="Bodega aurrera" DataField="6"/>  
                                                <asp:BoundField HeaderText="Walmart" DataField="7"/>  
                                                <asp:BoundField HeaderText="Total" DataField="100"/>  
                                            </Columns>   
                                            <FooterStyle CssClass="grid-footer" />                   
                                        <EmptyDataTemplate>
                                            <h1>Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView>    
                                </td>
                            </tr>
                        </table>
                        
                        <br />
                        <br />
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: center">
                                    PACÍFICO</td>
                                 
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:GridView ID="gridReporte1_R4" runat="server" AutoGenerateColumns="False" DataKeyNames="id_marca"
                                        Width="100%" ShowFooter="True" CssClass="grid-view">
                                            <Columns>
                                                <asp:BoundField HeaderText="Tocador" DataField="nombre_marca"/> 
                                                <asp:BoundField HeaderText="Casa ley" DataField="1"/> 
                                                <asp:BoundField HeaderText="Chedraui" DataField="2"/>
                                                <asp:BoundField HeaderText="Comercial mexicana" DataField="3"/>  
                                                <asp:BoundField HeaderText="ISSTE" DataField="4"/> 
                                                <asp:BoundField HeaderText="Soriana" DataField="5"/>  
                                                <asp:BoundField HeaderText="Bodega aurrera" DataField="6"/>  
                                                <asp:BoundField HeaderText="Walmart" DataField="7"/>  
                                                <asp:BoundField HeaderText="Total" DataField="100"/>  
                                            </Columns>   
                                            <FooterStyle CssClass="grid-footer" />                   
                                        <EmptyDataTemplate>
                                            <h1>Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView></td>
                                </tr>
                                <tr>
                                <td style="text-align: center">
                                    <asp:GridView ID="gridReporte2_R4" runat="server" AutoGenerateColumns="False" 
                                        CssClass="grid-view" DataKeyNames="id_marca" ShowFooter="True" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="nombre_marca" HeaderText="Detergentes" />
                                            <asp:BoundField DataField="1" HeaderText="Casa ley" />
                                            <asp:BoundField DataField="2" HeaderText="Chedraui" />
                                            <asp:BoundField DataField="3" HeaderText="Comercial mexicana" />
                                            <asp:BoundField DataField="4" HeaderText="ISSTE" />
                                            <asp:BoundField DataField="5" HeaderText="Soriana" />
                                            <asp:BoundField DataField="6" HeaderText="Bodega aurrera" />
                                            <asp:BoundField DataField="7" HeaderText="Walmart" />
                                            <asp:BoundField DataField="100" HeaderText="Total" />
                                        </Columns>
                                        <FooterStyle CssClass="grid-footer" />
                                        <EmptyDataTemplate>
                                            <h1>
                                                Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:GridView ID="gridReporte3_R4" runat="server" AutoGenerateColumns="False" DataKeyNames="id_marca"
                                        Width="100%" ShowFooter="True" CssClass="grid-view">
                                            <Columns>
                                                <asp:BoundField HeaderText="Jabón de lavandería" DataField="nombre_marca"/> 
                                                <asp:BoundField HeaderText="Casa ley" DataField="1"/> 
                                                <asp:BoundField HeaderText="Chedraui" DataField="2"/>
                                                <asp:BoundField HeaderText="Comercial mexicana" DataField="3"/>  
                                                <asp:BoundField HeaderText="ISSTE" DataField="4"/> 
                                                <asp:BoundField HeaderText="Soriana" DataField="5"/>  
                                                <asp:BoundField HeaderText="Bodega aurrera" DataField="6"/>  
                                                <asp:BoundField HeaderText="Walmart" DataField="7"/>  
                                                <asp:BoundField HeaderText="Total" DataField="100"/>  
                                            </Columns>   
                                            <FooterStyle CssClass="grid-footer" />                   
                                        <EmptyDataTemplate>
                                            <h1>Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView>    
                                </td>
                            </tr>
                        </table>
                        
                        <br />
                        <br />
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: center">
                                    NORTE</td>
                                 
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:GridView ID="gridReporte1_R5" runat="server" AutoGenerateColumns="False" DataKeyNames="id_marca"
                                        Width="100%" ShowFooter="True" CssClass="grid-view">
                                            <Columns>
                                                <asp:BoundField HeaderText="Tocador" DataField="nombre_marca"/> 
                                                <asp:BoundField HeaderText="Casa ley" DataField="1"/> 
                                                <asp:BoundField HeaderText="Chedraui" DataField="2"/>
                                                <asp:BoundField HeaderText="Comercial mexicana" DataField="3"/>  
                                                <asp:BoundField HeaderText="ISSTE" DataField="4"/> 
                                                <asp:BoundField HeaderText="Soriana" DataField="5"/>  
                                                <asp:BoundField HeaderText="Bodega aurrera" DataField="6"/>  
                                                <asp:BoundField HeaderText="Walmart" DataField="7"/>  
                                                <asp:BoundField HeaderText="Total" DataField="100"/>  
                                            </Columns>   
                                            <FooterStyle CssClass="grid-footer" />                   
                                        <EmptyDataTemplate>
                                            <h1>Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView></td>
                                </tr>
                                <tr>
                                <td style="text-align: center">
                                    <asp:GridView ID="gridReporte2_R5" runat="server" AutoGenerateColumns="False" 
                                        CssClass="grid-view" DataKeyNames="id_marca" ShowFooter="True" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="nombre_marca" HeaderText="Detergentes" />
                                            <asp:BoundField DataField="1" HeaderText="Casa ley" />
                                            <asp:BoundField DataField="2" HeaderText="Chedraui" />
                                            <asp:BoundField DataField="3" HeaderText="Comercial mexicana" />
                                            <asp:BoundField DataField="4" HeaderText="ISSTE" />
                                            <asp:BoundField DataField="5" HeaderText="Soriana" />
                                            <asp:BoundField DataField="6" HeaderText="Bodega aurrera" />
                                            <asp:BoundField DataField="7" HeaderText="Walmart" />
                                            <asp:BoundField DataField="100" HeaderText="Total" />
                                        </Columns>
                                        <FooterStyle CssClass="grid-footer" />
                                        <EmptyDataTemplate>
                                            <h1>
                                                Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:GridView ID="gridReporte3_R5" runat="server" AutoGenerateColumns="False" DataKeyNames="id_marca"
                                        Width="100%" ShowFooter="True" CssClass="grid-view">
                                            <Columns>
                                                <asp:BoundField HeaderText="Jabón de lavandería" DataField="nombre_marca"/> 
                                                <asp:BoundField HeaderText="Casa ley" DataField="1"/> 
                                                <asp:BoundField HeaderText="Chedraui" DataField="2"/>
                                                <asp:BoundField HeaderText="Comercial mexicana" DataField="3"/>  
                                                <asp:BoundField HeaderText="ISSTE" DataField="4"/> 
                                                <asp:BoundField HeaderText="Soriana" DataField="5"/>  
                                                <asp:BoundField HeaderText="Bodega aurrera" DataField="6"/>  
                                                <asp:BoundField HeaderText="Walmart" DataField="7"/>  
                                                <asp:BoundField HeaderText="Total" DataField="100"/>  
                                            </Columns>   
                                            <FooterStyle CssClass="grid-footer" />                   
                                        <EmptyDataTemplate>
                                            <h1>Sin información</h1>
                                        </EmptyDataTemplate>
                                    </asp:GridView>    
                                </td>
                            </tr>
                        </table>
                        
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
        
        <div class="clear">
        </div>
    </div>
</asp:Content>