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
    public partial class FrmAgregarComentario : Form
    {
        public FrmAgregarComentario()
        {
            InitializeComponent();
        }

        //Conexion
        //SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-2ECAU7F;Initial Catalog=BDfinal;Integrated Security=True");
        SqlConnection conexion = new SqlConnection("Data Source=segundo150\\segundo150;Initial Catalog=DAM_AnderSabariego;Integrated Security=True");

        //Comandos
        SqlCommand cmdMax = new SqlCommand();
        SqlCommand cmdAlta = new SqlCommand();
        SqlCommand cmdCargarComentarios = new SqlCommand();

        //Parametros
        SqlParameter prmDNI = new SqlParameter();
        SqlParameter prmDNIcargar = new SqlParameter();
        SqlParameter prmIdComentario = new SqlParameter();
        SqlParameter prmTextoComentario = new SqlParameter();
        SqlParameter prmFecha = new SqlParameter();
        SqlParameter prmMDNI = new SqlParameter();

        //DataReader
        SqlDataReader dreader;

        //Variables
        int PosX, PosY;
        Boolean Arrastra;
        public string dninuevocoment;
        private void FrmAgregarComentario_Load(object sender, EventArgs e)
        {
            //Defino el comando cmdCargar
            cmdMax.CommandType = CommandType.Text;
            cmdMax.CommandText = "select max(idComentario) from Comentarios where DNI=@prmMDNI";
            cmdMax.Connection = conexion;

            //Defino el comando cmdCargar
            cmdAlta.CommandType = CommandType.Text;
            cmdAlta.CommandText = "insert into Comentarios (DNI,idComentario,TextoComentario,fecha) Values (@prmDNI,@prmIdComentario,@prmTextoComentario,@prmFecha)";
            cmdAlta.Connection = conexion;

            //Defino el parametro para el comando mostrar
            prmDNI.ParameterName = "@prmDNI";
            prmDNI.DbType = DbType.String;
            prmDNI.Size = 9;
            prmDNI.Direction = ParameterDirection.Input;

            //Defino el parametro para el comando mostrar
            prmMDNI.ParameterName = "@prmMDNI";
            prmMDNI.DbType = DbType.String;
            prmMDNI.Size = 9;
            prmMDNI.Direction = ParameterDirection.Input;

            //Defino el parametro para el comando mostrar
            prmIdComentario.ParameterName = "@prmIdComentario";
            prmIdComentario.DbType = DbType.Int32;
            prmIdComentario.Direction = ParameterDirection.Input;

            //Defino el parametro para el comando mostrar
            prmTextoComentario.ParameterName = "@prmTextoComentario";
            prmTextoComentario.DbType = DbType.String;
            prmTextoComentario.Size = 50;
            prmTextoComentario.Direction = ParameterDirection.Input;

            //Defino el parametro para el comando mostrar
            prmFecha.ParameterName = "@prmFecha";
            prmFecha.DbType = DbType.DateTime;
            prmFecha.Direction = ParameterDirection.Input;

            //Defino el comando para mostrar
            cmdCargarComentarios.CommandType = CommandType.Text;
            cmdCargarComentarios.CommandText = "Select fecha,TextoComentario from Comentarios where DNI=@prmDNIcargar";
            cmdCargarComentarios.Connection = conexion;

            //Defino el parametro para el comando mostrar
            prmDNIcargar.ParameterName = "@prmDNIcargar";
            prmDNIcargar.DbType = DbType.String;
            prmDNIcargar.Size = 9;
            prmDNIcargar.Direction = ParameterDirection.Input;

            //Añado el parametro al comando
            cmdCargarComentarios.Parameters.Add(prmDNIcargar);
            

            
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Añado los parametros al comando
            cmdAlta.Parameters.Add(prmDNI);
            cmdAlta.Parameters.Add(prmIdComentario);
            cmdAlta.Parameters.Add(prmTextoComentario);
            cmdAlta.Parameters.Add(prmFecha);

            //Añado el parametro al comando
            cmdMax.Parameters.Add(prmMDNI);

            //Le doy el valor correspondiente al parametro del comando
            cmdMax.Parameters[0].Value = dninuevocoment;

            //Abro la conexion
            conexion.Open();
            
            //Si el comando cmdMax me devuelvo un nulo o un vacio
            if (cmdMax.ExecuteScalar()==null || cmdMax.ExecuteScalar().ToString()=="")
            {
                //Le asigno al parametro el valor uno
                cmdAlta.Parameters[1].Value = 1;
            }
            else
            {
                //Le asigno al parametro el valor devuelto por el comando cmdMax + 1
                cmdAlta.Parameters[1].Value = int.Parse(cmdMax.ExecuteScalar().ToString()) + 1;
            }

            //Le asigno los valores correspondientes a los parametros
            cmdAlta.Parameters[0].Value = dninuevocoment;
            cmdAlta.Parameters[2].Value = txtNuevoComentario.Text;
            cmdAlta.Parameters[3].Value = DateTime.Now;

            //Ejecuto el comando
            cmdAlta.ExecuteNonQuery();

            //Creo un nuevo objeto de tipo FrmVerComentarios y le digo que es el dueño de este formulario
            FrmVerComentarios verComentarios = Owner as FrmVerComentarios;

            //Limpio los items de lsComentarios
            verComentarios.lsComentarios.Items.Clear();

            //Ejecuto la funcion de cargar
            verComentarios.cargar();

            //Cierro la conexion
            conexion.Close();

            //Cierro el formulario
            this.Close();
        }

        private void pTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando hago click en el titulo

            //Asigno el valor de la variable PosX con el valor del MouseEventArg
            PosX = e.X;

            //Asigno el valor de la variable PosY con el valor del MouseEventArg
            PosY = e.Y;

            //Le asigno a la variable Arrasta el valor True
            Arrastra = true;
        }

        private void pTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando muevo por el titulo

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

        private void FrmAgregarComentario_MouseDown(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando hago click en el titulo

            //Asigno el valor de la variable PosX con el valor del MouseEventArg
            PosX = e.X;

            //Asigno el valor de la variable PosY con el valor del MouseEventArg
            PosY = e.Y;

            //Le asigno a la variable Arrasta el valor True
            Arrastra = true;
        }

        private void FrmAgregarComentario_MouseMove(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando muevo por el titulo

            //Si el valor de la variable Arrastra es igual a True
            if (Arrastra == true)
            {
                //Le resto a las posiciones actuales del formulario, los valores de las variables PosX y PosY
                this.SetDesktopLocation(MousePosition.X - PosX, MousePosition.Y - PosY);
            }
        }

        private void FrmAgregarComentario_MouseUp(object sender, MouseEventArgs e)
        {
            //Le asigno a la variable Arrasta el valor False
            Arrastra = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Cierro el form
            this.Close();
        }

    }
}
