
Module Admin
    Public Sub LstProyectos(ByVal Conexion As String, ByVal Grilla As GridView)
        CargaGrilla(Conexion, "select * from Administracion_Proyectos", _
                        Grilla)
    End Sub
End Module

