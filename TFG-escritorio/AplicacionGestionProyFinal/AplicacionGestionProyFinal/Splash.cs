using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicacionGestionProyFinal
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        //Variables
        int contador = 0;
        private void Splash_Load(object sender, EventArgs e)
        {
            //Inicio el timer
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            //Este codigo se ejecuta cada vez que el timer hace un tick

            //Si el contador es igual a 12
            if (contador == 12)
            {
                //Escondo el formulario
                this.Hide();

                //Creo un objeto de tipo FrmLogin
                FrmLogin login = new FrmLogin();

                //Muestro el formulario login
                login.Show();

                //Paro el timer
                timer.Stop();
            }
            else //Si el contador no es igual a 12
            {
                //Le sumo uno al contador
                contador++;
            }
        }

        
    }
}
