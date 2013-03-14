<%@ Page Language="vb" Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Administracion/AdministracionMars.Master"
    AutoEventWireup="false" 
    CodeBehind="AdminPromotoresMars.aspx.vb" 
    Inherits="procomlcd.AdminPromotoresMars"
    Title="Administración Mars" %>

<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdminMars" runat="Server">
  
<!--titulo-pagina -->
    <div id="titulo-pagina">
        Promotores Mars Autoservicio</div>    

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server"/>
           
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td bgcolor="#CCCCCC" colspan="2">
                                <asp:LinkButton ID="lnkConsultas" runat="server" Font-Bold="False">Consultas</asp:LinkButton>
                            </td>
                            <td style="text-align: right;">
                                <img alt="" src="../../../Img/arrow.gif" />
                        <a href="MenuAdminMars.aspx">Regresar</a>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lblAviso" runat="server" CssClass="aviso"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Región</td>
                            <td colspan="2">
                                <asp:DropDownList ID="cmbRegion" runat="server" AutoPostBack="True" 
                                    CssClass="ddl">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Plaza</td>
                            <td colspan="2">
                                <asp:DropDownList ID="cmbPlaza" runat="server" AutoPostBack="True" 
                                    CssClass="ddl">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlConsulta" runat="server" Height="500px" ScrollBars="Both">
                        <asp:GridView ID="gridPromotor" runat="server" AutoGenerateColumns="False" 
                            Height="16px" style="text-align: left" 
                            Width="100%" CssClass="grid-view" ShowFooter="True">
                            <Columns>       
                                <asp:CommandField ButtonType="Image" EditImageUrl="~/Img/Editar.png" 
                                    ShowEditButton="True" />
                                <asp:BoundField DataField="id_usuario" HeaderText="ID Usuario" />
                                <asp:BoundField DataField="nombre_region" HeaderText="Región" />
                                <asp:BoundField DataField="nombre" HeaderText="Promotor" />
                                <asp:BoundField DataField="fecha_ingreso" HeaderText="Fecha Ingreso" DataFormatString="{0:d}"/>
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                        </asp:GridView>
                    </asp:Panel>
                    <asp:Panel ID="pnlNuevo" runat="server" Visible="False" BorderWidth="1px">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: right; width: 181px; height: 19px;">
                                    Login ruta</td>
                                <td style="height: 19px" colspan="2">
                                    <asp:Label ID="lblIDUsuario" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 181px">
                                    Nombre promotor</td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtNombrePromotor" runat="server" 
                                        MaxLength="100" CssClass="cajastxt" Width="474px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvNombrePromotor" runat="server" 
                                        ControlToValidate="txtNombrePromotor" ErrorMessage="*" ValidationGroup="Mars"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 181px">
                                    Fecha ingreso (dd/mm/aaaa)</td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtFechaIngreso" runat="server" MaxLength="10" Width="102px"></asp:TextBox>
                                    </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 181px">
                                    Región</td>
                                <td colspan="2">
                                    <asp:Label ID="lblIDRegion" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 181px">
                                    Procom</td>
                                <td colspan="2">
                                    <asp:DropDownList ID="cmbSupervisor" runat="server" AutoPostBack="True" 
                                        CssClass="ddl">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 181px">
                                    Ejecutivo Mars</td>
                                <td colspan="2">
                                    <asp:DropDownList ID="cmbNoRegion" runat="server" AutoPostBack="True" 
                                        CssClass="ddl">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 181px">
                                    Ejecutivo Shelby</td>
                                <td colspan="2">
                                    <asp:DropDownList ID="cmbEjecutivo" runat="server" AutoPostBack="True" 
                                        CssClass="ddl">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 181px">
                                    &nbsp;</td>
                                <td colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 181px">
                                    &nbsp;</td>
                                <td bgcolor="#990000" colspan="2" style="color: #FFFFFF; text-align: center;">
                                    <span style="font-weight: 700; text-align: center; background-color: #990000">
                                    CALIFICACIONES MODULOS</span></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 181px">
                                    &nbsp;</td>
                                <td bgcolor="#990000" style="color: #FFFFFF; font-weight: 700;">
                                    Modulo 1</td>
                                <td style="width: 427px">
                                    <asp:TextBox ID="txtCalificacion1" runat="server" MaxLength="10" Width="102px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 181px">
                                    &nbsp;</td>
                                <td bgcolor="#990000" style="font-weight: 700; color: #FFFFFF;">
                                    Modulo 2</td>
                                <td style="width: 427px">
                                    <asp:TextBox ID="txtCalificacion2" runat="server" MaxLength="10" Width="102px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 181px">
                                    &nbsp;</td>
                                <td bgcolor="#990000" style="font-weight: 700; color: #FFFFFF">
                                    Modulo 3</td>
                                <td style="width: 427px">
                                    <asp:TextBox ID="txtCalificacion3" runat="server" MaxLength="10" Width="102px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 181px">
                                    &nbsp;</td>
                                <td bgcolor="#990000" style="font-weight: 700; color: #FFFFFF">
                                    Modulo 4</td>
                                <td style="width: 427px">
                                    <asp:TextBox ID="txtCalificacion4" runat="server" MaxLength="10" Width="102px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 181px">
                                    &nbsp;</td>
                                <td bgcolor="#990000" style="font-weight: 700; color: #FFFFFF">
                                    Total</td>
                                <td style="width: 427px">
                                    <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: right; ">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center; ">
                                    <asp:UpdateProgress ID="UpdateProgress" runat="server" 
                                        AssociatedUpdatePanelID="UpdatePanel1">
                                    <ProgressTemplate>
                                        <div id="pnlSave" >
                                                <br /><p><img alt="Cargando Reporte" src="../../../Img/loading.gif" /> El Reporte se 
                                                esta generando.</p><br />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress></td>
                            </tr>
                        </table>
                
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: right; width: 154px">
                                    &nbsp;</td>
                                <td style="width: 215px; text-align: center;">
                                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" 
                                        ValidationGroup="Mars" CssClass="button" />
                                </td>
                                <td style="text-align: center">
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                                        CausesValidation="False" CssClass="button" />
                                </td>
                            </tr>
                        </table>               
                    </asp:Panel>  
                </ContentTemplate>
            </asp:UpdatePanel>       
        </div>
        
<!--CONTENT SIDE COLUMN-->
            
            
            
        <div class="clear">
        </div>
    </div>
</asp:Content>
