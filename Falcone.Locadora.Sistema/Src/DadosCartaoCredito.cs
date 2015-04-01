using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Falcone.Locadora.Sistema.Src;

namespace Falcone.Locadora.Sistema.Data
{
  public partial class DadosCartaoCredito
  {
    public string NumeroCartaoDescriptografado
    {
      get
      {
        string retorno = null;
        if (!string.IsNullOrEmpty(this.NumeroCartao))
        {
          retorno = Util.Descriptografar(this.NumeroCartao);
        }
        return retorno;
      }
      set
      {
        string valorSet = null;
        if (!string.IsNullOrEmpty(value))
          valorSet = Util.Criptografar(value);

        this.NumeroCartao = valorSet;
      }
    }
  }
}
