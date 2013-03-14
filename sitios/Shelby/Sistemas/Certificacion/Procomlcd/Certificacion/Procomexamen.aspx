<%@ Page Language="vb" 
masterpagefile="~/sitios/Shelby/Shelby.Master"
AutoEventWireup="false"
 CodeBehind="Procomexamen.aspx.vb" 
 Inherits="procomlcd.Procomexamen" %>
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderShelby" runat="Server">


     
    <!--

POSTER PHOTO

-->
    <div id="poster-photo-container">
    
    <div id="poster-photo-image">
     <img src="../../../../Img/banner.jpg" width="900px" height ="110px" />
    </div>
         
          </div>
    <!--

CONTENT CONTAINER

-->
    <div id="contenido-columna-derecha">
        <!--

CONTENT MAIN COLUMN

-->
       <div class ="Examen2">
            <div class="Examenh3">
                <h3> Instrucciones para el realizar el examen de certificacion</h3>
            </div>
       </div> 
  
       <br />
       <div class="instru">
      Instrucciones:<br /> Estas a punto de iniciar tu examen de certificación.<br />
      Para que puedas realizar correctamente tu certificación debes de realizar lo siguiente:<br />
      Contestar cada pregunta, seleccionando la opción que tú creas adecuada. Cuando inicies tendras 10 minutos aprox. para contestar<br />
      5 preguntas por cada página. En total son 20 preguntas de opciones multiples; en dado caso de que tardes mas, se te cerrara la pagina y deberás <br />
      de Iniciar de nuevo el examen; podrás realizar 3 intentos para aprobar el examen piensa tu respuesta antes de seleccionarla.<br /><br />

    <b>Para seleccionar la respuesta solo desplaza el mouse sobre  la respuesta adecuada 
    y dale "clic izquierdo", como se realizara en el ejemplo siguiente:</b><br /><br />

        
        </div>
        
          <div class ="pregunta">
                                 <div  class="nombre">
                                     <div class="letritas"> 
                                            Selecciona que documento leiste para poder contestar y aprobar este examen...
                                     </div>
                                 </div>
                                        <div class ="respuestas">
                                             <asp:RadioButtonList ID="Rb1" runat="server"  >
                                                            <asp:ListItem Value="1">A) PDF del Sistema Procom </asp:ListItem>
                                                            <asp:ListItem Value="2">B) PDF del Sistema Operativo </asp:ListItem>
                                                            <asp:ListItem Value="3">C) PDF del Sistema Giro</asp:ListItem>
                                                            </asp:RadioButtonList>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                            ControlToValidate="Rb1" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                                        </div>
                   </div>
        
        
        
        
       <div class="next" >                  
          
               
             <asp:Button ID="btnSiguiente" runat="server" Text="Iniciar" CssClass="boton" />
    </div>
       
       
       
        <!--

CONTENT SIDE COLUMN AVISOS

-->
        
        <div class="clear">
        
        </div>
        
    </div>





</asp:Content>

