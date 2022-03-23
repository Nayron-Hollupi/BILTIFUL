using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BILTIFUL;
using BILTIFUL.Core.Controles;
using BILTIFUL.Core.Entidades;
using BILTIFUL.Core.Entidades.Enums;
using BILTIFUL.Core;

namespace BILTIFUL.Core
{
    public class CadastroService
    {
        
        public Cliente dbcliente = new Cliente();
        public Fornecedor dbfornecedor = new Fornecedor();
        public MPrima dbmateriaprima = new MPrima();
        public Produto dbproduto = new Produto();
        public Controle cadastros = new Controle();

        public void SubMenu()
        {
            string opc;
            do
            {
                opc = Menu();
                switch (opc)
                {
                    case "1":
                        CadastroCliente();
                        break;
                    case "2":
                        CadastroProduto();
                        break;
                    case "3":
                        CadastroFornecedor();
                        break;
                    case "4":
                        CadastroMateriaPrima();
                        break;
                    case "5":
                        CadastroInadimplente();
                        break;
                    case "6":
                        CadastroBloqueado();
                        break;
                    case "7":
                        RemoverInadimplencia();
                        break;
                    case "8":
                        RemoverBloqueio();
                        break;
                    case "9":
                        MostrarRegistro();
                        break;
                    case "10":
                        LocalizarRegistro();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Opção invalida!");
                        break;
                }
               
                Console.Clear();
            } while (opc != "0");
        }
        private string Menu()
        {
            string opc;
            Console.WriteLine("\n\t\t\t\t\t________________________________________________");
            Console.WriteLine("\t\t\t\t\t|+++++++++++++++++++| MENU |+++++++++++++++++++|");
            Console.WriteLine("\t\t\t\t\t|1| - CADASTRAR CLIENTE                        |");
            Console.WriteLine("\t\t\t\t\t|2| - CADASTRAR PRODUTO                        |");
            Console.WriteLine("\t\t\t\t\t|3| - CADASTRAR FORNECEDOR                     |");
            Console.WriteLine("\t\t\t\t\t|4| - CADASTRAR MATERIA PRIMA                  |");
            Console.WriteLine("\t\t\t\t\t|5| - ADICIONAR CLIENTE COMO INADIMPLENTE      |");
            Console.WriteLine("\t\t\t\t\t|6| - ADICIONAR FORNECEDOR A LISTA DE BLOQUEADO|");
            Console.WriteLine("\t\t\t\t\t|7| - REMOVER CLIENTE DA LISTA DE INADIMPLENTE |");
            Console.WriteLine("\t\t\t\t\t|8| - REMOVER FORNECEDOR DA LISTA DE BLOQUEADO |");
            Console.WriteLine("\t\t\t\t\t|9| - MOSTRAR REGISTROS                        |");
            Console.WriteLine("\t\t\t\t\t|10| - LOCALIZAR REGISTROS                     |");
            Console.WriteLine("\t\t\t\t\t|0| - VOLTAR PARA O MENU PRINCIPAL             |");
            Console.Write("\t\t\t\t\t|______________________________________________|\n" +
                          "\t\t\t\t\t|Opção: ");
            opc = Console.ReadLine();
            return opc;
        }
        public Cliente CadastroCliente()
        {
            string cpf;
            string datanascimento;
            string csexo;
            DateTime dnascimento;
            string nome;
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t===========CADASTRO CLIENTE===========");
            do
            {
                Console.Write("\t\t\t\t\tCPF: ");
                cpf = Console.ReadLine().Trim().Replace(".", "").Replace("-", "");//tira o ponto e o traço caso digitado
                if (!ValidaCpf(cpf))//valida cpf
                    Console.WriteLine("\t\t\t\t\tCpf invalido!\n\t\t\t\t\tDigite novamente");
            } while (!ValidaCpf(cpf));//enquanto cpf nao for valido digitar denovo

            if (dbcliente.VerificaCPF(cpf) == false)
            {
               
                Console.WriteLine("\t\t\t\t\tCliente com esse CPF ja existe");
                Console.ReadKey();
                return null;
            }
            do
            {
               
                Console.Write("\t\t\t\t\tNome: ");
                nome = Console.ReadLine().Trim();
                if (nome == "")
                    Console.WriteLine("\t\t\t\t\tNome nao pode ser vazio");
            } while (nome == "");
            do
            {
                Console.Write("\t\t\t\t\tData de nascimento(dd/mm/aaaa): ");
                datanascimento = Console.ReadLine();
                if (!DateTime.TryParse(datanascimento, out dnascimento))
                    Console.WriteLine("\t\t\t\t\tData invalida!");
            } while (!DateTime.TryParse(datanascimento, out dnascimento));
            if ((DateTime.Now - dnascimento).Days / 365 < 18)
            {
                Console.WriteLine("\t\t\t\t\tDeve ter pelomenos 18 anos para ser cliente!");
                return null;
            }
            do
            {
                Console.Write("\t\t\t\t\tSexo(M-Masculino e F-Feminino): ");
                csexo = Console.ReadLine().ToUpper();
            } while ((csexo != "M") && (csexo != "F"));
            Sexo sexo = (Sexo)char.Parse(csexo);

            Cliente cliente = new Cliente(cpf, nome, dnascimento, sexo);
          
            dbcliente.Inserir_Cliente(cliente);

            return cliente;

          
        }
        public static bool ValidaCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
        public Fornecedor CadastroFornecedor()
        {
            string dataabertura;
            DateTime dabertura;
            string rsocial;
            string cnpj;
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t===========CADASTRO FORNECEDOR===========");
            do
            {
                Console.Write("\t\t\t\t\tCNPJ: ");
                cnpj = Console.ReadLine().Trim().Replace(".", "").Replace("-", "").Replace("/", "");//tira o ponto e o traço caso digitado
                if (!ValidaCnpj(cnpj))//valida c
                    Console.WriteLine("\t\t\t\t\tCnpj invalido!\nDigite novamente");

            } while (!ValidaCnpj(cnpj));//enquanto cpf nao for valido digitar denovo
            if (dbfornecedor.VerificaCNPJ(cnpj) == false)
            {
                Console.WriteLine("\t\t\t\t\tFornecedor com esse cnpj ja existe");
                Console.ReadKey();
                return null;
            }
            do
            {
                Console.Write("\t\t\t\t\tRazão Social: ");
                rsocial = Console.ReadLine().Trim();
            } while (rsocial == "");
            do
            {
                Console.Write("\t\t\t\t\tData de abertura(dd/mm/aaaa): ");
                dataabertura = Console.ReadLine();
                if (!DateTime.TryParse(dataabertura, out dabertura))
                    Console.WriteLine("\t\t\t\t\tData invalida!");
            } while (!DateTime.TryParse(dataabertura, out dabertura));
            if ((DateTime.Now - dabertura).Days < 180)
            {
                Console.WriteLine("\t\t\t\t\tDeve ter se passado pelo menos 6 meses desde a abertura!");
                return null;
            }


            Fornecedor fornecedor = new Fornecedor(cnpj, rsocial, dabertura);

            dbfornecedor.Inserir_Fornecedor(fornecedor);
        
            return fornecedor;
        }
        public static bool ValidaCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }
        public Produto CadastroProduto()
        {
            string svalor;
            int valor;
            string nome;
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t===========CADASTRO PRODUTO===========");
            do
            {
                Console.Write("\t\t\t\t\tNome: ");
                nome = Console.ReadLine().Trim();
            } while (nome == "");
            do
            {
                Console.Write("\t\t\t\t\tValor($$$,$$)(valor precisa ser menor que 1000,00): ");
                svalor = Console.ReadLine().Trim().Replace(".", "").Replace(",", "");
                if (!int.TryParse(svalor, out valor) || (valor > 99999) || (valor <= 0))
                    Console.WriteLine("\t\t\t\t\tValor invalido!");
            } while (!int.TryParse(svalor, out valor) || (valor > 99999) || (valor <= 0));

            cadastros.codigos[0]++;
            SalvarCodigos();
            string cod = "" + cadastros.codigos[0];

             decimal valorvenda = Decimal.Parse(svalor);
            Produto produto = new Produto(cod, nome, valorvenda);
            dbproduto.Inserir_Produto(produto);
            return produto;
        }
        public void SalvarCodigos()
        {
            try
            {
                StreamWriter sw = new StreamWriter("Arquivos\\Controle.dat");

                cadastros.codigos.ForEach(c => sw.WriteLine(c));

                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public MPrima CadastroMateriaPrima()
        {
            string nome;
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t===========CADASTRO MATERIA PRIMA===========");
            do
            {
                Console.Write("\t\t\t\t\tDigite o nome da Materia Prima: ");
                nome = Console.ReadLine().Trim();
            } while (nome == "");

            cadastros.codigos[1]++;
            SalvarCodigos();
            string cod = "" + cadastros.codigos[1];

            MPrima mPrima = new MPrima(cod, nome);

            dbmateriaprima.Inserir_Materia_Prima(mPrima);

            return mPrima;

        }
        public string CadastroInadimplente()
        {
            string inadimplente;
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t===========CADASTRO DE INADIMPLENTE===========");
            do
            {
                Console.Write("\t\t\t\t\tDigite o cpf do inadimplente: ");
                inadimplente = Console.ReadLine().Trim().Replace(".", "").Replace("-", ""); ;
                if (!ValidaCpf(inadimplente))//valida cpf
                    Console.WriteLine("\t\t\t\t\tCpf invalido!\nDigite novamente");
            } while (!ValidaCpf(inadimplente));
            if (cadastros.inadimplentes.Find(p => p == "" + inadimplente) != null)
            {
                Console.WriteLine("\t\t\t\t\tInadimplente com esse CPF ja existe");
                return null;
            }
            string cpf = inadimplente;

            if (dbcliente.VerificaRisco(cpf) == false)
            {

                Console.WriteLine("\t\t\t\t\tCliente com esse CPF ja existe");
                Console.ReadKey();
                return null;
            }
            else
            {         
                dbcliente.Risco(cpf);
                return cpf;
            }
       
        }
        public string CadastroBloqueado()
        {
            string bloqueado;
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t===========CADASTRO DE BLOQUEADO===========");
            do
            {
                Console.Write("\t\t\t\t\tDigite o CNPJ do fornecedor: ");
                bloqueado = Console.ReadLine().Trim().Replace(".", "").Replace("-", "").Replace("/", "");
                if (!ValidaCnpj(bloqueado))//valida cpf
                    Console.WriteLine("\t\t\t\t\tCnpj invalido!\nDigite novamente");
            } while (!ValidaCnpj(bloqueado));
         

            string cnpj = bloqueado;
         
            if (dbfornecedor.VerificaBloqueado(cnpj) == false)
            {
                Console.WriteLine("\t\t\t\t\tFornecedor com esse cnpj ja existe");
                return null;
            }
            else {
                dbfornecedor.Bloqueado(cnpj);
                return cnpj;

            }
            
         
        }

        public void RemoverInadimplencia()
        {
            string inadimplente;
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t===========REMOVER DE INADIMPLENTE===========");
            do
            {
                Console.Write("\t\t\t\t\tDigite o cpf inadimplente: ");
                inadimplente = Console.ReadLine().Trim().Replace(".", "").Replace("-", "");
                if (!ValidaCpf(inadimplente))//valida cpf
                    Console.WriteLine("\t\t\t\t\tCpf invalido!\n\t\t\t\t\tDigite novamente");
            } while (!ValidaCpf(inadimplente));

            string cpf = inadimplente;

            if (dbcliente.VerificaRisco(cpf) == false)
            {
                dbcliente.RemoveRisco(cpf);
                Console.WriteLine("\t\t\t\t\tCpf Liberado");
            }
                
        }
        public void RemoverBloqueio()
        {
            string bloqueado;
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t===========REMOVER DE BLOQUEADO===========");
            do
            {
                Console.Write("\t\t\t\t\tDigite o cnpj do fornecedor bloqueado: ");
                bloqueado = Console.ReadLine().Trim().Replace(".", "").Replace("-", "").Replace("/", "");
                if (!ValidaCnpj(bloqueado))//valida cpf
                    Console.WriteLine("\t\t\t\t\tCpf invalido!\n\t\t\t\t\tDigite novamente");
            } while (!ValidaCnpj(bloqueado));

            string cnpj = bloqueado;

            if (dbfornecedor.VerificaBloqueado(cnpj) == false) {
                dbfornecedor.RemoveBloqueado(cnpj);
                Console.WriteLine("\t\t\t\t\tCnpj Liberado");

            }
               
        }
        public void Remover(long chave)
        {
            string schave = "" + chave;
            if (CadastroService.ValidaCpf(schave))
            {
                cadastros.inadimplentes.Remove(schave);
                try//envia cliente para arquivo como novo cliente]try
                {
                    int i = 0;
                    StreamWriter sw = new StreamWriter("Arquivos\\Risco.dat");
                    while (i != cadastros.inadimplentes.Count)
                    {
                        sw.WriteLine(cadastros.inadimplentes[i]);
                        i++;
                    }
                    sw.Close();
                    Console.WriteLine("\t\t\t\t\tCliente removido dos inadimplentes!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
            if (CadastroService.ValidaCnpj(schave))
            {
                cadastros.bloqueados.Remove(schave);
                try//envia cliente para arquivo como novo cliente]try
                {
                    int i = 0;
                    StreamWriter sw = new StreamWriter("Arquivos\\Bloqueado.dat");
                    while (i != cadastros.bloqueados.Count)
                    {
                        sw.WriteLine(cadastros.bloqueados[i]);
                        i++;
                    }
                    sw.Close();
                    Console.WriteLine("\t\t\t\t\tFornecedor desbloqueado!!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }

        public void MostrarRegistro()
        {
            string opc;
            do
            {
                Console.Clear();
                Console.WriteLine("\n\t\t\t\t\t________________________________________________");
                Console.WriteLine("\t\t\t\t\t|+++++++++++++| MENU DE REGISTROS |++++++++++++|");
                Console.WriteLine("\t\t\t\t\t|1| - REGISTROS DE CLIENTES                    |");
                Console.WriteLine("\t\t\t\t\t|2| - REGISTROS DE FORNECEDORES                |");
                Console.WriteLine("\t\t\t\t\t|3| - REGISTROS DE MATERIAS PRIMAS             |");
                Console.WriteLine("\t\t\t\t\t|4| - REGISTROS DE PRODUTOS                    |");
                Console.WriteLine("\t\t\t\t\t|5| - REGISTROS DE VENDAS                      |");
                Console.WriteLine("\t\t\t\t\t|6| - REGISTROS DE COMPRAS                     |");
                Console.WriteLine("\t\t\t\t\t|7| - REGISTROS DE PRODUÇÃO                    |");
                Console.WriteLine("\t\t\t\t\t|0| - VOLTAR                                   |");
                Console.Write("\t\t\t\t\t|______________________________________________|\n" +
                              "\t\t\t\t\t|Opção: ");
                opc = Console.ReadLine();
                switch (opc)
                {
                    case "1":
                      
                            dbcliente.MostrarCliente();
                       
                        break;
                    case "2":
                        dbfornecedor.MostrarFornecedor();
                        break;
                    case "3":
                        dbmateriaprima.MostrarMateriaPrima();
                        break;
                    case "4":
                        dbproduto.MostrarProduto();
                        break;
                    case "5":
                        if (cadastros.vendas.Count() != 0)
                            new Registros(cadastros.vendas, cadastros.itensvenda);
                        else
                            Console.WriteLine("\t\t\t\t\tNenhuma venda registrada");
                        break;
                    case "6":
                        if (cadastros.compras.Count() != 0)
                            new Registros(cadastros.compras, cadastros.itenscompra);
                        else
                            Console.WriteLine("\t\t\t\t\tNenhum produto registrado");
                        break;
                    case "7":
                        if (cadastros.producao.Count() != 0)
                            new Registros(cadastros.producao, cadastros.itensproducao);
                        else
                            Console.WriteLine("\t\t\t\t\tNenhum produto registrado");
                        break;
                    case "0":

                        break;
                    default:
                        Console.WriteLine("\t\t\t\t\tOpção invalida");
                        break;
                }
                Console.ReadKey();
            } while (opc != "0");
        }
        public void LocalizarRegistro()
        {
            string opc;
            do
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t\t________________________________________________");
                Console.WriteLine("\t\t\t\t\t|++++++++++++| MENU DE LOCALIZAÇÃO |+++++++++++|");
                Console.WriteLine("\t\t\t\t\t|1| - LOCALIZAR CLIENTES                       |");
                Console.WriteLine("\t\t\t\t\t|2| - LOCALIZAR FORNECEDORES                   |");
                Console.WriteLine("\t\t\t\t\t|3| - LOCALIZAR MATERIAS PRIMAS                |");
                Console.WriteLine("\t\t\t\t\t|4| - LOCALIZAR PRODUTOS                       |");
                Console.WriteLine("\t\t\t\t\t|5| - LOCALIZAR VENDAS                         |");
                Console.WriteLine("\t\t\t\t\t|6| - LOCALIZAR COMPRAS                        |");
                Console.WriteLine("\t\t\t\t\t|7| - LOCALIZAR PRODUÇÕES                      |");
                Console.WriteLine("\t\t\t\t\t|0| - VOLTAR                                   |");
                Console.Write("\t\t\t\t\t|______________________________________________|\n" +
                              "\t\t\t\t\t|Opção: ");
                opc = Console.ReadLine();
                bool encontrado = false;
                Console.Clear();
                switch (opc)
                {

                    case "1":
                        string cpf;
                        do
                        {
                            Console.Write("\t\t\t\t\tDigite o cpf que deseja localizar: ");
                            cpf = Console.ReadLine();
                            if (!ValidaCpf(cpf))
                            {
                                Console.WriteLine("\t\t\t\t\tCPF INVÁLIDO, TENTE NOVAMENTE!");
                            }
                        } while (ValidaCpf(cpf) != true);
                        Console.Clear();
                        dbcliente.LocalizarCliente(cpf);
                        break;
                    case "2":
                        string cnpj;
                        do
                        {
                            Console.Write("\t\t\t\t\tDigite o cnpj que deseja localizar: ");
                            cnpj = Console.ReadLine().Trim().Replace(".", "").Replace("-", "").Replace("/", "");                            
                            if (!ValidaCnpj(cnpj))
                            {
                                Console.WriteLine("\t\t\t\t\tCNPJ INVÁLIDO, TENTE NOVAMENTE!");
                            }
                        } while (ValidaCnpj(cnpj) != true);
                       
                        if (dbfornecedor.VerificaCNPJ(cnpj) == false)
                        {
                            Console.Clear();
                            dbfornecedor.LocalizarFornecedor(cnpj);
                           
                        }
                        break;
                    case "3":
                        Console.Write("\t\t\t\t\tDigite o nome que deseja localizar: ");
                        string nomeMateriaPrima = Console.ReadLine().Trim().ToLower();
                        List<MPrima> localizarmprima = cadastros.materiasprimas.FindAll(p => p.Nome.ToLower() == nomeMateriaPrima);
                        if (localizarmprima != null)
                        {
                            encontrado = true;
                            localizarmprima.ForEach(p => Console.WriteLine());
                        }
                        break;
                    case "4":
                        Console.Write("\t\t\t\t\tDigite o nome do produto que deseja localizar: ");
                        string nomeProduto = Console.ReadLine().Trim().ToLower();
                        List<Produto> localizaProduto = cadastros.produtos.FindAll(p => p.Nome.ToLower() == nomeProduto);
                        if (localizaProduto != null)
                        {
                            encontrado = true;
                            /*localizaProduto.ForEach(p => Console.WriteLine(p.DadosProduto()));*/
                        }
                        break;
                    case "5":
                        Console.Write("\t\t\t\t\tDigite a data de venda que deseja localizar(dd/mm/aaaa): ");
                        DateTime dvenda = DateTime.Parse(Console.ReadLine());
                        List<Venda> localizavenda = cadastros.vendas.FindAll(p => p.DataVenda == dvenda);
                        if (localizavenda != null)
                        {
                            encontrado = true;
                            foreach (Venda p in localizavenda)
                            {
                                Console.WriteLine(p.DadosVenda());
                                Console.WriteLine("\t\t\t\t\tItens: ");
                                foreach (ItemVenda i in cadastros.itensvenda)
                                {
                                    if (i.Id == p.Id)
                                        Console.WriteLine(i.DadosItemVenda());
                                }
                            }
                        }
                        break;
                    case "6":
                        Console.Write("\t\t\t\t\tDigite o data de compra que deseja localizar(dd/mm/aaaa): ");
                        DateTime dcompra = DateTime.Parse(Console.ReadLine());
                        List<Compra> localizacompra = cadastros.compras.FindAll(p => p.DataCompra == dcompra);
                        if (localizacompra != null)
                        {
                            encontrado = true;
                            foreach (Compra p in localizacompra)
                            {
                                Console.WriteLine(p.DadosCompra());
                                Console.WriteLine("\t\t\t\t\tItens: ");
                                foreach (ItemCompra i in cadastros.itenscompra)
                                {
                                    if (i.Id == p.Id)
                                        Console.WriteLine(i.DadosItemCompra());
                                }
                            }
                        }
                        break;
                    case "7":
                        Console.Write("\t\t\t\t\tDigite o data de produção que deseja localizar(dd/mm/aaaa): ");
                        DateTime dproducao = DateTime.Parse(Console.ReadLine());
                        List<Producao> localizaproducao = cadastros.producao.FindAll(p => p.DataProducao == dproducao);
                        if (localizaproducao != null)
                        {
                            encontrado = true;
                            foreach (Producao p in localizaproducao)
                            {
                                Console.WriteLine(p.DadosProducao());
                                Console.WriteLine("\t\t\t\t\tItens: ");
                                foreach (ItemProducao i in cadastros.itensproducao)
                                {
                                    if (i.Id == p.Id)
                                        Console.WriteLine(i.DadosItemProducao());
                                }
                            }
                        }
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("\t\t\t\t\tOpção invalida");
                        break;
                }
                if (encontrado == false && opc != "0")
                    Console.WriteLine("\t\t\t\t\t");
                Console.ReadKey();
            } while (opc != "0");
        }

       /* public void EditarRegistros()
        {
            string opc;
            do
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t\t________________________________________________");
                Console.WriteLine("\t\t\t\t\t|++++++++++++++| MENU DE EDIÇÃO |++++++++++++++|");
                Console.WriteLine("\t\t\t\t\t|1| - EDITAR CLIENTES                          |");
                Console.WriteLine("\t\t\t\t\t|2| - EDITAR FORNECEDORES                      |");
                Console.WriteLine("\t\t\t\t\t|3| - EDITAR MATERIAS PRIMAS                   |");
                Console.WriteLine("\t\t\t\t\t|4| - EDITAR PRODUTOS                          |");
                Console.WriteLine("\t\t\t\t\t|0| - VOLTAR                                   |");
                Console.Write("\t\t\t\t\t|_________________________________________________|\n" +
                              "\t\t\t\t\t|Opção: ");
                Console.WriteLine("\t________________________________________________");
                Console.WriteLine("\t|++++++++++++++| MENU DE EDIÇÃO |++++++++++++++|");
                Console.WriteLine("\t|1| - EDITAR CLIENTES                          |");
                Console.WriteLine("\t|2| - EDITAR FORNECEDORES                      |");
                Console.WriteLine("\t|3| - EDITAR MATERIAS PRIMAS                   |");
                Console.WriteLine("\t|4| - EDITAR PRODUTOS                          |");
                Console.WriteLine("\t|0| - VOLTAR                                   |");
                Console.Write("\t|______________________________________________|\n" +
                              "\t|Opção: ");
                opc = Console.ReadLine();
                bool encontrado = false;
                Console.Clear();
                switch (opc)
                {

                    case "1":
                        string cpf;
                        do
                        {
                            Console.Write("\t\t\t\t\tDigite o CPF do cliente que deseja alterar: ");
                            cpf = Console.ReadLine();
                            Console.Write("Digite o CPF do cliente que deseja alterar: ");
                            cpf = Console.ReadLine().Replace(".", "").Replace("-", "");
                            if (!ValidaCpf(cpf))
                            {
                                Console.WriteLine("\t\t\t\t\tCPF INVÁLIDO, TENTE NOVAMENTE!");
                            }
                        } while (ValidaCpf(cpf) != true);
                        Console.Clear();

                        Cliente localizarcliente = cadastros.clientes.Find(p => p.CPF == long.Parse(cpf));
                        if (localizarcliente != null)
                        {
                            string opca;
                            encontrado = true;
                            Console.WriteLine(localizarcliente.DadosCliente());
                            do
                            {
                                Console.WriteLine("\n\n\t\t\t\t\tSOMENTE O NOME É POSSÍVEL ALTERAR!");
                                Console.WriteLine("\t\t\t\t\tDeseja alterar o nome? [S - Sim] [N - Não] ");
                                opcao = Console.ReadLine().ToUpper();
                                if (opcao == "S")
                                Console.WriteLine("\n\nSOMENTE O NOME É POSSÍVEL ALTERAR!");
                                Console.WriteLine("Deseja alterar o nome? [S - Sim] [N - Não] ");
                                opca = Console.ReadLine().ToUpper();
                                if (opca == "S")
                                {
                                    Console.Write("\n\t\t\t\t\tInforme o novo Nome: ");
                                    string novoNome = Console.ReadLine();
                                    localizarcliente.Nome = novoNome;
                                    Console.WriteLine(localizarcliente.DadosCliente());
                                    Console.WriteLine("\t\t\t\t\tNOVO NOME ALTERADO COM SUCESSO!");
                                    Console.ReadLine();//////////
                                    Console.WriteLine("NOVO NOME ALTERADO COM SUCESSO!");
                                    Console.ReadLine();
                                    new Controle(localizarcliente);

                                    Console.ReadLine();
                                    break;
                                }
                                else if (opca == "N")
                                {
                                    Console.Clear();
                                    Console.WriteLine("\t\t\t\t\tALTERAÇÃO CANCELADA");
                                    break;
                                }
                            } while (opca != "S" || opca != "N");
                        }
                        break;
                    case "2":
                        string cnpj, opc2 = null;
                        do
                        {
                            Console.Write("\t\t\t\t\tDigite o cnpj que deseja localizar: ");
                            cnpj = Console.ReadLine();
                            if (!ValidaCnpj(cnpj))
                            Console.Write("Digite o cnpj que deseja localizar: ");
                            cnpj = Console.ReadLine().Replace(".", "").Replace("/", "").Replace("-", "");
                            if (ValidaCnpj(cnpj))
                            {
                                Console.WriteLine("\t\t\t\t\tCNPJ INVÁLIDO, TENTE NOVAMENTE!");
                            }
                        } while (ValidaCnpj(cnpj) != true);
                        Console.Clear();

                        Fornecedor localizarfornecedor = cadastros.fornecedores.Find(p => p.CNPJ == long.Parse(cnpj));
                        if (localizarfornecedor != null)
                        {
                            encontrado = true;
                            Console.WriteLine(localizarfornecedor.DadosFornecedor());
                        }
                                Fornecedor localizarfornecedor = cadastros.fornecedores.Find(p => p.CNPJ == long.Parse(cnpj));
                                if (localizarfornecedor != null)
                                {
                                    encontrado = true;
                                    Console.WriteLine(localizarfornecedor.DadosFornecedor());
                                }
                                else
                                {
                                    Console.WriteLine("CNPJ não encontrado");
                                    Console.ReadLine();
                                }
                                Console.WriteLine("CNPJ INVÁLIDO, TENTE NOVAMENTE!");
                                Console.WriteLine("Deseja Procurar novamente? [S - sim] [N - Não]");
                                opc2 = Console.ReadLine().ToUpper();
                                if (opc2 == "N")
                                {
                                    break;
                                }
                                Console.Clear();
                            }
                        } while (opc2 != "S");
                        Console.Clear();
                       
                        
                        break;
                    case "3":
                        Console.Write("\t\t\t\t\tDigite o nome que deseja localizar: ");
                        string nomeMateriaPrima = Console.ReadLine().Trim().ToLower();
                        List<MPrima> localizarmprima = cadastros.materiasprimas.FindAll(p => p.Nome.ToLower() == nomeMateriaPrima);
                        if (localizarmprima != null)
                        {
                            encontrado = true;
                            localizarmprima.ForEach(p => Console.WriteLine(p.DadosMateriaPrima()));
                        }
                        break;
                    case "4":
                        Console.Write("\t\t\t\t\tDigite o nome do produto que deseja localizar: ");
                        string nomeProduto = Console.ReadLine().Trim().ToLower();
                        List<Produto> localizaProduto = cadastros.produtos.FindAll(p => p.Nome.ToLower() == nomeProduto);
                        if (localizaProduto != null)
                        {
                            encontrado = true;
                            localizaProduto.ForEach(p => Console.WriteLine(p.DadosProduto()));
                        }
                        break;
                    case "5":
                        Console.Write("\t\t\t\t\tDigite a data de venda que deseja localizar(dd/mm/aaaa): ");
                        DateTime dvenda = DateTime.Parse(Console.ReadLine());
                        List<Venda> localizavenda = cadastros.vendas.FindAll(p => p.DataVenda == dvenda);
                        if (localizavenda != null)
                        {
                            encontrado = true;
                            foreach (Venda p in localizavenda)
                            {
                                Console.WriteLine(p.DadosVenda());
                                Console.WriteLine("\t\t\t\t\tItens: ");
                                foreach (ItemVenda i in cadastros.itensvenda)
                                {
                                    if (i.Id == p.Id)
                                        Console.WriteLine(i.DadosItemVenda());
                                }
                            }
                        }
                        break;
                    case "6":
                        Console.Write("\t\t\t\t\tDigite o data de compra que deseja localizar(dd/mm/aaaa): ");
                        DateTime dcompra = DateTime.Parse(Console.ReadLine());
                        List<Compra> localizacompra = cadastros.compras.FindAll(p => p.DataCompra == dcompra);
                        if (localizacompra != null)
                        {
                            encontrado = true;
                            foreach (Compra p in localizacompra)
                            {
                                Console.WriteLine(p.DadosCompra());
                                Console.WriteLine("\t\t\t\t\tItens: ");
                                foreach (ItemCompra i in cadastros.itenscompra)
                                {
                                    if (i.Id == p.Id)
                                        Console.WriteLine(i.DadosItemCompra());
                                }
                            }
                        }
                        break;
                    case "7":
                        Console.Write("\t\t\t\t\tDigite o data de produção que deseja localizar(dd/mm/aaaa): ");
                        DateTime dproducao = DateTime.Parse(Console.ReadLine());
                        List<Producao> localizaproducao = cadastros.producao.FindAll(p => p.DataProducao == dproducao);
                        if (localizaproducao != null)
                        {
                            encontrado = true;
                            foreach (Producao p in localizaproducao)
                            {
                                Console.WriteLine(p.DadosProducao());
                                Console.WriteLine("\t\t\t\t\tItens: ");
                                foreach (ItemProducao i in cadastros.itensproducao)
                                {
                                    if (i.Id == p.Id)
                                        Console.WriteLine(i.DadosItemProducao());
                                }
                            }
                        }
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("\t\t\t\t\tOpção invalida");
                        break;
                }
                if (encontrado == false && opc != "0")
                    Console.WriteLine("\t\t\t\t\tRegistro não encontrado");
                Console.ReadKey();
            } while (opc != "0");
        }*/
    }
}