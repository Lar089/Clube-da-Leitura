using Clube_da_Leitura.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clube_da_Leitura.Controlador
{
    class ControladorEmprestimo : ControladorBase
    {

        private ControladorAmigo controladorAmigo;
        private ControladorRevista controladorRevista;

        public ControladorEmprestimo(ControladorAmigo ctrlAmigo, ControladorRevista ctrlRevista)
        {
            controladorAmigo = ctrlAmigo;
            controladorRevista = ctrlRevista;
        }

        public string RegistrarEmprestimo(int id, int idAmigo, int idRevista)
        {
            Emprestimo emprestimo;

            emprestimo = new Emprestimo();
            int posicao = ObterPosicaoVaga();

            string resultadoValidacao = "";

            emprestimo.amigo = controladorAmigo.SelecionarAmigoPorId(idAmigo);
            emprestimo.revista = controladorRevista.SelecionarRevistaPorId(idRevista);
            emprestimo.dataEmprestimo = DateTime.Now;
            emprestimo.dataDevolucao = DateTime.Now.AddDays(30);

            if (emprestimo.amigo.status_emprestimo != true)
            {
                emprestimo.amigo.status_emprestimo = true;

                resultadoValidacao = emprestimo.Restricao();

                if (resultadoValidacao == "EMPRESTIMO_VALIDO")
                    registros[posicao] = emprestimo;
            }

            return resultadoValidacao;
        }

        public bool FinalizarRegistro(int id)
        {
            if (id == 0)
                return false;

            Emprestimo emprestimo;

            int posicao = ObterPosicaoOcupada(new Emprestimo(id));
            emprestimo = (Emprestimo)registros[posicao];

            emprestimo.dataDevolucao = DateTime.Now;
            emprestimo.amigo.status_emprestimo = false;

            string resultadoValidacao = emprestimo.Restricao();

            if (resultadoValidacao == "EMPRESTIMO_VALIDO")
                return true;

            return false;

        }

        public Emprestimo[] SelecionarEmprestimosFinalizados(string id)
        {
            int j = 0;
            Emprestimo[] emprestimosAux = new Emprestimo[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), emprestimosAux, emprestimosAux.Length);

            Emprestimo[] emprestimosFechados = new Emprestimo[emprestimosAux.Length];

            for (int i = 0; i < emprestimosAux.Length; i++)
            {
                if (emprestimosAux[i].dataDevolucao <= DateTime.Now && emprestimosAux[i].dataDevolucao.ToString("M").Equals(id))
                {
                    emprestimosFechados[j] = emprestimosAux[i];
                    j++;
                }
            }

            if (j == 0)
            {
                Emprestimo[] emprestimosZerado = new Emprestimo[j];
                return emprestimosZerado;
            }

            return emprestimosFechados;
        }

        public Emprestimo[] SelecionarEmprestimosAbertos()
        {
            int j = 0;
            Emprestimo[] emprestimosAux = new Emprestimo[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), emprestimosAux, emprestimosAux.Length);

            Emprestimo[] emprestimosAbertos = new Emprestimo[emprestimosAux.Length];

            for (int i = 0; i < emprestimosAux.Length; i++)
            {
                if (emprestimosAux[i].dataDevolucao >= DateTime.Now)
                {
                    emprestimosAbertos[j] = emprestimosAux[i];
                    j++;
                }
            }

            if (j == 0)
            {
                Emprestimo[] emprestimosZerado = new Emprestimo[j];
                return emprestimosZerado;
            }

            return emprestimosAbertos;
        }
    }
}
