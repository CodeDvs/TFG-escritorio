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
    public partial class FrmEliminarVenta : Form
    {
        public FrmEliminarVenta()
        {
            InitializeComponent();
        }
        //Conexion
        //SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-2ECAU7F;Initial Catalog=BDfinal;Integrated Security=True");
        SqlConnection conexion = new SqlConnection("Data Source=segundo150\\segundo150;Initial Catalog=DAM_AnderSabariego;Integrated Security=True");

        //Comandos
        SqlCommand cmdEliminar = new SqlCommand();
        SqlCommand cmdEliminarLin = new SqlCommand();
        SqlCommand cmdAltasHistorico = new SqlCommand();
        SqlCommand cmdMax = new SqlCommand();
        SqlCommand cmdDNI = new SqlCommand();

        //Parametros
        SqlParameter prmCabID = new SqlParameter();
        SqlParameter prmLinID = new SqlParameter();
        SqlParameter prmIDHistorico = new SqlParameter();
        SqlParameter prmIDventa = new SqlParameter();
        SqlParameter prmCorreo = new SqlParameter();
        SqlParameter prmDNI = new SqlParameter();
        SqlParameter prmFecha = new SqlParameter();
        SqlParameter prmMotivo = new SqlParameter();
        SqlParameter prmIDsacar = new SqlParameter();


        //Variables
        int PosX, PosY;
        Boolean Arrastra;
        Random rnd = new Random();
        int codigo;
        public int ideliminarventa;
        String dniHistorico="";


        private void FrmEliminarVenta_Load(object sender, EventArgs e)
        {
            FrmVentas ventas = new FrmVentas();

            //Deshabilito los controles
            txtCodigo.Enabled = false;
            btnConfirmar.Enabled = false;

            //Defino el comando 
            cmdMax.CommandType = CommandType.Text;
            cmdMax.CommandText = "Select max(idHistorico) from Historico";
            cmdMax.Connection = conexion;

            //Defino el comando 
            cmdDNI.CommandType = CommandType.Text;
            cmdDNI.CommandText = "Select DNI from CabVenta where idVenta=@prmIDsacar";
            cmdDNI.Connection = conexion;

            //Defino el parametro
            prmIDsacar.ParameterName = "@prmIDsacar";
            prmIDsacar.DbType = DbType.Int32;
            prmIDsacar.Direction = ParameterDirection.Input;

            cmdDNI.Parameters.Add(prmIDsacar);

            //Defino el comando 
            cmdEliminar.CommandType = CommandType.Text;
            cmdEliminar.CommandText = "Delete from CabVenta where idVenta = @prmCabID";
            cmdEliminar.Connection = conexion;

            //Defino el parametro
            prmCabID.ParameterName = "@prmCabID";
            prmCabID.DbType = DbType.Int32;
            prmCabID.Direction = ParameterDirection.Input;

            //Añado los parametros al comando
            cmdEliminar.Parameters.Add(prmCabID);

            //Defino el comando 
            cmdEliminarLin.CommandType = CommandType.Text;
            cmdEliminarLin.CommandText = "Delete from LinVenta where idVenta = @prmLinID";
            cmdEliminarLin.Connection = conexion;

            //Defino el parametro
            prmLinID.ParameterName = "@prmLinID";
            prmLinID.DbType = DbType.Int32;
            prmLinID.Direction = ParameterDirection.Input;

            cmdEliminarLin.Parameters.Add(prmLinID);

            //Defino el comando 
            cmdAltasHistorico.CommandType = CommandType.Text;
            cmdAltasHistorico.CommandText = "Insert into Historico (idHistorico,idVenta,correoresponsable,DNI,Fecha,motivo) values (@prmIDHistorico,@prmIDventa,@prmCorreo,@prmDNI,@prmFecha,@prmMotivo)";
            cmdAltasHistorico.Connection = conexion;

            //Defino el parametro
            prmIDHistorico.ParameterName = "@prmIDHistorico";
            prmIDHistorico.DbType = DbType.Int32;
            prmIDHistorico.Direction = ParameterDirection.Input;

            //Defino el parametro
            prmIDventa.ParameterName = "@prmIDventa";
            prmIDventa.DbType = DbType.Int32;
            prmIDventa.Direction = ParameterDirection.Input;

            //Defino el parametro
            prmCorreo.ParameterName = "@prmCorreo";
            prmCorreo.DbType = DbType.String;
            prmCorreo.Size = 50;
            prmCorreo.Direction = ParameterDirection.Input;

            //Defino el parametro
            prmDNI.ParameterName = "@prmDNI";
            prmDNI.DbType = DbType.String;
            prmDNI.Size = 9;
            prmDNI.Direction = ParameterDirection.Input;

            //Defino el parametro
            prmFecha.ParameterName = "@prmFecha";
            prmFecha.DbType = DbType.DateTime;
            prmFecha.Direction = ParameterDirection.Input;

            //Defino el parametro
            prmMotivo.ParameterName = "@prmMotivo";
            prmMotivo.DbType = DbType.String;
            prmMotivo.Size = 25;
            prmMotivo.Direction = ParameterDirection.Input;

            //Añado los parametros al comando
            cmdAltasHistorico.Parameters.Add(prmIDHistorico);
            cmdAltasHistorico.Parameters.Add(prmIDventa);
            cmdAltasHistorico.Parameters.Add(prmCorreo);
            cmdAltasHistorico.Parameters.Add(prmDNI);
            cmdAltasHistorico.Parameters.Add(prmFecha);
            cmdAltasHistorico.Parameters.Add(prmMotivo);

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Cierro la aplicacion
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            //Minimizo la aplicacion
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtCodigo_MouseDown(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando hago click en el cuerpo del formulario

            //Asigno el valor de la variable PosX con el valor del MouseEventArg
            PosX = e.X;

            //Asigno el valor de la variable PosY con el valor del MouseEventArg
            PosY = e.Y;

            //Le asigno a la variable Arrasta el valor True
            Arrastra = true;
        }

        private void txtCodigo_MouseMove(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando muevo por el Cuerpo del Formulario

            //Si el valor de la variable Arrastra es igual a True
            if (Arrastra == true)
            {
                //Le resto a las posiciones actuales del formulario, los valores de las variables PosX y PosY
                this.SetDesktopLocation(MousePosition.X - PosX, MousePosition.Y - PosY);
            }
        }

        private void txtCodigo_MouseUp(object sender, MouseEventArgs e)
        {
            //Le asigno a la variable Arrasta el valor False
            Arrastra = false;
        }

        private void FrmEliminarVenta_MouseDown(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando hago click en el cuerpo del formulario

            //Asigno el valor de la variable PosX con el valor del MouseEventArg
            PosX = e.X;

            //Asigno el valor de la variable PosY con el valor del MouseEventArg
            PosY = e.Y;

            //Le asigno a la variable Arrasta el valor True
            Arrastra = true;
        }

        private void FrmEliminarVenta_MouseMove(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando muevo por el Cuerpo del Formulario

            //Si el valor de la variable Arrastra es igual a True
            if (Arrastra == true)
            {
                //Le resto a las posiciones actuales del formulario, los valores de las variables PosX y PosY
                this.SetDesktopLocation(MousePosition.X - PosX, MousePosition.Y - PosY);
            }
        }

        private void FrmEliminarVenta_MouseUp(object sender, MouseEventArgs e)
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

        private void pTitulo_MouseUp(object sender, MouseEventArgs e)
        {
            //Le asigno a la variable Arrasta el valor False
            Arrastra = false;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            //Creo un nuevo objeto de tipo FrmVentas y le digo que es el dueño de este formulario
            FrmVentas ventas = Owner as FrmVentas;
            
            //Si el codigo es igual al txtCodigo
            if (txtCodigo.Text==codigo.ToString())
            {   
                //Abro la conexion
                conexion.Open();

                //Si el comando cmdMax me devuelve un nulo o un vacio
                if (cmdMax.ExecuteScalar() == null || cmdMax.ExecuteScalar().ToString() == "")
                {
                    //Le asigno un uno al valor del parametro
                    cmdAltasHistorico.Parameters[0].Value = 1;
                }
                else
                {
                    //le asigno al parametro el valor que me devuelve cmdMax + 1
                    cmdAltasHistorico.Parameters[0].Value = int.Parse(cmdMax.ExecuteScalar().ToString()) + 1;
                }
                
                //Valor para los parametros de eliminar
                cmdEliminarLin.Parameters[0].Value = ventas.idEliminar;
                cmdEliminar.Parameters[0].Value = ventas.idEliminar;

                //Doy el valor a los parametros para el alta del historico
                cmdDNI.Parameters[0].Value = ventas.idEliminar;
                dniHistorico = cmdDNI.ExecuteScalar().ToString();
                cmdAltasHistorico.Parameters[1].Value = ventas.idEliminar;
                cmdAltasHistorico.Parameters[2].Value = FrmLogin.correo;
                cmdAltasHistorico.Parameters[3].Value = dniHistorico;
                cmdAltasHistorico.Parameters[4].Value = DateTime.Now;
                cmdAltasHistorico.Parameters[5].Value = txtMotivo.Text;

                //Ejecuto los comandoas de eliminar y el de altas de historico
                cmdAltasHistorico.ExecuteNonQuery();
                cmdEliminarLin.ExecuteNonQuery();
                cmdEliminar.ExecuteNonQuery();

                //Ejecuto la funcion de cargar
                ventas.cargar();

                //Cierro la conexion
                conexion.Close();

                //Cierro este formulario
                this.Close();
            }
            else
            {
                //Le doy el siguiente valor al lblError
                lblError.Text = "El codigo introducido es incorrecto";

                //Le asigno el color rojo a la letra del lblError
                lblError.ForeColor = Color.Red;
            }
        }

        private void btnRecibirCodigo_Click(object sender, EventArgs e)
        {
            //Si el campo motivo esta vacio
            if (txtMotivo.Text=="")
            {
                //Le doy el siguiente valor al lblError
                lblError.Text = "¡No puedes dejar ningun campo vacio!";

                //Le asigno el color rojo a la letra del lblError
                lblError.ForeColor = Color.Red;
            }
            else
            {
                //Habilito el textbox
                txtCodigo.Enabled = true;

                //Habilito el boton
                btnConfirmar.Enabled = true;

                //Le doy a la variable codigo el valor de un nuevo rnd
                codigo = rnd.Next(999);

                /*Correo*/

                //Declaro el mailmessage
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

                //Añado el correo al que voy a enviarselo
                msg.To.Add(FrmLogin.correo);

                //Asunto
                msg.Subject = "Codigo de seguridad";

                //Se encodea el asunto por una cuestion de servidores
                msg.SubjectEncoding = System.Text.Encoding.UTF8;


                msg.Body = "Este es el codigo de seguridad " + codigo;
                //Se encodea el body por una cuestion de servidores
                msg.BodyEncoding = System.Text.Encoding.UTF8;


                //Desde donde enviamos el correo
                msg.From = new System.Net.Mail.MailAddress("proyectofinalseim@gmail.com");

                //Creamos el cliente correo
                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

                //Le damos las credenciales para que pueda enviar el correo
                cliente.Credentials = new System.Net.NetworkCredential("proyectofinalseim@gmail.com", "seim2022");

                //Para enviar a email
                cliente.Port = 587;
                //La seguridad
                cliente.EnableSsl = true;
                //Nombre del servidor de salida
                cliente.Host = "smtp.gmail.com";

                try
                {
                    //Este es el metodo que lo envia
                    cliente.Send(msg); //Envia todo lo que esta en la variable msg
                }
                catch (Exception)
                {
                    //Lanza un error al enviar
                    lblError.Text = "¡Error, no se pudo enviar el codigo!";
                }
            }
        }

    }
}
