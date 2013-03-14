<%@ Page Language="vb"
masterpagefile="~/sitios/Shelby/Shelby.Master"
 AutoEventWireup="false" 
 CodeBehind="Creportes.aspx.vb" 
 Inherits="procomlcd.Creportes" %>
 
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderShelby" runat="Server">


     
    <!--

POSTER PHOTO

-->
    <div id="poster-photo-container">
    
    <div id="poster-photo-image">
        <img src="../../../Img/banner3.jpg" width="900px" height ="110px" />
    </div>
         
          </div>
    <!--

CONTENT CONTAINER

-->
    <div id="contenido-columna-derecha">
        <!--

CONTENT MAIN COLUMN

-->
        <div id="content-main-two-column">

            <div id="three-column-container">
                      <asp:DropDownList ID="Lista" runat="server" AutoPostBack="True">
                    <asp:ListItem Value=0>Selecciona una opcion</asp:ListItem>
                    <asp:ListItem Value=1>Aprobado</asp:ListItem>
                    <asp:ListItem Value=2>No Aprobado</asp:ListItem>
                </asp:DropDownList>


                    
                
                
                <br />
                <br />
                
                <asp:Panel ID="Panel1" runat="server" CssClass="panel-grid">
                
           
                <asp:GridView ID="aprobados" runat="server" CellPadding="4" ForeColor="#333333" 
                          GridLines="None" CssClass ="grid-view" >
                    <RowStyle BackColor="#EFF3FB" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                
                </asp:Panel>
                
            </div>
        </div>
        <!--

CONTENT SIDE COLUMN AVISOS

-->
        
        <div class="clear">
        
        </div>
        
    </div>





</asp:Content>


