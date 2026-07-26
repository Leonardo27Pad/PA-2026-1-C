using System;
using System.Collections.Generic;
using System.Text;

namespace TareaBingo
{
    class Cartilla
    {
        public Casilla[,] Matriz { get; private set; }
        private Random random = new Random();

        public Cartilla()
        {
            Matriz = new Casilla[5, 5];
        }

        public void Generar()
        {
            int[][] rangos = new int[][]
            {
                new int[] { 1, 15 },   // B
                new int[] { 16, 30 },  // I
                new int[] { 31, 45 },  // N
                new int[] { 46, 60 },  // G
                new int[] { 61, 75 }   // O
            };

            for (int col = 0; col < 5; col++)
            {
                List<int> numerosColumna = GenerarNumerosUnicos(rangos[col][0], rangos[col][1], 5);

                for (int fila = 0; fila < 5; fila++)
                {
                    if (col == 2 && fila == 2)
                    {
                        Matriz[fila, col] = new Casilla(0, esEspacioLibre: true);
                    }
                    else
                    {
                        Matriz[fila, col] = new Casilla(numerosColumna[fila]);
                    }
                }
            }
        }

        public void MarcarNumero(int numero)
        {
            for (int f = 0; f < 5; f++)
            {
                for (int c = 0; c < 5; c++)
                {
                    if (!Matriz[f, c].EsEspacioLibre && Matriz[f, c].Numero == numero)
                    {
                        Matriz[f, c].Marcado = true;
                    }
                }
            }
        }

        public bool EsLineaCompleta()
        {
            //Revisar Filas
            for (int f = 0; f < 5; f++)
            {
                bool completa = true;
                for (int c = 0; c < 5; c++)
                {
                    if (!Matriz[f, c].Marcado) { completa = false; break; }
                }
                if (completa) return true;
            }

            //Revisar Columnas
            for (int c = 0; c < 5; c++)
            {
                bool completa = true;
                for (int f = 0; f < 5; f++)
                {
                    if (!Matriz[f, c].Marcado) { completa = false; break; }
                }
                if (completa) return true;
            }

            //Revisar Diagonal \
            bool diag1 = true;
            for (int i = 0; i < 5; i++)
            {
                if (!Matriz[i, i].Marcado) { diag1 = false; break; }
            }
            if (diag1) return true;

            //Revisar Diagonal /
            bool diag2 = true;
            for (int i = 0; i < 5; i++)
            {
                if (!Matriz[i, 4 - i].Marcado) { diag2 = false; break; }
            }
            if (diag2) return true;

            return false;
        }

        public void MarcarCasillasGanadoras()
        {
            // Filas
            for (int f = 0; f < 5; f++)
            {
                bool completa = true;
                for (int c = 0; c < 5; c++)
                {
                    if (!Matriz[f, c].Marcado) { completa = false; break; }
                }
                if (completa)
                {
                    for (int c = 0; c < 5; c++) Matriz[f, c].EsGanadora = true;
                }
            }

            // Columnas
            for (int c = 0; c < 5; c++)
            {
                bool completa = true;
                for (int f = 0; f < 5; f++)
                {
                    if (!Matriz[f, c].Marcado) { completa = false; break; }
                }
                if (completa)
                {
                    for (int f = 0; f < 5; f++) Matriz[f, c].EsGanadora = true;
                }
            }

            // Diagonal \
            bool d1 = true;
            for (int i = 0; i < 5; i++)
            {
                if (!Matriz[i, i].Marcado) { d1 = false; break; }
            }
            if (d1)
            {
                for (int i = 0; i < 5; i++) Matriz[i, i].EsGanadora = true;
            }

            // Diagonal /
            bool d2 = true;
            for (int i = 0; i < 5; i++)
            {
                if (!Matriz[i, 4 - i].Marcado) { d2 = false; break; }
            }
            if (d2)
            {
                for (int i = 0; i < 5; i++) Matriz[i, 4 - i].EsGanadora = true;
            }
        }

        private List<int> GenerarNumerosUnicos(int min, int max, int cantidad)
        {
            List<int> numeros = new List<int>();
            while (numeros.Count < cantidad)
            {
                int num = random.Next(min, max + 1);
                if (!numeros.Contains(num))
                {
                    numeros.Add(num);
                }
            }
            return numeros;
        }
    }
}