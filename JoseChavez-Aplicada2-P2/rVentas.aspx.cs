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
                FechaTextBox.Text = DateTime.Now.ToString("dd/MM/yyyy");
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Articulo"), new DataColumn("Cantidad"), new DataColumn("Precio") });
                ViewState["Venta"] = dt;
            }
        }
        private void CargarDrop()
        {
           
            Articulos art = new Articulos();
            ArticuloDropDownList.DataSource = art.Listado("ArticuloId, Descripcion", "1=1", "");
            ArticuloDropDownList.DataTextField = "Descripcion";
            ArticuloDropDownList.DataValueField = "ArticuloId";
            ArticuloDropDownList.DataBind();

            PrecioDropDownList.DataSource = art.Listado("ArticuloId,Precio", "ArticuloId =" + ArticuloDropDownList.SelectedValue, "");
            PrecioDropDownList.DataTextField = "Precio";
            PrecioDropDownList.DataValueField = "ArticuloId";
            PrecioDropDownList.DataBind();

        }
        private void LlenarDatos(Ventas v)
        {
            v.Fecha = FechaTextBox.Text;
            v.Monto = Utility.ConvierteFloat(MontoTextBox.Text);
            foreach(GridViewRow item in VentaGridView.Rows)
            {
                v.AgregarVenta(Convert.ToInt32(item.Cells[0]), Convert.ToInt32(item.Cells[1]), Convert.ToInt32(item.Cells[2]), (float)Convert.ToDecimal(item.Cells[3]));
            }
        }

        private void Limpiar()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Articulo"), new DataColumn("Cantidad"), new DataColumn("Precio") });
            ViewState["Venta"] = dt;
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
                    if (art.Buscar(Utility.ConvierteEntero(ArticuloDropDownList.SelectedValue)))
                    {
                        paso = venta.Editar();
                    }
                    else
                    {
                        Utility.MensajeToastr(this.Page, "No quedan de esos Articulos", "Fatal", "Danger");
                    }
                    
                }
                if (paso)
                {
                    Utility.MensajeToastr(this.Page, "Exito", "bien", "success");
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
                venta.VentaId = Utility.ConvierteEntero(IdTextBox.Text);
                if (string.IsNullOrWhiteSpace(IdTextBox.Text))
                {
                    paso = venta.Eliminar();
                }
               
                if (paso)
                {
                    Utility.MensajeToastr(this.Page, "Exito elimino", "bien", "success");
                }
            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Rows.Add(ArticuloDropDownList.SelectedValue, CantidadTextBox.Text, PrecioDropDownList.Text);
                ViewState["Venta"] = dt;
                CargarGrid();
                CantidadTextBox.Text = string.Empty;
            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
            }
        }
    }
}