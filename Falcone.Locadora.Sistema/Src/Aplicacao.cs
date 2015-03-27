using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Falcone.Locadora.Sistema.Data;

namespace Falcone.Locadora.Sistema.Src
{
  public class Aplicacao
  {
    private static Aplicacao instancia;

    public Usuario Usuario { get; set; }

    /// <summary>
    /// Instância da aplicação
    /// </summary>
    public static Aplicacao Instancia
    {
      get
      {
        return instancia;
      }
    }


    

    public static void Criar(Usuario usuario)
    {
      if (instancia == null)
        instancia = new Aplicacao() { Usuario = usuario };
      else
        throw new InvalidOperationException("Aplicação já instanciada");
     
    }
  }
}
