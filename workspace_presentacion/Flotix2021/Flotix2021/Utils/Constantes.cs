using System;
using System.Collections.Generic;
using System.Text;

namespace Flotix2021.Utils
{
    public static class Constantes
    {
        public static readonly string SERVIDOR = "http://florius8.ddns.net:6969/flotix/api/";

        public static readonly String[] arrayPlazas = new String[] { "Seleccionar", "2", "3", "4", "5", "6", "8", "9" };
        public static readonly String[] arrayTam = new String[] { "Seleccionar", "400", "800", "1200", "1600", "2000", "2400" };
        public enum Disponibilidad { Seleccionar, Si, No };
        public enum TipoKM { Mes, Año};

        public static readonly int NUEVO = 1;
        public static readonly int CONSULTA = 2;
        public static readonly int MODIFICA = 3;
    }
}
