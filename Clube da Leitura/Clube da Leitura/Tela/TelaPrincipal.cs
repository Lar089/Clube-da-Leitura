using Clube_da_Leitura.Controlador;
using Clube_da_Leitura.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clube_da_Leitura.Tela
{
    class TelaPrincipal : TelaBase
    {
        private readonly ControladorCaixa controladorCaixa;
        private readonly ControladorAmigo controladorAmigo;
        private readonly ControladorRevista controladorRevista;
        private readonly ControladorEmprestimo controladorEmprestimo;

        public TelaPrincipal(ControladorAmigo ctlrAmigo, ControladorRevista ctlrRevista,
            ControladorCaixa ctlrCaixa, ControladorEmprestimo ctlrEmprestimo) 
            : base("Tela Principal")
        {
            controladorAmigo = ctlrAmigo;
            controladorCaixa = ctlrCaixa;
            controladorRevista = ctlrRevista;
            controladorEmprestimo = ctlrEmprestimo;
        }

        public ICadastravel ObterOpcao()
        {
            ConfigurarTela("Escolha uma opção: ");

            ICadastravel telaSelecionada = null;
            string opcao;
            do
            {
                Console.WriteLine("Digite 1 para o Cadastro de Amigos");
                Console.WriteLine("Digite 2 para o Controle de Caixas");
                Console.WriteLine("Digite 3 para o Cadastro de Revistas");
                Console.WriteLine("Digite 4 para o Controle de Emprestimos");

                Console.WriteLine("Digite S para Sair");

                opcao = Console.ReadLine();

                if (opcao == "1")
                    telaSelecionada = new TelaAmigo(controladorAmigo);

                else if (opcao == "2")
                    telaSelecionada = new TelaCaixa(controladorCaixa);

                else if (opcao == "3")
                    telaSelecionada = new TelaRevista(controladorRevista, controladorCaixa);
                
                else if (opcao == "4")
                    telaSelecionada = new TelaEmprestimo(controladorEmprestimo, controladorRevista, controladorAmigo);

                else if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    telaSelecionada = null;

            } while (OpcaoInvalida(opcao));

            return telaSelecionada;
        }

        private bool OpcaoInvalida(string opcao)
        {
            if (opcao != "1" && opcao != "2" && opcao != "3" && opcao != "4" && opcao != "S" && opcao != "s")
            {
                ApresentarMensagem("Opção inválida", TipoMensagem.Erro);
                return true;
            }
            else
                return false;
        }
    }
}
