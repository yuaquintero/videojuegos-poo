namespace CineAPP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opcion = 1;

            Sala[] salas = new Sala[3];
            salas[0] = new Sala(1, new Funcion("Matrix", "Accion", "10:00 pm", "Ingles", 210));
            salas[1] = new Sala(2, new Funcion("Up", "Aminacion", "11:00 am", "Ingles", 180));
            salas[2] = new Sala(3, new Funcion("El exorcista", "Terror", "10:00 pm", "Ingles", 150));

            while(opcion!=0)
            {
                Console.WriteLine("\n*******************************");
                Console.WriteLine("***   BIENVENIDO AL CINEMA  ***");
                Console.WriteLine("*                             *");
                Console.WriteLine("*  Seleccione una opción      *");
                Console.WriteLine("*    1. Ver la cartelera      *");
                Console.WriteLine("*    2. Reservar asiento      *");
                Console.WriteLine("*    3. Cancelar reservar     *");
                Console.WriteLine("*    4. Ver mapa de ocupación *");
                Console.WriteLine("*    0. Salir                 *");
                Console.WriteLine("*******************************");
                opcion = int.Parse(Console.ReadLine());
                switch(opcion)
                {
                    case 1:
                        Console.WriteLine("************* Cinema # *************");
                        for (int i = 0; i < 3; i++)
                        {
                            Console.WriteLine("********************************");
                            Console.WriteLine("Sala : {0}", i + 1);
                            Console.WriteLine(salas[i].Funcion.MostrarInformacion());
                        }
                        break;
                    case 2:
                        Console.WriteLine("Ingrese el nombre de la pelicula");
                        string nombre= Console.ReadLine();
                        int NumSala = -1;
                        for (int i = 0; i < 3; i++)
                            {
                             if (salas[i].Funcion.NombrePelicula.Equals
                                (nombre, StringComparison.OrdinalIgnoreCase))
                                     NumSala = i;
                            }
                        if(NumSala != -1)
                        {
                            Imprimir(salas[NumSala].MapaOcupacion, NumSala);
                            char fila;
                            int asiento;
                            Console.WriteLine("Indique la fila");
                            fila = char.Parse(Console.ReadLine());
                            Console.WriteLine("Ingrese el asiento");
                            asiento= int.Parse(Console.ReadLine());
                            if (salas[NumSala].ReservarAsientos(fila, asiento))
                            {
                                Console.WriteLine("Reserva realiza con éxito");
                                Console.WriteLine("Desea imprimir el recibo? \n1. Si \n2. No");
                                int respuesta = int.Parse(Console.ReadLine());
                                if(respuesta==1)
                                {
                                    Console.WriteLine(salas[NumSala].Funcion.MostrarInformacion());
                                    Console.WriteLine("Sala : {0}", NumSala + 1);
                                    Console.WriteLine("Asiento: {0}{1}", fila, asiento);
                                }
                            }
                        }
                            break;


                    case 3: 
                        break;
                    case 4:
                            Console.WriteLine("Ingrese el numero de la sala");
                            int numSala= int.Parse(Console.ReadLine())-1;    
                            Imprimir(salas[numSala].MapaOcupacion, numSala);
                            break;


             }
            }
         }
    
       public static void Imprimir(char[,] mapa, int numSala)
        {
            Console.WriteLine("  Ocupación de la sala {0}", numSala + 1);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("-- ");
            for (int i = 1; i <= 11; i++)
                Console.Write(" {0} ", i);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            for (int i=0; i<10; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(" {0} ", (char)(i + 65));
                for (int j = 0; j < 11; j++)
                {
                    if(mapa[i, j]=='-')
                        Console.ForegroundColor= ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" {0} ", mapa[i, j]);
                }
                
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    
    
    }
}