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

namespace Falcone.Locadora.WPF.Forms
{
  /// <summary>
  /// Interaction logic for CadastroVeiculo.xaml
  /// </summary>
  public partial class CadastroVeiculo : Window
  {
    DbEntities banco;

    private bool temAlteracoesPendentes = false;
    public CadastroVeiculo()
    {
      InitializeComponent();
      Load();
    }

    private void Load()
    {
      banco = new DbEntities();
      var query = banco.Carros.ToList();
      if (rbDisponiveis != null && rbDisponiveis.IsChecked != null && rbDisponiveis.IsChecked.Value)
      {
        query = query.Where(c => c.Status == StatusCarro.Disponivel).ToList();
      }
      else if (rbAlugados != null && rbAlugados.IsChecked != null && rbAlugados.IsChecked.Value)
      {
        query = query.Where(c => c.Status == StatusCarro.Alugado).ToList();
      }

      var carros = query;//.ToList();
      //carros.Add(new Carro() { Placa = "KKKKKKK", Ano = 2010, Modelo = "Siena", Quilometragem = 30000 });
      dgVeiculos.DataContext = carros;
      dgVeiculos.UpdateLayout();
      btGravar.IsEnabled = temAlteracoesPendentes = false;
      
    }

    private void dgVeiculos_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void btGravar_Click(object sender, RoutedEventArgs e)
    {
      banco.SaveChanges();
      btGravar.IsEnabled = temAlteracoesPendentes = false;
    }

    private void dgVeiculos_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
    {
      var veiculoEditado = e.Row.DataContext as Carro;
      var carroBanco = banco.Carros.Where(c => c.Id == veiculoEditado.Id).SingleOrDefault();
      if (carroBanco == null)
        banco.Carros.Add(veiculoEditado);
      else
      {
        carroBanco.CopiarPropriedades(veiculoEditado);
        /*carroBanco.Ano = veiculoEditado.Ano;
        carroBanco.Modelo = veiculoEditado.Modelo;
        carroBanco.Quilometragem = veiculoEditado.Quilometragem;*/
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
          //MessageBox.Show("Carro: " + carroSelecionado.Placa);
          if (carroSelecionado.Status == StatusCarro.Alugado)
          {
            DbEntities banco = new DbEntities();
            Carro carroBanco = banco.Carros.Where(c => c.Id == carroSelecionado.Id).Single();
            Aluguel aluguel = carroBanco.AluguelPendente;
            if (aluguel != null)
            {
              aluguel.DataDevolucao = DateTime.Now;
              aluguel.QuilometragemFinal = aluguel.QuilometragemInicial + 1000;
              aluguel.Carro.Quilometragem = aluguel.QuilometragemFinal;
              banco.SaveChanges();
            }
            //carroSelecionado.CopiarPropriedades(carroBanco);
          }
          else if (carroSelecionado.Status == StatusCarro.Disponivel)
          {
            /*DbEntities banco = new DbEntities();
            Carro carroBanco = banco.Carros.Where(c => c.Id == carroSelecionado.Id).Single();
            Aluguel aluguel = new Aluguel()
            {
              Carro = carroBanco,
              Cliente_Id = -1,
              DataAluguel = DateTime.Now,
              QuilometragemInicial = carroBanco.Quilometragem,
              DataDevolucao = default(DateTime),
              QuilometragemFinal = default(int)
            };
            //carroBanco.Alugueis.Add(aluguel);
            banco.Aluguels.Add(aluguel);
            banco.SaveChanges();
            //carroSelecionado.CopiarPropriedades(carroBanco);*/
            AluguelManutencao frmManutencao = new AluguelManutencao(carroSelecionado);
            frmManutencao.ShowDialog();
          }
          else
            MessageBox.Show("Grave os dados antes desta operação");
          //dgVeiculos.UpdateLayout();
          Load();
        }
      }


    }

  
    
    private void dgVeiculos_LoadingRow(object sender, DataGridRowEventArgs e)
    {
      e.Row.Loaded += new RoutedEventHandler(Row_Loaded);

      
      /*Button botao = dgVeiculos;
      Carro carroAtual = e.Row.DataContext as Carro;
      if (carroAtual != null && botao != null && carroAtual.Status == StatusCarro.NaoCadastrado)
      {
        botao.Visibility = System.Windows.Visibility.Hidden;
      }*/
      

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
