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

namespace Falcone.Locadora.WPF.Forms
{
  /// <summary>
  /// Interaction logic for ClienteManutencao.xaml
  /// </summary>
  public partial class ClienteManutencao : Falcone.Locadora.WPF.Forms.Base.BaseWindow
  {
    private Cliente _cliente;

    private bool isNovoCliente = false;
    public ClienteManutencao()
    {
      InitializeComponent();
    }

    public ClienteManutencao(Cliente cliente) : this()
    {
      if (cliente != null)
        this.Cliente = this.Banco.Clientes.Where(a => a.Id == cliente.Id).Single();
    }
    
    private Cliente Cliente
    {
      get
      {
        if (this._cliente == null)
        {
          this._cliente = new Cliente() { Endereco = new Endereco() };
          isNovoCliente = true;
        }
        return this._cliente;
      }
      set
      {
        this._cliente = value;
      }
    }

    private void BaseWindow_Loaded(object sender, RoutedEventArgs e)
    {
      this.DataContext = this.Cliente;
    }

    private void btGravar_Click(object sender, RoutedEventArgs e)
    {

      try
      {
        //ValidarTela();
        //AtualizarAluguelTela();

        if (this.isNovoCliente)
        {
          this.Banco.Clientes.Add(this.Cliente);
        }
        
        this.Banco.SaveChanges();
        this.Close();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }
  }
}
