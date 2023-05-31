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
using static Pintacars_Express.FrmInicio_Sesion;

namespace Pintacars_Express
{
    public partial class FrmCostos_Generales : Form
    {
        CN_Materiales oCN_Materiales = new CN_Materiales();
        CN_Tipo_Trabajador oCN_Tipo_Trabajador = new CN_Tipo_Trabajador();
        //CN_Marca oCN_Marca = new CN_Marca();   
        CN_Costos_Generales oCN_Costos_Generales = new CN_Costos_Generales();
        CN_Costos_Generales_Materiales oCN_Costos_Generales_Materiales = new CN_Costos_Generales_Materiales();
        CN_Costos_Generales_Trabajadores oCN_Costos_Generales_Trabajadores = new CN_Costos_Generales_Trabajadores();
        CN_Vehiculos oCN_Vehiculos =  new CN_Vehiculos();

        int numerocostosgenerales = 0;
        string numeroordentrabajo = FrmOrden_Trabajo.OrdenTrabajoGlobal.OrdenTrabajo;
        float valortotalmateriales = 0;
        float valortotaltrabajadores = 0;
        string idvendedor = string.Empty;

        public FrmCostos_Generales()
        {
            InitializeComponent();
        }

        private void Orden_Servicio_Load(object sender, EventArgs e)
        {
            BuscarNumeroCostosGenerales();
            MostrarMateriales();
            MostrarTipoTrabajador();
            //MostrarMarcas();
            TraerUsuario();
            MostrarVehiculo();
            BtnTerminar.Enabled = false;
            LblNumero_Orden_Trabajo.Text = "N° " + numeroordentrabajo;
            //creacion_Dgv_Costo_Materiales();
            //creacion_Dgv_Costo_Mano_de_Obra();
        }

        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form panel_principal = new FrmPanel_Principal();
            panel_principal.Show();
            Hide();
        }

        private void iniciarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form inicio_sesion = new FrmInicio_Sesion();
            inicio_sesion.Show();
            Hide();
        }

        private void nuevaCotizaciónInicialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Corizacion_Inicial = new FrmCotizacion_Inicial();
            Corizacion_Inicial.Show();
            Hide();
        }

        private void nuevaOrdendeTrabajoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form orden_trabajo = new FrmOrden_Trabajo();
            orden_trabajo.Show();
            Hide();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        #region Mis Métodos


        //private void creacion_Dgv_Costo_Materiales()
        //{
        //    DataTable dt = new DataTable();

        //    dt.Columns.Add("CANTIDAD");
        //    dt.Columns.Add("DESCRIPCIÓN");
        //    dt.Columns.Add("COSTO");
        //    dt.Columns.Add("FECHA DE COMPRA");

        //    DataRow filas = dt.NewRow();


        //    DgvCosto_Materiales.RowHeadersVisible = false;

        //    DgvCosto_Materiales.DataSource = dt;
        //    filas["DESCRIPCION"] = oCN_Materiales.MostrarMateriales();

        //    dt.Rows.Add(filas);
        //}


        //private void creacion_Dgv_Costo_Mano_de_Obra()
        //{
        //    DataTable dt = new DataTable();

        //    dt.Columns.Add("POSICIONES");
        //    dt.Columns.Add("RESPONSABLES");
        //    dt.Columns.Add("COSTO P/ HORA");
        //    DgvCosto_Mano_Obra.RowHeadersVisible = false;

        //    DgvCosto_Mano_Obra.DataSource = dt;
        //}


        //Consultar


        private void BuscarNumeroCostosGenerales()
        {
            if (oCN_Costos_Generales.BuscarNumeroCostosGenerales() != " ")
            {
                numerocostosgenerales = Convert.ToInt32(oCN_Costos_Generales.BuscarNumeroCostosGenerales()) + 1;
                LblNumero_Costos_Generales.Text = "N° " + numerocostosgenerales.ToString();
            }
            else
            {
                numerocostosgenerales = 1;
                LblNumero_Costos_Generales.Text = "N° " + numerocostosgenerales.ToString();
            }
        }


        private void TraerUsuario()
        {
            TxtRecibe.Text = UsuarioGlobal.Usuario.Rows[0][1].ToString();
            idvendedor = UsuarioGlobal.Usuario.Rows[0][0].ToString();
        }


        private void MostrarMateriales()
        {
            DgvCosto_Materiales.Columns.Add("CANTIDAD", "CANTIDAD");
            DgvCosto_Materiales.DataSource = oCN_Materiales.MostrarMateriales();
            DgvCosto_Materiales.Columns.Add("COSTO","COSTO");
            DgvCosto_Materiales.Columns.Add("FECHA_COMPRA", "FECHA DE COMPRA");
            DgvCosto_Materiales.Columns["Cod"].Visible = false;
            //DgvCosto_Materiales.Columns["DESCRIPCIÓN"].ReadOnly = true;
        }


        private void MostrarTipoTrabajador()
        {
            DgvCosto_Mano_Obra.DataSource = oCN_Tipo_Trabajador.MostrarTipoTrabajador();
            DgvCosto_Mano_Obra.Columns.Add("RESPONSABLES", "RESPONSABLES");
            DgvCosto_Mano_Obra.Columns.Add("COSTOPHORA", "COSTO P/ HORA");
            DgvCosto_Mano_Obra.Columns["Cod"].Visible = false;
            //DgvCosto_Mano_Obra.Columns["POSICIONES"].ReadOnly = true;
        }


        //private void MostrarMarcas()
        //{
        //    CboMarca.DataSource = oCN_Marca.MostrarMarcas();
        //    CboMarca.DisplayMember = "Nombre";
        //    CboMarca.ValueMember = "Cod";
        //    CboMarca.SelectedIndex = -1;
        //}


        private void MostrarVehiculo()
        {
            DataTable tablavehiculo = new DataTable();
            CE_Orden_Trabajo orden_trabajo = new CE_Orden_Trabajo();

            orden_trabajo.Cod = numeroordentrabajo;

            tablavehiculo = oCN_Vehiculos.MostrarVehiculo(orden_trabajo);

            TxtMatricula.Text = tablavehiculo.Rows[0][0].ToString();
            Txt_Marca.Text = tablavehiculo.Rows[0][1].ToString();
        }


        #endregion


        //Confirmar


        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            valortotalmateriales = 0;
            valortotaltrabajadores = 0;

            foreach (DataGridViewRow filas in DgvCosto_Materiales.Rows)
            {
                if (filas.Cells["CANTIDAD"].Value.ToString() == null ||
                    filas.Cells["COSTO"].Value.ToString() == null ||
                    filas.Cells["FECHA_COMPRA"].Value.ToString() == null ||
                    filas.Cells["Cod"].Value.ToString() == null ||
                    filas.Cells["DESCRIPCIÓN"].Value.ToString() == null)
                {
                    break;
                }
                else
                {
                    valortotalmateriales += float.Parse(filas.Cells["COSTO"].Value.ToString()) * float.Parse(filas.Cells["CANTIDAD"].Value.ToString());
                    DgvCosto_Materiales.AllowUserToAddRows = false;
                }
            }

            foreach (DataGridViewRow filas in DgvCosto_Mano_Obra.Rows)
            {
                if (filas.Cells["POSICIONES"].Value.ToString() == null ||
                    filas.Cells["RESPONSABLES"].Value.ToString() == null ||
                    filas.Cells["COSTOPHORA"].Value.ToString() == null ||
                    filas.Cells["Cod"].Value.ToString() == null)
                {
                    break;
                }
                else
                {
                    //DgvCosto_Mano_Obra.AllowUserToAddRows = false;
                    valortotaltrabajadores += float.Parse(filas.Cells["COSTOPHORA"].Value.ToString());
                }
            }
            TxtCosto_Total_Materiales.Text = valortotalmateriales.ToString();
            TxtCosto_Total_Mano_Obra.Text = valortotaltrabajadores.ToString();
            TxtCosto_Total.Text = (valortotalmateriales + valortotaltrabajadores).ToString();
            BtnTerminar.Enabled = true;
            DgvCosto_Materiales.AllowUserToAddRows = true;
            //DgvCosto_Mano_Obra.AllowUserToAddRows = true;
        }


        //Insertar


        private void BtnTerminar_Click(object sender, EventArgs e)
        {
            CE_Costos_Generales costos_generales = new CE_Costos_Generales();
            CE_Costos_Generales_Materiales costos_generales_materiales = new CE_Costos_Generales_Materiales();
            CE_Costos_Generales_Trabajadores costos_generales_trabajadores = new CE_Costos_Generales_Trabajadores();

            costos_generales.Fecha_Inicio = DtpFecha_Inicio.Value;
            costos_generales.Fecha_Entrega_Final = DtpFecha_Entrega.Value;
            costos_generales.Proceso = TxtProyecto.Text;
            costos_generales.Costo_Total_Materiales = float.Parse(TxtCosto_Total_Materiales.Text);
            costos_generales.Costo_Total_Mano_Obra = float.Parse(TxtCosto_Total_Mano_Obra.Text);
            costos_generales.Costo_Total = float.Parse(TxtCosto_Total.Text);
            costos_generales.D_I_Usuario = idvendedor;
            costos_generales.Matricula_Vehiculo = TxtMatricula.Text;
            costos_generales.Cod_Orden_Trabajo = Convert.ToInt32(numeroordentrabajo);
            oCN_Costos_Generales.InsertarCostosGenerales(costos_generales);

            foreach (DataGridViewRow filas in DgvCosto_Materiales.Rows)
            { 
                if (filas.Cells["CANTIDAD"].Value.ToString() == null ||
                    filas.Cells["COSTO"].Value.ToString() == null ||
                    filas.Cells["FECHA_COMPRA"].Value.ToString() == null ||
                    filas.Cells["Cod"].Value.ToString() == null ||
                    filas.Cells["DESCRIPCIÓN"].Value.ToString() == null)
                {
                    break;
                }
                else
                {
                    costos_generales_materiales.Cantidad = float.Parse(filas.Cells["CANTIDAD"].Value.ToString());
                    costos_generales_materiales.Costos_Unitarios = float.Parse(filas.Cells["COSTO"].Value.ToString());
                    costos_generales_materiales.Fecha_Compra = Convert.ToDateTime(filas.Cells["FECHA_COMPRA"].Value.ToString());
                    costos_generales_materiales.Cod_Materiales = Convert.ToInt32(filas.Cells["Cod"].Value.ToString());
                    costos_generales_materiales.Cod_Costos_Generales = numerocostosgenerales;

                    oCN_Costos_Generales_Materiales.InsertarCostosGeneralesMateriales(costos_generales_materiales);
                    DgvCosto_Materiales.AllowUserToAddRows = false;
                }
            }
            foreach (DataGridViewRow filas in DgvCosto_Mano_Obra.Rows)
            {
                if (filas.Cells["POSICIONES"].Value.ToString() == null ||
                    filas.Cells["RESPONSABLES"].Value.ToString() == null ||
                    filas.Cells["COSTOPHORA"].Value.ToString() == null ||
                    filas.Cells["Cod"].Value.ToString() == null)
                {
                    break;
                }
                else
                {
                    costos_generales_trabajadores.responsable = filas.Cells["RESPONSABLES"].Value.ToString();
                    costos_generales_trabajadores.costo_hora = float.Parse(filas.Cells["COSTOPHORA"].Value.ToString());
                    costos_generales_trabajadores.cod_tipo_trabajador = Convert.ToInt32(filas.Cells["Cod"].Value.ToString());
                    costos_generales_trabajadores.Cod_Costos_Generales = numerocostosgenerales ;

                    oCN_Costos_Generales_Trabajadores.InsertarCostosGeneralesTrabajadores(costos_generales_trabajadores);
                    DgvCosto_Mano_Obra.AllowUserToAddRows = false;
                }
            }
            //Limpiar();
            DgvCosto_Materiales.AllowUserToAddRows = true;
            DgvCosto_Mano_Obra.AllowUserToAddRows = true;
            MessageBox.Show("Costos Generales Terminada");
            BtnTerminar.Enabled = false;
            BuscarNumeroCostosGenerales();
            LblNumero_Orden_Trabajo.Text = "N° ";
        }
    }
}
