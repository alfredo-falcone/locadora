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
using Falcone.Locadora.Sistema.Src.Extension;
using System.Windows.Controls.Primitives;
using Falcone.Locadora.Sistema.Src;

namespace Falcone.Locadora.WPF.Forms
{
  /// <summary>
  /// Interaction logic for CadastroVeiculo.xaml
  /// </summary>
  public partial class CadastroCliente : Falcone.Locadora.WPF.Forms.Base.BaseWindow
  {
    
    private bool temAlteracoesPendentes = false;
    public CadastroCliente()
    {
      InitializeComponent();
      Load();
    }

    private void Load()
    {
      this.ResetBanco();
      var query = this.Banco.Clientes.ToList();
      /*if (rbDisponiveis != null && rbDisponiveis.IsChecked != null && rbDisponiveis.IsChecked.Value)
      {
        query = query.Where(c => c.Status == StatusCarro.Disponivel).ToList();
      }
      else if (rbAlugados != null && rbAlugados.IsChecked != null && rbAlugados.IsChecked.Value)
      {
        query = query.Where(c => c.Status == StatusCarro.Alugado).ToList();
      }*/

      var clientes = query;//.ToList();

      dgClientes.DataContext = clientes;
      btGravar.IsEnabled = temAlteracoesPendentes = false;
      
    }

   
    private void btGravar_Click(object sender, RoutedEventArgs e)
    {
      this.Banco.SaveChanges();
      btGravar.IsEnabled = temAlteracoesPendentes = false;
    }

   
    private void btAcao_Click(object sender, RoutedEventArgs e)
    {
      if (!temAlteracoesPendentes || MessageBox.Show("Deseja descartar as alterações pendentes?", "Perder alterações", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
      {

        Cliente clienteSelecionado = dgClientes.SelectedItem as Cliente;
        if (clienteSelecionado == null)
        {
          MessageBox.Show("Ação não permitida. Acione para um cliente já gravado.");
        }
        else
        {
          ClienteManutencao frmManutencao = new ClienteManutencao(clienteSelecionado);
          frmManutencao.ShowDialog(this);
          //MessageBox.Show("Edição de cliente " + clienteSelecionado.Nome);

          Load();
        }
      }


    }


    private void rbTodos_Checked(object sender, RoutedEventArgs e)
    {
      Load();
    }

    private void btNovoCliente_Click(object sender, RoutedEventArgs e)
    {
      ClienteManutencao frmManutencao = new ClienteManutencao();
      frmManutencao.ShowDialog(this);
      //MessageBox.Show("Edição de cliente " + clienteSelecionado.Nome);

      Load();
    }

    
  }
}
