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
using Falcone.Locadora.Sistema.Data;
using Falcone.Locadora.Sistema.Src;
using System.Text.RegularExpressions;
using Falcone.Locadora.Sistema.Src.Extension;


namespace Falcone.Locadora.WPF.Forms
{
  /// <summary>
  /// Interaction logic for AluguelManutencao.xaml
  /// </summary>
  public partial class AluguelManutencao : Falcone.Locadora.WPF.Forms.Base.BaseWindow
  {
    private Aluguel _aluguel = null;
    private Carro _carro = null;
    private bool isNovoAluguel = false;
    
    public AluguelManutencao()
    {
      InitializeComponent();
      this.tbQuilometragemAluguel.IsEnabled = false;
    }

    public AluguelManutencao(Aluguel aluguel) : this()
    {
      this.Aluguel = this.Banco.Aluguels.Where(a => a.Id == aluguel.Id).Single() ;
      this.dpDataAluguel.IsEnabled = false;
      this.dpDataDevolucao.SelectedDate = DateTime.Now;
    }

    public AluguelManutencao(Carro carro) : this()
    {
      this.Carro = this.Banco.Carros.Where(c => c.Id == carro.Id).Single();
      this.dpDataDevolucao.IsEnabled = false;
      this.tbQuilometragemDevolucao.IsEnabled = false;
    }

    private Carro Carro
    {
      get
      {
        if (this._aluguel != null)
        {
          this._carro = this._aluguel.Carro;
        }
        return this._carro;
      }
      set
      {
        this._carro = value;
      }

    }
    private Aluguel Aluguel
    {
      get
      {
        if (this._aluguel == null)
        {
          this._aluguel = new Aluguel() { Carro = this.Carro, DataAluguel = DateTime.Now, DataDevolucao = Constantes.DATA_MINIMA, QuilometragemInicial = this.Carro.Quilometragem };
          this.isNovoAluguel = true;
        }
        return this._aluguel;
      }
      set
      {
        this._aluguel = value;
      }
    }

    private void rbDocumento_Checked(object sender, RoutedEventArgs e)
    {
      tbDocumento.IsEnabled = rbDocumento.IsChecked != null && rbDocumento.IsChecked.Value;
      cmbClientes.IsEnabled = !(tbDocumento.IsEnabled);
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      cmbClientes.DisplayMemberPath = "Nome";
      cmbClientes.SelectedValuePath = "CPF";
      if (this.Aluguel.Cliente == null)
      {

        var clientes = this.Banco.Clientes.ToList();
        cmbClientes.ItemsSource = clientes;
      }
      else
      {
        var clientes = new List<Cliente>();
        clientes.Add(this.Aluguel.Cliente);
        cmbClientes.ItemsSource = clientes;
        cmbClientes.SelectedItem = this.Aluguel.Cliente;
        tbDocumento.Text = this.Aluguel.Cliente.CPF;
        rbNome.IsChecked = true;
        rbNome.IsEnabled = false;
        rbDocumento.IsEnabled = false;
        tbDocumento.IsEnabled = false;
        cmbClientes.IsEnabled = false;
      }
      tbPlaca.Text = this.Carro.Placa;
      tbModelo.Text = this.Carro.Modelo;
      tbAno.Text = this.Carro.Ano.ToString();
      tbQuilometragem.Text = this.Carro.Quilometragem.ToString();
      dpDataAluguel.SelectedDate = this.Aluguel.DataAluguel;
      if (this.Aluguel.DataDevolucao != Constantes.DATA_MINIMA)
        dpDataDevolucao.SelectedDate = this.Aluguel.DataDevolucao;
      else
        dpDataDevolucao.SelectedDate = null;

      tbQuilometragemAluguel.Text = this.Aluguel.QuilometragemInicial.ToString();
      if (this.Aluguel.QuilometragemFinal != default(int))
        tbQuilometragemDevolucao.Text = this.Aluguel.QuilometragemFinal.ToString();

      
    }

    private void ValidarTela()
    {
      StringBuilder sbErros = new StringBuilder();
      if (dpDataAluguel.SelectedDate == null)
        sbErros.AppendLine("=> Data de aluguel deve ser preenchida");
      if(string.IsNullOrEmpty(tbQuilometragemAluguel.Text))
        sbErros.AppendLine("=> Quilometragem de aluguel deve ser preenchida");
      if (cmbClientes.SelectedItem == null)
      {
        if (string.IsNullOrEmpty(tbDocumento.Text))
        {
          sbErros.AppendLine("=> Cliente deve ser preenchido");
        }
        else
        {
          sbErros.AppendLine("=> Cliente com documento informado não foi encontrado. Cadastre o cliente e tente novamente.");
        }
      }

    
      if (!this.isNovoAluguel)
      {
        if (dpDataDevolucao.SelectedDate == null)
          sbErros.AppendLine("=> Data de devolução deve ser preenchida");
        if (string.IsNullOrEmpty(tbQuilometragemDevolucao.Text))
          sbErros.AppendLine("=> Quilometragem de devolução deve ser preenchida");
      
        if (int.Parse(this.tbQuilometragemAluguel.Text) > int.Parse(this.tbQuilometragemDevolucao.Text))
        sbErros.AppendLine("Quilometragem final deve ser superior à inicial");

      }
      
      
      if (sbErros.Length > 0)
      {
        throw new ArgumentException("Erro validando dados" + Environment.NewLine + sbErros.ToString());
      }
    }


    private void AtualizarAluguelTela()
    {
      this.Aluguel.Cliente = cmbClientes.SelectedItem as Cliente;
      this.Aluguel.DataAluguel = dpDataAluguel.SelectedDate.Value;
      this.Aluguel.QuilometragemInicial = int.Parse(tbQuilometragemAluguel.Text);
      if (dpDataDevolucao.SelectedDate != null)
        this.Aluguel.DataDevolucao = dpDataDevolucao.SelectedDate.Value;
      if (!string.IsNullOrEmpty(this.tbQuilometragemDevolucao.Text))
      {
        this.Aluguel.QuilometragemFinal = int.Parse(tbQuilometragemDevolucao.Text);
        this.Carro.Quilometragem = this.Aluguel.QuilometragemFinal;
      }
    }

    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
      Regex regex = new Regex("[^0-9]+");
      e.Handled = regex.IsMatch(e.Text);
    }

    private void btGravar_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        ValidarTela();
        AtualizarAluguelTela();
        
        if (this.isNovoAluguel)
        {
          this.Banco.Aluguels.Add(this.Aluguel);
        }
        //else
       //{
        //  Aluguel aluguelBanco = this.Banco.Aluguels.Where(a => a.Id == this.Aluguel.Id).Single();
        //  aluguelBanco.CopiarPropriedades(this.Aluguel);
       // }
        LogAplicacao.RegistrarAtividade(
          string.Format("Operação: {0}; Placa: {1}; Quilometragem inicial:{2}{3}",
          ((this.isNovoAluguel) ? "Aluguel" : "Devolução"),
          this.Carro.Placa, this.Aluguel.QuilometragemInicial,
          ((this.isNovoAluguel) ? string.Empty : string.Format("; Quilometragem final: {0}", this.Aluguel.QuilometragemFinal))));

        this.Banco.SaveChanges();
        this.Close();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private void tbDocumento_TextChanged(object sender, TextChangedEventArgs e)
    {
      Cliente clienteDocumento = cmbClientes.Items.Cast<Cliente>().Where(c => c.CPF.ToUpper() == tbDocumento.Text.ToUpper()).SingleOrDefault();
      cmbClientes.SelectedItem = clienteDocumento;
    }

    
    
  }
}
