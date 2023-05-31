using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Pintacars_Express.FrmInicio_Sesion;

namespace Pintacars_Express
{
    public partial class FrmCotizacion_Inicial : Form
    {
        CN_Clientes oCN_Clientes = new CN_Clientes();
        CN_Cotizacion_Inicial oCN_Cotizacion_Inicial = new CN_Cotizacion_Inicial();
        CN_Inspeccion_Inicial oCN_Inspeccion_Inicial = new CN_Inspeccion_Inicial();
        CN_Tipo_Documento oCN_Tipo_Documento = new CN_Tipo_Documento();

        int numerocotizacioninicial = 0;
        float valortotal = 0;
        string idvendedor = string.Empty;

        static public class CotizacionInicialGlobal //Se crea una variable global
        {
            static public string CotizacionInicial { get; set; } = string.Empty;
        }


        public FrmCotizacion_Inicial()
        {
            InitializeComponent();
        }


        private void FrmCotizacion_Inicial_Load(object sender, EventArgs e)
        {
            creacion_Dgv_Servicios();
            BuscarNumeroCotizacionInicial();
            MostrarTipoDocumento();
            TraerUsuario();
            BtnTerminar_Cotizacion_Inicial.Enabled = false;
        }


        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form panel_principal = new FrmPanel_Principal();
            panel_principal.Show();
            Hide();
        }
        private void nuevaOrdendeTrabajoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form orden_trabajo = new FrmOrden_Trabajo();
            orden_trabajo.Show();
            Hide();
        }

        private void nuevaCostosGeneralesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form costos_generales = new FrmCostos_Generales();
            costos_generales.Show();
            Hide();
        }

        private void inicioSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form inicio_sesion = new FrmInicio_Sesion();
            inicio_sesion.Show();
            Hide();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        #region Mis métodos



        private void creacion_Dgv_Servicios()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("CANTIDAD");
            dt.Columns.Add("DESCRIPCÍON");
            dt.Columns.Add("Vr. TOTAL");

            DgvServicios.DataSource = dt;

            DgvServicios.RowHeadersVisible = false;
        }


        //Consultar


        private void BuscarNumeroCotizacionInicial() 
        {
            if (oCN_Cotizacion_Inicial.BuscarNumeroCotizacionInicial() != " ")
            {
                numerocotizacioninicial = Convert.ToInt32(oCN_Cotizacion_Inicial.BuscarNumeroCotizacionInicial()) + 1;
                LblNumero_Cotizacion_Inicial.Text = "N° " + numerocotizacioninicial.ToString();
            }
            else
                numerocotizacioninicial = 1;
                LblNumero_Cotizacion_Inicial.Text = "N° " + numerocotizacioninicial.ToString();
        }


        private void TraerUsuario()
        {
            TxtVendedor.Text = UsuarioGlobal.Usuario.Rows[0][1].ToString();
            idvendedor = UsuarioGlobal.Usuario.Rows[0][0].ToString();
        }


        //private int BuscarCodigoInspeccionInicial()
        //{
        //    int codinspeccioninicial = 0;
        //    if (oCN_Inspeccion_Inicial.BuscarCodigoInspeccionInicial() != " ")
        //    {
        //        codinspeccioninicial = Convert.ToInt32(oCN_Inspeccion_Inicial.BuscarCodigoInspeccionInicial());
        //        return codinspeccioninicial;
        //    }
        //    else
        //        codinspeccioninicial = 1;
        //    return codinspeccioninicial;
        //}


        private void MostrarTipoDocumento() 
        {
            CboTipo_Documento.DataSource = oCN_Tipo_Documento.MostrarTipoDocumento();
            CboTipo_Documento.DisplayMember = "Nombre";
            CboTipo_Documento.ValueMember = "Cod";
            CboTipo_Documento.SelectedIndex = -1;
        }


        //Limpiar


        private void Limpiar()
        {
            TxtCliente.Clear();
            TxtDireccion.Clear();
            CboTipo_Documento.SelectedIndex = -1;
            TxtNit.Clear();
            TxtTelefono.Clear();
            TxtCelular.Clear();
            TxtMatricula.Clear();
            TxtCorreo.Clear();
            TxtObservaciones.Clear();
            TxtValor_Total.Clear();
            TxtVendedor.Clear();
            DataTable dt = (DataTable)DgvServicios.DataSource;
            dt.Clear();
        }


        #endregion


        //Confirmar


        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            valortotal = 0;
            foreach (DataGridViewRow filas in DgvServicios.Rows)
            {
                if (filas.Cells["CANTIDAD"].Value.ToString() == null ||
                    filas.Cells["DESCRIPCÍON"].Value.ToString() == null ||
                    filas.Cells["Vr. TOTAL"].Value.ToString() == null)
                {
                    break;
                }
                else
                {
                    valortotal += float.Parse(filas.Cells["Vr. TOTAL"].Value.ToString()) * float.Parse(filas.Cells["CANTIDAD"].Value.ToString());
                    DgvServicios.AllowUserToAddRows = false;
                }
            }
            TxtValor_Total.Text = valortotal.ToString();
            BtnTerminar_Cotizacion_Inicial.Enabled = true;
            DgvServicios.AllowUserToAddRows = true;
        }


        //Insertar


        private void BtnTerminar_Cotizacion_Inicial_Click(object sender, EventArgs e)
        {
            CE_Clientes cliente = new CE_Clientes();
            CE_Cotizacion_Inicial cotizacion_inicial = new CE_Cotizacion_Inicial();
            CE_Inspeccion_Inicial inspeccion_inicial = new CE_Inspeccion_Inicial();


            cliente.Nombre = TxtCliente.Text;
            cliente.Direccion = TxtDireccion.Text;
            cliente.Cod_Tipo_Doc = Convert.ToInt32(CboTipo_Documento.SelectedValue);
            cliente.D_I = TxtNit.Text;
            cliente.Telefono = TxtTelefono.Text;
            cliente.Celular = TxtCelular.Text;
            cliente.Correo = TxtCorreo.Text;

            cotizacion_inicial.Fecha_Llegada = DtpFecha_Llegada.Value;
            cotizacion_inicial.Observaciones = TxtObservaciones.Text;
            cotizacion_inicial.Costo_Total = float.Parse(TxtValor_Total.Text);
            cotizacion_inicial.D_I_Usuario = idvendedor;
            cotizacion_inicial.D_I_Cliente = TxtNit.Text;
            cotizacion_inicial.Matricula_Vehiculo = TxtMatricula.Text;

            //inspeccion_inicial.Fecha_Llegada = DtpFecha_Llegada.Value;

            oCN_Clientes.InsertarCliente(cliente);
            oCN_Cotizacion_Inicial.InsertarCotizacionInicial(cotizacion_inicial);

            CotizacionInicialGlobal.CotizacionInicial = numerocotizacioninicial.ToString();

            foreach (DataGridViewRow filas in DgvServicios.Rows)
            {
                if (filas.Cells["CANTIDAD"].Value.ToString() == null ||
                    filas.Cells["DESCRIPCÍON"].Value.ToString() == null ||
                    filas.Cells["Vr. TOTAL"].Value.ToString() == null)
                {
                    break;
                }
                else
                {
                    inspeccion_inicial.Cantidad = float.Parse(filas.Cells["CANTIDAD"].Value.ToString());
                    inspeccion_inicial.Descripcion = filas.Cells["DESCRIPCÍON"].Value.ToString();
                    inspeccion_inicial.Costos_Unitarios = float.Parse(filas.Cells["Vr. TOTAL"].Value.ToString());
                    inspeccion_inicial.cod_cotizacion_inicial = numerocotizacioninicial;
                    //cotizacion_inspeccioninicial.cod_inspeccion_inicial = BuscarCodigoInspeccionInicial();
                    oCN_Inspeccion_Inicial.InsertarInspeccionInicial(inspeccion_inicial);
                    DgvServicios.AllowUserToAddRows = false;
                    valortotal += float.Parse(filas.Cells["Vr. TOTAL"].Value.ToString());
                }
            }
            BuscarNumeroCotizacionInicial();
            Limpiar();
            DgvServicios.AllowUserToAddRows = true;
            MessageBox.Show("Cotización Terminada");
            BtnTerminar_Cotizacion_Inicial.Enabled = false;
        }
    }
}
