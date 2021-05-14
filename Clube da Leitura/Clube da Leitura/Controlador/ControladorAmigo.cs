using Clube_da_Leitura.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clube_da_Leitura.Controlador
{
    class ControladorAmigo : ControladorBase
    {
        public string RegistrarAmigo(int id, string nome, string responsavel, string telefone, string endereco)
        {
            Amigo amigo;

            int posicao;

            if (id == 0)
            {
                amigo = new Amigo();
                posicao = ObterPosicaoVaga();
            }
            else
            {
                posicao = ObterPosicaoOcupada(new Amigo(id));
                amigo = (Amigo)registros[posicao];
            }

            amigo.nome = nome;
            amigo.responsavel = responsavel;
            amigo.telefone = telefone;
            amigo.endereco = endereco;
            amigo.status_emprestimo = false;

            string resultadoValidacao = amigo.Restricao();

            if (resultadoValidacao == "AMIGO_VALIDO")
                registros[posicao] = amigo;

            return resultadoValidacao;
        }

        public Amigo[] SelecionarTodosAmigos()
        {
            Amigo[] amigosAux = new Amigo[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), amigosAux, amigosAux.Length);

            return amigosAux;
        }

        public bool ExcluirAmigo(int idSelecionado)
        {
            return ExcluirRegistro(new Amigo(idSelecionado));
        }

        public Amigo SelecionarAmigoPorId(int id)
        {
            return (Amigo)SelecionarRegistroPorId(new Amigo(id));
        }
    }
}
