using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pintacars_Express
{
    public partial class FrmInicio_Sesion : Form
    {
        CN_Usuarios oCN_Usuarios = new CN_Usuarios();
        CN_Validaciones validaciones = new CN_Validaciones();

        public FrmInicio_Sesion()
        {
            InitializeComponent();
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            CE_Usuarios usuario = new CE_Usuarios();

            usuario.Correo = TxtCorreo.Text.Trim();
            usuario.Contraseña = validaciones.Encriptacion(TxtContrasena.Text.Trim());

            if (oCN_Usuarios.BuscarUsuario(usuario) == true)
            {
                Form panel_principal = new FrmPanel_Principal();
                panel_principal.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Datos Incorrectos");
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
