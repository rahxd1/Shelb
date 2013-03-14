<%@ Page Language="vb"
Masterpagefile="~/sitios/Demos/Penaranda/DemoPenaranda.Master"
 AutoEventWireup="false"
  CodeBehind="DemoPenarandaManuales.aspx.vb" 
  Inherits="procomlcd.DemoPenarandaManuales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DemoPenarandaContent" runat="Server">

    <!--
titulo-pagina
-->
        <div id="titulo-pagina">
            Manuales</div>
<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
       
            <asp:HyperLink ID="HyperLink1" runat="server" 
                NavigateUrl="~/sitios/Demos/Penaranda/capacitacion/DemoPenarandaCapacitacion.aspx">&lt;-Regresar</asp:HyperLink>
                
            <br />
            <br />
            <asp:HyperLink ID="HyperLink2" runat="server" 
                NavigateUrl="SISTEMA PROCOM.pdf">Manual del Sistema</asp:HyperLink>
            
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        </div>
        <!-- END MAIN COLUMN -->
        <!--

  CONTENT SIDE 2 COLUMN

  -->
      
        <div class="clear">
        </div>
    </div>
    </asp:Content>
