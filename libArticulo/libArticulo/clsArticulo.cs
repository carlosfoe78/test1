using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibconecxionBD;
using MySql.Data;
using MySql.Data.MySqlClient;
using LibLlenarCombos;
using System.Web.UI.WebControls;
using LibLlenarGrids;
using clsInventario;



namespace libArticulo
{
    public class clsArticulo
    {
        #region"constructor"
        public clsArticulo()
        {
            strIdArticulo = string.Empty;
            strNombre = string.Empty;
            intIdUnd = 0;
            intTipoArticulo = 0;
            blnlistar = true;
            blnDescontidunado = false;
            intIdLineaProd = 0;
            strCodSC = string.Empty;
            //intIdTipoArticulo=0;
            dblCantInventario = 0;
            dblCantMinInventario = 0;
            dblValorUnd = 0;
            dblPesoMillar = 0;
            dblVlrProducion = 0;
            dblVlrVenta = 0;
            strFormaEmpaque = string.Empty;
            strMatFabricacion = string.Empty;
            intidPolimero = 0;
            intCalibre = 0;
            dbllargo = 0;
            dblancho = 0;
            intidFormaMat = 0;
            intidColor = 0;

            strError = string.Empty;
            strSQL = string.Empty;
        }
        #endregion
        #region "Atributos"
        private string strIdArticulo;
        private string strNombre;
        private int intIdUnd; //unidad de medida
        private int intTipoArticulo;
        private bool blnlistar;
        private bool blnDescontidunado;
        private int intIdLineaProd;
        private string strCodSC;
        //private int intIdTipoArticulo;
        private double dblCantInventario;
        private double dblCantMinInventario;
        private double dblValorUnd;
        //DETALLE DE PRODUCTO TERMINADO
        private double dblPesoMillar;
        private double dblVlrProducion;
        private double dblVlrVenta;
        private string strFormaEmpaque;
        private string strMatFabricacion;
        private int intUndEmpaque;
        //private int idLineaProd;
        private string strIdMatEmpaque;
        //DETALLE DE MATERIALES
        private int intidPolimero;
        private int intCalibre;
        private double dbllargo;
        private double dblancho;
        private int intidFormaMat;
        private int intidColor;

        private string strError;
        private string strSQL;
        private clsConexionMySQLDB objCnx;
        private MySqlDataReader Reader_local;
        #endregion
        #region "Propiedades"
        public string CodArticulo
        {
            set { strIdArticulo = value; }
            get { return strIdArticulo; }
        }
        public string NombreArticulo
        {
            set { strNombre = value; }
            get { return strNombre; }
        }
        public int idTipoArticulo
        {
            set { intTipoArticulo = value; }
            get { return intTipoArticulo; }
        }
        public bool listar
        {
            set { blnlistar = value; }
            get { return blnlistar; }
        }
        public bool Descontidunado
        {
            set { blnDescontidunado = value; }
            get { return blnDescontidunado; }
        }

        public string CodigoSisContable
        {
            set { strCodSC = value; }
            get { return strCodSC; }
        }

        public int UndMedida
        {
            set { intIdUnd = value; }
            get { return intIdUnd; }
        }

        public double CantidadInventario
        {
            set { dblCantInventario = value; }
            get { return dblCantInventario; }
        }

        public double CantidadMinimaInventario
        {
            set { dblCantMinInventario = value; }
            get { return dblCantMinInventario; }
        }
        public double ValorUnd
        {
            set { dblValorUnd = value; }
            get { return dblValorUnd; }
        }

        public double PesoMillar
        {
            set { dblPesoMillar = value; }
            get { return dblPesoMillar; }
        }
        public double VlrProducion
        {
            set { dblVlrProducion = value; }
            get { return dblVlrProducion; }
        }
        public double VlrVenta
        {
            set { dblVlrVenta = value; }
            get { return dblVlrVenta; }
        }
        public string FormaEmpaque
        {
            set { strFormaEmpaque = value; }
            get { return strFormaEmpaque; }
        }
        public int UndEmpaque
        {
            set { intUndEmpaque = value; }
            get { return intUndEmpaque; }
        }
        public int LineaProduccion
        {
            set { intIdLineaProd = value; }
            get { return intIdLineaProd; }
        }
        public string idMatEmpaque 
        {
            set { strIdMatEmpaque = value; }
            get { return strIdMatEmpaque; }
        }
     
        public string MaterialFabricacion
        {
            set { strMatFabricacion = value; }
            get { return strMatFabricacion; }
        }
        public int idPolimero
        {
            set { intidPolimero = value; }
            get { return intidPolimero; }
        }
        public int Calibre
        {
            set { intCalibre = value; }
            get { return intCalibre; }
        }
        public double largo
        {
            set { dbllargo = value; }
            get { return dbllargo; }
        }
        public double ancho
        {
            set { dblancho = value; }
            get { return dblancho; }
        }
        public int idFormaMat
        {
            set { intidFormaMat = value; }
            get { return intidFormaMat; }
        }
        public int idColor
        {
            set { intidColor = value; }
            get { return intidColor; }
        }
        public string Error
        {
            get { return strError; }
        }
        #endregion
        #region"MetodosPrivados"
        private bool Grabar(string SQL)
        {
            try
            {
                clsConexionMySQLDB objCnx = new clsConexionMySQLDB();
                objCnx.SQL = SQL;
                if (!objCnx.ConsultarValorUnico(false))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }
                strIdArticulo = objCnx.ValorUnico;
                objCnx.CerrarConexion();
                objCnx = null;
                return true;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        #endregion
        #region"MetodosPublicos"
        public bool LlenarCombo(DropDownList Combo_A_llenar)
        {
            try
            {
                if (Combo_A_llenar == null)
                {
                    strError = "Combo nulo";
                    return false;
                }
                clsLlenarCombosMySQL objLlenar = new clsLlenarCombosMySQL();
                objLlenar.SQL = "call usp_llenarComboArticuloXCod;";
                objLlenar.NombreTabla = "tblCombo";
                objLlenar.ColumnaValor = "Codigo";
                objLlenar.ColumnaTexto = "Nombre";
                if (!objLlenar.LlenarCombo_Web(Combo_A_llenar))
                {
                    strError = objLlenar.Error;
                    objLlenar = null;
                    return false;
                }
                objLlenar = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        #region"Metodos datos basicos"
        // llenar combo articulo segun el tipo
        public bool LlenarComboXCodTipo(DropDownList Combo_A_llenar, int intCodTipoArt)
        {
            try
            {
                if (Combo_A_llenar == null)
                {
                    strError = "Combo nulo";
                    return false;
                }
                clsLlenarCombosMySQL objLlenar = new clsLlenarCombosMySQL();
                objLlenar.SQL = "call usp_llenarComboArticuloXTipo (" + intCodTipoArt + ");";
                objLlenar.NombreTabla = "tblCombo";
                objLlenar.ColumnaValor = "Codigo";
                objLlenar.ColumnaTexto = "Nombre";
                if (!objLlenar.LlenarCombo_Web(Combo_A_llenar))
                {
                    strError = objLlenar.Error;
                    objLlenar = null;
                    return false;
                }
                objLlenar = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        public bool LlenarComboPTTF(DropDownList Combo_A_llenar)
        {
            try
            {
                if (Combo_A_llenar == null)
                {
                    strError = "Combo nulo";
                    return false;
                }
                clsLlenarCombosMySQL objLlenar = new clsLlenarCombosMySQL();
                objLlenar.SQL = "call usp_llenarComboArticuloTF ();";
                objLlenar.NombreTabla = "tblCombo";
                objLlenar.ColumnaValor = "Codigo";
                objLlenar.ColumnaTexto = "Nombre";
                if (!objLlenar.LlenarCombo_Web(Combo_A_llenar))
                {
                    strError = objLlenar.Error;
                    objLlenar = null;
                    return false;
                }
                objLlenar = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        public bool LlenarGridConsulta(GridView Grid_Llenar, int intTArticulo, string strIDArticulo, int intIDProvedor, int intIDEstado)
        {
            try
            {
                if (Grid_Llenar == null)
                {
                    strError = "Grid es nulo";
                    return false;
                }

                strSQL = "call USP_InverarioConsulta (" + intTArticulo + ",'" + strIDArticulo + "'," + intIDProvedor + "," + intIDEstado + ");";

                clsLlenarGridMySQL objetoGrids = new clsLlenarGridMySQL();
                objetoGrids.SQL = strSQL;
                objetoGrids.NombreTabla = "tblLlenar";

                if (!objetoGrids.LlenarGrid_Web(Grid_Llenar))
                {
                    strError = objetoGrids.Error;
                    objetoGrids = null;
                    return false;
                }

                objetoGrids = null;
                return true;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        public bool BuscarMaestro(string strCodArticulo)
        {
            try
            {
                if (string.IsNullOrEmpty(strCodArticulo))
                {
                    strError = "No definio un codigo de articulo o no es valido";
                    return false;
                }

                strSQL = "call usp_BuscarArticuloXCodigo ('" + strCodArticulo + "');";
                objCnx = new clsConexionMySQLDB();
                objCnx.SQL = strSQL;
                if (!objCnx.Consultar(false))
                {
                    strError = objCnx.Error;
                    return false;
                }
                Reader_local = objCnx.Reader;
                if (!Reader_local.HasRows)
                {
                    strError = "No se encontro ningún registro: " + strCodArticulo;
                    Reader_local.Close();
                    objCnx = null;
                    return false;
                }

                Reader_local.Read();
                intIdUnd = Reader_local.GetInt32(0);
                intTipoArticulo = Reader_local.GetInt32(1);
                blnlistar = Reader_local.GetInt32(2) == 1 ? true : false;
                dblCantInventario = Reader_local.GetDouble(3);
                dblCantMinInventario = Reader_local.GetDouble(4);
                dblValorUnd = Reader_local.GetDouble(5);
                blnDescontidunado=Reader_local.GetInt32(6) == 1 ? true : false;
                strNombre = Reader_local.GetString(7);
                strCodSC = Reader_local.IsDBNull(8)?string.Empty:Reader_local.GetString(8);
                intIdLineaProd =  Reader_local.IsDBNull(9)?0:Reader_local.GetInt32(9);
                Reader_local.Close();
                objCnx.CerrarConexion();
                objCnx = null;
                return true;
            }


            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }

        }
        public bool GrabarMaestro()
        {
           try
            {
                objCnx = new clsConexionMySQLDB();
                strSQL = "usp_GrabarCabezeraArticulo";
                objCnx.SQL = strSQL;

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_id_art", System.Data.DbType.String, 18, strIdArticulo))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_Nombre", System.Data.DbType.String, 80, strNombre))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_idUnd", System.Data.DbType.UInt32, 1, intIdUnd))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_id_TipoArticulo", System.Data.DbType.UInt32, 1, intTipoArticulo))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_listar", System.Data.DbType.Boolean, 1, blnlistar))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_CantInventario", System.Data.DbType.Double, 12, dblCantInventario))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }
                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_CantMinInventario", System.Data.DbType.Double, 12, dblCantMinInventario))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }
                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_ValorUnd", System.Data.DbType.Double, 12, dblValorUnd))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_Descontinuado", System.Data.DbType.Boolean, 1, blnDescontidunado))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_CodSC", System.Data.DbType.String, 16, strCodSC, string.IsNullOrEmpty(strCodSC)?true:false))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.ConsultarValorUnico(true))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                strIdArticulo = objCnx.ValorUnico;
                objCnx.CerrarConexion();
                objCnx = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }


        }
        public bool ModificarMaestro()
        {
            /*try
            {
                int intListar = blnlistar == true ? 1 : 0;
                int intDescontinuado = blnDescontidunado == true ? 1 : 0;
                strSQL = "call usp_ModificarCabezeraArticulo ('" + strIdArticulo + "','" + strNombre + "'," + intIdUnd + "," + intTipoArticulo + "," + intListar
                                                            + "," + dblCantInventario + "," + dblCantMinInventario + "," + dblValorUnd +","+intDescontinuado+ ");";
                return Grabar(strSQL);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }*/

            try
            {
                objCnx = new clsConexionMySQLDB();
                strSQL = "usp_ModificarCabezeraArticulo";
                objCnx.SQL = strSQL;

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_id_art", System.Data.DbType.String, 18, strIdArticulo))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_Nombre", System.Data.DbType.String, 80, strNombre))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_idUnd", System.Data.DbType.UInt32, 1, intIdUnd))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_id_TipoArticulo", System.Data.DbType.UInt32, 1, intTipoArticulo))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_listar", System.Data.DbType.Boolean, 1, blnlistar))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_CantInventario", System.Data.DbType.Double, 12, dblCantInventario))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }
                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_CantMinInventario", System.Data.DbType.Double, 12, dblCantMinInventario))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }
                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_ValorUnd", System.Data.DbType.Double, 12, dblValorUnd))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_Descontinuado", System.Data.DbType.Boolean, 1, blnDescontidunado))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }
                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_CodSC", System.Data.DbType.String, 16,strCodSC))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.ConsultarValorUnico(true))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                strIdArticulo = objCnx.ValorUnico;
                objCnx.CerrarConexion();
                objCnx = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }


        }
        public bool VerificarInventario(Int64 intCodEtiqueta)
        {
            try
            {
                if (intCodEtiqueta<0)
                {
                    strError = "El codigo de etiqueta no es correcoto";
                    return false;
                }

                strSQL = "call usp_ConsultaInventarioXArticulo(" + intCodEtiqueta + ");";
                objCnx = new clsConexionMySQLDB();
                objCnx.SQL = strSQL;
                if (!objCnx.Consultar(false))
                {
                    strError = objCnx.Error;
                    return false;
                }
                Reader_local = objCnx.Reader;
                if (!Reader_local.HasRows)
                {
                    strError = "No se encontro ningún registro: " + intCodEtiqueta;
                    Reader_local.Close();
                    objCnx = null;
                    return false;
                }

                Reader_local.Read();
                strIdArticulo = Reader_local.GetString(0);
                strNombre = Reader_local.GetString(1);
                dblCantInventario = Reader_local.GetDouble(2);
                dblCantMinInventario = Reader_local.GetDouble(3);
                Reader_local.Close();
                objCnx = null;
                return true;  

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        #endregion
        #region"Metodos detalle producto terminado"
        public bool BuscarDetallePT(string strCodArticulo)
        {
            try
            {
                if (string.IsNullOrEmpty(strCodArticulo))
                {
                    strError = "No definio un codigo de articulo o no es valido";
                    return false;
                }

                strSQL = "call usp_buscarDetallePT ('" + strCodArticulo + "');";
                objCnx = new clsConexionMySQLDB();
                objCnx.SQL = strSQL;
                if (!objCnx.Consultar(false))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    return false;
                }
                Reader_local = objCnx.Reader;

                Reader_local.Read();
                dblPesoMillar = Reader_local.GetDouble(0);
                dblVlrProducion = Reader_local.GetDouble(1);
                dblVlrVenta = Reader_local.GetDouble(2);
                strFormaEmpaque = Reader_local.GetString(3);
                strMatFabricacion = Reader_local.GetString(4);
                intUndEmpaque = Reader_local.GetInt32(5);
                intIdLineaProd = Reader_local.GetInt32(6);
                strIdMatEmpaque = Reader_local.GetString(7);
                Reader_local.Close();
                objCnx.CerrarConexion();
                objCnx = null;
                return true;
            }


            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        public bool GrabarDetallePT()
        {
            try
            {
                objCnx = new clsConexionMySQLDB();
                strSQL = "usp_GrabarDetallePT";
                objCnx.SQL = strSQL;

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_idARt", System.Data.DbType.String, 18, strIdArticulo))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_dblPesoMillar", System.Data.DbType.Double, 12, dblPesoMillar))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_dblValorProd", System.Data.DbType.Double, 12, dblVlrProducion))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_dblValorVenta", System.Data.DbType.Double, 12, dblVlrVenta))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_strFormaEmp", System.Data.DbType.String, 120, strFormaEmpaque))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }
                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_undEmpaque", System.Data.DbType.Int32, 11, intUndEmpaque))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_idMat", System.Data.DbType.String, 18, strMatFabricacion))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_idLineaProd", System.Data.DbType.Int32, 11, intIdLineaProd))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }
                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_idMatEmpaque", System.Data.DbType.String, 18, strIdMatEmpaque))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.ConsultarValorUnico(true))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                strIdArticulo = objCnx.ValorUnico;
                objCnx.CerrarConexion();
                objCnx = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        public bool ModificarDetallePT()
        {
            try
            {
                objCnx = new clsConexionMySQLDB();
                strSQL = "usp_ModificarDetallePT";
                objCnx.SQL = strSQL;

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_idARt", System.Data.DbType.String, 18, strIdArticulo))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_dblPesoMillar", System.Data.DbType.Double, 12, dblPesoMillar))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_dblValorProd", System.Data.DbType.Double, 12, dblVlrProducion))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_dblValorVenta", System.Data.DbType.Double, 12, dblVlrVenta))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_strFormaEmp", System.Data.DbType.String, 120, strFormaEmpaque))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }
                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_undEmpaque", System.Data.DbType.Int32, 11, intUndEmpaque))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_idMat", System.Data.DbType.String, 18, strMatFabricacion))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_idLineaProd", System.Data.DbType.Int32, 11, intIdLineaProd))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }
                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_idMatEmpaque", System.Data.DbType.String, 18, strIdMatEmpaque))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.ConsultarValorUnico(true))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                strIdArticulo = objCnx.ValorUnico;
                objCnx.CerrarConexion();
                objCnx = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        public bool GenerarCodigoPT()
        {
            try
            {
                objCnx = new clsConexionMySQLDB();
                strSQL = "usp_AsignacionCodigoPT";
                objCnx.SQL = strSQL;

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_idLineaProd", System.Data.DbType.Int32, 18, intIdLineaProd))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.ConsultarValorUnico(true))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                strIdArticulo = objCnx.ValorUnico;
                objCnx.CerrarConexion();
                objCnx = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        #endregion
        #region"Metotos detalle materia prima"
        public bool BuscarDetalleMP(string strCodArticulo)
        {
            try
            {
                if (string.IsNullOrEmpty(strCodArticulo))
                {
                    strError = "No definio un codigo de articulo o no es valido";
                    return false;
                }

                strSQL = "call usp_buscarDetalleMP ('" + strCodArticulo + "');";
                objCnx = new clsConexionMySQLDB();
                objCnx.SQL = strSQL;
                if (!objCnx.Consultar(false))
                {
                    strError = objCnx.Error;
                    return false;
                }
                Reader_local = objCnx.Reader;
                if (!Reader_local.HasRows)
                {
                    strError = "No se encontro ningún registro: " + strCodArticulo;
                    Reader_local.Close();
                    objCnx = null;
                    return false;
                }

                Reader_local.Read();
                intidPolimero = Reader_local.GetInt32(0);
                intCalibre = Reader_local.GetInt32(1);
                dblancho = Reader_local.GetDouble(2);
                dbllargo=  Reader_local.GetDouble(3);
                intidFormaMat = Reader_local.GetInt32(4);
                intidColor = Reader_local.GetInt32(5);
                Reader_local.Close();
                objCnx.CerrarConexion();
                objCnx = null;
                return true;
            }


            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        public bool GrabarDetalleMP()
        {
            try
            {
                objCnx = new clsConexionMySQLDB();
                strSQL = "usp_GrabarDetalleMP";
                objCnx.SQL = strSQL;

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_idArt", System.Data.DbType.String, 18, strIdArticulo))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_idPolimiero", System.Data.DbType.Int32, 12, intidPolimero))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_calibre", System.Data.DbType.Int32, 12, intCalibre))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_largo", System.Data.DbType.Double, 12, dbllargo))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_ancho", System.Data.DbType.Double, 12, dblancho))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_formaMat", System.Data.DbType.Int32, 12, intidFormaMat))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }
                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_idColor", System.Data.DbType.Int32, 12, intidColor))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.ConsultarValorUnico(true))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                strIdArticulo = objCnx.ValorUnico;
                objCnx.CerrarConexion();
                objCnx = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        public bool ModificarDetalleMP()
        {
            try
            {
                objCnx = new clsConexionMySQLDB();
                strSQL = "usp_ModificarDetalleMP";
                objCnx.SQL = strSQL;

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_idArt", System.Data.DbType.String, 18, strIdArticulo))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_idPolimiero", System.Data.DbType.Int32, 12, intidPolimero))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_calibre", System.Data.DbType.Int32, 12, intCalibre))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_largo", System.Data.DbType.Double, 12, dbllargo))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_ancho", System.Data.DbType.Double, 12, dblancho))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_formaMat", System.Data.DbType.Int32, 12, intidFormaMat))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }
                if (!objCnx.AgregarParametro(System.Data.ParameterDirection.Input, "pr_idColor", System.Data.DbType.Int32, 12, intidColor))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }
                if (!objCnx.ConsultarValorUnico(true))
                {
                    strError = objCnx.Error;
                    objCnx.CerrarConexion();
                    objCnx = null;
                    return false;
                }

                strIdArticulo = objCnx.ValorUnico;
                objCnx.CerrarConexion();
                objCnx = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        #endregion

        #endregion

    }



}
