using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
namespace WebApplication3
{
    public partial class index : System.Web.UI.Page
    {
        SqlConnection objCnx;
        SqlCommand objCommand;
        SqlDataAdapter objAdapter;
        SqlDataReader objReader;
        DataSet objDataSet;

        protected void Page_Load(object sender, EventArgs e)
        {   
            llenargrid();
        }

        protected void llenargrid()
        {
            openConection();
            llenarDataSet();
            grvDatos.DataSource = objDataSet.Tables["tblUsuario"];
            grvDatos.DataBind();
            objCnx.Close();
        }

        protected void openConection()
        {
            try 
            {
                string strCnx;
                strCnx = "Data Source= localhost; Database = test1; Integrated Security=SSPI;";
                // strCnx = "Data Source= localhost; InitialCatalog= test1; User id=admin; Password=admin;";
                objCnx = new SqlConnection(strCnx);
                objCnx.Open();
            }
            catch (Exception e)
            {
                litError.Text = e.Message;
            }
        }

        protected void llenarDataSet() 
        {
            try
            {
                objCommand = new SqlCommand();
                objAdapter = new SqlDataAdapter();
                objDataSet = new DataSet();
                objCommand.Connection = objCnx;
                objCommand.CommandText = "select * from  \"user\"";
                objCommand.CommandType = CommandType.Text;
                objAdapter.SelectCommand = objCommand;
                objAdapter.Fill(objDataSet, "tblUsuario");
                objCommand = null;
                objAdapter = null;
            }
            catch (Exception e) 
            {
                litError.Text = e.Message;
            }
        }
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string strnombre, strApellido, strEdad, strTelefono, strDireccion, strSQL;

            strnombre = this.txtNombre.Text;
            strApellido = this.txtApellido.Text;
            strEdad = this.txtEdad.Text;
            strTelefono = this.txtTelefono.Text;
            strDireccion = this.txtDireccion.Text;

            strSQL = "insert into \"user\" values('" + strnombre + "','" + strApellido + "'," + strEdad + ",'" + strTelefono + "','" + strDireccion + "')";
            ejecutar(strSQL);
        }


        //consultar
        protected bool BuscarId()
        {
            try
            {
                int intId;
                string strSQl;

                if (string.IsNullOrEmpty(this.txtId.Text))
                {
                    litError.Text = "no ha escrito un id";
                    return false;
                }
                intId = Convert.ToInt32(this.txtId.Text);

                openConection();
                
                strSQl = "select * from \"user\" where id=" + intId;
                objCommand = objCnx.CreateCommand();
                objCommand.CommandText = strSQl;
                objCommand.CommandType = CommandType.Text;
                objReader = objCommand.ExecuteReader();

                if (!objReader.HasRows)
                {
                    litError.Text = "No se encontraron datos para el id " + intId;
                    objReader.Close();
                    return false;
                }

                objReader.Read();
                this.txtNombre.Text = objReader.GetString(1);
                this.txtApellido.Text = objReader.GetString(2);
                this.txtEdad.Text = objReader.GetInt32(3).ToString();
                this.txtTelefono.Text = objReader.GetString(4);
                this.txtDireccion.Text = objReader.GetString(5);
                objReader.Close();
                objCnx.Close();

                return true;

            }
            catch (Exception e)
            {
                litError.Text = e.Message;
                return false;
            }
        }

        //ejecutar sentencia
        protected bool ejecutar(string strSQL)
        {
            try
            {
                if (string.IsNullOrEmpty(strSQL))
                {
                    litError.Text = ("No se definio una consulta");
                    return false;
                }
                if (objCnx.State != ConnectionState.Open)
                {
                    openConection();
                }

                objCommand = new SqlCommand();
                objCommand.Connection = objCnx;
                objCommand.CommandText = strSQL;
                objCommand.CommandType = CommandType.Text;
                if(objCommand.ExecuteNonQuery()==0)
                {
                    litError.Text = "La sentencia SQL no se ejecuto correctamente";
                    return false;
                }
                objCommand = null;
                objCnx.Close();
                return true;
            }
            catch (Exception e)
            {
                litError.Text = e.Message;
                return false;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarId();
        }
    }
}