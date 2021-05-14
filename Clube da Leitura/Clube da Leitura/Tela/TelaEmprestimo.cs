using Clube_da_Leitura.Controlador;
using Clube_da_Leitura.Dominio;
using Clube_da_Leitura.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clube_da_Leitura.Tela
{
    class TelaEmprestimo : TelaBase, ICadastravel, IRegistro
    {
        private ControladorEmprestimo controladorEmprestimo;
        private ControladorRevista controladorRevista;
        private ControladorAmigo controladorAmigo;

        public TelaEmprestimo(ControladorEmprestimo controladorEmprestimo, ControladorRevista controladorRevista, 
            ControladorAmigo controladorAmigo) : base("Cadastro dos Emprestimos")
        {
            this.controladorEmprestimo = controladorEmprestimo;
            this.controladorRevista = controladorRevista;
            this.controladorAmigo = controladorAmigo;
        }

        public void InserirNovoRegistro()
        {
            ConfigurarTela("Registrando um novo emprestimo...");

            bool conseguiuGravar = GravarEmprestimo(0);

            if (conseguiuGravar)
                ApresentarMensagem("Emprestimo realizado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar registrar o emprestimo", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public void FinalizarRegistro()
        {
            ConfigurarTela("Finalizando um emprestimo...");

            VisualizarRegistrosAbertos();

            Console.Write("Digite o Id do emprestimo que deseja finalizar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool conseguiuFinalizar = controladorEmprestimo.FinalizarRegistro(id);

            if (conseguiuFinalizar)
                ApresentarMensagem("Emprestimo finalizado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar finalizar o emprestimo", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public void VisualizarRegistrosFechados()
        {
            ConfigurarTela("Visualizando Emprestimos Finalizados...");

            Console.Write("Digite o mês que deseja ver os emprestimos que então finalizados: ");
            string id = Console.ReadLine();

            Emprestimo[] emprestimos = controladorEmprestimo.SelecionarEmprestimosFinalizados(id);

            if (emprestimos.Length == 0)
            {
                ApresentarMensagem("Nenhuma emprestimo registrado!", TipoMensagem.Atencao);
                return;
            }

            string configuracaColunasTabela = "{0,-10} | {1,-15} | {2,-15} | {3,-45} | {4,-15}";

            MontarCabecalhoTabela();

            for (int i = 0; i < emprestimos.Length; i++)
            {
                Console.WriteLine(configuracaColunasTabela,
                   emprestimos[i].id, emprestimos[i].dataEmprestimo, emprestimos[i].dataDevolucao, emprestimos[i].amigo.id, emprestimos[i].revista.id);
            }
        }

        public void VisualizarRegistrosAbertos()
        {
            ConfigurarTela("Visualizando Emprestimos Abertos...");

            string configuracaColunasTabela = "{0,-10} | {1,-15} | {2,-15} | {3,-45} | {4,-15}";

            MontarCabecalhoTabela();

            Emprestimo[] emprestimos = controladorEmprestimo.SelecionarEmprestimosAbertos();

            if (emprestimos.Length == 0)
            {
                ApresentarMensagem("Nenhuma emprestimo registrado!", TipoMensagem.Atencao);
                return;
            }

            for (int i = 0; i < emprestimos.Length; i++)
            {
                Console.WriteLine(configuracaColunasTabela,
                   emprestimos[i].id, emprestimos[i].dataEmprestimo, emprestimos[i].dataDevolucao, emprestimos[i].amigo.id, emprestimos[i].revista.id);
            }
        }

        public string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir um novo emprestimo");
            Console.WriteLine("Digite 2 para visualizar os empréstimos realizados no mês");
            Console.WriteLine("Digite 3 para visualizar emprestimos que estão em aberto");
            Console.WriteLine("Digite 4 para fechar um emprestimo");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        #region Métodos Privados
        private bool GravarEmprestimo(int id)
        {
            string resultadoValidacao;
            bool conseguiuGravar = true;

            VisualizarAmigos();

            Console.Write("Digite o Id do amigo que deseja fazer um emprestimo: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine());

            VisualizarRevistas();

            Console.Write("Digite o Id da revista para o emprestimo: ");
            int idRevista = Convert.ToInt32(Console.ReadLine());

            resultadoValidacao = controladorEmprestimo.RegistrarEmprestimo(id, idAmigo, idRevista);

            if (resultadoValidacao != "EMPRESTIMO_VALIDO")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }

        private void MontarCabecalhoTabela()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("{0,-10} | {1,-15} | {2,-15} | {3,-45} | {4,-15}", "Id", "Data do Emprestimo", "Data da Devolução", "Id do Amigo", "Id da Revista");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }

        private void VisualizarAmigos()
        {
            Console.WriteLine();
            Amigo[] amigos = controladorAmigo.SelecionarTodosAmigos();

            Console.WriteLine("{0,-10} | {1,-30}", "Id", "Nome");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            foreach (var e in amigos)
            {
                Console.WriteLine("{0,-10} | {1,-30}", e.id, e.nome);
            }
            Console.WriteLine();
        }

        private void VisualizarRevistas()
        {
            Console.WriteLine();
            Revista[] revistas = controladorRevista.SelecionarTodosRevistas();

            Console.WriteLine("{0,-10} | {1,-30}", "Id", "Tipo de Coleção");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            foreach (var e in revistas)
            {
                Console.WriteLine("{0,-10} | {1,-30}", e.id, e.tipo_colecao);
            }
            Console.WriteLine();
        }
        #endregion

    }
}
