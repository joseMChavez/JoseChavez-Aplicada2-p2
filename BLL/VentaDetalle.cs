using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class VentaDetalle
    {
        public int Id { get; set; }
        public int VentaId { get; set; }
        public int ArticuloId { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }

        public VentaDetalle( int articuloId, int cantidad, float precio)
        {
        
            this.ArticuloId = articuloId;
            this.Cantidad = cantidad;
            this.Precio = precio;
        }
    }
}
