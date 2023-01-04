using System.Drawing;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Reflection;

namespace ByteBank1
{

    public class Program
    {

        static void ShowMenu()
        {
            //Console.Clear();
            Console.WriteLine("\n\n  * Byte Bank *  \n   ");
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Listar todas as contas registradas");
            Console.WriteLine("4 - Detalhes de um usuário");
            Console.WriteLine("5 - Quantia armazenada no banco");
            Console.WriteLine("6 - Manipular a conta");
            Console.WriteLine("0 - Para sair do programa");
            Console.Write("Digite a opção desejada: ");
        }
        static void ShowMenuManipularConta()
        {            
            Console.WriteLine("1 - Depositar");
            Console.WriteLine("2 - Sacar");
            Console.WriteLine("3 - Realizar Transferencia");
            Console.WriteLine("4 - Saldo");
            Console.WriteLine("5 - Editar Nome");            
            Console.WriteLine("0 - Voltar ao menu principal");
            Console.Write("Digite a opção desejada: ");
        }

        static void RegistrarNovoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            cpfs.Add(Console.ReadLine());
            Console.Write("Digite o nome: ");
            titulares.Add(Console.ReadLine());
            Console.Write("Digite a senha: ");
            senhas.Add(Console.ReadLine());
            saldos.Add(0);
        }

        static void DeletarUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            string cpfParaDeletar = Console.ReadLine();
            int indexParaDeletar = cpfs.FindIndex(cpf => cpf == cpfParaDeletar);

            if (indexParaDeletar == -1)
            {
                Console.WriteLine("Não foi possível deletar esta Conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
            }
            else
            {
                cpfs.Remove(cpfParaDeletar);
                titulares.RemoveAt(indexParaDeletar);
                senhas.RemoveAt(indexParaDeletar);
                saldos.RemoveAt(indexParaDeletar);

                Console.WriteLine("Conta deletada com sucesso");
            }
        }

        static void ListarTodasAsContas(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.WriteLine("\n\n"  );
            for (int i = 0; i < cpfs.Count; i++)
            {
                ApresentaConta(i, cpfs, titulares, saldos);
            }
        }

        static void ApresentarUsuario(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaApresentar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaApresentar == -1)
            {
                Console.WriteLine("Não foi possível apresentar esta Conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
            }
            else { ApresentaConta(indexParaApresentar, cpfs, titulares, saldos); }

        }

        static void ApresentarValorAcumulado(List<double> saldos)
        {
            Console.WriteLine($"\n\nTotal acumulado no banco: {saldos.Sum()}");
            // saldos.Sum(); ou .Agregatte(0.0, (x, y) => x + y)
        }

        static int ConsultarConta(string pergunta, List<string> cpfs)
        {
            int option = 0, indexParaApresentar;
            do
            {
                option = 0;
                Console.Write(pergunta);
                string cpfParaApresentar = Console.ReadLine();
                indexParaApresentar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

                if (indexParaApresentar == -1)
                {
                    Console.WriteLine();
                    Console.WriteLine("Conta não encontrada.");
                    Console.WriteLine("------------------------");
                    //Console.WriteLine("Deseja realizar uma nova consulta? ");
                    Console.WriteLine("Digite (1) para nova consulta ou qualquer numero para sair");
                    option = int.Parse(Console.ReadLine());
                    Console.Clear();

                }
            } while (option == 1);
            return indexParaApresentar;
        }

        static void ValidarSenha(int indexParaApresentar,List<string> cpfs, List<string> titulares, List<double> saldos, List<String> senhas)
        {

                int tentativas = 0;
                Console.Write("Informe a senha: ");
                do
                {
                    string senhaInformada = Console.ReadLine();
                    if (senhaInformada == senhas[indexParaApresentar])
                        tentativas = 4;
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("***  Senha incorreta *****");
                        Console.Write("Informe a senha: ");
                        tentativas++;

                    }


                } while (tentativas < 3);

                if (tentativas == 3)
                {
                    Console.Clear();
                    Console.WriteLine("\n\n********* Voce errou a senha 3 vezes ***********  ");
                    Console.WriteLine("Pressione enter para voltar ao menu principal");
                    Console.ReadLine();
                    //ShowMenu();
                }
                else
                {
                    Console.WriteLine($"\n-------------------------------\nTitular : {titulares[indexParaApresentar]}");
                    Console.WriteLine("-------------------------------");
                    ManipularConta(indexParaApresentar, cpfs, titulares, saldos, senhas);
                }
            

        }
           

        static void ApresentaConta(int index, List<string> cpfs, List<string> titulares, List<double> saldos)
        {
           Console.WriteLine($"CPF = {cpfs[index]} | Titular = {titulares[index]} | Saldo = R${saldos[index]:F2} | ");
           Console.WriteLine($"------------------------------------------------------------------------");
        }

        public static void Main(string[] args)
        {

            
            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();

            int option;

            do
            {
                ShowMenu();
                option = int.Parse(Console.ReadLine());
                Console.Clear();
                //Console.WriteLine("-----------------");

                switch (option)
                {
                    case 0:
                        Console.WriteLine("Estou encerrando o programa...");
                        break;
                    case 1:
                        RegistrarNovoUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 2:
                        DeletarUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 3:
                        ListarTodasAsContas(cpfs, titulares, saldos);
                        break;
                    case 4:
                        ApresentarUsuario(cpfs, titulares, saldos);
                        break;
                    case 5:
                        ApresentarValorAcumulado(saldos);
                        break;
                    case 6:
                        int indexParaApresentar = ConsultarConta("Informe o CPF: ",cpfs);
                        if (indexParaApresentar != -1) {
                            ValidarSenha(indexParaApresentar,cpfs, titulares, saldos, senhas);
                        }
                        break;
                }

               // Console.WriteLine("------------------------------------");

            } while (option != 0);



        }

        static void Depositar(int index, List<string> titulares, List<double> saldos) {
            Console.WriteLine("Informe o valor para deposito");
            Double valor = double.Parse(Console.ReadLine());
            saldos[index] += valor;
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Deposito realizado com sucesso!");
            Console.WriteLine($"Titular = {titulares[index]}");
            Console.WriteLine("-------------------------------------------\n\n");
            //Console.WriteLine($"CPF = {cpfs[index]} | Titular = {titulares[index]} | Saldo = R${saldos[index]:F2}");

        }

        static void Sacar(int index, List<double> saldos) {
            Console.WriteLine("Informe o valor do saque:");
            Double valor = double.Parse(Console.ReadLine());
            if (valor > saldos[index])
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Saldo insuficiente");
                Console.WriteLine("------------------------------------\n");
            }
            else
            {
                saldos[index] -= valor;
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Saque realizado com Sucesso!");
                Console.WriteLine("------------------------------------\n");
            }
        }
        static void Transferir(int index,int indexParaApresentar, List<string> titulares, List<string> cpfs, List<double> saldos)
        {
            

            if (indexParaApresentar != -1)
            {
                Console.Write("Informe o valor do transferencia:");
                Double valor = double.Parse(Console.ReadLine());
                if (valor > saldos[index])
                {
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine("Saldo insuficiente");
                    Console.WriteLine("------------------------------------\n");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Dados da transferencia: \nTitular: {titulares[indexParaApresentar]}\nValor: R${valor:F2}");
                    Console.WriteLine("Confirmar a tranferencia? (s)Sim (n)Não");
                    String resposta = Console.ReadLine();
                    if (resposta == "n")
                    {
                        Console.Clear();
                        Console.WriteLine("Transferencia cancelada pelo usuario \n\n");
                        return;
                    }
                    else
                    {
                        saldos[index] -= valor;
                        saldos[indexParaApresentar] += valor;
                        Console.Clear();
                        Console.WriteLine("-----------------------------------------------------------------------------------------------");
                        Console.WriteLine($"Transferencia realizada com sucesso!\nTitular = {titulares[indexParaApresentar]}\nValor de R${valor:F2}");
                        Console.WriteLine("-----------------------------------------------------------------------------------------------\n");
                    }
                }
               
            }
        }

        static void MostrarSaldo(int index, List<String> titulares, List<Double> saldos) {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($"Titular = {titulares[index]} | Saldo = R${saldos[index]:F2}");
            Console.WriteLine("--------------------------------------------\n");
        }

        static void EditarNome(int index, List<String> titulares) {
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Infome o nome para ser atualizado");
            titulares[index] = Console.ReadLine();
            Console.WriteLine($"Nome atualizado para {titulares[index]}");
            Console.WriteLine("------------------------------------\n");
        }

        static void ManipularConta(int index, List<string> cpfs, List<string> titulares, List<double> saldos, List<String> senhas)
        {
            int option = 0;
            Double valor = 0.0;

            do
            {
                ShowMenuManipularConta();
                option = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (option)
                {
                    case 0:
                        //Console.WriteLine("Voltar ao Menu Principal");
                        Console.Clear();
                        break;
                    case 1:
                        Depositar(index, titulares, saldos);
                        break;

                    case 2:
                        Sacar(index, saldos);
                        break;
                    case 3:
                        int indexParaApresentar = ConsultarConta("Informe o CPF do Titular que recebera a transferencia: ", cpfs);
                        if (indexParaApresentar != -1)
                        {
                            Transferir(index,indexParaApresentar, titulares, cpfs, saldos);
                        }
                        break;
                    case 4:
                        MostrarSaldo(index, titulares, saldos);                        
                        break;
                    case 5:
                        EditarNome(index, titulares);
                        break;
                    

                }

            } while (option != 0);


        }

    }
}