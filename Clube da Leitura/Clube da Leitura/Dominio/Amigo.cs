using System;
using System.Collections.Generic;
using System.Text;

namespace Clube_da_Leitura.Dominio
{
    class Amigo
    {
        public int id;
        public string nome;
        public string responsavel;
        public string telefone;
        public string endereco;
        public bool status_emprestimo;

        public Amigo()
        {
            id = GeradorId.GerarIdAmigo();
        }

        public Amigo(int idSelecionado)
        {
            id = idSelecionado;
        }

        public string Restricao()
        {
            string resultadoValidacao = "";

            if (string.IsNullOrEmpty(nome))
                resultadoValidacao += "O campo nome é obrigatório \n";

            if (string.IsNullOrEmpty(responsavel))
                resultadoValidacao += "O campo responsável é obrigatório \n";

            if (string.IsNullOrEmpty(telefone))
                resultadoValidacao += "O campo telefone é obrigatório \n";

            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "AMIGO_VALIDO";

            return resultadoValidacao;
        }

        public override bool Equals(object obj)
        {
            Amigo amigo = (Amigo)obj;

            if (id == amigo.id)
                return true;
            else
                return false;
        }

    }
}
