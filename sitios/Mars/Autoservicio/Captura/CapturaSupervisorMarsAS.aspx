<%@ Page Language="vb" 
    Culture="es-MX" 
    MasterPageFile="~/sitios/Mars/Autoservicio/Mars_Autoservicio.Master" 
    AutoEventWireup="false" 
    CodeBehind="CapturaSupervisorMarsAS.aspx.vb" 
    Inherits="procomlcd.CapturaSupervisorMarsAS"
    title="Mars Autoservicio - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsAutoservicio" runat="Server">

 
<!--titulo-pagina-->
    <div id="titulo-pagina">Captura información precios</div>

<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
                <table style="width: 100%">
                    <tr>
                        <td style="width: 434px">
                            &nbsp;</td>
                        <td style="text-align: right">
                            <img alt="" src="../../../../Img/arrow.gif" /><asp:HyperLink 
                            ID="lnkRegresar" runat="server" 
                            NavigateUrl="~/sitios/Mars/Autoservicio/Captura/RutaMarsAS.aspx">Regresar</asp:HyperLink></td>
                    </tr>
                    </table>
                
                <asp:GridView ID="gridProcesos" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="id_proceso" Width="99%" style="text-align: center" ShowFooter="True" 
                    Caption="ALINEACIONES E IMPLEMENTACIONES" CssClass="grid-view" CellPadding="4" 
                ForeColor="#333333" GridLines="None">
                    <RowStyle HorizontalAlign="Left" BackColor="#FFFBD6" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField HeaderText="" DataField="nombre_proceso" />
                        <asp:BoundField HeaderText="" DataField="nombre_tipoproceso" />
                        <asp:TemplateField HeaderText="Promotor">
                            <ItemTemplate>
                                <asp:Label ID="lblUsuario" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id_usuario") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha (dd/mm/aaaa)">
                            <ItemTemplate>
                                <asp:TextBox ID="txtFecha" runat="server" MaxLength="10" Width="100" 
                                    Text='<%#DataBinder.Eval(Container.DataItem, "fecha","{0:d}")%>'></asp:TextBox>
                                <asp:RegularExpressionValidator id="rvaFecha" runat="server" ErrorMessage="La fecha no es correcta formato 'dd/mm/aaaa'" Display="Dynamic" 
                                    ValidationExpression="\d{2}/\d{2}/\d{4}" ControlToValidate="txtFecha" ValidationGroup="Mars" >*</asp:RegularExpressionValidator>
                                <asp:RangeValidator ID="rvFecha" runat="server" ErrorMessage="La fecha no es valida" 
                                    ValidationGroup="Mars" ControlToValidate="txtFecha"
                                    MaximumValue="31/12/3000" MinimumValue="01/01/2010" Type="Date">*</asp:RangeValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        <h1>No hay implementacios ni alineaciones actuales</h1>
                    </EmptyDataTemplate>
                    <FooterStyle Font-Bold="True" ForeColor="White" CssClass="grid-footer" 
                        BackColor="#990000" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle HorizontalAlign="Left" BackColor="White" />
                </asp:GridView>

                <br />
                
                <asp:GridView ID="gridEntrenamiento" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="id_entrenamiento" Width="99%" style="text-align: center" ShowFooter="True" 
                    Caption="ENTRENAMIENTOS TÉCNICOS" CssClass="grid-view" CellPadding="4" 
                ForeColor="#333333" GridLines="None">
                    <RowStyle HorizontalAlign="Left" BackColor="#FFFBD6" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField HeaderText="" DataField="nombre_entrenamiento" />
                        <asp:TemplateField HeaderText="Promotor">
                            <ItemTemplate>
                                <asp:Label ID="lblUsuario" runat="server" 
                                    Text='<%# DataBinder.Eval(Container.DataItem, "id_usuario") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha (dd/mm/aaaa)">
                            <ItemTemplate>
                                <asp:TextBox ID="txtFecha" runat="server" MaxLength="10" Width="100" 
                                    Text='<%#DataBinder.Eval(Container.DataItem, "fecha","{0:d}")%>'></asp:TextBox>
                                <asp:RegularExpressionValidator id="rvaFecha" runat="server" ErrorMessage="La fecha no es correcta formato 'dd/mm/aaaa'" Display="Dynamic" 
                                    ValidationExpression="\d{2}/\d{2}/\d{4}" ControlToValidate="txtFecha" ValidationGroup="Mars" >*</asp:RegularExpressionValidator>
                                <asp:RangeValidator ID="rvFecha" runat="server" ErrorMessage="La fecha no es valida" 
                                    ValidationGroup="Mars" ControlToValidate="txtFecha"
                                    MaximumValue="31/12/3000" MinimumValue="01/01/2010" Type="Date">*</asp:RangeValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        <h1>No hay entrenamientos técnicos actuales</h1>
                    </EmptyDataTemplate>
                    <FooterStyle Font-Bold="True" ForeColor="White" CssClass="grid-footer" 
                        BackColor="#990000" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle HorizontalAlign="Left" BackColor="White" />
                </asp:GridView>

                  <br />
                                 
                    <asp:UpdatePanel ID="UpdatePanel" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%">
                               <tr>
                                   <td style="text-align: center; height: 31px; " colspan="2">
                                   
                                       <asp:ValidationSummary ID="vsMars" runat="server" 
                                           ValidationGroup="Mars" />
                                           <asp:Label ID="lblFecha" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                   <asp:UpdateProgress ID="UpdateProgress" runat="server" 
                                           AssociatedUpdatePanelID="UpdatePanel" DisplayAfter="0">
                                           <ProgressTemplate>
                                              <div id="pnlSave" >
                                                   <img alt="" src="../../../../Img/loading.gif" />La información se esta guardando<br />
                                                       Por favor espera...<br />
                                                       <br />
                                                       <br />
                                                </div>
                                           </ProgressTemplate>
                                       </asp:UpdateProgress>
                                   </td>
                               </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" 
                                            ValidationGroup="Mars" CssClass="button" />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                                            Text="Cancelar" CssClass="button" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>  
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
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
