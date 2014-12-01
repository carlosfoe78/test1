using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//referenciar y usar
using System.Xml;
using System.Windows.Forms;


namespace LibParametros
{
    public class clsParametros
    {
        #region"Constructor"
        public clsParametros()
        { 
        strServidor=String.Empty;
        strBaseDatos = String.Empty;
        strUsuario = String.Empty;
        strClave = String.Empty;
        strStringConexion = String.Empty;
        strSeguridadIntegrada = String.Empty;
        strError = String.Empty;
        strArchivoXml = String.Empty;
        }
                
        #endregion
        #region"Atributos"
                
        private string strServidor;
        private string strBaseDatos;
        private string strUsuario;
        private string strClave;
        private string strStringConexion;
        private string strSeguridadIntegrada;
        private string strError;
        private string strArchivoXml;
        private XmlDocument objDocumento = new XmlDocument();
        private XmlNode objNode;        
        
        #endregion
        #region"Propiedades"
        public string StringConexion
        {
            get { return strStringConexion; }
        }

        public string Error
        {
            get { return strError; }
        }
        
        #endregion
        #region"Metodos Privados"
        #endregion
        #region"Metodos Publicos"
        public bool GerenarString()
        {
            string strNombreAplicacion = AppDomain.CurrentDomain.BaseDirectory;
            strNombreAplicacion = strNombreAplicacion.Substring(1, strNombreAplicacion.Length - 2);
            strNombreAplicacion=strNombreAplicacion.Substring(strNombreAplicacion.LastIndexOf('\\') + 1, strNombreAplicacion.Length - (strNombreAplicacion.LastIndexOf('\\') + 1));
            strArchivoXml = Application.StartupPath + "\\CON_" + strNombreAplicacion + ".xml";

            try
            {
                objDocumento.Load(strArchivoXml);
                objNode = objDocumento.SelectSingleNode("//Servidor");
                strServidor = objNode.InnerText;
                objNode = objDocumento.SelectSingleNode("//BaseDatos");
                strBaseDatos = objNode.InnerText;
                objNode = objDocumento.SelectSingleNode("//Usuario");
                strUsuario= objNode.InnerText;
                objNode = objDocumento.SelectSingleNode("//Clave");
                strClave = objNode.InnerText;
                objNode = objDocumento.SelectSingleNode("//SeguridadIntegrada");
                strSeguridadIntegrada= objNode.InnerText;

                if (strSeguridadIntegrada.ToLower() == "si")
                    strStringConexion = "Data Source=" + strServidor + ";InitialCatalog=" + strBaseDatos + ";Integrated Security=SSPI;";
                else
                    strStringConexion = "Data Source" + strServidor + "; InitialCatalog=" + strBaseDatos + ";User id=" + strUsuario + ";Password=" + strClave + ";";
                objDocumento = null;
                return true;
            }

            catch (Exception ex)
            {
                strError = ex.Message;
                objDocumento = null;
                return false;
            }
        
        
        }

        public bool GerenarStringMySQL()
        {
            string strNombreAplicacion = AppDomain.CurrentDomain.BaseDirectory;
            strNombreAplicacion = strNombreAplicacion.Substring(1, strNombreAplicacion.Length - 2);
            strNombreAplicacion = strNombreAplicacion.Substring(strNombreAplicacion.LastIndexOf('\\') + 1, strNombreAplicacion.Length - (strNombreAplicacion.LastIndexOf('\\') + 1));
            strArchivoXml = Application.StartupPath + "\\CON_" + strNombreAplicacion + "MySQL.xml";
            string strPuerto;
            try
            {
                objDocumento.Load(strArchivoXml);
                objNode = objDocumento.SelectSingleNode("//Servidor");
                strServidor = objNode.InnerText;
                objNode = objDocumento.SelectSingleNode("//BaseDatos");
                strBaseDatos = objNode.InnerText;
                objNode = objDocumento.SelectSingleNode("//Usuario");
                strUsuario = objNode.InnerText;
                objNode = objDocumento.SelectSingleNode("//Clave");
                strClave = objNode.InnerText;
                objNode = objDocumento.SelectSingleNode("//Puerto");
                strPuerto= objNode.InnerText;

                //if (strSeguridadIntegrada.ToLower() == "si")
                  //  strStringConexion = "Server=" + strServidor + ";Database=" + strBaseDatos + ";Integrated Security=SSPI;";
                //else
                strStringConexion = "server=" + strServidor + ";user=" + strUsuario + "; database=" + strBaseDatos + ";Port=" + strPuerto + ";Pwd=" + strClave+";";
                objDocumento = null;
                return true;
            }

            catch (Exception ex)
            {
                strError = ex.Message;
                objDocumento = null;
                return false;
            }


        }
        public bool GerenarStringMySQL(int intCnLIfeTime)
        {
            string strNombreAplicacion = AppDomain.CurrentDomain.BaseDirectory;
            strNombreAplicacion = strNombreAplicacion.Substring(1, strNombreAplicacion.Length - 2);
            strNombreAplicacion = strNombreAplicacion.Substring(strNombreAplicacion.LastIndexOf('\\') + 1, strNombreAplicacion.Length - (strNombreAplicacion.LastIndexOf('\\') + 1));
            strArchivoXml = Application.StartupPath + "\\CON_" + strNombreAplicacion + "MySQL.xml";
            string strPuerto;
            try
            {
                objDocumento.Load(strArchivoXml);
                objNode = objDocumento.SelectSingleNode("//Servidor");
                strServidor = objNode.InnerText;
                objNode = objDocumento.SelectSingleNode("//BaseDatos");
                strBaseDatos = objNode.InnerText;
                objNode = objDocumento.SelectSingleNode("//Usuario");
                strUsuario = objNode.InnerText;
                objNode = objDocumento.SelectSingleNode("//Clave");
                strClave = objNode.InnerText;
                objNode = objDocumento.SelectSingleNode("//Puerto");
                strPuerto = objNode.InnerText;

                //if (strSeguridadIntegrada.ToLower() == "si")
                //  strStringConexion = "Server=" + strServidor + ";Database=" + strBaseDatos + ";Integrated Security=SSPI;";
                //else
                strStringConexion = "server=" + strServidor + ";user=" + strUsuario + "; database=" + strBaseDatos + ";Port=" + strPuerto + ";Pwd=" + strClave + ";ConnectionLifeTime=" + intCnLIfeTime + 
                                    ";ConnectionTimeout=" + intCnLIfeTime + ";maximumpoolsize =500"+";";
                objDocumento = null;
                return true;
            }

            catch (Exception ex)
            {
                strError = ex.Message;
                objDocumento = null;
                return false;
            }


        }
        public bool GerenarStringMySQL(string strUser, string strPass, int maxPool)
        {
            string strNombreAplicacion = AppDomain.CurrentDomain.BaseDirectory;
            strNombreAplicacion = strNombreAplicacion.Substring(1, strNombreAplicacion.Length - 2);
            strNombreAplicacion = strNombreAplicacion.Substring(strNombreAplicacion.LastIndexOf('\\') + 1, strNombreAplicacion.Length - (strNombreAplicacion.LastIndexOf('\\') + 1));
            strArchivoXml = Application.StartupPath + "\\CON_" + strNombreAplicacion + "MySQL.xml";
            string strPuerto;
            try
            {
                objDocumento.Load(strArchivoXml);
                objNode = objDocumento.SelectSingleNode("//Servidor");
                strServidor = objNode.InnerText;
                objNode = objDocumento.SelectSingleNode("//BaseDatos");
                strBaseDatos = objNode.InnerText;
                objNode = objDocumento.SelectSingleNode("//Usuario");
                strUsuario = objNode.InnerText;
                objNode = objDocumento.SelectSingleNode("//Clave");
                strClave = objNode.InnerText;
                objNode = objDocumento.SelectSingleNode("//Puerto");
                strPuerto = objNode.InnerText;

                strStringConexion = "server=" + strServidor + ";user=" + strUser + "; database=" + strBaseDatos + ";Port=" + strPuerto + ";Pwd=" + strPass + ";Maximum Pool Size=" + maxPool + ";";
                objDocumento = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objDocumento = null;
                return false;
            }
        }
        public bool GerenarStringMySQL2()
        {
            string strNombreAplicacion = AppDomain.CurrentDomain.BaseDirectory;
            strNombreAplicacion = strNombreAplicacion.Substring(1, strNombreAplicacion.Length - 2);
            strNombreAplicacion = strNombreAplicacion.Substring(strNombreAplicacion.LastIndexOf('\\') + 1, strNombreAplicacion.Length - (strNombreAplicacion.LastIndexOf('\\') + 1));
            strArchivoXml = Application.StartupPath + "\\CON2_" + strNombreAplicacion + "MySQL.xml";
            string strPuerto;
            try
            {
                objDocumento.Load(strArchivoXml);
                objNode = objDocumento.SelectSingleNode("//Servidor");
                strServidor = objNode.InnerText;
                objNode = objDocumento.SelectSingleNode("//BaseDatos");
                strBaseDatos = objNode.InnerText;
                objNode = objDocumento.SelectSingleNode("//Usuario");
                strUsuario = objNode.InnerText;
                objNode = objDocumento.SelectSingleNode("//Clave");
                strClave = objNode.InnerText;
                objNode = objDocumento.SelectSingleNode("//Puerto");
                strPuerto = objNode.InnerText;

                strStringConexion = "server=" + strServidor + ";user=" + strUsuario + "; database=" + strBaseDatos + ";Port=" + strPuerto + ";Pwd=" + strClave + ";";
                objDocumento = null;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                objDocumento = null;
                return false;
            }
        }       
        
        #endregion

    }
}
