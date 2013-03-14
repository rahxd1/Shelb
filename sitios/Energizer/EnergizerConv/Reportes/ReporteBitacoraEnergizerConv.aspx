﻿<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Energizer/EnergizerConv/Energizer_Conv.Master"
    AutoEventWireup="false" 
    CodeBehind="ReporteBitacoraEnergizerConv.aspx.vb" 
    Inherits="procomlcd.ReporteBitacoraEnergizerConv"
    title="Energizer Conveniencia - Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1EnergizerConv" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
            Reporte bitácora captura</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%">
                            <tr><td style="width: 96px">&nbsp;</td>
                                <td style="width: 359px">&nbsp;</td>
                                <td style="text-align: right">
                                    <img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" runat="server" 
                                            NavigateUrl="~/sitios/Energizer/EnergizerConv/Reportes/ReportesEnergizerConv.aspx">Regresar</asp:HyperLink></td>
                            </tr>
                            <tr><td style="width: 96px">Periodo</td>
                                <td colspan="2"><asp:DropDownList ID="cmbPeriodo" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                            </tr>
                            <tr><td style="width: 96px">Región</td>
                                <td colspan="2"><asp:DropDownList ID="cmbRegion" runat="server" CssClass="ddl" AutoPostBack="True" /></td>
                            </tr>
                            <tr><td style="width: 96px">Promotor</td>
                                <td colspan="2"><asp:DropDownList ID="cmbPromotor" runat="server" CssClass="ddl" AutoPostBack="True" />
                                    </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center;">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                        <ProgressTemplate>
                                            <table id="pnlSave" >
                                            <tr><td>
                                                <br /><p><img alt="Cargando Reporte" src="../../../../Img/loading.gif" /> El Reporte se 
                                                esta generando.</p><br /></td></tr>
                                        </table>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" ShowFooter="True" 
                            Width="100%" CssClass="grid-view">
                            <Columns>
                                <asp:CommandField EditText="Ver detalle" ShowEditButton="True">
                                <ControlStyle ForeColor="Red" />
                                </asp:CommandField>
                                <asp:BoundField DataField="id_usuario" HeaderText="Promotor" />
                                <asp:BoundField DataField="Tiendas" HeaderText="Tiendas" />
                                <asp:BoundField DataField="Capturas" HeaderText="Capturas" />
                                <asp:TemplateField HeaderText="% Porcentaje">
                                    <ItemStyle Font-Bold="True" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                            <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:Button ID="btnDetalles" runat="server" Text="Ver Detalle General" />
                        <br />
                        <asp:Panel ID="pnlDetalle" runat="server" Visible="False" BorderColor="Black" 
                        BorderStyle="Dashed" style="text-align: center" BorderWidth="2px">
                       
                        <asp:GridView ID="gridDetalle" runat="server" 
                             AutoGenerateColumns="False" ShowFooter="True" Width="100%" Caption="DETALLES" 
                             Font-Bold="False" CssClass="grid-view">
                             <Columns>
                                 <asp:BoundField DataField="id_usuario" HeaderText="Promotor" />
                                 <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena"/>
                                 <asp:BoundField DataField="nombre" HeaderText="Tienda" />
                                 <asp:BoundField DataField="estatus" HeaderText="Capturada" />
                             </Columns>
                             <FooterStyle CssClass="grid-footer" />
                             <EmptyDataTemplate>
                                <h1>Sin información</h1>
                            </EmptyDataTemplate>
                         </asp:GridView>
                     </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
        </div>
        
        <!--CONTENT SIDE COLUMN-->
        <div id="content-side-two-column">
            
            <asp:LinkButton ID="lnkExportar" runat="server" Font-Bold="True">Exportar Tabla a Excel</asp:LinkButton>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>