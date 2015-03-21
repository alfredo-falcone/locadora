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

namespace Falcone.Locadora.WPF.Forms
{
  /// <summary>
  /// Interaction logic for Principal.xaml
  /// </summary>
  public partial class Principal : Window
  {
    public Principal()
    {
      InitializeComponent();
    }

    private void MenuVeiculo_Click(object sender, RoutedEventArgs e)
    {
      CadastroVeiculo cadastro = new CadastroVeiculo();
      cadastro.ShowDialog();
    }
  }
}