using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySleepy
{
    public partial class ArticulosTotales : Form
    {
        private ConnectDB conexion;
        private int p1;
        private ArticulosForm articulosForm;
        private int idUsuario;
        private int p2;
        //patron singleton
        private static ArticulosTotales instance;

        internal static ArticulosTotales Instance(ConnectDB conexion, int p1, ArticulosForm articulosForm, int idUsuario, int p2)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new ArticulosTotales(conexion,p1,articulosForm,idUsuario,p2);
            }
            return instance;
        }

        public ArticulosTotales()
        {
            InitializeComponent();
        }

        public ArticulosTotales(ConnectDB conexion, int p1, ArticulosForm articulosForm, int idUsuario, int p2)
        {
            InitializeComponent();
            this.conexion = conexion;
            this.p1 = p1;
            this.articulosForm = articulosForm;
            this.idUsuario = idUsuario;
            this.p2 = p2;
        }

        
        private void botonSimples_Click(object sender, EventArgs e)
        {
            AddNuevoArticulo add = new AddNuevoArticulo(conexion, 0, this, idUsuario, 0);
            add.ShowDialog();
            this.Close();
        }

        private void botonSalir_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void botonCompuestos_Click(object sender, EventArgs e)
        {
            ArticulosCompuestos artcomp = ArticulosCompuestos.Instance(conexion,idUsuario);
            artcomp.Show();
            this.Close();
        }

        private void ArticulosTotales_Load(object sender, EventArgs e)
        {

        }
    }
}
