using System;

namespace Pruebas2
{
    /// <summary>
    /// Hacer un 3 en raya
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            bool jugador = false;//Esta variable es para indicar a qué jugador le toca
            char[,] tablero = new char[3, 3];

            for (int i = 0; i < 3; i++)
            {//Esto es para rellenar el tablero
                for (int j = 0; j < 3; j++)
                {
                    tablero[i, j] = 'n';
                }
            }
            bool ganado = false;//Esto es para decir que un principio no hay ganador
            //Empieza el juego y uso las funciones para ello. el bucle seguirá hasta que haya 3 en raya lo cual se comprueba con el método ganado
            while (ganado == false)
            {
                jugador = cambioTurno(jugador);
                casillaFicha(jugador, ref tablero);
                ganado = ganador(ref tablero);
                Console.WriteLine("Presiona una tecla para continuar");
                Console.ReadKey();
                Console.Clear();
                //Voy a imprimir para ver si funciona
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine();
                    for (int j = 0; j < 3; j++)
                    {
                        Console.Write(tablero[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("¡¡¡HAS GANADO!!!");
        }


        //AQUÍ EMPIEZAN LOS MÉTODOS

        //Este es para cambiar de turno. Se llamará en método casillaFicha para poner círuclos o cruces
        public static bool cambioTurno(bool jugador)
        {
            if (jugador) return false;
            else return true;
        }
        //Este método es para colocar las fichas en el tablero. Tiene diferentes partes que paso a explicar:
        public static void casillaFicha(bool jugador, ref char[,] tablero)
        {

            char signoJugador;
            bool correcto = true;
            int pos1 = 0, pos2 = 0;
            //El booleano de jugador va cambiando en cada recorrido del método gracias al método cambioTurno que es llamado en el main
            if (jugador)
            {
                signoJugador = 'O';
                Console.WriteLine("Es el turno del jugador 1, con fichas O");
            }
            else
            {
                signoJugador = 'X';
                Console.WriteLine("Es el turno del jugador 2, con fichas X");
            }
            //Este bucle sirve para elegir una casilla donde poner la ficha
            do
            {
                Console.WriteLine("Introduce el número de la fila para poner la fila o seleciona tu ficha para poder cambiarla");
                rellenoCondiciones(ref pos1, ref pos2, ref correcto);//Este método sirve para guardar la posición de la matriz tablero
            } while (pos1 < 0 || pos1 > 3 || pos2 < 0 || pos2 > 3 || !correcto);


            do
            {
                if (tablero[pos1, pos2] == 'n') tablero[pos1, pos2] = signoJugador;//Si la casilla está vacía se puede poner la ficha
                else if (tablero[pos1, pos2] == signoJugador)//Si está ocupada por una de tus fichas, te obliga a cambiarla de lugar
                {
                    tablero[pos1, pos2] = 'n';
                    Console.WriteLine("Selecciona una fila donde poner la ficha que has cambiado.");
                    rellenoCondiciones(ref pos1, ref pos2, ref correcto);
                    if (tablero[pos1, pos2] != 'n')
                    { //Si el lugar donde vas a cambiar la ficha está ocupado, te obliga a cambiar de casilla
                        Console.WriteLine("Esta posición está ocupada por una ficha tuya"); correcto = false;
                    }
                    else tablero[pos1, pos2] = signoJugador;//Si está vacío se puede poner la ficha
                }
                else
                {//Si la casilla donde quieres poner tu ficha está ocupada por el otro jugador te bliga a cambiar de casilla
                    Console.WriteLine("Este espacio está ocupado por la ficha del jugador contrario. Por favor, selecciona una ficha para cambiarla de sitio");
                    rellenoCondiciones(ref pos1, ref pos2, ref correcto);

                    if (tablero[pos1, pos2] != 'n')
                    {//Este te obliga a cambiar de casilla por si sigues eligiendo una casilla ocupada
                        Console.WriteLine("Esta casilla tampoco esta libre");
                        correcto = false;
                    }
                    else
                    {
                        tablero[pos1, pos2] = signoJugador;
                    }
                }
                if (otraFicha(tablero) == false)//El método de otraFicha te indica que si hay 3 fichas iguales no puedes añadir otra
                {
                    tablero[pos1, pos2] = 'n';
                    correcto = false;
                    Console.WriteLine("No puede poner más de 3 fichas");
                    return;
                }
            } while (pos1 < 0 || pos1 > 3 || pos2 < 0 || pos2 > 3 || !correcto);
        }

        public static bool ganador(ref char[,] tablero)//Este es para saber si hay un ganador
        {
            bool jugadorGanador = false;
            for (int i = 0; i < 3; i++)
            {
                if (tablero[i, 0] != 'n' && tablero[i, 0] == tablero[i, 1] && tablero[i, 0] == tablero[i, 2]) return true;//Comprueba columnas
                else if (tablero[0, i] != 'n' && tablero[0, i] == tablero[1, i] && tablero[0, i] == tablero[2, i]) return true;//Comprueba filas
                else if (tablero[0, 0] != 'n' && tablero[0, 0] == tablero[1, 1] && tablero[0, 0] == tablero[2, 2]) return true;//Comprueba diagonales
                else if (tablero[0, 2] != 'n' && tablero[0, 2] == tablero[1, 1] && tablero[0, 2] == tablero[2, 0]) return true;//Comprueba diagonales
                else jugadorGanador = false;
            }
            return jugadorGanador;
        }

        public static bool otraFicha(char[,] tablero)//Este método es para saber si se puede añadir otra ficha más o si hay 3 ya en el tablero
        {
            int contO = 0, contX = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tablero[i, j] == 'O') contO++;
                    else if (tablero[i, j] == 'X') contX++;
                }
            }
            if (contX >= 4 || contO >= 4) return false;
            else return true;

        }
        public static void rellenoCondiciones(ref int pos1, ref int pos2, ref bool correcto)//Para pedir posicion de matriz
        {
            correcto = Int32.TryParse(Console.ReadLine(), out pos1);
            pos1 -= 1;
            Console.WriteLine("Ahora introduce el número de la columna:");
            correcto = Int32.TryParse(Console.ReadLine(), out pos2);
            pos2 -= 1;
        }




    }
}







