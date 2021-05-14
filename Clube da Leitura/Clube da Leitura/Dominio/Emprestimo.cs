using System;
using System.Collections.Generic;
using System.Text;

namespace Clube_da_Leitura.Dominio
{
    class Emprestimo
    {
        public int id;
        public DateTime dataEmprestimo;
        public DateTime dataDevolucao;
        public Amigo amigo;
        public Revista revista;

        public Emprestimo()
        {
            id = GeradorId.GerarIdEmprestimo();
        }

        public Emprestimo(int idSelecionado)
        {
            id = idSelecionado;
        }

        public string Restricao()
        {
            string resultadoValidacao = "";

            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "EMPRESTIMO_VALIDO";

            return resultadoValidacao;
        }
    }
}
