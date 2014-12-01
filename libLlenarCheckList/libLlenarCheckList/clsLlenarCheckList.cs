using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//referenciar y usar
using System.Windows.Forms;
using System.Web.UI.WebControls;
using LibconecxionBD;

namespace libLlenarCheckList
{
    public class clsLlenarCheckList
    {

        #region"Constructor"
        public clsLlenarCheckList()
        {
            strSQL = string.Empty;
            strNombreTabla = string.Empty;
            strError = string.Empty;
            strColumnaTexto = string.Empty;
            strColumnaValor = string.Empty;
        }
        #endregion
        #region"Atributos"
        private string strSQL;
        private string strNombreTabla;
        private string strColumnaTexto;
        private string strColumnaValor;
        private string strError;
        #endregion

        #region"Propiedades"
        public string SQL
        { set { strSQL = value; } }

        public string NombreTabla
        { set { strNombreTabla = value; } }

        public string ColumnaTexto
        { set { strColumnaTexto = value; } }

        public string ColumnaValor
        { set { strColumnaValor = value; } }

        public string Error
        { get { return strError; } }
        #endregion

        #region"Metodos Privados"
        private bool Validar()
        {
            if (string.IsNullOrEmpty(strSQL))
            {
                strError = "Debe definir la instrucción SQL";
                return false;
            }
            if (string.IsNullOrEmpty(strColumnaTexto))
            {
                strError = "Debe definir el nombre de la columna: Texto";
                return false;
            }
            if (string.IsNullOrEmpty(strColumnaValor))
            {
                strError = "Debe definir el nombre de la columna: Valor";
                return false;
            }

            if (string.IsNullOrEmpty(strNombreTabla))
                strNombreTabla = "Tabla";
            return true;
        }
        #endregion

        #region"Metodos Publicos"
        public bool LlenarCombo_Windows(System.Windows.Forms.CheckedListBox Generico)
        {
            if (!Validar())
                return false;
            clsConexionBD objConecionBD = new clsConexionBD();
            objConecionBD.SQL = strSQL;
            objConecionBD.NombreTabla = strNombreTabla;

            if (!objConecionBD.LlenarDataSet(false))
            {
                strError = objConecionBD.Error;
                objConecionBD.CerrarConexion();
                objConecionBD = null;
                return false;
            }
            
            Generico.DataSource = objConecionBD.MiDataSet.Tables[strNombreTabla];
            Generico.DisplayMember = strColumnaTexto;
            Generico.ValueMember = strColumnaValor;
            Generico.Refresh();
            objConecionBD.CerrarConexion();
            objConecionBD = null;
            return true;
        }

        public bool LlenarGrid_Web(System.Web.UI.WebControls.CheckBoxList Generico)
        {
            if (!Validar())
                return false;
            clsConexionBD objConexionBD = new clsConexionBD();
            objConexionBD.SQL = strSQL;
            objConexionBD.NombreTabla = strNombreTabla;

            if (!objConexionBD.LlenarDataSet(false))
            {
                strError = objConexionBD.Error;
                objConexionBD.CerrarConexion();
                objConexionBD = null;
                return false;
            }
            Generico.DataSource = objConexionBD.MiDataSet.Tables[strNombreTabla];
            Generico.DataTextField = strColumnaTexto;
            Generico.DataValueField = strColumnaValor;
            Generico.DataBind();
            objConexionBD.CerrarConexion();
            objConexionBD = null;
            return true;
        }
        #endregion

    }

    public class clsLlenarCheckListMySQL
    {

        #region"Constructor"
        public clsLlenarCheckListMySQL()
        {
            strSQL = string.Empty;
            strNombreTabla = string.Empty;
            strError = string.Empty;
            strColumnaTexto = string.Empty;
            strColumnaValor = string.Empty;
        }
        #endregion
        #region"Atributos"
        private string strSQL;
        private string strNombreTabla;
        private string strColumnaTexto;
        private string strColumnaValor;
        private string strError;
        #endregion

        #region"Propiedades"
        public string SQL
        { set { strSQL = value; } }

        public string NombreTabla
        { set { strNombreTabla = value; } }

        public string ColumnaTexto
        { set { strColumnaTexto = value; } }

        public string ColumnaValor
        { set { strColumnaValor = value; } }

        public string Error
        { get { return strError; } }
        #endregion

        #region"Metodos Privados"
        private bool Validar()
        {
            if (string.IsNullOrEmpty(strSQL))
            {
                strError = "Debe definir la instrucción SQL";
                return false;
            }
            if (string.IsNullOrEmpty(strColumnaTexto))
            {
                strError = "Debe definir el nombre de la columna: Texto";
                return false;
            }
            if (string.IsNullOrEmpty(strColumnaValor))
            {
                strError = "Debe definir el nombre de la columna: Valor";
                return false;
            }

            if (string.IsNullOrEmpty(strNombreTabla))
                strNombreTabla = "Tabla";
            return true;
        }
        #endregion

        #region"Metodos Publicos"
        public bool LlenarCombo_Windows(System.Windows.Forms.CheckedListBox Generico)
        {
            if (!Validar())
                return false;
            clsConexionMySQLDB objConecionBD = new clsConexionMySQLDB();
            objConecionBD.SQL = strSQL;
            objConecionBD.NombreTabla = strNombreTabla;

            if (!objConecionBD.LlenarDataSet(false))
            {
                strError = objConecionBD.Error;
                objConecionBD.CerrarConexion();
                objConecionBD = null;
                return false;
            }

            Generico.DataSource = objConecionBD.MiDataSet.Tables[strNombreTabla];
            Generico.DisplayMember = strColumnaTexto;
            Generico.ValueMember = strColumnaValor;
            Generico.Refresh();
            objConecionBD.CerrarConexion();
            objConecionBD = null;
            return true;
        }

        public bool LlenarGrid_Web(System.Web.UI.WebControls.CheckBoxList Generico)
        {
            if (!Validar())
                return false;
            clsConexionMySQLDB objConexionBD = new clsConexionMySQLDB();
            objConexionBD.SQL = strSQL;
            objConexionBD.NombreTabla = strNombreTabla;

            if (!objConexionBD.LlenarDataSet(false))
            {
                strError = objConexionBD.Error;
                objConexionBD.CerrarConexion();
                objConexionBD = null;
                return false;
            }
            Generico.DataSource = objConexionBD.MiDataSet.Tables[strNombreTabla];
            Generico.DataTextField = strColumnaTexto;
            Generico.DataValueField = strColumnaValor;
            Generico.DataBind();
            objConexionBD.CerrarConexion();
            objConexionBD = null;
            return true;
        }
        #endregion

    }
}
