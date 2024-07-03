using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauricioGonzales_ChessGame
{
    class Tablero
    {
        public int Tamanio { get; set; }
        public Celda[,] casilla { get; set; }
        public Celda celdavacia { get; set; }

        public bool hackeoencurso { get; set; }

        public bool atackDirr { get; set; }
        public Tablero(int tam)
        {
            Tamanio = tam;
            celdavacia = new Celda();
            casilla = new Celda[Tamanio, Tamanio];

            celdavacia.llenado("", "", 0, 0, "empty.png");
            celdavacia.contPasos = 0;

            for (int i = 0; i < Tamanio; i++)
            {
                for (int j = 0; j < Tamanio; j++)
                {
                    casilla[i, j] = new Celda();
                }
            }
        }

        public void MovLegales(Tablero TT, int x, int y, string pieza, int tur, int conte)
        {

            int iii = 1;

            switch (pieza)
            {
                case "Caballo":
                    MovSeguros(x, y, x + 2, y + 1, tur);
                    MovSeguros(x, y, x + 2, y - 1, tur);
                    MovSeguros(x, y, x - 2, y + 1, tur);
                    MovSeguros(x, y, x - 2, y - 1, tur);
                    MovSeguros(x, y, x + 1, y + 2, tur);
                    MovSeguros(x, y, x + 1, y - 2, tur);
                    MovSeguros(x, y, x - 1, y + 2, tur);
                    MovSeguros(x, y, x - 1, y - 2, tur);

                    break;

                case "Rey":

                    MovSeguros(x, y, x + 1, y + 1, tur);
                    MovSeguros(x, y, x + 1, y, tur);
                    MovSeguros(x, y, x + 1, y - 1, tur);
                    MovSeguros(x, y, x, y + 1, tur);
                    MovSeguros(x, y, x, y - 1, tur);
                    MovSeguros(x, y, x - 1, y + 1, tur);
                    MovSeguros(x, y, x - 1, y, tur);
                    MovSeguros(x, y, x - 1, y - 1, tur);

                    PosibleEnroque(x, y, tur);

                    break;

                case "Torre":
                    iii = 1;
                    while (x + iii >= 0 && x + iii < TT.Tamanio && casilla[x + iii, y].equipo != casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x + iii, y, tur);

                        if (casilla[x + iii, y].equipo == -1 * casilla[x, y].equipo)
                        {
                            iii = 10;
                        }
                        iii++;
                    }

                    iii = 1;
                    while (x - iii >= 0 && x - iii < TT.Tamanio && casilla[x - iii, y].equipo != casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x - iii, y, tur);

                        if (casilla[x - iii, y].equipo == -1 * casilla[x, y].equipo)
                        {
                            iii = 10;
                        }
                        iii++;
                    }

                    iii = 1;
                    while (y + iii >= 0 && y + iii < TT.Tamanio && casilla[x, y + iii].equipo != casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x, y + iii, tur);
                        if (casilla[x, y + iii].equipo == -1 * casilla[x, y].equipo)
                        {
                            iii = 10;
                        }
                        iii++;

                    }

                    iii = 1;
                    while (y - iii >= 0 && y - iii < TT.Tamanio && casilla[x, y - iii].equipo != casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x, y - iii, tur);
                        if (casilla[x, y - iii].equipo == -1 * casilla[x, y].equipo)
                        {
                            iii = 10;
                        }
                        iii++;
                    }
                    break;


                case "Alfil":

                    iii = 1;
                    while (y - iii >= 0 && y - iii < TT.Tamanio && x - iii >= 0 && x - iii < TT.Tamanio && casilla[x - iii, y - iii].equipo != casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x - iii, y - iii, tur);
                        if (casilla[x - iii, y - iii].equipo == -1 * casilla[x, y].equipo)
                        {
                            iii = 10;
                        }
                        iii++;
                    }

                    iii = 1;
                    while (y - iii >= 0 && y - iii < TT.Tamanio && x + iii >= 0 && x + iii < TT.Tamanio && casilla[x + iii, y - iii].equipo != casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x + iii, y - iii, tur);
                        if (casilla[x + iii, y - iii].equipo == -1 * casilla[x, y].equipo)
                        {
                            iii = 10;
                        }
                        iii++;
                    }

                    iii = 1;
                    while (y + iii >= 0 && y + iii < TT.Tamanio && x - iii >= 0 && x - iii < TT.Tamanio && casilla[x - iii, y + iii].equipo != casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x - iii, y + iii, tur);
                        if (casilla[x - iii, y + iii].equipo == -1 * casilla[x, y].equipo)
                        {
                            iii = 10;
                        }
                        iii++;
                    }

                    iii = 1;
                    while (y + iii >= 0 && y + iii < TT.Tamanio && x + iii >= 0 && x + iii < TT.Tamanio && casilla[x + iii, y + iii].equipo != casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x + iii, y + iii, tur);
                        if (casilla[x + iii, y + iii].equipo == -1 * casilla[x, y].equipo)
                        {
                            iii = 10;
                        }
                        iii++;
                    }
                    break;


                case "Reina":
                    iii = 1;
                    while (x + iii >= 0 && x + iii < TT.Tamanio && casilla[x + iii, y].equipo != casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x + iii, y, tur);

                        if (casilla[x + iii, y].equipo == -1 * casilla[x, y].equipo)
                        {
                            iii = 10;
                        }
                        iii++;
                    }

                    iii = 1;
                    while (x - iii >= 0 && x - iii < TT.Tamanio && casilla[x - iii, y].equipo != casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x - iii, y, tur);

                        if (casilla[x - iii, y].equipo == -1 * casilla[x, y].equipo)
                        {
                            iii = 10;
                        }
                        iii++;
                    }

                    iii = 1;
                    while (y + iii >= 0 && y + iii < TT.Tamanio && casilla[x, y + iii].equipo != casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x, y + iii, tur);
                        if (casilla[x, y + iii].equipo == -1 * casilla[x, y].equipo)
                        {
                            iii = 10;
                        }
                        iii++;

                    }

                    iii = 1;
                    while (y - iii >= 0 && y - iii < TT.Tamanio && casilla[x, y - iii].equipo != casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x, y - iii, tur);
                        if (casilla[x, y - iii].equipo == -1 * casilla[x, y].equipo)
                        {
                            iii = 10;
                        }
                        iii++;
                    }

                    iii = 1;
                    while (y - iii >= 0 && y - iii < TT.Tamanio && x - iii >= 0 && x - iii < TT.Tamanio && casilla[x - iii, y - iii].equipo != casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x - iii, y - iii, tur);
                        if (casilla[x - iii, y - iii].equipo == -1 * casilla[x, y].equipo)
                        {
                            iii = 10;
                        }
                        iii++;
                    }

                    iii = 1;
                    while (y - iii >= 0 && y - iii < TT.Tamanio && x + iii >= 0 && x + iii < TT.Tamanio && casilla[x + iii, y - iii].equipo != casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x + iii, y - iii, tur);
                        if (casilla[x + iii, y - iii].equipo == -1 * casilla[x, y].equipo)
                        {
                            iii = 10;
                        }
                        iii++;
                    }

                    iii = 1;
                    while (y + iii >= 0 && y + iii < TT.Tamanio && x - iii >= 0 && x - iii < TT.Tamanio && casilla[x - iii, y + iii].equipo != casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x - iii, y + iii, tur);
                        if (casilla[x - iii, y + iii].equipo == -1 * casilla[x, y].equipo)
                        {
                            iii = 10;
                        }
                        iii++;
                    }

                    iii = 1;
                    while (y + iii >= 0 && y + iii < TT.Tamanio && x + iii >= 0 && x + iii < TT.Tamanio && casilla[x + iii, y + iii].equipo != casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x + iii, y + iii, tur);
                        if (casilla[x + iii, y + iii].equipo == -1 * casilla[x, y].equipo)
                        {
                            iii = 10;
                        }
                        iii++;
                    }

                    break;


                case "PeonB":
                    if (conte == 0 && casilla[x - 1, y].equipo == 0 && casilla[x - 2, y].equipo == 0)
                    {
                        MovSeguros(x, y, x - 2, y, tur);
                    }
                    if (y - 1 >= 0 && y - 1 < TT.Tamanio && casilla[x - 1, y - 1].equipo == -1 * casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x - 1, y - 1, tur);
                    }
                    if (y + 1 >= 0 && y + 1 < TT.Tamanio && casilla[x - 1, y + 1].equipo == -1 * casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x - 1, y + 1, tur);
                    }
                    if (casilla[x - 1, y].equipo != -1 * casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x - 1, y, tur);
                    }

                    PosibilidadDePeonAlPaso(x, y, tur);

                    break;

                case "PeonN":

                    if (conte == 0 && casilla[x + 1, y].equipo == 0 && casilla[x + 2, y].equipo == 0)
                    {
                        MovSeguros(x, y, x + 2, y, tur);
                    }

                    if (y - 1 >= 0 && y - 1 < TT.Tamanio && casilla[x + 1, y - 1].equipo == -1 * casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x + 1, y - 1, tur);
                    }
                    if (y + 1 >= 0 && y + 1 < TT.Tamanio && casilla[x + 1, y + 1].equipo == -1 * casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x + 1, y + 1, tur);
                    }

                    if (casilla[x + 1, y].equipo != -1 * casilla[x, y].equipo)
                    {
                        MovSeguros(x, y, x + 1, y, tur);
                    }

                    PosibilidadDePeonAlPaso(x, y, tur);

                    break;

                default:
                    break;
            }
        }

        private void MovSeguros(int u1, int u2, int v1, int v2, int tt)
        {
            if (hackeoencurso == false)
            {
                if (v1 < Tamanio && v1 >= 0 && v2 < Tamanio && v2 >= 0 && casilla[u1, u2].equipo == tt)
                {
                    if (atackDirr == false)
                    {
                        if (casilla[u1, u2].equipo != casilla[v1, v2].equipo)
                        {
                            if (casilla[u1, u2].tipo == "Rey")
                            {
                                if (casilla[v1, v2].zonaamenazada == false)
                                {
                                    casilla[v1, v2].permiso = true;

                                }
                            }
                            else
                            {
                                casilla[v1, v2].permiso = true;
                            }
                        }
                    }
                    else if (atackDirr == true)
                    {
                        if (casilla[u1, u2].tipo == "Rey")
                        {
                            if (casilla[v1, v2].zonaamenazada == false)
                            {
                                casilla[v1, v2].permiso = true;

                            }
                        }
                        else
                        {
                            casilla[v1, v2].permiso = true;
                        }
                    }
                }
            }
            else if (hackeoencurso == true)
            {
                if (v1 < Tamanio && v1 >= 0 && v2 < Tamanio && v2 >= 0 && casilla[u1, u2].equipo != casilla[v1, v2].equipo && casilla[u1, u2].equipo == tt)
                {
                    if (casilla[u1, u2].tipo != "Rey")
                    {
                        if (casilla[v1, v2].bloquearjaque == true)
                        {
                            casilla[v1, v2].defensahack = true;
                        }
                    }

                    else if (casilla[u1, u2].tipo == "Rey")
                    {
                        if (casilla[v1, v2].zonaamenazada == false && casilla[v1, v2].ataqueactivohack == false)
                        {
                            casilla[v1, v2].defensahack = true;

                        }
                    }
                }

            }

        }

        public void LlenadoInicial()
        {
            casilla[0, 0].llenado("torre Negra 1", "Torre", -1, 0, "torrenn.png");
            casilla[0, 1].llenado("caballo Negro 1", "Caballo", -1, 0, "caballonn.png");
            casilla[0, 2].llenado("alfil Negro 1", "Alfil", -1, 0, "alfilnn.png");
            casilla[0, 3].llenado("reina Negra ", "Reina", -1, 0, "reinann.png");
            casilla[0, 4].llenado("re x Negro ", "Rey", -1, 0, "reynn.png");
            casilla[0, 5].llenado("alfil Negro 2", "Alfil", -1, 0, "alfilnn.png");
            casilla[0, 6].llenado("caballo Negro 2", "Caballo", -1, 0, "caballonn.png");
            casilla[0, 7].llenado("torre Negra 2", "Torre", -1, 0, "torrenn.png");

            casilla[1, 0].llenado("peón Negro 1", "PeonN", -1, 0, "peonnn.png");
            casilla[1, 1].llenado("peón Negro 2", "PeonN", -1, 0, "peonnn.png");
            casilla[1, 2].llenado("peón Negro 3", "PeonN", -1, 0, "peonnn.png");
            casilla[1, 3].llenado("peón Negro 4", "PeonN", -1, 0, "peonnn.png");
            casilla[1, 4].llenado("peón Negro 5", "PeonN", -1, 0, "peonnn.png");
            casilla[1, 5].llenado("peón Negro 6", "PeonN", -1, 0, "peonnn.png");
            casilla[1, 6].llenado("peón Negro 7", "PeonN", -1, 0, "peonnn.png");
            casilla[1, 7].llenado("peón Negro 8", "PeonN", -1, 0, "peonnn.png");

            for (int i = 2; i < 6; i++)
            {
                for (int j = 0; j < Tamanio; j++)
                {
                    casilla[i, j].llenado("", "", 0, 0, "empty.png");
                }

            }

            casilla[6, 0].llenado("peón Blanco 1", "PeonB", 1, 0, "peonbb.png");
            casilla[6, 1].llenado("peón Blanco 2", "PeonB", 1, 0, "peonbb.png");
            casilla[6, 2].llenado("peón Blanco 3", "PeonB", 1, 0, "peonbb.png");
            casilla[6, 3].llenado("peón Blanco 4", "PeonB", 1, 0, "peonbb.png");
            casilla[6, 4].llenado("peón Blanco 5", "PeonB", 1, 0, "peonbb.png");
            casilla[6, 5].llenado("peón Blanco 6", "PeonB", 1, 0, "peonbb.png");
            casilla[6, 6].llenado("peón Blanco 7", "PeonB", 1, 0, "peonbb.png");
            casilla[6, 7].llenado("peón Blanco 8", "PeonB", 1, 0, "peonbb.png");

            casilla[7, 0].llenado("torre Blanca 1", "Torre", 1, 0, "torrebb.png");
            casilla[7, 1].llenado("caballo Blanco 1", "Caballo", 1, 0, "caballobb.png");
            casilla[7, 2].llenado("alfil Blanco 1", "Alfil", 1, 0, "alfilbb.png");
            casilla[7, 3].llenado("reina Blanca", "Reina", 1, 0, "reinabb.png");
            casilla[7, 4].llenado("re x Blanco", "Rey", 1, 0, "reybb.png");
            casilla[7, 5].llenado("alfil Blanco 2", "Alfil", 1, 0, "alfilbb.png");
            casilla[7, 6].llenado("caballo Blanco 2", "Caballo", 1, 0, "caballobb.png");
            casilla[7, 7].llenado("torre Blanca 2", "Torre", 1, 0, "torrebb.png");
            for (int i = 0; i < Tamanio; i++)
            {
                for (int j = 0; j < Tamanio; j++)
                {
                    casilla[i, j].copiaLadoRey = false;
                    casilla[i, j].peonAcabadeSaltarDoble = false;
                }
            }
        }
        public void AtaqueDirecto(Tablero TT, int x, int y, int xrey, int yrey, string pieza, int tur, int conte)
        {
            int delX = xrey - x;
            int delY = yrey - y;
            int iii = 1;
            switch (pieza)
            {
                case
                    "Caballo":
                    MovSeguros(x, y, x + delX, y + delY, tur);
                    break;
                case "Rey":
                    MovSeguros(x, y, x + delX, y + delY, tur);
                    break;

                case "Torre":
                    if (delX > 0)
                    {
                        iii = 1;
                        while (x + iii >= 0 && x + iii < TT.Tamanio && casilla[x + iii, y].equipo != casilla[x, y].equipo)
                        {

                            MovSeguros(x, y, x + iii, y, tur);

                            iii++;
                        }
                    }
                    else if (delX < 0)
                    {
                        iii = 1;
                        while (x - iii >= 0 && x - iii < TT.Tamanio && casilla[x - iii, y].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x - iii, y, tur);

                            iii++;
                        }
                    }

                    else if (delY > 0)
                    {
                        iii = 1;
                        while (y + iii >= 0 && y + iii < TT.Tamanio && casilla[x, y + iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x, y + iii, tur);

                            iii++;

                        }
                    }

                    else if (delY < 0)
                    {
                        iii = 1;
                        while (y - iii >= 0 && y - iii < TT.Tamanio && casilla[x, y - iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x, y - iii, tur);

                            iii++;
                        }
                    }
                    break;
                case "Alfil":
                    if (delX < 0 && delY < 0)
                    {
                        iii = 1;
                        while (y - iii >= 0 && y - iii < TT.Tamanio && x - iii >= 0 && x - iii < TT.Tamanio)
                        {
                            MovSeguros(x, y, x - iii, y - iii, tur);

                            iii++;
                        }
                    }

                    else if (delX > 0 && delY < 0)
                    {
                        iii = 1;
                        while (y - iii >= 0 && y - iii < TT.Tamanio && x + iii >= 0 && x + iii < TT.Tamanio && casilla[x + iii, y - iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x + iii, y - iii, tur);

                            iii++;
                        }
                    }

                    else if (delY > 0 && delX < 0)
                    {
                        iii = 1;
                        while (y + iii >= 0 && y + iii < TT.Tamanio && x - iii >= 0 && x - iii < TT.Tamanio && casilla[x - iii, y + iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x - iii, y + iii, tur);

                            iii++;
                        }
                    }

                    else if (delY > 0 && delX > 0)
                    {
                        iii = 1;
                        while (y + iii >= 0 && y + iii < TT.Tamanio && x + iii >= 0 && x + iii < TT.Tamanio && casilla[x + iii, y + iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x + iii, y + iii, tur);

                            iii++;
                        }
                    }
                    break;
                case "Reina":
                    if (delX > 0 && delY == 0)
                    {
                        iii = 1;
                        while (x + iii >= 0 && x + iii < TT.Tamanio && casilla[x + iii, y].equipo != casilla[x, y].equipo)
                        {

                            MovSeguros(x, y, x + iii, y, tur);

                            iii++;
                        }
                    }
                    else if (delX < 0 && delY == 0)
                    {
                        iii = 1;
                        while (x - iii >= 0 && x - iii < TT.Tamanio && casilla[x - iii, y].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x - iii, y, tur);

                            iii++;
                        }
                    }

                    else if (delY > 0 && delX == 0)
                    {
                        iii = 1;
                        while (y + iii >= 0 && y + iii < TT.Tamanio && casilla[x, y + iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x, y + iii, tur);

                            iii++;

                        }
                    }

                    else if (delY < 0 && delX == 0)
                    {
                        iii = 1;
                        while (y - iii >= 0 && y - iii < TT.Tamanio && casilla[x, y - iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x, y - iii, tur);

                            iii++;
                        }
                    }

                    else if (delX < 0 && delY < 0)
                    {
                        iii = 1;
                        while (y - iii >= 0 && y - iii < TT.Tamanio && x - iii >= 0 && x - iii < TT.Tamanio && casilla[x - iii, y - iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x - iii, y - iii, tur);

                            iii++;
                        }
                    }

                    else if (delX > 0 && delY < 0)
                    {
                        iii = 1;
                        while (y - iii >= 0 && y - iii < TT.Tamanio && x + iii >= 0 && x + iii < TT.Tamanio && casilla[x + iii, y - iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x + iii, y - iii, tur);

                            iii++;
                        }
                    }

                    else if (delY > 0 && delX < 0)
                    {
                        iii = 1;
                        while (y + iii >= 0 && y + iii < TT.Tamanio && x - iii >= 0 && x - iii < TT.Tamanio && casilla[x - iii, y + iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x - iii, y + iii, tur);

                            iii++;
                        }
                    }

                    else if (delY > 0 && delX > 0)
                    {
                        iii = 1;
                        while (y + iii >= 0 && y + iii < TT.Tamanio && x + iii >= 0 && x + iii < TT.Tamanio && casilla[x + iii, y + iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x + iii, y + iii, tur);

                            iii++;
                        }
                    }
                    break;
                case "PeonB":
                    if (delX < 0 && delY < 0)
                    {
                        if (y - 1 >= 0 && y - 1 < TT.Tamanio && casilla[x - 1, y - 1].equipo == -1 * casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x - 1, y - 1, tur);
                        }
                    }
                    else if (delX < 0 && delY > 0)
                    {
                        if (y + 1 >= 0 && y + 1 < TT.Tamanio && casilla[x - 1, y + 1].equipo == -1 * casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x - 1, y + 1, tur);
                        }
                    }
                    break;

                case "PeonN":
                    if (delX > 0 && delY < 0)
                    {
                        if (y - 1 >= 0 && y - 1 < TT.Tamanio && casilla[x + 1, y - 1].equipo == -1 * casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x + 1, y - 1, tur);
                        }
                    }

                    else if (delX > 0 && delY > 0)
                    {
                        if (y + 1 >= 0 && y + 1 < TT.Tamanio && casilla[x + 1, y + 1].equipo == -1 * casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x + 1, y + 1, tur);
                        }
                    }

                    break;
            }

        }

        public void BloqueableAtaque(Tablero TT, int x, int y, int xrey, int yrey, string pieza, int tur, int conte)
        {
            int delX = xrey - x;
            int delY = yrey - y;
            int iii = 1;
            switch (pieza)
            {
                case
                    "Caballo":
                    MovSeguros(x, y, x + delX, y + delY, tur);
                    break;
                case "Rey":
                    MovSeguros(x, y, x + delX, y + delY, tur);
                    break;

                case "Torre":
                    if (delX > 0)
                    {
                        iii = 1;
                        while (x + iii >= 0 && x + iii < TT.Tamanio && casilla[x + iii, y].equipo != casilla[x, y].equipo)
                        {

                            MovSeguros(x, y, x + iii, y, tur);

                            if (casilla[x + iii, y].equipo == -1 * casilla[x, y].equipo)
                            {
                                iii = 10;
                            }

                            iii++;
                        }
                    }
                    else if (delX < 0)
                    {
                        iii = 1;
                        while (x - iii >= 0 && x - iii < TT.Tamanio && casilla[x - iii, y].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x - iii, y, tur);

                            if (casilla[x - iii, y].equipo == -1 * casilla[x, y].equipo)
                            {
                                iii = 10;
                            }

                            iii++;
                        }
                    }

                    else if (delY > 0)
                    {
                        iii = 1;
                        while (y + iii >= 0 && y + iii < TT.Tamanio && casilla[x, y + iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x, y + iii, tur);

                            if (casilla[x, y + iii].equipo == -1 * casilla[x, y].equipo)
                            {
                                iii = 10;
                            }

                            iii++;

                        }
                    }

                    else if (delY < 0)
                    {
                        iii = 1;
                        while (y - iii >= 0 && y - iii < TT.Tamanio && casilla[x, y - iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x, y - iii, tur);

                            if (casilla[x, y - iii].equipo == -1 * casilla[x, y].equipo)
                            {
                                iii = 10;
                            }

                            iii++;
                        }
                    }
                    break;
                case "Alfil":
                    if (delX < 0 && delY < 0)
                    {
                        iii = 1;
                        while (y - iii >= 0 && y - iii < TT.Tamanio && x - iii >= 0 && x - iii < TT.Tamanio && casilla[x - iii, y - iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x - iii, y - iii, tur);


                            if (casilla[x - iii, y - iii].equipo == -1 * casilla[x, y].equipo)
                            {
                                iii = 10;
                            }

                            iii++;
                        }
                    }

                    else if (delX > 0 && delY < 0)
                    {
                        iii = 1;
                        while (y - iii >= 0 && y - iii < TT.Tamanio && x + iii >= 0 && x + iii < TT.Tamanio && casilla[x + iii, y - iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x + iii, y - iii, tur);

                            if (casilla[x + iii, y - iii].equipo == -1 * casilla[x, y].equipo)
                            {
                                iii = 10;
                            }

                            iii++;
                        }
                    }

                    else if (delY > 0 && delX < 0)
                    {
                        iii = 1;
                        while (y + iii >= 0 && y + iii < TT.Tamanio && x - iii >= 0 && x - iii < TT.Tamanio && casilla[x - iii, y + iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x - iii, y + iii, tur);

                            if (casilla[x - iii, y + iii].equipo == -1 * casilla[x, y].equipo)
                            {
                                iii = 10;
                            }

                            iii++;
                        }
                    }

                    else if (delY > 0 && delX > 0)
                    {
                        iii = 1;
                        while (y + iii >= 0 && y + iii < TT.Tamanio && x + iii >= 0 && x + iii < TT.Tamanio && casilla[x + iii, y + iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x + iii, y + iii, tur);

                            if (casilla[x + iii, y + iii].equipo == -1 * casilla[x, y].equipo)
                            {
                                iii = 10;
                            }

                            iii++;
                        }
                    }
                    break;
                case "Reina":
                    if (delX > 0 && delY == 0)
                    {
                        iii = 1;
                        while (x + iii >= 0 && x + iii < TT.Tamanio && casilla[x + iii, y].equipo != casilla[x, y].equipo)
                        {

                            MovSeguros(x, y, x + iii, y, tur);

                            if (casilla[x + iii, y].equipo == -1 * casilla[x, y].equipo)
                            {
                                iii = 10;
                            }

                            iii++;
                        }
                    }
                    else if (delX < 0 && delY == 0)
                    {
                        iii = 1;
                        while (x - iii >= 0 && x - iii < TT.Tamanio && casilla[x - iii, y].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x - iii, y, tur);

                            if (casilla[x - iii, y].equipo == -1 * casilla[x, y].equipo)
                            {
                                iii = 10;
                            }

                            iii++;
                        }
                    }

                    else if (delY > 0 && delX == 0)
                    {
                        iii = 1;
                        while (y + iii >= 0 && y + iii < TT.Tamanio && casilla[x, y + iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x, y + iii, tur);

                            if (casilla[x, y + iii].equipo == -1 * casilla[x, y].equipo)
                            {
                                iii = 10;
                            }

                            iii++;

                        }
                    }

                    else if (delY < 0 && delX == 0)
                    {
                        iii = 1;
                        while (y - iii >= 0 && y - iii < TT.Tamanio && casilla[x, y - iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x, y - iii, tur);

                            if (casilla[x, y - iii].equipo == -1 * casilla[x, y].equipo)
                            {
                                iii = 10;
                            }

                            iii++;
                        }
                    }

                    else if (delX < 0 && delY < 0)
                    {
                        iii = 1;
                        while (y - iii >= 0 && y - iii < TT.Tamanio && x - iii >= 0 && x - iii < TT.Tamanio && casilla[x - iii, y - iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x - iii, y - iii, tur);

                            if (casilla[x - iii, y - iii].equipo == -1 * casilla[x, y].equipo)
                            {
                                iii = 10;
                            }

                            iii++;
                        }
                    }

                    else if (delX > 0 && delY < 0)
                    {
                        iii = 1;
                        while (y - iii >= 0 && y - iii < TT.Tamanio && x + iii >= 0 && x + iii < TT.Tamanio && casilla[x + iii, y - iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x + iii, y - iii, tur);

                            if (casilla[x + iii, y - iii].equipo == -1 * casilla[x, y].equipo)
                            {
                                iii = 10;
                            }

                            iii++;
                        }
                    }

                    else if (delY > 0 && delX < 0)
                    {
                        iii = 1;
                        while (y + iii >= 0 && y + iii < TT.Tamanio && x - iii >= 0 && x - iii < TT.Tamanio && casilla[x - iii, y + iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x - iii, y + iii, tur);

                            if (casilla[x - iii, y + iii].equipo == -1 * casilla[x, y].equipo)
                            {
                                iii = 10;
                            }

                            iii++;
                        }
                    }

                    else if (delY > 0 && delX > 0)
                    {
                        iii = 1;
                        while (y + iii >= 0 && y + iii < TT.Tamanio && x + iii >= 0 && x + iii < TT.Tamanio && casilla[x + iii, y + iii].equipo != casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x + iii, y + iii, tur);

                            if (casilla[x + iii, y + iii].equipo == -1 * casilla[x, y].equipo)
                            {
                                iii = 10;
                            }

                            iii++;
                        }
                    }
                    break;
                case "PeonB":
                    if (delX < 0 && delY < 0)
                    {
                        if (y - 1 >= 0 && y - 1 < TT.Tamanio && casilla[x - 1, y - 1].equipo == -1 * casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x - 1, y - 1, tur);
                        }
                    }
                    else if (delX < 0 && delY > 0)
                    {
                        if (y + 1 >= 0 && y + 1 < TT.Tamanio && casilla[x - 1, y + 1].equipo == -1 * casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x - 1, y + 1, tur);
                        }
                    }
                    break;

                case "PeonN":
                    if (delX > 0 && delY < 0)
                    {
                        if (y - 1 >= 0 && y - 1 < TT.Tamanio && casilla[x + 1, y - 1].equipo == -1 * casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x + 1, y - 1, tur);
                        }
                    }

                    else if (delX > 0 && delY > 0)
                    {
                        if (y + 1 >= 0 && y + 1 < TT.Tamanio && casilla[x + 1, y + 1].equipo == -1 * casilla[x, y].equipo)
                        {
                            MovSeguros(x, y, x + 1, y + 1, tur);
                        }
                    }

                    break;
            }
        }
        public void PeonAmenaza(Tablero TT, int x, int y, string pieza, int tur, int conte)
        {

            if (pieza == "PeonB")
            {
                if (y - 1 >= 0 && y - 1 < TT.Tamanio)
                {
                    MovSeguros(x, y, x - 1, y - 1, tur);
                }
                if (y + 1 >= 0 && y + 1 < TT.Tamanio)
                {
                    MovSeguros(x, y, x - 1, y + 1, tur);
                }
            }
            else if (pieza == "PeonN")
            {
                if (y - 1 >= 0 && y - 1 < TT.Tamanio)
                {
                    MovSeguros(x, y, x + 1, y - 1, tur);
                }
                if (y + 1 >= 0 && y + 1 < TT.Tamanio)
                {
                    MovSeguros(x, y, x + 1, y + 1, tur);
                }
            }
        }

        public void PosibleEnroque(int xenr, int yenr, int turenr)
        {
            if (casilla[xenr, yenr].equipo == turenr && hackeoencurso == false && casilla[xenr, yenr].contPasos == 0)
            {

                int pDER = 0, pIZQ = 0;
                int A = 5;
                if (casilla[xenr, yenr].equipo == 1)
                {
                    A = 7;
                }
                else if (casilla[xenr, yenr].equipo == -1)
                {
                    A = 0;
                }

                if (casilla[A, 7].tipo == "Torre" && casilla[A, 7].contPasos == 0 && casilla[A, 7].equipo == turenr)
                {
                    for (int eii = 0; eii < 2; eii++)
                    {
                        if (casilla[A, 5 + eii].zonaamenazada == false)
                        {
                            if (casilla[A, 5 + eii].equipo == 0)
                            {
                                pDER++;
                            }
                        }
                    }
                    if (pDER == 2)
                    {
                        casilla[A, 6].permiso = true;
                    }

                }


                if (casilla[A, 0].tipo == "Torre" && casilla[A, 0].contPasos == 0 && casilla[A, 0].equipo == turenr)
                {
                    if (casilla[A, 1].equipo == 0)
                    {
                        for (int eii = 0; eii < 2; eii++)
                        {
                            if (casilla[A, 3 - eii].zonaamenazada == false)
                            {
                                if (casilla[A, 3 - eii].equipo == 0)
                                {
                                    pIZQ++;
                                }
                            }
                        }
                    }

                    if (pIZQ == 2)
                    {
                        casilla[A, 2].permiso = true;
                    }
                }

            }
        }

        private void PosibilidadDePeonAlPaso(int xpeo, int ypeo, int turPeo)
        {
            if (casilla[xpeo, ypeo].equipo == turPeo)
            {
                if ((casilla[xpeo, ypeo].tipo == "PeonB" && xpeo == 3) || (casilla[xpeo, ypeo].tipo == "PeonN" && xpeo == 4))
                {
                    int arm = 0;
                    if (casilla[xpeo, ypeo].tipo == "PeonB")
                    {
                        arm = 1;
                    }
                    else if (casilla[xpeo, ypeo].tipo == "PeonN")
                    {
                        arm = -1;
                    }

                    if (ypeo - 1 < Tamanio && ypeo - 1 >= 0 && casilla[xpeo, ypeo - 1].peonAcabadeSaltarDoble == true)
                    {

                        if (casilla[xpeo - arm, ypeo - 1].equipo == 0 && casilla[xpeo, ypeo - 1].equipo == -1 * turPeo)
                        {
                            if (hackeoencurso == false)
                            {
                                casilla[xpeo - arm, ypeo - 1].permiso = true;
                            }
                            else if (hackeoencurso == true)
                            {
                                if (casilla[xpeo, ypeo - 1].bloquearjaque == true)
                                {
                                    casilla[xpeo - arm, ypeo - 1].defensahack = true;

                                }
                            }
                        }
                    }

                    if (ypeo + 1 < Tamanio && ypeo + 1 >= 0 && casilla[xpeo, ypeo + 1].peonAcabadeSaltarDoble == true)
                    {
                        if (casilla[xpeo - arm, ypeo + 1].equipo == 0 && casilla[xpeo, ypeo + 1].equipo == -1 * turPeo)
                        {
                            if (hackeoencurso == false)
                            {
                                casilla[xpeo - arm, ypeo + 1].permiso = true;
                            }
                            else if (hackeoencurso == true)
                            {
                                if (casilla[xpeo, ypeo + 1].bloquearjaque == true)
                                {
                                    casilla[xpeo - arm, ypeo + 1].defensahack = true;

                                }
                            }
                        }
                    }
                }

            }

        }
    }
}
