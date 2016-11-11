using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DAL;
namespace BLL
{
    public class Ventas : ClaseMaestra
    {

        public int VentaId { get; set; }
        public string Fecha { get; set; }
        public float Monto { get; set; }
        public List<VentaDetalle> Detalle { get; set; }
        public Ventas()
        {
            this.VentaId = 0;
            this.Fecha = "";
            this.Monto = 0;
            this.Detalle = new List<VentaDetalle>();
        }
        public void AgregarVenta( int Articuloid,int Cantidad, float precio)
        {
            Detalle.Add(new VentaDetalle( Articuloid, Cantidad, precio));
        }

       
        public override bool Insertar()
        {

            int Retorno = 0;
            ConexionDb con = new ConexionDb();
            try
            {
                int.TryParse(con.ObtenerValor(string.Format("Insert into Ventas(Fecha,Monto) values('{0}',{1});select SCOPE_IDENTITY()", this.Fecha, this.Monto)).ToString(), out Retorno);

                foreach (VentaDetalle item in Detalle)
                {
                    con.Ejecutar(string.Format("insert into VentasDetalle(VentaId,ArticuloId,Cantidad,Precio) values({0},{1},{2},{3})", Retorno, item.ArticuloId, item.Cantidad, item.Precio));
                    con.Ejecutar(string.Format("update Articulos set Existencia=Existencia-" + item.Cantidad + " where ArticuloId=" + item.ArticuloId));
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Retorno > 0;
        }
        public override bool Editar()
        {
            ConexionDb con = new ConexionDb();
            bool Retorno = false;
            try
            {
                Retorno = con.Ejecutar(string.Format("update Venta set Fecha = '{0}',Monto={1}  where VentaId= " + this.VentaId));

                if (Retorno)
                {
                    con.Ejecutar(string.Format("delete from VentasDetalle where VentaId={0}",this.VentaId));
                    foreach (VentaDetalle item in Detalle)
                    {
                        con.Ejecutar(string.Format("insert into VentasDetalle(VentaId,ArticuloId,Cantidad,Precio) values({0},{1},{2},{3})", Retorno, item.ArticuloId, item.Cantidad, item.Precio));
                        con.Ejecutar(string.Format("update Articulos set Existencia=Existencia-" + item.Cantidad + " where ArticuloId=" + item.ArticuloId));
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Retorno;
        }

        public override bool Eliminar()
        {
            ConexionDb con = new ConexionDb();
            bool retorno = false;
            try
            {
                retorno = con.Ejecutar(string.Format("Delete from VentasDetalle where Ventaid={0};"+"delete from Ventas where VentaId = {0}",this.VentaId));
                foreach (VentaDetalle item in Detalle)
                {
                    con.Ejecutar(string.Format("update Articulos set Existencia=Existencia +" + item.Cantidad + " where ArticuloId=" + item.ArticuloId));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return retorno;
        }

        public override bool Buscar(int IdBuscado)
        {
            ConexionDb con = new ConexionDb();
            DataTable dt = new DataTable();
            DataTable detalle = new DataTable();
            try
            {
                dt = con.ObtenerDatos(string.Format("Select * from Ventas where VentaId={0}", IdBuscado));
                if (dt.Rows.Count>0)
                {
                    this.VentaId = (int)dt.Rows[0]["VentaId"];
                    this.Monto = (float)Convert.ToDecimal(dt.Rows[0]["Monto"]);
                    this.Fecha = dt.Rows[0]["Fecha"].ToString();

                    detalle = con.ObtenerDatos(string.Format("Select * from VentasDetalle where Ventaid={0}", this.VentaId));
                    foreach(DataRow row in detalle.Rows)
                        AgregarVenta((int)row["ArticuloId"], (int)row["Cantidad"], (float)Convert.ToDecimal(row["Precio"].ToString()));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return dt.Rows.Count > 0;
        }
        public static DataTable ListadoDos( string Condicion)
        {
            DataTable dt = new DataTable();
            ConexionDb con = new ConexionDb();
            return dt = con.ObtenerDatos(string.Format("select V.VentaId as Id, V.Monto, VD.ArticuloId,VD.Cantidad, VD.Precio, V.Fecha from Ventas as V inner join VentasDetalle as VD on V.VentaId = VD.Ventaid where " + Condicion));
        }
        public override DataTable Listado(string Campos, string Condicion, string Orden)
        {
            DataTable dt = new DataTable();
            ConexionDb con = new ConexionDb();
            string order = "";
            if (Orden!="")
            {
                order = " order by";
            }
            return dt = con.ObtenerDatos(string.Format("Select"+Campos+" from Ventas"+Condicion+order));
        }
    }
}
