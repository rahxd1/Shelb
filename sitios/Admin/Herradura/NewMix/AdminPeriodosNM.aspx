<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="~/sitios/Admin/AdminMaster.Master"
    AutoEventWireup="false" 
    CodeBehind="AdminPeriodosNM.aspx.vb" 
    Inherits="procomlcd.AdminPeriodosNM"
    Title="Administración New Mix" %>

<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
  
<!--titulo-pagina -->
    <div id="titulo-pagina">
       Periodos New Mix</div>    

<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
            <asp:ScriptManager ID="ScriptManager" runat="server"/>
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>
                
                    <div id="menu-edicion">
                        <ul>
                            <li><asp:LinkButton ID="lnkNuevo" runat="server" Font-Bold="False">Nuevo</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lnkConsultas" runat="server" Font-Bold="False">Consultas</asp:LinkButton></li>
                        </ul>
                    </div>
                    
                    <div id="div-aviso">        
                        <asp:Label ID="lblAviso" runat="server" />
                    </div>
                    
                    <asp:Panel ID="pnlConsulta" runat="server"  ScrollBars="Both"
                        CssClass="panel-grid">
                        <asp:GridView ID="gridPeriodo" runat="server" AutoGenerateColumns="False" 
                             GridLines="None" Width="100%" CssClass="grid-view" ShowFooter="True">
                            <Columns>       
                                <asp:CommandField ButtonType="Image" EditImageUrl="~/Img/Editar.png" 
                                    ShowEditButton="True" />
                                <asp:BoundField DataField="id_periodo" HeaderText="ID Perido" />
                                <asp:BoundField DataField="nombre_periodo" HeaderText="Periodo" />
                                <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha inicio" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="fecha_cierre" HeaderText="Fecha cierre" DataFormatString="{0:d}" />
                            </Columns>
                            <FooterStyle CssClass="grid-footer" />
                        </asp:GridView>
                    </asp:Panel>
                    
                    <asp:Panel ID="pnlNuevo" runat="server" Visible="False">
                        <asp:Label ID="lblIDPeriodo" runat="server" Visible="false" /><br />
                        
                        <asp:Label ID="lblNombrePeriodo" runat="server" Text="Nombre periodo" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtNombrePeriodo" runat="server" Width="350px"
                            MaxLength="50" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvNombrePeriodo" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="txtNombrePeriodo" ErrorMessage="Nombre periodo" ValidationGroup="Periodo">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblMes" runat="server" Text="Mes" CssClass="caja-texto-lbl" />
                        <asp:DropDownList ID="cmbMes" runat="server" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvMes" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="cmbMes" ErrorMessage="Mes" ValidationGroup="Periodo">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblAnio" runat="server" Text="Año" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtAnio" runat="server" Width="350px"
                            MaxLength="50" CssClass="caja-texto-caja" />
                        <asp:RequiredFieldValidator ID="rfvAnio" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="txtAnio" ErrorMessage="Año" ValidationGroup="Periodo">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblFechaInicio" runat="server" Text="Fecha inicio" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtFechaInicio" runat="server" MaxLength="10" 
                            CssClass="caja-texto-caja" />
                        <a href="javascript:;" onclick="window.open('../../../App_class/Calendario/popup.aspx?textbox=ctl00$ContenidoAdministracion$txtFechaInicio','cal','width=250,height=225,left=270,top=180')">
                        <img alt="" border="0" src="../../../../Img/SmallCalendar.gif" /></a>
                        <asp:RequiredFieldValidator CssClass="caja-texto-rf"
                            ID="FechaInicio" runat="server" ControlToValidate="txtFechaInicio" 
                            ErrorMessage="Fecha inicio" ValidationGroup="Periodo">*</asp:RequiredFieldValidator>
                        <br />
                                                      
                        <asp:Label ID="lblFechaFin" runat="server" Text="Fecha fin" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtFechaFin" runat="server" MaxLength="10" 
                            CssClass="caja-texto-caja" />
                        <a href="javascript:;" onclick="window.open('../../../App_class/Calendario/popup.aspx?textbox=ctl00$ContenidoAdministracion$txtFechaFin','cal','width=250,height=225,left=270,top=180')">
                        <img border="0" src="../../../../Img/SmallCalendar.gif" /></a>
                        <asp:RequiredFieldValidator CssClass="caja-texto-rf"
                            ID="rfvFechaFin" runat="server" ControlToValidate="txtFechaFin" 
                            ErrorMessage="Fecha fin" ValidationGroup="Periodo">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblFechaCierre" runat="server" Text="Fecha cierre" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtFechaCierre" runat="server" MaxLength="10" 
                            CssClass="caja-texto-caja" />
                        <a href="javascript:;" onclick="window.open('../../../App_class/Calendario/popup.aspx?textbox=ctl00$ContenidoAdministracion$txtFechaCierre','cal','width=250,height=225,left=270,top=180')">
                        <img border="0" src="../../../../Img/SmallCalendar.gif" /></a>
                        <asp:RequiredFieldValidator CssClass="caja-texto-rf"
                            ID="rfvtxtFechaCierre" runat="server" ControlToValidate="txtFechaCierre" 
                            ErrorMessage="Fecha cierre" ValidationGroup="Periodo">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:ValidationSummary ID="vsPeriodo" runat="server" CssClass="aviso"
                            HeaderText="Completa los siguientes campos:" ShowMessageBox="True" 
                            ValidationGroup="Periodo" />  
                        <br />
                        
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" ValidationGroup="Periodo" CssClass="button" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                            CausesValidation="False" CssClass="button" />               
                    </asp:Panel>  
                    
                    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updPnl">
                        <ProgressTemplate>
                            <div id="pnlSave" >
                                <img alt="Cargando..." src="~/Img/loading.gif" /> 
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
                <li><a href="/sitios/Admin/Herradura/AdminUsuariosHerradura.aspx">Usuarios</a></li>
                <li><a href="/sitios/Admin/Herradura/AdminCadenasHerradura.aspx">Cadenas</a></li>
                <li><a href="/sitios/Admin/Herradura/AdminRegionesHerradura.aspx">Regiones</a></li>
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
</asp:Content>
