using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicacionGestionProyFinal
{
    public partial class FrmNuevaReparacion : Form
    {
        public FrmNuevaReparacion()
        {
            InitializeComponent();
        }
        //Conexion
        //SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-2ECAU7F;Initial Catalog=BDfinal;Integrated Security=True");
        SqlConnection conexion = new SqlConnection("Data Source=segundo150\\segundo150;Initial Catalog=DAM_AnderSabariego;Integrated Security=True");

        //Comandos
        SqlCommand cmdCargarCmb = new SqlCommand();
        SqlCommand cmdAlta = new SqlCommand();
        SqlCommand cmdMax = new SqlCommand();
        SqlCommand cmdCargarReparaciones = new SqlCommand();

        //Parametros
        SqlParameter prmId = new SqlParameter();
        SqlParameter prmTipo = new SqlParameter();
        SqlParameter prmFecha = new SqlParameter();
        SqlParameter prmDNI = new SqlParameter();
        SqlParameter prmReparado = new SqlParameter();

        //DataTable
        DataTable dtCmb = new DataTable();

        //Adaptadores
        SqlDataAdapter adaptador = new SqlDataAdapter();

        //DataReader
        SqlDataReader dreader;

        //Variables
        int PosX, PosY;
        Boolean Arrastra;
        String Reparado;
        private void FrmNuevaReparacion_Load(object sender, EventArgs e)
        {

            //Defino el comando cmdCargar
            cmdCargarCmb.CommandType = CommandType.Text;
            cmdCargarCmb.CommandText = "Select id,TipoEquipo from TipoEquipo";
            cmdCargarCmb.Connection = conexion;

            //Defino el comando cmdCargar
            cmdMax.CommandType = CommandType.Text;
            cmdMax.CommandText = "select max(IDReparacion) from Reparaciones";
            cmdMax.Connection = conexion;

            //Defino el comando cmdCargar
            cmdAlta.CommandType = CommandType.Text;
            cmdAlta.CommandText = "insert into Reparaciones (IDReparacion,TipoDeEquipo,Fecha,DNIpropietario,Reparado) Values (@prmId,@prmTipo,@prmFecha,@prmDNI,@prmReparado)";
            cmdAlta.Connection = conexion;

            //Defino el comando cmdCargar
            cmdCargarReparaciones.CommandType = CommandType.Text;
            cmdCargarReparaciones.CommandText = "Select IDreparacion,DNIpropietario,Fecha from Reparaciones";
            cmdCargarReparaciones.Connection = conexion;

            //Defino el parametro para el ID
            prmId.ParameterName = "@prmId";
            prmId.DbType = DbType.Int32;
            prmId.Direction = ParameterDirection.Input;

            //Defino el parametro para el tipo
            prmTipo.ParameterName = "@prmTipo";
            prmTipo.DbType = DbType.String;
            prmTipo.Size = 50;
            prmTipo.Direction = ParameterDirection.Input;

            //Defino el parametro para la fecha
            prmFecha.ParameterName = "@prmFecha";
            prmFecha.DbType = DbType.Date;
            prmFecha.Direction = ParameterDirection.Input;

            //Defino el parametro para el DNI
            prmDNI.ParameterName = "@prmDNI";
            prmDNI.DbType = DbType.String;
            prmDNI.Size = 9;
            prmDNI.Direction = ParameterDirection.Input;

            //Defino el parametro para el estado de reparacion
            prmReparado.ParameterName = "@prmReparado";
            prmReparado.DbType = DbType.Boolean;
            prmReparado.Direction = ParameterDirection.Input;

            //Añado los parametros al comando
            cmdAlta.Parameters.Add(prmId);
            cmdAlta.Parameters.Add(prmTipo);
            cmdAlta.Parameters.Add(prmFecha);
            cmdAlta.Parameters.Add(prmDNI);
            cmdAlta.Parameters.Add(prmReparado);

            //Le asigno el comando al adaptador
            adaptador.SelectCommand = cmdCargarCmb;

            //Cargo la tabla mediante el metodo fill
            adaptador.Fill(dtCmb);

            //Monto el combo
            cmbTipo.DataSource = dtCmb;
            cmbTipo.DisplayMember = "TipoEquipo";
            cmbTipo.ValueMember = "TipoEquipo";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Cierro este formulario
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Si los siguientes campos estan vacios
            if (txtDNI.Text=="" || cmbTipo.SelectedValue.ToString()=="")
            {
                //Le asigno el siguiente valor al lblRespuesta
                lblRespuesta.Text = "¡No puedes dejar ningun campo vacio!";

                //Le doy el color rojo a la letra del lblRespuesta
                lblRespuesta.ForeColor = Color.Red;
            }
            else
            {
                //Abro la conexion
                conexion.Open();

                //Si el cmdMax me devuelve un nulo
                if (cmdMax.ExecuteScalar() == null)
                {
                    //Le asigno al parametro el valor uno
                    cmdAlta.Parameters[0].Value = 1;
                }
                else
                {
                    //Le asigno al 
                    cmdAlta.Parameters[0].Value = int.Parse(cmdMax.ExecuteScalar().ToString())+1; 
                }

                //Asigno los valores correspondientes a los parametros del comando cmdAlta
                cmdAlta.Parameters[1].Value = cmbTipo.SelectedValue.ToString();
                cmdAlta.Parameters[2].Value = DateTime.Now;
                cmdAlta.Parameters[3].Value = txtDNI.Text;
                cmdAlta.Parameters[4].Value = false;

                //Ejecuto el comando
                cmdAlta.ExecuteNonQuery();

                //Le asigno el siguiente valor al lblRespuesta
                lblRespuesta.Text = "¡Reparacion añadida con exito!";

                //Le doy el color rojo a la letra del lblRespuesta
                lblRespuesta.ForeColor = Color.Green;


                //Creo un objeto de tipo FrmReparacion y le digo que es el propietario de este formulario
                FrmReparacion reparacion = Owner as FrmReparacion;

                //Limpio los items de lsReparaciones
                reparacion.lsReparaciones.Items.Clear();

                //Le digo al lector que los datos que tiene que leer son los que le llegan de la ejecucion del siguiente comando
                dreader = cmdCargarReparaciones.ExecuteReader();

                //Mientras el reader lea
                while (dreader.Read())
                {
                    //Creo un objeto de tipo ListViewItem
                    ListViewItem li =  new ListViewItem();

                    //Añado los items a la Listview
                    li = reparacion.lsReparaciones.Items.Add(dreader[1].ToString());
                    li.SubItems.Add(dreader[2].ToString());
                }
                //Cierro el lector
                dreader.Close();

                //Cierro la conexion
                conexion.Close();

                //Cierro el formulario
                this.Close();
            }
        }

        private void pTitulo_MouseUp(object sender, MouseEventArgs e)
        {
            //Le asigno a la variable Arrasta el valor False
            Arrastra = false;
        }

        private void pTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando muevo por el titulo del Formulario

            //Si el valor de la variable Arrastra es igual a True
            if (Arrastra == true)
            {
                //Le resto a las posiciones actuales del formulario, los valores de las variables PosX y PosY
                this.SetDesktopLocation(MousePosition.X - PosX, MousePosition.Y - PosY);
            }
        }

        private void FrmNuevaReparacion_MouseDown(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando hago click en el cuerpo del formulario

            //Asigno el valor de la variable PosX con el valor del MouseEventArg
            PosX = e.X;

            //Asigno el valor de la variable PosY con el valor del MouseEventArg
            PosY = e.Y;

            //Le asigno a la variable Arrasta el valor True
            Arrastra = true;
        }

        private void FrmNuevaReparacion_MouseUp(object sender, MouseEventArgs e)
        {
            //Le asigno a la variable Arrasta el valor False
            Arrastra = false;
        }

        private void FrmNuevaReparacion_MouseMove(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando muevo por el Cuerpo del Formulario

            //Si el valor de la variable Arrastra es igual a True
            if (Arrastra == true)
            {
                //Le resto a las posiciones actuales del formulario, los valores de las variables PosX y PosY
                this.SetDesktopLocation(MousePosition.X - PosX, MousePosition.Y - PosY);
            }
        }

        private void pTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando hago click en el titulo del formulario

            //Asigno el valor de la variable PosX con el valor del MouseEventArg
            PosX = e.X;

            //Asigno el valor de la variable PosY con el valor del MouseEventArg
            PosY = e.Y;

            //Le asigno a la variable Arrasta el valor True
            Arrastra = true;
        }
    }
}
