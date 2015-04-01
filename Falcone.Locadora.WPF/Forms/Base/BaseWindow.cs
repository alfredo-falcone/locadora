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
using System.Text.RegularExpressions;

namespace Falcone.Locadora.WPF.Forms.Base
{
  /// <summary>
  /// Interaction logic for BaseWindow.xaml
  /// </summary>
  public partial class BaseWindow : Window
  {
    DbEntities banco = null;

    public BaseWindow()
    {
      var uriSource = new Uri(@"pack://application:,,/Images/pictographs-car_rental_inv_t.png", UriKind.RelativeOrAbsolute);
      this.Icon = new BitmapImage(uriSource);
      this.ShowInTaskbar = false;
      this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
      this.Loaded += (sender, e) =>
      {
        MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
      };

    }

    protected DbEntities Banco
    {
      get
      {
        if (banco == null)
        {
          banco = new DbEntities();
        }
        return banco;
      }
    }

    protected void ResetBanco()
    {
      banco = null;
    }

    public bool? ShowDialog(Window owner)
    {
      this.Owner = owner;
      return this.ShowDialog();
    }

    /*private bool IsNumerico(string texto)
    {
      Regex regex = new Regex("[^0-9]+");
      return !regex.IsMatch(texto);
    }
    protected void ValidacaoTextBoxNumerico(object sender, TextCompositionEventArgs e)
    {
      e.Handled = !IsNumerico(e.Text);
    }

    protected void ValidacaoTextBoxTelefone(object sender, TextCompositionEventArgs e)
    {
      e.Handled = true;
      if (String.IsNullOrEmpty(e.Text))
        e.Handled = false;
      else
      {
        if (IsNumerico(e.Text))
        {
          string textoSemFormatacao = ((TextBox)sender).Text.Replace("(", string.Empty).Replace(")", string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty) + e.Text;
          StringBuilder sbFormatador = new StringBuilder();
          if (textoSemFormatacao.Length >= 2)
          {
            sbFormatador.AppendFormat("({0}) ", textoSemFormatacao.Substring(0, 2));
            if (textoSemFormatacao.Length >= 6)
              sbFormatador.AppendFormat("{0}-{1}", textoSemFormatacao.Substring(2, 4), textoSemFormatacao.Substring(6));
            else
              sbFormatador.Append(textoSemFormatacao.Substring(2));
          }
          else
            sbFormatador.Append(textoSemFormatacao);

          ((TextBox)sender).Text = sbFormatador.ToString();
          ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
          ((TextBox)sender).SelectionLength = 0;
          //((TextBox)sender).
          e.Handled = true;

        }
      }
    }*/

  }
}
