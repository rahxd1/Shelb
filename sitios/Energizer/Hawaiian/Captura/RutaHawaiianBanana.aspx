<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Energizer/Hawaiian/HawaiianBanana.Master"
    AutoEventWireup="false" 
    CodeBehind="RutaHawaiianBanana.aspx.vb" 
    Inherits="procomlcd.RutaHawaiianBanana"
    TITLE="Hawaiian/Banana - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1HawaiianBanana" runat="Server">
  
    <!--CONTENT CONTAINER-->
<div id="titulo-pagina">
        Ruta</div>
        <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
            <asp:ScriptManager ID="ScriptManager1" runat="server"/>
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                      <asp:Panel ID="panelMenu" runat="server" Visible="False">
                          <table style="width: 101%">
                              <tr>
                                  <td style="border-top-color: #cccccc; border-top-style: outset; border-bottom-color: #ffffff; text-align: right; width: 134px;">
                                      Región:
                                  </td>
                                  <td style="border-top-color: #cccccc; border-top-style: outset; border-bottom-color: #ffffff">
                                      <asp:DropDownList ID="cmbRegion" runat="server" Height="22px" Width="150px" 
                                          AutoPostBack="True" >
                                      </asp:DropDownList>
                                  </td>
                              </tr>
                              <tr>
                                  <td style="border-top-color: #cccccc; border-bottom-style: outset; border-bottom-color: #ffffff; text-align: right; width: 134px;">
                                      Promotor:</td>
                                  <td style="border-top-color: #cccccc; border-bottom-style: outset; border-bottom-color: #ffffff">
                                      <asp:DropDownList ID="cmbPromotor" runat="server" AutoPostBack="True" 
                                          Height="22px" Width="150px">
                                      </asp:DropDownList>
                                  </td>
                              </tr>
                          </table>
                      </asp:Panel>
                      <table style="width: 100%" width="100%">
                          <tr>
                              <td style="border-top-style: outset; border-bottom-style: outset; border-top-color: #CCCCCC; border-bottom-color: #000000; text-align: right; width: 130px;">
                                  Periodo</td>
                              <td style="border-top-style: outset; border-bottom-style: outset; border-top-color: #CCCCCC; border-bottom-color: #000000; width: 520px;">
                                      <asp:DropDownList ID="cmbPeriodo" runat="server" Height="21px" 
                                          Width="296px" AutoPostBack="True"></asp:DropDownList></td>
                          </tr>
                          <tr>
                              <td colspan="2" 
                                  
                                  style="border-top-style: outset; border-bottom-style: outset; border-top-color: #CCCCCC; border-bottom-color: #000000">
                       
                               <asp:GridView ID="gridRutas" runat="server" AutoGenerateColumns ="False" 
                                   Caption ='- - - - - - - - - - TIENDAS EN TU RUTA - - - - - - - - - -' 
                                   Width="100%" style="text-align: center" ShowFooter="True" 
                                      CssClass="grid-view-ruta" >
                                        <Columns>                                             
                                            <asp:BoundField HeaderText="" DataField="captura" HtmlEncode="false" />
                                            <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena" />
                                            <asp:BoundField HeaderText="Tienda" DataField="nombre" />
                                            <asp:BoundField HeaderText="Hawaiian" DataField="img_status1" HtmlEncode="false" />
                                            <asp:TemplateField >
                                                <ItemTemplate>
                                                <a href="FormatoCapturaHawaiianBanana.aspx?tienda=<%#Eval("id_tienda")%>&usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("id_periodo")%>&folio=<%#Eval("folio_historial1")%>&marca=1">Ver</a>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField> 
                                            <asp:BoundField HeaderText="Banana" DataField="img_status2" HtmlEncode="false" />                                     
                                            <asp:TemplateField >
                                                <ItemTemplate>
                                                <a href="FormatoCapturaHawaiianBanana.aspx?tienda=<%#Eval("id_tienda")%>&usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("id_periodo")%>&folio=<%#Eval("folio_historial2")%>&marca=2">Ver</a>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField> 
                                        </Columns>
                                    <FooterStyle CssClass="grid-footer" />
                                </asp:GridView>
                                <div style="text-align: center">
                                   <asp:Label ID="Label1" runat="server" Text="-&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; 
                                   -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; 
                                   -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; 
                                   -&nbsp; -&nbsp; -&nbsp; -&nbsp; -&nbsp; -"></asp:Label>
                                </div>
                          </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmbPeriodo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbRegion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbPromotor" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        
<!--CONTENT SIDE COLUMN AVISOS-->
   <div id="content-side-two-column">
            <h2>
              Estatus Captura:</h2>
            <ul class="list-of-links">
                <li><img src="../../../../Img/Capturado.gif" alt="C"/> Tienda Capturada</li>
                <li><img src="../../../../Img/Falta.gif" alt="C"/> Tienda falta por capturar</li>
                <li><img src="../../../../Img/Pendiente.gif" alt="C"/> Tienda incompleta</li>
             </ul>
             <br />
            <h2>
                Estatus Tienda:</h2>
                <ul class="list-of-links">    
                <li><img src="../../../../Img/Abierta.png" alt="C"/> Tienda Abierta</li>
                <li><img src="../../../../Img/Cerrado.png" alt="C"/> Tienda Cerrada</li>
            </ul>
        </div>
       
        
        <div class="clear">
        
        </div>
    </div>
    
</asp:Content>
