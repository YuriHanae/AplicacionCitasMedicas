'------------------------------------------------------------------------------
' <generado automáticamente>
'     Este código fue generado por una herramienta.
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código. 
' </generado automáticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class frmPaciente

    '''<summary>
    '''Control btnCerrarSesion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnCerrarSesion As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control lblNombrePaciente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblNombrePaciente As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control btnCrearCita.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnCrearCita As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control btnEditarCita.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnEditarCita As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control btnMostrarCitas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnMostrarCitas As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control pnlCrearCita.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlCrearCita As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control lblCrearCitaError.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblCrearCitaError As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtFecha.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtFecha As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtHora.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtHora As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control ddlDoctor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlDoctor As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtObservaciones.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtObservaciones As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnGuardarCita.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnGuardarCita As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control pnlEditarCitaBuscar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlEditarCitaBuscar As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control lblEditarCitaError.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblEditarCitaError As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtBuscarIdCita.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtBuscarIdCita As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnBuscarCita.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnBuscarCita As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control pnlEditarCitaForm.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlEditarCitaForm As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control txtEditFecha.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtEditFecha As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtEditHora.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtEditHora As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtEditObservaciones.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtEditObservaciones As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnActualizarCita.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnActualizarCita As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control pnlMostrarCitas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlMostrarCitas As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control gvCitas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents gvCitas As Global.System.Web.UI.WebControls.GridView
End Class
