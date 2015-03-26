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
using Falcone.Locadora.WPF.Forms;
using Falcone.Locadora.Sistema.Src;

namespace Falcone.Locadora.WPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Falcone.Locadora.WPF.Forms.Base.BaseWindow
    {
        public Login()
        {
            InitializeComponent();
            Load();
            //tbLogin.Focus();
            this.ShowInTaskbar = true;
        }

        private void Load()
        {

          var usuarios = this.Banco.Usuarios.ToList();
            //cmbUsuarios.DisplayMemberPath = "Nome";
            //cmbUsuarios.SelectedValuePath = "Login";
            //cmbUsuarios.ItemsSource = usuarios;
            //var usuario = new Usuario();
            //string nometipo = usuario.GetType().Name;
            /*var sequenciais = banco.Sequenciais.Where(s => s.Entidade == nometipo).ToList();
            foreach (Sequencial s in sequenciais)
            {
              s.ProximoSequencial += 1;
            }
             
            banco.Sequenciais.Add(new Sequencial() { Entidade = "Teste", ProximoSequencial = 3 });
            banco.SaveChanges();
          */

        }

        private void btCadastro_Click(object sender, RoutedEventArgs e)
        {
            CadastroUsuario cadastro = new CadastroUsuario();
            cadastro.ShowDialog(this);
            Load();
        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {

          try
          {
            Util.ValidarLogin(tbLogin.Text, tbSenha.Password);
            //MessageBox.Show("Senha ok");
            Principal frmPrincipal = new Principal();
            frmPrincipal.Show();
            this.Close();
          }
          catch (Exception ex)
          {
            MessageBox.Show(ex.Message);
          }
          
        }
    }
}
