<%@ Page Language="vb" 
 MasterPageFile ="~/sitios/Shelby/Shelby.Master"
AutoEventWireup="false" 
CodeBehind="Procompreguntas3.aspx.vb"
 Inherits="procomlcd.Procompreguntas3" %>


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
                                            11.	Puedo acceder al sistema desde:
                                     </div>
                                 </div>
                                        <div class ="respuestas">
                                             <asp:RadioButtonList ID="Rb1" runat="server"  >
                                                             <asp:ListItem Value="1">A) Tabletas.</asp:ListItem>
                                                             <asp:ListItem Value="2">B) Celulares con acceso internet.</asp:ListItem>
                                                             <asp:ListItem Value="3">C) Computadoras con acceso a internet</asp:ListItem>
                                                             <asp:ListItem Value="4">D) Todas las anteriores.</asp:ListItem>
                                             </asp:RadioButtonList>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                            ControlToValidate="Rb1" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                                        </div>
                   </div>
                   
                   <div class ="pregunta">
                                <div  class="nombre">
                                     <div class="letritas"> 
                                            12.	¿Qué necesito para acceder a las plataformas de nuestros clientes en el sistema procom?
                                     </div> 
                                </div>
                                            <div class ="respuestas">
                                                 <asp:RadioButtonList ID="Rb2" runat="server" >
                                                             <asp:ListItem Value="1">A) Menú principal de la plataforma.</asp:ListItem>
                                                             <asp:ListItem Value="2">B) Usuario y Contraseña  de cada plataforma.</asp:ListItem>
                                                             <asp:ListItem Value="3">C) Permiso de mi usuario para acceder a cada plataforma.</asp:ListItem>
                                                             <asp:ListItem Value="4">D) B Y C son correctas</asp:ListItem>
                                                 </asp:RadioButtonList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                            ControlToValidate="Rb2" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                                            </div> 
                    </div>  
                              
                   <div class ="pregunta">    
                               <div  class="nombre">
                                      <div class="letritas"> 
                                            13.	Generar Reportes Ejecutivos y Dar información real de las tiendas a nuestros clientes son:
                                      </div> 
                               </div>
                                        <div class ="respuestas">
                                             <asp:RadioButtonList ID="Rb3" runat="server"  >
                                                             <asp:ListItem Value="1">A) Secciones del sistema.</asp:ListItem>
                                                             <asp:ListItem Value="2">B) Objetivos del sistema.</asp:ListItem>
                                                             <asp:ListItem Value="3">C) Módulos del sistema.</asp:ListItem>
                                                             <asp:ListItem Value="4">D) Todas las Anteriores.</asp:ListItem>
                                             </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                        ControlToValidate="Rb3" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                                        </div>
                                
                    </div>   
                             
                   <div class ="pregunta"> 
                                  <div  class="nombre">
                                          <div class="letritas"> 
                                              14.-  Su configuración es diferente, su diseño único para cada cliente
                                          </div>
                                  </div>
                                            <div class ="respuestas">
                                                 <asp:RadioButtonList ID="Rb4" runat="server" >
                                                                <asp:ListItem Value="1">A) Formato de Captura.</asp:ListItem>
                                                                <asp:ListItem Value="2">B) Reportes Ejecutivos.</asp:ListItem>
                                                                <asp:ListItem Value="3">C) Menú de cada plataforma.</asp:ListItem>
                                                                <asp:ListItem Value="4">D) Todas las anteriores.</asp:ListItem>
                                                 </asp:RadioButtonList>
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                            ControlToValidate="Rb4" ErrorMessage="* Debes de seleccionar alguna opcion"></asp:RequiredFieldValidator>
                                            </div>
                                
                                
                    </div> 
                               
                   <div class ="pregunta"> 
                          <div class="nombre">
                                     <div class="letritas"> 
                                               15.-  Que parte del sistema podemos utilizar para desarrollar evaluaciones y certificaciones.
                                     </div>
                          </div> 
                                        <div class ="respuestas">
                                         <asp:RadioButtonList ID="Rb5" runat="server"  >
                                                        <asp:ListItem Value="1">Modulo de 100certificado</asp:ListItem>
                                                        <asp:ListItem Value="2">Sección Capacitación.</asp:ListItem>
                                                        <asp:ListItem Value="3"> Join.me</asp:ListItem>
                                                        <asp:ListItem Value="4">B y C son correctas.</asp:ListItem>
                                                       
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