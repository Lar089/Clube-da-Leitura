using Clube_da_Leitura.Interface;
using Clube_da_Leitura.Controlador;
using Clube_da_Leitura.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clube_da_Leitura.Tela
{
    class TelaCaixa : TelaBase, ICadastravel, IEditavel
    {
        private ControladorCaixa controladorCaixa;

        public TelaCaixa(ControladorCaixa controladorCaixa)
            : base("Cadastro das Caixas")
        {
            this.controladorCaixa = controladorCaixa;
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando uma novo caixa...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número da caixa que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());


            bool conseguiuEditar = GravarCaixa(id);

            if (conseguiuEditar)
                ApresentarMensagem("Caixa editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar editar o caixa", TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo uma novo caixa...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número da caixa que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());


            bool conseguiuExcluir = controladorCaixa.ExcluirCaixa(id);

            if (conseguiuExcluir)
                ApresentarMensagem("Caixa excluído com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir o caixa", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo uma novo Caixa...");

            bool conseguiuGravar = GravarCaixa(0);

            if (conseguiuGravar)
                ApresentarMensagem("Caixa inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar inserir o caixa", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public void VisualizarRegistros()
        {
            ConfigurarTela("Visualizando Caixas...");

            string configuracaColunasTabela = "{0,-10} | {1,-15} | {2,-15} | {3,-45}";

            MontarCabecalhoTabela();

            Caixa[] caixas = controladorCaixa.SelecionarTodosCaixas();

            if (caixas.Length == 0)
            {
                ApresentarMensagem("Nenhuma caixa cadastrado!", TipoMensagem.Atencao);
                return;
            }

            for (int i = 0; i < caixas.Length; i++)
            {
                Console.WriteLine(configuracaColunasTabela,
                   caixas[i].id, caixas[i].cor, caixas[i].etiqueta, caixas[i].numero);
            }
        }

        public string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir uma novo caixa");
            Console.WriteLine("Digite 2 para visualizar caixas");
            Console.WriteLine("Digite 3 para editar uma caixa");
            Console.WriteLine("Digite 4 para excluir uma caixa");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        #region Métodos Privados
        private bool GravarCaixa(int id)
        {
            string resultadoValidacao;
            bool conseguiuGravar = true;

            Console.Write("Digite o cor da caixa: ");
            string cor = Console.ReadLine();

            Console.Write("Digite o etiqueta da caixa: ");
            string etiqueta = Console.ReadLine();

            Console.Write("Digite o numero da caixa: ");
            string numero = Console.ReadLine();

            resultadoValidacao = controladorCaixa.RegistrarCaixa(
                id, cor, etiqueta, numero);

            if (resultadoValidacao != "CAIXA_VALIDA")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;

        }

        private void MontarCabecalhoTabela()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("{0,-10} | {1,-15} | {2,-15} | {3,-45}", "Id", "Cor", "Etiqueta", "Número");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }
        #endregion
    }
}
