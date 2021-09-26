using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalissLexicoUri
{
    /* En esta clase se recibe el texto con la variable de entrada*/
    // Tambien se crea el listado  y la clase
    class Analizador
    {
        ArrayList tokens;
        ArrayList tipos;

        ArrayList Operaciones;
        ArrayList Graficas_ar;

        ArrayList Lista_Operaciones;
        static private List<Token> listaTokens;

        
        private String retorno;
        public int estado_token;

        //errores tokens
        static private List<ErroresToken> listaErrores;

        public Analizador()
        {
            //this.listaTokens = new List<Token>();
            listaTokens = new List<Token>();
            tokens = new ArrayList();
            tipos = new ArrayList();

            tipos.Add("Valor");
            tipos.Add("Operador");
            tipos.Add("IZQ");
            tipos.Add("DER");


            Lista_Operaciones = new ArrayList();
          

            //errores toks
            listaErrores = new List<ErroresToken>();

        }
        //Pues esto recibe los parametros y se va almacenando la lista de token
        public void addToken(String lexema, String idToken, int linea, int columna, int indice)
        {
            //MessageBox.Show("*" + lexema + "* lin: " + linea + " col: " + columna, "Lexema_final");
            //se llama al listado y se la enviando los datos que se recibieron
            Token nuevo = new Token(lexema, idToken, linea, columna, indice );
            listaTokens.Add(nuevo);
        }

        public void addError(String lexema, String idToken, int linea, int columna)
        {
            ErroresToken errtok = new ErroresToken(lexema, idToken, linea, columna);
            listaErrores.Add(errtok);
        }
        /* Aqui toma la cadena ingresada en el codigo si no se ingresa un espacio en blanco no toma la ultima palabra.
         Mejor dicho, si escribo gato volador y despues volador.*/
        public void Analizador_cadena(String entrada)
        {
            int estado = 0;
            int columna = 0;
            int fila = 1;
            string lexema = "";
            Char c;
            //MessageBox.Show(entrada, "111 entrada");
            entrada = entrada + " ";
            //entrada = entrada;
            //MessageBox.Show(entrada, "222 entrada");
            //Este for es para reccorer caracter por caracter con una variable 'C' que se le asigno a el caracter actual
            for (int i = 0; i < entrada.Length; i++)
            {
                c = entrada[i];
                columna++;
                //MessageBox.Show(c.ToString(), i.ToString() );
                //MessageBox.Show(estado.ToString(), "estado");

                //Bueno, aqui se incializa el analizador lexico en la cual primero se crea un switch que basicamente va leyendo el estado.
                //Al iniciar le asignamos un estado inicial que viene siendo 0
                switch (estado)
                {
                    //En el estado inicial se verifica que tipo de caracter, en caso, de ser letra pasa al estado 1,
                    //sino es asi y es un numero pasa el estado 2, toda veces se concateneras para armar la palabra ingresada
                    case 0:
                        //columna++;
                        if (Char.IsLetter(c))
                        {
                            estado = 1;
                            lexema += c;
                        }
                        else if (Char.IsDigit(c))
                        {
                            estado = 2;
                            lexema += c;
                        }
                        //else if (c == '-')
                        //{
                        //    estado = 3;
                        //    lexema += c;
                        // }

                        // si es comilla se asigna el estado 4.
                        else if (c == '"')
                        {
                            estado = 4;
                            i--;
                            columna--;
                        }
                        else if (c == ',')
                        {
                            estado = 6;
                            i--;
                            columna--;
                        }
                        else if (c == ' ')
                        {
                            estado = 0;
                        }
                        else if (c == '\n')
                        {
                            columna = 0;
                            fila++;
                            estado = 0;
                        }
                        /*nuevos*/
                        else if (c == '{')
                        {
                            lexema += c;
                            ////addToken(lexema, "llaveIzq", pos + 1, 0);

                            addToken(lexema, "llaveIzq", fila, columna, i - lexema.Length );
                            lexema = "";
                        }
                        else if (c == '}')
                        {
                            lexema += c;
                            addToken(lexema, "llaveDer", fila, columna, i - lexema.Length);
                            ////addToken(lexema, "llaveDer", pos + 1, 0);
                            lexema = "";
                        }
                        else if (c == '(')
                        {
                            lexema += c;
                            addToken(lexema, "parIzq", fila, columna, i - lexema.Length);
                            lexema = "";
                        }
                        else if (c == ')')
                        {
                            lexema += c;
                            addToken(lexema, "parDer", fila, columna, i - lexema.Length );
                            lexema = "";
                        }
                        else if (c == ',')
                        {
                            lexema += c;
                            //addToken(lexema, "coma", pos + 1, 0);
                            lexema = "";
                        }

                        else if (c == ';')
                        {
                            lexema += c;
                            addToken(lexema, "Punto y Coma", fila, columna, i - lexema.Length);
                            lexema = "";
                        }

                        else if (c == '<')
                        {
                            lexema += c;
                            addToken(lexema, "Menor", fila, columna , i - lexema.Length);
                            lexema = "";
                        }
                        else if (c == '>')
                        {
                            lexema += c;
                            addToken(lexema, "Mayor", fila, columna, i - lexema.Length);
                            lexema = "";
                        }

                        else if (c == '.')
                        {
                            lexema += c;
                            addToken(lexema, "Punto", fila, columna, i - lexema.Length );
                            lexema = "";
                        }

                        /*fin nuevos*/
                        // Pues aqui termina el estado 1 en este se le asignara todos los caracteres que tendra el codigo, lo que se va usar parentesis
                        // operadores -, +, signos % &
                        /*operadores mat*/
                        else if (c == '+')
                        {
                            lexema += c;
                            addToken(lexema, "Suma", fila, columna, i);
                            lexema = "";
                        }
                        else if (c == '-')
                        {
                            lexema += c;
                            addToken(lexema, "Menos", fila, columna, i );
                            lexema = "";
                        }
                        else if (c == '*')
                        {
                            lexema += c;
                            addToken(lexema, "Multiplicacion", fila, columna, i );
                            lexema = "";
                        }
                        else if (c == '/')
                        {
                            lexema += c;
                            addToken(lexema, "Division", fila, columna, i);
                            lexema = "";
                        }


                        /*fin operadors mat*/
                        //Este verifica si  el caracter no es valido a cual se le da un estado -99
                        // se le resta i-- porque este interpreta un porciento y ahi tomara un valor de -99
                        else
                        {
                            //addError(c.ToString() , "Desconocido", fila, columna);
                            estado = -99;
                            i--;
                            columna--;
                        }
                        break;
                        //Aqui hay una validacion para que acepte numeros y letras y una rayita bajo mejor dicho uriel_2. kinston_vc22@hotmail.com
                        
                    case 1:
                        //if (Char.IsLetter(c))
                        if (Char.IsLetterOrDigit(c) || c == '_')
                        {
                            lexema += c;
                            estado = 1;
                            //MessageBox.Show("*1*"+lexema + "*1*", "lexema");
                        }
                        //Este indica que se termino de leer el caracter ingresado o la palabra
                        else
                        {

                            Boolean encontrado = false;
                            /*if (verificarReservada(lexema))*/
                            encontrado = Macht_enReser(lexema);
                            if (encontrado)
                            {
                                //token = new Token(1, "PalabraReservada", lexema, fila, columna);
                                //tokens.add(token);

                                //////////////////////////77MessageBox.Show("*1*" + lexema + "*1*", "lexema");
                                
                                /// aqui se va enlistando e interpretar si la palabra era reservada o indentificador
                                addToken(lexema, "Reservada", fila, columna, i - lexema.Length);
                            }
                            //Aqui se va guardando un alistado aqui se envia la clase addToken a almacenar el lexema, la descripcion y fila
                            else
                            {
                                ////////////////////////777MessageBox.Show("*2*" + lexema + "*2*", "lexema");
                                addToken(lexema, "Identificador", fila, columna, i - lexema.Length);
                                /*nuevo inicio*/
                                /*Boolean encon_tipo = false;
                                encon_tipo = Macht_enTipo(lexema);
                                if (encon_tipo)
                                {  addToken(lexema, "Tipo", fila, columna);  }
                                else
                                {  addToken(lexema, "Identificador", fila, columna);  }*/
                                 /*nuevo fin*/


                                }
                            //MessageBox.Show("*2*" + lexema + "*2*", "lexema");
                            //addToken(lexema, "Identificador", fila, columna);

                            lexema = "";
                            i--;
                            columna--;
                            estado = 0;
                        }
                        break;
                    case 2:
                        //aqui se chequea si es numero
                        if (Char.IsDigit(c))
                        {
                            lexema += c;
                            estado = 2;
                        }
                        /*nuevo*/
                        else if (c == '.')
                        {
                            estado = 8;
                            lexema += c;
                        }
                        /*nuevo fin*/
                        else
                        {
                            //token = new Token(3, "Numero", lexema, fila, columna);
                            //tokens.add(token);
                            addToken(lexema, "Digito", fila, columna, i - lexema.Length);
                            lexema = "";
                            i--;
                            columna--;
                            estado = 0;
                        }
                        break;
                    case 3:
                        if (Char.IsDigit(c))
                        {
                            lexema += c;
                            estado = 2;
                        }
                        else
                        {
                            estado = -99;
                            i = i - 2;
                            columna = columna - 2;
                            lexema = "";
                        }
                        break;
                    case 4:
                        if (c == '"')
                        {
                            lexema += c;
                            estado = 5;
                        }
                        break;
                    case 5:
                        if (c != '"')
                        {
                            lexema += c;
                            estado = 5;
                        }
                        else
                        {
                            estado = 6;
                            i--;
                            columna--;
                        }
                        break;
                    case 6:
                        if (c == '"')
                        {
                            lexema += c;
                            //token = new Token(2, "Cadena", lexema, fila, columna);
                            //tokens.add(token);
                            addToken(lexema, "Cadena", fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                        }
                        else if (c == ',')
                        {
                            lexema += c;
                            //token = new Token(4, "Coma", lexema, fila, columna);
                            //tokens.add(token);
                            addToken(lexema, "Coma", fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                        }

                        break;

                    /**inicio nuevo**/
                    case 8:
                        if (Char.IsDigit(c))
                        {
                            estado = 9;
                            lexema += c;
                        }
                        else
                        {
                            //retorno += "Se esperaba un digito[" + caracter + "]" + Environment.NewLine;
                            addError(lexema, "Se esperaba un digito [" + lexema + "]", fila, columna);
                            estado = 0;
                            lexema = "";
                        }
                        break;
                    case 9:
                        if (Char.IsDigit(c))
                        {
                            estado = 9;
                            lexema += c;
                        }
                        else
                        {
                            //addToken(lexema, "decimal", pos + 1, 0);
                            //estado = 0;
                            //lexema = "";
                            //pos -= 1;
                            addToken(lexema, "Digito", fila, columna, i - lexema.Length);
                            lexema = "";
                            i--;
                            columna--;
                            estado = 0;
                        }

                        break;
                    /*fin nuevo*/

                    case -99:
                        lexema += c;


                        addError(lexema, "Carácter Desconocido", fila, columna);

                        estado = 0;
                        lexema = "";
                        break;
                }
            }


        }

        public Boolean Macht_enReser(String sente)
        {
            Boolean enco = false;
            for (int i = 0; i < tokens.Count; ++i)
            {
                //MessageBox.Show(tokens[i].ToString(), sente);
                //(reservadas[i].Lexema.Equals(lexema)) a = reservadas[i].Id;
                if (sente.ToString() == tokens[i].ToString())
                {
                    enco = true;
                    estado_token = i;
                    return enco;
                }
                else { enco = false; }

            }
            return enco;
        }
       
        
    


        public void generarLista()
        {
            for (int i = 0; i < listaTokens.Count; i++)
            {
                Token actual = listaTokens.ElementAt(i);
                retorno += "[Lexema:" + actual.getLexema() + ",IdToken: " + actual.getIdToken() + ",Linea: " + actual.getLinea() + "]" + Environment.NewLine;
            }
        }
        public String getRetorno()
        {
            return this.retorno;
        }

        //Aqui retorna el listado que se ha guardado los tokens obtenidos
        public List<Token> getListaTokens()
        {
           return listaTokens;
        }


    }
}
