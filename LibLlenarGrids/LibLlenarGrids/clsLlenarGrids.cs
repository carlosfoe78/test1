using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using LibconecxionBD;


namespace LibLlenarGrids
{
    public class clsLlenarGrids
    {
        #region"Constructor"
        public clsLlenarGrids()
        {
        strSQL=string.Empty;
        strNombreTabla=string.Empty;
        strError=string.Empty;
        }
        #endregion
        #region"Atributos"
        private string strSQL;
        private string strNombreTabla;
        private string strError;
        #endregion        
        #region"Propiedades"
        public string SQL
        { set { strSQL = value; } }

        public string NombreTabla
        { set { strNombreTabla = value; } }

        public string Error
        { get { return strError; } }
        #endregion        
        #region"Metodos Privados"
        private bool Validar()
        { 
        if(string.IsNullOrEmpty(strSQL))
            {
            strError="Debe definir la instrucción SQL";
            return false;
            }
        if (string.IsNullOrEmpty(strNombreTabla))
            strNombreTabla = "Tabla";
        return true;
        }
        #endregion        
        #region"Metodos Publicos"
        public bool LlenarGrid_Windows(DataGridView Generico)
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
            Generico.Refresh();
            objConecionBD.CerrarConexion();
            objConecionBD = null;
            return true;
        }

        public bool LlenarGrid_Web(System.Web.UI.WebControls.GridView Generico)
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
            Generico.DataBind();
            objConexionBD.CerrarConexion();
            objConexionBD = null;
            return true;
        }
        #endregion

    }

    public class clsLlenarGridMySQL
    { 
        #region"Constructor"
        public clsLlenarGridMySQL()
        {
        strSQL=string.Empty;
        strNombreTabla=string.Empty;
        strError=string.Empty;
        }
        #endregion
        #region"Atributos"
        private string strSQL;
        private string strNombreTabla;
        private string strError;
        #endregion        
        #region"Propiedades"
        public string SQL
        { set { strSQL = value; } }

        public string NombreTabla
        { set { strNombreTabla = value; } }

        public string Error
        { get { return strError; } }
        #endregion        
        #region"Metodos Privados"
        
        private bool Validar()
        { 
        if(string.IsNullOrEmpty(strSQL))
            {
            strError="Debe definir la instrucción SQL";
            return false;
            }
        if (string.IsNullOrEmpty(strNombreTabla))
            strNombreTabla = "Tabla";
        return true;
        }
        #endregion        
        #region"Metodos Publicos"
        public bool LlenarGrid_Windows(DataGridView Generico)
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
            Generico.Refresh();
            objConecionBD.CerrarConexion();
            objConecionBD = null;
            return true;
        }

        public bool LlenarGrid_Web(System.Web.UI.WebControls.GridView Generico)
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
            Generico.DataBind();
            objConexionBD.CerrarConexion();
            objConexionBD = null;
            return true;
        }
        #endregion
    
    }

}
