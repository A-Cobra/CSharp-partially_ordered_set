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
    public partial class Form2 : Form
    {
        //Private variables;
        Logic programa;
        Label[] hasseLabels;

        //Hasse
        Form2 hasseForm;
        Coordinates[] hasseCoordinates;

        public Form2()
        {
            InitializeComponent();
        }

        //Graph
        

        //Sobrecarga de formulario
        public Form2(ref Logic programa)
        {
            InitializeComponent();
            this.programa = programa;
            hasseForm = this;
        }

        public void createFormLabels(){
            //Le pasamos el label para inicializarlo en Logic;
            programa.createLabels(ref hasseLabels, ref hasseForm, ref hasseCoordinates);

            //hasseLabels[0].Size = new Size(40, 15);

            //for (int i = 0; i < hasseLabels.Length; i++)
            //{
            //    this.Controls.Add(hasseLabels[i]);

            //}




        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Size= Size = new Size(programa.getConsoleWidth(), programa.getConsoleHeight());
            createFormLabels();
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {

            //We draw the lines

            Graphics background = e.Graphics;

            Brush blue = new SolidBrush(Color.Blue);//Brocha
            Pen bluePen = new Pen(blue, 2);//Lapicero

            for (int i = 0; i < hasseLabels.Length - 1; i++)
            {
                for (int j = i + 1; j < hasseLabels.Length; j++)
                {
                    //Si la division entre el numero que se encuentra en el label i 
                    //y el label j es primo, entonces dibujamos una linea
                    //Se usa la funcion isPrime de la clase Logic, ya que no es necesario tener que escribirlo otra vez en esta clase
                    if (((Convert.ToInt32(hasseLabels[i].Text)) % (Convert.ToInt32(hasseLabels[j].Text)) == 0) && 
                        programa.isPrime((Convert.ToInt32(hasseLabels[i].Text)) / (Convert.ToInt32(hasseLabels[j].Text)))) 
                    {
                        background.DrawLine(bluePen, hasseCoordinates[i].x, hasseCoordinates[i].y, hasseCoordinates[j].x, hasseCoordinates[j].y);  
                    
                    }
                }
            }
        }
    }
}
