<%@ Page Language="vb" Culture="es-MX" 
    masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion.master"
    AutoEventWireup="false" 
    CodeBehind="ResultadosMars.aspx.vb" 
    Inherits="procomlcd.ResultadosMars" %>

<asp:Content id="Content1" ContentPlaceHolderID="ContentPlaceHolderCapacitacion" runat="Server">
    <!--
Title Under Menu
-->
    <div id="titulo-pagina">
        Resultados</div>
    <div id="contenido-three-column">
        <!--

  CONTENT SIDE 1 COLUMN

  -->
        <!--

  CENTER COLUMN

  -->
        <div id="content-side1-three-column" >
            <ul class="list-of-links">
                <li><asp:LinkButton ID="lnkCalificaciones" runat="server">Calificaciones</asp:LinkButton></li>
                <li><asp:LinkButton ID="lnkDetalle" runat="server">Detalle Examenes</asp:LinkButton></li>
            </ul>
        </div>
        
        <div id="content-main-three-column">
            <asp:Panel ID="pnlUsuarios" runat="server" style="text-align: center" 
                Width="536px" Visible="False">
            <asp:Panel ID="pnlEjecutivoCuenta" runat="server" Height="33px" Width="490px">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 107px; text-align: left;">Ejecutivo Cuenta</td>
                        <td style="width: 360px">
                            <asp:DropDownList ID="cmbEjecutivoCuenta" runat="server" AutoPostBack="True" Height="20px" Width="370px"></asp:DropDownList>
                        </td>
                    </tr>
                    </table>
            </asp:Panel>
            
             <asp:Panel ID="pnlEjecutivo" runat="server" Width="490px">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 107px; text-align: left;">Ejecutivo</td>
                        <td style="width: 360px">
                            <asp:DropDownList ID="cmbEjecutivo" runat="server" AutoPostBack="True" Height="20px" Width="370px"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            
            <asp:Panel ID="pnlSupervisor" runat="server" Width="490px">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 107px; text-align: left;">Supervisor</td>
                        <td style="width: 360px">
                            <asp:DropDownList ID="cmbSupervisor" runat="server" Height="20px" Width="370px" AutoPostBack="True"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            
            <asp:Panel ID="pnlPromotor" runat="server" Width="490px" >
                <table style="width: 100%">
                    <tr>
                        <td style="width: 107px; text-align: left;">Promotor</td>
                        <td style="width: 360px">
                            <asp:DropDownList ID="cmbPromotor" runat="server" Height="20px" Width="370px" AutoPostBack="True"></asp:DropDownList>
                        </td>
                    </tr>
                 </table>
            </asp:Panel>
                    <table class="style5">
                        <tr>
                            <td>
                                <asp:Button ID="btnVer" runat="server" Text="Ver" Width="81px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="upCalificaciones" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                                        </asp:ScriptManager>
                                        <asp:GridView ID="gridCalificaciones" runat="server" 
                                            AutoGenerateColumns="False" Caption="MODULOS" CellPadding="4" 
                                            DataKeyNames="id_modulo" ForeColor="#333333" GridLines="None" Height="16px" 
                                            ShowFooter="True" style="text-align: center" Width="57%">
                                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="nombre_modulo" HeaderText="Modulo" />
                                                <asp:BoundField DataField="calificacion" HeaderText="Calificación" />
                                            </Columns>
                                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                            <EmptyDataTemplate>
                                                <h1>
                                                    Selecciona el Promotor</h1>
                                            </EmptyDataTemplate>
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                        <br />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                </table>
            </asp:Panel>
            
            <asp:Panel ID="pnlResultados" runat="server" Visible="False">
                <h1>Detalle Examenes</h1>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                
            </asp:Panel>
            
            <asp:Panel ID="pnlImagen" runat="server" style="text-align: center">
                <img alt="" src="Imagenes/superpromotor.jpg" /><br />
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
