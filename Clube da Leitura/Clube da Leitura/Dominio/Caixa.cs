using System;
using System.Collections.Generic;
using System.Text;

namespace Clube_da_Leitura.Dominio
{
    class Caixa
    {
        public int id;
        public string cor;
        public string etiqueta;
        public string numero;

        public Caixa()
        {
            id = GeradorId.GerarIdCaixa();
        }

        public Caixa(int idSelecionado)
        {
            id = idSelecionado;
        }

        public string Restricao()
        {
            string resultadoValidacao = "";

            if (string.IsNullOrEmpty(cor))
                resultadoValidacao += "O campo cor é obrigatório \n";

            if (string.IsNullOrEmpty(etiqueta))
                resultadoValidacao += "O campo etiqueta é obrigatório \n";

            if (string.IsNullOrEmpty(numero))
                resultadoValidacao += "O campo número é obrigatório \n";

            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "CAIXA_VALIDA";

            return resultadoValidacao;
        }

        public override bool Equals(object obj)
        {
            Caixa caixa = (Caixa)obj;

            if (id == caixa.id)
                return true;
            else
                return false;
        }
    }
}
