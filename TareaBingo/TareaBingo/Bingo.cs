using System;
using System.Collections.Generic;
using System.Text;

namespace TareaBingo
{
    class Bingo
    {
        private List<int> bolillasDispo;
        private Random random = new Random();

        public Bingo()
        {
            bolillasDispo = new List<int>();
            for (int i = 1; i <= 75; i++)
            {
                bolillasDispo.Add(i);
            }
        }

        public int SacarBolilla()
        {
            if (bolillasDispo.Count == 0)
            {
                throw new InvalidOperationException("Se han agotado todas las bolillas.");
            }

            int index = random.Next(bolillasDispo.Count);
            int bolilla = bolillasDispo[index];
            bolillasDispo.RemoveAt(index);
            return bolilla;
        }

        public string ObtenerLetra(int numero)
        {
            if (numero <= 15) return "B";
            if (numero <= 30) return "I";
            if (numero <= 45) return "N";
            if (numero <= 60) return "G";
            return "O";
        }
    }
}

