using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Xml;

namespace Euromilhões___Avaliação
{
    //Este programa tem como objetivo a criação de um jogo como o Euromilhões
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" ########  ##     ##  ########    #######   ##     ##  ####  ##        ##     ##   #######   ########   ######  \r\n ##        ##     ##  ##     ##  ##     ##  ###   ###   ##   ##        ##     ##  ##     ##  ##        ##    ## \r\n ##        ##     ##  ##     ##  ##     ##  #### ####   ##   ##        ##     ##  ##     ##  ##        ##       \r\n ######    ##     ##  ########   ##     ##  ## ### ##   ##   ##        #########  ##     ##  ######     ######  \r\n ##        ##     ##  ##   ##    ##     ##  ##  #  ##   ##   ##        ##     ##  ##     ##  ##              ## \r\n ##        ##     ##  ##    ##   ##     ##  ##     ##   ##   ##        ##     ##  ##     ##  ##        ##    ## \r\n ########   #######   ##     ##   #######   ##     ##  ####  ########  ##     ##   #######   ########   ######  \n\n");
            Console.WriteLine("Prémios do Euromilhões:");
            Console.WriteLine("1º Prémio: 5 números, 2 estrelas");
            Console.WriteLine("2º Prémio: 5 números, 1 estrela");
            Console.WriteLine("3º Prémio: 5 números, 0 estrelas");
            Console.WriteLine("4º Prémio: 4 números, 2 estrelas");
            Console.WriteLine("5º Prémio: 4 números, 1 estrela");
            Console.WriteLine("6º Prémio: 4 números, 0 estrelas");

            //Contadores para as estrelas e números que são correspondentes.
            int estrelasCounter = 0;
            int numsCounter = 0;

            //****************************  Chamamento dos métodos  ***********************

            //Arrays que vão guardar os  numeros e chaves geradas aleatóriamente
            int[] numsAleatórios = GeradorNumeros(5, 1, 50);
            int[] estrelasAletorios = GeradorNumeros(2, 1, 12);


            Console.WriteLine("\n\n\nEscolha os números da sua chave (de 1 a 50) \n");
            //Arrays que vão guardar os  numeros e chaves inseridas pelo utilizador
            int[] numsUtilizador = chaveUtilizador(5,1,50);

            Console.WriteLine("\n\n\nEscolha as estrelas da sua chave (de 1 a 12) \n");
            int[] estrelasUtilizador = chaveUtilizador(2,1,12);

            //Output da chave inseridade pelo utilizador
            Console.WriteLine("\nA chave do utilizador é:\n");
            ImprimirChave(numsUtilizador, estrelasUtilizador);

            //Output da chave gerada automáticamente
            Console.WriteLine($" A chave gerada foi: \n");
            ImprimirChave(numsAleatórios, estrelasAletorios);

            //Array que vai guardar os numeros que são encontrados em ambos os arrays.(Gerado aleatóriamente e inseridos pelo utilizador)
            int[] numerosEncontrados = NumerosExistentesNaChave(numsAleatórios, numsUtilizador, ref numsCounter);

            if (numsCounter > 0)
            {
                Console.Write("Os numeros em que acertou foram: ");
                for (int j = 0; j < numsCounter; j++)
                {
                    Console.Write($"{numerosEncontrados[j]} ");
                }
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("Não acertou nenhum número \n");
            }

            //Array que vai guardar os numeros que são encontrados em ambos os arrays.(Gerado aleatóriamente e inseridos pelo utilizador)
            int[] estrelasEncontradas = NumerosExistentesNaChave(estrelasAletorios, estrelasUtilizador, ref estrelasCounter);

            if (estrelasCounter > 0)
            {
                Console.Write("As estrelas em que acertou foram: ");
                for (int j = 0; j < estrelasCounter; j++)
                {
                    Console.Write($"{estrelasEncontradas[j]} ");
                }
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("Não acertou em nenhuma estrela \n");
            }
            
            //Switch que usa os contadores dos numeros e das chaves acertadas como expressão e compara com as opção dos prémios
            switch ((numsCounter, estrelasCounter))
            {
                case (5, 2):
                    Console.WriteLine("Parabéns ganhou o 1º prémio!!!");
                    break;
                case (5, 1):
                    Console.WriteLine("Parabéns ganhou o 2º prémio!!!");
                    break;
                case (5, 0):
                    Console.WriteLine("Parabéns ganhou o 3º prémio!!!");
                    break;
                case (4, 2):
                    Console.WriteLine("Parabéns ganhou o 4º prémio!!!");
                    break;
                case (4, 1):
                    Console.WriteLine("Parabéns ganhou o 5º prémio!!!");
                    break;
                case (4, 0):
                    Console.WriteLine("Parabéns ganhou o 6º prémio!!!");
                    break;
                default:
                    Console.WriteLine("Não ganhou qualquer prémio.");
                    break;
            }

        }
        //******************************Métodos***********************************************

        //Método para gerar uma quantidade de numeros aleatorios com limite inferior e superior.
        static int[] GeradorNumeros(int quantidade, int inferior, int superior)
        {
            //Instanciação do array
            int[] numerosAleatorios = new int[quantidade];

            //Instanciação do gerador de numeros aleatórios
            Random geradorNumeros = new Random();

            //Preenchimento dos arrays dos numeros através do método Random
            for (int i = 0; i < quantidade; i++)
            {
                int numeroGerado;
                do
                {
                    numeroGerado = geradorNumeros.Next(inferior, superior+1);

                } while (Array.IndexOf(numerosAleatorios, numeroGerado) != -1);
                numerosAleatorios[i] = numeroGerado;
            }
            //Ordenação do array por ordem crescente
            Array.Sort(numerosAleatorios);

            return numerosAleatorios;
        }
        static int[] chaveUtilizador(int quantidade, int inferior, int superior)
        {
            //Instanciação do array
            int[] numerosUtilizador = new int[quantidade];

            //Preenchimentodo array com os numeros inseridos pelo utilizador
            for (int i = 0; i < quantidade; i++)
            {
                int numeroInserido;
                do
                {
                    Console.WriteLine($"Insira o número {i + 1}: ");
                    while (!int.TryParse(Console.ReadLine(), out numeroInserido) || numeroInserido < inferior || numeroInserido > superior)
                    {
                        //Mostra esta mensagem caso o caracter inseridos não possa ser convertido para um inteiro ou seja < 1 ou seja > 50
                        Console.WriteLine("Insira um número válido! ");
                    }
                    //Procura no array para ver se o numero inserido pelo utilizador já foi inserido e imprime uma mensagem de erro caso isso aconteça
                    if (Array.IndexOf(numerosUtilizador, numeroInserido) != -1)
                    {
                        Console.WriteLine("Número já inserido. Insira um número diferente.");
                        // Define um valor  para o número inserido para permitir que  o loop continue
                        numeroInserido = -1;
                    }
                    else
                    {
                        numerosUtilizador[i] = numeroInserido;
                    }
                } while (numeroInserido == -1); // Loop enquanto a condição for verdadeira            
            }
            //Ordenação do array por ordem crescente
            Array.Sort(numerosUtilizador);
            return numerosUtilizador;
        }
        static void ImprimirChave(int[] numeros, int[] estrelas)
        {
            //Output dos numeros inseridos pelo utilizador
            Console.Write("Numeros: ");
            foreach (int number in numeros)
            {
                Console.Write($"{number} ");
            }
            //Output das estrelas inseridos pelo utilizador
            Console.Write("\nEstrelas: ");
            foreach (int estrela in estrelas)
            {
                Console.Write($"{estrela} ");
            }
            Console.WriteLine("\n\n");
        }
        //Método para ver se os numeros inseridos pelo utilizador existem na chave gerada aleatóriamente
        static int[] NumerosExistentesNaChave(int[] numsAleatorios, int[] numsUtilizador, ref int  contador)
        {
            int[] numerosIguais = new int[numsUtilizador.Length];



            for (int i = 0; i < numsUtilizador.Length; i++)
            {
                int index = Array.IndexOf(numsAleatorios, numsUtilizador[i]);

                if (index != -1)
                {
                    numerosIguais[contador++] = numsAleatorios[index];
                }
            }
            return numerosIguais;
        }
    }
}
