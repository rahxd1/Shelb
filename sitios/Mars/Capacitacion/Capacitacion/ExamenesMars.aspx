<%@ Page Language="vb" 
    masterpagefile="~/sitios/Mars/Capacitacion/Capacitacion.master"
    AutoEventWireup="false" 
    CodeBehind="ExamenesMars.aspx.vb" 
    Inherits="procomlcd.ExamenesMars" %>

<asp:Content id="Content1" ContentPlaceHolderID="ContentPlaceHolderCapacitacion" runat="Server">
    <!--
Title Under Menu
-->
    <div id="titulo-pagina">
        Comunicación</div>
    <div id="contenido-three-column2">
        <!--

  CONTENT SIDE 1 COLUMN

  -->
        <div id="content-side1-three-column">
            <ul class="list-of-links">
                <li><a href="~/Evaluaciones/Examenes.aspx"></a></li>
                <li><a href="~/Evaluaciones/Resultados.aspx"></a></li>
            </ul>
        </div>
        <!--

  CENTER COLUMN

  -->
        <div id="content-main-three-column">
            
                      
            <b style="text-align: center">
            <br />
            <br />
            
            <asp:Label ID="Label1" runat="server" Text="EN CONSTRUCCIÓN" Font-Bold="True" 
                Font-Size="XX-Large" ForeColor="Red"></asp:Label>
            
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            </b>
            
                      
        </div>
        <!-- END MAIN COLUMN -->
        <!--

  CONTENT SIDE 2 COLUMN

  -->
        <div class="clear">
        </div>
    </div>
</asp:Content>
