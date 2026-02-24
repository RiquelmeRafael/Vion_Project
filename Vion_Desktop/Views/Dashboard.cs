using System;
using System.Windows.Forms;
using Vion_Desktop.Services;

namespace Vion_Desktop.Views
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            ConfigurarBotoes();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            // Controle de acesso ao botão de Usuários
            if (ApiClient.CurrentRole != "Admin" && ApiClient.CurrentRole != "Gerente")
            {
                btnUsuarios.Visible = false;
            }
        }

        private void ConfigurarBotoes()
        {
            btnProdutos.Click += (s, e) => AbrirFormNoPanel(new ProdutosView());
            btnCategorias.Click += (s, e) => AbrirFormNoPanel(new CategoriasView());
            btnTamanhos.Click += (s, e) => AbrirFormNoPanel(new TamanhosView());
            btnCupons.Click += (s, e) => AbrirFormNoPanel(new CuponsView());
            btnPedidos.Click += (s, e) => AbrirFormNoPanel(new PedidosView());
            btnUsuarios.Click += (s, e) => AbrirFormNoPanel(new UsuariosView());
            btnSair.Click += (s, e) =>
            {
                this.Hide();
                new Login().Show();
            };
        }

        private void AbrirFormNoPanel(Form formFilho)
        {
            if (panelConteudo.Controls.Count > 0)
                panelConteudo.Controls.RemoveAt(0);

            formFilho.TopLevel = false;
            formFilho.FormBorderStyle = FormBorderStyle.None;
            formFilho.Dock = DockStyle.Fill;
            panelConteudo.Controls.Add(formFilho);
            panelConteudo.Tag = formFilho;
            formFilho.Show();
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {

        }
    }
}
