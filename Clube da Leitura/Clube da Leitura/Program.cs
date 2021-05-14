using Clube_da_Leitura.Controlador;
using Clube_da_Leitura.Interface;
using Clube_da_Leitura.Tela;
using System;

namespace Clube_da_Leitura
{
    class Program
    {
        static void Main(string[] args)
        {
            ControladorAmigo ctrlAmigo = new ControladorAmigo();

            ControladorCaixa ctrlCaixa = new ControladorCaixa();

            ControladorRevista ctrlRevista = new ControladorRevista(ctrlCaixa);

            ControladorEmprestimo ctrlEmprestimo = new ControladorEmprestimo(ctrlAmigo, ctrlRevista);

            TelaPrincipal telaPrincipal = new TelaPrincipal(ctrlAmigo, ctrlRevista, ctrlCaixa, ctrlEmprestimo);

            while (true)
            {
                ICadastravel telaSelecionada = telaPrincipal.ObterOpcao();

                if (telaSelecionada == null)
                    break;

                Console.Clear();

                if (telaSelecionada is TelaBase)
                    Console.WriteLine(((TelaBase)telaSelecionada).Titulo); Console.WriteLine();

                string opcao = telaSelecionada.ObterOpcao();

                if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    continue;

                if (opcao == "1")
                    telaSelecionada.InserirNovoRegistro();

                else if (opcao == "2")
                {
                    if (telaSelecionada is IEditavel)
                    {
                        ((IEditavel)telaSelecionada).VisualizarRegistros();
                    }
                    else
                    {
                        ((IRegistro)telaSelecionada).VisualizarRegistrosFechados();
                    }
                        Console.ReadLine();
                }

                else if (opcao == "3")
                    if (telaSelecionada is IEditavel)
                    {
                        ((IEditavel)telaSelecionada).EditarRegistro();
                    }
                    else
                    {
                        ((IRegistro)telaSelecionada).VisualizarRegistrosAbertos();
                    }

                else if (opcao == "4")
                    if (telaSelecionada is IEditavel)
                    {
                        ((IEditavel)telaSelecionada).ExcluirRegistro();
                    }
                    else
                    {
                        ((IRegistro)telaSelecionada).FinalizarRegistro();
                    }

                Console.Clear();
            }
        }
    }
}
