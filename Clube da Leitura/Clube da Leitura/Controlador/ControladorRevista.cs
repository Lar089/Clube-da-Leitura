using Clube_da_Leitura.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clube_da_Leitura.Controlador
{
    class ControladorRevista : ControladorBase
    {
        private ControladorCaixa controladorCaixa;

        public ControladorRevista(ControladorCaixa ctrlCaixa)
        {
            controladorCaixa = ctrlCaixa;
        }

        public string RegistrarRevista(int id, string tipo_colecao, string numero_edicao, string ano_revista, int idCaixa)
        {
            Revista revista;

            int posicao;

            if (id == 0)
            {
                revista = new Revista();
                posicao = ObterPosicaoVaga();
            }
            else
            {
                posicao = ObterPosicaoOcupada(new Revista(id));
                revista = (Revista)registros[posicao];
            }

            revista.caixa = controladorCaixa.SelecionarCaixaPorId(idCaixa);
            revista.tipo_colecao = tipo_colecao;
            revista.numero_edicao = numero_edicao;
            revista.ano_revista = ano_revista;

            string resultadoValidacao = revista.Restricao();

            if (resultadoValidacao == "REVISTA_VALIDA")
                registros[posicao] = revista;

            return resultadoValidacao;
        }

        public Revista[] SelecionarTodosRevistas()
        {
            Revista[] revistasAux = new Revista[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), revistasAux, revistasAux.Length);

            return revistasAux;
        }

        public bool ExcluirRevista(int id)
        {
            return ExcluirRegistro(new Revista(id));
        }

        public Revista SelecionarRevistaPorId(int id)
        {
            return (Revista)SelecionarRegistroPorId(new Revista(id));
        }

    }
}
