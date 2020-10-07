using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Fabricante
    {
        private string marca;
        private EPais pais;

        public Fabricante(string marca, EPais pais)
        {
            this.marca = marca;
            this.pais = pais;
        }
            
        public static implicit operator String(Fabricante f)
        {
            return f.marca + " - " + f.pais;
        }

        public static bool operator ==(Fabricante a, Fabricante b)
        {
            bool rtn = false;
            if(a.marca==b.marca&&a.pais==b.pais)
            {
                rtn = true;
            }
            return rtn;
        }

        public static bool operator !=(Fabricante a, Fabricante b)
        {
            return !(a == b);
        }


    }

    public abstract class Vehiculo
    {
        protected Fabricante fabricante;
        protected static Random generadorDeVelocidades;
        protected string modelo;
        protected float precio;
        protected int velocidadMaxima;

        static Vehiculo()
        {
            Vehiculo.generadorDeVelocidades = new Random();
        }

        public int VelocidadMaxima
        {
            get { 
                
                if(this.velocidadMaxima==0)
                {
                    this.velocidadMaxima = Vehiculo.generadorDeVelocidades.Next(100, 280);
                }
                
                return this.velocidadMaxima; }

        }

        public Vehiculo(float precio, string modelo, Fabricante fabril)
        {
            this.precio = precio;
            this.modelo = modelo;
            this.fabricante = fabril;
            this.velocidadMaxima = this.VelocidadMaxima;
        }

        public Vehiculo(string marca, EPais pais, string modelo, float precio)
            :this(precio,modelo,new Fabricante(marca,pais))
        {
        }

        private static string Mostrar(Vehiculo v)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Fabricante: {(String)v.fabricante}");
            sb.AppendLine($"Modelo: {v.modelo}");
            sb.AppendLine($"Precio: {v.precio}");
            sb.AppendLine($"Velocidad Maxima: {v.velocidadMaxima}");


            return sb.ToString();
        }


        public static explicit operator String(Vehiculo v)
        {
            return Vehiculo.Mostrar(v);
        }

        public static bool operator ==(Vehiculo a, Vehiculo b)
        {
            bool rtn = false;
            if(a.modelo==b.modelo&&a.fabricante==b.fabricante)
            {
                rtn = true;
            }
            return rtn;
        }

        public static bool operator !=(Vehiculo a, Vehiculo b)
        {
            return !(a == b);
        }



    }


    public class Auto:Vehiculo
    {
        public ETipo tipo;

        public Auto(string modelo, float precio, Fabricante fabri, ETipo tipo)
            :base(precio,modelo,fabri)
        {
            this.tipo = tipo;
        }

        public static bool operator ==(Auto a, Auto b)
        {
            bool rtn = false;
            if(a.tipo==b.tipo&&(Vehiculo)a==(Vehiculo)b)
            {
                rtn = true;
            }


            return rtn;
        }

        public static bool operator !=(Auto a, Auto b)
        {
            return !(a == b);
        }

        public static explicit operator Single(Auto a)
        {
            return a.precio;
        }

        public override bool Equals(object obj)
        {
            bool rtn = false;
            if(obj is Auto)
            {
                rtn = this == (Auto)obj;
            }

            return rtn;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine((String)((Vehiculo)this));
            sb.AppendLine($"Tipo de Auto: {this.tipo}");
            return sb.ToString();
        }



    }


    public class Moto : Vehiculo
    {
        public ECilindrada cilindrada;


        public Moto(string marca, EPais pais, string modelo, float precio, ECilindrada cilindrada)
            : base(marca,pais,modelo,precio)
        {
            this.cilindrada = cilindrada;
        }

        public static bool operator ==(Moto a, Moto b)
        {
            bool rtn = false;
            if (a.cilindrada == b.cilindrada && (Vehiculo)a == (Vehiculo)b)
            {
                rtn = true;
            }


            return rtn;
        }

        public static bool operator !=(Moto a, Moto b)
        {
            return !(a == b);
        }

        public static implicit operator Single(Moto m)
        {
            return m.precio;
        }
        public override bool Equals(object obj)
        {
            bool rtn = false;
            if (obj is Moto)
            {
                rtn = this == (Moto)obj;
            }

            return rtn;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine((String)((Vehiculo)this));
            sb.AppendLine($"Tipo de Cilindrada: {this.cilindrada}");
            return sb.ToString();
        }

    }

    public class Concesionaria
    {
        private int capacidad;
        private List<Vehiculo> vehiculos;

        private Concesionaria()
        {
            vehiculos = new List<Vehiculo>();
        }
        private Concesionaria(int capacidad):this()
        {
            this.capacidad = capacidad;
        }

        public static string Mostrar(Concesionaria c)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Capacidad de concesionario: {c.capacidad}");
            sb.AppendLine($"Total por autos: {c.PrecioDeAutos}");
            sb.AppendLine($"Total por autos: {c.PrecioDeMotos}");
            sb.AppendLine($"Total por autos: {c.PrecioTotal}");
            sb.AppendLine("Vehiculos: ");
            sb.AppendLine("------------");
            foreach(Vehiculo item in c.vehiculos)
            {
                sb.AppendLine(item.ToString());
                sb.AppendLine("_________________________");
            }
            
            return sb.ToString();
        }

        public static implicit operator Concesionaria(int capacidad)
        {
            return new Concesionaria(capacidad);
        }

        public static bool operator ==(Concesionaria c, Vehiculo v)
        {
            bool rtn = false;
            foreach(Vehiculo item in c.vehiculos)
            {
                if(v.Equals(item))
                {
                    rtn = true;
                    break;
                }
            }
            return rtn;
        }

        public static bool operator !=(Concesionaria c, Vehiculo v)
        {
            return !(c == v);
        }

        public static Concesionaria operator +(Concesionaria c, Vehiculo v)
        {
            Concesionaria cc = new Concesionaria();
            cc = c;

            if(c.vehiculos.Count()<c.capacidad&&c!=v)
            {
                cc.vehiculos.Add(v);
            }
            else
            {
                Console.WriteLine("No se ha podido agregar el vehiculo a la lista");
            }
            return cc;
        }

        private double ObtenerPrecio(EVehiculo tipoVehiculo)
        {
            double precio=0;
            foreach (Vehiculo v in this.vehiculos)
            {
                switch (tipoVehiculo)
                {
                    case EVehiculo.PrecioDeAutos:
                        if (v is Auto)
                        {
                            precio += (Single)((Auto)v);
                        }
                        break;
                    case EVehiculo.PrecioDeMotos:
                        if (v is Moto)
                        {
                            precio += (Moto)v;
                        }
                        break;
                    case EVehiculo.PrecioTotal:
                        if (v is Auto)
                        {
                            precio += (Single)((Auto)v);
                        }
                        else if(v is Moto)
                        {
                            precio += (Moto)v;
                        }
                        break;
                    default:
                        break;
                }                
            }
            return precio;
        }

        /*Las propiedades públicas PrecioDeAutos, PrecioDeMotos y PrecioTotal están asociadas al método ObtenerPrecio. 
         * Reutilizar código.*/
        public double PrecioDeAutos
        {
            get { return this.ObtenerPrecio(EVehiculo.PrecioDeAutos); }
        }
        public double PrecioDeMotos
        {
            get { return this.ObtenerPrecio(EVehiculo.PrecioDeMotos); }
        }

        public double PrecioTotal
        {
            get { return this.ObtenerPrecio(EVehiculo.PrecioTotal); }
        }


    }



}
