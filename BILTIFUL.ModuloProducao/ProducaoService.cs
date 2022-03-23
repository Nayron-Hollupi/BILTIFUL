using BILTIFUL.Core;
using BILTIFUL.Core.Controles;
using BILTIFUL.Core.Entidades;
using BILTIFUL.Core.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BILTIFUL.ModuloProducao
{
    public class ProducaoService
    {
        public MPrima dbmateriaprima = new MPrima();
        public Produto dbproduto = new Produto();
        public ItemProducao dbitemproducao = new ItemProducao();
        public Producao dbproducao = new Producao();
         CadastroService cadastroService = new CadastroService();
  
    
        public void SubMenu()
        {
            string opcao = "";


            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t __________________________________________________");
            Console.WriteLine("\t\t\t\t\t|+++++++++++++++++++| PRODUÇÃO |+++++++++++++++++++|");
            Console.WriteLine("\t\t\t\t\t|1| - ADICIONAR PRODUÇÃO                           |");
            Console.WriteLine("\t\t\t\t\t|2| - LOCALIZAR PRODUÇÃO                           |");
            Console.WriteLine("\t\t\t\t\t|3| - EXIBIR PRODUÇÃO CADASTRADAS                  |");
            Console.WriteLine("\t\t\t\t\t|0| - SAIR                                         |");
            Console.Write("\t\t\t\t\t|__________________________________________________|\n" +
                          "\t\t\t\t\t|Opção: ");


            opcao = Console.ReadLine();

            switch (opcao)
            {
                case "0":
                    break;

                case "1":
                    Console.Clear();
                    if (!dbmateriaprima.VerificaMPrima())
                    {
                        EntradaDadosProducao();
                    }
                    else
                    {
                        Console.WriteLine("\n\t\t\tNenhuma Materia Prima cadastrada no sistema.");
                        BackMenu();
                    }
                    break;

                case "2":
                    Console.Clear();
                    if (!ProducaoVazia()) Localizar();
                    break;

                case "3":
                    Console.Clear();
                    if (!ProducaoVazia()) ImpressaoDoRegistro();
                    break;
                default:
                    Console.WriteLine("\t\t\t\t\tOpção inválida! ");
                    Console.ReadKey();
                    SubMenu();
                    break;
            }

        }

        public void BackMenu()
        {
            Console.Write("\n\t\t\t Pressione qualquer tecla para voltar ao menu de Produção...");
            Console.ReadKey();
            Console.Clear();
            SubMenu();
        }

        public bool ProducaoVazia()
        {
            if (cadastroService.cadastros.producao.Count == 0)
            {
                Console.WriteLine("\n\t\t\tNenhuma produção cadastrada no sistema.");
                BackMenu();
                return true;
            }

            return false;
        }

        public void EntradaDadosProducao()
        {
          
            string nomeProduto ="0";
            if (dbproduto.ProdutoCadastrado()) {
                Console.WriteLine("\n\t\t\tNenhum produto cadastrado");
            }

            Console.Write("\n\t\t\tDeseja cadastrar um novo produto para Produção (S/N): ");
            string existe = Console.ReadLine().ToUpper();
            if (existe == "S")
            {
                cadastroService.CadastroProduto();
            }
            else if (existe == "N")
            {

                int sair = 0;
                do
                {
                    Console.Write("\n\t\t\tInsira o nome do produto a ser localizado: ");
                    nomeProduto = Console.ReadLine().Trim().Replace(".", "").Replace("-", "").Replace("/", "");

                    if (dbproduto.VerificaProduto(nomeProduto) == false)
                    {
                        dbproduto.LocalizarProduto(nomeProduto);
                        sair++;
                    }
                    else
                    {
                        Console.WriteLine("\n\t\t\tProduto não localizado");
                    }
                } while (dbproduto.VerificaProduto(nomeProduto) == true);
            }
            else EntradaDadosProducao();


            Console.Write("\n\t\t\tQuantidade de produtos: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal quantidade))
            { quantidade = quantidade; }
            else
            {
                Console.WriteLine("\n\t\t\tQuantidade inválida");
                EntradaDadosProducao();
            }


            Console.Write("\n\t\t\tDeseja adicionar alguma materia prima? (S/N): ");
            string confirmar = Console.ReadLine().ToUpper();

            if (confirmar == "S" || confirmar == "s")
            {
                decimal quantidadeMprima = 0;
                Console.WriteLine("\n\t\t\tQuais as materias primas utilizadas?");
                dbmateriaprima.MostrarMateriaPrima();


                Console.Write("\n\t\t\tDigite o nome da materia prima uqe deseja utilizar : ");
                string nome = Console.ReadLine().Trim().Replace(".", "").Replace("-", "").Replace("/", "");


                if (dbmateriaprima.VerificaNomeMPrima(nome) == false)
                {
                    Console.Write("\n\t\t\tQuantidade Materia prima: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal quant))
                    {
                        quantidadeMprima = quant;
                    }
                    else
                    {
                        Console.WriteLine("\n\t\t\tQuantidade inválida");
                      
                    }

                }

                dbproduto.CodigoProduto(nomeProduto);

                string codigoProduto = dbproduto.CodigoProduto(nomeProduto);
                Producao producao = new Producao(codigoProduto, quantidade);

                dbmateriaprima.CodigoMPrima(nome);

                string codigo = dbmateriaprima.CodigoMPrima(nome);
                ItemProducao itemproducao  = new ItemProducao(codigo, quantidadeMprima);

                  ;

            Console.Write("\n\t\t\tDeseja cadastrar a produção (S/N): ");
            string confirma = Console.ReadLine().ToUpper();

            if (confirma == "S")
            {           
                dbitemproducao.Inserir_Item_Producao(itemproducao);
         
                dbproducao.Inserir_Producao(producao);
            }
            

            }

            Console.WriteLine("\n\t\t\tProducao Adicionada com Sucesso");
            BackMenu();

        }

   

        void Cadastro(Producao producao, List<ItemProducao> itemProducaos)
        {
            producao.Id = (++cadastroService.cadastros.codigos[4]).ToString();
            cadastroService.SalvarCodigos();

            cadastroService.cadastros.producao.Add(producao);
            

            cadastroService.cadastros.itensproducao.AddRange(itemProducaos);
          

        }

        void ImpressaoDoRegistro()
        {

            int i = 0;
            string opc = "-1";
            while (opc != "0")
            {
                Console.Clear();
                /*DadosProducao(cadastroService.cadastros.producao[i]);*/
                Console.WriteLine();
                if (i > 0)
                {
                    Console.WriteLine("1-primeiro");
                    Console.WriteLine("2-anterior");
                }
                if (i < cadastroService.cadastros.producao.Count() - 1)
                {
                    Console.WriteLine("3-proximo");
                    Console.WriteLine("4-ultimo");
                }
                Console.WriteLine("0-Sair");
                Console.Write("Opção: ");
                opc = Console.ReadLine();
                switch (opc)
                {
                    case "1":
                        i = 0;
                        break; ;
                    case "2":
                        if (i - 1 >= 0)
                            i--;
                        else
                            Console.WriteLine("Não existe registro antes deste");
                        break;
                    case "3":
                        if (i + 1 <= cadastroService.cadastros.producao.Count() - 1)
                            i++;
                        else
                            Console.WriteLine("Não existe registro depois deste");
                        break;
                    case "4":
                        i = cadastroService.cadastros.producao.Count() - 1;
                        break;
                    case "0":
                        break;
                    default:
                        break;
                }

            }

        }

        void Localizar()
        {

            Console.Write("Digite o nome ou código de barras do produto para localizar a produção: ");
            string busca = Console.ReadLine();

            Produto produto = cadastroService.cadastros.produtos.FirstOrDefault(c => c.Nome == busca || c.CodigoBarras == busca);
            Producao producao = produto != null ? producao = cadastroService.cadastros.producao.Find(c => c.Produto == produto.CodigoBarras) : null;

           /* if (producao != null) DadosProducao(cadastroService.cadastros.producao.Find(c => c.Produto == produto.CodigoBarras));
            else Console.WriteLine("Nenhuma produção encontrada para esse produto\n\n");*/

            BackMenu();
        }

    }
}
