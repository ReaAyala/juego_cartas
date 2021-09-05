using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace proyecto
{
    public partial class Form1 : Form
    {
        public List<Cartas> cartas;
        public List<Cartas> mazo1; //mazo del primer jugador 
        public List<Cartas> mazo2; //mazo del segundo jugador
        public List<string> mano2; //mano del segundo jugador
        public List<string> mano; //mano del primer jugador
        public Form1()
        {

            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cartas = new List<Cartas>();
            mazo1 = new List<Cartas>();
            mazo2 = new List<Cartas>();
            mano2 = new List<string>();
            mano = new List<string>();

            StreamReader sr = new StreamReader("mazo.txt");
            while (!sr.EndOfStream)
            {
                var vari = sr.ReadLine().Split(',');
                string nombre = vari[0];
                int ataque = Convert.ToInt32(vari[1]);
                int defensa = Convert.ToInt32(vari[2]);
                string elemento = vari[3];
                bool activo = true;
                Cartas c = new Cartas(ataque, defensa, elemento, nombre,activo);                           
                cartas.Add(c);
            }
            sr.Close();         
            int contador = 0;
            foreach (var i in cartas)
            {
                contador++;
               
                if (contador <= 20) {
                    mazo1.Add(i);
                }
                if (contador>20 && contador <41) {
                    mazo2.Add(i);
                }
            }
            Random random = new Random();
            int numero = random.Next(3);
            if (numero == 1)
            {
                botonjugador1.Text = "ataca";
                botonjugador2.Text = "defiende";
            }
            else
            {
                botonjugador1.Text = "defiende";
                botonjugador2.Text = "ataca";
            }
            numero = random.Next(4);



            textoataque.Visible = false;
            textodefesa.Visible = false;
            textoelemento.Visible = false;
            textBoxataque2.Visible = false;
            textBoxdefensa2.Visible = false;
            textBoxelemento2.Visible = false;

            
            llenar_cartas2();
            comboBoxCarta1.SelectedIndex = 0;
            comboBoxcarta2.SelectedIndex = 0;

            actualizar_vidas();


        }

        bool a = false, b = false;
        bool evento = false; // esta varibel se usa para dar el evento de atacar o defender
        int vidasj1 = 3, vidasj2 = 3;



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var i in cartas)
            {
                if (comboBoxCarta1.Text == i.Nombre) {
                    textoataque.Text = i.Ataque.ToString();
                    textodefesa.Text = i.Defesa.ToString();
                    textoelemento.Text = i.Elemento;
                }
            }
        }

        private void comboBoxcarta2_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var i in cartas)
            {
                if (comboBoxcarta2.Text == i.Nombre)
                {
                    textBoxataque2.Text = i.Ataque.ToString();
                    textBoxdefensa2.Text = i.Defesa.ToString();

                    textBoxelemento2.Text = i.Elemento.ToString();
                }
            }
        }
        public void actualizar_vidas(){

            vidaj1.Text = vidasj1.ToString();
            vidaj2.Text = vidasj2.ToString();

        }
        public void llenar_cartas2() {
            Random ran = new Random();
            int numero = ran.Next(mazo1.Count);
            int prueba = mazo1.Count;
            int contador = 0;
            bool encontro = false;
            string nombre = "";
            while (contador != 5) {
                encontro = false;
                numero = ran.Next(mazo1.Count);
                nombre = mazo1[numero].Nombre;
                foreach (var i in mano)
                {
                    if (i == nombre) {
                        encontro = true;
                        
                    }
                }
                if (!encontro) {
                    mano.Add(nombre);
                    comboBoxCarta1.Items.Add(nombre);
                    mazo1.RemoveAt(numero);
                    cartas[numero].Activo = false;
                    contador++;
                }
                
            }
            //a qui se llena el combo del jugaor dos sin cartas repatidas
            contador = 0;
            while (contador != 5) {
                encontro = false;
                numero = ran.Next(mazo2.Count);
                nombre = mazo2[numero].Nombre;
                foreach (var t in mano2)
                {
                    if (t == nombre) {
                        encontro = true;
                    }
                }
                if (!encontro) {
                    mano2.Add(nombre);
                    comboBoxcarta2.Items.Add(nombre);
                    mazo2.RemoveAt(numero);
                    cartas[numero].Activo = false;
                    contador++;
                }
            }

        }
        public void inicia_boton(){
            if (a == true && b == true) {
                botoninicia.Visible = true;               
            }
}
        private void botonjugador1_Click(object sender, EventArgs e)
        {
            if (botonjugador1.Text == "defiende") {
                evento = true; // false = jugador uno ataca true = jugador uno defiende
            }
            if (botonjugador1.Text == "ataca")
            {
                evento = false;
            }
            botonjugador1.Text = "¡Listo!";
            b = true;
            inicia_boton();
        }
        private void botonjugador2_Click(object sender, EventArgs e)
        {
            botonjugador2.Text = "¡Listo!";
            a = true;
            inicia_boton();

        }
        private void checkBoxVerj1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxVerj1.Checked)
            {
                textoataque.Visible = true;
                textodefesa.Visible = true;
                textoelemento.Visible = true;
            }
            else {
                textoataque.Visible = false;
                textodefesa.Visible = false;
                textoelemento.Visible = false;
            }
        

        }
        private void checkBoxVerj2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxVerj2.Checked)
            {
                textBoxataque2.Visible = true;
                textBoxdefensa2.Visible = true;
                textBoxelemento2.Visible = true;
            }
            else {
                textBoxataque2.Visible = false;
                textBoxdefensa2.Visible = false;
                textBoxelemento2.Visible = false;
            }
        }
        public void quitar_agregar_carta()
        {
            string a_quitar1 = comboBoxCarta1.SelectedItem.ToString();
            string a_quitar2 = comboBoxcarta2.SelectedItem.ToString();
            
            foreach (var i in cartas)
            {
                if (i.Nombre == a_quitar1)
                {
                    comboBoxCarta1.Items.Remove(a_quitar1);                   
                    mano.Remove(i.Nombre);                  
                }
                if (i.Nombre == a_quitar2) {                    
                    comboBoxcarta2.Items.Remove(a_quitar2);                  
                    mano2.Remove(i.Nombre);
                }
            }
        }
        public void carta_alzar() {
            Random ran = new Random();
            int numero = 0;
            bool encontroactivo = false,encontroactivo2 = false;
            string nombre = "";
            while (!encontroactivo && !encontroactivo2)
            {
                bool encontro = false;
                numero = ran.Next(mazo1.Count);
                
                nombre = mazo1[numero].nombre;
                foreach (var i in mano)
                {
                    if (i == nombre)
                    {
                        encontro = true;

                    }
                }
                if (!encontro)
                {
                    mano.Add(nombre);
                    comboBoxCarta1.Items.Add(nombre);
                    mazo1.RemoveAt(numero);
                    encontroactivo = true;
                    
                }

                bool encontro2 = false;
                numero = ran.Next(mazo2.Count);
                nombre = mazo2[numero].nombre;
                foreach (var t in mano2)
                {
                    if (t == nombre) {
                        encontro2 = true;
                    }
                }
                if (!encontro2) {
                    mano2.Add(nombre);
                    comboBoxcarta2.Items.Add(nombre);
                    mazo2.RemoveAt(numero);
                    encontroactivo2 = true;
                }

            }
            comboBoxCarta1.SelectedIndex = 0;
            comboBoxcarta2.SelectedIndex = 0;
        }


        public void nuevo_turno() {
            botoninicia.Visible = false;
            if (vidasj1 == 0 || vidasj2 == 0) {
                MessageBox.Show("fin del juego");
            }
            if (!evento)
            {
                botonjugador1.Text = "ataca";
                botonjugador2.Text = "defiende";
            }
            else {
                botonjugador1.Text = "defiende";
                botonjugador2.Text = "ataca";
            }

        }

        private void botoninicia_Click(object sender, EventArgs e)
        {
            int ataquej1 = Convert.ToInt32(textoataque.Text);
            int defensaj1 = Convert.ToInt32(textodefesa.Text);
            string elementoj1 = textoelemento.Text;

            int ataquej2 = Convert.ToInt32(textBoxataque2.Text);
            int defensaj2 = Convert.ToInt32(textBoxdefensa2.Text);
            string elemntoj2 = textBoxelemento2.Text;

            //eljugador uno ataca
            if (!evento)
            {
                //el jugador uno ataca y le elemento cumple con alguna de las reglas
                if (elementoj1 == "hielo" && elemntoj2 == "fuego")
                {
                    ataquej1 = ataquej1 * 2;
                    defensaj1 = defensaj1 * 2;
                }
                if (elementoj1 == "fuego" && elemntoj2 == "tierra") {
                    ataquej1 = ataquej1 * 2;
                    defensaj1 = defensaj1 * 2;
                }
                if (elementoj1 == "tierra" && elemntoj2 == "electricidad") {
                    ataquej1 = ataquej1 * 2;
                    defensaj1 = defensaj1 * 2;
                }
                if (elementoj1 == "electricidad" && elemntoj2 == "hielo") {
                    ataquej1 = ataquej1 * 2;
                    defensaj1 = defensaj1 * 2;
                }
                //se acutlizara el valor del texto
                textoataque.Text = ataquej1.ToString();
                textodefesa.Text = defensaj2.ToString();
                //jugador 1 ataca 
                if (ataquej1 >= defensaj2)
                {
                    //el jugador 2 pierde una vida
                    vidasj2--;
                    actualizar_vidas();
                   quitar_agregar_carta();
                    
                    carta_alzar();
                }
                else {
                    quitar_agregar_carta();
                    
                    carta_alzar();
                }


                //fin del turno
                evento = true;
                nuevo_turno();
            }
            else {
                //el jugador 2 ataca y se verivican las reglas de elementos
                if (elemntoj2 == "hielo" && elementoj1 == "fuego")
                {
                    ataquej2 = ataquej2 * 2;
                    defensaj2 = defensaj2 * 2;
                }
                if (elemntoj2 == "fuego" && elementoj1 == "tierra")
                {
                    ataquej2 = ataquej2 * 2;
                    defensaj2 = defensaj2 * 2;
                }
                if (elemntoj2 == "tierra" && elementoj1 == "electricidad")
                {
                    ataquej2 = ataquej2 * 2;
                    defensaj2 = defensaj2 * 2;
                }
                if (elemntoj2 == "electricidad" && elementoj1 == "hielo")
                {
                    ataquej2 = ataquej2 * 2;
                    defensaj2 = defensaj2 * 2;
                }
                //se actualiza el texto de los jugadores
                textBoxataque2.Text = ataquej2.ToString();
                textBoxdefensa2.Text = defensaj2.ToString();
                if (ataquej2 >= defensaj1)
                {
                    vidasj1--;
                    actualizar_vidas();
                    quitar_agregar_carta();                   
                    carta_alzar();

                }
                else {
                    quitar_agregar_carta();                   
                    carta_alzar();
                }
                //fin del turno
                evento = false;
                //se actualizan los texto de los botones
                nuevo_turno();
            }



        }
    }
}
