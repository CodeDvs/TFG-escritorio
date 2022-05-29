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
    public partial class msgBoxEliminarEquipo : Form
    {
        public msgBoxEliminarEquipo()
        {
            InitializeComponent();
        }
        //Conexion
        //SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-2ECAU7F;Initial Catalog=BDfinal;Integrated Security=True");
        SqlConnection conexion = new SqlConnection("Data Source=segundo150\\segundo150;Initial Catalog=DAM_AnderSabariego;Integrated Security=True");

        //Comandos
        SqlCommand cmdCargarReparaciones = new SqlCommand();
        SqlCommand cmdEliminar = new SqlCommand();
        SqlCommand cmdEliminarComent = new SqlCommand();

        //Parametros
        SqlParameter prmDNI = new SqlParameter();
        SqlParameter prmDNICOM = new SqlParameter();

        //DataReader
        SqlDataReader dreader;

        

        //Variables
        int PosX, PosY;
        Boolean Arrastra;
        public String DNI;
        private void msgBoxEliminarEquipo_Load(object sender, EventArgs e)
        {
            lblEliminarEquipo.Text = DNI;

            //Defino el comando cmdCargar
            cmdCargarReparaciones.CommandType = CommandType.Text;
            cmdCargarReparaciones.CommandText = "Select IDreparacion,DNIpropietario,Fecha from Reparaciones";
            cmdCargarReparaciones.Connection = conexion;

            //Defino el comando para eliminarcab
            cmdEliminar.CommandType = CommandType.Text;
            cmdEliminar.CommandText = "Delete from Reparaciones where DNIpropietario=@prmDNI";
            cmdEliminar.Connection = conexion;

            cmdEliminarComent.CommandType = CommandType.Text;
            cmdEliminarComent.CommandText = "Delete from Comentarios where DNI=@prmDNICOM";
            cmdEliminarComent.Connection = conexion;

            //Defino el parametro para el comando eliminar
            prmDNI.ParameterName = "@prmDNI";
            prmDNI.DbType = DbType.String;
            prmDNI.Size = 9;
            prmDNI.Direction = ParameterDirection.Input;

            //Defino el parametro para el comando eliminarcoment
            prmDNICOM.ParameterName = "@prmDNICOM";
            prmDNICOM.DbType = DbType.String;
            prmDNICOM.Size = 9;
            prmDNICOM.Direction = ParameterDirection.Input;

            //Añado los parametros a los comandos
            cmdEliminar.Parameters.Add(prmDNI);
            cmdEliminarComent.Parameters.Add(prmDNICOM);

            FrmReparacion reparacion = Owner as FrmReparacion;
            lblEliminarEquipo.Text = reparacion.dni;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            FrmReparacion reparacion = Owner as FrmReparacion;
            try
            {

                //Le doy valor a los parametros y a su vez ejecuto los comandos correespondientes para la eliminacion de comentarios y equipos en reparacion
                cmdEliminarComent.Parameters[0].Value = DNI;
                cmdEliminarComent.ExecuteNonQuery();
                cmdEliminar.Parameters[0].Value = DNI;
                cmdEliminar.ExecuteNonQuery();


                //Le doy a lblRespuesta el siguiente valor
                reparacion.lblRespuesta.Text = "¡Elemento eliminado correctamente!";

                //Le doy el color verde a la letra de lblRespuesta
                reparacion.lblRespuesta.ForeColor = Color.Green;

                //Vacio los cuadros de texto
                reparacion.txtDNI.Text = "";
                reparacion.txtFecha.Text = "";
                reparacion.txtReparado.Text = "";
                reparacion.txtTipo.Text = "";


                //Limpio los items de lsReparaciones
                reparacion.lsReparaciones.Items.Clear();

                //Le digo al lector que lo que tiene que leer son los datos que le lleguen del cmdLogin al ejecutarse
                dreader = cmdCargarReparaciones.ExecuteReader();

                //Mientras el reader lea
                while (dreader.Read())
                {
                    //Creo un objeto de tipo ListViewItem
                    ListViewItem li = new ListViewItem();

                    //Añado un nuevo item a lsReparaciones
                    li = reparacion.lsReparaciones.Items.Add(dreader[1].ToString());
                   
                }
                //Cierro el datareader
                dreader.Close();

                //Cierro este formulario
                this.Close();
            }
            catch
            {
                //Cambio el texto del lblRespuesta
                reparacion.lblRespuesta.Text = "¡Error al eliminar el elemento!";

                //Cambio el color de lblRespuesta
                reparacion.lblRespuesta.ForeColor = Color.Red;

                //Cierra el formulario
                this.Close();
            }
            //Cierro la conexion
            conexion.Close();
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

        private void msgBoxEliminarEquipo_MouseDown(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando hago click en el cuerpo del formulario

            //Asigno el valor de la variable PosX con el valor del MouseEventArg
            PosX = e.X;

            //Asigno el valor de la variable PosY con el valor del MouseEventArg
            PosY = e.Y;

            //Le asigno a la variable Arrasta el valor True
            Arrastra = true;
        }

        private void msgBoxEliminarEquipo_MouseMove(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando muevo por el Cuerpo del Formulario

            //Si el valor de la variable Arrastra es igual a True
            if (Arrastra == true)
            {
                //Le resto a las posiciones actuales del formulario, los valores de las variables PosX y PosY
                this.SetDesktopLocation(MousePosition.X - PosX, MousePosition.Y - PosY);
            }
        }

        private void msgBoxEliminarEquipo_MouseUp(object sender, MouseEventArgs e)
        {
            //Le asigno a la variable Arrasta el valor False
            Arrastra = false;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            //Cierra el formulario
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Cierra el formulario
            this.Close();
        }
    }
}
