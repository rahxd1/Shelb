<%@ Page Language="vb"
 MasterPageFile ="~/sitios/Shelby/Shelby.Master"
 AutoEventWireup="false" 
 CodeBehind="Procompreguntas2.aspx.vb"
  Inherits="procomlcd.Procompreguntas2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderShelby" runat="Server">


     
    <!--

POSTER PHOTO

-->

    <!--

CONTENT CONTAINER

-->
    <div id="contenido-columna-derecha">
        <!--

CONTENT MAIN COLUMN

--><div class ="Examen">
       <h3> Examen de certificacion</h3>
   </div>
       <br />
       <div class="Examenn">
            <div class="ptitulo">
                    <div class="p1titulo">
                        Selecciona la respuesta Correcta
                    </div> 
            </div> 
                    <div class ="pregunta">
                                 <div  class="nombre">
                                     <div class="letritas"> 
                                            6.-¿Cual NO es un objetivo del sistema?
                                     </div>
                                 </div>
                                        <div class ="respuestas">
                                             <asp:RadioButtonList ID="Rb1" runat="server"  >
                                                             <asp:ListItem Value="1">A) Proporcionar una vía de comunicación para bajar señales a campo.</asp:ListItem>
                                                             <asp:ListItem Value="2">B) Solucionar tareas complejas de los promotores.</asp:ListItem>
                                                             <asp:ListItem Value="3">C) Cubrir las necesidades de información de nuestros clientes.</asp:ListItem>
                                                             <asp:ListItem Value="4">D) Todas las anteriores.</asp:ListItem>
                                             </asp:RadioButtonList>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                            ControlToValidate="Rb1" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                                        </div>
                   </div>
                   
                   <div class ="pregunta">
                                <div  class="nombre">
                                     <div class="letritas"> 
                                         7.-	Que parte del sistema podemos utilizar como medio de comunicación para Capacitaciones y Nuevas implementaciones.
                                     </div> 
                                </div>
                                            <div class ="respuestas">
                                                 <asp:RadioButtonList ID="Rb2" runat="server" >
                                                             <asp:ListItem Value="1">A) Sección Noticias.</asp:ListItem>
                                                             <asp:ListItem Value="2">B) Sección Capacitación.</asp:ListItem>
                                                             <asp:ListItem Value="3">C) Sección Avisos.</asp:ListItem>
                                                             <asp:ListItem Value="4">D) B y C son correctas.</asp:ListItem>
                                                 </asp:RadioButtonList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                            ControlToValidate="Rb2" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                                            </div> 
                    </div>  
                              
                   <div class ="pregunta">    
                               <div  class="nombre">
                                      <div class="letritas"> 
                                            8.- ¿Cuál es la dirección web para acceder al sistema?
                                      </div> 
                               </div>
                                        <div class ="respuestas">
                                             <asp:RadioButtonList ID="Rb3" runat="server"  >
                                                             <asp:ListItem Value="1">A) www.procomlcd.com.mx</asp:ListItem>
                                                             <asp:ListItem Value="2">B) www.procom.com.mx</asp:ListItem>
                                                             <asp:ListItem Value="3">C) www.procomlcd.com</asp:ListItem>
                                                             <asp:ListItem Value="4">D) Ninguna de la anterior</asp:ListItem>
                                             </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                        ControlToValidate="Rb3" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                                        </div>
                                
                    </div>   
                             
                   <div class ="pregunta"> 
                                  <div  class="nombre">
                                          <div class="letritas"> 
                                              9.- Herramienta del sistema que nos permite demostrar si un usuario accedió al sistema.
                                          </div>
                                  </div>
                                            <div class ="respuestas">
                                                 <asp:RadioButtonList ID="Rb4" runat="server" >
                                                                <asp:ListItem Value="1">A) Acceso a plataformas.</asp:ListItem>
                                                                <asp:ListItem Value="2">B) Sección de captura del sistema.</asp:ListItem>
                                                                <asp:ListItem Value="3">C) Bitácora en sistema.</asp:ListItem>
                                                                <asp:ListItem Value="4">D) A y C son correctas.</asp:ListItem>
                                                 </asp:RadioButtonList>
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                            ControlToValidate="Rb4" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                                            </div>
                                
                                
                    </div> 
                               
                   <div class ="pregunta"> 
                          <div class="nombre">
                                     <div class="letritas"> 
                                                10.- ¿Qué datos se deben de proporcionar por parte del usuario cuando se levanta un ticket en sistema?
                                     </div>
                          </div> 
                                        <div class ="respuestas">
                                         <asp:RadioButtonList ID="Rb5" runat="server"  >
                                                        <asp:ListItem Value="1">A) Login del sistema, su nombre y  teléfono.</asp:ListItem>
                                                        <asp:ListItem Value="2">B) Nombre, correo electrónico y teléfono.</asp:ListItem>
                                                        <asp:ListItem Value="3">C) A y B son correctas</asp:ListItem>
                                                        <asp:ListItem Value="4">D) Ninguna de las anteriores.</asp:ListItem>
                                                       
                                        </asp:RadioButtonList>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                        ControlToValidate="Rb5" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                                        </div>
                                
                    </div>            
     <div class="next" >                  
          
               
             <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" CssClass="boton" />
           
    </div>
                
                 
        <!--

CONTENT SIDE COLUMN AVISOS

-->
        
        <div class="clear">
        </div>
        
    </div>


<br />
<br />
<br />
<br />
<br />


    </div>
</asp:Content>