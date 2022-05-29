using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AplicacionGestionProyFinal
{
    public partial class FrmGestion : Form
    {
        public FrmGestion()
        {
            InitializeComponent();
        }
        //Conexion
        //SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-2ECAU7F;Initial Catalog=BDfinal;Integrated Security=True");
        SqlConnection conexion = new SqlConnection("Data Source=segundo150\\segundo150;Initial Catalog=DAM_AnderSabariego;Integrated Security=True");


        //Comandos
        SqlCommand cmdCargarReparaciones = new SqlCommand();
        SqlCommand cmdMostrar = new SqlCommand();

        //DataReader
        SqlDataReader dreader;
        SqlDataReader dreaderseleccionar;

        //Parametros
        SqlParameter prmDNI = new SqlParameter();

        //Variables
        int PosX, PosY;
        Boolean Arrastra;
        private void FrmGestion_Load(object sender, EventArgs e)
        {
            //Pongo los controles en readonly
            txtDNI.ReadOnly = true;
            txtFecha.ReadOnly = true;
            txtTipo.ReadOnly = true;

            //Defino el comando cmdCargar
            cmdCargarReparaciones.CommandType = CommandType.Text;
            cmdCargarReparaciones.CommandText = "Select DNIpropietario,Fecha from Reparaciones where Reparado='False'";
            cmdCargarReparaciones.Connection = conexion;

            //Cierro la conexion
            conexion.Open();

            //Limpio los items del lsReparaciones
            lsReparaciones.Items.Clear();

            //Le digo al reader que los datos que tiene que leer son los que le llegan de la ejecucion del comando
            dreader = cmdCargarReparaciones.ExecuteReader();

            //Mientras el lector lea
            while (dreader.Read())
            {
                //Creo un objeto de tipo ListViewItem
                ListViewItem li = new ListViewItem();

                //Añado los objetos a la ListView
                li = lsReparaciones.Items.Add(dreader[0].ToString());
                li.SubItems.Add(dreader[1].ToString());
            }
            //Cierro el reader
            dreader.Close();

            //Cierro la conexion
            conexion.Close();

            //Defino el comando 
            cmdMostrar.CommandType = CommandType.Text;
            cmdMostrar.CommandText = "Select DNIpropietario,TipoDeEquipo,Fecha from Reparaciones where DNIpropietario=@prmDNI";
            cmdMostrar.Connection = conexion;

            //Defino el parametro 
            prmDNI.ParameterName = "@prmDNI";
            prmDNI.DbType = DbType.String;
            prmDNI.Size = 9;
            prmDNI.Direction = ParameterDirection.Input;

            //Le añado el parametro al comando
            cmdMostrar.Parameters.Add(prmDNI);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            //Cierro la aplicacion
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            //Minimizo la aplicacion
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            //Creo un objeto de tipo FrmLogin
            FrmLogin login = new FrmLogin();

            //Muestro el formulario
            login.Show();

            //Cierro este formulario
            this.Close();
        }

        private void btnReparacion_Click(object sender, EventArgs e)
        {
            //Creo un objeto de tipo FrmReparacion
            FrmReparacion reparacion = new FrmReparacion();

            //Muestro el formulario
            reparacion.Show();

            //Cierro este formulario
            this.Close();
        }
        private void btnVenta_Click(object sender, EventArgs e)
        {
            //Creo un objeto de tipo FrmVentas
            FrmVentas ventas = new FrmVentas();

            //Muestro el formulario
            ventas.Show();

            //Cierro este formulario
            this.Close();
        }

        private void pTitulo_MouseUp(object sender, MouseEventArgs e)
        {
            //Le asigno a la variable Arrasta el valor False
            Arrastra = false;
        }

        private void FrmGestion_MouseUp(object sender, MouseEventArgs e)
        {
            //Le asigno a la variable Arrasta el valor False
            Arrastra = false;
        }

        private void pTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando hago click en el cuerpo del formulario

            //Asigno el valor de la variable PosX con el valor del MouseEventArg
            PosX = e.X;

            //Asigno el valor de la variable PosY con el valor del MouseEventArg
            PosY = e.Y;

            //Le asigno a la variable Arrasta el valor True
            Arrastra = true;
        }

        private void FrmGestion_MouseDown(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando hago click en el cuerpo del formulario

            //Asigno el valor de la variable PosX con el valor del MouseEventArg
            PosX = e.X;

            //Asigno el valor de la variable PosY con el valor del MouseEventArg
            PosY = e.Y;

            //Le asigno a la variable Arrasta el valor True
            Arrastra = true;
        }

        private void pTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando muevo por el Cuerpo del Formulario

            //Si el valor de la variable Arrastra es igual a True
            if (Arrastra == true)
            {
                //Le resto a las posiciones actuales del formulario, los valores de las variables PosX y PosY
                this.SetDesktopLocation(MousePosition.X - PosX, MousePosition.Y - PosY);
            }
        }

        private void FrmGestion_MouseMove(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando muevo por el Cuerpo del Formulario

            //Si el valor de la variable Arrastra es igual a True
            if (Arrastra == true)
            {
                //Le resto a las posiciones actuales del formulario, los valores de las variables PosX y PosY
                this.SetDesktopLocation(MousePosition.X - PosX, MousePosition.Y - PosY);
            }
        }

        private void lsReparaciones_ItemActivate(object sender, EventArgs e)
        {
            //Abro la conexion
            conexion.Open();

            //Le asigno el valor correspondiente al parametro
            cmdMostrar.Parameters[0].Value = lsReparaciones.SelectedItems[0].Text;

            ////Le digo al reader que los datos que tiene que leer son los que le llegan de la ejecucion del comando
            dreaderseleccionar = cmdMostrar.ExecuteReader();

            //Si el reader lee
            if (dreaderseleccionar.Read())
            {
                //Asigno el valor correspondiente a los textBox
                txtDNI.Text = dreaderseleccionar[0].ToString();
                txtTipo.Text = dreaderseleccionar[1].ToString();
                txtFecha.Text = dreaderseleccionar[2].ToString();

            }

            //Cierro el reader
            dreaderseleccionar.Close();

            //Cierro la conexion
            conexion.Close();
        }
        
    }
}
