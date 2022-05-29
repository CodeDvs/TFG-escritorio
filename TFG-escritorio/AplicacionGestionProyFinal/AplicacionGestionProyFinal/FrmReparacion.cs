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
    public partial class FrmReparacion : Form
    {
        public FrmReparacion()
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
        String Reparado;
        int id;
        public string dni;

        private void FrmReparacion_Load(object sender, EventArgs e)
        {
            //Pongo los controles en readonly
            txtDNI.ReadOnly = true;
            txtFecha.ReadOnly = true;
            txtTipo.ReadOnly = true;
            txtReparado.ReadOnly = true;

            //Ejecuto la funcion cargarlista
            cargarlista();

            //Defino el comando para mostrar
            cmdMostrar.CommandType = CommandType.Text;
            cmdMostrar.CommandText = "Select IDReparacion,DNIpropietario,TipoDeEquipo,Fecha,Reparado from Reparaciones where DNIpropietario=@prmDNI";
            cmdMostrar.Connection = conexion;

            //Defino el parametro para el comando mostrar
            prmDNI.ParameterName = "@prmDNI";
            prmDNI.DbType = DbType.String;
            prmDNI.Size = 9;
            prmDNI.Direction = ParameterDirection.Input;

            //Añado el valor correspondiente al parametro del comando cmdMostrar
            cmdMostrar.Parameters.Add(prmDNI);


        }

        private void pTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando hago click en titulo

            //Asigno el valor de la variable PosX con el valor del MouseEventArg
            PosX = e.X;

            //Asigno el valor de la variable PosY con el valor del MouseEventArg
            PosY = e.Y;

            //Le asigno a la variable Arrasta el valor True
            Arrastra = true;
        }

        private void pTitulo_MouseUp(object sender, MouseEventArgs e)
        {
            //Le asigno a la variable Arrasta el valor False
            Arrastra = false;
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

        private void FrmReparacion_MouseDown(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando hago click en el cuerpo del formulario

            //Asigno el valor de la variable PosX con el valor del MouseEventArg
            PosX = e.X;

            //Asigno el valor de la variable PosY con el valor del MouseEventArg
            PosY = e.Y;

            //Le asigno a la variable Arrasta el valor True
            Arrastra = true;
        }

        private void FrmReparacion_MouseUp(object sender, MouseEventArgs e)
        {
            //Le asigno a la variable Arrasta el valor False
            Arrastra = false;
        }

        private void FrmReparacion_MouseMove(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando muevo por el Cuerpo del Formulario

            //Si el valor de la variable Arrastra es igual a True
            if (Arrastra == true)
            {
                //Le resto a las posiciones actuales del formulario, los valores de las variables PosX y PosY
                this.SetDesktopLocation(MousePosition.X - PosX, MousePosition.Y - PosY);
            }
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            //Creo un nuevo objeto de tipo FrmGestion
            FrmGestion inicio = new FrmGestion();

            //Muestro el formulario inicio
            inicio.Show();

            //Cierro el formulario
            this.Hide();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            
            //Si los siguientes campos esta vacios
            if (txtDNI.Text == "" || txtFecha.Text == "" || txtTipo.Text == "" || txtReparado.Text == "")
            {
                //Le asigno el siguiente valor a lblRespuesta
                lblRespuesta.Text = "¡Debes seleccionar un elemento para poder eliminarlo!";

                //Le doy el color rojo al texto del lblRespuesta
                lblRespuesta.ForeColor = Color.Red;
            }
            else
            {
                //Creo un objeto de tipo msgBoxEliminarEquipo
                msgBoxEliminarEquipo eliminar = new msgBoxEliminarEquipo();

                //Añado el formulario del que es propietario
                AddOwnedForm(eliminar);

                //Le asigno el valor correspondiente a la variable DNI
                eliminar.DNI = txtDNI.Text;

                //Muestro el formulario en forma de dialogo
                eliminar.ShowDialog();
               
            }
        }

        private void btnComentarios_Click(object sender, EventArgs e)
        {
            //Si los siguientes campos esta vacios
            if (txtDNI.Text == "" || txtFecha.Text == "" || txtTipo.Text == "" || txtReparado.Text == "")
            {
                //Asigno el siguiente valor al lblRespuesta
                lblRespuesta.Text = "¡Debes seleccionar un elemento para ver los comentarios asociados!";

                //Le doy el color Rojo a la letra del lblRespuesta
                lblRespuesta.ForeColor = Color.Red;
            }
            else
            {
                //Creo un objeto de tipo FrmVerComentarios
                FrmVerComentarios comentarios = new FrmVerComentarios();

                //Le asigno el valor correspondiente a la variable dnicoment
                comentarios.dnicoment = dni;

                //Muestro el formulario
                comentarios.Show();

                //Cierro el formulario
                this.Close();
                
            }
        }

        private void lsReparaciones_ItemActivate(object sender, EventArgs e)
        {
            //Abro la conexion
            conexion.Open();

            //Vacio el lblRespuesta
            lblRespuesta.Text = "";
            
            //Le asigno el valor correspondiente al parametro del comando cmdMostrar
            cmdMostrar.Parameters[0].Value = lsReparaciones.SelectedItems[0].Text;

            //Le digo al lector que los datos que tiene que leer son los que le llegan de la ejecucion del siguiente comando
            dreaderseleccionar = cmdMostrar.ExecuteReader();

            //Si el reader lee
            if (dreaderseleccionar.Read())
            {
                //Asigno los valores correspondientes a los Textbox
                id = int.Parse(dreaderseleccionar[0].ToString());
                txtDNI.Text = dreaderseleccionar[1].ToString();
                dni= dreaderseleccionar[1].ToString();
                txtTipo.Text = dreaderseleccionar[2].ToString();
                txtFecha.Text = dreaderseleccionar[3].ToString();

                //Le asigno el valor del parametro correspondiente a la variable Reparado
                Reparado = dreaderseleccionar[4].ToString();

                if (Reparado == "True")//Si reparado es igual a true
                {
                    //Muestro el texto indicaro en el txtReparado
                    txtReparado.Text = "Si";
                }//Si reparado es igual a false, muestro el siguiente texto
                else { txtReparado.Text = "No"; }
               
            }

            //Cierro el reader
            dreaderseleccionar.Close();

            //Cierro la conexion
            conexion.Close();
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //Creo un objeto de tipo FrmNuevaReparacion
            FrmNuevaReparacion nueva = new FrmNuevaReparacion();

            //Le añado el formulario del que es propietario
            AddOwnedForm(nueva);

            //Muestro el formulario en forma de dialogo
            nueva.ShowDialog();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            //Si los siguientes campos estan vacios
            if (txtDNI.Text == "" || txtFecha.Text == "" || txtTipo.Text == "" || txtReparado.Text == "")
            {
                //Asigno el siguiente valor al lblRespuesta
                lblRespuesta.Text = "¡Debes seleccionar un elemento antes de editarlo!";
                //Le asigno el color rojo a la letra del lblRespuesta
                lblRespuesta.ForeColor = Color.Red;
            }
            else
            {
                //Creo un nuevo objeto de tipo FrmEditarReparacion
                FrmEditarReparacion editar = new FrmEditarReparacion();

                //Le añado el formulario del que es propietario
                AddOwnedForm(editar);

                //Muestro el formulario en forma de dialogo
                editar.ShowDialog();
            }
            
        }

        public void cargarlista()
        {
            
            //Defino el comando 
            cmdCargarReparaciones.CommandType = CommandType.Text;
            cmdCargarReparaciones.CommandText = "Select IDreparacion,DNIpropietario,Fecha from Reparaciones";
            cmdCargarReparaciones.Connection = conexion;

            //Abro la conexion
            conexion.Open();

            //Limpio los items del lsReparaciones
            lsReparaciones.Items.Clear();

            //Le digo al reader que lo que tiene que leer es lo que reciba del comando cmdCargarReparaciones
            dreader = cmdCargarReparaciones.ExecuteReader();

            //Mientras lea
            while (dreader.Read())
            {
                //Creo un nuevo objeto de tipo ListViewItem
                ListViewItem li = new ListViewItem();

                //Añado los items a la ListView
                li = lsReparaciones.Items.Add(dreader[1].ToString());
                li.SubItems.Add(dreader[2].ToString());
            }

            //Cierro el lector
            dreader.Close();

            //Cierro la conexion
            conexion.Close();
        }
    }
}
