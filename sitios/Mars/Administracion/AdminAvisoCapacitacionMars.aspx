<%@ Page Language="vb" Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion.master"
    AutoEventWireup="false" 
    CodeBehind="AdminAvisoCapacitacionMars.aspx.vb" 
    Inherits="procomlcd.AdminAvisoCapacitacionMars" %>

<asp:Content id="Content1" ContentPlaceHolderID="ContentPlaceHolderCapacitacion" runat="Server">
    <!--
Title Under Menu  
--> 
    <div id="titulo-pagina">
        Crear avisos</div>
    <div id="contenido-three-column" style="width: 748px">
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
            <table class="style5">
                <tr>
                    <td style="width: 61px" bgcolor="#999999">
                        <asp:LinkButton ID="linkNuevo" runat="server">Nuevo</asp:LinkButton>
                    </td>
                    <td style="width: 92px" bgcolor="#999999">
                        <asp:LinkButton ID="LinkConsultas" runat="server">Consultas</asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                </table>
        
            <asp:Panel ID="panelGrid" runat="server" Width="423px">
                <asp:GridView ID="gridAvisos" runat="server" CellPadding="4" 
                    ForeColor="#333333" GridLines="None" ShowFooter="True" 
                    AutoGenerateColumns="False" Width="536px" Height="16px" Font-Bold="False">
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <Columns>
                            <asp:CommandField ButtonType="Image" 
                                EditImageUrl="~/sitios/Mars/Imagenes/Editar.png" ShowEditButton="True" />
                            <asp:CommandField ButtonType="Image" 
                                DeleteImageUrl="~/sitios/Mars/Imagenes/delete-icon.png" 
                                ShowDeleteButton="True" />
                            <asp:BoundField HeaderText="Aviso" DataField="id_aviso"/>
                            <asp:BoundField HeaderText="Titulo" DataField="titulo_aviso"/>
                            <asp:BoundField HeaderText="Fecha Inicio" DataField="fecha_inicio" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="Fecha Fin" DataField="fecha_fin" DataFormatString="{0:d}" />
                        </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </asp:Panel>         
            <asp:Panel ID="pnlAvisos" runat="server" Width="422px" Visible="False">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 102px">
                            Titulo</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtTitulo" runat="server" Width="316px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" 
                                ControlToValidate="txtTitulo" ErrorMessage="*" ValidationGroup="Mars"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Descripción</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtDescripcion" runat="server" Width="320px" Height="115px" 
                                TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" 
                                ControlToValidate="txtDescripcion" ErrorMessage="*" 
                                ValidationGroup="Mars"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Dirigido a</td>
                        <td colspan="2">
                            <asp:DropDownList ID="cmbDirigido" runat="server" AutoPostBack="True" 
                                Height="21px" Width="224px">
                                <asp:ListItem>TODOS</asp:ListItem>
                                <asp:ListItem>PROMOTORES</asp:ListItem>
                                <asp:ListItem>SUPERVISORES</asp:ListItem>
                                <asp:ListItem>EJECUTIVOS</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Fecha Inicio</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtFechaInicio" runat="server" Width="160px"></asp:TextBox>
                            <a href="javascript:;" 
                                onclick="window.open('../../../Calendario/popup.aspx?textbox=ctl00$ContentPlaceHolderCapacitacion$txtFechaInicio','cal','width=250,height=225,left=270,top=180')">
                            <img alt="" border="0" src="../Img/SmallCalendar.gif" /></a><asp:RequiredFieldValidator 
                                ID="rfvFechaInicio" runat="server" ControlToValidate="txtFechaInicio" 
                                ErrorMessage="*" ValidationGroup="Mars"></asp:RequiredFieldValidator>
                                <br /><asp:RegularExpressionValidator id="rvaFechaInicio" runat="server" ErrorMessage="La fecha no es correcta formato 'dd/mm/aaaa'" Display="Dynamic" 
                                        ValidationExpression="\d{2}/\d{2}/\d{4}" ControlToValidate="txtFechaInicio" ValidationGroup="Mars" />
                               <asp:RangeValidator ID="rvFechaInicio" runat="server" ErrorMessage="La fecha no es valida" 
                                    ValidationGroup="Mars" ControlToValidate="txtFechaInicio"
                                        MaximumValue="31/12/3000" MinimumValue="01/01/2010" Type="Date"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            Fecha Fin</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtFechaFin" runat="server" Width="160px"></asp:TextBox>
                            <a href="javascript:;" 
                                onclick="window.open('../../../../Calendario/popup.aspx?textbox=ctl00$ContentPlaceHolderCapacitacion$txtFechaFin','cal','width=250,height=225,left=270,top=180')">
                            <img alt="" border="0" src="../Img/SmallCalendar.gif" /></a><asp:RequiredFieldValidator 
                                ID="rfvFechaFin" runat="server" 
                                ControlToValidate="txtFechaFin" ErrorMessage="*" 
                                ValidationGroup="Mars"></asp:RequiredFieldValidator>
                            <br /><asp:RegularExpressionValidator id="rvaFechaFin" runat="server" ErrorMessage="La fecha no es correcta formato 'dd/mm/aaaa'" Display="Dynamic" 
                                        ValidationExpression="\d{2}/\d{2}/\d{4}" ControlToValidate="txtFechaFin" ValidationGroup="Mars" />
                               <asp:RangeValidator ID="rvFechaFin" runat="server" ErrorMessage="La fecha no es valida" 
                                    ValidationGroup="Mars" ControlToValidate="txtFechaFin"
                                        MaximumValue="31/12/3000" MinimumValue="01/01/2010" Type="Date"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px">
                            <asp:Label ID="lblIDAviso" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td style="text-align: center">
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
            
        </div>
        <!-- END MAIN COLUMN -->
        <!--

  CONTENT SIDE 2 COLUMN


  -->
        <div class="clear">
        </div>
    </div>
</asp:Content>
