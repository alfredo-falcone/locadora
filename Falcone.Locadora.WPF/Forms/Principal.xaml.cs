using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using Falcone.Locadora.Sistema.Src;

namespace Falcone.Locadora.WPF.Forms
{
  /// <summary>
  /// Interaction logic for Principal.xaml
  /// </summary>
  public partial class Principal : Falcone.Locadora.WPF.Forms.Base.BaseWindow
  {
    private System.Windows.Threading.DispatcherTimer timerCotacaoMoeda = null;
    private bool timerInicializado = false;
    public Principal()
    {
      InitializeComponent();
      this.ShowInTaskbar = true;
      timerCotacaoMoeda = new System.Windows.Threading.DispatcherTimer();
      timerCotacaoMoeda.Tick += timerCotacaoMoeda_Tick;
      timerCotacaoMoeda.Interval = new TimeSpan(0, 0, 1);
      
      timerCotacaoMoeda.Start();
    }

    private void MenuVeiculo_Click(object sender, RoutedEventArgs e)
    {
      CadastroVeiculo cadastro = new CadastroVeiculo();
      cadastro.ShowDialog(this);
    }

    private void timerCotacaoMoeda_Tick(object sender, EventArgs e)
    {
      timerCotacaoMoeda.Stop();
      /*Task tarefa = new Task(() => 
      {
        WebRequest requisicao = WebRequest.Create("http://developers.agenciaideias.com.br/cotacoes/json");
        WebResponse resposta = requisicao.GetResponse();
        byte[] dados = new byte[resposta.GetResponseStream().Length];

        resposta.GetResponseStream().Read(dados, 0, (int)resposta.GetResponseStream().Length);
        string retorno = Falcone.Locadora.Sistema.Src.Util.ConverterByteArrayEmString(dados);
        lbCotacao.Content = retorno;
      });
      tarefa.Start();*/
      if (!timerInicializado)
      {
        timerCotacaoMoeda.Interval = new TimeSpan(0, 0, 300);
        timerInicializado = true;
      }
      try
      {
        CarregarCotacao();
      }
      finally
      {
        timerCotacaoMoeda.Start();
      }
    }

    private void CarregarCotacao()
    {
      HttpWebRequest requisicao = WebRequest.Create("http://developers.agenciaideias.com.br/cotacoes/xml") as HttpWebRequest;
      HttpWebResponse resposta = requisicao.GetResponse() as HttpWebResponse;
      //byte[] dados = new byte[200];
      StringBuilder sbCotacoes = new StringBuilder();
      sbCotacoes.AppendLine("Cotações:");
      using (StreamReader reader = new StreamReader(resposta.GetResponseStream()))
      {
        XElement elemento = XElement.Parse(reader.ReadToEnd());
        var bovespa = elemento.Elements().Where(el => el.Name.LocalName == "bovespa").SingleOrDefault();
        var valorDolar = elemento.Elements().Where(el => el.Name.LocalName == "dolar").SingleOrDefault().Elements().Where(el2 => el2.Name.LocalName == "cotacao").SingleOrDefault().Value;
        var valorEuro = elemento.Elements().Where(el => el.Name.LocalName == "euro").SingleOrDefault().Elements().Where(el2 => el2.Name.LocalName == "cotacao").SingleOrDefault().Value;
        sbCotacoes.AppendFormat("Dolar: {0}{1}", valorDolar, Environment.NewLine);
        sbCotacoes.AppendFormat("Euro: {0}{1}", valorEuro, Environment.NewLine);
        sbCotacoes.AppendFormat("Data consulta: {0}{1}", DateTime.Now, Environment.NewLine);

      }



      //string retorno = Falcone.Locadora.Sistema.Src.Util.ConverterByteArrayEmString(dados);
      //string retorno = System.Text.Encoding.UTF8.GetString(dados);
      lbCotacao.Content = sbCotacoes.ToString();
    }


    /*private void frmPrincipal_Activated(object sender, EventArgs e)
    {

      foreach (Window janela in Application.Current.Windows)
      {
        if (janela.IsFocused)
          janela.BringIntoView();
      }
    }

    private void frmPrincipal_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
    {
      
    }*/

  }
}
