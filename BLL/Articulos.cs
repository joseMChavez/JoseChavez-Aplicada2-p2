using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class Articulos : ClaseMaestra
    {
        public int ArticuloId { get; set; }
        public string Descripcion { get; set; }
        public int Existencia { get; set; }
        public float Precio { get; set; }

        public Articulos()
        {
            this.ArticuloId = 0;
            this.Descripcion = "";
            this.Existencia = 0;
            this.Precio = 0;
        }

        public override bool Buscar(int IdBuscado)
        {
            throw new NotImplementedException();
        }
        public static bool BuscarExistencia(int IdBuscado,int cantidad)
        {
            DataTable dt = new DataTable();
            ConexionDb con = new ConexionDb();

            dt = con.ObtenerDatos(string.Format("Select * from Articulos where ArticuloId=" + IdBuscado + "And Existencia >="+cantidad));
            return dt.Rows.Count > 0;
        }
        public override bool Editar()
        {
            throw new NotImplementedException();
        }

        public override bool Eliminar()
        {
            throw new NotImplementedException();
        }

        public override bool Insertar()
        {
            throw new NotImplementedException();
        }

        public override DataTable Listado(string Campos, string Condicion, string Orden)
        {
            ConexionDb con = new ConexionDb();
            string Order = "";
            if (!Orden.Equals(""))
            {
                Order = "order bye";
            }
            return con.ObtenerDatos(string.Format("select "+Campos+" from Articulos where "+Condicion+Order));
        }
    }
}
