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
        public override bool Insertar()
        {
            ConexionDb con = new ConexionDb();
            bool retorno = false;
            try
            {
                retorno = con.Ejecutar(string.Format("Inset Into Articulos(Descripcion,Existencia,Precio) values('{0}',{1},{2})", this.Descripcion, this.Existencia, this.Precio));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }
        public override bool Editar()
        {
            ConexionDb con = new ConexionDb();
            bool retorno = false;
            try
            {
                retorno = con.Ejecutar(string.Format("update Articulos set Descripcion='{0}',Existencia={1}, Precio={2} where ArticuloId={3}", this.Descripcion, this.Existencia, this.Precio,ArticuloId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }
        public override bool Buscar(int IdBuscado)
        {
            DataTable dt = new DataTable();
            ConexionDb con = new ConexionDb();
            try
            {

                dt = con.ObtenerDatos(string.Format("Select * from Articulos where ArticuloId=" + IdBuscado));
                if (dt.Rows.Count>0)
                {
                    this.ArticuloId = (int)dt.Rows[0]["ArticuloId"];
                    this.Descripcion = dt.Rows[0]["Descripcion"].ToString();
                    this.Existencia = (int)dt.Rows[0]["Existencia"];
                    this.Precio = (float)Convert.ToDecimal(dt.Rows[0]["Precio"]);
                }
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return dt.Rows.Count > 0;
        }
        public static bool BuscarExistencia(int IdBuscado,int cantidad)
        {
            DataTable dt = new DataTable();
            ConexionDb con = new ConexionDb();

            dt = con.ObtenerDatos(string.Format("Select * from Articulos where ArticuloId=" + IdBuscado + "And Existencia >="+cantidad));
            return dt.Rows.Count > 0;
        }
       

        public override bool Eliminar()
        {
            ConexionDb con = new ConexionDb();
            bool retorno = false;
            try
            {
                retorno = con.Ejecutar(string.Format(" delete from Articulos  where ArticuloId={0}",this.ArticuloId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
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
