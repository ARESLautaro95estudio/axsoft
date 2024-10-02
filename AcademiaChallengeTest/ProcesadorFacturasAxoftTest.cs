using AcademiaChallenge;
using AcademiaChallenge.Exceptions;
using AcademiaChallenge.Model;

namespace AcademiaChallengeTest
{
    [TestClass]
    public class ProcesadorFacturasAxoftTest
    {
        #region Validar
        #region Validar casos correctos
        [TestMethod]
        public void Validar_CasoCorrecto00_SinFacturas_NoTiraExcepcion()
        {
            List<Factura> facturas = [];

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            procesadorFacturas.Validar();
        }

        [TestMethod]
        public void Validar_CasoCorrecto01_UnaFacturaUnRenglon_NoTiraExcepcion()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 1,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            procesadorFacturas.Validar();
        }

        [TestMethod]
        public void Validar_CasoCorrecto02_UnaFacturaDosRenglones_NoTiraExcepcion()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 1,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 5,
                        Total = 5
                    },
                    new RenglonFactura()
                    {
                        NumeroRenglon = 2,
                        CodigoArticulo = "Art02",
                        DescripcionArtigulo = "SoyElArticulo2",
                        PrecioUnitario = 5,
                        Cantidad = 1,
                        Total = 5
                    }
                ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            procesadorFacturas.Validar();
        }

        [TestMethod]
        public void Validar_CasoCorrecto03_DosFacturas_NoTiraExcepcion()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 1,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });
            facturas.Add(new Factura()
            {
                Numero = 2,
                Fecha = new DateTime(2024, 01, 02),
                CodigoCliente = "Cli02",
                RazonSocial = "Cliente 02 SA",
                Cuil = "987654321",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            procesadorFacturas.Validar();
        }
        #endregion

        #region Validar numeración correlativa
        [TestMethod]
        public void Validar_NumeracionCorrelativa01_PimerFactura0TiraError()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 0,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            Assert.ThrowsException<NumeracionInvalidaException>(procesadorFacturas.Validar, "Numeración inválida");
        }

        [TestMethod]
        public void Validar_NumeracionCorrelativa03_SegundaFactura3TiraError()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 1,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });
            facturas.Add(new Factura()
            {
                Numero = 3,
                Fecha = new DateTime(2024, 01, 02),
                CodigoCliente = "Cli02",
                RazonSocial = "Cliente 02 SA",
                Cuil = "987654321",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                            [
                                new RenglonFactura()
                                {
                                    NumeroRenglon = 1,
                                    CodigoArticulo = "Art01",
                                    DescripcionArtigulo = "SoyElArticulo1",
                                    PrecioUnitario = 1,
                                    Cantidad = 10,
                                    Total = 10
                                }
                            ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            Assert.ThrowsException<NumeracionInvalidaException>(procesadorFacturas.Validar, "Numeración inválida");
        }

        [TestMethod]
        public void Validar_NumeracionCorrelativa02_PimerFactura2TiraError()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 2,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            Assert.ThrowsException<NumeracionInvalidaException>(procesadorFacturas.Validar, "Numeración inválida");
        }
        #endregion
        #endregion

        #region Consultas
        #region Consultas TotalFacturado
        [TestMethod]
        public void TotalFacturado00_SinFacturasEsCero()
        {
            List<Factura> facturas = [];

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            procesadorFacturas.Validar(); // Validamos antes de procesar

            Assert.AreEqual(0, procesadorFacturas.TotalFacturado());
        }

        [TestMethod]
        public void TotalFacturado01_UnaFactura_TotalFacturadoEsIgualAlLaunicaFactura()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 1,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            procesadorFacturas.Validar(); // Validamos antes de procesar

            Assert.AreEqual(12.1, procesadorFacturas.TotalFacturado());
        }

        [TestMethod]
        public void TotalFacturado02_DosFactura_TotalFacturadoEsIgualALaSuma()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 1,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });
            facturas.Add(new Factura()
            {
                Numero = 2,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 100,
                PorcentajeIva = 21,
                ImporteIva = 21,
                TotalConIva = 121,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 100,
                        Total = 100
                    }
                ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            procesadorFacturas.Validar(); // Validamos antes de procesar

            Assert.AreEqual(133.1, procesadorFacturas.TotalFacturado());
        }
        #endregion
        #endregion

        [TestMethod]
        public void cantidadVendidasTest()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 1,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });
            facturas.Add(new Factura()
            {
                Numero = 2,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 100,
                PorcentajeIva = 21,
                ImporteIva = 21,
                TotalConIva = 121,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 100,
                        Total = 100
                    }
                ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            procesadorFacturas.Validar(); // Validamos antes de procesar

            Assert.AreEqual("Art01", procesadorFacturas.ArticuloMasVendido());
        }


        [TestMethod]
        public void ArticuloMasVendidoTest()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 1,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });
            facturas.Add(new Factura()
            {
                Numero = 2,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 100,
                PorcentajeIva = 21,
                ImporteIva = 21,
                TotalConIva = 121,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 100,
                        Total = 100
                    }
                ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            procesadorFacturas.Validar(); // Validamos antes de procesar
            Assert.AreEqual("Art01", procesadorFacturas.ArticuloMasVendido());
        }

        [TestMethod]
        public void ClienteQueMasGastoTest()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 1,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });
            facturas.Add(new Factura()
            {
                Numero = 2,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 100,
                PorcentajeIva = 21,
                ImporteIva = 21,
                TotalConIva = 121,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 100,
                        Total = 100
                    }
                ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            procesadorFacturas.Validar(); // Validamos antes de procesar
            Assert.AreEqual("Cli01", procesadorFacturas.ClienteQueMasGasto());
        }


        [TestMethod]
        public void ArticuloMasCompradoDeClienteTest()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 1,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });
            facturas.Add(new Factura()
            {
                Numero = 2,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 100,
                PorcentajeIva = 21,
                ImporteIva = 21,
                TotalConIva = 121,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 100,
                        Total = 100
                    }
                ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            procesadorFacturas.Validar(); // Validamos antes de procesar
            Assert.AreEqual("SoyElArticulo1", procesadorFacturas.ArticuloMasCompradoDeCliente("Cli01"));

        }

        [TestMethod]
        public void TotalFacturadoDeFechaTest()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 1,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });
            facturas.Add(new Factura()
            {
                Numero = 2,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 100,
                PorcentajeIva = 21,
                ImporteIva = 21,
                TotalConIva = 121,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 100,
                        Total = 100
                    }
                ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            procesadorFacturas.Validar(); // Validamos antes de procesar
            Assert.AreEqual(133.1, procesadorFacturas.TotalFacturadoDeFecha(new DateTime(2024, 01, 12)));
        }

        [TestMethod]
        public void ClienteQueMasComproArticuloTest()
        {
            List<Factura> facturas = [];
            facturas.Add(new Factura()
            {
                Numero = 1,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 10,
                PorcentajeIva = 21,
                ImporteIva = 2.1,
                TotalConIva = 12.1,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 10,
                        Total = 10
                    }
                ]
            });
            facturas.Add(new Factura()
            {
                Numero = 2,
                Fecha = new DateTime(2024, 01, 01),
                CodigoCliente = "Cli01",
                RazonSocial = "Cliente 01 SA",
                Cuil = "123456789",
                ImporteTotalSinIva = 100,
                PorcentajeIva = 21,
                ImporteIva = 21,
                TotalConIva = 121,
                Renglones =
                [
                    new RenglonFactura()
                    {
                        NumeroRenglon = 1,
                        CodigoArticulo = "Art01",
                        DescripcionArtigulo = "SoyElArticulo1",
                        PrecioUnitario = 1,
                        Cantidad = 100,
                        Total = 100
                    }
                ]
            });

            ProcesadorFacturasAxoft procesadorFacturas = new(facturas);
            procesadorFacturas.Validar(); // Validamos antes de procesar
            Assert.AreEqual("123456789", procesadorFacturas.ClienteQueMasComproArticulo("Art01"));
        }
    }
}