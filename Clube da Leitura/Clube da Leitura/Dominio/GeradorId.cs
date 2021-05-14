using System;
using System.Collections.Generic;
using System.Text;

namespace Clube_da_Leitura
{
    class GeradorId
    {
        private static int idAmigo = 0;
        private static int idCaixa = 0;
        private static int idRevista = 0;
        private static int idEmprestimo = 0;

        public static int GerarIdAmigo()
        {
            return ++idAmigo;
        }

        public static int GerarIdCaixa()
        {
            return ++idCaixa;
        }

        public static int GerarIdEmprestimo()
        {
            return ++idEmprestimo;
        }

        public static int GerarIdRevista()
        {
            return ++idRevista;
        }
    }
}
