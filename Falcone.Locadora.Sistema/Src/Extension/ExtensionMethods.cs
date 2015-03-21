using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Falcone.Locadora.Sistema.Src.Extension
{
  public static class ExtensionMethods
  {
    public static void CopiarPropriedades<T>(this T destino, T objetoOrigem)  where T:class
    {
      foreach(var propriedade in objetoOrigem.GetType().GetProperties())
      {
        if(propriedade.Name != "Id" && propriedade.CanWrite)
        {
          propriedade.SetValue(destino, propriedade.GetValue(objetoOrigem, null), null);
        }
      }
    }
  }
}
