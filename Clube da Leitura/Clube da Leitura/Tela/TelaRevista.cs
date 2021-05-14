using Clube_da_Leitura.Interface;
using Clube_da_Leitura.Controlador;
using Clube_da_Leitura.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clube_da_Leitura.Tela
{
    class TelaRevista : TelaBase, ICadastravel, IEditavel
    {
        private ControladorRevista controladorRevista;
        private ControladorCaixa controladorCaixa;

        public TelaRevista(ControladorRevista controladorRevista, ControladorCaixa controladorCaixa)
            : base("Cadastro das Revistas")
        {
            this.controladorRevista = controladorRevista;
            this.controladorCaixa = controladorCaixa;
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando uma novo revista...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número da revista que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());


            bool conseguiuEditar = GravarRevista(id);

            if (conseguiuEditar)
                ApresentarMensagem("Revista editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar editar o revista", TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo uma novo revista...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número da revista que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());


            bool conseguiuExcluir = controladorRevista.ExcluirRevista(id);

            if (conseguiuExcluir)
                ApresentarMensagem("Revista excluído com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir o revista", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo uma novo revista...");

            bool conseguiuGravar = GravarRevista(0);

            if (conseguiuGravar)
                ApresentarMensagem("Revista inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar inserir o revista", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public void VisualizarRegistros()
        {
            ConfigurarTela("Visualizando Revistas...");

            string configuracaColunasTabela = "{0,-10} | {1,-15} | {2,-15} | {3,-45} | {4,-15}";

            MontarCabecalhoTabela();

            Revista[] revistas = controladorRevista.SelecionarTodosRevistas();

            if (revistas.Length == 0)
            {
                ApresentarMensagem("Nenhuma revista cadastrado!", TipoMensagem.Atencao);
                return;
            }

            foreach (Revista revista in revistas)
            {
                Console.WriteLine(configuracaColunasTabela,
                   revista.id, revista.tipo_colecao, revista.numero_edicao, revista.ano_revista, revista.caixa.id);
            }
        }

        public string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir uma novo revista");
            Console.WriteLine("Digite 2 para visualizar revistas");
            Console.WriteLine("Digite 3 para editar uma revista");
            Console.WriteLine("Digite 4 para excluir uma revista");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        #region Métodos Privados
        private bool GravarRevista(int id)
        {
            string resultadoValidacao;
            bool conseguiuGravar = true;

            VisualizarCaixa();

            Console.Write("Digite o Id da caixa que está a revista: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite o tipo de coleção do revista: ");
            string tipo_colecao = Console.ReadLine();

            Console.Write("Digite o número da edição do revista: ");
            string numero_edicao = Console.ReadLine();

            Console.Write("Digite o ano da revista: ");
            string ano_revista = Console.ReadLine();

            resultadoValidacao = controladorRevista.RegistrarRevista(
                id, tipo_colecao, numero_edicao, ano_revista, idCaixa);

            if (resultadoValidacao != "REVISTA_VALIDA")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;

        }

        private void MontarCabecalhoTabela()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("{0,-10} | {1,-15} | {2,-15} | {3,-45} | {4,-15}", "Id", "Tipo de Coleção", "Número da Edição", "Ano", "Id da Caixa");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }

        private void VisualizarCaixa()
        {
            Console.WriteLine();
            Caixa[] caixas = controladorCaixa.SelecionarTodosCaixas();

            Console.WriteLine("{0,-10} | {1,-30}", "Id", "Número");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            foreach (var e in caixas)
            {
                Console.WriteLine("{0,-10} | {1,-30}", e.id, e.numero);
            }
            Console.WriteLine();
        }
        #endregion
    }
}
