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
                int.TryParse(con.ObtenerValor(string.Format("Insert into Venta(Fecha,Monto) values('{0}',{1}); SCOPE IDENTITY()", this.Fecha, this.Monto)).ToString(), out Retorno);

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
                retorno = con.Ejecutar(string.Format("delete from Ventas VentaId = {0};"+"Delete from VentasDetalle where VentaId={0}",this.VentaId));
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return retorno;
        }

        public override bool Buscar(int IdBuscado)
        {
            throw new NotImplementedException();
        }

        public override DataTable Listado(string Campos, string Condicion, string Orden)
        {
            throw new NotImplementedException();
        }
    }
}
