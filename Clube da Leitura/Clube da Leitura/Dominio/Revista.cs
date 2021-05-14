using System;
using System.Collections.Generic;
using System.Text;

namespace Clube_da_Leitura.Dominio
{
    class Revista
    {
        public int id;
        public string tipo_colecao;
        public string numero_edicao;
        public string ano_revista;
        public Caixa caixa;

        public Revista()
        {
            id = GeradorId.GerarIdRevista();
        }

        public Revista(int idSelecionado)
        {
            id = idSelecionado;
        }

        public string Restricao()
        {
            string resultadoValidacao = "";

            if (string.IsNullOrEmpty(tipo_colecao))
                resultadoValidacao += "O campo tipo da coleção é obrigatório \n";

            if (string.IsNullOrEmpty(numero_edicao))
                resultadoValidacao += "O campo etiqueta é obrigatório \n";

            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "REVISTA_VALIDA";

            return resultadoValidacao;
        }

        public override bool Equals(object obj)
        {
            Revista revista = (Revista)obj;

            if (id == revista.id)
                return true;
            else
                return false;
        }

    }
}
