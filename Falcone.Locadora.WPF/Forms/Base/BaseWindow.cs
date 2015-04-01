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

  }
}
