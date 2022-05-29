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
    public partial class FrmVentas : Form
    {
        public FrmVentas()
        {
            InitializeComponent();
        }
        //Conexion
        //SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-2ECAU7F;Initial Catalog=BDfinal;Integrated Security=True");
        SqlConnection conexion = new SqlConnection("Data Source=segundo150\\segundo150;Initial Catalog=DAM_AnderSabariego;Integrated Security=True");

        //Comandos
        SqlCommand cmdCargarServicios = new SqlCommand();
        SqlCommand cmdCargarDNI = new SqlCommand();
        SqlCommand cmdValor = new SqlCommand();
        SqlCommand cmdAltaCab = new SqlCommand();
        SqlCommand cmdMax = new SqlCommand();
        SqlCommand cmdMaxLin = new SqlCommand();
        SqlCommand cmdCargarHistorial = new SqlCommand();
        SqlCommand cmdAltaLin = new SqlCommand();

        //Parametros
        SqlParameter prmServicio = new SqlParameter();
        SqlParameter prmID = new SqlParameter();
        SqlParameter prmDNI = new SqlParameter();
        SqlParameter prmFecha = new SqlParameter();
        SqlParameter prmCosteTotal = new SqlParameter();
        SqlParameter prmIDLinea = new SqlParameter();
        SqlParameter prmIDVenta = new SqlParameter();
        SqlParameter prmServicioLin = new SqlParameter();
        SqlParameter prmCosteServicioLin = new SqlParameter();

        //DataReader
        SqlDataReader dreader;
        SqlDataReader dreaderseleccionar;

        //DataTable
        DataTable dtCmb = new DataTable();
        DataTable dtDNI = new DataTable();

        //Adaptadores
        SqlDataAdapter adaptador = new SqlDataAdapter();
        SqlDataAdapter adapDni = new SqlDataAdapter();

        //Variables
        int PosX, PosY;
        Boolean Arrastra;
        String valor;
        float CosteTotal;
        int idVenta;
        public int idEliminar;
        public string dniHistorico;
        int i = 1;

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            //Limpio la listView
            lsServicios.Items.Clear();

            //Le doy el valor 0 a coste total;
            CosteTotal = 0;

            //Defino el comando 
            cmdCargarHistorial.CommandType = CommandType.Text;
            cmdCargarHistorial.CommandText = "select idVenta,DNI,Fecha from CabVenta";
            cmdCargarHistorial.Connection = conexion;

            //Defino el comando 
            cmdMax.CommandType = CommandType.Text;
            cmdMax.CommandText = "select max(idVenta) from CabVenta";
            cmdMax.Connection = conexion;

            //Defino el comando 
            cmdMaxLin.CommandType = CommandType.Text;
            cmdMaxLin.CommandText = "select max(idVenta) from CabVenta";
            cmdMaxLin.Connection = conexion;

            //Defino el comando para cargar el combo
            cmdCargarServicios.CommandType = CommandType.Text;
            cmdCargarServicios.CommandText = "Select Servicio,CosteServicio from Servicios";
            cmdCargarServicios.Connection = conexion;

            //Defino el comando para cargar el combo
            cmdCargarDNI.CommandType = CommandType.Text;
            cmdCargarDNI.CommandText = "Select DNIpropietario from Reparaciones";
            cmdCargarDNI.Connection = conexion;

            //Defino el comando para cargar el combo
            cmdValor.CommandType = CommandType.Text;
            cmdValor.CommandText = "Select CosteServicio from Servicios where Servicio=@prmServicio";
            cmdValor.Connection = conexion;

            //Defino el parametro
            prmServicio.ParameterName = "@prmServicio";
            prmServicio.DbType = DbType.String;
            prmServicio.Size = 50;
            prmServicio.Direction = ParameterDirection.Input;

            //Añado el parametro
            cmdValor.Parameters.Add(prmServicio);

            //Le asigno el comando al adaptador
            adapDni.SelectCommand = cmdCargarDNI;

            //Cargo la tabla mediante el metodo fill
            adapDni.Fill(dtDNI);

            //Monto en ccombo box servicios
            cmbDNI.DataSource = dtDNI;
            cmbDNI.DisplayMember = "DNIpropietario";
            cmbDNI.ValueMember = "DNIpropietario";

            //Le asigno el comando al adaptador
            adaptador.SelectCommand = cmdCargarServicios;

            //Cargo la tabla mediante el metodo fill
            adaptador.Fill(dtCmb);

            //Monto en ccombo box servicios
            cmbServicios.DataSource = dtCmb;
            cmbServicios.DisplayMember = "Servicio";
            cmbServicios.ValueMember = "Servicio";

            //Defino el comando para dar de alta la cabecera
            cmdAltaCab.CommandType = CommandType.Text;
            cmdAltaCab.CommandText = "insert into CabVenta (idVenta,DNI,Fecha,CosteTotal) values (@prmID,@prmDNI,@prmFecha,@prmCosteTotal)";
            cmdAltaCab.Connection = conexion;

            //Defino el parametro para el ID
            prmID.ParameterName = "@prmID";
            prmID.DbType = DbType.Int32;
            prmID.Direction = ParameterDirection.Input;

            //Defino el parametro para el DNI
            prmDNI.ParameterName = "@prmDNI";
            prmDNI.DbType = DbType.String;
            prmDNI.Size = 9;
            prmDNI.Direction = ParameterDirection.Input;

            //Defino el parametro para la fecha
            prmFecha.ParameterName = "@prmFecha";
            prmFecha.DbType = DbType.Date;
            prmFecha.Direction = ParameterDirection.Input;

            //Defino el parametro para el tipo
            prmCosteTotal.ParameterName = "@prmCosteTotal";
            prmCosteTotal.DbType = DbType.Int32;
            prmCosteTotal.Direction = ParameterDirection.Input;

            //Añado los parametros al comando
            cmdAltaCab.Parameters.Add(prmID);
            cmdAltaCab.Parameters.Add(prmDNI);
            cmdAltaCab.Parameters.Add(prmFecha);
            cmdAltaCab.Parameters.Add(prmCosteTotal);

            //Defino el comando cmdCargar
            cmdAltaLin.CommandType = CommandType.Text;
            cmdAltaLin.CommandText = "insert into LinVenta (idLineasVenta,idVenta,Servicio,CosteServicio) values (@prmIDLinea,@prmIDVenta,@prmServicioLin,@prmCosteServicioLin)";
            cmdAltaLin.Connection = conexion;

            //Defino el parametro 
            prmIDLinea.ParameterName = "@prmIDLinea";
            prmIDLinea.DbType = DbType.Int32;
            prmIDLinea.Direction = ParameterDirection.Input;

            //Defino el parametro 
            prmIDVenta.ParameterName = "@prmIDVenta";
            prmIDVenta.DbType = DbType.Int32;
            prmIDVenta.Direction = ParameterDirection.Input;

            //Defino el parametro
            prmServicioLin.ParameterName = "@prmServicioLin";
            prmServicioLin.DbType = DbType.String;
            prmServicioLin.Size = 50;
            prmServicioLin.Direction = ParameterDirection.Input;

            //Defino el parametro 
            prmCosteServicioLin.ParameterName = "@prmCosteServicioLin";
            prmCosteServicioLin.DbType = DbType.Int32;
            prmCosteServicioLin.Direction = ParameterDirection.Input;

            //Agrego los parametros al comando de AltasLin
            cmdAltaLin.Parameters.Add(prmIDLinea);
            cmdAltaLin.Parameters.Add(prmIDVenta);
            cmdAltaLin.Parameters.Add(prmServicioLin);
            cmdAltaLin.Parameters.Add(prmCosteServicioLin);

            //Ejecuto la funcion cargar
            cargar();
        }

        private void btnAnadir_Click(object sender, EventArgs e)
        {
            //Abro la conexion
            conexion.Open();

            //Le asigno el valor correspondiente al parametro del comando
            cmdValor.Parameters[0].Value = cmbServicios.SelectedValue;

            //Guardo lo devuelto por el comando cmdValor en una variable
            valor = cmdValor.ExecuteScalar().ToString();

            //Creo un nuevo objeto de tipo ListViewItem
            ListViewItem li = new ListViewItem();

            //Añado los items a la ListView
            li=lsServicios.Items.Add(cmbServicios.SelectedValue.ToString());
            li.SubItems.Add(valor.ToString());

            //Voy sumando valores en la variable CosteTotal
            CosteTotal+=float.Parse(valor);

            //Muestro el coste total de la venta en lblCosteTotal
            lblCosteTotal.Text = CosteTotal.ToString() + "€";

            //Cierro la conexion
            conexion.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            //Limpio los objetos de la lsServicios
            lsServicios.Items.Clear();

            //Vacio el valor de coste total
            lblCosteTotal.Text = "";

            //Le asigno el valor 0 a CosteTotal
            CosteTotal = 0;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Cierro el programa
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            //Minimizo el formulario
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            //Creo un nuevo objeto de tipo FrmGestion
            FrmGestion inicio = new FrmGestion();

            //Muestro el formulario
            inicio.Show();

            //Cierro este formulario
            this.Close();
            
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

        private void FrmVentas_MouseDown(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando hago click en cuerpo

            //Asigno el valor de la variable PosX con el valor del MouseEventArg
            PosX = e.X;

            //Asigno el valor de la variable PosY con el valor del MouseEventArg
            PosY = e.Y;

            //Le asigno a la variable Arrasta el valor True
            Arrastra = true;
        }

        private void FrmVentas_MouseMove(object sender, MouseEventArgs e)
        {
            //Evento que se ejecuta cuando muevo por el cuerpo

            //Si el valor de la variable Arrastra es igual a True
            if (Arrastra == true)
            {
                //Le resto a las posiciones actuales del formulario, los valores de las variables PosX y PosY
                this.SetDesktopLocation(MousePosition.X - PosX, MousePosition.Y - PosY);
            }
        }

        private void FrmVentas_MouseUp(object sender, MouseEventArgs e)
        {
            //Le asigno a la variable Arrasta el valor False
            Arrastra = false;
        }

        private void lsHistorial_ItemActivate(object sender, EventArgs e)
        {
            //Le asigno el valor seleccionado a la variable idEliminar
            idEliminar = int.Parse(lsHistorial.SelectedItems[0].Text);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Si el idEliminar es igual a = o a vacio
            if (idEliminar==0 || idEliminar.ToString()=="")
            {
                //Creo un objeto de tipo msgBoxDobleClick
                msgBoxDobleClick error = new msgBoxDobleClick();

                //Muestro el formulario en forma de dialogo
                error.ShowDialog();
            }
            else
            {
                //Creo un nuevo objeto de tipo FrmEliminarVenta
                FrmEliminarVenta eliminar = new FrmEliminarVenta();

                //Le añado el el formulario del que es dueño este formulario
                AddOwnedForm(eliminar);

                //Le asigno a la variable ideliminarventa el valor de idEliminar
                eliminar.ideliminarventa = idEliminar;

                //Muestro el formulario de eliminar como si fuera un dialogo
                eliminar.ShowDialog();
            }
            
        }

        private void btnCerrarVenta_Click(object sender, EventArgs e)
        {
            //Abro la conexion
            conexion.Open();

            //Si cmdMax me devuelve un valor nulo o un vacio
            if (cmdMax.ExecuteScalar() == null || cmdMax.ExecuteScalar().ToString() == "")
            {
                //Le doy a idVenta el valor uno
                idVenta = 1;
            }
            else
            {
                //Le doy el valor devuelto por cmdMax a la variable idVenta
                idVenta = int.Parse(cmdMax.ExecuteScalar().ToString())+1;
            }

            //Asigno los valores correspondientes a los parametros del comando
            cmdAltaCab.Parameters[0].Value = idVenta;
            cmdAltaCab.Parameters[1].Value = cmbDNI.SelectedValue;
            cmdAltaCab.Parameters[2].Value = DateTime.Now;
            cmdAltaCab.Parameters[3].Value = CosteTotal;

            //Si el lsServicios es null
            if (lsServicios.Items==null)
            {
                //Le asigno este valor al lblErro
                lblError.Text = "¡Error, no puedes dejar los servicios en blanco!";

                //Le asigno el color rojo al texto de lblError
                lblError.ForeColor = Color.Red;
            }
            else
            {
                //Ejecuto el comando de alta
                cmdAltaCab.ExecuteNonQuery();

                foreach (ListViewItem it in lsServicios.Items)
                {
                    //Asigno a los parametro el valor correspondiente
                    cmdAltaLin.Parameters[0].Value = i++;
                    cmdAltaLin.Parameters[1].Value = idVenta;
                    cmdAltaLin.Parameters[2].Value = it.Text;

                    //Guardo en la variable de texto el valor del subitem de la listview
                    string texto = it.SubItems[1].Text;

                    //Creo una varaible valor
                    int valor;

                    //Parseo el texto
                    int.TryParse(texto, out valor);

                    //Le asigno el valor correspondiente al parametro del comando cmdAltaLin
                    cmdAltaLin.Parameters[3].Value = valor;

                    //Ejecuto el comando de alta de linea
                    cmdAltaLin.ExecuteNonQuery();
                }
                    
                
                
            }

            //Cerrar la conexion
            conexion.Close();

            //Cargo la tabla
            cargar();
        }

        public void cargar()
        {
            //Abro la conexion
            conexion.Open();

            //Limpio los items de lsHistorial
            lsHistorial.Items.Clear();

            //Le digo al lector que lo que lee es lo que ejecuta del cmdCargarHistorial
            dreader = cmdCargarHistorial.ExecuteReader();

            //Mientras lea
            while (dreader.Read())
            {
                //Creo un objeto de tipo ListViewItem
                ListViewItem li = new ListViewItem();

                //Añado los items a la listview
                li = lsHistorial.Items.Add(dreader[0].ToString());
                li.SubItems.Add(dreader[1].ToString());
                li.SubItems.Add(dreader[2].ToString());
            }
            //Cierro el lector
            dreader.Close();

            //Cierro la conexion
            conexion.Close();
        }
      

    }
}
