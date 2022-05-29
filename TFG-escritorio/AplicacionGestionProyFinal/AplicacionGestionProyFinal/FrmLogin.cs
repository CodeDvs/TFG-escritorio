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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        //Conexion
        //SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-2ECAU7F;Initial Catalog=BDfinal;Integrated Security=True");
        SqlConnection conexion = new SqlConnection("Data Source=segundo150\\segundo150;Initial Catalog=DAM_AnderSabariego;Integrated Security=True");

        //Comandos
        SqlCommand cmdLogin = new SqlCommand();

        //DataReader
        SqlDataReader dreader;

        //Parametros
        SqlParameter prmCorreo = new SqlParameter();
        SqlParameter prmPass = new SqlParameter();

        //Variables
        int PosX, PosY;
        Boolean Arrastra;
        public static string correo;

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            //Habilito el passwordchar para que en vez de mostrar la contraseña muestre puntos
            txtContrasena.UseSystemPasswordChar = true;

            //Defino el comando cmdCargar
            cmdLogin.CommandType = CommandType.Text;
            cmdLogin.CommandText = "Select correo from Usuarios where correo=@prmCorreo and pass=@prmPass";
            cmdLogin.Connection = conexion;

            //Defino el parametro 
            prmCorreo.ParameterName = "@prmCorreo";
            prmCorreo.DbType = DbType.String;
            prmCorreo.Size = 50;
            prmCorreo.Direction = ParameterDirection.Input;

            //Defino el parametro 
            prmPass.ParameterName = "@prmPass";
            prmPass.DbType = DbType.String;
            prmPass.Size = 25;
            prmPass.Direction = ParameterDirection.Input;

            //Añado los parametros al comando
            cmdLogin.Parameters.Add(prmCorreo);
            cmdLogin.Parameters.Add(prmPass);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Salgo de la aplicacion
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            //Minimizo la aplicacion
            this.WindowState = FormWindowState.Minimized;
        }

        private void pTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando hago click en el Titulo del formulario

            //Asigno el valor de la variable PosX con el valor del MouseEventArg
            PosX = e.X;

            //Asigno el valor de la variable PosY con el valor del MouseEventArg
            PosY = e.Y;

            //Le asigno a la variable Arrasta el valor True
            Arrastra = true;
        }

        private void pTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando muevo por el Titulo del Formulario

            //Si el valor de la variable Arrastra es igual a True
            if (Arrastra == true)
            {
                //Le resto a las posiciones actuales del formulario, los valores de las variables PosX y PosY
                this.SetDesktopLocation(MousePosition.X - PosX, MousePosition.Y - PosY);
            }
        }

        private void pTitulo_MouseUp(object sender, MouseEventArgs e)
        {
            //Le asigno a la variable Arrasta el valor False
            Arrastra = false;
        }

        private void FrmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando hago click en el cuerpo del formulario

            //Asigno el valor de la variable PosX con el valor del MouseEventArg
            PosX = e.X;

            //Asigno el valor de la variable PosY con el valor del MouseEventArg
            PosY = e.Y;

            //Le asigno a la variable Arrasta el valor True
            Arrastra = true;
        }

        private void FrmLogin_MouseMove(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando muevo por el cuerpo del Formulario

            //Si el valor de la variable Arrastra es igual a True
            if (Arrastra == true)
            {
                //Le resto a las posiciones actuales del formulario, los valores de las variables PosX y PosY
                this.SetDesktopLocation(MousePosition.X - PosX, MousePosition.Y - PosY);
            }
        }

        private void FrmLogin_MouseUp(object sender, MouseEventArgs e)
        {
            //Le asigno a la variable Arrasta el valor False
            Arrastra = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //Deshabilito el passwordchar para que muestre la contraseña
            txtContrasena.UseSystemPasswordChar = false;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //Habilito el passwordchar para que en vez de mostrar la contraseña muestre puntos
            txtContrasena.UseSystemPasswordChar = true; 
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Abro la conexion
            conexion.Open();

            //Asigno los valores correspondientes a los parametros
            cmdLogin.Parameters[0].Value = txtCorreo.Text;
            cmdLogin.Parameters[1].Value = txtContrasena.Text;

            //Le digo al lector que lo que tiene que leer son los datos que le lleguen del cmdLogin al ejecutarse
            dreader = cmdLogin.ExecuteReader();

            //Si el reader lee
            if (dreader.Read())
            {
                //Creo un objeto de tipo FrmGestion
                FrmGestion inicio = new FrmGestion();

                //guardo en la variable correo el valor de txtCorreo para su posterior uso
                correo = txtCorreo.Text;

                //Muestro el formulario
                inicio.Show();

                //Cierro este formulario
                this.Close();

                //Cierro la conexion
                conexion.Close();
            }
            else //Si el reader no lee
            {
                //Le doy el siguiente valor al lblError
                lblError.Text = "¡Las credenciales no son correctas!";

                //Le cambio el color de la letra a rojo
                lblError.ForeColor = Color.Red;

                //Cierro la conexion
                conexion.Close();
            }
        }

        
    }
}
