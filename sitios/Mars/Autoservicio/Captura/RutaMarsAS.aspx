<%@ Page Language="vb" 
    Culture="es-MX" 
    Masterpagefile="~/sitios/Mars/Autoservicio/Mars_Autoservicio.Master"
    AutoEventWireup="false" 
    CodeBehind="RutaMarsAS.aspx.vb" 
    Inherits="procomlcd.RutaMarsAS"
    Title="Mars Autoservicio - Captura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMarsAutoservicio" runat="Server">
  
<!--CONTENT CONTAINER-->
    <div id="titulo-pagina">
        Rutas</div>
    
        <div id="contenido-columna-derecha">
        
            <!--CONTENT MAIN COLUMN-->
            <div id="content-main-two-column">
                  <asp:Panel ID="panelMenu" runat="server" Visible="False">
                      <table style="width: 100%">
                          <tr>
                              <td style="text-align: right; width: 190px;">
                                  Región:</td>
                              <td>
                                  <asp:DropDownList ID="cmbRegion" runat="server" AutoPostBack="True" 
                                      Height="22px" Width="150px">
                                  </asp:DropDownList></td>
                          </tr>
                          <tr>
                              <td style="text-align: right; width: 190px;">
                                  Ejecutivo:</td>
                              <td>
                                  <asp:DropDownList ID="cmbEjecutivo" runat="server" AutoPostBack="True" 
                                      Height="22px" Width="150px">
                                  </asp:DropDownList></td>
                          </tr>
                          <tr>
                              <td style="text-align: right; width: 190px;">
                                  Promotor:</td>
                              <td>
                                  <asp:DropDownList ID="cmbPromotor" runat="server" AutoPostBack="True" 
                                      Height="22px" Width="150px">
                                  </asp:DropDownList></td>
                          </tr>
                      </table>
                  </asp:Panel>
                  <br />
                  
                  <table style="width: 100%">
                      <tr>
                          <td style="text-align: right; width: 190px;">
                              Periodo:</td>
                          <td>
                              <asp:DropDownList ID="cmbPeriodo" runat="server" Height="21px" Width="255px" 
                                AutoPostBack="True">
                              </asp:DropDownList>
                          </td>
                      </tr>
                      <tr>
                          <td colspan="2" style="text-align: center">
                                <asp:LinkButton ID="lnkReporteFocoQ1" runat="server" Font-Bold="True">Reporte Foco Quincena 1</asp:LinkButton>
                                <br />
                              <asp:LinkButton ID="lnkReporteFocoQ2" runat="server" Font-Bold="True">Reporte Foco Quincena 2</asp:LinkButton>
                          </td>
                      </tr>
                      <tr>
                          <td colspan="2">
                              <asp:GridView ID="gridAreaNielsen" runat="server" AutoGenerateColumns="False" 
                                    BackColor="#FFFF99" CaptionAlign="Top" HorizontalAlign="Center" 
                                    ShowFooter="True" Width="50%">
                                    <RowStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:BoundField DataField="Area" HeaderText="Area nielsen" 
                                            ItemStyle-Width="39px" />
                                        <asp:BoundField DataField="O_ps" HeaderText="Adulto Seco" 
                                            ItemStyle-Width="83px" />
                                        <asp:BoundField DataField="O_pc" HeaderText="Cachorro Seco" 
                                            ItemStyle-Width="95px" />
                                        <asp:BoundField DataField="O_ph" HeaderText="Perro Húmedo" 
                                            ItemStyle-Width="60px" />
                                        <asp:BoundField DataField="O_pb" HeaderText="Perro Botanas" 
                                            ItemStyle-Width="56px" />
                                        <asp:BoundField DataField="O_gs" HeaderText="Gato Seco" 
                                            ItemStyle-Width="50px" />
                                        <asp:BoundField DataField="O_gh" HeaderText="Gato Húmedo" 
                                            ItemStyle-Width="60px" />
                                        <asp:BoundField DataField="O_gb" HeaderText="Gato Botana" 
                                            ItemStyle-Width="56px" />
                                    </Columns>
                                    <FooterStyle BackColor="#17375D" />
                                    <HeaderStyle BackColor="#02456F" ForeColor="White" />
                                </asp:GridView>
                           </td>
                      </tr>
                      <tr>
                          <td colspan="2">
                              <asp:GridView ID="gridRutas" runat="server" AutoGenerateColumns ="False" 
                                Width="100%" ShowFooter="True" CssClass="grid-view" GridLines="None" >
                            <Columns>           
                                <asp:BoundField HeaderText="" DataField="captura" HtmlEncode="false" />                                
                                <asp:BoundField HeaderText="Cadena" DataField="nombre_cadena" />
                                <asp:BoundField HeaderText="Tienda" DataField="nombre" />
                                <asp:BoundField HeaderText="Clasificación" DataField="clasificacion_tienda" />
                                <asp:BoundField HeaderText="Formato" DataField="nombre_formato" />
                                
                                 <asp:TemplateField HeaderText="Anaquel Q1" >
                                      <ItemTemplate>
                                            <%#Eval("estatus_anaquel_q1")%>
                                            <a href="FormatoCapturaMarsAS.aspx?folio=<%#Eval("folio_historial1")%>&tienda=<%#Eval("id_tienda")%>&usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("orden")%>&quincena=Q1"><%#Eval("ver_anaquel_q1")%></a>
                                       </ItemTemplate>
                                      <ItemStyle />
                                 </asp:TemplateField>   
                                 <asp:TemplateField HeaderText="Precios Q1" >
                                      <ItemTemplate>
                                            <%#Eval("estatus_precio_q1")%>
                                            <a href="FormatoCapturaPreciosMarsAS.aspx?folio=<%#Eval("folio_historialpre1")%>&tienda=<%#Eval("id_tienda")%>&usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("orden")%>&quincena=Q1&nombre=<%#Eval("nombre")%>"><%#Eval("ver_precio_q1")%></a>
                                       </ItemTemplate>
                                      <ItemStyle />
                                 </asp:TemplateField> 
                                 <asp:TemplateField HeaderText="Anaquel Q2" >
                                      <ItemTemplate>
                                            <%#Eval("estatus_anaquel_q2")%>
                                            <a href="FormatoCapturaMarsAS.aspx?folio=<%#Eval("folio_historial2")%>&tienda=<%#Eval("id_tienda")%>&usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("orden")%>&quincena=Q2"><%#Eval("ver_anaquel_q2")%></a>
                                       </ItemTemplate>
                                      <ItemStyle />
                                 </asp:TemplateField>      
                                 <asp:TemplateField HeaderText="Precios Q2" >
                                      <ItemTemplate>
                                            <%#Eval("estatus_precio_q2")%>
                                            <a href="FormatoCapturaPreciosMarsAS.aspx?folio=<%#Eval("folio_historialpre2")%>&tienda=<%#Eval("id_tienda")%>&usuario=<%#Eval("id_usuario")%>&periodo=<%#Eval("orden")%>&quincena=Q2&nombre=<%#Eval("nombre")%>"><%#Eval("ver_precio_q2")%></a>
                                       </ItemTemplate>
                                      <ItemStyle />
                                 </asp:TemplateField>                  
                            </Columns>
                           <FooterStyle CssClass="grid-footer" />
                          
                        </asp:GridView>
                        </td>
                      </tr>
                  </table>

                     <asp:Panel ID="pnlDetalle2" runat="server" Visible="False" BackColor="White" 
                          ScrollBars="Both" Height="600px" CssClass="pnlDetalle">
                        <br />
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: left"></td>
                                <td style="text-align: right">
                                    <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True">Cerrar</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                     
                        <asp:GridView ID="gridDetalle2" runat="server" AutoGenerateColumns="False" 
                            Font-Names="Arial" Font-Size="Small" Height="307px" ShowFooter="True" 
                            style="font-size: small; text-align: center;" Width="960px">
                            <RowStyle HorizontalAlign="Left" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    
                                        <table class="style5" width="100%">
                                            <tr>
                                                <td align="center" bgcolor="#025483" class="style6" width="100%">
                                                    <b style="color: #FFFFFF">REPORTE DE EVALUACIÓN DE CAPTURA EN LA TIENDA</b>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td class="style4">
                                                    Cadena:</td>
                                                <td>
                                                    <b><%#Eval("nombre_cadena")%></b></td>
                                            </tr>
                                            <tr>
                                                <td class="style4">
                                                    Tienda:</td>
                                                <td>
                                                    <b><%#Eval("nombre")%></b></td>
                                            </tr>
                                            <tr>
                                                <td class="style4">
                                                    Tipo tienda:</td>
                                                <td>
                                                    <b><%#Eval("clasificacion_tienda")%></b></td>
                                            </tr>
                                            <tr>
                                                <td class="style4">
                                                    Formato:</td>
                                                <td>
                                                    <b><%#Eval("nombre_formato")%></b></td>
                                            </tr>
                                            <tr>
                                                <td class="style4">
                                                    Area nielsen:</td>
                                                <td>
                                                    <b><%#Eval("area_nielsen")%></b></td>
                                            </tr>
                                        </table>
                                        <br />
                                        <table border="1" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="center" bgcolor="#FFFF66" class="style2" colspan="14">
                                                    <asp:Label ID="Label3" runat="server" style="font-weight: 700" 
                                                        Text="FRENTES QUE TE HACEN FALTA EN TU TIENDA PARA CUBRIR TU PAUTA"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="style2" colspan="4">
                                                    Perro</td>
                                                <td align="center" class="style2" colspan="3">
                                                    Gato</td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="style2">
                                                    Seco</td>
                                                <td align="center" class="style2">
                                                    Cachorro</td>
                                                <td align="center" class="style2">
                                                    Humedo</td>
                                                <td align="center" class="style2">
                                                    Botanas</td>
                                                <td align="center" class="style2">
                                                    Seco</td>
                                                <td align="center" class="style2">
                                                    Humedo</td>
                                                <td align="center" class="style2">
                                                    Botanas</td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblPS" runat="server" Height="20px" Text='<%#Eval("PS")%>' 
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="lblPC" runat="server" Height="20px" Text='<%#Eval("PC")%>' 
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="lblPH" runat="server" Height="20px" Text='<%#Eval("PH")%>' 
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="lblPB" runat="server" Height="20px" Text='<%#Eval("PB")%>' 
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="lblGS" runat="server" Height="20px" Text='<%#Eval("GS")%>' 
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="lblGH" runat="server" Height="20px" Text='<%#Eval("GH")%>' 
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="lblGB" runat="server" Height="20px" Text='<%#Eval("GB")%>' 
                                                        Width="100px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                       
<table border="1" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td bgcolor="#FFFF66" colspan="7" style="text-align: center">
                                                    <asp:Label ID="Label1" runat="server" style="font-weight: 700" 
                                                        
                            Text="EXHIBICIONES QUE TE HACEN FALTA PARA CUBRIR TU PAUTA"></asp:Label>
                                                </td>
                </tr>
                <tr>
                    <td bgcolor="#F79646" colspan="4" style="color: #FFFFFF; text-align: center">
                        <b style="text-align: center">Anaquel</b></td>
                    <td bgcolor="#4F81BD" colspan="3" style="text-align: center; color: #FFFFFF">
                        <b>Pasillo mascota</b></td>
                </tr>
                <tr>
                    <td style="text-align: center" bgcolor="#F79646">
                        <b style="text-align: center">Pouchero</b></td>
                    <td style="text-align: center" bgcolor="#F79646">
                        <b>Latero</b></td>
                    <td style="text-align: center" bgcolor="#F79646">
                        <b>Tira</b></td>
                    <td style="text-align: center" bgcolor="#F79646">
                        <b>Balcón</b></td>
                    <td style="text-align: center" bgcolor="#4F81BD">
                        <b>Cabecera</b></td>
                    <td style="text-align: center" bgcolor="#4F81BD">
                        <b>Isla</b></td>
                    <td style="text-align: center" bgcolor="#4F81BD">
                        <b>Botadero</b></td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="lblAnaquel1" runat="server" Font-Bold="true"
                            Height="15px" Text='<%# Eval("A_Det_12") %>' 
                                                        Width="60px" style="text-align: center"></asp:Label>
                                                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblAnaquel2" runat="server" 
                            Height="15px" Text='<%# Eval("A_Det_11") %>' 
                                                        Width="60px" style="text-align: center"></asp:Label>
                                                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblAnaquel3" runat="server" 
                            Height="15px" Text='<%# Eval("A_Det_10") %>' 
                                                        Width="60px" style="text-align: center"></asp:Label>
                                                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblAnaquel4" runat="server" 
                            Height="15px" Text='<%# Eval("A_Det_9") %>' 
                                                        Width="60px" style="text-align: center"></asp:Label>
                                                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblPasillo1" runat="server" 
                            Height="15px" Text='<%# Eval("M_Det_6") %>' 
                                                        Width="60px" style="text-align: center"></asp:Label>
                                                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblPasillo2" runat="server" 
                            Height="15px" Text='<%# Eval("M_Det_1") %>' 
                                                        Width="60px" style="text-align: center"></asp:Label>
                                                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblPasillo3" runat="server" 
                            Height="15px" Text='<%# Eval("M_Det_15") %>' 
                                                        Width="60px" style="text-align: center"></asp:Label>
                                                    </td>
                </tr>
                <tr style="color: #FFFFFF">
                    <td bgcolor="Red" colspan="4" style="text-align: center">
                        <b>Zona caliente</b></td>
                    <td bgcolor="#00B050" colspan="3" style="text-align: center">
                        <b>Entrada o salida</b></td>
                </tr>
                <tr>
                    <td style="text-align: center" bgcolor="Red">
                        <b>o Isla</b></td>
                    <td style="text-align: center" bgcolor="Red">
                        <b>o Botadero</b></td>
                    <td style="text-align: center" bgcolor="Red">
                        <b>o Mix feeding</b></td>
                    <td style="text-align: center" bgcolor="Red">
                        <b>o Mini rack</b></td>
                    <td colspan="3" rowspan="2" style="text-align: center">
                        <table border="1" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td bgcolor="#00B050">
                                    <b>Mini rack</b></td>
                                <td bgcolor="#00B050">
                                    <b>Wet móvil</b></td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                        <asp:Label ID="lblES1" runat="server" 
                            Height="15px" Text='<%# Eval("ES_Det_5") %>' 
                                                        Width="60px"></asp:Label>
                                                    </td>
                                <td style="text-align: center">
                        <asp:Label ID="lblES2" runat="server" 
                            Height="15px" Text='<%# Eval("ES_Det_16") %>' 
                                                        Width="60px"></asp:Label>
                                                    </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="lblCaliente1" runat="server" 
                            Height="15px" Text='<%# Eval("C_Det_1") %>' 
                                                        Width="60px" style="text-align: center"></asp:Label>
                                                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblCaliente2" runat="server" 
                            Height="15px" Text='<%# Eval("C_Det_15") %>' 
                                                        Width="60px" style="text-align: center"></asp:Label>
                                                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblCaliente3" runat="server" 
                            Height="15px" Text='<%# Eval("C_Det_3") %>' 
                                                        Width="60px" style="text-align: center"></asp:Label>
                                                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblCaliente4" runat="server" 
                            Height="15px" Text='<%# Eval("C_Det_5") %>' 
                                                        Width="60px" style="text-align: center"></asp:Label>
                                                    </td>
                </tr>
            </table>      
                                       
                                        <table class="style5" width="100%">
                                            <tr>
                                                <td align="center" bgcolor="#025483" class="style6" width="100%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </ItemTemplate>
                                    <ItemStyle />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: left"></td>
                                <td style="text-align: right">
                                    <asp:LinkButton ID="LinkButton2" runat="server" Font-Bold="True">Cerrar</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                     </asp:Panel>
            </div>
            
       
        
<!--CONTENT SIDE COLUMN AVISOS-->
        <div id="content-side-two-column">
                                       
            </div>
            
       
        
<!--CONTENT SIDE COLUMN AVISOS-->
        <div id="content-side-two-column">
            <h2>
                &nbsp;</h2>
            <h2>
              Estatus tienda:</h2>
            <ul class="list-of-links">
                <li><img src="../../../../Img/Capturado.gif" alt="C"/> Capturada</li>
                <li><img src="../../../../Img/Falta.gif" alt="C"/> Sin capturar</li>
             </ul>
             <br />
            <h2>
                Estatus periodo:</h2>
                <ul class="list-of-links">    
                <li><img src="../../../../Img/Abierta.png" alt="C"/> Abierto</li>
                <li><img src="../../../../Img/Cerrado.png" alt="C"/> Cerrado</li>
            </ul>
        </div>
       
        
        <div class="clear"></div>
         </div>
         
                
                           
</asp:Content>
