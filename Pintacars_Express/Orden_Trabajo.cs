using CapaEntidades.Orden_de_Trabajo;
using CapaEntidades;
using CapaNegocio;
using CapaNegocio.Orden_de_Trabajo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Pintacars_Express.FrmInicio_Sesion;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pintacars_Express
{
    public partial class FrmOrden_Trabajo : Form
    {
        CN_Partes_Inspeccion oCN_Partes_Inspeccion = new CN_Partes_Inspeccion();
        CN_Tipo_Pago oCN_Tipo_Pago = new CN_Tipo_Pago();
        CN_Marca oCN_Marca = new CN_Marca();
        CN_Vehiculos oCN_Vehiculos = new CN_Vehiculos();
        CN_Orden_Trabajo oCN_Orden_Trabajo = new CN_Orden_Trabajo();
        CN_Orden_Trabajo_Inspeccion oCN_Orden_Trabajo_Inspeccion = new CN_Orden_Trabajo_Inspeccion();
        CN_Cotizacion_Inicial oCN_Cotizacion_Inicial = new CN_Cotizacion_Inicial();
        CN_Pagos oCN_Pagos = new CN_Pagos();
        CN_Clientes oCN_Clientes = new CN_Clientes();

        int numeroordentrabajo = 0;
        string numerocotizacioninicial = FrmCotizacion_Inicial.CotizacionInicialGlobal.CotizacionInicial;
        string idvendedor = string.Empty;
        float totalpagos = 0;
        string doccliente = string.Empty;

        static public class OrdenTrabajoGlobal //Se crea una variable global
        {
            static public string OrdenTrabajo { get; set; } = string.Empty;
        }

        public FrmOrden_Trabajo()
        {
            InitializeComponent();
        }


        private void FrmOrden_Trabajo_Load(object sender, EventArgs e)
        {
            BuscarNumeroOrdenTrabajo();
            MostrarPartesInspecion_CE();
            MostrarPartesInspecion_IC();
            MostrarPartesInspecion_IB();
            MostrarPartesInspecion_IV();
            MostrarTipoPago();
            MostrarMarcas();
            TraerUsuario();
            MostrarCliente();
            BtnTerminar.Enabled = false;
            LblCotizacion_Inicial.Text = "N° " + FrmCotizacion_Inicial.CotizacionInicialGlobal.CotizacionInicial;
            CboCotizacion_Inicial.Text = FrmCotizacion_Inicial.CotizacionInicialGlobal.CotizacionInicial;
            MostrarCotizacionInicial();

            //creacion_Dgv_ConjuntoExt();

            //creacion_DgvAbonos();
        }


        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form panel_principal = new FrmPanel_Principal();
            panel_principal.Show();
            Hide();
        }

        private void nuevaCotizaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Corizacion_Inicial = new FrmCotizacion_Inicial();
            Corizacion_Inicial.Show();
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


        //Consultar


        private void BuscarNumeroOrdenTrabajo()
        {
            if (oCN_Orden_Trabajo.BuscarNumeroOrdenTrabajo() != " ")
            {
                numeroordentrabajo = Convert.ToInt32(oCN_Orden_Trabajo.BuscarNumeroOrdenTrabajo()) + 1;
                LblNumero_Orden_Trabajo.Text = "N° " + numeroordentrabajo.ToString();
            }
            else
                numeroordentrabajo = 1;
            LblNumero_Orden_Trabajo.Text = "N° " + numeroordentrabajo.ToString();
        }


        private void TraerUsuario()
        {
            TxtRecibe.Text = UsuarioGlobal.Usuario.Rows[0][1].ToString();
            idvendedor = UsuarioGlobal.Usuario.Rows[0][0].ToString();
        }


        private void MostrarPartesInspecion_CE()
        {
            DgvConjuntoExt.DataSource = oCN_Partes_Inspeccion.MostrarPartesInspecion_CE();
            DgvConjuntoExt.Columns.Add("OBSERVACIONES", "OBSERVACIONES");
            DgvConjuntoExt.Columns["cod"].Visible = false;
            //DgvConjuntoExt.Columns["CONJUNTO EXTERIOR"].ReadOnly = true;
        }


        private void MostrarPartesInspecion_IC()
        {
            DgvInteriorCapot.DataSource = oCN_Partes_Inspeccion.MostrarPartesInspecion_IC();
            DgvInteriorCapot.Columns.Add("OBSERVACIONES", "OBSERVACIONES");
            DgvInteriorCapot.Columns["cod"].Visible = false;
            //DgvInteriorCapot.Columns["INTERIOR CAPOT"].ReadOnly = true;
        }


        private void MostrarPartesInspecion_IB()
        {
            DgvInteriorBaul.DataSource = oCN_Partes_Inspeccion.MostrarPartesInspecion_IB();
            DgvInteriorBaul.Columns.Add("OBSERVACIONES", "OBSERVACIONES");
            DgvInteriorBaul.Columns["cod"].Visible = false;
            //DgvInteriorBaul.Columns["INTERIOR BAUL"].ReadOnly = true;
        }


        private void MostrarPartesInspecion_IV()
        {
            DgvInteriorVehiculo.DataSource = oCN_Partes_Inspeccion.MostrarPartesInspecion_IV();
            DgvInteriorVehiculo.Columns.Add("ESTADO", "ESTADO");
            DgvInteriorVehiculo.Columns["cod"].Visible = false;
            //DgvInteriorVehiculo.Columns["INTERIOR DEL VEHICULO"].ReadOnly = true;
        }


        private void MostrarTipoPago()
        {
            DataGridViewComboBoxColumn columna = new DataGridViewComboBoxColumn();

            DgvAbonos.Columns.Add("ABONO","ABONO");

            columna.HeaderText = "TIPO DE PAGO";
            columna.Name = "TIPO_PAGO";
            columna.DataSource = oCN_Tipo_Pago.MostrarTipoPago();
            columna.DisplayMember= "Nombre";
            columna.ValueMember = "Cod";
            DgvAbonos.Columns.Add(columna);
        }


        private void MostrarMarcas()
        {
            CboMarca.DataSource = oCN_Marca.MostrarMarcas();
            CboMarca.DisplayMember = "Nombre";
            CboMarca.ValueMember = "Cod";
            CboMarca.SelectedIndex = -1;
        }


        private void MostrarCotizacionInicial()
        {
            CboCotizacion_Inicial.DataSource = oCN_Cotizacion_Inicial.MostrarCotizacionInicial();
            CboCotizacion_Inicial.DisplayMember = "Cod";
            CboCotizacion_Inicial.ValueMember = "Cod";
            CboCotizacion_Inicial.SelectedIndex = -1;
        }


        private void MostrarCliente()
        {
            DataTable tablacliente = new DataTable();
            CE_Cotizacion_Inicial cotizacion_inicial = new CE_Cotizacion_Inicial();

            cotizacion_inicial.Cod = Convert.ToInt32(numerocotizacioninicial);

            tablacliente = oCN_Clientes.MostrarCliente(cotizacion_inicial);

            doccliente = tablacliente.Rows[0][0].ToString(); 
            TxtSeñores.Text = tablacliente.Rows[0][1].ToString();
            TxtDireccion.Text = tablacliente.Rows[0][2].ToString();
            TxtCel_Tel.Text = tablacliente.Rows[0][3].ToString();
            TxtPlaca.Text = tablacliente.Rows[0][4].ToString();
        }


        #endregion

        private void BtnTerminar_Click_1(object sender, EventArgs e)
        {
            CE_Vehiculos vehiculo = new CE_Vehiculos();
            CE_Orden_Trabajo orden_trabajo = new CE_Orden_Trabajo();
            CE_Orden_Trabajo_Inspeccion orden_trabajo_inspeccion = new CE_Orden_Trabajo_Inspeccion();
            CE_Pagos pago = new CE_Pagos();

            vehiculo.Matricula = TxtPlaca.Text;
            vehiculo.Modelo = TxtModelo.Text;
            vehiculo.Color = TxtColor.Text;
            vehiculo.Año = TxtAño.Text;
            vehiculo.Cod_Marca = Convert.ToInt32(CboMarca.SelectedValue);
            oCN_Vehiculos.InsertarVehiculo(vehiculo);

            orden_trabajo.Clave = TxtClave.Text;
            orden_trabajo.Kilometraje = TxtKilometraje.Text;
            orden_trabajo.Fecha_Llegada = DtpFecha_Recibido.Value;
            orden_trabajo.Fecha_Entrega = DtpFecha_Entrega.Value;
            foreach (DataGridViewRow filas in DgvRelacionTrabajo.Rows)
            {
                orden_trabajo.Descripcion = filas.Cells["RELACION_TRABAJO"].Value.ToString();
                DgvRelacionTrabajo.AllowUserToAddRows = false;
            }
            orden_trabajo.entrega = TxtEntrega.Text;
            orden_trabajo.D_I_Usuario = idvendedor.ToString();
            orden_trabajo.mano_obra = float.Parse(TxtMano_Obra.Text);
            orden_trabajo.repuestos = float.Parse(TxtRepuestos.Text);
            orden_trabajo.otros = float.Parse(TxtOtros.Text);
            orden_trabajo.Precio = float.Parse(TxtTotal.Text);
            orden_trabajo.saldo = float.Parse(TxtSaldo.Text);
            orden_trabajo.D_I_Cliente = doccliente;
            orden_trabajo.Matricula_Vehiculo = TxtPlaca.Text;
            orden_trabajo.Cod_Cotizacion_Inicial = Convert.ToInt32(numerocotizacioninicial);
            DgvRelacionTrabajo.AllowUserToAddRows = true;
            oCN_Orden_Trabajo.InsertarOrdenTrabajo(orden_trabajo);

            OrdenTrabajoGlobal.OrdenTrabajo = numeroordentrabajo.ToString();

            foreach (DataGridViewRow filas in DgvConjuntoExt.Rows)
            {
                orden_trabajo_inspeccion.observaciones = filas.Cells["OBSERVACIONES"].Value.ToString();
                orden_trabajo_inspeccion.cod_partes_inspeccion = Convert.ToInt32(filas.Cells["cod"].Value.ToString());
                orden_trabajo_inspeccion.cod_Orden_Trabajo = numeroordentrabajo;
                oCN_Orden_Trabajo_Inspeccion.InsertarOrdenTrabajoInspeccion(orden_trabajo_inspeccion);
                DgvConjuntoExt.AllowUserToAddRows = false;
            }

            foreach (DataGridViewRow filas in DgvInteriorCapot.Rows)
            {
                orden_trabajo_inspeccion.observaciones = filas.Cells["OBSERVACIONES"].Value.ToString();
                orden_trabajo_inspeccion.cod_partes_inspeccion = Convert.ToInt32(filas.Cells["Cod"].Value.ToString());
                orden_trabajo_inspeccion.cod_Orden_Trabajo = numeroordentrabajo;
                oCN_Orden_Trabajo_Inspeccion.InsertarOrdenTrabajoInspeccion(orden_trabajo_inspeccion);
                DgvInteriorCapot.AllowUserToAddRows = false;
            }

            foreach (DataGridViewRow filas in DgvInteriorVehiculo.Rows)
            {
                orden_trabajo_inspeccion.cod_partes_inspeccion = Convert.ToInt32(filas.Cells["Cod"].Value.ToString());
                orden_trabajo_inspeccion.observaciones = filas.Cells["ESTADO"].Value.ToString();
                orden_trabajo_inspeccion.cod_Orden_Trabajo = numeroordentrabajo;
                oCN_Orden_Trabajo_Inspeccion.InsertarOrdenTrabajoInspeccion(orden_trabajo_inspeccion);
                DgvInteriorVehiculo.AllowUserToAddRows = false;
            }

            foreach (DataGridViewRow filas in DgvInteriorBaul.Rows)
            {
                orden_trabajo_inspeccion.cod_partes_inspeccion = Convert.ToInt32(filas.Cells["Cod"].Value.ToString());
                orden_trabajo_inspeccion.observaciones = filas.Cells["OBSERVACIONES"].Value.ToString();
                orden_trabajo_inspeccion.cod_Orden_Trabajo = numeroordentrabajo;
                oCN_Orden_Trabajo_Inspeccion.InsertarOrdenTrabajoInspeccion(orden_trabajo_inspeccion);
                DgvInteriorBaul.AllowUserToAddRows = false;
            }

            foreach (DataGridViewRow filas in DgvAbonos.Rows)
            {
                pago.Monto = float.Parse(filas.Cells["ABONO"].Value.ToString());
                pago.Fecha = DateTime.Now;
                pago.Cod_Tipo_Pago = Convert.ToInt32(filas.Cells["TIPO_PAGO"].Value.ToString());
                pago.Cod_Orden_Trabajo = numeroordentrabajo;
                oCN_Pagos.InsertarPago(pago);
                DgvAbonos.AllowUserToAddRows = false;
            }

            DgvConjuntoExt.AllowUserToAddRows = true;
            DgvInteriorCapot.AllowUserToAddRows = false;
            DgvInteriorVehiculo.AllowUserToAddRows = false;
            DgvInteriorBaul.AllowUserToAddRows = false;
            DgvAbonos.AllowUserToAddRows = false;

            BuscarNumeroOrdenTrabajo();
            BtnTerminar.Enabled = false;
            numerocotizacioninicial = string.Empty;
            LblCotizacion_Inicial.Text = "N° ";
            MostrarCotizacionInicial();
            MessageBox.Show("Orden de Trabajo Terminada");
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            TxtTotal.Text = (float.Parse(TxtMano_Obra.Text) + float.Parse(TxtRepuestos.Text) + float.Parse(TxtOtros.Text)).ToString();
            BtnTerminar.Enabled = true;
        }

        private void BtnConfirmar_Pago_Click(object sender, EventArgs e)
        {
            totalpagos = 0;
            foreach (DataGridViewRow filas in DgvAbonos.Rows)
            {
                totalpagos += float.Parse(filas.Cells["ABONO"].Value.ToString());
                DgvAbonos.AllowUserToAddRows = false;
            }
            DgvAbonos.AllowUserToAddRows = true;
            TxtSaldo.Text = (float.Parse(TxtTotal.Text) - totalpagos).ToString();
        }

        private void CboCotizacion_Inicial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CboCotizacion_Inicial.Text != numerocotizacioninicial & CboCotizacion_Inicial.SelectedIndex >0)
            {
                LblCotizacion_Inicial.Text = "N° " + CboCotizacion_Inicial.Text;
                numerocotizacioninicial = CboCotizacion_Inicial.Text;
                MostrarCliente(); 
            }
            //else
            //{
            //    CboCotizacion_Inicial.Text = FrmCotizacion_Inicial.CotizacionInicialGlobal.CotizacionInicial;
            //}
        }


        private void BtnCambiar_Click(object sender, EventArgs e)
        {
            CboCotizacion_Inicial.Enabled = true;
        }
    }
}
