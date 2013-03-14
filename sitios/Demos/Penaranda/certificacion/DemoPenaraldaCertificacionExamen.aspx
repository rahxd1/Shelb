<%@ Page Language="vb"
Masterpagefile="~/sitios/Demos/Penaranda/DemoPenaranda.Master"
 AutoEventWireup="false" 
 CodeBehind="DemoPenaraldaCertificacionExamen.aspx.vb"
 Inherits="procomlcd.DemoPenaraldaCertificacionExamen" %>
 
 <asp:Content ID="Content1" ContentPlaceHolderID="DemoPenarandaContent" runat="Server">
     <!--

POSTER PHOTO

-->
   
    <!--

CONTENT CONTAINER

-->
    <div id="contenido-columna-derecha">
        <!--

CONTENT MAIN COLUMN

-->
        <div id="content-main-two-column">

            <table style="width: 100%">
                <tr>
                    <td style="height: 19px; text-align: right;">

                        
                        <asp:HyperLink ID="HyperLink7" runat="server" 
                            
                            NavigateUrl="~/sitios/Demos/Penaranda/certificacion/DemoPenaraldaCertificacion.aspx">&lt;-- Regresar</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                <!-- celda animacion flash-->
                    <td style="height: 19px">

                        
                        <br />
                        <asp:Label ID="lblAviso" runat="server" 
                            style="color: #FF3300; font-weight: 700" Text="Label" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; color: #FFFFFF; background-color: #004020; height: 19px;">
                        Examen de Certificacion </td>
                </tr>
                <tr>
                    <td style="text-align: left; height: 20px;">
                         
                         

                         

                        Instrucciones:<span style="color: #006699"> Hola, estas a punto de iniciar tu 
                        examen de certificacion. </span></td>
                </tr>
                <tr>
                    <td style="text-align: justify; height: 20px; color: #006699;">
                         
                         

                         

                        Para que puedas realizar correctamente tu certificacion debes de realizar lo 
                        siguiente:</td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF; color: #006699; text-align: justify;">
                        Contestar cada pregunta, seleccionando la opcion que tu creas adecuada. tienes 
                        10 minutos aprox. para contestar </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF; color: #006699; text-align: justify;">
                        dos preguntas por cada pagina. en total son 20; en dado caso de que tardes mas, 
                        se te cerrara la pagina y deberas </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF; color: #006699; text-align: justify;">
                        de Iniciar; solo se puede realizar una vez el examen asi que piensa bien tu 
                        respuesta antes de selecionarla.</td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF; height: 19px;">
                        </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF; color: #FF0000; height: 32px;">
                        Da Clic en el Boton Iniciar --&gt;<asp:Button ID="btnIniciar" runat="server" 
                            Text="Iniciar" />
                    </td>
                </tr>
              
                <tr>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="background-color: #004020">
                        &nbsp;</td>
                </tr>
                </table>
        </div>
        <!--

CONTENT SIDE COLUMN AVISOS

-->
        <div id="content-side-two-column">
            <h3>
                &nbsp;</h3>
            <p style="text-align: center; color: #CC0000;">
                    &nbsp;</p>
                    <br />
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                                            &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
               </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        
        <div class="clear" style="color: #FFFFFF">
            .<br />
      
        
        </div>
        
    </div>
</asp:Content>

