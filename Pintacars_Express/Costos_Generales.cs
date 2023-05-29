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
    public partial class FrmCostos_Generales : Form
    {
        CN_Materiales oCN_Materiales = new CN_Materiales();
        CN_Tipo_Trabajador oCN_Tipo_Trabajador = new CN_Tipo_Trabajador();
        CN_Marca oCN_Marca = new CN_Marca();   
        CN_Costos_Generales oCN_Costos_Generales = new CN_Costos_Generales();
        CN_Costos_Generales_Materiales oCN_Costos_Generales_Materiales = new CN_Costos_Generales_Materiales();
        CN_Costos_Generales_Trabajadores oCN_Costos_Generales_Trabajadores = new CN_Costos_Generales_Trabajadores();


        public FrmCostos_Generales()
        {
            InitializeComponent();
        }

        private void Orden_Servicio_Load(object sender, EventArgs e)
        {
            MostrarMateriales();
            MostrarTipoTrabajador();
            MostrarMarcas();
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


        private void MostrarMarcas()
        {
            CboMarca.DataSource = oCN_Marca.MostrarMarcas();
            CboMarca.DisplayMember = "Nombre";
            CboMarca.ValueMember = "Cod";
            CboMarca.SelectedIndex = -1;
        }

        #endregion


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
            costos_generales.D_I_Usuario = TxtRecibe.Text;
            costos_generales.Matricula_Vehiculo = TxtMatricula.Text;
            costos_generales.Cod_Orden_Trabajo = 1;
            oCN_Costos_Generales.InsertarCostosGenerales(costos_generales);

            foreach (DataGridViewRow filas in DgvCosto_Materiales.Rows)
            {
                //MessageBox.Show(filas.Cells["CANTIDAD"].Value.ToString());
                //MessageBox.Show(filas.Cells["COSTO"].Value.ToString());
                //MessageBox.Show(filas.Cells["FECHA_COMPRA"].Value.ToString());
                //MessageBox.Show(filas.Cells["Cod"].Value.ToString());
                //MessageBox.Show(filas.Cells["DESCRIPCIÓN"].Value.ToString());

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
                    costos_generales_materiales.Cod_Costos_Generales = 1;

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
                    costos_generales_trabajadores.Cod_Costos_Generales = 1;

                    oCN_Costos_Generales_Trabajadores.InsertarCostosGeneralesTrabajadores(costos_generales_trabajadores);
                    DgvCosto_Mano_Obra.AllowUserToAddRows = false;
                }
            }
            //Limpiar();
            DgvCosto_Materiales.AllowUserToAddRows = true;
            DgvCosto_Mano_Obra.AllowUserToAddRows = true;
            MessageBox.Show("Costos Generales Terminada");
        }
    }
}
