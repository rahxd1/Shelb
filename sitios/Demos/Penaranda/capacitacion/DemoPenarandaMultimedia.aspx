<%@ Page Language="vb" 
masterpagefile="~/sitios/Demos/Penaranda/DemoPenaranda.Master"
AutoEventWireup="false" 
CodeBehind="DemoPenarandaMultimedia.aspx.vb"
Inherits="procomlcd.DemoPenarandaMultimedia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DemoPenarandaContent" runat="Server">

    <!--
titulo-pagina
-->
        <div id="titulo-pagina">
           Multimedia</div>
<!--CONTENT CONTAINER-->
    <div id="contenido-columna-derecha">
        
<!--CONTENT MAIN COLUMN-->
        <div id="content-main-two-column">
       
            <asp:HyperLink ID="HyperLink1" runat="server" 
                NavigateUrl="~/sitios/Demos/Penaranda/capacitacion/DemoPenarandaCapacitacion.aspx">&lt;-Regresar</asp:HyperLink> 
                
            <asp:HyperLink ID="HyperLink2" runat="server" 
                NavigateUrl="~/ARCHIVOS/SHELBY/DEMOS/demoprocom.wmv">Video Demo Captura en Sistema</asp:HyperLink>    
            <br />
            <br />
          
           
            
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
            <br />
            &nbsp;&nbsp;&nbsp;

            <br />
            <br />
         
        <!-- END MAIN COLUMN -->
        <!--

  CONTENT SIDE 2 COLUMN

  -->
      
        <div class="clear">
        </div>
    </div>
    </asp:Content>


