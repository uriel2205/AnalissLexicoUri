using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalissLexicoUri
{
    class Rutas
    {

        private string path;
        private String Nombre;

        public Rutas(string path, String Nombre)
        {

            this.path = path;
            this.Nombre = Nombre;
        }
        public string getPath()
        {
            return this.path;
        }
        public String getNombre()
        {
            return this.Nombre;
        }
      
    }
}
