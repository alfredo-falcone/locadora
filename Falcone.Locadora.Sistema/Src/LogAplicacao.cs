using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Falcone.Locadora.Sistema.Src
{
  public static class LogAplicacao
  {
    public static void RegistrarAtividade(string textoAtividade)
    {
      //StringBuilder builder = new StringBuilder();
      File.AppendAllText("activities.log", string.Format("{0} - Usuário: {1} => {2}{3}", DateTime.Now, Aplicacao.Instancia.Usuario.Login, textoAtividade, Environment.NewLine));
    }
  }
}
