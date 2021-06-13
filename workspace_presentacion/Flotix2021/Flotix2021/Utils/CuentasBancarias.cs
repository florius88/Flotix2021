using System;
using System.Text.RegularExpressions;

namespace Flotix2021.Utils
{
    /// <summary>
    /// Servicios de validación de las cuentas bancarias españolas
    /// </summary>
    public static class CuentasBancarias
    {

        /// <summary>
        /// Validación de una cuenta bancaria española
        /// </summary>
        /// <param name="banco">Código del banco en formato "0000"</param>
        /// <param name="oficina">Código de la sucursal en formato "0000"</param>
        /// <param name="dc">Dígito de control en formato "00"</param>
        /// <param name="cuenta">Número de cuenta en formato "0000000000"</param>
        /// <returns>true si el número de cuenta es correcto</returns>
        public static bool ValidaCuentaBancaria(string banco, string oficina,
                                                        string dc, string cuenta)
        {
            /*
             * Se comprueba que realmente sean números los datos pasados 
             * como parámetros y que las longitudes sean correctas 
             */
            if (!IsNumeric(banco) || banco.Length != 4)
                throw new ArgumentException
                    ("El banco no tiene un formato adecuado");

            if (!IsNumeric(oficina) || oficina.Length != 4)
                throw new ArgumentException
                    ("La oficina no tiene un formato adecuado");

            if (!IsNumeric(dc) || dc.Length != 2)
                throw new ArgumentException
                    ("El dígito de control no tiene un formato adecuado");

            if (!IsNumeric(cuenta) || cuenta.Length != 10)
                throw new ArgumentException
                    ("El número de cuenta no tiene un formato adecuado");

            return CompruebaCuenta(banco, oficina, dc, cuenta);
        }

        /// <summary>
        /// Validación de una cuenta bancaria española
        /// </summary>
        /// <param name="cuentaCompleta">Número de cuenta completa con 
        /// carácteres numéricos y 20 dígitos</param>
        /// <returns>true si el número de cuenta es correcto</returns>
        public static bool ValidaCuentaBancaria(string cuentaCompleta)
        {
            // Comprobaciones de la cadena
            if (cuentaCompleta.Length != 20)
                throw new ArgumentException
                    ("El número de cuenta no el formato adecuado");

            string banco = cuentaCompleta.Substring(0, 4);
            string oficina = cuentaCompleta.Substring(4, 4);
            string dc = cuentaCompleta.Substring(8, 2);
            string cuenta = cuentaCompleta.Substring(10, 10);

            return ValidaCuentaBancaria(banco, oficina, dc, cuenta);

        }

        /// <summary>
        /// Validación de una cuenta bancaria española
        /// </summary>
        /// <param name="banco">Código del banco</param>
        /// <param name="oficina">Código de la oficina</param>
        /// <param name="dc">Dígito de control</param>
        /// <param name="cuenta">Número de cuenta</param>
        /// <returns>true si el número de cuenta es correcto</returns>
        public static bool ValidaCuentaBancaria(UInt64 banco, UInt64 oficina,
                    UInt64 dc, UInt64 cuenta)
        {
            return ValidaCuentaBancaria(
                                banco.ToString("0000")
                                , oficina.ToString("0000")
                                , dc.ToString("00")
                                , cuenta.ToString("0000000000")
                                );
        }

        /// <summary>
        /// Comprueba que la cadena sólo incluya números
        /// </summary>
        /// <param name="numero">Cadena de texto en formato número</param>
        /// <returns>true si <paramref name="numero"/> contiene sólo números</returns>
        /// <remarks>No se contemplan decimales</remarks>
        private static bool IsNumeric(string numero)
        {
            Regex regex = new Regex("[0-9]");
            return regex.Match(numero).Success;
        }

        /// <summary>
        /// Una cuenta bancaria está validada si los dígitos de control 
        /// calculados coinciden con los que se han pasado en los argumentos
        /// </summary>
        private static bool CompruebaCuenta(string banco, string oficina,
            string dc, string cuenta)
        {
            return GetDigitoControl("00" + banco + oficina)
                + GetDigitoControl(cuenta) == dc;
        }

        /// <summary>
        /// Obtiene el dígito de control de una cuenta bancaria. 
        /// La función sólo devuelve un número que corresponderá a una de las dos opciones.
        ///     - Codigo + CódigoOficina
        ///     - CuentaBancaria
        /// </summary>
        private static string GetDigitoControl(string CadenaNumerica)
        {
            int[] pesos = { 1, 2, 4, 8, 5, 10, 9, 7, 3, 6 };
            UInt32 suma = 0;
            UInt32 resto;

            for (int i = 0; i < pesos.Length; i++)
            {
                suma += (UInt32)pesos[i] *
                    UInt32.Parse(CadenaNumerica.Substring(i, 1));
            }
            resto = 11 - (suma % 11);

            if (resto == 10) resto = 1;
            if (resto == 11) resto = 0;

            return resto.ToString("0");
        }
    }
}
