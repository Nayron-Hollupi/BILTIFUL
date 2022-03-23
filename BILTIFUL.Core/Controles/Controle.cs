using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BILTIFUL.Core.Entidades;
using BILTIFUL.Core.Entidades.Enums;

namespace BILTIFUL.Core.Controles
{
    public class Controle
    {
        //uma lista para cada arquivos que eu tiver
        public List<int> codigos { get; set; }
        public List<Cliente> clientes { get; set; }
        public List<Produto> produtos { get; set; }
        public List<Fornecedor> fornecedores { get; set; }
        public List<MPrima> materiasprimas { get; set; }
        public List<Compra> compras { get; set; }
        public List<ItemCompra> itenscompra { get; set; }
        public List<Producao> producao { get; set; }
        public List<ItemProducao> itensproducao { get; set;}
        public List<Venda> vendas { get; set; }
        public List<ItemVenda> itensvenda { get; set; }
        public List<string> inadimplentes { get; set; }
        public List<string> bloqueados { get; set; }



        public Controle()//esse construtora instanciará todas as listas qnd feito
        {
            //instancia cada lista qnd o programa é iniciado
            codigos = new List<int>();
            clientes = new List<Cliente>();
            produtos = new List<Produto>();
            fornecedores = new List<Fornecedor>();
            materiasprimas = new List<MPrima>();
            compras = new List<Compra>(); //
            itenscompra = new List<ItemCompra>(); //adicionar arquivos
            producao = new List<Producao>(); //
            itensproducao = new List<ItemProducao>();//
            vendas = new List<Venda>();//
            itensvenda = new List<ItemVenda>();//
            inadimplentes = new List<string>();
            bloqueados = new List<string>();
            
            try
            {
                StreamReader sr;
                string line;

                if (!Directory.Exists("Arquivos"))
                {
                    Directory.CreateDirectory("Arquivos");
                }

                //LISTA CONTROLE
                if (File.Exists("Arquivos\\Controle.dat"))
                {
                    sr = new StreamReader("Arquivos\\Controle.dat");//le o arquivo controle
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        codigos.Add(int.Parse(line));//adicionando na minha lista codigo linha por linha
                        line = sr.ReadLine();
                    }
                    sr.Close();
                }
                else
                {
                    StreamWriter streamWriter = new StreamWriter("Arquivos\\Controle.dat");
                    streamWriter.WriteLine("0");//Produto
                    streamWriter.WriteLine("0");//Materia Prima
                    streamWriter.WriteLine("0");//Venda
                    streamWriter.WriteLine("0");//Compra
                    streamWriter.WriteLine("0");//Produção
                    streamWriter.Close();

                }

            
               

             

                //LISTA PRODUTOS
                if (File.Exists("Arquivos\\Cosmetico.dat"))
                {
                    sr = new StreamReader("Arquivos\\Cosmetico.dat");
                    line = sr.ReadLine();
                    while (line != null)
                    {
                     /*   string cbarras = line.Substring(0, 12);
                        string nome = line.Substring(12, 20).Trim();
                        string vvendas = line.Substring(32, 5);
                        DateTime uvenda = DateTime.Parse(line.Substring(37, 10));
                        DateTime dcadastro = DateTime.Parse(line.Substring(47, 10));
                        Situacao situacao = (Situacao)char.Parse(line.Substring(57, 1));
                        produtos.Add(new Produto(cbarras, nome, vvendas, uvenda, dcadastro, situacao));*/
                        line = sr.ReadLine();
                    }
                    sr.Close();
                }
           

                //LISTA INADIMPLENTES
                if (File.Exists("Arquivos\\Risco.dat"))
                {
                    sr = new StreamReader("Arquivos\\Risco.dat");//le o arquivo controle
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        inadimplentes.Add(line);//adicionando na minha lista codigo linha por linha
                        line = sr.ReadLine();
                    }
                    sr.Close();
                }

                //LISTA BLOQUEADOS
                if (File.Exists("Arquivos\\Bloqueado.dat"))
                {
                    sr = new StreamReader("Arquivos\\Bloqueado.dat");//le o arquivo controle
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        bloqueados.Add(line);//adicionando na minha lista codigo linha por linha
                        line = sr.ReadLine();
                    }
                    sr.Close();
                }

                //COMPRA
                if (File.Exists("Arquivos\\Compra.dat"))
                {
                    sr = new StreamReader("Arquivos\\Compra.dat");
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        string cod = line.Substring(0, 5);
                        DateTime dcompra = DateTime.Parse(line.Substring(5, 10));
                        string cnpj = line.Substring(15, 14);
                        string valor = line.Substring(29, 7);
                        compras.Add(new Compra(cod,dcompra,cnpj,valor));
                        line = sr.ReadLine();
                    }
                    sr.Close();
                }

                //ITEM COMPRA
                if (File.Exists("Arquivos\\ItemCompra.dat"))
                {
                    sr = new StreamReader("Arquivos\\ItemCompra.dat");
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        string cod = line.Substring(0, 5);
                        DateTime dcompra = DateTime.Parse(line.Substring(5, 10));
                        string materiaprima = line.Substring(15, 6);
                        string quantidade = line.Substring(21, 5);
                        string valoruni = line.Substring(26, 5);
                        string valortotal = line.Substring(31, 6);
                        itenscompra.Add(new ItemCompra(cod,dcompra,materiaprima,quantidade,valoruni,valortotal));
                        line = sr.ReadLine();
                    }
                    sr.Close();
                }

           
               

                //VENDAS
                if (File.Exists("Arquivos\\Venda.dat"))
                {
                    sr = new StreamReader("Arquivos\\Venda.dat");
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        string id = line.Substring(0, 5);
                        DateTime dvenda = DateTime.Parse(line.Substring(5, 10));
                        string cliente = line.Substring(15, 11);
                        string vtotal = line.Substring(26, 7);
                        vendas.Add(new Venda(id, dvenda, cliente, vtotal));
                        line = sr.ReadLine();
                    }
                    sr.Close();
                }

                //ITENS VENDAS
                if (File.Exists("Arquivos\\ItemVenda.dat"))
                {
                    sr = new StreamReader("Arquivos\\ItemVenda.dat");
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        string id = line.Substring(0, 5);
                        string produto = (line.Substring(5, 12));
                        string quantidade = line.Substring(17, 3);
                        string vunit = line.Substring(20, 5);
                        string total = line.Substring(25, 6);
                        itensvenda.Add(new ItemVenda(id, produto, quantidade, vunit, total));
                        line = sr.ReadLine();
                    }
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        public Controle(Cliente cliente)
        {
            if (cliente != null)
            {
                try//envia cliente para arquivo como novo cliente]try
                {
                    StreamWriter sw = new StreamWriter("Arquivos\\Clientes.dat", append: true);
                    /*sw.WriteLine(cliente.ConverterParaEDI());*/
                    sw.Close();
                    Console.WriteLine("\n\t\t\t\t\tCliente cadastrado com sucesso!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }
     
   
        public Controle(MPrima materiaprima)
        {
            if (materiaprima != null)
            {
                try//envia cliente para arquivo como novo cliente]try
                {
                    StreamWriter sw = new StreamWriter("Arquivos\\Materia.dat", append: true);
                    sw.WriteLine();
                    sw.Close();
                    Console.WriteLine("\n\t\t\t\t\tMateria prima cadastrado com sucesso!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }
        public Controle(Compra compra)
        {
            if (compra != null)
            {
                try//envia cliente para arquivo como novo cliente]try
                {
                    StreamWriter sw = new StreamWriter("Arquivos\\Compra.dat", append: true);
                    sw.WriteLine(compra.ConverterParaEDI());
                    sw.Close();
                    Console.WriteLine("\n\t\t\t\t\tCompra cadastrada cadastrado com sucesso!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }
        public Controle(ItemCompra itemcompra)
        {
            if (itemcompra != null)
            {
                try//envia cliente para arquivo como novo cliente]try
                {
                    StreamWriter sw = new StreamWriter("Arquivos\\ItemCompra.dat", append: true);
                    sw.WriteLine(itemcompra.ConverterParaEDI());
                    sw.Close();
                    Console.WriteLine("\n\t\t\t\t\tItem de Compra cadastrado sucesso!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }
     
       
        public Controle(Venda venda)
        {
            if (venda != null)
            {
                try//envia cliente para arquivo como novo cliente]try
                {
                    StreamWriter sw = new StreamWriter("Arquivos\\Venda.dat", append: true);
                    sw.WriteLine(venda.ConverterParaEDI());
                    sw.Close();
                    //Console.WriteLine("\n\t\t\t\t\tItem de produção cadastrado com sucesso!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }
        public Controle(ItemVenda itemvenda)
        {
            if (itemvenda != null)
            {
                try//envia cliente para arquivo como novo cliente]try
                {
                    StreamWriter sw = new StreamWriter("Arquivos\\ItemVenda.dat", append: true);
                    sw.WriteLine(itemvenda.ConverterParaEDI());
                    sw.Close();
                    //Console.WriteLine("\n\t\t\t\t\tItem de produção cadastrado com sucesso!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }


        public Controle(string chave)
        {
            
            if (CadastroService.ValidaCpf(chave))
            {
                try//envia cliente para arquivo como novo cliente]try
                {
                    StreamWriter sw = new StreamWriter("Arquivos\\Risco.dat", append: true);
                    sw.WriteLine(chave);
                    sw.Close();
                    Console.WriteLine("\n\t\t\t\t\tCliente cadastrado como inadimplente!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
            if (CadastroService.ValidaCnpj(chave))
            {
                try//envia cliente para arquivo como novo cliente]try
                {
                    StreamWriter sw = new StreamWriter("Arquivos\\Bloqueado.dat", append: true);
                    sw.WriteLine(chave);
                    sw.Close();
                    Console.WriteLine("\n\t\t\t\t\tFornecedor bloqueado!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }
    }

}
