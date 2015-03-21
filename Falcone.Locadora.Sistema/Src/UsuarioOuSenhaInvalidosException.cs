using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Falcone.Locadora.Sistema.Src
{
  public class UsuarioOuSenhaInvalidosException : Exception
  {
    public UsuarioOuSenhaInvalidosException() : base("Usuário ou senha inválidos")
    {

    }
  }
}
