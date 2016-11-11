using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using BLL;

namespace JoseChavez_Aplicada2_P2
{
    public partial class rVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDrop();
                FechaTextBox.Text = DateTime.Today.ToString("dd/MM/yyyy");
                DataTable dt = new DataTable();
                Ventas venta = new Ventas();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("ArticuloId"), new DataColumn("Cantidad"), new DataColumn("Precio") });
                ViewState["Venta"] = dt;
                int id = 0;
                if (Request.QueryString["ID"] != null)
                {
                    id = Utility.ConvierteEntero(Request.QueryString["ID"].ToString());
                    if (venta.Buscar(id))
                    {
                        if (VentaGridView.Rows.Count == 0)
                        {
                            Devolverdatos(venta);
                            FechaTextBox.Focus();
                        }
                    }
                }
            }
        }
        private void CargarDrop()
        {

            Articulos art = new Articulos();
            ArticuloDropDownList.DataSource = art.Listado("ArticuloId, Descripcion", "1=1", "");
            ArticuloDropDownList.DataTextField = "Descripcion";
            ArticuloDropDownList.DataValueField = "ArticuloId";
            ArticuloDropDownList.DataBind();

        }
        private void LlenarDatos(Ventas v)
        {
            v.Fecha = FechaTextBox.Text;
            v.Monto = Utility.ConvierteFloat(MontoTextBox.Text);

            foreach (GridViewRow item in VentaGridView.Rows)
            {
                v.AgregarVenta(Utility.ConvierteEntero(item.Cells[0].Text), Utility.ConvierteEntero(item.Cells[1].Text), Utility.ConvierteFloat(item.Cells[2].Text));

            }

        }
        private void Devolverdatos(Ventas v)
        {
            IdTextBox.Text = v.VentaId.ToString();
            FechaTextBox.Text = v.Fecha;
            MontoTextBox.Text = v.Monto.ToString();
            foreach (var item in v.Detalle)
            {
                DataTable dt = (DataTable)ViewState["Venta"];
                dt.Rows.Add(item.ArticuloId, item.Cantidad, item.Precio);
                ViewState["Venta"] = dt;
                VentaGridView.DataSource = ViewState["Venta"];
                VentaGridView.DataBind();
            }
        }
        private void Limpiar()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("ArticuloId"), new DataColumn("Cantidad"), new DataColumn("Precio") });
            ViewState["Venta"] = dt;
            CargarGrid();
            CantidadTextBox.Text = string.Empty;
            IdTextBox.Text = string.Empty;
            MontoTextBox.Text = string.Empty;
            FechaTextBox.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        private void CargarGrid()
        {

            VentaGridView.DataSource = (DataTable)ViewState["Venta"];
            VentaGridView.DataBind();
        }
        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void GuardarButton_Click(object sender, EventArgs e)
        {
            Ventas venta = new Ventas();
            LlenarDatos(venta);
            bool paso = false;
            Articulos art = new Articulos();
            try
            {
                if (string.IsNullOrWhiteSpace(IdTextBox.Text))
                {

                    paso = venta.Insertar();

                }
                else
                {
                    paso = venta.Editar();

                }
                if (paso)
                {
                    Utility.MensajeToastr(this.Page, "Exito", "bien", "success");
                    Limpiar();
                }
            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            Ventas venta = new Ventas();
            bool paso = false;
            try
            {
                venta.VentaId = Utility.ConvierteEntero(Request.QueryString["ID"]);
                if (Request.QueryString["ID"] != null)
                {
                    paso = venta.Eliminar();

                }

                if (paso)
                {
                    Utility.MensajeToastr(this.Page, "Exito elimino", "bien", "success");
                    Limpiar();
                }
            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            float total = 0, suma = 0, resultado = 0;
            try
            {
                DataTable dt = (DataTable)ViewState["Venta"];
                if (!string.IsNullOrWhiteSpace(CantidadTextBox.Text) && Articulos.BuscarExistencia(Utility.ConvierteEntero(ArticuloDropDownList.SelectedValue), Utility.ConvierteEntero(CantidadTextBox.Text)))
                {
                    dt.Rows.Add(ArticuloDropDownList.SelectedValue, CantidadTextBox.Text, PrecioDropDownList.SelectedValue);
                    ViewState["Venta"] = dt;
                    CargarGrid();
                    CantidadTextBox.Text = string.Empty;

                    foreach (GridViewRow item in VentaGridView.Rows)
                    {

                        suma = suma + Utility.ConvierteFloat(item.Cells[1].Text);
                        total = total + Utility.ConvierteFloat(item.Cells[2].Text);
                    }
                    resultado = suma * total;
                    MontoTextBox.Text = resultado.ToString();
                    CantidadTextBox.Text = string.Empty;
                }
                else
                {
                    Utility.MensajeToastr(this.Page, "Ya no Quedan esa Cantidad de Articulos!", "Cuidado!", "Warning");
                }

            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }

        protected void ArticuloDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Articulos art = new Articulos();
            PrecioDropDownList.DataSource = art.Listado("ArticuloId,Precio", "ArticuloId =" + ArticuloDropDownList.SelectedValue, "");
            PrecioDropDownList.DataTextField = "Precio";
            PrecioDropDownList.DataValueField = "Precio";
            PrecioDropDownList.DataBind();
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Consultas/cVentas.aspx");
        }
    }
}