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

namespace Falcone.Locadora.WPF.Forms.Base
{
  /// <summary>
  /// Interaction logic for BaseWindow.xaml
  /// </summary>
  public partial class BaseWindow : Window
  {
    public BaseWindow()
    {
      /*IconBitmapDecoder ibd = new IconBitmapDecoder(new Uri(@"pack://application:,,/Images/car-rental-systems-icon-big.png", UriKind.RelativeOrAbsolute),

           BitmapCreateOptions.None,

           BitmapCacheOption.Default);
      this.Icon = ibd.Frames[0];
       */
      var uriSource = new Uri(@"pack://application:,,/Images/pictographs-car_rental_inv_t.png", UriKind.RelativeOrAbsolute);
      this.Icon = new BitmapImage(uriSource);
    }
  }
}
