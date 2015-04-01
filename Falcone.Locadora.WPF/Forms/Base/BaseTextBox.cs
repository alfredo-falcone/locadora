using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
namespace Falcone.Locadora.WPF.Forms.Base
{
  public enum TipoConteudoTextBox
  {
    Digitos,
    CEP,
    Telefone,
    Nenhum,
    Placa,
  }
  public class BaseTextBox : TextBox
  {
    public TipoConteudoTextBox TipoConteudo
    {
      get { return GetTipoConteudo(base.GetValue(TipoConteudoProperty)) ; }
      set { base.SetValue(TipoConteudoProperty, value); }
    }

    public static readonly DependencyProperty TipoConteudoProperty =
      DependencyProperty.Register("TipoConteudo", typeof(TipoConteudoTextBox), typeof(BaseTextBox));


    private TipoConteudoTextBox GetTipoConteudo(object valor)
    {
      if (valor != null && valor is TipoConteudoTextBox)
      {
        return (TipoConteudoTextBox)valor;
      }
      else
        return TipoConteudoTextBox.Nenhum;
    }

    protected override void OnPreviewTextInput(TextCompositionEventArgs e)
    {
      base.OnPreviewTextInput(e);
      switch (this.TipoConteudo)
      {
        case TipoConteudoTextBox.Digitos:
          ValidacaoTextBoxNumerico(e);
          break;
        case TipoConteudoTextBox.Telefone:
          ValidacaoTextBoxTelefone(e);
          break;
        case TipoConteudoTextBox.Placa:
          ValidacaoTextBoxPlaca(e);
          break;
        case TipoConteudoTextBox.CEP:
          ValidacaoTextBoxCEP(e);
          break;
      }
    }



    private bool IsNumerico(string texto)
    {
      Regex regex = new Regex("[^0-9]+");
      return !regex.IsMatch(texto);
    }
    private bool IsLetra(string texto)
    {
      Regex regex = new Regex("[a-zA-Z]");
      return regex.IsMatch(texto);
    }
    
    protected void ValidacaoTextBoxNumerico(TextCompositionEventArgs e)
    {
      e.Handled = !IsNumerico(e.Text);
    }

    protected void ValidacaoTextBoxTelefone(TextCompositionEventArgs e)
    {
      e.Handled = true;
      if (String.IsNullOrEmpty(e.Text))
        e.Handled = false;
      else
      {
        if (IsNumerico(e.Text))
        {
          string textoSemFormatacao = this.Text.Replace("(", string.Empty).Replace(")", string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty) + e.Text;
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

          this.Text = sbFormatador.ToString();
          this.SelectionStart = this.Text.Length;
          this.SelectionLength = 0;
          
          e.Handled = true;

        }
      }
    }

    protected void ValidacaoTextBoxPlaca(TextCompositionEventArgs e)
    {
      e.Handled = true;
      if (String.IsNullOrEmpty(e.Text))
        e.Handled = false;
      else
      {
        if (this.Text.Length < 3 && IsLetra(e.Text))
        {
          this.Text += e.Text.ToUpper() + ((this.Text.Length == 2) ? "-" : string.Empty);
          e.Handled = true;
        }
        else if (this.Text.Length >= 3 && this.Text.Length < 8 && IsNumerico(e.Text))
        {
          string textoSemFormatacao = this.Text.Replace("-", string.Empty) + e.Text;
          StringBuilder sbFormatador = new StringBuilder();
          if (textoSemFormatacao.Length >= 3)
          {
            sbFormatador.AppendFormat("{0}-{1}", textoSemFormatacao.Substring(0, 3), textoSemFormatacao.Substring(3));
          }
          else
            sbFormatador.Append(textoSemFormatacao);

          this.Text = sbFormatador.ToString();
          e.Handled = true;
        }
        this.SelectionStart = this.Text.Length;
        this.SelectionLength = 0;
          


      }
    }

    protected void ValidacaoTextBoxCEP(TextCompositionEventArgs e)
    {
      e.Handled = true;
      if (String.IsNullOrEmpty(e.Text))
        e.Handled = false;
      else
      {
        if (this.Text.Length < 9 && IsNumerico(e.Text))
        {
          string textoSemFormatacao = this.Text.Replace("-", string.Empty) + e.Text;
          StringBuilder sbFormatador = new StringBuilder();
          if (textoSemFormatacao.Length >= 5)
          {
            sbFormatador.AppendFormat("{0}-{1}", textoSemFormatacao.Substring(0, 5), textoSemFormatacao.Substring(5));
          }
          else
            sbFormatador.Append(textoSemFormatacao);

          this.Text = sbFormatador.ToString();
          this.SelectionStart = this.Text.Length;
          this.SelectionLength = 0;
          
          e.Handled = true;

        }

      }
    }
  }
}
