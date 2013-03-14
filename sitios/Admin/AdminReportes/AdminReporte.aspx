<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master"
    AutoEventWireup="false" 
    CodeBehind="AdminReporte.aspx.vb" 
    Inherits="procomlcd.AdminReporte" %>

<asp:Content id="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
    <!--
Title Under Menu  
--> 
    <div id="titulo-pagina">Administración reportes sitio web</div>

    <div id="contenido-derecha">
        
        <!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>
                
                    <div id="menu-edicion">
                        <ul><li><asp:LinkButton ID="lnkNuevo" runat="server">Nuevo</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lnkConsultas" runat="server">Consultas</asp:LinkButton></li>
                        </ul>
                    </div>
                
                    <div id="div-aviso">        
                        <asp:Label ID="lblAviso" runat="server" />
                    </div>
                
                <asp:Panel ID="pnlConsulta" runat="server" Visible="False">
                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario" CssClass="caja-texto-lbl" />
                    <asp:DropDownList ID="cmbUsuario" runat="server" AutoPostBack="True" CssClass="caja-texto-caja">
                        <asp:ListItem Value="0">Sin resolver</asp:ListItem>
                        <asp:ListItem Value="1">Resuelto</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    
                    <asp:Panel ID="pnlGrid" runat="server" CssClass="panel-grid" ScrollBars="Both">
                        <asp:GridView ID="gridConsultas" runat="server" ShowFooter="True" 
                            AutoGenerateColumns="False" Width="100%" 
                            CssClass="grid-view" GridLines="None">
                            <Columns>
                                <asp:CommandField ButtonType="Image" 
                                    EditImageUrl="~/Img/Editar.png" ShowEditButton="True" />
                                <asp:BoundField HeaderText="Reporte" DataField="id_reporte"/>
                                <asp:BoundField HeaderText="Fecha" DataField="fecha" DataFormatString="{0:d}" />
                                <asp:BoundField HeaderText="Usuario" DataField="id_usuario"/>
                                <asp:BoundField HeaderText="Nombre" DataField="nombre"/>
                                <asp:BoundField HeaderText="Fecha Leido" DataField="fecha_leido" 
                                    DataFormatString="{0:d}" />
                                <asp:BoundField HeaderText="Fecha resuelto" DataField="fecha_resuelto" 
                                    DataFormatString="{0:d}" />
                                <asp:BoundField HeaderText="Soporte" DataField="soporte"/> 
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                        </asp:GridView>
                    </asp:Panel>
                </asp:Panel><br />
                
                 <asp:Panel ID="pnlConsultas" runat="server" Width="100%" Visible="False" 
                    style="text-align: left">
                     <asp:Panel ID="pnlDatos" runat="server">
                        <table style="width: 100%">
                             <tr>
                                 <td>
                                     Reporte</td>
                                 <td>
                                     <asp:Label ID="lblIDReporte" runat="server" Font-Bold="True" ForeColor="Black" />
                                 </td>
                                 <td>
                                     Fecha reporte</td>
                                 <td>
                                     <asp:Label ID="lblFecha" runat="server" Font-Bold="True" ForeColor="Black" />
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     Usuario</td>
                                 <td>
                                     <asp:Label ID="lblIDUsuario" runat="server" Font-Bold="True" ForeColor="Black" />
                                 </td>
                                 <td>
                                     Nombre</td>
                                 <td>
                                     <asp:Label ID="lblNombre" runat="server" Font-Bold="True" ForeColor="Black" />
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     Correo</td>
                                 <td>
                                     &nbsp;<asp:LinkButton ID="lnkCorreo" runat="server" Font-Bold="True">lnkCorreo</asp:LinkButton>
                                 </td>
                                 <td>
                                     Teléfono</td>
                                 <td>
                                     (<asp:Label ID="lblLada" runat="server" Font-Bold="True" ForeColor="Black" />
                                     ) -<asp:Label ID="lblTelefono" runat="server" Font-Bold="True" 
                                         ForeColor="Black" />
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     Problema</td>
                                 <td colspan="3">
                                     <asp:Label ID="lblProblema" runat="server" Font-Bold="True" ForeColor="Black" />
                                 </td>
                             </tr>
                        </table>
                     </asp:Panel>
                     <asp:Panel ID="pnlNuevo" runat="server">
                        <table style="width: 100%">
                             <tr>
                                 <td>
                                     Usuario</td>
                                 <td>
                                     <asp:TextBox ID="txtUsuario" runat="server" MaxLength="15" 
                                         CssClass="caja-texto-caja" />
                                 </td>
                                 <td>
                                     Nombre</td>
                                 <td>
                                     <asp:TextBox ID="txtNombre" runat="server" Width="280px" MaxLength="50" 
                                         CssClass="caja-texto-caja" />
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     Correo</td>
                                 <td>
                                     <asp:TextBox ID="txtCorreo" runat="server" Width="215px" MaxLength="40" 
                                         CssClass="caja-texto-caja" />
                                 </td>
                                 <td>
                                     Teléfono</td>
                                 <td>
                                     (<asp:TextBox ID="txtLada" runat="server" Width="57px" MaxLength="4" 
                                         CssClass="caja-texto-caja" />
                                     ) -<asp:TextBox ID="txtTelefono" runat="server" MaxLength="10" 
                                         CssClass="caja-texto-caja" />
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     Problema</td>
                                 <td colspan="3">
                                     <asp:TextBox ID="txtProblema" runat="server" Height="68px" TextMode="MultiLine" 
                                         Width="576px" MaxLength="300" CssClass="caja-texto-caja" />
                                 </td>
                             </tr>
                         </table>
                    </asp:Panel>
                        <br />
                        <table>
                             <tr>
                                 <td>Dirigido a</td>
                                 <td>
                                     <asp:DropDownList ID="cmbDirigido" runat="server" AutoPostBack="True" 
                                         Height="23px" Width="156px" CssClass="caja-texto-caja">
                                     </asp:DropDownList>
                                 </td>
                                 <td>
                                     <asp:LinkButton ID="lnkCorreoDirigido" runat="server" Font-Bold="True">quia.gdl@e-shelby.com</asp:LinkButton>
                                 </td>
                                 <td>&nbsp;</td>
                             </tr>
                             <tr>
                                 <td>Fecha leido</td>
                                 <td>
                                     <asp:Label ID="lblFechaLeido" runat="server" Font-Bold="True" ForeColor="Black" />
                                 </td>
                                 <td>
                                     Fecha resuelto</td>
                                 <td>
                                     <asp:Label ID="lblFechaResuelto" runat="server" Font-Bold="True" 
                                         ForeColor="Black" />
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     Estatus</td>
                                 <td>
                                     <asp:DropDownList ID="cmbEstatus" runat="server" AutoPostBack="True" 
                                         CssClass="caja-texto-caja">
                                         <asp:ListItem Value="0">Sin resolver</asp:ListItem>
                                         <asp:ListItem Value="1">Resuelto</asp:ListItem>
                                     </asp:DropDownList>
                                 </td>
                                 <td>
                                     &nbsp;</td>
                                 <td>
                                     &nbsp;</td>
                             </tr>
                             <tr>
                                 <td>
                                     Comentarios</td>
                                 <td colspan="3">
                                     <asp:TextBox ID="txtResuelto" runat="server" Height="65px" MaxLength="500" 
                                         TextMode="MultiLine" Width="567px" CssClass="caja-texto-caja" />
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     &nbsp;</td>
                                 <td>
                                     &nbsp;</td>
                                 <td style="text-align: center">
                                     <asp:Button ID="btnGuardar" runat="server" CssClass="button" Text="Guardar" 
                                         ValidationGroup="" />
                                 </td>
                                 <td style="text-align: center">
                                     <asp:Button ID="btnCancelar" runat="server" CssClass="button" 
                                         style="text-align: center" Text="Cancelar" ValidationGroup="" />
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     &nbsp;</td>
                                 <td>
                                     &nbsp;</td>
                                 <td>
                                     &nbsp;</td>
                                 <td>
                                     &nbsp;</td>
                             </tr>
                         </table>
                </asp:Panel>
             </ContentTemplate>
             </asp:UpdatePanel>
        
        </div>
        <!-- END MAIN COLUMN -->
        <!--

  CONTENT SIDE 2 COLUMN


  -->
        <div id="contenido-dos-columnas-2">
            <ul class="list-of-links">
                <li><asp:LinkButton ID="lnkAvisos1" runat="server" Font-Bold="True">Reportes sin leer</asp:LinkButton></li>
                <li><asp:LinkButton ID="lnkAvisos2" runat="server" Font-Bold="True">Reportes sin resolver</asp:LinkButton></li>
                <li><asp:LinkButton ID="lnkAvisos3" runat="server" Font-Bold="True">Reportes resueltos</asp:LinkButton></li>
            </ul>
        </div>
        
        <div class="clear"></div>
    </div>
</asp:Content>
