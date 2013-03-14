<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Energizer/Schick/Schick.Master"
    AutoEventWireup="false" 
    CodeBehind="ReportePromosSchick.aspx.vb" 
    Inherits="procomlcd.ReportePromosShick"
    title="Schick - Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1Schick" runat="Server">

<!--titulo-pagina-->
        <div id="titulo-pagina">
            Reporte Promocionales entregados</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr><td style="width: 73px; height: 18px;"></td>
                            <td style="width: 351px; height: 18px;"></td>
                            <td style="text-align: right; height: 18px;"><img alt="Regresar" src="../../../../Img/arrow.gif" />
                            <asp:HyperLink ID="lnkRegresar" runat="server" NavigateUrl="~/sitios/Energizer/Schick/Reportes/ReportesSchick.aspx">Regresar</asp:HyperLink></td>
                        </tr>
                       
                        <tr>
                            <td style="width: 73px">
                                Periodo</td>
                            <td colspan="2">
                                <asp:DropDownList ID="cmbPeriodo" runat="server" AutoPostBack="True" CssClass="ddl" />
                            </td>
                        </tr>
                        <tr><td style="width: 73px">Región</td>
                            <td colspan="2"><asp:DropDownList ID="cmbRegion" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr><td style="width: 73px">Promotor</td>
                            <td colspan="2"><asp:DropDownList ID="cmbPromotor" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr><td style="width: 73px">Cadena</td>
                            <td colspan="2"><asp:DropDownList ID="cmbCadena" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr>
                            <td style="width: 73px">Tienda</td>
                            <td colspan="2"><asp:DropDownList ID="cmbTienda" runat="server" CssClass="ddl" AutoPostBack="True"/></td>
                        </tr>
                        <tr><td colspan="3" style="text-align: center;">
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
                    <asp:Panel ID="PanelFS" runat="server"/>   
                    <asp:GridView ID="gridReporte" runat="server" AutoGenerateColumns="False" 
                        CellPadding="3" 
                        GridLines="Vertical" Width="100%" ShowFooter="True" BackColor="White" 
                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px">
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                                <asp:BoundField HeaderText="Promocional" DataField="nombre_promo"/> 
                                <asp:BoundField HeaderText="Cantidad" DataField="totales" /> 
                            </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            <h1>Sin información</h1>
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#DCDCDC" />
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
        
        <!--CONTENT SIDE COLUMN-->
        <div id="content-side-two-column">
            
            <asp:LinkButton ID="lnkExportar" runat="server" Font-Bold="True">Exportar Tabla a Excel</asp:LinkButton>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>


