using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalissLexicoUri
{
    public partial class Form1 : Form
    {
        static private List<Rutas> rutas;
        public String Path_actual;
        public String nombre_acual;

        static private List<Token> lis_toks;
        public Form1()
        {
            InitializeComponent();
            rutas = new List<Rutas>();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

       
        /* Aqui se llama el texto del campurado con el objetivo de que analices*/
        private void b_correr_Click(object sender, EventArgs e)
        {
            String texto;
            texto = richTextBox1.Text;
            Analizador analiz = new Analizador();
            analiz.Analizador_cadena(texto);

            analiz.generarLista();
            comen.Text = analiz.getRetorno();


            lis_toks = new List<Token>();
            lis_toks = analiz.getListaTokens();

            for (int i = 0; i < lis_toks.Count; i++)
            {
                Token actual = lis_toks.ElementAt(i);
                MessageBox.Show("[Lexema:" + actual.getLexema() + ",IdToken: " + actual.getIdToken() + ",Linea: " + actual.getLinea() + "]", "des");
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void guardarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            guardarComo();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Boolean existe = false;
            string path = "";
            for (int i = 0; i < rutas.Count; i++)
            {
                Rutas ru = rutas.ElementAt(i);
                if (Path_actual == ru.getPath() )
                {
                    path = Path_actual;
                    existe = true;
                }
            }
            if (existe == false)
            {
                guardarComo();
            }
            else
            {
                guardar(path);
            }
        }

        private void guardar(string path)
        {
            try
            {
                //FileStream file = File.Create(path);
                //pathTH = file.Name;
                //file.Close();
                
                //text_1 = richTextBox1.Controls[];
                string text = richTextBox1.Text;
                StreamWriter writer = new StreamWriter(path);
                writer.Write(text);
                writer.Flush();
                writer.Close();

                string nombre = Path.GetFileNameWithoutExtension(path);
                //MessageBox.Show(nombre, "nombre");
                //seleccionado.Text = nombre;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void guardarComo()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "[LFP]|*.txt";
            saveFile.Title = "Guardar archivo";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = (FileStream)saveFile.OpenFile();
                fs.Close();
                string path = saveFile.FileName;
                guardar(path);
                string nombre = Path.GetFileNameWithoutExtension(path);
                Rutas path_r = new Rutas(path, nombre);
                rutas.Add(path_r);

                //MessageBox.Show(path, "path");
                //MessageBox.Show(rutas.Count.ToString(), "rutas.Count");
                Path_actual = path;
                nombre_acual = nombre;
                this.Text = nombre_acual;

            }

        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "[LFP]|*.txt";
            string texto = "";
            string fila = "";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string ruta1 = openFile.FileName;
                StreamReader streamReader = new StreamReader(ruta1, System.Text.Encoding.UTF8);
                string nombreC = Path.GetFileNameWithoutExtension(openFile.FileName);
                while ((fila = streamReader.ReadLine()) != null)
                {
                    texto += fila + System.Environment.NewLine;
                }
                richTextBox1.Text = texto;
                streamReader.Close();
                //MessageBox.Show(nombreC, "nombreC");
                //MessageBox.Show(ruta1, "ruta1");

                rutas.Clear();
                Rutas path = new Rutas(ruta1, nombreC);
                rutas.Add(path);

                //MessageBox.Show(rutas.Count.ToString() , "rutas.Count");
                Path_actual = ruta1;
                nombre_acual = nombreC;
                this.Text = nombre_acual;
    
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
   
        }

        private void analizarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.b_correr_Click(sender, e );
        }
    }
}
