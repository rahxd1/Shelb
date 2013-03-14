<%@ Page Language="vb" Culture="es-MX" 
    masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion.master"
    AutoEventWireup="false" 
    CodeBehind="CapacitacionMars.aspx.vb" 
    Inherits="procomlcd.CapacitacionMars" %>

<asp:Content id="Content1" ContentPlaceHolderID="ContentPlaceHolderCapacitacion" runat="Server">
    <!--
Title Under Menu
-->
    <div id="titulo-pagina">
        Capacitación</div>
    <div id="contenido-three-column">
        <!--

  CONTENT SIDE 1 COLUMN

  -->
        <div id="content-side1-three-column" >
            <ul class="list-of-links">
                <li><asp:LinkButton ID="lnkValidaFechas" runat="server">Validar Fechas</asp:LinkButton></li>
                <li><asp:LinkButton ID="lnkPromotor" runat="server">Bajar Material</asp:LinkButton></li>
                <li><a target="_blank" href="http://100certificado.com/evaluacion_mod1/">Certificación Modulo 1</a> </li>
                <li><a target="_blank" href="http://100certificado.com/evaluacion_mod2/">Certificación Modulo 2</a> </li>
                <li><a target="_blank" href="http://100certificado.com/evaluacion_mod3/">Certificación Modulo 3</a> </li>
                <li><a target="_blank" href="http://100certificado.com/">Certificación Modulo 4</a> </li>
            </ul>
        </div>
        <!--

  CENTER COLUMN

  -->
        <div id="content-main-three-column">
            <asp:Panel ID="pnlUsuarios" runat="server" Height="160px" 
                style="text-align: center" Width="565px" Visible="False">
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
                    <table class="style5" style="width: 65%">
                        <tr>
                            <td style="height: 17px;">
                                <asp:Button ID="btnVer" runat="server" Text="Ver" Width="81px" />
                            </td>
                        </tr>
                </table>
            </asp:Panel>
            
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            
            
            <asp:UpdatePanel ID="upPromotor" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <asp:Panel ID="pnlFechasPromotor" runat="server" style="text-align: center" 
                    Visible="false" Width="608px">

                    <table style="width: 100%">
                        
                        <tr>
                            <td style="height: 19px;">
                                
                                <asp:GridView ID="gridModulosPromotor" runat="server" 
                                AutoGenerateColumns="False" DataKeyNames="id_modulo" Width="88%" 
                                CellPadding="4" ForeColor="#333333" GridLines="None" 
                                style="text-align: center" ShowFooter="True" 
                            Caption="MODULOS" Height="16px">
                               <RowStyle BackColor="#FFFBD6" HorizontalAlign="Left" ForeColor="#333333" 
                                        VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundField HeaderText="Modulo" DataField="nombre_modulo" />
                            <asp:TemplateField HeaderText="Fecha entrega Material">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMaterial" runat="server" MaxLength="10" Width="70" Text='<%#DataBinder.Eval(Container.DataItem, "fecha_material_p","{0:d}")%>'></asp:TextBox>
                                    <asp:RegularExpressionValidator id="rvaMaterial" runat="server" ErrorMessage="La fecha no es correcta formato 'dd/mm/aaaa'" Display="Dynamic" 
                                        ValidationExpression="\d{2}/\d{2}/\d{4}" ControlToValidate="txtMaterial" ValidationGroup="MarsPromotor" />
                                    <asp:RangeValidator ID="rvFechaMaterial" runat="server" ErrorMessage="La fecha no es valida" 
                                    ValidationGroup="MarsPromotor" ControlToValidate="txtMaterial"
                                        MaximumValue="31/12/3000" MinimumValue="01/01/2010" Type="Date"></asp:RangeValidator>
                                </ItemTemplate>
                                <ItemStyle Width="10%" VerticalAlign="top" />
                            </asp:TemplateField>                      
                            <asp:TemplateField HeaderText="Fecha Capacitación">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCapacitacion" runat="server" MaxLength="10" Width="70" Text='<%# DataBinder.Eval(Container.DataItem, "fecha_capacitacion_p","{0:d}") %>' ></asp:TextBox>
                                    <asp:RegularExpressionValidator id="rvaCapacitacion" runat="server" ErrorMessage="La fecha no es correcta formato 'dd/mm/aaaa'" Display="Dynamic" 
                                        ValidationExpression="\d{2}/\d{2}/\d{4}" ControlToValidate="txtCapacitacion" ValidationGroup="MarsPromotor" />
                                        <asp:RangeValidator ID="rvFechaCapacitacion" runat="server" ErrorMessage="La fecha no es valida" 
                                        ValidationGroup="MarsPromotor" ControlToValidate="txtCapacitacion"
                                        MaximumValue="31/12/3000" MinimumValue="01/01/2010" Type="Date"></asp:RangeValidator>
                                </ItemTemplate>
                                <ItemStyle Width="10%" VerticalAlign="top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha Certificación">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCertificacion" runat="server" MaxLength="10" Width="70" Text='<%# DataBinder.Eval(Container.DataItem, "fecha_certificacion_p","{0:d}") %>'></asp:TextBox>
                                    <asp:RegularExpressionValidator id="rvaCertificacion" runat="server" ErrorMessage="La fecha no es correcta formato 'dd/mm/aaaa'" Display="Dynamic" 
                                        ValidationExpression="\d{2}/\d{2}/\d{4}" ControlToValidate="txtCertificacion" ValidationGroup="MarsPromotor" />
                                        <asp:RangeValidator ID="rvFechaCertificacion" runat="server" ErrorMessage="La fecha no es valida" 
                                        ValidationGroup="MarsPromotor" ControlToValidate="txtCertificacion"
                                        MaximumValue="31/12/3000" MinimumValue="01/01/2010" Type="Date"></asp:RangeValidator>
                                </ItemTemplate>
                                <ItemStyle Width="10%" VerticalAlign="top" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Calificación" DataField="calificacion" />
                        </Columns>
                               <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                               <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            <h1>Selecciona el promotor</h1>
                        </EmptyDataTemplate>
                               <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                               <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                               <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Button ID="btnGuardarPromotor" runat="server" Text="Guardar Cambios" 
                        ValidationGroup="MarsPromotor" />
                    <br />
                <br />
                           
                </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardarPromotor" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>   
            
            
            <!--PANEL SUPERVISOR-->
            <asp:UpdatePanel ID="upSupervisor" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <asp:Panel ID="pnlFechasSupervisor" runat="server" style="text-align: center" 
                    Visible="false" Width="632px">

                    <table style="width: 100%">
                    
                    <tr>
                        <td style="height: 19px;">
                            <asp:GridView ID="gridModulosSupervisor" runat="server" 
                            AutoGenerateColumns="False" DataKeyNames="id_modulo" Width="67%" 
                            CellPadding="4" ForeColor="#333333" GridLines="None" 
                            style="text-align: center" ShowFooter="True" 
                        Caption="MODULOS" Height="16px" CaptionAlign="Top">
                           <RowStyle BackColor="#FFFBD6" HorizontalAlign="Left" ForeColor="#333333" 
                                    VerticalAlign="Top" />
                    <Columns>
                        <asp:BoundField HeaderText="Modulo" DataField="nombre_modulo" />
                        <asp:BoundField HeaderText="Promotor" DataFormatString="{0:dd/MMM/yy}" DataField="fecha_material_p" />
                        <asp:TemplateField HeaderText="Fecha entrega Material">
                            <ItemTemplate>
                                <asp:TextBox ID="txtMaterial" runat="server" MaxLength="10"  Width="70" Text='<%#DataBinder.Eval(Container.DataItem, "fecha_material_s","{0:d}") %>'></asp:TextBox>
                                <asp:RegularExpressionValidator id="rvaMaterial" runat="server" ErrorMessage="La fecha no es correcta formato 'dd/mm/aaaa'" Display="Dynamic" 
                                    ValidationExpression="\d{2}/\d{2}/\d{4}" ControlToValidate="txtMaterial" ValidationGroup="MarsSupervisor" />
                                    <asp:RangeValidator ID="rvFechaMaterial" runat="server" ErrorMessage="La fecha no es valida" 
                                    ValidationGroup="MarsSupervisor" ControlToValidate="txtMaterial"
                                    MaximumValue="31/12/3000" MinimumValue="01/01/2010" Type="Date"></asp:RangeValidator>
                            </ItemTemplate>
                            <ItemStyle Width="0%" VerticalAlign="top" />
                        </asp:TemplateField>                   
                        <asp:BoundField HeaderText="Promotor" DataFormatString="{0:dd/MMM/yy}" DataField="fecha_capacitacion_p" />   
                        <asp:TemplateField HeaderText="Fecha Capacitación">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCapacitacion" runat="server" MaxLength="10" Width="70" Text='<%# DataBinder.Eval(Container.DataItem, "fecha_capacitacion_s","{0:d}") %>' ></asp:TextBox>
                                <asp:RegularExpressionValidator id="rvaCapacitacion" runat="server" ErrorMessage="La fecha no es correcta formato 'dd/mm/aaaa'" Display="Dynamic" 
                                    ValidationExpression="\d{2}/\d{2}/\d{4}" ControlToValidate="txtCapacitacion" ValidationGroup="MarsSupervisor" />
                                    <asp:RangeValidator ID="rvFechaCapacitacion" runat="server" ErrorMessage="La fecha no es valida" 
                                    ValidationGroup="MarsSupervisor" ControlToValidate="txtCapacitacion"
                                    MaximumValue="31/12/3000" MinimumValue="01/01/2010" Type="Date"></asp:RangeValidator>
                            </ItemTemplate>
                            <ItemStyle Width="0%" VerticalAlign="top" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Promotor" DataFormatString="{0:dd/MMM/yy}" DataField="fecha_certificacion_p" />
                        <asp:TemplateField HeaderText="Fecha Certificación">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCertificacion" runat="server" MaxLength="10" Width="70" Text='<%# DataBinder.Eval(Container.DataItem, "fecha_certificacion_s","{0:d}") %>'></asp:TextBox>
                                <asp:RegularExpressionValidator id="rvaCertificacion" runat="server" ErrorMessage="La fecha no es correcta formato 'dd/mm/aaaa'" Display="Dynamic" 
                                    ValidationExpression="\d{2}/\d{2}/\d{4}" ControlToValidate="txtCertificacion" ValidationGroup="MarsSupervisor" />
                                    <asp:RangeValidator ID="rvFechaCertificacion" runat="server" ErrorMessage="La fecha no es valida" 
                                    ValidationGroup="MarsSupervisor" ControlToValidate="txtCertificacion"
                                    MaximumValue="31/12/3000" MinimumValue="01/01/2010" Type="Date"></asp:RangeValidator>
                            </ItemTemplate>
                            <ItemStyle Width="0%" VerticalAlign="top" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Calificación" DataField="calificacion" />
                    </Columns>
                           <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                           <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        <h1>Selecciona el Promotor</h1>
                    </EmptyDataTemplate>
                           <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                           <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                           <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="btnGuardarSupervisor" runat="server" Text="Guardar Cambios" 
                    ValidationGroup="MarsSupervisor" />
                <br />
            <br />
                       
            </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardarSupervisor" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel> 
            
            <!--PANEL EJECUTIVO MARS-->
            <asp:UpdatePanel ID="upEjecutivoMars" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <asp:Panel ID="pnlFechasEjecutivoMars" runat="server" style="text-align: center" 
                Visible="false">

                    <table style="width: 100%">
                    
                    <tr>
                        <td style="height: 19px; width: 649px;">
                            <asp:GridView ID="gridModulosEjecutivoMars" runat="server" 
                            AutoGenerateColumns="False" DataKeyNames="id_modulo" Width="92%" 
                            CellPadding="4" ForeColor="#333333" GridLines="None" 
                            style="text-align: center" ShowFooter="True" 
                        Caption="MODULOS" Height="16px">
                           <RowStyle BackColor="#FFFBD6" HorizontalAlign="Left" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField HeaderText="Modulo" DataField="nombre_modulo" />
                        <asp:BoundField HeaderText="Entrega Material (promotor)" DataField="fecha_material_p" DataFormatString="{0:dd/MMM/yy}" />
                        <asp:BoundField HeaderText="Entrega Material (supervisor)" DataField="fecha_material_s" DataFormatString="{0:dd/MMM/yy}" />
                        <asp:BoundField HeaderText="Capa citación (promotor)" DataField="fecha_capacitacion_p" DataFormatString="{0:dd/MMM/yy}" />
                        <asp:BoundField HeaderText="Capa citación (supervisor)" DataField="fecha_capacitacion_s" DataFormatString="{0:dd/MMM/yy}" />
                        <asp:BoundField HeaderText="Certi ficación (promotor)" DataField="fecha_certificacion_p" DataFormatString="{0:dd/MMM/yy}" />
                        <asp:BoundField HeaderText="Certi ficación (supervisor)" DataField="fecha_certificacion_s" DataFormatString="{0:dd/MMM/yy}" />
                        <asp:BoundField HeaderText="Calificación" DataField="calificacion" />
                    </Columns>
                           <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                           <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        <h1>Selecciona el Promotor</h1>
                    </EmptyDataTemplate>
                           <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                           <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                           <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="btnGuardarEjecutivoMars" runat="server" Text="Guardar Cambios" />
                <br />
            <br />
                       
            </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardarEjecutivo" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel> 
            
            <!--PANEL EJECUTIVO-->
            <asp:UpdatePanel ID="upEjecutivo" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <asp:Panel ID="pnlFechasEjecutivo" runat="server" style="text-align: center" 
                Visible="false" Width="685px">

                    <table style="width: 100%">
                    
                    <tr>
                        <td style="height: 19px; width: 649px;">
                            <asp:GridView ID="gridModulosEjecutivo" runat="server" 
                            AutoGenerateColumns="False" DataKeyNames="id_modulo" Width="92%" 
                            CellPadding="4" ForeColor="#333333" GridLines="None" 
                            style="text-align: center" ShowFooter="True" 
                        Caption="MODULOS" Height="16px">
                           <RowStyle BackColor="#FFFBD6" HorizontalAlign="Left" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField HeaderText="Modulo" DataField="nombre_modulo" />
                        <asp:BoundField HeaderText="Entrega Material (promotor)" DataField="fecha_material_p" DataFormatString="{0:dd/MMM/yy}" />
                        <asp:BoundField HeaderText="Entrega Material (supervisor)" DataField="fecha_material_s" DataFormatString="{0:dd/MMM/yy}" />
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkMaterial" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "fecha_material_e")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>                      
                        <asp:BoundField HeaderText="Capa citación (promotor)" DataField="fecha_capacitacion_p" DataFormatString="{0:dd/MMM/yy}" />
                        <asp:BoundField HeaderText="Capa citación (supervisor)" DataField="fecha_capacitacion_s" DataFormatString="{0:dd/MMM/yy}" />
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCapacitacion" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "fecha_capacitacion_e")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Certi ficación (promotor)" DataField="fecha_certificacion_p" DataFormatString="{0:dd/MMM/yy}" />
                        <asp:BoundField HeaderText="Certi ficación (supervisor)" DataField="fecha_certificacion_s" DataFormatString="{0:dd/MMM/yy}" />
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCertificacion" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "fecha_certificacion_e")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Calificación" DataField="calificacion" />
                    </Columns>
                           <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                           <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        <h1>Selecciona el Promotor</h1>
                    </EmptyDataTemplate>
                           <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                           <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                           <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="btnGuardarEjecutivo" runat="server" Text="Guardar Cambios" />
                <br />
            <br />
                       
            </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardarEjecutivo" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel> 
            
            <br />
            <br />
            
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
