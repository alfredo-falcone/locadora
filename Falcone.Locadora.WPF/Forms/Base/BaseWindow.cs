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
      /*IconBitmapDecoder ibd = new IconBitmapDecoder(new Uri(@"pack://application:,,/Images/car-rental-systems-icon-big.png", UriKind.RelativeOrAbsolute),

           BitmapCreateOptions.None,

           BitmapCacheOption.Default);
      this.Icon = ibd.Frames[0];
       */
      var uriSource = new Uri(@"pack://application:,,/Images/pictographs-car_rental_inv_t.png", UriKind.RelativeOrAbsolute);
      this.Icon = new BitmapImage(uriSource);
      this.VerticalAlignment = System.Windows.VerticalAlignment.Center;
      this.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
      this.ShowInTaskbar = false;

      this.Loaded += (sender, e) => MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
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
