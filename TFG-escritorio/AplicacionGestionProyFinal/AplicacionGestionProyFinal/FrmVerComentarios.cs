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
    public partial class FrmVerComentarios : Form
    {
        public FrmVerComentarios()
        {
            InitializeComponent();
        }
        //Conexion
        //SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-2ECAU7F;Initial Catalog=BDfinal;Integrated Security=True");
        SqlConnection conexion = new SqlConnection("Data Source=segundo150\\segundo150;Initial Catalog=DAM_AnderSabariego;Integrated Security=True");

        //Comandos
        SqlCommand cmdCargarComentarios = new SqlCommand();
        SqlCommand cmdEliminar = new SqlCommand();

        //Parametros
        SqlParameter prmDNI = new SqlParameter();
        SqlParameter prmIdEliminar = new SqlParameter();

        //DataReader
        SqlDataReader dreader;

        //Variables
        int PosX, PosY;
        Boolean Arrastra;
        int ideliminar;
        public string dnicoment;

        private void btnVolver_Click(object sender, EventArgs e)
        {
            //Creo un nuevo objeto de tipo FrmReparacion
            FrmReparacion reparacion = new FrmReparacion();

            //Muestro reparacion
            reparacion.Show();

            //Cierro este formulario
            this.Close();

        }

        private void FrmVerComentarios_Load(object sender, EventArgs e)
        {

            //Defino el comando para eliminar
            cmdEliminar.CommandType = CommandType.Text;
            cmdEliminar.CommandText = "Delete Comentarios where idComentario=@prmIdEliminar";
            cmdEliminar.Connection = conexion;

            //Defino el comando para mostrar
            cmdCargarComentarios.CommandType = CommandType.Text;
            cmdCargarComentarios.CommandText = "Select idComentario,fecha,TextoComentario from Comentarios where DNI=@prmDNI";
            cmdCargarComentarios.Connection = conexion;

            //Defino el parametro para el comando mostrar
            prmDNI.ParameterName = "@prmDNI";
            prmDNI.DbType = DbType.String;
            prmDNI.Size = 9;
            prmDNI.Direction = ParameterDirection.Input;

            //Defino el parametro para el comando mostrar
            prmIdEliminar.ParameterName = "@prmIdEliminar";
            prmIdEliminar.DbType = DbType.Int32;
            prmIdEliminar.Direction = ParameterDirection.Input;

            //Añado el parametro al comando
            cmdCargarComentarios.Parameters.Add(prmDNI);

            //Le asigno el valor correspondiente al comando
            cmdCargarComentarios.Parameters[0].Value = dnicoment;

            //Añado el parametro  al comando correspondiente
            cmdEliminar.Parameters.Add(prmIdEliminar);

            //Ejecuto la funcion cargar
            cargar();

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

        private void pTitulo_MouseUp(object sender, MouseEventArgs e)
        {
            //Le asigno a la variable Arrasta el valor False
            Arrastra = false;
        }

        private void FrmVerComentarios_MouseDown(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando hago click en el titulo

            //Asigno el valor de la variable PosX con el valor del MouseEventArg
            PosX = e.X;

            //Asigno el valor de la variable PosY con el valor del MouseEventArg
            PosY = e.Y;

            //Le asigno a la variable Arrasta el valor True
            Arrastra = true;
        }

        private void FrmVerComentarios_MouseUp(object sender, MouseEventArgs e)
        {
            //Le asigno a la variable Arrasta el valor False
            Arrastra = false;
        }

        private void FrmVerComentarios_MouseMove(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando muevo por el titulo

            //Si el valor de la variable Arrastra es igual a True
            if (Arrastra == true)
            {
                //Le resto a las posiciones actuales del formulario, los valores de las variables PosX y PosY
                this.SetDesktopLocation(MousePosition.X - PosX, MousePosition.Y - PosY);
            }
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Creo un objeto de tipo FrmAgregarComentario
            FrmAgregarComentario agregarComentario = new FrmAgregarComentario();

            //Le agrego el formulario del que es propietario
            AddOwnedForm(agregarComentario);

            //Le asigno el valor correspondiente a la variable dninuevocoment
            agregarComentario.dninuevocoment = dnicoment;

            //Muestro el formulario en forma de dialogo
            agregarComentario.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Si el ideliminar es igual a 0 o es igual a ""
            if (ideliminar==0 || ideliminar.ToString()=="")
            {
                //Creo un objeto de tipo msgBoxDobleClick
                msgBoxDobleClick atencion = new msgBoxDobleClick();

                //Muestro el dialogo del formulario atencion
                atencion.ShowDialog();
            }
            else
            {
                //Abro la conexion
                conexion.Open();

                //Le asigno al parametro del comando el valor correspondiente
                cmdEliminar.Parameters[0].Value = ideliminar;

                //Ejecuto el comando para eliminar
                cmdEliminar.ExecuteNonQuery();

                //Cierro la conexion
                conexion.Close();

                //Ejecuto la funcion cargar
                cargar();
            }
            
        }

        private void lsComentarios_ItemActivate(object sender, EventArgs e)
        {
            //Le asigno el id seleccionado a la variable ideliminar
            ideliminar = int.Parse(lsComentarios.SelectedItems[0].Text);
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

        public void cargar()
        {
            //Abro la conexion
            conexion.Open();

            //Limpio los items de lsComentarios
            lsComentarios.Items.Clear();


            dreader = cmdCargarComentarios.ExecuteReader();

            //Mientras lea
            while (dreader.Read())
            {
                //Creo un objeto de tipo ListViewItem
                ListViewItem li = new ListViewItem();

                //Añado los objetos a la listview
                li = lsComentarios.Items.Add(dreader[0].ToString());
                li.SubItems.Add(dreader[1].ToString());
                li.SubItems.Add(dreader[2].ToString());
            }
            //Cierro el data reader
            dreader.Close();

            //Cierro la conexion
            conexion.Close();
        }
    }
}
