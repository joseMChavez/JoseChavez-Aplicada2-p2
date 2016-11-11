using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace JoseChavez_Aplicada2_P2.Consultas
{
    public partial class cVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private string Mostrar()
        {
            string filtro = "";
            if (ActivaCheckBox.Checked.Equals(false))
            {
                if (string.IsNullOrWhiteSpace(FiltroTextBox.Text))
                    filtro = "1=1";
                else
                    filtro = DropDLFiltro.SelectedValue + " like '%" + FiltroTextBox.Text + "%'";
            }
            else
                filtro = "Fecha  BETWEEN '" + DesdeTextBox.Text + "' AND '" + HastaTextBox.Text + "' ";

            VentasGridView.DataSource = Ventas.ListadoDos(filtro);
            VentasGridView.DataBind();
            return filtro;
        }
        protected void ButtonBuscar_Click(object sender, EventArgs e)
        {
            Mostrar();
        }
    }
}