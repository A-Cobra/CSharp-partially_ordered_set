using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matematica_Discreta
{
    public partial class Form1 : Form
    {

        //Form newForm;
        int number;
        //int numberofDivisors;
        //List<Divisor> divisor;
        List<int> divisorsList;
        //List<Relaciones> subconjunctsList;
        //int aux;
        string cadenaTexto;
        string cadenaTexto2;
        //Relaciones auxiliar;


        //List<Relaciones> listOPrelationships;


        //Prueba
        Logic programa;
        Form3 previousForm;


        public Form1(ref Form3 previousForm)
        {

            InitializeComponent();
            //listOPrelationships = new List<Relaciones>();
            //Calculos calculos = new Calculos();
            cadenaTexto = "";
            cadenaTexto2 = "";
            this.previousForm = previousForm;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (txtNumero.Text != "") {

                cadenaTexto = "";
                cadenaTexto2 = "";


                //divisorsList = new List<int>();
                //divisor = new List<int>();
                int numero = Convert.ToInt32(txtNumero.Text);

                if (numero > 0 && numero < 10000) {

                    //Prueba
                    programa = new Logic(numero);
                    programa.findDivisors();
                    programa.listDivisors(ref cadenaTexto);
                    programa.findPrimeDivisors();
                    programa.findSets();
                    programa.listSets(ref cadenaTexto2);

                    //NO BORRAR LOS COMENTARIOS POR FAVOR
                    //programa.findPrimeNumbersIterations();
                    //programa.findGraphHeight();
                    //programa.showPrimeDivisors(ref cadenaTexto2);
                    //
                    //programa.copyDivisorsList();
                    //programa.hasseSpace();
                    //programa.fillFirstLine();
                    //programa.fillHasseGraph1(/*ref cadenaTexto2*/);
                    //programa.fillHasseGraph2(/*ref cadenaTexto2*/);
                    //programa.listStack(ref cadenaTexto2);

                    txtDivisores.Text = cadenaTexto;
                    txtSubconjuntos.Text = cadenaTexto2;

                    //Para hallar subconjuntos
                    /*
                    for(int i = 0; i < divisorsList.Count; i++)
                    {
                        for(int j = i; j < divisorsList.Count; j++)
                        {

                            if (divisorsList.ElementAt(j) % divisorsList.ElementAt(i) == 0)
                            {
                                subconjunctsList.Add(type of String());

                            }

                        }


                    }

                    */
                    

                    //for (int i = 0; i < divisorsList.Count; i++)
                    //   {
                    //       for (int j = i; j < divisorsList.Count; j++)
                    //       {
                    //           if (divisorsList[j] % divisorsList[i] == 0)
                    //           {
                    //               //auxiliar = new Relationships;
                    //               auxiliar.set_a(divisorsList[i]);
                    //               auxiliar.set_b(divisorsList[j]);
                    //               subconjunctsList.Add(auxiliar);

                    //           }
                    //       }
                    //   }

                }
                else
                {
                    MessageBox.Show("Ingrese un número mayor a 0 y menor a 10000");
                }

            }

        }
        private void hasseDiagramBtn_Click(object sender, EventArgs e)
        {
            
            if (txtNumero.Text!="") {

                int numero = Convert.ToInt32(txtNumero.Text);
                //Si y solo si el numero es mayor a 0 podemos mostrr o abrir el diagrama de hasse
                if (numero > 0 && numero < 10000)
                {

                    //Hide this form 
                    //Ocultar este Form
                    txtNumero.Clear();
                    txtDivisores.Clear();
                    txtSubconjuntos.Clear();
                    this.Hide();



                    //IMPORTANTE
                    //Funciones para llenar programa
                    cadenaTexto = "";
                    cadenaTexto2 = "";
                    //Prueba
                    programa = new Logic(numero);
                    programa.findDivisors();
                    //programa.listDivisors(ref cadenaTexto);
                    programa.findPrimeDivisors();
                    programa.findPrimeNumbersIterations();
                    programa.findGraphHeight();
                    //programa.showPrimeDivisors(ref cadenaTexto2);
                    //
                    programa.copyDivisorsList();
                    programa.hasseSpace();
                    programa.fillFirstLine();
                    programa.fillHasseGraph1(/*ref cadenaTexto2*/);
                    programa.fillHasseGraph2(/*ref cadenaTexto2*/);
                    //programa.listStack(ref cadenaTexto2);
                    //txtDivisores.Text = cadenaTexto;
                    //txtSubconjuntos.Text = cadenaTexto2;
                    //txtNumero.Clear();







                    //Create an instance of Form2
                    //Crear un objeto de Form2
                    Form2 displayHasse = new Form2(ref programa);

                    //Show Form2
                    //mostrar Form2
                    displayHasse.ShowDialog();

                    //Dispose Form2
                    //Deshacerse de Form2
                    displayHasse = null;

                    //Show form1 again
                    //Mostrar form1 de nuevo
                    this.Show();


                }

                else
                {
                    MessageBox.Show("Ingrese un número mayor a 0 y menor a 10000");
                }


            }
            txtNumero.Clear();


        }
        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar>=32 && e.KeyChar<=47)|| (e.KeyChar>=58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo números enteros", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            previousForm.Close();
        }
    }
}
