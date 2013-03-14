<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master" 
    AutoEventWireup="false" 
    CodeBehind="AdminRutasMarsMay.aspx.vb" 
    Inherits="procomlcd.AdminRutasMarsMay"
    Title="Administración Mars Mayoreo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
  
<!--titulo-pagina-->
    <div id="titulo-pagina">
        Rutas de promotores Mars Mayoreo</div>
        
<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
            <asp:ScriptManager ID="ScriptManager" runat="server"/>
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>
                    <div id="div-aviso">        
                        <asp:Label ID="lblAviso" runat="server" />
                    </div>
            
                    <asp:Panel ID="pnlPromotor" runat="server">
                        <asp:Label ID="lblRegion" runat="server" Text="Región" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbRegion" runat="server" AutoPostBack="True" 
                            CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblPromotor" runat="server" Text="Promotor" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbPromotor" runat="server" AutoPostBack="True" 
                            CssClass="caja-texto-caja" />
                        <br />
                    </asp:Panel>
                    
                    <asp:Panel ID="pnlRuta" runat="server"  ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridRuta" runat="server" AutoGenerateColumns="False" 
                             GridLines="None" Width="100%" 
                            DataKeyNames="id_tienda" CssClass="grid-view" ShowFooter="True">
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" ButtonType="Image" 
                                    DeleteImageUrl="~/Img/delete-icon.png" />
                                <asp:BoundField DataField="id_usuario" HeaderText="Promotor" />
                                <asp:BoundField DataField="nombre" HeaderText="Tienda" />
                                <asp:BoundField DataField="id_tienda" HeaderText="ID Tienda" />
                                <asp:BoundField DataField="nombre_cadena" HeaderText="Cadena" />  
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="D">
                                    <ItemTemplate>                                    
                                        <asp:CheckBox ID="chkW1_1" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W1_1")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="L">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW1_2" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W1_2")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="M">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW1_3" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W1_3")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="I">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW1_4" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W1_4")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="J">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW1_5" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W1_5")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="V">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW1_6" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W1_6")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="S">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW1_7" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W1_7")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="D">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW2_1" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W2_1")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="L">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW2_2" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W2_2")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="M">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW2_3" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W2_3")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="I">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW2_4" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W2_4")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="J">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW2_5" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W2_5")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="V">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW2_6" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W2_6")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="S">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW2_7" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W2_7")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="D">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW3_1" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W3_1")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="L">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW3_2" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W3_2")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="M">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW3_3" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W3_3")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="I">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW3_4" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W3_4")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="J">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW3_5" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W3_5")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="V">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW3_6" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W3_6")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="S">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW3_7" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W3_7")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="D">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW4_1" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W4_1")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="L">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW4_2" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W4_2")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="M">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW4_3" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W4_3")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="I">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW4_4" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W4_4")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="J">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW4_5" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W4_5")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="V">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW4_6" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W4_6")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="XX-Small" HeaderText="S">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkW4_7" runat="server" 
                                            Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "W4_7")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>                    
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                        </asp:GridView>
                    </asp:Panel>
                    
                    <asp:Button ID="btnGuardarCambios" runat="server" CssClass="button" 
                        Enabled="False" Text="Guardar cambios" Width="119px" />
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar Tienda" Width="119px" 
                        CssClass="button" Enabled="False" />           
            
                    <asp:Panel ID="pnlAgregar" runat="server" Visible="False">
                        <asp:Label ID="lblEstado" runat="server" Text="Estado" CssClass="caja-texto-lbl" />
                        <asp:ListBox ID="ListEstado" runat="server" CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblCadena" runat="server" Text="Cadena" CssClass="caja-texto-lbl" />
                        <asp:ListBox ID="ListCadena" runat="server" CssClass="caja-texto-caja" />
                        <br />
                        
                        <asp:Label ID="lblTienda" runat="server" Text="Tienda" CssClass="caja-texto-lbl" />
                        <asp:ListBox ID="ListTienda" runat="server" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvTienda" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="ListTienda" ErrorMessage="*Selecciona la tienda" 
                            Font-Bold="True" ValidationGroup="Tienda" />
                        <br />
                        
                        <br />
                        <asp:Label ID="lblSemana1" runat="server" Text="Semana 1" CssClass="texto-lbl" /><br />
                        <asp:CheckBox ID="chkW1_1" runat="server" Checked="false" Text="Domingo" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW1_2" runat="server" Checked="false" Text="Lunes" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW1_3" runat="server" Checked="false" Text="Martes" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW1_4" runat="server" Checked="false" Text="Miércoles" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW1_5" runat="server" Checked="false" Text="Jueves" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW1_6" runat="server" Checked="false" Text="Viernes" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW1_7" runat="server" Checked="false" Text="Sábado" CssClass="texto-chk" />
                        
                        <br /><br />
                        <asp:Label ID="lblSemana2" runat="server" Text="Semana 2" CssClass="texto-lbl" /><br />
                        <asp:CheckBox ID="chkW2_1" runat="server" Checked="false" Text="Domingo" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW2_2" runat="server" Checked="false" Text="Lunes" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW2_3" runat="server" Checked="false" Text="Martes" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW2_4" runat="server" Checked="false" Text="Miércoles" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW2_5" runat="server" Checked="false" Text="Jueves" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW2_6" runat="server" Checked="false" Text="Viernes" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW2_7" runat="server" Checked="false" Text="Sábado" CssClass="texto-chk" />
                        
                        <br /><br />
                        <asp:Label ID="lblSemana3" runat="server" Text="Semana 3" CssClass="texto-lbl" /><br />
                        <asp:CheckBox ID="chkW3_1" runat="server" Checked="false" Text="Domingo" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW3_2" runat="server" Checked="false" Text="Lunes" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW3_3" runat="server" Checked="false" Text="Martes" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW3_4" runat="server" Checked="false" Text="Miércoles" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW3_5" runat="server" Checked="false" Text="Jueves" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW3_6" runat="server" Checked="false" Text="Viernes" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW3_7" runat="server" Checked="false" Text="Sábado" CssClass="texto-chk" />
                        
                        <br /><br />
                        <asp:Label ID="lblSemana4" runat="server" Text="Semana 4" CssClass="texto-lbl" /><br />
                        <asp:CheckBox ID="chkW4_1" runat="server" Checked="false" Text="Domingo" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW4_2" runat="server" Checked="false" Text="Lunes" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW4_3" runat="server" Checked="false" Text="Martes" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW4_4" runat="server" Checked="false" Text="Miércoles" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW4_5" runat="server" Checked="false" Text="Jueves" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW4_6" runat="server" Checked="false" Text="Viernes" CssClass="texto-chk" />
                        <asp:CheckBox ID="chkW4_7" runat="server" Checked="false" Text="Sábado" CssClass="texto-chk" />
                        
                        <asp:Label ID="lblGuardado" runat="server" CssClass="aviso" />
                        
                        <asp:Button ID="btnAceptar" runat="server" CssClass="button" 
                            Text="Agrega Tienda" ValidationGroup="Tienda" />
                        <asp:Button ID="btnTerminar" runat="server" CausesValidation="False" 
                            CssClass="button" Text="Terminar" />
                    </asp:Panel>
                    
                    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updPnl">
                        <ProgressTemplate>
                            <div id="pnlSave" >
                                <<img alt="Cargando..." src="~/Img/loading.gif" /> 
                                Cargando información.
                            </div>
                         </ProgressTemplate>
                    </asp:UpdateProgress>
                </ContentTemplate>
            </asp:UpdatePanel> 
        </div>
        
<!--CONTENT SIDE COLUMN-->
        <div id="contenido-dos-columnas-2">
            <h2>Generales</h2>
            <ul class="list-of-links">
                <li><a href="/sitios/Admin/Mars/AdminUsuariosMars.aspx">Usuarios</a></li>
                <li><a href="/sitios/Admin/Mars/AdminCadenasMars.aspx">Cadenas</a></li>
                <li><a href="/sitios/Admin/Mars/AdminCadenasMayoreoMars.aspx">Cadenas mayoreo</a></li>
                <li><a href="/sitios/Admin/Mars/AdminRegionesMars.aspx">Regiones</a></li>
                <li><a href="/sitios/Admin/Mars/AdminPeriodosMars.aspx">Periodos</a></li>
            </ul>
            
            <h2>Por proyecto</h2>
                <asp:GridView ID="gridProyectos" runat="server" AutoGenerateColumns="False" 
                    GridLines="None" Height="16px" ShowHeader="False" Width="165px" 
                    CellPadding="3" style="text-align: center">
                    <RowStyle ForeColor="#000066" HorizontalAlign="Left" />
                   <Columns>
                     <asp:TemplateField HeaderText="" >
                       <ItemTemplate>
                        <ul class="list-of-links">
                            <a href='<%#Eval("ruta")%>'><%#Eval("pagina")%></a> 
                        </ul>                           </ItemTemplate></asp:TemplateField>
                   </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="lblAvisos" runat="server" Font-Bold="True" ForeColor="Red" 
                                Text="No hay Avisos Actuales" />
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle HorizontalAlign="Left" />
                </asp:GridView>
            <br />
        </div>
        
        <div class="clear"></div>
    </div>
    </div>
</asp:Content>
