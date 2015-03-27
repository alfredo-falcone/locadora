using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Falcone.Locadora.Sistema.Src;

namespace Falcone.Locadora.Sistema.Data
{
  public partial class Carro
  {
    public StatusCarro Status 
    {
      get
      {
        StatusCarro retorno = StatusCarro.Disponivel;
        if (this.Id == default(int))
        {
          retorno = StatusCarro.NaoCadastrado;
        }
        if (this.Alugueis.Where(a => a.DataDevolucao == Constantes.DATA_MINIMA).Count() > 0)
        {
          retorno = StatusCarro.Alugado;
        }
        return retorno;

      }
      
    }

    public string NomeAcaoPermitida
    {
      get
      {
        string retorno = null;
        if(this.Status == StatusCarro.Alugado)
          retorno = "Registrar Devolução";
        else if(this.Status == StatusCarro.Disponivel)
          retorno = "Alugar Carro";
        return retorno;
      }
    }

    public Aluguel AluguelPendente
    {
      get
      {
        return this.Alugueis.Where(a => a.DataDevolucao == Constantes.DATA_MINIMA).SingleOrDefault();
      }
    }

    
  }
}
