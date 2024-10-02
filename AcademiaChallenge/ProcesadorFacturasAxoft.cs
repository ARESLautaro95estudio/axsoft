using AcademiaChallenge.Exceptions;
using AcademiaChallenge.Model;

namespace AcademiaChallenge
{
    /// <summary>
    /// Representa un conjunto de facturas.
    /// Proporciona métodos para validarlas y para realizar consultas sobre las mismas.
    /// </summary>
    /// <param name="facturas">
    /// Lista de facturas que serán procesadas y validadas.
    /// </param>
    public class ProcesadorFacturasAxoft(List<Factura> facturas)
    {
        private readonly List<Factura> facturas = facturas;

        #region private validations
        /// <summary>
        /// Valida que la numeración de las facturas comienza en 1, está en orden correlativo y sin huecos.
        /// </summary>
        /// <exception cref="NumeracionInvalidaException">
        /// Se lanza cuando la numeración no es correlativa o presenta huecos.
        /// </exception>
        private void ValidarNumeracionCorrelativa()
        {
            for (int i = 1; i <= facturas.Count; i++)
            {
                if (facturas[i - 1].Numero != i)
                {
                    throw new NumeracionInvalidaException();
                }
            }
        }
        #endregion

        /// <summary>
        /// Realiza las validaciones necesarias sobre las facturas.
        /// </summary>
        /// <exception cref="ValidacionFacturaException">
        /// Se lanza cuando alguna de las validaciones de la factura falla.
        /// </exception>
        public void Validar()
        {
            ValidarNumeracionCorrelativa();
            // TODO: ValidarOrdenCronologicoFacturas
            // TODO: ValidarDatosConsistentesCliente
            // TODO: ValidarConsistenciaArticulo
            // TODO: ValidarOrdenNumeracionRenglones
            // TODO: ValidarTotalRenglon
            // TODO: ValidarTotalSinIVA
            // TODO: ValidarPorcentajeIVA
            // TODO: ValidarImporteIVA
            // TODO: ValidarTotalConIVA
        }

        /// <summary>
        /// Calcula el total facturado sumando los totales de todas las facturas.
        /// </summary>
        /// <returns>El total facturado con IVA incluido.</returns>
        public double TotalFacturado()
        {
            double total = 0;
            for (int i = 0; i < facturas.Count; i++)
            {
                total += facturas[i].TotalConIva;
            }
            return total;
        }

        /// <summary>
        /// Artículo que ha sido vendido en mayor cantidad.
        /// </summary>
        /// <returns>Código del artículo.</returns>
        public string ArticuloMasVendido()
        {
            Dictionary<string, int> CantidadPorArticulo = new Dictionary<string, int> ();
            for (int i =0;i<facturas.Count;i++)
            {
                foreach(RenglonFactura renglon in this.facturas[i].Renglones)
                {
                    if (CantidadPorArticulo.ContainsKey(renglon.CodigoArticulo))
                    {
                        CantidadPorArticulo[renglon.CodigoArticulo]+=renglon.Cantidad;
                    }
                    else
                    {
                       CantidadPorArticulo.Add(renglon.CodigoArticulo,renglon.Cantidad);
                    }
                }
            }
            int maximo = 0;
            string producto = "";
            foreach (KeyValuePair<string, int> productoCantidad in CantidadPorArticulo)
            {
                if (maximo< productoCantidad.Value)
                {
                    maximo = productoCantidad.Value;
                    producto = productoCantidad.Key;
                }
            }
            return producto;
        }

        /// <summary>
        /// Cliente que realizó el mayor gasto total
        /// </summary>
        /// <returns>Razón social del cliente.</returns>
        public string ClienteQueMasGasto()
        {
            Dictionary<string, double> ClienteGastoMaximo = new Dictionary<string, double>();
            for (int i = 0; i < facturas.Count; i++)
            {
                
                    if (ClienteGastoMaximo.ContainsKey(this.facturas[i].CodigoCliente))
                    {
                        ClienteGastoMaximo[this.facturas[i].CodigoCliente] += this.facturas[i].TotalConIva;
                    }
                    else
                    {
                        ClienteGastoMaximo.Add(this.facturas[i].CodigoCliente, this.facturas[i].TotalConIva);
                    }
            }
            double maximo = 0;
            string cliente = "";
            foreach (KeyValuePair<string, double> clienteGasto in ClienteGastoMaximo)
            {
                if (maximo < clienteGasto.Value)
                {
                    maximo = clienteGasto.Value;
                    cliente = clienteGasto.Key;
                }
            }
            return cliente;
        }

        /// <summary>
        /// Artículo mas comprado por un cliente
        /// </summary>
        /// <param name="codigoCliente">Código del cliente</param>
        /// <returns>Descripción del artículo</returns>
        public string ArticuloMasCompradoDeCliente(string codigoCliente)
        {
            Dictionary<string, int> cantidadDescripcion = new Dictionary<string, int>();
            for (int i = 0; i < facturas.Count; i++)
            {
                if (facturas[i].CodigoCliente==codigoCliente)
                {
                    foreach (RenglonFactura renglon in facturas[i].Renglones)
                    {
                        if (cantidadDescripcion.ContainsKey(renglon.DescripcionArtigulo))
                        {
                            cantidadDescripcion[renglon.DescripcionArtigulo] += renglon.Cantidad   ;
                        }
                        else
                        {
                            cantidadDescripcion.Add(renglon.DescripcionArtigulo, renglon.Cantidad);
                        }
                    }
                }
            }
            double maximo = 0;
            string DescripcionArticulo = "";
            foreach (KeyValuePair<string, int> compraCantidad in cantidadDescripcion)
            {
                if (maximo < compraCantidad.Value)
                {
                    maximo = compraCantidad.Value;
                    DescripcionArticulo = compraCantidad.Key;
                }
            }
            return DescripcionArticulo;
        }

        /// <summary>
        /// Calcula el total facturado para la fecha.
        /// </summary>
        /// <param name="fecha">Fecha para la que se va a calcular el total facturado</param>
        /// <returns>Total facturado para la fecha</returns>
        public double TotalFacturadoDeFecha(DateTime fecha)
        {
            double totalFacturado=0;
            for (int i = 0; i < facturas.Count; i++)
            {
                if (fecha > this.facturas[i].Fecha)
                {
                    totalFacturado += this.facturas[i].TotalConIva;
                }
            }
            return totalFacturado;
        }

        /// <summary>
        /// Cliente que copró mas cantidad de un artículo
        /// </summary>
        /// <param name="codigoArticulo">Código del artículo</param>
        /// <returns>CUIL del cliente</returns>
        public string ClienteQueMasComproArticulo(string codigoArticulo)
        {
            Dictionary<string,int> cuilCantidad = new   Dictionary<string,int>();
            for (int i = 0; i < facturas.Count; i++)
            {
                foreach (RenglonFactura renglon in this.facturas[i].Renglones)
                {
                    if (codigoArticulo==renglon.CodigoArticulo)
                    {
                        if (cuilCantidad.ContainsKey(facturas[i].Cuil))
                        {
                            cuilCantidad[facturas[i].CodigoCliente] += renglon.Cantidad;
                        }
                        else
                        { 
                            cuilCantidad.Add(facturas[i].Cuil, renglon.Cantidad);
                        
                        }
                    }
                }
            }
            int maximo = 0;
            string cuil = "";
            foreach (KeyValuePair<string, int> llaveValor in cuilCantidad)
            {
                if (maximo < llaveValor.Value)
                {
                    maximo = llaveValor.Value;
                    cuil = llaveValor.Key;
                }
            }
            return cuil;

        }
    }
}
