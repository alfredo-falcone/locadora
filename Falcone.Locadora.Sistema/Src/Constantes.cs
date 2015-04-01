using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Falcone.Locadora.Sistema.Src
{
  public static class Constantes
  {
    public static readonly DateTime DATA_MINIMA = new DateTime(1753, 1, 1);
    public static class Criptografia
    {
      public const string Chave = "FALCONE2015C#PORTODIGITAL";
      public static readonly byte[] Salt = new byte[] { 0x21, 0x3e, 0x77, 0x9a, 0x20, 0x1c, 0x11, 0x44, 0x7b, 0x25, 0x68, 0xa1, 0xcc };
    }
  }
}
