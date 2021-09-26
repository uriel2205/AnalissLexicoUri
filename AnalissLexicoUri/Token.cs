using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalissLexicoUri
{
    //Se crea una clase para guardar una lista dinamica con datos recibidos por el lexema
    class Token
    {

        private String lexema;
        private String idToken;
        private int linea;
        private int columna;
        private int indice;
        public Token(String lexema, String idToken, int linea, int columna, int indice)
        {

            this.lexema = lexema;
            this.idToken = idToken;
            this.linea = linea;
            this.columna = columna;
            this.indice = indice;
        }

        public int getIndice()
        {
            return this.indice;
        }
        public String getLexema()
        {
            return this.lexema;
        }
        public String getIdToken()
        {
            return this.idToken;
        }
        public int getLinea()
        {
            return this.linea;
        }
        public int getColumna()
        {
            return this.columna;
        }
    }
}
