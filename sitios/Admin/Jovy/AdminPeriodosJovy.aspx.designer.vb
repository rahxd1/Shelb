﻿'------------------------------------------------------------------------------
' <auto-generated>
'     Este código fue generado por una herramienta.
'     Versión del motor en tiempo de ejecución:2.0.50727.4927
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On



Partial Public Class AdminPeriodosJovy

    '''<summary>
    '''Control ScriptManager.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ScriptManager As Global.System.Web.UI.ScriptManager

    '''<summary>
    '''Control updPnl.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents updPnl As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control lnkNuevo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lnkNuevo As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Control lnkConsultas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lnkConsultas As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Control lblAviso.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblAviso As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control pnlConsulta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlConsulta As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control gridPeriodo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents gridPeriodo As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Control pnlNuevo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlNuevo As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control lblIDPeriodo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblIDPeriodo As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblNombrePeriodo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblNombrePeriodo As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtNombrePeriodo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtNombrePeriodo As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control rfvNombrePeriodo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rfvNombrePeriodo As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control lblFechaInicio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblFechaInicio As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtFechaInicio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtFechaInicio As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control FechaInicio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FechaInicio As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control lblFechaFin.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblFechaFin As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtFechaFin.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtFechaFin As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control rfvFechaFin.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rfvFechaFin As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control lblFechaCierre.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblFechaCierre As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtFechaCierre.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtFechaCierre As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control rfvFechaCierre.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rfvFechaCierre As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control vsPeriodo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents vsPeriodo As Global.System.Web.UI.WebControls.ValidationSummary

    '''<summary>
    '''Control btnGuardar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnGuardar As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control btnCancelar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnCancelar As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control UpdateProgress.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdateProgress As Global.System.Web.UI.UpdateProgress
End Class
