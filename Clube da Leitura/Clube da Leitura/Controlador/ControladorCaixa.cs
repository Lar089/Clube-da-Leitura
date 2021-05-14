using Clube_da_Leitura.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clube_da_Leitura.Controlador
{
    class ControladorCaixa : ControladorBase
    {
        public string RegistrarCaixa(int id, string cor, string etiqueta, string numero)
        {
            Caixa caixa;

            int posicao;

            if (id == 0)
            {
                caixa = new Caixa();
                posicao = ObterPosicaoVaga();
            }
            else
            {
                posicao = ObterPosicaoOcupada(new Caixa(id));
                caixa = (Caixa)registros[posicao];
            }

            caixa.cor = cor;
            caixa.etiqueta = etiqueta;
            caixa.numero = numero;

            string resultadoValidacao = caixa.Restricao();

            if (resultadoValidacao == "CAIXA_VALIDA")
                registros[posicao] = caixa;

            return resultadoValidacao;
        }

        public Caixa[] SelecionarTodosCaixas()
        {
            Caixa[] caixasAux = new Caixa[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), caixasAux, caixasAux.Length);

            return caixasAux;
        }

        public bool ExcluirCaixa(int idSelecionado)
        {
            return ExcluirRegistro(new Caixa(idSelecionado));
        }

        public Caixa SelecionarCaixaPorId(int id)
        {
            return (Caixa)SelecionarRegistroPorId(new Caixa(id));
        }
    }
}
