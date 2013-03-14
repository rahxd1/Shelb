<%@ Page Language="vb"
   MasterPageFile ="~/sitios/Shelby/Shelby.Master"
   AutoEventWireup="false"
   CodeBehind="Procompreguntas4.aspx.vb"
   Inherits="procomlcd.Procompreguntas4" %>


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
                                            16.-¿Al desarrollar una plataforma, que pasaría si no proporciono al área de sistemas todos los requerimientos.?
                                     </div>
                                 </div>
                                        <div class ="respuestas">
                                             <asp:RadioButtonList ID="Rb1" runat="server"  >
                                                             <asp:ListItem Value="1">A) Fallan las plataformas.</asp:ListItem>
                                                             <asp:ListItem Value="2">B) Los usuarios no tendrán sus bonos.</asp:ListItem>
                                                             <asp:ListItem Value="3">C) A y b son correctas</asp:ListItem>
                                                             <asp:ListItem Value="4">D) Ninguna de las anteriores.</asp:ListItem>
                                             </asp:RadioButtonList>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                            ControlToValidate="Rb1" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                                        </div>
                   </div>
                   
                   <div class ="pregunta">
                                <div  class="nombre">
                                     <div class="letritas"> 
                                         17.-	¿Qué elementos se definen en la creación de las plataformas?
                                     </div> 
                                </div>
                                            <div class ="respuestas">
                                                 <asp:RadioButtonList ID="Rb2" runat="server" >
                                                             <asp:ListItem Value="1">A) Rutas del promotor.</asp:ListItem>
                                                             <asp:ListItem Value="2">B) Menú del formato de Captura.</asp:ListItem>
                                                             <asp:ListItem Value="3">C) Formato de la plataforma.</asp:ListItem>
                                                             <asp:ListItem Value="4">D) Todas las anteriores.</asp:ListItem>
                                                             <asp:ListItem Value="5">E) Ninguna de las anteriores.</asp:ListItem>
                                                 </asp:RadioButtonList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                            ControlToValidate="Rb2" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                                            </div> 
                    </div>  
                              
                   <div class ="pregunta">    
                               <div  class="nombre">
                                      <div class="letritas"> 
                                            18.- ¿Quién debe de validar que la  información capturada por el promotor sea real en campo?
                                      </div> 
                               </div>
                                        <div class ="respuestas">
                                             <asp:RadioButtonList ID="Rb3" runat="server"  >
                                                             <asp:ListItem Value="1">A) Auditor de la información</asp:ListItem>
                                                             <asp:ListItem Value="2">B) Área de operaciones.</asp:ListItem>
                                                             <asp:ListItem Value="3">C) Supervisor de la información.</asp:ListItem>
                                                             <asp:ListItem Value="4">D) A  y B son correctas.</asp:ListItem>
                                             </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                        ControlToValidate="Rb3" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                                        </div>
                                
                    </div>   
                             
                   <div class ="pregunta"> 
                                  <div  class="nombre">
                                          <div class="letritas"> 
                                              19.- ¿Siempre está disponible el sistema?
                                          </div>
                                  </div>
                                            <div class ="respuestas">
                                                 <asp:RadioButtonList ID="Rb4" runat="server" >
                                                                <asp:ListItem Value="1">A) Si, pues está disponible las 24 hrs.</asp:ListItem>
                                                                <asp:ListItem Value="2">B) No, porque puede haber mantenimiento del sistema.</asp:ListItem>
                                                                <asp:ListItem Value="3">C) Sí, porque está en internet.</asp:ListItem>
                                                                <asp:ListItem Value="4">D) No, porque el soporte es de 9:00am a 10:00pm.</asp:ListItem>
                                                 </asp:RadioButtonList>
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                            ControlToValidate="Rb4" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                                            </div>
                                
                                
                    </div> 
                               
                   <div class ="pregunta"> 
                          <div class="nombre">
                                     <div class="letritas"> 
                                                20.- Con la versión Móvil del sistema puedo acceder desde:
                                     </div>
                          </div> 
                                        <div class ="respuestas">
                                         <asp:RadioButtonList ID="Rb5" runat="server"  >
                                                        <asp:ListItem Value="1">A) Tabletas</asp:ListItem>
                                                        <asp:ListItem Value="2">B) Celulares con internet</asp:ListItem>
                                                        <asp:ListItem Value="3">C) A y B son correctas</asp:ListItem>
                                                        <asp:ListItem Value="4">D) Todas las anteriores.</asp:ListItem>
                                                        <asp:ListItem Value="5">E) Ninguna de las anteriores.</asp:ListItem>
                                                       
                                        </asp:RadioButtonList>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                        ControlToValidate="Rb5" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                                        </div>
                                
                    </div>            
     <div class="next" >                  
          
               
             <asp:Button ID="btnSiguiente" runat="server" Text="Terminar" CssClass="boton" />
           
             
           
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