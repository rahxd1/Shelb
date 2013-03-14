<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Autoservicio/Mars_Autoservicio.Master"
    AutoEventWireup="false" 
    CodeBehind="ReportePDPMarsAS.aspx.vb" 
    Inherits="procomlcd.ReportePDPMarsAS"
    title="Mars Autoservicio - PDP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsAutoservicio" runat="Server">

    <!--titulo-pagina-->
        <div id="titulo-pagina">
                        PDP</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                <tr>
                    <td style="width: 137px">
                        &nbsp;</td>
                    <td style="width: 448px">
                        &nbsp;</td>
                    <td style="text-align: right">
                        <img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink ID="lnkRegresar" runat="server" 
                                NavigateUrl="~/sitios/Mars/Autoservicio/MenuMarsAS.aspx">Regresar</asp:HyperLink></td>
                </tr>
                        <tr>
                            <td style="width: 137px">
                                Region</td>
                            <td colspan="2">
                                <asp:DropDownList ID="cmbRegion" runat="server" AutoPostBack="True" 
                                    CssClass="ddl">
                                </asp:DropDownList>
                            </td>
                        </tr>
                <tr>
                    <td style="width: 137px">
                        Ejecutivo</td>
                    <td colspan="2">
                        <asp:DropDownList ID="cmbEjecutivo" runat="server" CssClass="ddl"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                        <tr>
                            <td style="width: 137px">
                                Supervisor</td>
                            <td colspan="2">
                                <asp:DropDownList ID="cmbSupervisor" runat="server" AutoPostBack="True" 
                                    CssClass="ddl">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 137px">
                                Ruta</td>
                            <td colspan="2">
                                <asp:DropDownList ID="cmbPromotor" runat="server" AutoPostBack="True" 
                                    CssClass="ddl">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center;">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                    <ProgressTemplate>
                                        <table id="pnlSave" >
                                            <tr><td style="width: 640px">
                                                <br /><p><img alt="Cargando Reporte" src="../../../../Img/loading.gif" /> El Reporte se 
                                                esta generando.</p><br /></td></tr>
                                        </table>
                                    </ProgressTemplate>
                                </asp:UpdateProgress></td>
                        </tr>
                </table>
                    <asp:Panel ID="pnlPDP" runat="server" Visible="False">
                        <br />
                        <table style="width: 100%" align="center">
                            <tr>
                                <td style="text-align: center" align="center" colspan="4">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%; height: 142px;" 
                                        width="100%">
                                        <tr>
                                            <td style="border-top: thin solid #254061; border-bottom: thin solid #254061; text-align: center; width: 371px; border-left-color: #254061; border-left-width: thin; border-right-color: #254061; border-right-width: thin;">
                                                <b>OBJETIVO</b></td>
                                            <td style="border-top: thin solid #254061; border-bottom: thin solid #254061; text-align: center; width: 96px; border-left-color: #254061; border-left-width: thin; border-right-color: #254061; border-right-width: thin;">
                                                <b>VALOR</b></td>
                                            <td style="border-top: thin solid #254061; border-bottom: thin solid #254061; text-align: center; border-left-color: #254061; border-left-width: thin; border-right-color: #254061; border-right-width: thin;">
                                                <b>RESULTADO</b></td>
                                        </tr>
                                        <tr>
                                            <td style="background-color: #EFF3FB; text-align: left; width: 371px;">
                                                1. Alcance punto de venta ideal</td>
                                            <td style="background-color: #EFF3FB; text-align: center; width: 96px;">
                                                35 %</td>
                                            <td style="background-color: #EFF3FB">
                                                <asp:Label ID="lblResultado1" runat="server" style="font-weight: 700"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 371px">
                                                2. Alcance Segmento</td>
                                            <td style="text-align: center; width: 96px">
                                                35 %</td>
                                            <td>
                                                <asp:Label ID="lblResultado2" runat="server" style="font-weight: 700"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background-color: #EFF3FB; text-align: left; width: 371px;">
                                                3. Implementacion en Tiempo y forma</td>
                                            <td style="background-color: #EFF3FB; text-align: center; width: 96px;">
                                                10 %</td>
                                            <td style="background-color: #EFF3FB">
                                                <asp:Label ID="lblResultado3" runat="server" style="font-weight: 700"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 371px">
                                                4. Desarrollo</td>
                                            <td style="text-align: center; width: 96px">
                                                15 %</td>
                                            <td>
                                                <asp:Label ID="lblResultado4" runat="server" style="font-weight: 700"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-bottom: thin solid #254061; background-color: #EFF3FB; text-align: left; width: 371px; border-left-color: #254061; border-left-width: thin; border-right-color: #254061; border-right-width: thin; border-top-color: #254061; border-top-width: thin;">
                                                5. Cumplimiento de captura</td>
                                            <td style="border-bottom: thin solid #254061; background-color: #EFF3FB; text-align: center; width: 96px; border-left-color: #254061; border-left-width: thin; border-right-color: #254061; border-right-width: thin; border-top-color: #254061; border-top-width: thin;">
                                                5 %</td>
                                            <td style="border-bottom-style: solid; border-width: thin; border-color: #254061; background-color: #EFF3FB;">
                                                <asp:Label ID="lblResultado5" runat="server" style="font-weight: 700"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-bottom: thin solid #254061; background-color: #4F81BD; text-align: left; width: 371px; border-left-color: #254061; border-left-width: thin; border-right-color: #254061; border-right-width: thin; border-top-color: #254061; border-top-width: thin;">
                                                &nbsp;</td>
                                            <td style="border-bottom: thin solid #254061; background-color: #4F81BD; text-align: center; width: 96px; border-left-color: #254061; border-left-width: thin; border-right-color: #254061; border-right-width: thin; border-top-color: #254061; border-top-width: thin;">
                                                &nbsp;</td>
                                            <td style="border-bottom-style: solid; border-width: thin; border-color: #254061; background-color: #4F81BD;">
                                                <asp:Label ID="lblResultadoTotal" runat="server" ForeColor="White" 
                                                    style="font-weight: 700"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: center" align="center" colspan="4">
                                    <table cellpadding="0" cellspacing="0" 
                                        
                                        style="border-top: thin solid #254061; border-bottom: thin solid #254061; width: 99%; height: 53px; border-left-color: #254061; border-left-width: thin; border-right-color: #254061; border-right-width: thin;">
                                        <tr>
                                            <td style="width: 449px">
                                                Nombre</td>
                                            <td>
                                                Fecha ingreso</td>
                                        </tr>
                                        <tr>
                                            <td style="background-color: #EFF3FB; width: 449px;">
                                                <asp:Label ID="lblNombrePromotor" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="background-color: #EFF3FB">
                                                <asp:Label ID="lblFechaIngreso" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    Del periodo</td>
                                <td>
                                    <asp:DropDownList ID="cmbPeriodoA" runat="server" AutoPostBack="True" 
                                        CssClass="ddl" Height="22px" Width="135px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    al periodo</td>
                                <td>
                                    <asp:DropDownList ID="cmbPeriodoB" runat="server" AutoPostBack="True" 
                                        CssClass="ddl" Height="22px" Width="135px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table style="border: thin solid #808080; width: 100%">
                                        <tr>
                                            <td style="border: thin ridge #4F81BD; text-align: center; width: 479px; background-color: #4F81BD; color: #FFFFFF; font-weight: bold; height: 52px;">
                                                OBJETIVO 1<br />
                                                &nbsp;ALCANCE DE PUNTO DE VENTA IDEAL</td>
                                            <td style="border: thin solid #385D8A; text-align: center; height: 52px; background-color: #254061; color: #FFFFFF; font-weight: bold;">
                                                VALOR&nbsp;
                                                <br />
                                                35%</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="gridObjetivo1" runat="server" AutoGenerateColumns="False" 
                                                    CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" 
                                                    Width="100%" HorizontalAlign="Center">
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Clasificación de <br /> tiendas">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblClasificacion" runat="server" Text='<%#Eval("clasificacion_tienda")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Tiendas" HeaderText="Tiendas totales" />
                                                        <asp:BoundField DataField="Si" HeaderText="Cuantas SI" />
                                                        <asp:TemplateField HeaderText="Cuantas <br /> NO"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Subtotal por <br /> Clasificación">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCumplimiento" runat="server" Text='<%#Eval("Si")%>'></asp:Label>
                                                                de
                                                                <asp:Label ID="lblTiendas" runat="server" Text='<%#Eval("Tiendas")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#D0D8E8" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <EmptyDataTemplate>
                                                        <h2>
                                                            No hay información de este promotor</h2>
                                                    </EmptyDataTemplate>
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#4F81BD" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#4F81BD" />
                                                    <AlternatingRowStyle BackColor="#D0D8E8" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 479px; text-align: left;">
                                                &nbsp;<asp:LinkButton ID="lnkObjetivo1" runat="server" Font-Bold="True">Ver Detalles</asp:LinkButton>
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" width="100%" style="height: 71px" 
                                                    align="center">
                                                    <tr>
                                                        <td style="background-color: #4F81BD; color: #FFFFFF; font-weight: bold; height: 33px; width: 85px; text-align: center;">
                                                            Alcance</td>
                                                        <td style="background-color: #4F81BD; color: #FFFFFF; font-weight: bold; height: 33px; text-align: center;">
                                                            <asp:Label ID="lblAlcance1" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bgcolor="#D0D8E8" style="width: 85px; text-align: center">
                                                            <b style="text-align: center">Resultado</b></td>
                                                        <td style="background-color: #254061; font-weight: bold; color: #FFFFFF; text-align: center;">
                                                            <asp:Label ID="lblPonderacion1" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table style="border: thin solid #808080; width: 100%">
                                        <tr>
                                            <td style="border: thin ridge #4F81BD; text-align: center; width: 479px; background-color: #4F81BD; color: #FFFFFF; font-weight: bold; height: 52px;">
                                                OBJETIVO 2<br />
                                                &nbsp;ALCANCE SEGMENTO</td>
                                            <td style="border: thin solid #385D8A; text-align: center; height: 52px; background-color: #254061; color: #FFFFFF; font-weight: bold;">
                                                VALOR&nbsp;
                                                <br />
                                                35%</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="gridObjetivo2" runat="server" AutoGenerateColumns="False" 
                                                    CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" 
                                                    Width="100%">
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Segmentos">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSegmentos" runat="server" Text='<%#Eval("Segmento")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Tiendas" HeaderText="Tiendas totales" />
                                                        <asp:BoundField DataField="Cumplimiento" HeaderText="Cuantos SI" />
                                                        <asp:BoundField DataField="Falta" HeaderText="Cuantos NO" />
                                                        <asp:TemplateField HeaderText="Subtotal <br />por Segmento">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCumplimiento" runat="server" Text='<%#Eval("Cumplimiento")%>'></asp:Label>
                                                                de
                                                                <asp:Label ID="lblTiendas" runat="server" Text='<%#Eval("Tiendas")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#D0D8E8" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <EmptyDataTemplate>
                                                        <h2>
                                                            No hay información de este promotor</h2>
                                                    </EmptyDataTemplate>
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#4F81BD" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <AlternatingRowStyle BackColor="#D0D8E8" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 479px">
                                                <asp:LinkButton ID="lnkObjetivo2" runat="server" Font-Bold="True">Ver Detalles</asp:LinkButton>
                                            </td>
                                            <td>
                                                <table align="center" cellpadding="0" cellspacing="0" style="height: 71px" 
                                                    width="100%">
                                                    <tr>
                                                        <td style="background-color: #4F81BD; color: #FFFFFF; font-weight: bold; height: 33px; width: 85px; text-align: center;">
                                                            Alcance</td>
                                                        <td style="background-color: #4F81BD; color: #FFFFFF; font-weight: bold; height: 33px; text-align: center;">
                                                            <asp:Label ID="lblAlcance2" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bgcolor="#D0D8E8" style="width: 85px; text-align: center">
                                                            <b style="text-align: center">Resultado</b></td>
                                                        <td style="background-color: #254061; font-weight: bold; color: #FFFFFF; text-align: center;">
                                                            <asp:Label ID="lblPonderacion2" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table style="border: thin solid #808080; width: 100%">
                                        <tr>
                                            <td style="border: thin ridge #4F81BD; text-align: center; width: 479px; background-color: #4F81BD; color: #FFFFFF; font-weight: bold; height: 52px;">
                                                OBJETIVO 3<br />
                                                &nbsp;IMPLEMENTACIÓN EN TIEMPO Y FORMA</td>
                                            <td style="border: thin solid #385D8A; text-align: center; height: 52px; background-color: #254061; color: #FFFFFF; font-weight: bold;">
                                                VALOR&nbsp;
                                                <br />
                                                10%</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="gridObjetivo3" runat="server" AutoGenerateColumns="False" 
                                                    CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" 
                                                    Width="100%">
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Implementación">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblImplementacion" runat="server" Text='<%#Eval("nombre_proceso")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="SiNo" HeaderText="SiNo" />
                                                        <asp:BoundField DataField="Cumplimiento" HeaderText="Cumplimiento" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#D0D8E8" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <EmptyDataTemplate>
                                                        <h2>
                                                            Sin implementaciones</h2>
                                                    </EmptyDataTemplate>
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#4F81BD" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <AlternatingRowStyle BackColor="#D0D8E8" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 479px">
                                                <asp:LinkButton ID="lnkObjetivo3" runat="server" Font-Bold="True">Ver Detalles</asp:LinkButton>
                                            </td>
                                            <td>
                                                <table align="center" cellpadding="0" cellspacing="0" style="height: 71px" 
                                                    width="100%">
                                                    <tr>
                                                        <td style="background-color: #4F81BD; color: #FFFFFF; font-weight: bold; height: 33px; width: 85px; text-align: center;">
                                                            Alcance</td>
                                                        <td style="background-color: #4F81BD; color: #FFFFFF; font-weight: bold; height: 33px; text-align: center;">
                                                            <asp:Label ID="lblAlcance3" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bgcolor="#D0D8E8" style="width: 85px; text-align: center">
                                                            <b style="text-align: center">Resultado</b></td>
                                                        <td style="background-color: #254061; font-weight: bold; color: #FFFFFF; text-align: center;">
                                                            <asp:Label ID="lblPonderacion3" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table style="border: thin solid #808080; width: 100%">
                                        <tr>
                                            <td style="border: thin ridge #4F81BD; text-align: center; width: 479px; background-color: #4F81BD; color: #FFFFFF; font-weight: bold; height: 52px;">
                                                OBJETIVO 4<br />
                                                DESARROLLO</td>
                                            <td style="border: thin solid #385D8A; text-align: center; height: 52px; background-color: #254061; color: #FFFFFF; font-weight: bold;">
                                                VALOR&nbsp;
                                                <br />
                                                15%</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="gridObjetivo4A" runat="server" AutoGenerateColumns="False" 
                                                    CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" 
                                                    Width="100%" Caption="<b>Certificación vigente</b>" CaptionAlign="Top">
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCertifiacion" runat="server" Text='<%#Eval("Certifiacion")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="SiNo" HeaderText="SiNo" />
                                                        <asp:BoundField DataField="Cumplimiento" HeaderText="Cumplimiento" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#D0D8E8" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <EmptyDataTemplate>
                                                        <h2>
                                                            Sin certifiaciones vigentes</h2>
                                                    </EmptyDataTemplate>
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#4F81BD" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <AlternatingRowStyle BackColor="#D0D8E8" />
                                                </asp:GridView>
                                                <br />
                                                <asp:GridView ID="gridObjetivo4B" runat="server" AutoGenerateColumns="False" 
                                                    CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" 
                                                    Width="100%" Caption="<b>Alineaciones</b>" CaptionAlign="Top">
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProceso" runat="server" Text='<%#Eval("nombre_proceso")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="SiNo" HeaderText="SiNo" />
                                                        <asp:BoundField DataField="Cumplimiento" HeaderText="Cumplimiento" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#D0D8E8" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <EmptyDataTemplate>
                                                        <h2>
                                                            Sin alineaciones</h2>
                                                    </EmptyDataTemplate>
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#4F81BD" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <AlternatingRowStyle BackColor="#D0D8E8" />
                                                </asp:GridView>
                                                <br />
                                                <asp:GridView ID="gridObjetivo4C" runat="server" AutoGenerateColumns="False" 
                                                    CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" 
                                                    Width="100%" Caption="<b>Entrenamientas técnicos</b>" CaptionAlign="Top">
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEntrenamiento" runat="server" Text='<%#Eval("nombre_entrenamiento")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="SiNo" HeaderText="SiNo" />
                                                        <asp:BoundField DataField="Cumplimiento" HeaderText="Cumplimiento" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#D0D8E8" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <EmptyDataTemplate>
                                                        <h2>
                                                            Sin entrenamientos técnicos</h2>
                                                    </EmptyDataTemplate>
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <AlternatingRowStyle BackColor="#D0D8E8" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 479px">
                                                <asp:LinkButton ID="lnkObjetivo4" runat="server" Font-Bold="True">Ver Detalles</asp:LinkButton>
                                                <br />
                                                <asp:Label ID="lblAlcance4A" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="lblAlcance4C" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="lblAlcance4B" runat="server" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <table align="center" cellpadding="0" cellspacing="0" style="height: 71px" 
                                                    width="100%">
                                                    <tr>
                                                        <td style="background-color: #4F81BD; color: #FFFFFF; font-weight: bold; height: 33px; width: 85px; text-align: center;">
                                                            Alcance</td>
                                                        <td style="background-color: #4F81BD; color: #FFFFFF; font-weight: bold; height: 33px; text-align: center;">
                                                            <asp:Label ID="lblAlcance4" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bgcolor="#D0D8E8" style="width: 85px; text-align: center">
                                                            <b style="text-align: center">Resultado</b></td>
                                                        <td style="background-color: #254061; font-weight: bold; color: #FFFFFF; text-align: center;">
                                                            <asp:Label ID="lblPonderacion4" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table style="border: thin solid #808080; width: 100%">
                                        <tr>
                                            <td style="border: thin ridge #4F81BD; text-align: center; width: 479px; background-color: #4F81BD; color: #FFFFFF; font-weight: bold; height: 52px;">
                                                OBJETIVO 5<br />
                                                &nbsp;CUMPLIMIENTO DE CAPTURA</td>
                                            <td style="border: thin solid #385D8A; text-align: center; height: 52px; background-color: #254061; color: #FFFFFF; font-weight: bold;">
                                                VALOR&nbsp;
                                                <br />
                                                5%</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="gridObjetivo5" runat="server" AutoGenerateColumns="False" 
                                                    CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" 
                                                    Width="100%">
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDato" runat="server" Text='<%#Eval("Dato")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Cumplimiento" HeaderText="" />
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                de
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Periodos" HeaderText="" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#D0D8E8" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <EmptyDataTemplate>
                                                        <h2>
                                                            Sin capturas</h2>
                                                    </EmptyDataTemplate>
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#4F81BD" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <AlternatingRowStyle BackColor="#D0D8E8" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 479px">
                                                <asp:LinkButton ID="lnkObjetivo5" runat="server" Font-Bold="True">Ver Detalles</asp:LinkButton>
                                            </td>
                                            <td>
                                                <table align="center" cellpadding="0" cellspacing="0" style="height: 71px" 
                                                    width="100%">
                                                    <tr>
                                                        <td style="background-color: #4F81BD; color: #FFFFFF; font-weight: bold; height: 33px; width: 85px; text-align: center;">
                                                            Alcance</td>
                                                        <td style="background-color: #4F81BD; color: #FFFFFF; font-weight: bold; height: 33px; text-align: center;">
                                                            <asp:Label ID="lblAlcance5" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bgcolor="#D0D8E8" style="width: 85px; text-align: center">
                                                            <b style="text-align: center">Resultado</b></td>
                                                        <td style="background-color: #254061; font-weight: bold; color: #FFFFFF; text-align: center;">
                                                            <asp:Label ID="lblPonderacion5" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                    
                    
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPromotor" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        
        <!--CONTENT SIDE COLUMN-->
        <div id="content-side-two-column">
                &nbsp;</div>
        <div class="clear">
        </div>
    </div>
</asp:Content>