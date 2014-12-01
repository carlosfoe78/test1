using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//referenciar y usar
using System.Data;
using System.Data.SqlClient;
using LibParametros;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace LibconecxionBD
{
    public class clsConexionBD
    {
        #region"Constructor"
        public clsConexionBD()
       
        {
            objConexionBD=new SqlConnection();
            objCommand=new SqlCommand();
            objAdapter=new SqlDataAdapter();
            objDataSet=new DataSet();
    
        }
        
        
        
        #endregion
        #region"Atributos"
        private string strStingConexion;
        private string strNombreTabla;
        private string strVrUnico;
        private string strSQL;
        private string strError;
        private bool blnAbrio;
        private SqlConnection objConexionBD;
        private SqlCommand objCommand;
        private SqlDataReader objReader;
        private SqlDataAdapter objAdapter;
        private DataSet objDataSet;


        #endregion
        #region"Propiedades"
        public string SQL
        { set { strSQL = value; } }
        public string NombreTabla
        { set { strNombreTabla = value; } }
        public string ValorUnico
        { get { return strVrUnico; } }
        public string Error
        { get { return strError; } }

        public DataSet MiDataSet
        { get { return objDataSet; } }
        public SqlDataReader Reader
        { get { return objReader; } }


        #endregion
        #region"Metodos Privados"
        private bool GenerarString()
        {
            clsParametros objParametros = new clsParametros();
            if (!objParametros.GerenarString())
            {
                strError = objParametros.Error;
                objParametros = null;
                return false;
            }
            strStingConexion=objParametros.StringConexion;
            objParametros=null;
            return true;
        }

        private bool AbrirConexion()
        {
            if (!GenerarString())
                return false;
            objConexionBD.ConnectionString = strStingConexion;
            try
            {
                objConexionBD.Open();
                blnAbrio = true;
                return true;
            }
            catch (Exception exeption)
            {
                strError = exeption.Message;
                blnAbrio = false;
                return false;
            }
        }
        
        #endregion
        #region"Metodos Publicos"
        public void CerrarConexion()
        {
            try
            {
                objConexionBD.Close();
                objConexionBD = null;
            }
            catch (Exception ex)
            { strError = "No se cerró o liberó la conexión" +ex.Message;}
        }

        public bool Consultar(bool blnParametros)
        {
            try
            {

                if (string.IsNullOrEmpty(strSQL))
                {
                    strError = "No defino la instrución SQL";
                    return false;
                }
                if (!blnAbrio)
                {
                    if (!AbrirConexion())
                        return false;
                }

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }

            objCommand.Connection = objConexionBD;
            objCommand.CommandText = strSQL;
            if (blnParametros)
                objCommand.CommandType = CommandType.StoredProcedure;
            else
                objCommand.CommandType = CommandType.Text;
            //Realizar la tansaccion en la BD
            objReader = objCommand.ExecuteReader();
            return true;
        
        }

        public bool  ConsultarValorUnico(bool blnParametros)
        {
            try
            {
                if (string.IsNullOrEmpty(strSQL))
                {
                    strError = "No definio la instrucción SQL";
                    return false;
                }
                if (!blnAbrio)
                {
                    if (!AbrirConexion())
                        return false;

                }

                objCommand.Connection = objConexionBD;
                objCommand.CommandText = strSQL;
                if (blnParametros)
                    objCommand.CommandType = CommandType.StoredProcedure;
                else
                    objCommand.CommandType = CommandType.Text;
                //Realizar la transaccion en la BD
                strVrUnico = objCommand.ExecuteScalar().ToString();
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        
        }

        public bool EjecutarSentencia(bool blnParametros)
        {
            try
            { 
                if(string.IsNullOrEmpty(strSQL))
                {
                    strError="No definio la instruccion SQL";
                    return false;
                }
                if(!blnAbrio)
                {
                    if(!AbrirConexion())
                        return false;

                }

                objCommand.Connection=objConexionBD;
                objCommand.CommandText=strSQL;
                if(blnParametros)
                    objCommand.CommandType=CommandType.StoredProcedure;
                else
                    objCommand.CommandType=CommandType.Text;
                //Realizar la transaccion en la BD
                objCommand.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        
        }

        public bool LlenarDataSet(bool blnParametros)
        {
            try
            {
                if (string.IsNullOrEmpty(strSQL))
                {
                    strError = "NO definio la instrucción SQL";
                    return false;
                }
                if (!blnAbrio)
                {
                    if (!AbrirConexion())
                        return false;
                }

                objCommand.Connection = objConexionBD;
                objCommand.CommandText = strSQL;
                if (blnParametros)
                    objCommand.CommandType = CommandType.StoredProcedure;
                else
                    objCommand.CommandType = CommandType.Text;
                //El dataadapter utiliza el command para la transacion
                objAdapter.SelectCommand = objCommand;
                //Realizzr la transaccion en la BD y el llenado del DATaSEt/Datatable
                objAdapter.Fill(objDataSet,strNombreTabla);
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        
        
        }

        
        #endregion
    }

    public class clsConexionMySQLDB
    {
        #region"CONSTRUCTOR"
        public clsConexionMySQLDB()
        {
            objConexionBD = new MySqlConnection();
            objCommand = new MySqlCommand();
            objAdapter = new MySqlDataAdapter();
            objDataSet = new DataSet();
            objParametro = new MySqlParameter();
        }
        #endregion
        #region"ATRIBUTOS"
        private string strStingConexion;
        private string strNombreTabla;
        private string strVrUnico;
        private string strSQL;
        private string strError;
        private bool blnAbrio;
        private MySqlConnection objConexionBD;
        private MySqlCommand objCommand;
        private MySqlDataReader objReader;
        private MySqlDataAdapter objAdapter;
        private DataSet objDataSet;
        private MySqlParameter objParametro;
        private MySqlTransaction MiTransaccion;
        
        
        #endregion
        #region"PROPIEDADES"
        public string SQL
        { set { strSQL = value; } }
        public string NombreTabla
        { set { strNombreTabla = value; } }
        public string ValorUnico
        { get { return strVrUnico; } }
        public string Error
        { get { return strError; } }

        public DataSet MiDataSet
        { get { return objDataSet; } }
        public MySqlDataReader Reader
        { get { return objReader; } }
        public MySqlParameter miObjParametro
        { get { return (objParametro); } }
        
        #endregion
        #region"METODOS PRIVADOS"
        private bool GenerarStringMySQL()
        {
            clsParametros objParametros = new clsParametros();
            if (!objParametros.GerenarStringMySQL())
            {
                strError = objParametros.Error;
                objParametros = null;
                return false;
            }
            strStingConexion=objParametros.StringConexion;
            objParametros=null;
            return true;
        }

        private bool AbrirConexion()
        {
            if (!GenerarStringMySQL())
                return false;
            objConexionBD.ConnectionString = strStingConexion;
            try
            {
                objConexionBD.Open();
                blnAbrio = true;
                return true;
            }
            catch (Exception exeption)
            {
                strError = exeption.Message;
                blnAbrio = false;
                return false;
            }
        }
                
        #endregion
        #region"METODOS PUBLICOS"
        public bool AgregarParametro(ParameterDirection Direccion, string Nombre, DbType TipoDato, Int16 Tamano, object Valor)
        {
            try
            {
                objParametro.Direction = Direccion;
                objParametro.ParameterName = Nombre;
                objParametro.DbType = TipoDato;
                objParametro.Size = Tamano;
                objParametro.Value = Valor;

                objCommand.Parameters.Add(objParametro);
                objParametro = new MySqlParameter();
                return (true);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return (false);
            }
        }
        public bool AgregarParametro(ParameterDirection Direccion, string Nombre, DbType TipoDato, Int16 Tamano, object Valor, bool Nulo)
        {
            try
            {
                objParametro.Direction = Direccion;
                objParametro.ParameterName = Nombre;
                objParametro.DbType = TipoDato;
                objParametro.Size = Tamano;
                objParametro.Value = Valor;
                objParametro.IsNullable = Nulo;

                objCommand.Parameters.Add(objParametro);
                objParametro = new MySqlParameter();
                return (true);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return (false);
            }
        }
        public void CerrarConexion()
        {
            try
            {
                objConexionBD.Close();
                objConexionBD = null;
            }
            catch (Exception ex)
            { strError = "No se cerró o liberó la conexión" + ex.Message; }
        }

        // llena un objeta dataReader con el resultado de una comsulta
        public bool Consultar(bool blnParametros)
        {
            try
            {

                if (string.IsNullOrEmpty(strSQL))
                {
                    strError = "No defino la instrución SQL";
                    return false;
                }
                if (!blnAbrio)
                {
                    if (!AbrirConexion())
                        return false;
                }

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }

            objCommand.Connection = objConexionBD;
            objCommand.CommandText = strSQL;
            //Define el tipo de comando a ejecutar (Stored Prosedureo o instrucccion sql)
            if (blnParametros)
                objCommand.CommandType = CommandType.StoredProcedure;
            else
                objCommand.CommandType = CommandType.Text;
            //Realizar la tansaccion en la BD
            objReader = objCommand.ExecuteReader();
            return true;

        }

        public bool AbrirTransaccion()
        {
            try
            {

                if (string.IsNullOrEmpty(strSQL))
                {
                    strError = "No defino la instrución SQL";
                    return false;
                }
                if (!blnAbrio)
                {
                    if (!AbrirConexion())
                        return false;
                }

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
            try
            {
                MiTransaccion = objConexionBD.BeginTransaction();
                objCommand.Transaction = MiTransaccion;
                return (true);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        public bool AceptarTransaccion()
        {
            try
            {
                MiTransaccion.Commit();
                return (true);
            }
            catch (Exception ex)
            {
                try
                {
                    MiTransaccion.Rollback();
                    strError = "No se aceptó la transacción" + ex.Message;
                    return (false);
                }
                catch (Exception ex1)
                {
                    strError = "No se aceptó la transacción ni la retrocedio: " + ex1.Message;
                    return (false);
                }
            }
        }
        
        public bool RechazarTransaccion()
        {
            try
            {
                MiTransaccion.Rollback();
                return (true);
            }
            catch (Exception ex)
            {
                strError = "No se retrocedió la transacción: " + ex.Message;
                return (false);
            }
        }

        //entrega el valor de la primera fila y priemra columna de una sentencia sql
        public bool ConsultarValorUnico(bool blnParametros)
        {
            try
            {
                if (string.IsNullOrEmpty(strSQL))
                {
                    strError = "No definio la instrucción SQL";
                    return false;
                }
                if (!blnAbrio)
                {
                    if (!AbrirConexion())
                        return false;

                }

                objCommand.Connection = objConexionBD;
                objCommand.CommandText = strSQL;
                if (blnParametros)
                    objCommand.CommandType = CommandType.StoredProcedure;
                else
                    objCommand.CommandType = CommandType.Text;
                //Realizar la transaccion en la BD
                strVrUnico = objCommand.ExecuteScalar().ToString();
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }

        }

        // ejecuta una sentenca sql o SP sin entregar algun resultado
        public bool EjecutarSentencia(bool blnParametros)
        {
            try
            {
                if (string.IsNullOrEmpty(strSQL))
                {
                    strError = "No definio la instruccion SQL";
                    return false;
                }
                if (!blnAbrio)
                {
                    if (!AbrirConexion())
                        return false;

                }

                objCommand.Connection = objConexionBD;
                objCommand.CommandText = strSQL;
                if (blnParametros)
                    objCommand.CommandType = CommandType.StoredProcedure;
                else
                    objCommand.CommandType = CommandType.Text;
                //Realizar la transaccion en la BD
                objCommand.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }

        }

        // llena un obeto dataSet
        public bool LlenarDataSet(bool blnParametros)
        {
            try
            {
                if (string.IsNullOrEmpty(strSQL))
                {
                    strError = "NO definio la instrucción SQL";
                    return false;
                }
                if (!blnAbrio)
                {
                    if (!AbrirConexion())
                        return false;
                }

                objCommand.Connection = objConexionBD;
                objCommand.CommandText = strSQL;
                if (blnParametros)
                    objCommand.CommandType = CommandType.StoredProcedure;
                else
                    objCommand.CommandType = CommandType.Text;
                //El dataadapter utiliza el command para la transacion
                objAdapter.SelectCommand = objCommand;
                //Realizzr la transaccion en la BD y el llenado del DATaSEt/Datatable
                objAdapter.Fill(objDataSet, strNombreTabla);
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        

        public bool CerrarConexionesDurmientes(bool blnParametros, string strUsuario)
        {
            try
            {
                if (!blnAbrio)
                {
                    if (!AbrirConexion())
                        return false;
                }

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }

            objCommand.Connection = objConexionBD;
            strSQL = "SELECT `ID` FROM `information_schema`.`PROCESSLIST` where USER='" + strUsuario + "' and COMMAND='Sleep'";
            objCommand.CommandText = strSQL;
            //Define el tipo de comando a ejecutar (Stored Prosedureo o instrucccion sql)
            if (blnParametros)
                objCommand.CommandType = CommandType.StoredProcedure;
            else
                objCommand.CommandType = CommandType.Text;
            //Realizar la tansaccion en la BD
            objReader = objCommand.ExecuteReader();
            objReader.Read();
            foreach (var id in objReader)
            {
                objCommand.CommandText = "kill " + objReader.GetInt32(0);
                objCommand.CommandType = CommandType.Text;
                objCommand.ExecuteNonQuery();
            }
            return true;

        }
        
        
        #endregion
    }
}
