<%@ Page Language="vb" 
    Culture="es-MX" 
    masterpagefile="../AdminMaster.master"
    AutoEventWireup="false" 
    CodeBehind="AdminPeriodosFluid.aspx.vb" 
    Inherits="procomlcd.AdminPeriodosFluid"
    Title="Administración Fluidmaster" %>

<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdministracion" runat="Server">
  
<!--titulo-pagina -->
    <div id="titulo-pagina">
        Catalogo de periodos Fluidmaster</div>    

<!--CONTENT CONTAINER-->
    <div id="contenido-derecha">

<!--CONTENT MAIN COLUMN-->
        <div id="contenido-dos-columnas-1">
            <asp:ScriptManager ID="ScriptManager" runat="server"/>
            
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>
                    <div ID="menu-edicion">
                        <ul>
                            <li><asp:LinkButton ID="lnkNuevo" runat="server">Nuevo</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lnkConsultas" runat="server">Consultas</asp:LinkButton></li>
                        </ul>
                    </div>
                    
                    <div id="div-aviso">        
                        <asp:Label ID="lblAviso" runat="server" />
                    </div>
            
                    <asp:Panel ID="pnlConsulta" runat="server" ScrollBars="Both" CssClass="panel-grid">
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
                        <asp:Label ID="lblNombrePeriodo" runat="server" Text="Nombre periodo" 
                            CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtNombrePeriodo" runat="server" CssClass="caja-texto-caja" 
                            MaxLength="40" Width="350px" />
                        <asp:RequiredFieldValidator ID="rfvNombrePeriodo" runat="server" CssClass="caja-texto-rf"
                            ControlToValidate="txtNombrePeriodo" ErrorMessage="Nombre periodo" ValidationGroup="Periodo">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblFechaInicio" runat="server" Text="Fecha inicio" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtFechaInicio" runat="server" MaxLength="10" CssClass="caja-texto-caja" />
                        <a href="javascript:;" onclick="window.open('../../../App_class/Calendario/popup.aspx?textbox=ctl00$ContenidoAdministracion$txtFechaInicio','cal','width=250,height=225,left=270,top=180')">
                        <img alt="" border="0" src="../../../Img/SmallCalendar.gif" /></a>
                        <asp:RequiredFieldValidator ID="FechaInicio" runat="server" ControlToValidate="txtFechaInicio" CssClass="caja-texto-rf"
                            ErrorMessage="Fecha inicio" ValidationGroup="Periodo">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblFechaFin" runat="server" Text="Fecha fin" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtFechaFin" runat="server" MaxLength="10" CssClass="caja-texto-caja" />
                        <a href="javascript:;" onclick="window.open('../../../App_class/Calendario/popup.aspx?textbox=ctl00$ContenidoAdministracion$txtFechaFin','cal','width=250,height=225,left=270,top=180')">
                        <img border="0" src="../../../Img/SmallCalendar.gif" /></a>
                        <asp:RequiredFieldValidator ID="rfvFechaFin" runat="server" ControlToValidate="txtFechaFin" CssClass="caja-texto-rf"
                            ErrorMessage="Fecha fin" ValidationGroup="Periodo">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:Label ID="lblFechaCierre" runat="server" Text="Fecha cierre" CssClass="caja-texto-lbl" />
                        <asp:TextBox ID="txtFechaCierre" runat="server" MaxLength="10" CssClass="caja-texto-caja" />
                        <a href="javascript:;" onclick="window.open('../../../App_class/Calendario/popup.aspx?textbox=ctl00$ContenidoAdministracion$txtFechaCierre','cal','width=250,height=225,left=270,top=180')">
                        <img border="0" src="../../../Img/SmallCalendar.gif" /></a>
                        <asp:RequiredFieldValidator ID="rfvFechaCierre" runat="server" ControlToValidate="txtFechaCierre" CssClass="caja-texto-rf"
                            ErrorMessage="Fecha cierre" ValidationGroup="Periodo">*</asp:RequiredFieldValidator>
                        <br />
                        
                        <asp:ValidationSummary ID="vsPeriodo" runat="server" CssClass="aviso"
                            HeaderText="Completa los siguientes campos:" ShowMessageBox="True" 
                            ValidationGroup="Periodo" />  
                        <br />
                        
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" ValidationGroup="Periodo" CssClass="button" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="False" CssClass="button" />           
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
                <li><a href="AdminUsuariosFluid.aspx">Usuarios</a></li>
                <li><a href="AdminTiendasFluid.aspx">Tiendas</a></li>
                <li><a href="AdminCadenasFluid.aspx">Distribuidor</a></li>
                <li><a href="AdminRegionesFluid.aspx">Regiones</a></li>
                <li><a href="AdminPeriodosFluid.aspx">Periodos</a></li>
            </ul>
            
            <h2>Por proyecto</h2>
            <ul class="list-of-links">
                <li><a href="AdminRutasFluid.aspx">Editar rutas</a></li>
            </ul>
            <br />
        </div>
        
        <div class="clear"/>
    </div>
    </div>
</asp:Content>
