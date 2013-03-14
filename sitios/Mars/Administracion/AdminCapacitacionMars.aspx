<%@ Page Language="vb" Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion.master"
    AutoEventWireup="false" 
    CodeBehind="AdminCapacitacionMars.aspx.vb" 
    Inherits="procomlcd.AdminCapacitacionMars" %>

<asp:Content id="Content1" ContentPlaceHolderID="ContentPlaceHolderCapacitacion" runat="Server">
    <!--
Title Under Menu  
--> 
    <div id="titulo-pagina">
        Edición de nombres</div>
    <div id="contenido-three-column" style="width: 770px">
        <!--

  CONTENT SIDE 1 COLUMN id=""

  -->
        <div id="content-side1-three-column" >
            <ul class="list-of-links">
                <li><asp:HyperLink ID="hyperEdicion" runat="server" NavigateUrl="~/sitios/Mars/Capacitacion/AdminCapacitacionMars.aspx" >Edición Promotores, Supervisores y Ejecutivos</asp:HyperLink></li>
                <li><asp:HyperLink ID="hyperAvisos" runat="server" NavigateUrl="~/sitios/Mars/Capacitacion/AdminAvisoCapacitacionMars.aspx" >Edición Avisos</asp:HyperLink></li>
                <li><asp:HyperLink ID="hyperBitacora" runat="server" NavigateUrl="~/sitios/Mars/Capacitacion/BitacoraAccesoCapacitacionMars.aspx" >Bitacora Accesos</asp:HyperLink></li>
                <li><asp:HyperLink ID="HyperParticipacion" runat="server" NavigateUrl="~/sitios/Mars/Capacitacion/AdminParticipacionMars.aspx" >Participación Usuarios</asp:HyperLink></li>
            </ul>
        </div>
        <!--

  CENTER COLUMN

  -->
        <div id="content-main-three-column">
            <asp:Label ID="lblAviso" runat="server" Text="Label" Font-Bold="True" 
                ForeColor="Red" Visible="False"></asp:Label>
                
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager> 
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlFiltro" runat="server" Width="423px">
                        <table style="width: 99%">
                            <tr>
                                <td style="width: 127px">
                                    Región</td>
                                <td>
                                    <asp:DropDownList ID="cmbRegion" runat="server" Height="21px" Width="240px" 
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 127px">
                                    Estado</td>
                                <td>
                                    <asp:DropDownList ID="cmbEstado" runat="server" Height="21px" Width="240px" 
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 127px">
                                    Ejecutivo de Cuenta</td>
                                <td>
                                    <asp:DropDownList ID="cmbEjecutivoCuenta" runat="server" Height="21px" Width="240px" 
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 127px">
                                    Ejecutivo</td>
                                <td>
                                    <asp:DropDownList ID="cmbEjecutivo" runat="server" Height="21px" 
                                        Width="240px" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 127px">
                                    Supervisor</td>
                                <td>
                                    <asp:DropDownList ID="cmbSupervisor" runat="server" Height="21px" Width="240px" 
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 127px">
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" Width="77px" 
                                        Enabled="False" />
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="gridUsuarios" runat="server" CellPadding="4" 
                            ForeColor="#333333" GridLines="None" ShowFooter="True" 
                            AutoGenerateColumns="False" Width="424px" Height="16px" Font-Bold="False">
                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                            <Columns>
                                    <asp:CommandField ButtonType="Image" 
                                        EditImageUrl="~/sitios/Mars/Imagenes/Editar.png" ShowEditButton="True" />
                                    <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/>
                                    <asp:BoundField HeaderText="Nombre" DataField="NombrePromotor"/>
                                    <asp:BoundField HeaderText="Activo" DataField="Activo"/>
                                </Columns>
                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </asp:Panel>    

            <asp:Panel ID="pnlPromotor" runat="server" Width="422px" Visible="False">
                <table style="width: 123%">
                    <tr>
                        <td style="width: 102px">
                            &nbsp;</td>
                        <td colspan="2" style="text-align: left" bgcolor="#999999">
                            <asp:LinkButton ID="linkBorrar" runat="server">Borrar Calificaciones del Promotor</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Promotor</td>
                        <td style="text-align: left; width: 222px;">
                            <asp:TextBox ID="txtLoginPromotor" runat="server" Enabled="False" Width="160px"></asp:TextBox>
                        </td>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Nombre</td>
                        <td colspan="2" style="text-align: left">
                            <asp:TextBox ID="txtNombrePromotor" runat="server" Width="320px" 
                                MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Fecha Nacimiento</td>
                        <td colspan="2" style="text-align: left">
                            <asp:TextBox ID="txtCumpleaños" runat="server" Width="160px" MaxLength="11"></asp:TextBox>
                            <asp:RegularExpressionValidator id="rvaCumpleaños" runat="server" ErrorMessage="La fecha no es correcta formato 'dd/mm/aaaa'" Display="Dynamic" 
                                        ValidationExpression="\d{2}/\d{2}/\d{4}" ControlToValidate="txtCumpleaños" ValidationGroup="Mars" />
                                    <asp:RangeValidator ID="rvFechaCumpleaños" runat="server" ErrorMessage="La fecha no es valida" 
                                    ValidationGroup="Mars" ControlToValidate="txtCumpleaños"
                                        MaximumValue="31/12/3000" MinimumValue="01/01/1930" Type="Date"></asp:RangeValidator>
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Activo</td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="cmbActivo" runat="server" AutoPostBack="True" 
                                Height="22px" Width="80px">
                                <asp:ListItem Value="1">SI</asp:ListItem>
                                <asp:ListItem Value="0">NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Fecha Ingreso</td>
                        <td colspan="2" style="text-align: left">
                            <asp:TextBox ID="txtFechaIngresoPromotor" runat="server" Width="160px" 
                                MaxLength="11"></asp:TextBox>
                            <a href="javascript:;" 
                                onclick="window.open('../../../Calendario/popup.aspx?textbox=ctl00$ContentPlaceHolder1$txtFechaInicio','cal','width=250,height=225,left=270,top=180')">
                            <img alt="" border="0" src="../Img/SmallCalendar.gif" /></a></td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Estado</td>
                        <td colspan="2" style="text-align: left">
                            <asp:DropDownList ID="cmbEstadoPromotor" runat="server" Height="22px" 
                                Width="210px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Región</td>
                        <td colspan="2" style="text-align: left">
                            <asp:DropDownList ID="cmbRegionPromotor" runat="server" Height="22px" 
                                Width="210px" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Supervisor</td>
                        <td colspan="2" style="text-align: left">
                            <asp:DropDownList ID="cmbLoginSupervisor" runat="server" Height="22px" 
                                Width="210px" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Nombre</td>
                        <td colspan="2" style="text-align: left">
                            <asp:TextBox ID="txtNombreSupervisor" runat="server" Width="319px" 
                                MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            No. Región</td>
                        <td colspan="2" style="text-align: left">
                            <asp:DropDownList ID="cmbLoginEjecutivo" runat="server" Height="22px" 
                                Width="210px" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Ejecutivo</td>
                        <td colspan="2" style="text-align: left">
                            <asp:TextBox ID="txtNombreEjecutivo" runat="server" Width="316px" 
                                Enabled="False" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Ejecutivo Cuenta</td>
                        <td colspan="2" style="text-align: left">
                            <asp:DropDownList ID="cmbLoginEjecutivoCuenta" runat="server" Height="22px" 
                                Width="210px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            &nbsp;</td>
                        <td style="text-align: center; width: 222px;">
                            <asp:Button ID="btnGuardar" runat="server" style="text-align: center" 
                                Text="Guardar" ValidationGroup="Mars" />
                        </td>
                        <td style="text-align: center">
                            <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                                style="text-align: center" Text="Cancelar" />
                        </td>
                    </tr>
                </table>
            <br />
            </asp:Panel>
            </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnFiltrar" />
<asp:PostBackTrigger ControlID="btnGuardar"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnGuardar"></asp:PostBackTrigger>
                </Triggers>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnGuardar" />
                </Triggers>
            </asp:UpdatePanel>
            
        </div>
        <!-- END MAIN COLUMN -->
        <!--

  CONTENT SIDE 2 COLUMN


  -->
        <div class="clear">
        </div>
    </div>
</asp:Content>
