<%@ Page Language="vb" Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Administracion/AdministracionMars.Master"
    AutoEventWireup="false" 
    CodeBehind="AdminEntrenamientosMars.aspx.vb" 
    Inherits="procomlcd.AdminEntrenamientosMars"
    Title="Administración Mars" %>

<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdminMars" runat="Server">
  
<!--titulo-pagina -->
    <div id="titulo-pagina">
        Mars Autoservicio - Entrenamientos técnicos</div>    

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server"/>
           
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td bgcolor="#CCCCCC" style="height: 19px">
                                <asp:LinkButton ID="lnkNuevo" runat="server" Font-Bold="True">Nuevo</asp:LinkButton>
                            </td>
                            <td bgcolor="#CCCCCC" style="height: 19px; width: 164px;">
                                <asp:LinkButton ID="lnkConsultas" runat="server" Font-Bold="True" 
                                    Font-Italic="False">Consultas</asp:LinkButton>
                            </td>
                            <td style="height: 19px">
                                &nbsp;</td>
                            <td style="width: 115px; height: 19px;">
                                &nbsp;</td>
                            <td style="text-align: right; height: 19px;">
                                <img alt="" src="../Img/arrow.gif" />&nbsp;<a href="MenuAdminMars.aspx">Regresar</a>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:Label ID="lblAviso" runat="server" CssClass="aviso"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlConsulta" runat="server" Visible="False">
                        <asp:GridView ID="gridEntrenamiento" runat="server" AutoGenerateColumns="False" 
                            Height="16px" style="text-align: left" 
                            Width="100%" CssClass="grid-view" ShowFooter="True">
                            <Columns>       
                                <asp:CommandField ButtonType="Image" EditImageUrl="~/Img/Editar.png" 
                                    ShowEditButton="True" />
                                <asp:BoundField DataField="id_entrenamiento" HeaderText="ID Entrenamiento" />
                                <asp:BoundField DataField="nombre_entrenamiento" HeaderText="Nombre" />
                                <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha Inicio" DataFormatString="{0:d}"/>
                                <asp:BoundField DataField="fecha_fin" HeaderText="Fecha Fin" DataFormatString="{0:d}"/>
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                        </asp:GridView>
                    </asp:Panel>
                    <asp:Panel ID="pnlNuevo" runat="server" Visible="False" BorderWidth="1px">
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: right; width: 148px">
                                    Nombre:</td>
                                <td>
                                    <asp:TextBox ID="txtNombreEntrenamiento" runat="server" CssClass="cajastxt" MaxLength="100" 
                                        Width="245px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvNombreEntrenamiento" runat="server" 
                                        ControlToValidate="txtNombreEntrenamiento" ErrorMessage="*" ValidationGroup="Mars"></asp:RequiredFieldValidator>
                                    <asp:Label ID="lblIDEntrenamiento" runat="server" Font-Bold="True" 
                                        ForeColor="Black" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 148px" valign="top">
                                    Descripción:</td>
                                <td>
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="cajastxt" 
                                        Height="129px" MaxLength="100" TextMode="MultiLine" Width="474px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" 
                                        ControlToValidate="txtDescripcion" ErrorMessage="*" ValidationGroup="Mars"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 148px">
                                    Fecha Inicio:</td>
                                <td>
                                    <asp:TextBox ID="txtFechaInicio" runat="server" MaxLength="10" Width="102px"></asp:TextBox>
                                    <a href="javascript:;" onclick="window.open('../../../Calendario/popup.aspx?textbox=ctl00$ContenidoAdminMars$txtFechaInicio','cal','width=250,height=225,left=270,top=180')">
                                    <img alt="" border="0" src="../../../Img/SmallCalendar.gif" /></a><asp:RequiredFieldValidator 
                                        ID="FechaInicio" runat="server" ControlToValidate="txtFechaInicio" 
                                        ErrorMessage="*" ValidationGroup="Mars"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 148px">
                                    Fecha Fin:</td>
                                <td>
                                    <asp:TextBox ID="txtFechaFin" runat="server" MaxLength="10" Width="102px"></asp:TextBox>
                                    <a href="javascript:;" 
                                        onclick="window.open('../../../Calendario/popup.aspx?textbox=ctl00$ContenidoAdminMars$txtFechaFin','cal','width=250,height=225,left=270,top=180')">
                                    <img alt="" border="0" src="../../../Img/SmallCalendar.gif" /></a><asp:RequiredFieldValidator 
                                        ID="FechaFin" runat="server" ControlToValidate="txtFechaFin" 
                                        ErrorMessage="*" ValidationGroup="Mars"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 148px">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center; ">
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
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
                    </Triggers>
            </asp:UpdatePanel>       
                        <asp:GridView ID="gridRuta" runat="server" AutoGenerateColumns="False" 
                            Height="16px" style="text-align: left" Width="100%" 
                            Visible="False" CssClass="grid-view" ShowFooter="True">
                            <Columns>
                                <asp:BoundField HeaderText="Promotor" DataField="id_usuario"/> 
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                        </asp:GridView>
                        
        </div>
        
<!--CONTENT SIDE COLUMN-->
            
            
            
        <div class="clear">
        </div>
    </div>
</asp:Content>
