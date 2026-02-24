using System;
using System.Windows.Forms;
using Vion_Desktop.Services;
using Vion_Desktop.Models;

namespace Vion_Desktop.Views
{
    public partial class Login : Form
    {
        private readonly AuthService _authService;

        public Login()
        {
            InitializeComponent();
            _authService = new AuthService();
            this.AcceptButton = btnEntrar; // Permite login com Enter
            ConfigurarEventos();
        }

        private void ConfigurarEventos()
        {
            btnEntrar.Click += BtnEntrar_Click;
            btnFechar.Click += BtnFechar_Click;
            btnCadastrarUsuario.Click += BtnCadastrarUsuario_Click;
        }

        private async void BtnEntrar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("Preencha todos os campos!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnEntrar.Enabled = false;
                btnEntrar.Text = "Entrando...";

                var (sucesso, mensagem) = await _authService.LoginAsync(txtEmail.Text, txtSenha.Text);

                if (sucesso)
                {
                    this.Hide();
                    var dashboard = new Dashboard();
                    dashboard.Show();
                }
                else
                {
                    MessageBox.Show(mensagem, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnEntrar.Enabled = true;
                btnEntrar.Text = "ENTRAR";
            }
        }

        private void BtnFechar_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void BtnCadastrarUsuario_Click(object? sender, EventArgs e)
        {
            using (var form = new UsuarioForm())
            {
                if (form.ShowDialog() != DialogResult.OK || form.NovoUsuario == null)
                {
                    return;
                }

                try
                {
                    btnCadastrarUsuario.Enabled = false;
                    btnCadastrarUsuario.Text = "Carregando...";

                    var service = new UsuarioService();
                    await service.RegisterAdminAsync(form.NovoUsuario);

                    MessageBox.Show("Cadastro realizado com sucesso. Agora faça login com o usuário criado.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao cadastrar usuário: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnCadastrarUsuario.Enabled = true;
                    btnCadastrarUsuario.Text = "Cadastrar Usuário";
                }
            }
        }

       
    }
}
