<%@ Page Language="vb"
   MasterPageFile="~/sitios/Shelby/Shelby.Master"
  AutoEventWireup="false"
  CodeBehind="Procompreguntas.aspx.vb" 
  Inherits="procomlcd.Procompreguntas" %>

 
 
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
        <div class="Examenh3">
            <h3> Examen de certificacion</h3>
        </div>
   </div>

       <div class="Examenn">
            <div class="ptitulo">
                    <div class="p1titulo">
                        Selecciona la respuesta Correcta
                    </div> 
            </div> 
             <br />
                    <div class ="pregunta">
                                 <div  class="nombre">
                                     <div class="letritas"> 
                                            1.-El área de sistemas diseña las plataformas en base a los comentarios  del  área de operaciones de shelby.
                                     </div>
                                 </div>
                                        <div class ="respuestas">
                                             <asp:RadioButtonList ID="Rb1" runat="server" Width="156px" >
                                                            <asp:ListItem Value="1">A) Verdadero</asp:ListItem>
                                                            <asp:ListItem Value="2">B) Falso</asp:ListItem>
                                                            </asp:RadioButtonList>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                            ControlToValidate="Rb1" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                                        </div>
                   </div>
                   
                   <div class ="pregunta">
                                <div  class="nombre">
                                     <div class="letritas"> 
                                         2.-	¿Está disponible el sistema 24 horas?
                                     </div> 
                                </div>
                                            <div class ="respuestas">
                                                 <asp:RadioButtonList ID="Rb2" runat="server" Width="189px" >
                                                               <asp:ListItem Value="1">A) Verdadero</asp:ListItem>
                                                               <asp:ListItem Value="2">B) Falso</asp:ListItem>
                                                 </asp:RadioButtonList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                            ControlToValidate="Rb2" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                                            </div> 
                    </div>  
                              
                   <div class ="pregunta">    
                               <div  class="nombre">
                                      <div class="letritas"> 
                                            3.- ¿Puedo acceder desde cualquier navegador web?
                                      </div> 
                               </div>
                                        <div class ="respuestas">
                                             <asp:RadioButtonList ID="Rb3" runat="server" Width="199px" >
                                                             <asp:ListItem Value="1">A) Verdadero</asp:ListItem>
                                                             <asp:ListItem Value="2">B) Falso</asp:ListItem>
                                             </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                        ControlToValidate="Rb3" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                                        </div>
                                
                    </div>   
                             
                   <div class ="pregunta"> 
                                  <div  class="nombre">
                                          <div class="letritas"> 
                                              4.- ¿Qué es el sistema PROCOM?
                                          </div>
                                  </div>
                                            <div class ="respuestas">
                                                 <asp:RadioButtonList ID="Rb4" runat="server" >
                                                                <asp:ListItem Value="1">A) sistema para llevar el control de asistencia de los promotores.</asp:ListItem>
                                                                <asp:ListItem Value="2">B) Herramienta para tomar decisiones de los bonos del promotor.</asp:ListItem>
                                                                <asp:ListItem Value="3">C) Sistema  de información para la toma de decisiones de nuestros clientes.</asp:ListItem>
                                                                <asp:ListItem Value="4">D) Todas las anteriores.</asp:ListItem>
                                                 </asp:RadioButtonList>
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                            ControlToValidate="Rb4" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                                            </div>
                                
                                
                    </div> 
                               
                   <div class ="pregunta"> 
                          <div class="nombre">
                                     <div class="letritas"> 
                                                5.- Los usuarios para recibir soporte en el sistema deben de contactar directamente a
                                     </div>
                          </div> 
                                        <div class ="respuestas">
                                         <asp:RadioButtonList ID="Rb5" runat="server" Width="258px" >
                                                        <asp:ListItem Value="1">A) Jefes  de Operaciones.</asp:ListItem>
                                                        <asp:ListItem Value="2">B) Supervisores PROCOM.</asp:ListItem>
                                                        <asp:ListItem Value="3">C) Ejecutiva de Cuenta.</asp:ListItem>
                                                        <asp:ListItem Value="4">D) A y C son Correctas.</asp:ListItem>
                                                        <asp:ListItem Value="5">E) Ninguna de las anteriores.</asp:ListItem>
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

