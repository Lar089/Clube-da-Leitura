using Clube_da_Leitura.Interface;
using Clube_da_Leitura.Controlador;
using Clube_da_Leitura.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clube_da_Leitura.Tela
{
    class TelaAmigo : TelaBase, ICadastravel, IEditavel
    {
        private ControladorAmigo controladorAmigo;

        public TelaAmigo(ControladorAmigo controladorAmigo) 
            : base("Cadastro dos Amigos")
        {
            this.controladorAmigo = controladorAmigo;
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando um novo amigo...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do amigo que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());


            bool conseguiuEditar = GravarAmigo(id);

            if (conseguiuEditar)
                ApresentarMensagem("Amigo editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar editar o amigo", TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo um novo amigo...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do amigo que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());


            bool conseguiuExcluir = controladorAmigo.ExcluirAmigo(id);

            if (conseguiuExcluir)
                ApresentarMensagem("Amigo excluído com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir o amigo", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo um novo amigo...");

            bool conseguiuGravar = GravarAmigo(0);

            if (conseguiuGravar)
                ApresentarMensagem("Amigo inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar inserir o amigo", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public void VisualizarRegistros()
        {
            ConfigurarTela("Visualizando Amigos...");

            string configuracaColunasTabela = "{0,-10} | {1,-15} | {2,-15} | {3,-45} | {4,-15}";

            MontarCabecalhoTabela();

            Amigo[] amigos = controladorAmigo.SelecionarTodosAmigos();

            if (amigos.Length == 0)
            {
                ApresentarMensagem("Nenhum amigo cadastrado!", TipoMensagem.Atencao);
                return;
            }

            for (int i = 0; i < amigos.Length; i++)
            {
                Console.WriteLine(configuracaColunasTabela,
                   amigos[i].id, amigos[i].nome, amigos[i].responsavel, amigos[i].telefone, amigos[i].endereco);
            }
        }

        public string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir novo amigo");
            Console.WriteLine("Digite 2 para visualizar amigos");
            Console.WriteLine("Digite 3 para editar um amigo");
            Console.WriteLine("Digite 4 para excluir um amigo");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        #region Métodos Privados
        private bool GravarAmigo(int id)
        {
            string resultadoValidacao;
            bool conseguiuGravar = true;

            Console.Write("Digite o nome do amigo: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o responsável do amigo: ");
            string responsavel = Console.ReadLine();

            Console.Write("Digite o telefone do amigo: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite o endereço do amigo: ");
            string endereco = Console.ReadLine();

            resultadoValidacao = controladorAmigo.RegistrarAmigo(
                id, nome, responsavel, telefone, endereco);

            if (resultadoValidacao != "AMIGO_VALIDO")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;

        }

        private void MontarCabecalhoTabela()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("{0,-10} | {1,-15} | {2,-15} | {3,-45} | {4,-15}", "Id", "Nome", "Responsável", "Telefone", "Endereço");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }
        #endregion
    }
}
