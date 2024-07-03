using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauricioGonzales_ChessGame
{
    class Celda
    {
        public int equipo { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; }
        public bool permiso { get; set; }

        public int contPasos { get; set; }

        public Image figura { get; set; }
        public void llenado(string nom, string tip, int equ, int cont, string fg)
        {
            nombre = nom;
            tipo = tip;
            equipo = equ;
            contPasos = cont;
            string direccion = "../../img/" + fg;
            figura = Image.FromFile(direccion);
        }
        public bool ataqueactivohack { get; set; }
        public bool defensahack { get; set; }

        public bool zonaamenazada { get; set; }
        public bool copiaLadoRey { get; set; }
        public bool bloquearjaque { get; set; }
        public bool peonAcabadeSaltarDoble { get; set; }

    }
}
