﻿using System;
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
  public partial class CadastroVeiculo : Falcone.Locadora.WPF.Forms.Base.BaseWindow
  {
    
    private bool temAlteracoesPendentes = false;
    public CadastroVeiculo()
    {
      InitializeComponent();
      Load();
    }

    private void Load()
    {
      this.ResetBanco();
      var query = this.Banco.Carros.ToList();
      if (rbDisponiveis != null && rbDisponiveis.IsChecked != null && rbDisponiveis.IsChecked.Value)
      {
        query = query.Where(c => c.Status == StatusCarro.Disponivel).ToList();
      }
      else if (rbAlugados != null && rbAlugados.IsChecked != null && rbAlugados.IsChecked.Value)
      {
        query = query.Where(c => c.Status == StatusCarro.Alugado).ToList();
      }

      var carros = query;//.ToList();
     
      dgVeiculos.DataContext = carros;
      //dgVeiculos.UpdateLayout();
      btGravar.IsEnabled = temAlteracoesPendentes = false;
      string textoOriginal = "tEsTeFalconeção";
      string textoCriptografado = Util.Criptografar(textoOriginal);
      string textoDescriptografado = Util.Descriptografar(textoCriptografado);
      if (textoDescriptografado != textoOriginal)
      {
        throw new ArgumentException("Caracteres inapropriados na senha");
      }
      //DadosCartaoCredito cartaoIncluir = new DadosCartaoCredito(){ ClienteId=-1, NumeroCartao
      // DadosCartaoCredito cartao = Banco.DadosCartaoCreditoes.Where(c => c.ClienteId == -1).FirstOrDefault();

      
    }

    private void dgVeiculos_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void btGravar_Click(object sender, RoutedEventArgs e)
    {
      this.Banco.SaveChanges();
      btGravar.IsEnabled = temAlteracoesPendentes = false;
    }

    private void dgVeiculos_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
    {
      var veiculoEditado = e.Row.DataContext as Carro;
      var carroBanco = this.Banco.Carros.Where(c => c.Id == veiculoEditado.Id).SingleOrDefault();
      if (carroBanco == null)
        this.Banco.Carros.Add(veiculoEditado);
      else
      {
        carroBanco.CopiarPropriedades(veiculoEditado);
      }
      
    }

    private void btAcao_Click(object sender, RoutedEventArgs e)
    {
      if (!temAlteracoesPendentes || MessageBox.Show("Deseja descartar as alterações pendentes?", "Perder alterações", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
      {

        Carro carroSelecionado = dgVeiculos.SelectedItem as Carro;
        if (carroSelecionado == null)
        {
          MessageBox.Show("Ação não permitida. Acione para um veículo já gravado.");
        }
        else
        {
          if (carroSelecionado.Status == StatusCarro.Alugado)
          {
            Aluguel aluguel = carroSelecionado.AluguelPendente;
            if (aluguel != null)
            {
              AluguelManutencao frmAluguel = new AluguelManutencao(aluguel);
              frmAluguel.ShowDialog(this);
            }
          }
          else if (carroSelecionado.Status == StatusCarro.Disponivel)
          { 
            AluguelManutencao frmManutencao = new AluguelManutencao(carroSelecionado);
            frmManutencao.ShowDialog(this);
          }
          else
            MessageBox.Show("Grave os dados antes desta operação");
          Load();
        }
      }


    }

  
    
    private void dgVeiculos_LoadingRow(object sender, DataGridRowEventArgs e)
    {
      e.Row.Loaded += new RoutedEventHandler(Row_Loaded);

    }

    void Row_Loaded(object sender, RoutedEventArgs e)
    {
      //var botao = (dgVeiculos.Columns[5].GetCellContent(sender as DataGridRow) as ContentPresenter)
    }

    private void dgVeiculos_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
    {
      btGravar.IsEnabled = temAlteracoesPendentes = true;
    }

    private void rbTodos_Checked(object sender, RoutedEventArgs e)
    {
      Load();
    }

    
  }
}
