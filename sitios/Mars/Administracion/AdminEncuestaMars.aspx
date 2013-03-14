<%@ Page Language="vb" Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion.master"
    AutoEventWireup="false" 
    CodeBehind="AdminEncuestaMars.aspx.vb" 
    Inherits="procomlcd.AdminEncuestaMars" %>

<asp:Content id="Content1" ContentPlaceHolderID="ContentPlaceHolderCapacitacion" runat="Server">
    <!--
Title Under Menu
-->
    <div id="titulo-pagina">
        Crear encuesta</div>
    <div id="contenido-three-column">
        <!--

  CONTENT SIDE 1 COLUMN

  -->
        <div id="content-side1-three-column" >
            <br />
            <br />
        </div>
        <!--

  CENTER COLUMN

  -->
        <div id="content-main-three-column" style="text-align: left">
            <table class="style5">
                <tr>
                    <td style="width: 58px" bgcolor="Silver">
                        <asp:LinkButton ID="lnkNuevo" runat="server">Nuevo</asp:LinkButton>
                    </td>
                    <td style="width: 75px" bgcolor="Silver">
                        <asp:LinkButton ID="lnkConsultas" runat="server">Consultas</asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            
            <asp:Panel ID="pnlConsultas" runat="server">
                <asp:GridView ID="gridEncuestas" runat="server" CellPadding="4" ForeColor="#333333" 
                    GridLines="None" ShowFooter="True" Width="538px" 
                    AutoGenerateColumns="False">
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" 
                            EditImageUrl="~/sitios/Mars/Imagenes/Editar.png" ShowEditButton="True" />
                        <asp:BoundField DataField="id_encuesta" HeaderText="ID Encuesta" />
                        <asp:BoundField DataField="titulo" HeaderText="Titulo" />
                        <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha Inicio" />
                        <asp:BoundField DataField="fecha_fin" HeaderText="Fecha Fin" />
                    </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </asp:Panel>
            
            <br />
            <asp:Panel ID="pnlNuevo" runat="server" Visible="False">
                <table>
                    <tr>
                        <td style="width: 167px">
                            Titulo encuesta</td>
                        <td style="width: 366px" colspan="2">
                        <asp:TextBox ID="txtTitulo" runat="server" Height="18px" Width="336px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 167px">
                            Fecha Inicio</td>
                        <td style="width: 366px" colspan="2">
                            <asp:TextBox ID="txtFechaInicio" runat="server" Height="18px" Width="160px" 
                                Enabled="False"></asp:TextBox>
                            <a href="javascript:;" 
                                
                                
                                onclick="window.open('../../../Calendario/popup.aspx?textbox=ctl00$ContentPlaceHolderCapacitacion$txtFechaInicio','cal','width=250,height=225,left=270,top=180')">
                            <img alt="" border="0" src="../Img/SmallCalendar.gif" /></a></td>
                    </tr>
                    <tr>
                        <td style="width: 167px">
                            Fecha Fin</td>
                        <td style="text-align: left; width: 366px;" colspan="2">
                            <asp:TextBox ID="txtFechaFin" runat="server" Height="18px" Width="160px" 
                                Enabled="False"></asp:TextBox>
                            <a href="javascript:;" 
                                
                                
                                onclick="window.open('../../../Calendario/popup.aspx?textbox=ctl00$ContentPlaceHolderCapacitacion$txtFechaFin','cal','width=250,height=225,left=270,top=180')">
                            <img alt="" border="0" src="../Img/SmallCalendar.gif" /></a></td>
                    </tr>
                    <tr>
                        <td style="width: 167px">
                            &nbsp;</td>
                        <td style="text-align: left; width: 366px;">
                            <asp:Label ID="lblAviso" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 366px;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 167px">
                            &nbsp;</td>
                        <td colspan="2" style="text-align: left; ">
                            <asp:GridView ID="gridFrentes" runat="server" AutoGenerateColumns="False" 
                                BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
                                CellPadding="4" Height="16px" ShowFooter="True" style="text-align: left" Width="100%">
                                <RowStyle BackColor="White" ForeColor="#000000" HorizontalAlign="Left" 
                                    VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="no_pregunta " HeaderText="No." />
                                    <asp:TemplateField HeaderText="Pregunta">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPregunta" runat="server" 
                                                Text='<%# DataBinder.Eval(Container.DataItem, "Pregunta") %>' textmode="MultiLine" Width="200" Height="50"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="top" Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tipo Respuesta">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAbierta" runat="server" text="Abierta" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "completo5")) %>' />
                                                <br /><asp:CheckBox ID="chkSiNo" runat="server" text="Si/No" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "completo5")) %>' />
                                            </ItemTemplate>
                                        <ItemStyle VerticalAlign="top" Width="40%" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <h1>
                                        No hay datos</h1>
                                </EmptyDataTemplate>
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;</td>
                    </tr>
                </table>
                
                <asp:Panel ID="pnlPregunta" runat="server" Visible="False" BorderStyle="Solid" 
                    BorderWidth="1px">
                </asp:Panel>
               
            </asp:Panel>
            
                      
        </div>
        <!-- END MAIN COLUMN -->
        <!--

  CONTENT SIDE 2 COLUMN

  -->
        <div class="clear">
        </div>
    </div>
</asp:Content>
