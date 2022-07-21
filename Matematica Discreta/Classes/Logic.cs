using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//For Labels
using System.Windows.Forms;
using System.Drawing;



namespace Matematica_Discreta
{
    public class Logic
    {
        //privateItems

        int number;
        List<int> divisorsList;//Lista de divisores
        
        //Para subconjuntos
        List<Relaciones> setList;
        Relaciones setAux;

        //Para el diagrama de Hasse
        int height;
        List<PrimeFactor> primeDecomposition;//Lista de factores primos
        PrimeFactor auxFactor;//Auxiliar para agregar Factores Primos
        int auxInt;//Auxiliar para hallar las iteraciones de cada factor primo
        Stack<int> firstPhaseDivisors;//Para la segunda fase de llenado
        List<int> secondPhaseDivisors;//Para la primera fase de llenado
        List<int>[] hasseStack;//Para la data del diagrama de hasse
        bool keepFilling;
        bool includeInList;
        bool breakOption;

        //To show The Hasse Diagram
        //Para mostrar el diagrama de hasse
        int dx;
        int dy;
        //Coordinates[] hasseCoordinates;

        //Consola
        int ConsoleHeight;
        int ConsoleWidth;


        //Constructors
        public Logic() { 
            
        }

        public Logic(int number)
        {
            //Creamos la lista
            divisorsList = new List<int>();
            //asignamos el numero que introdujo el usuario
            this.number = number;


            //Para el diagrama de Hasse
            height = 1;
            keepFilling = true;
            includeInList = true;
            breakOption = false;
                //Separamos espacio
            primeDecomposition= new List<PrimeFactor>();
            auxInt = number;
                //Separamos espacio
            firstPhaseDivisors = new Stack<int>();
            secondPhaseDivisors = new List<int>();
            setList = new List<Relaciones>();


            //Consola
            ConsoleHeight = 700;
            ConsoleWidth = 1200;
        }


        //Other functions


        //Encontrar divisores de Number
        public void findDivisors() {
            for (int i = 0; i < number; i++)//i=1, 
            {

                if ((number % (i + 1)) == 0)
                {
                    divisorsList.Add(i + 1);
                }
            }


            

        }
        //List The divisors
        public void listDivisors(ref string cadenaDivisores) {

            cadenaDivisores += "D = {";
            for (int i = 0; i < divisorsList.Count; i++)
            {
                //If the last one
                //Si es que es el ultimo
                if (i == divisorsList.Count - 1) { 
                    cadenaDivisores += divisorsList[i] + "}";
                    break;

                }

                cadenaDivisores += divisorsList[i] + ", ";
            }

        }

        //Encontrar relaciones de orden parcial





        //Encontrar factores primos y su iteracion


        public void findPrimeDivisors() {
		    for (int i = 0; i < divisorsList.Count; i++){
			    if (isPrime(divisorsList[i])) {//If the divisor is prime, we add it to the list
				    auxFactor = new PrimeFactor(divisorsList[i]);
				    primeDecomposition.Add(auxFactor);
			    }
		    }

	    }
        //List the primeDivisors
        public void showPrimeDivisors(ref string cadenaDivisores2)
        {
            for (int i = 0; i < primeDecomposition.Count; i++)
            {
                cadenaDivisores2 += "Factor primo " + (i + 1) + ": " + primeDecomposition[i].getValue() + " con " + primeDecomposition[i].getIterations() + "repeticiones\n ";
            }
            cadenaDivisores2 += "\nAltura del grafico de Hasse: " + height;
        }

        //Count the Prime numbers iterations
        //Contar la iteracion de cada divisor

        public void findPrimeNumbersIterations() {
		    //cout << "El numero es " << auxInt << endl;
		    for (int i = 0; i <  primeDecomposition.Count; i++) {
			    while (auxInt % (primeDecomposition[i].getValue()) == 0 && (auxInt!=0)) {
				   primeDecomposition[i].incrementIterations();//Works to increment the iterations //Incrementa el contador
				    auxInt /= primeDecomposition[i].getValue();
			
			    }
		    }
	    }

        //Find the height of hasse's graph
        //Encontrar la altura del grafico de hasse

        
        public void findGraphHeight() {
		    if (divisorsList.Count == 1) {
			    height = 1;
                return;
		    }
		 
		    for (int i = 0; i < primeDecomposition.Count; i++)
		    {
			    height += primeDecomposition[i].getIterations();
		    }
	    }


        //Hasse graph Implementation
        //Implementacion del grafico de hasse

        //Copy of the divisorsList
        //Copia de la lista de divisores
        public void copyDivisorsList() {
            for (int i = 0; i < divisorsList.Count; i++){
                firstPhaseDivisors.Push(divisorsList[i]);//Copiamos los elementos en un orden inverso al normal
                                                         //Osea de mayor a menor

            }
        
        }


        public void findSets()
        {
            for (int i = 0; i < divisorsList.Count; i++)
            {
                for (int j = i; j < divisorsList.Count; j++)
                {
                    if (divisorsList[j] % divisorsList[i] == 0)
                    {
                        setAux = new Relaciones(divisorsList[i], divisorsList[j]);
                        setList.Add(setAux);
                    }
                }
            }
        }


        public void listSets(ref string cadenaTexto) {
            cadenaTexto += "P = {";

            for (int i = 0; i < setList.Count; i++)
            {
                //El último término
                if (i == setList.Count - 1)
                {
                    cadenaTexto += $"({setList[i].get_a()} , {setList[i].get_b()})" + "}";
                    break;
                }
                cadenaTexto += $"({setList[i].get_a()} , {setList[i].get_b()})" + " , ";

            }



        }




        //Filling of the hasse graph
        //Llenado del diagrama de Hasse

        //First phase
        //Primera fase



        public void hasseSpace(){
            //Memory allocation for the array
            //Separacion de espacio para el arreglo
            hasseStack = new List<int>[height];
            
            //Initialization of the lists
            //Separacion de espacio para listas
            for (int i = 0; i < height; i++) {
                hasseStack[i]= new List<int>();
            
            }
            



        }


        public void fillHasseGraph1(/*ref string cadenaTexto*/)
        {
            
            keepFilling = true;


            //We start the filling of the array f stacks
            //Comenzamos a llenar el array de litas
            for (int i = 1; i < height; i++)//Para cada una de las lineas
            {
                //cadenaTexto += $"Llenando la fila {i}, ";
                
                while (keepFilling && firstPhaseDivisors.Count > 0) {//Mientras se pueda llenar la linea y haya elementos con que rellenar


                    //If the list is empty and it is the first line
                    //Si la lista está vacia y es la primera linea
                    

                    //If the list is empty
                    //Si la lista está vacía
                    if (hasseStack[i].Count == 0 && firstPhaseDivisors.Count > 0) {


                        //Comparar con la lista de la fila anterior;
                        checkPreviousLineFirstPhase(i);

                        if (includeInList)//Incluirlo a la lista
                        {
                            hasseStack[i].Insert(0, firstPhaseDivisors.Peek());
                            //cadenaTexto += $" Elemento {firstPhaseDivisors.Peek()} agregado a la linea vacia {i}, ";

                            firstPhaseDivisors.Pop();
                        }

                        else { //Pasarlo a la segunda lista y eliminarlo del stack
                            secondPhaseDivisors.Add(firstPhaseDivisors.Peek());
                            //cadenaTexto += $" Elemento {firstPhaseDivisors.Peek()} NO agregado a la linea vacia {i}, ";
                            firstPhaseDivisors.Pop();
                        }
                    }


                    //If the list isn't empty
                    //Si la lista contiene elementos
                    else if (hasseStack[i].Count > 0 && firstPhaseDivisors.Count > 0)
                    {
                        //Check if the line ends
                        //Chequear si la lista (linea) se sigue llenando
                        checkFileEnd(i/*, ref cadenaTexto*/);
                        if (!keepFilling)//Si no se puede seguir llenando la linea
                        {
                            //
                            //cadenaTexto += $"Roto de la linea {i} por el elemento: {firstPhaseDivisors.Peek()}, ";
                            break;
                        }


                        //IF filling is possible, check if the element is a direct divisor of the previous line
                        //Si se puede seguir llenando, comprobar si el elemento es divisor directo de un elemento de arriba
                        checkPreviousLineFirstPhase(i);

                        //If includedInList we can fill it to the hasse array of lists
                        //Si es correcto, se incluye en la lista 
                        if (!includeInList)
                        {
                            secondPhaseDivisors.Add(firstPhaseDivisors.Peek());
                            //cadenaTexto += $" Elemento {firstPhaseDivisors.Peek()} NO agregado a la linea no vacia {i}, ";
                            firstPhaseDivisors.Pop();
                        }
                        else {

                            hasseStack[i].Insert(0, firstPhaseDivisors.Peek());
                            //cadenaTexto += $" Elemento {firstPhaseDivisors.Peek()} agregado a la linea no vacia {i}, ";
                            firstPhaseDivisors.Pop();
                        }





                    }


                }

                //Once the line ends up with the filling we refresh the counters
                //Cuando la linea caba, reseteamos el contador
                keepFilling = true;

            }



        }

        public void fillFirstLine() {
            hasseStack[0].Add(firstPhaseDivisors.Peek());
            firstPhaseDivisors.Pop();
        }


        //To try the HasseStack
        public void listStack(ref string cadenaTexto)
        {
            //for (int i = 0; i < secondPhaseDivisors.Count; i++)
            //{
            //    cadenaTexto += $"Elemento {i + 1} de la segunda lista: {secondPhaseDivisors[i]}, ";
            //}
            for (int i = 0; i < height; i++)
            {
                cadenaTexto += $"Hay {hasseStack[i].Count} elementos en la fila {i + 1} ";
                for (int j = 0; j < hasseStack[i].Count; j++)
                {

                    cadenaTexto += $"Elemento {j + 1} de la linea {i + 1}: {hasseStack[i][j]}, ";

                }
                cadenaTexto += "------";
            }

        }

        public void fillHasseGraph2(/*ref string cadenaTexto*/) {
            int counter = secondPhaseDivisors.Count;

            for (int j = 0; j < counter; j++)//For each element of the second list //Por cada elemento de la lista dos
            {
                //cadenaTexto += $"Llenando elemento {secondPhaseDivisors[0]}...";
                for (int i = 0; i < height ; i++)
                {
                    //Comparamos con cada elemento existente en el diagrama de hasse
                    //We check for every element in the array of linked lists
                    checKLineSecondPhase(i);
                    //If the condition is meet, we add the element to the array of linked lists
                    //Si se cumple se ñade al diagrama de hasse
                    //
                    if (breakOption)
                    {
                        hasseStack[i + 1].Insert(0, secondPhaseDivisors[0]);
                        //cadenaTexto += $"Elemento {secondPhaseDivisors[0]} agregado correctamente";
                        secondPhaseDivisors.RemoveAt(0);
                        
                        break;

                    }

                   
                }
                breakOption = false;// Reset the counter
            }
            //cadenaTexto = $"Numero de elementos que queda en la lista {secondPhaseDivisors.Count}";


        }



        //To show the diagram we are going to use labels
        //Para poder mostrar el gráfico necesitamos labels

        public void createLabels(ref Label[] hasseLabels, ref Form2 hasseForm, ref Coordinates[] hasseCoordinates) { 
            //Separamos espacio para los nuevos labels
            hasseLabels = new Label[divisorsList.Count];
            hasseCoordinates= new Coordinates[divisorsList.Count];
            //We find dx y dy
            //Encontramos dx y dy
            findDxDy();

            //We set each parameter of each label
            //Introducimos las caracteristicas de cada label

            //for (int i = 0; i < divisorsList.Count; i++)
            //{
            //    //int j = 1;
            //    hasseLabels[i] = new Label();
            //    hasseLabels[i].Text = $"{divisorsList[i]}";
            //    hasseLabels[i].Size = new Size(25, 15);
            //    hasseLabels[i].Location = new Point((i + 1) * dx,  j * dy) ;//QUIZÁS SE PUEDA CREAR UN ARRAY DE LOCALIZACIONES PARA PODER GRAFICAR LINEAS
            //    //you can set other property here like Border or else
            //    hasseForm.Controls.Add(hasseLabels[i]);
            //    j += 1;
            //}


            //int j = 1;
            for (int labelIndex = 0; labelIndex<divisorsList.Count; ) {

                for (int i = 0; i < height; i++)
                {
                    for (int k = 0; k < hasseStack[i].Count; k++)
                    {
         
                        hasseLabels[labelIndex] = new Label();
                        hasseLabels[labelIndex].Text = $"{hasseStack[i][k]}";
                        hasseLabels[labelIndex].Size = new Size(40, 22);
                        hasseLabels[labelIndex].Location = new Point((k + 1) * dx, (i + 1) * dy);

                        //Coordenadas
                        hasseCoordinates[labelIndex].x = (k + 1) * dx + hasseLabels[labelIndex].Width / 2;
                        hasseCoordinates[labelIndex].y = (i + 1) * dy + hasseLabels[labelIndex].Height / 2;



                        //you can set other property here like Border or else
                        hasseLabels[labelIndex].TextAlign = ContentAlignment.MiddleCenter;
                        hasseLabels[labelIndex].ForeColor= Color.Aqua;
                        //hasseLabels[labelIndex].Font = new Font("Forte", 10);
                        hasseLabels[labelIndex].Font = new Font("Freehand521 BT", 10);
                        hasseLabels[labelIndex].BorderStyle = BorderStyle.Fixed3D;
                        hasseLabels[labelIndex].BackColor= Color.Black;
                        //hasseLabels[labelIndex].Font = new Font("Script MT Bold", 10);


                        hasseForm.Controls.Add(hasseLabels[labelIndex]);


                        //Icrementamos el contador
                        labelIndex++;
                        //j++;
                    }

                    //Refresh the counter
                    //Refrescamos el contador
                    //j = 1;
                }

            }
        
        
        }














        //Useful functions


        public bool isPrime(int anyNumber)
        {
            int counter = 0;
            for (int i = 0; i < anyNumber; i++)
            {
                if (anyNumber % (i + 1) == 0)
                {
                    counter += 1;
                }
            }
            if (counter == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //Revisa si el siguiente elemento que se va a ingresar es divisor de alguno de los elementos de la linea (lista)
        public void checkFileEnd(int i/*, ref string cadenaTexto*/) {
            for (int j = 0; j < hasseStack[i].Count; j++)
            {
                if ((hasseStack[i][j] % firstPhaseDivisors.Peek() == 0) && isPrime((hasseStack[i][j] / firstPhaseDivisors.Peek())))
                {
                    //cadenaTexto += $"No se puede seguir llenado la fila ";
                    //We check if it is divisible and also if the division is prime
                    //If so, we break the filling of the line

                    //chequeamos si es divisible y primo
                    //Si es así, se rompe el llenado de la linea
                    keepFilling = false;
                }
                else {
                    //cadenaTexto += "Chill ";
                    keepFilling = true;
                }
            }
        }


        //Revisa si en la lista anterior alguno era multiplicador de el elemento top
        public void checkPreviousLineFirstPhase(int index){

            //Revisamos la linea previa
            //We check for the previous list
            index -= 1;//We declare the variable as the previous list
            //Hacemos que la linea sea la anterior
            for (int j = 0; j < hasseStack[index].Count; j++)//Check the previous list size
            {
                if ((hasseStack[index][j] % firstPhaseDivisors.Peek() == 0) && isPrime((hasseStack[index][j] / firstPhaseDivisors.Peek())))
                {
                    //We check if it is divisible and also if the division is prime
                    //If so, we can add it to the line, 
                    
                    includeInList = true;
                }
                //Else, we pass don't include the item
                else
                {
                    includeInList = false;
                }
            }

        }


        //Para comparar todos los elementos de la lista con el que sigue
        void checKLineSecondPhase(int index) {

            for (int j = 0; j < hasseStack[index].Count; j++)
            {
                //Comparamos con cada elemento de la lista y el elemento indice 0 de la segunda lista
                if (hasseStack[index][j] % secondPhaseDivisors[0] == 0 && isPrime((hasseStack[index][j] / secondPhaseDivisors[0])))
                {
                    
                    breakOption = true;



                }

            }

        }


        void findDxDy() {
            dy = ConsoleHeight / (height + 1);
            int max;
            max = hasseStack[0].Count;
            for (int i = 0; i < height; i++)
            {
                if (hasseStack[i].Count >= max)
                {
                    max = hasseStack[i].Count;
                }
            }
            dx = ConsoleWidth / (max + 1);
        
        }


        public int getConsoleHeight() {

            return ConsoleHeight;
        }

        public int getConsoleWidth()
        {

            return ConsoleWidth;
        }

    }
}
