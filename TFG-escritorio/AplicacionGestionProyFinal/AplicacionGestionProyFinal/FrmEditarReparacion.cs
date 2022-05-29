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
    public partial class FrmEditarReparacion : Form
    {
        public FrmEditarReparacion()
        {
            InitializeComponent();

        }

        //Conexion
        //SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-2ECAU7F;Initial Catalog=BDfinal;Integrated Security=True");
        SqlConnection conexion = new SqlConnection("Data Source=segundo150\\segundo150;Initial Catalog=DAM_AnderSabariego;Integrated Security=True");

        //Comandos
        SqlCommand cmdCargarCmb = new SqlCommand();
        SqlCommand cmdActualizar = new SqlCommand();

        //Parametros
        SqlParameter prmDNI = new SqlParameter();
        SqlParameter prmTipo = new SqlParameter();
        SqlParameter prmFecha = new SqlParameter();
        SqlParameter prmReparado = new SqlParameter();

        //DataTable
        DataTable dtCmb = new DataTable();

        //Adaptadores
        SqlDataAdapter adaptador = new SqlDataAdapter();

        //Variables
        int PosX, PosY;
        Boolean Arrastra;
        Boolean Reparado;
        private void FrmEditarReparacion_Load(object sender, EventArgs e)
        {

            FrmReparacion reparacion = Owner as FrmReparacion;


            //Defino el comando para cargar el combo
            cmdCargarCmb.CommandType = CommandType.Text;
            cmdCargarCmb.CommandText = "Select id,TipoEquipo from TipoEquipo";
            cmdCargarCmb.Connection = conexion;

            //Defino el comando para cargar la update
            cmdActualizar.CommandType = CommandType.Text;
            cmdActualizar.CommandText = "Update Reparaciones Set TipoDeEquipo=@prmTipo, Fecha=@prmFecha,Reparado=@prmReparado where DNIpropietario=@prmDNI";
            cmdActualizar.Connection = conexion;

            //Defino el parametro para el tipo
            prmTipo.ParameterName = "@prmTipo";
            prmTipo.DbType = DbType.String;
            prmTipo.Size = 50;
            prmTipo.Direction = ParameterDirection.Input;

            //Defino el parametro para la fecha
            prmFecha.ParameterName = "@prmFecha";
            prmFecha.DbType = DbType.Date;
            prmFecha.Direction = ParameterDirection.Input;

            //Defino el parametro para el estado de reparacion
            prmReparado.ParameterName = "@prmReparado";
            prmReparado.DbType = DbType.Boolean;
            prmReparado.Direction = ParameterDirection.Input;

            //Defino el parametro para el DNI
            prmDNI.ParameterName = "@prmDNI";
            prmDNI.DbType = DbType.String;
            prmDNI.Size = 9;
            prmDNI.Direction = ParameterDirection.Input;


            //Añado los parametros al comando
            cmdActualizar.Parameters.Add(prmTipo);
            cmdActualizar.Parameters.Add(prmFecha);
            cmdActualizar.Parameters.Add(prmReparado);
            cmdActualizar.Parameters.Add(prmDNI);

            //Le doy el valor al parametro prmDNI
            cmdActualizar.Parameters[3].Value = reparacion.txtDNI.Text;

            //Cierro la conexion
            conexion.Open();

            //Le asigno el comando al adaptador
            adaptador.SelectCommand = cmdCargarCmb;

            //Cargo la tabla mediante el metodo fill
            adaptador.Fill(dtCmb);

            //Monto en combo box
            cmbTipo.DataSource = dtCmb;
            cmbTipo.DisplayMember = "TipoEquipo";
            cmbTipo.ValueMember = "TipoEquipo";

            //Cierro la conexion
            conexion.Close();
            
            //Txt DNI
            txtDNI.ReadOnly = true;
            txtDNI.Text = reparacion.txtDNI.Text;

            //Txt Fecha
            txtFecha.Text = reparacion.txtFecha.Text;

            //Asigno los siguientes valores a los chk segun la condicion que cumplan
            if (reparacion.txtReparado.Text == "No")
            {
                chkNo.Checked = true;
            }
            else if (reparacion.txtReparado.Text == "Si")
            {
                chkSi.Checked = true;
            }

            //Asigno los siguientes valores a los Combos segun la condicion que cumplan
            if (reparacion.txtTipo.Text=="Portatil")
            {
                cmbTipo.SelectedIndex = 0;
            }
            else if (reparacion.txtTipo.Text == "Sobremesa")
            {
                cmbTipo.SelectedIndex = 1;
            }
            else if (reparacion.txtTipo.Text == "Movil")
            {
                cmbTipo.SelectedIndex = 2;
            }
            else if (reparacion.txtTipo.Text == "Tablet")
            {
                cmbTipo.SelectedIndex = 3;
            }
            else if (reparacion.txtTipo.Text == "Smartwatch")
            {
                cmbTipo.SelectedIndex = 4;
            }

        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Cierro el formulario
            this.Close();
        }

        private void chkNo_CheckedChanged(object sender, EventArgs e)
        {
            //Si chkNo esta checked
            if (chkNo.Checked==true)
            {
                //Descheckeo el chkSi
                chkSi.Checked = false;

                //Le doy el valor false a la variable Reparado
                Reparado = false;
            }
        }

        private void chkSi_CheckedChanged(object sender, EventArgs e)
        {
            //Si chkSi esta checked
            if (chkSi.Checked == true)
            {
                //Descheckeo el chkNo
                chkNo.Checked = false;

                //Le doy el valor true a la variable Reparado
                Reparado = true;
            }
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

        private void FrmEditarReparacion_MouseDown(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando hago click en titulo

            //Asigno el valor de la variable PosX con el valor del MouseEventArg
            PosX = e.X;

            //Asigno el valor de la variable PosY con el valor del MouseEventArg
            PosY = e.Y;

            //Le asigno a la variable Arrasta el valor True
            Arrastra = true;
        }

        private void FrmEditarReparacion_MouseMove(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando muevo por el titulo

            //Si el valor de la variable Arrastra es igual a True
            if (Arrastra == true)
            {
                //Le resto a las posiciones actuales del formulario, los valores de las variables PosX y PosY
                this.SetDesktopLocation(MousePosition.X - PosX, MousePosition.Y - PosY);
            }
        }

        private void FrmEditarReparacion_MouseUp(object sender, MouseEventArgs e)
        {
            //Le asigno a la variable Arrasta el valor False
            Arrastra = false;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            //Si estos campos estan vacios
            if (txtDNI.Text == "" || txtFecha.Text == "" || chkNo.Checked == false && chkSi.Checked == false)
            {
                //Le asigno el siguiente valor al lblError
                lblError.Text = "!No puedes dejar ningun campo en blanco¡";

                //Le asigno el color rojo a la letra del lblError
                lblError.ForeColor = Color.Red;
            }
            else 
            {
                try
                {
                    //Creo un objeto de tipo FrmReparacion y le digo que es el dueño de este formulario
                    FrmReparacion reparacion = Owner as FrmReparacion;

                    //Asigno los valores a los parametros del comando
                    cmdActualizar.Parameters[0].Value = cmbTipo.SelectedValue;
                    cmdActualizar.Parameters[1].Value = txtFecha.Text;
                    cmdActualizar.Parameters[2].Value = Reparado;

                    //Abro la conexion
                    conexion.Open();

                    //Ejecuto el comando
                    cmdActualizar.ExecuteNonQuery();

                    //Limpio la listView
                    reparacion.lsReparaciones.Items.Clear();

                    //Cargo la lista
                    reparacion.cargarlista();

                    //Cierro la conexion
                    conexion.Close();

                    //Cierro el formulario
                    this.Close();
                }
                catch
                {
                    //Le asigno el siguiente valor al lblError
                    lblError.Text = "!Erro, la actualizacion no se pudo realizar¡";

                    //Le asigno el color rojo a la letra del lblError
                    lblError.ForeColor = Color.Red;
                }
                
            }
        }
    }
}
