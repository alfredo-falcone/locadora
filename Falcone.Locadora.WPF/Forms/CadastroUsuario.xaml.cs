using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Falcone.Locadora.Sistema;
using Falcone.Locadora.Sistema.Data;

namespace Falcone.Locadora.WPF.Forms
{
    /// <summary>
    /// Interaction logic for CadastroUsuario.xaml
    /// </summary>
  public partial class CadastroUsuario : Falcone.Locadora.WPF.Forms.Base.BaseWindow
    {
        public CadastroUsuario()
        {
            InitializeComponent();
        }

        private void btGravar_Click(object sender, RoutedEventArgs e)
        {
            var usuario = new Usuario() { Login = tbLogin.Text, Nome = tbNome.Text, Sal = Guid.NewGuid().ToString(), Senha = tbSenha.Password };
            string senha = Falcone.Locadora.Sistema.Src.Util.GerarMD5(usuario.Sal + usuario.Senha);
            usuario.Senha = senha;
            this.Banco.Usuarios.Add(usuario);
            this.Banco.SaveChanges();
            this.Close();
        }
    }
}
