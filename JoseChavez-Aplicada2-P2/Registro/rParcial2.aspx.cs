﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace JoseChavez_Aplicada2_P2.Registro
{
    public partial class rParcial2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Utility.MensajeToastr(this.Page, "hola", "info");
 
        }
    }
}