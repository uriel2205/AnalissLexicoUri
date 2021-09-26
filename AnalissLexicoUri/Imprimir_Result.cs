using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalissLexicoUri
{
 

    public class Imprimir_Result
    {
        String expresion;
        double resultado;

        public Imprimir_Result(String expresion, double resultado)
        {

            this.expresion = expresion;
            this.resultado = resultado;

        }

        public String getExpresion()
        {
            return this.expresion;
        }

        public double getResultado()
        {
            return this.resultado;
        }

  
    }


}
