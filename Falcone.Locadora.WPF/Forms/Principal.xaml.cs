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

namespace Falcone.Locadora.WPF.Forms
{
  /// <summary>
  /// Interaction logic for Principal.xaml
  /// </summary>
  public partial class Principal : Falcone.Locadora.WPF.Forms.Base.BaseWindow
  {
    public Principal()
    {
      InitializeComponent();
      this.ShowInTaskbar = true;
      
    }

    private void MenuVeiculo_Click(object sender, RoutedEventArgs e)
    {
      CadastroVeiculo cadastro = new CadastroVeiculo();
      cadastro.ShowDialog(this);
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
