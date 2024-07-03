using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MauricioGonzales_ChessGame
{
    public partial class Form_Chess : Form
    {
        static Tablero miTablero = new Tablero(8);
        public Button[,] botonesTab = new Button[miTablero.Tamanio, miTablero.Tamanio];

        public int P, Q;
        public int turno;
        public string auxnom, auxtip;
        public int auxequ, auxcont;
        public Image auxImg;
        public int h_parahack;
        public int xhack, yhack;
        public Form_Chess()
        {
            InitializeComponent();
            PropiedadesTablero();
        }
        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        public void PropiedadesTablero()
        {
            miTablero.LlenadoInicial();
            miTablero.hackeoencurso = false;
            turno = 1;

            panel1.Width = panel1.Height;

            for (int i = 0; i < miTablero.Tamanio; i++)
            {
                for (int j = 0; j < miTablero.Tamanio; j++)
                {
                    botonesTab[i, j] = new Button();
                    botonesTab[i, j].Width = panel1.Width / miTablero.Tamanio;
                    botonesTab[i, j].Height = panel1.Height / miTablero.Tamanio;
                }
            }

            for (int i = 0; i < miTablero.Tamanio; i++)
            {
                for (int j = 0; j < miTablero.Tamanio; j++)
                {
                    panel1.Controls.Add(botonesTab[i, j]);
                    botonesTab[i, j].Location = new Point(j * botonesTab[i, j].Width, i * botonesTab[i, j].Width);
                    botonesTab[i, j].Tag = new Point(i, j);
                    botonesTab[i, j].MouseUp += EventoClick;
                }
            }


            for (int i = 0; i < miTablero.Tamanio; i++)
            {
                for (int j = 0; j < miTablero.Tamanio; j++)
                {
                    botonesTab[i, j].BackgroundImage = miTablero.casilla[i, j].figura;
                    botonesTab[i, j].BackgroundImageLayout = ImageLayout.Zoom;
                }
            }

            label1.Text = "WHITE'S TURN";
            ColoresTablero();
        }

        private void EventoClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Button botonClickeado = (Button)sender;
                Point ubicacion = (Point)botonClickeado.Tag;

                int x1 = ubicacion.X;
                P = x1;
                int y1 = ubicacion.Y;
                Q = y1;

                ColoresTablero();

                auxnom = miTablero.casilla[P, Q].nombre;
                auxtip = miTablero.casilla[P, Q].tipo;
                auxequ = miTablero.casilla[P, Q].equipo;
                auxcont = miTablero.casilla[P, Q].contPasos;
                auxImg = miTablero.casilla[P, Q].figura;

                for (int i = 0; i < miTablero.Tamanio; i++)
                {
                    for (int j = 0; j < miTablero.Tamanio; j++)
                    {
                        miTablero.casilla[i, j].permiso = false;
                        miTablero.casilla[i, j].defensahack = false;
                        botonesTab[i, j].Text = "";
                    }
                }

                if (miTablero.casilla[x1, y1].tipo != "Rey")
                {
                    int equipoauxiliar = 0;
                    int auxreyX = 0, auxreyY = 0;
                    int auxatX = 0, auxatY = 0;
                    bool permiso_para_dejar_alRey = true;

                    equipoauxiliar = miTablero.casilla[x1, y1].equipo;
                    miTablero.casilla[x1, y1].equipo = 0;

                    for (int i = 0; i < miTablero.Tamanio; i++)
                    {
                        for (int j = 0; j < miTablero.Tamanio; j++)
                        {
                            if (miTablero.casilla[i, j].tipo != "PeonB" && miTablero.casilla[i, j].tipo != "PeonN")
                            {
                                miTablero.MovLegales(miTablero, i, j, miTablero.casilla[i, j].tipo, turno * -1, miTablero.casilla[i, j].contPasos);
                            }

                            else if (miTablero.casilla[i, j].tipo == "PeonB" || miTablero.casilla[i, j].tipo == "PeonN")
                            {
                                miTablero.PeonAmenaza(miTablero, i, j, miTablero.casilla[i, j].tipo, turno * -1, miTablero.casilla[i, j].contPasos);
                            }

                            for (int ll = 0; ll < miTablero.Tamanio; ll++)
                            {
                                for (int rr = 0; rr < miTablero.Tamanio; rr++)
                                {
                                    if (miTablero.casilla[ll, rr].tipo == "Rey" && miTablero.casilla[ll, rr].permiso == true && miTablero.casilla[ll, rr].equipo == turno)
                                    {
                                        permiso_para_dejar_alRey = false;
                                        auxreyX = ll;
                                        auxreyY = rr;
                                        auxatX = i;
                                        auxatY = j;

                                    }

                                    miTablero.casilla[ll, rr].permiso = false;
                                }
                            }

                        }
                    }

                    miTablero.casilla[x1, y1].equipo = equipoauxiliar;
                    for (int i = 0; i < miTablero.Tamanio; i++)
                    {
                        for (int j = 0; j < miTablero.Tamanio; j++)
                        {
                            miTablero.casilla[i, j].permiso = false;
                            miTablero.casilla[i, j].defensahack = false;
                            botonesTab[i, j].Text = "";
                        }
                    }

                    if (permiso_para_dejar_alRey == true)
                    {

                        miTablero.MovLegales(miTablero, x1, y1, miTablero.casilla[x1, y1].tipo, turno, miTablero.casilla[x1, y1].contPasos);
                    }

                    else if (permiso_para_dejar_alRey == false)
                    {
                        if (miTablero.hackeoencurso == false)
                        {

                            equipoauxiliar = miTablero.casilla[x1, y1].equipo;
                            miTablero.casilla[x1, y1].equipo = 0;

                            miTablero.BloqueableAtaque(miTablero, auxatX, auxatY, auxreyX, auxreyY, miTablero.casilla[auxatX, auxatY].tipo, turno * -1, miTablero.casilla[auxatX, auxatY].contPasos);

                            for (int i = 0; i < miTablero.Tamanio; i++)
                            {
                                for (int j = 0; j < miTablero.Tamanio; j++)
                                {
                                    if (miTablero.casilla[i, j].permiso == true)
                                    {
                                        miTablero.casilla[i, j].defensahack = true;
                                    }

                                    miTablero.casilla[i, j].permiso = false;
                                }
                            }

                            miTablero.casilla[auxatX, auxatY].defensahack = true;
                            miTablero.casilla[x1, y1].equipo = equipoauxiliar;
                            miTablero.MovLegales(miTablero, x1, y1, miTablero.casilla[x1, y1].tipo, turno, miTablero.casilla[x1, y1].contPasos);

                            for (int i = 0; i < miTablero.Tamanio; i++)
                            {
                                for (int j = 0; j < miTablero.Tamanio; j++)
                                {
                                    if (miTablero.casilla[i, j].defensahack == false)
                                    {
                                        miTablero.casilla[i, j].permiso = false;
                                    }
                                    miTablero.casilla[i, j].defensahack = false;
                                }
                            }

                        }


                    }

                }
                else if (miTablero.casilla[x1, y1].tipo == "Rey")
                {

                    if (miTablero.hackeoencurso == true)
                    {

                        miTablero.MovLegales(miTablero, x1, y1, miTablero.casilla[x1, y1].tipo, turno, miTablero.casilla[x1, y1].contPasos);

                        miTablero.hackeoencurso = false;
                        miTablero.MovLegales(miTablero, x1, y1, miTablero.casilla[x1, y1].tipo, turno, miTablero.casilla[x1, y1].contPasos);
                        if (miTablero.casilla[xhack, yhack].zonaamenazada == false && miTablero.casilla[xhack, yhack].permiso == true)
                        {
                            miTablero.casilla[xhack, yhack].defensahack = true;
                        }
                        for (int i = 0; i < miTablero.Tamanio; i++)
                        {
                            for (int j = 0; j < miTablero.Tamanio; j++)
                            {
                                miTablero.casilla[i, j].permiso = false;
                            }
                        }
                        miTablero.hackeoencurso = true;

                    }
                    else if (miTablero.hackeoencurso == false)
                    {
                        miTablero.MovLegales(miTablero, x1, y1, miTablero.casilla[x1, y1].tipo, turno, miTablero.casilla[x1, y1].contPasos);

                        for (int i = 0; i < miTablero.Tamanio; i++)
                        {
                            for (int j = 0; j < miTablero.Tamanio; j++)
                            {
                                if (miTablero.casilla[i, j].permiso == true && miTablero.casilla[i, j].equipo == -1 * turno)
                                {
                                    miTablero.casilla[i, j].equipo = turno;
                                    miTablero.casilla[i, j].copiaLadoRey = true;
                                }

                                miTablero.casilla[i, j].permiso = false;
                            }
                        }

                        for (int i = 0; i < miTablero.Tamanio; i++)
                        {
                            for (int j = 0; j < miTablero.Tamanio; j++)
                            {
                                if (miTablero.casilla[i, j].equipo == -1 * turno)
                                {
                                    if (miTablero.casilla[i, j].tipo != "PeonB" && miTablero.casilla[i, j].tipo != "PeonN")
                                    {
                                        miTablero.MovLegales(miTablero, i, j, miTablero.casilla[i, j].tipo, turno * -1, miTablero.casilla[i, j].contPasos);
                                    }

                                    else if (miTablero.casilla[i, j].tipo == "PeonB" || miTablero.casilla[i, j].tipo == "PeonN")
                                    {
                                        miTablero.PeonAmenaza(miTablero, i, j, miTablero.casilla[i, j].tipo, turno * -1, miTablero.casilla[i, j].contPasos);
                                    }
                                }
                            }
                        }

                        for (int i = 0; i < miTablero.Tamanio; i++)
                        {
                            for (int j = 0; j < miTablero.Tamanio; j++)
                            {
                                if (miTablero.casilla[i, j].permiso == true)
                                {
                                    miTablero.casilla[i, j].zonaamenazada = true;
                                }

                                miTablero.casilla[i, j].permiso = false;
                            }
                        }

                        ProteccionAtaqueContiguoRey(-1 * turno);
                        for (int i = 0; i < miTablero.Tamanio; i++)
                        {
                            for (int j = 0; j < miTablero.Tamanio; j++)
                            {

                                if (miTablero.casilla[i, j].copiaLadoRey == true)
                                {
                                    miTablero.casilla[i, j].equipo = -1 * turno;
                                }

                                miTablero.casilla[i, j].permiso = false;
                                miTablero.casilla[i, j].copiaLadoRey = false;
                            }
                        }

                        for (int i = 0; i < miTablero.Tamanio; i++)
                        {
                            for (int j = 0; j < miTablero.Tamanio; j++)
                            {
                                if (miTablero.casilla[i, j].tipo != "PeonB" && miTablero.casilla[i, j].tipo != "PeonN")
                                {
                                    miTablero.MovLegales(miTablero, i, j, miTablero.casilla[i, j].tipo, turno * -1, miTablero.casilla[i, j].contPasos);
                                }

                                else if (miTablero.casilla[i, j].tipo == "PeonB" || miTablero.casilla[i, j].tipo == "PeonN")
                                {
                                    miTablero.PeonAmenaza(miTablero, i, j, miTablero.casilla[i, j].tipo, turno * -1, miTablero.casilla[i, j].contPasos);
                                }
                            }
                        }
                        for (int i = 0; i < miTablero.Tamanio; i++)
                        {
                            for (int j = 0; j < miTablero.Tamanio; j++)
                            {
                                if (miTablero.casilla[i, j].permiso == true)
                                {
                                    miTablero.casilla[i, j].zonaamenazada = true;
                                }
                                miTablero.casilla[i, j].permiso = false;
                            }
                        }
                        miTablero.MovLegales(miTablero, x1, y1, miTablero.casilla[x1, y1].tipo, turno, miTablero.casilla[x1, y1].contPasos);

                        for (int i = 0; i < miTablero.Tamanio; i++)
                        {
                            for (int j = 0; j < miTablero.Tamanio; j++)
                            {
                                if (miTablero.casilla[i, j].zonaamenazada == true)
                                {
                                    miTablero.casilla[i, j].permiso = false;
                                }

                                miTablero.casilla[i, j].zonaamenazada = false;
                            }
                        }
                    }


                }

                for (int i = 0; i < miTablero.Tamanio; i++)
                {
                    for (int j = 0; j < miTablero.Tamanio; j++)
                    {
                        if (miTablero.hackeoencurso == false)
                        {
                            if (miTablero.casilla[i, j].permiso == true)
                            {
                                botonesTab[i, j].BackColor = Color.Orange;
                            }
                        }
                        else if (miTablero.hackeoencurso == true)
                        {
                            if (miTablero.casilla[i, j].defensahack == true)
                            {
                                botonesTab[i, j].BackColor = Color.Orange;
                            }

                        }
                    }
                }


            }

            else if (e.Button == MouseButtons.Right)
            {
                Button botonClickeado2 = (Button)sender;
                Point ubicacion2 = (Point)botonClickeado2.Tag;

                int x2 = ubicacion2.X;
                int y2 = ubicacion2.Y;
                xhack = 0; yhack = 0;

                int rPeon2 = 0;


                if (botonesTab[x2, y2].BackColor == Color.Orange)
                {
                    miTablero.hackeoencurso = false;
                    DetectarSiHuboPeonAlPaso(x2, y2, P, Q, turno);
                    if (auxtip == "PeonB" || auxtip == "PeonN")
                    {
                        if (auxcont == 0 && Math.Abs(x2 - P) == 2)
                        {
                            rPeon2 = 1;
                            miTablero.casilla[x2, y2].peonAcabadeSaltarDoble = true;
                        }
                    }

                    DetectarSiHuboEnroque(x2, y2, P, Q, turno);
                    miTablero.casilla[x2, y2].nombre = auxnom;
                    miTablero.casilla[x2, y2].tipo = auxtip;
                    miTablero.casilla[x2, y2].equipo = auxequ;
                    miTablero.casilla[x2, y2].contPasos = auxcont;
                    miTablero.casilla[x2, y2].figura = auxImg;


                    miTablero.casilla[P, Q].nombre = miTablero.celdavacia.nombre;
                    miTablero.casilla[P, Q].tipo = miTablero.celdavacia.tipo;
                    miTablero.casilla[P, Q].equipo = miTablero.celdavacia.equipo;
                    miTablero.casilla[P, Q].contPasos = miTablero.celdavacia.contPasos;
                    miTablero.casilla[P, Q].figura = miTablero.celdavacia.figura;

                    miTablero.casilla[x2, y2].contPasos = miTablero.casilla[x2, y2].contPasos + 1 + rPeon2;
                    rPeon2 = 0;
                    if (miTablero.casilla[x2, y2].tipo == "PeonN" || miTablero.casilla[x2, y2].tipo == "PeonB")
                    {
                        if (miTablero.casilla[x2, y2].contPasos == 6)
                        {
                            if (miTablero.casilla[x2, y2].tipo == "PeonN")
                            {
                                miTablero.casilla[x2, y2].nombre = "reina Negra 22";
                                miTablero.casilla[x2, y2].figura = Image.FromFile("reinann.png");
                            }
                            else if (miTablero.casilla[x2, y2].tipo == "PeonB")
                            {
                                miTablero.casilla[x2, y2].nombre = "reina Blanca 22";
                                miTablero.casilla[x2, y2].figura = Image.FromFile("reinabb.png");
                            }
                            miTablero.casilla[x2, y2].tipo = "Reina";
                        }
                    }
                    ChequearJaque(x2, y2, turno);

                    turno = turno * -1;

                }

                for (int i = 0; i < miTablero.Tamanio; i++)
                {
                    for (int j = 0; j < miTablero.Tamanio; j++)
                    {
                        botonesTab[i, j].BackgroundImage = miTablero.casilla[i, j].figura;
                        botonesTab[i, j].BackgroundImageLayout = ImageLayout.Zoom;

                    }
                }

                ColoresTablero();
                if (turno == 1)
                {
                    label1.Text = "WHITE'S TURN";
                }
                else if (turno == -1)
                {
                    label1.Text = "BLACK'S TURN";
                }

            }
        }

        private void ChequearJaque(int r1, int r2, int tu)
        {
            h_parahack = 0;
            xhack = 0;
            yhack = 0;
            int xrey = 0, yrey = 0;

            miTablero.atackDirr = false;
            for (int i = 0; i < miTablero.Tamanio; i++)
            {
                for (int j = 0; j < miTablero.Tamanio; j++)
                {
                    miTablero.casilla[i, j].zonaamenazada = false;
                    miTablero.casilla[i, j].ataqueactivohack = false;
                    miTablero.casilla[i, j].defensahack = false;
                    miTablero.casilla[i, j].bloquearjaque = false;
                    if (miTablero.casilla[i, j].tipo != "PeonB" && miTablero.casilla[i, j].tipo != "PeonN")
                    {
                        miTablero.MovLegales(miTablero, i, j, miTablero.casilla[i, j].tipo, tu, miTablero.casilla[i, j].contPasos);
                    }

                    else if (miTablero.casilla[i, j].tipo == "PeonB" || miTablero.casilla[i, j].tipo == "PeonN")
                    {
                        miTablero.PeonAmenaza(miTablero, i, j, miTablero.casilla[i, j].tipo, tu, miTablero.casilla[i, j].contPasos);
                    }
                    for (int k = 0; k < miTablero.Tamanio; k++)
                    {
                        for (int rr = 0; rr < miTablero.Tamanio; rr++)
                        {
                            if (miTablero.casilla[k, rr].tipo == "Rey" && miTablero.casilla[k, rr].permiso == true && tu == -1 * miTablero.casilla[k, rr].equipo && miTablero.hackeoencurso == false)
                            {

                                miTablero.hackeoencurso = true;
                                xrey = k;
                                yrey = rr;
                                xhack = i;
                                yhack = j;
                            }
                        }
                    }
                    miTablero.casilla[i, j].permiso = false;
                    miTablero.casilla[i, j].defensahack = false;
                }
            }
            if (miTablero.hackeoencurso == true)
            {

                miTablero.hackeoencurso = false;
                miTablero.atackDirr = true;

                miTablero.casilla[xhack, yhack].permiso = true;
                miTablero.AtaqueDirecto(miTablero, xhack, yhack, xrey, yrey, miTablero.casilla[xhack, yhack].tipo, tu, miTablero.casilla[xhack, yhack].contPasos);

                for (int i = 0; i < miTablero.Tamanio; i++)
                {
                    for (int j = 0; j < miTablero.Tamanio; j++)
                    {
                        if (miTablero.casilla[i, j].permiso == true)
                        {
                            miTablero.casilla[i, j].ataqueactivohack = true;
                        }

                        miTablero.casilla[i, j].permiso = false;
                    }
                }

                miTablero.atackDirr = false;
                miTablero.BloqueableAtaque(miTablero, xhack, yhack, xrey, yrey, miTablero.casilla[xhack, yhack].tipo, tu, miTablero.casilla[xhack, yhack].contPasos);
                miTablero.casilla[xhack, yhack].permiso = true;

                for (int i = 0; i < miTablero.Tamanio; i++)
                {
                    for (int j = 0; j < miTablero.Tamanio; j++)
                    {
                        if (miTablero.casilla[i, j].permiso == true)
                        {
                            miTablero.casilla[i, j].bloquearjaque = true;
                        }
                        miTablero.casilla[i, j].permiso = false;
                    }
                }

                for (int i = 0; i < miTablero.Tamanio; i++)
                {
                    for (int j = 0; j < miTablero.Tamanio; j++)
                    {
                        miTablero.casilla[i, j].permiso = false;
                        miTablero.casilla[i, j].defensahack = false;
                    }
                }
                miTablero.MovLegales(miTablero, xrey, yrey, miTablero.casilla[xrey, yrey].tipo, tu * -1, miTablero.casilla[xrey, yrey].contPasos);

                for (int i = 0; i < miTablero.Tamanio; i++)
                {
                    for (int j = 0; j < miTablero.Tamanio; j++)
                    {
                        if (miTablero.casilla[i, j].permiso == true && miTablero.casilla[i, j].equipo == tu)
                        {
                            miTablero.casilla[i, j].equipo = -1 * turno;
                            miTablero.casilla[i, j].copiaLadoRey = true;
                        }

                        miTablero.casilla[i, j].permiso = false;
                    }
                }

                for (int i = 0; i < miTablero.Tamanio; i++)
                {
                    for (int j = 0; j < miTablero.Tamanio; j++)
                    {
                        if (miTablero.casilla[i, j].equipo == tu)
                        {
                            if (miTablero.casilla[i, j].tipo != "PeonB" && miTablero.casilla[i, j].tipo != "PeonN")
                            {
                                miTablero.MovLegales(miTablero, i, j, miTablero.casilla[i, j].tipo, tu, miTablero.casilla[i, j].contPasos);
                            }

                            else if (miTablero.casilla[i, j].tipo == "PeonB" || miTablero.casilla[i, j].tipo == "PeonN")
                            {
                                miTablero.PeonAmenaza(miTablero, i, j, miTablero.casilla[i, j].tipo, tu, miTablero.casilla[i, j].contPasos);
                            }
                        }
                    }
                }


                for (int i = 0; i < miTablero.Tamanio; i++)
                {
                    for (int j = 0; j < miTablero.Tamanio; j++)
                    {
                        if (miTablero.casilla[i, j].permiso == true)
                        {
                            miTablero.casilla[i, j].zonaamenazada = true;
                        }
                        miTablero.casilla[i, j].permiso = false;

                    }
                }
                ProteccionAtaqueContiguoRey(tu);
                for (int i = 0; i < miTablero.Tamanio; i++)
                {
                    for (int j = 0; j < miTablero.Tamanio; j++)
                    {

                        if (miTablero.casilla[i, j].copiaLadoRey == true)
                        {
                            miTablero.casilla[i, j].equipo = tu;
                        }

                        miTablero.casilla[i, j].permiso = false;
                        miTablero.casilla[i, j].copiaLadoRey = false;
                    }
                }

                for (int i = 0; i < miTablero.Tamanio; i++)
                {
                    for (int j = 0; j < miTablero.Tamanio; j++)
                    {
                        /*
                        if (miTablero.casilla[i,j].tipo=="PeonN" || miTablero.casilla[i, j].tipo == "PeonN")
                        {
                            miTablero.hackeoencurso = true;
                            miTablero.MovLegales(miTablero, i, j, miTablero.casilla[i, j].tipo, tu, miTablero.casilla[i, j].contPasos);
                            miTablero.hackeoencurso = false;
                        }
                        else
                        {
                        */
                        if (miTablero.casilla[i, j].tipo != "PeonB" && miTablero.casilla[i, j].tipo != "PeonN")
                        {
                            miTablero.MovLegales(miTablero, i, j, miTablero.casilla[i, j].tipo, tu, miTablero.casilla[i, j].contPasos);
                        }

                        else if (miTablero.casilla[i, j].tipo == "PeonB" || miTablero.casilla[i, j].tipo == "PeonN")
                        {
                            miTablero.PeonAmenaza(miTablero, i, j, miTablero.casilla[i, j].tipo, tu, miTablero.casilla[i, j].contPasos);
                        }

                    }
                }


                for (int i = 0; i < miTablero.Tamanio; i++)
                {
                    for (int j = 0; j < miTablero.Tamanio; j++)
                    {
                        if (miTablero.casilla[i, j].permiso == true)
                        {

                            miTablero.casilla[i, j].zonaamenazada = true;
                        }

                    }
                }

                for (int i = 0; i < miTablero.Tamanio; i++)
                {
                    for (int j = 0; j < miTablero.Tamanio; j++)
                    {
                        miTablero.casilla[i, j].permiso = false;
                        miTablero.casilla[i, j].defensahack = false;
                    }
                }

                miTablero.hackeoencurso = true;
                for (int i = 0; i < miTablero.Tamanio; i++)
                {
                    for (int j = 0; j < miTablero.Tamanio; j++)
                    {
                        miTablero.MovLegales(miTablero, i, j, miTablero.casilla[i, j].tipo, tu * -1, miTablero.casilla[i, j].contPasos);
                    }
                }

                miTablero.hackeoencurso = false;

                miTablero.MovLegales(miTablero, xrey, yrey, miTablero.casilla[xrey, yrey].tipo, tu * -1, miTablero.casilla[xrey, yrey].contPasos);
                if (miTablero.casilla[xhack, yhack].zonaamenazada == false && miTablero.casilla[xhack, yhack].permiso == true)
                {
                    miTablero.casilla[xhack, yhack].defensahack = true;
                }
                miTablero.hackeoencurso = true;

                for (int i = 0; i < miTablero.Tamanio; i++)
                {
                    for (int j = 0; j < miTablero.Tamanio; j++)
                    {
                        if (miTablero.casilla[i, j].defensahack == true)
                        {
                            h_parahack++;
                        }
                        if (miTablero.casilla[i, j].zonaamenazada == true)
                        {
                        }
                    }
                }

                if (h_parahack > 0)
                {
                    MessageBox.Show("JAQUE");
                    for (int i = 0; i < miTablero.Tamanio; i++)
                    {
                        for (int j = 0; j < miTablero.Tamanio; j++)
                        {
                            miTablero.casilla[i, j].permiso = false;
                            miTablero.casilla[i, j].defensahack = false;
                        }
                    }
                }
                else if (h_parahack == 0)
                {
                    if (tu == 1)
                    {
                        MessageBox.Show("CHECKMATE! \nWHITE WINS!\nGAME OVER");
                        label1.Text = "WHITE WINS!";
                        label1.BackColor = Color.Red;
                    }
                    else if (tu == -1)
                    {
                        MessageBox.Show("CHECKMATE! \nBLACK WINS!\nGAME OVER");
                        label1.Text = "BLACK WINS!";
                        label1.BackColor = Color.Red;
                    }
                    turno = 100;
                }
            }
            else
            {
                xhack = 0;
                yhack = 0;

            }


        }

        private void ColoresTablero()
        {
            for (int i = 0; i < miTablero.Tamanio; i++)
            {
                for (int j = 0; j < miTablero.Tamanio; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        botonesTab[i, j].BackColor = Color.White;
                    }
                    else
                    {
                        botonesTab[i, j].BackColor = Color.Brown;
                    }
                }
            }
        }

        private void DetectarSiHuboEnroque(int rr1, int rr2, int pq1, int pq2, int turrr)
        {
            if (miTablero.casilla[pq1, pq2].tipo == "Rey" && miTablero.casilla[pq1, pq2].contPasos == 0)
            {
                int AA = 5;
                if (turrr == 1)
                {
                    AA = 7;
                }
                else if (turrr == -1)
                {
                    AA = 0;
                }
                string copiaTorNom = "", copiaTorTip = "";
                int copiaTorEqu = 5, copiaTorCont = 5;
                Image copiaTorImg;

                if (pq1 == AA && pq2 == 4)
                {

                    if (rr1 == AA && rr2 == 6)
                    {

                        copiaTorNom = miTablero.casilla[AA, 7].nombre;
                        copiaTorTip = miTablero.casilla[AA, 7].tipo;
                        copiaTorEqu = miTablero.casilla[AA, 7].equipo;
                        copiaTorCont = miTablero.casilla[AA, 7].contPasos;
                        copiaTorImg = miTablero.casilla[AA, 7].figura;

                        miTablero.casilla[AA, 7].nombre = miTablero.celdavacia.nombre;
                        miTablero.casilla[AA, 7].tipo = miTablero.celdavacia.tipo;
                        miTablero.casilla[AA, 7].equipo = miTablero.celdavacia.equipo;
                        miTablero.casilla[AA, 7].contPasos = miTablero.celdavacia.contPasos;
                        miTablero.casilla[AA, 7].figura = miTablero.celdavacia.figura;


                        miTablero.casilla[AA, 5].nombre = copiaTorNom;
                        miTablero.casilla[AA, 5].tipo = copiaTorTip;
                        miTablero.casilla[AA, 5].equipo = copiaTorEqu;
                        miTablero.casilla[AA, 5].contPasos = copiaTorCont + 1;
                        miTablero.casilla[AA, 5].figura = copiaTorImg;
                    }
                    else if (rr1 == AA && rr2 == 2)
                    {
                        copiaTorNom = miTablero.casilla[AA, 0].nombre;
                        copiaTorTip = miTablero.casilla[AA, 0].tipo;
                        copiaTorEqu = miTablero.casilla[AA, 0].equipo;
                        copiaTorCont = miTablero.casilla[AA, 0].contPasos;
                        copiaTorImg = miTablero.casilla[AA, 0].figura;

                        miTablero.casilla[AA, 0].nombre = miTablero.celdavacia.nombre;
                        miTablero.casilla[AA, 0].tipo = miTablero.celdavacia.tipo;
                        miTablero.casilla[AA, 0].equipo = miTablero.celdavacia.equipo;
                        miTablero.casilla[AA, 0].contPasos = miTablero.celdavacia.contPasos;
                        miTablero.casilla[AA, 0].figura = miTablero.celdavacia.figura;

                        miTablero.casilla[AA, 3].nombre = copiaTorNom;
                        miTablero.casilla[AA, 3].tipo = copiaTorTip;
                        miTablero.casilla[AA, 3].equipo = copiaTorEqu;
                        miTablero.casilla[AA, 3].contPasos = copiaTorCont + 1;
                        miTablero.casilla[AA, 3].figura = copiaTorImg;
                    }
                }

            }
        }

        private void DetectarSiHuboPeonAlPaso(int xx2, int yy2, int pp1x, int pp2y, int turpp)
        {

            if ((miTablero.casilla[pp1x, pp2y].tipo == "PeonB" && pp1x == 3) || (miTablero.casilla[pp1x, pp2y].tipo == "PeonN" && pp1x == 4))
            {
                int arr = 0;
                if (miTablero.casilla[pp1x, pp2y].tipo == "PeonB")
                {
                    arr = 1;
                }
                else if (miTablero.casilla[pp1x, pp2y].tipo == "PeonN")
                {
                    arr = -1;
                }

                if (xx2 == pp1x - arr)
                {

                    if ((yy2 == pp2y - 1 || yy2 == pp2y + 1) && miTablero.casilla[xx2, yy2].equipo == 0)
                    {

                        if (miTablero.casilla[pp1x, yy2].peonAcabadeSaltarDoble == true && miTablero.casilla[pp1x, yy2].equipo == -1 * turpp)
                        {
                            miTablero.casilla[pp1x, yy2].nombre = miTablero.celdavacia.nombre;
                            miTablero.casilla[pp1x, yy2].tipo = miTablero.celdavacia.tipo;
                            miTablero.casilla[pp1x, yy2].equipo = miTablero.celdavacia.equipo;
                            miTablero.casilla[pp1x, yy2].contPasos = miTablero.celdavacia.contPasos;
                            miTablero.casilla[pp1x, yy2].figura = miTablero.celdavacia.figura;
                        }
                    }
                }
            }
            for (int i = 0; i < miTablero.Tamanio; i++)
            {
                for (int j = 0; j < miTablero.Tamanio; j++)
                {
                    miTablero.casilla[i, j].peonAcabadeSaltarDoble = false;
                }
            }
        }

        private void ProteccionAtaqueContiguoRey(int ttuurr)
        {
            for (int i = 0; i < miTablero.Tamanio; i++)
            {
                for (int j = 0; j < miTablero.Tamanio; j++)
                {

                    if (miTablero.casilla[i, j].copiaLadoRey == true)
                    {
                        miTablero.casilla[i, j].equipo = ttuurr;

                        if (miTablero.casilla[i, j].tipo != "PeonB" && miTablero.casilla[i, j].tipo != "PeonN")
                        {
                            miTablero.MovLegales(miTablero, i, j, miTablero.casilla[i, j].tipo, ttuurr, miTablero.casilla[i, j].contPasos);
                        }

                        else if (miTablero.casilla[i, j].tipo == "PeonB" || miTablero.casilla[i, j].tipo == "PeonN")
                        {
                            miTablero.PeonAmenaza(miTablero, i, j, miTablero.casilla[i, j].tipo, ttuurr, miTablero.casilla[i, j].contPasos);
                        }

                        for (int k = 0; k < miTablero.Tamanio; k++)
                        {
                            for (int mm = 0; mm < miTablero.Tamanio; mm++)
                            {
                                if (miTablero.casilla[k, mm].permiso == true)
                                {
                                    miTablero.casilla[k, mm].zonaamenazada = true;
                                }
                                miTablero.casilla[k, mm].permiso = false;
                            }
                        }
                        miTablero.casilla[i, j].equipo = -1 * ttuurr;
                    }
                }
            }
        }


    }
}
