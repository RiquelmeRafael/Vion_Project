using Guna.UI2.WinForms;
using Vion_Desktop.Models;
using Vion_Desktop.Services;

namespace Vion_Desktop.Views
{
    public class UsuarioForm : Form
    {
        private UsuarioService _service;
        private UsuarioDto? _usuario;
        private Label lblNome = null!;
        private Label lblEmail = null!;
        private Label lblSenha = null!;
        private Label lblTipo = null!;
        private Guna2TextBox txtNome = null!;
        private Guna2TextBox txtEmail = null!;
        private Guna2TextBox txtSenha = null!;
        private Guna2ComboBox cbTipoUsuario = null!;
        private Guna2Button btnSalvar = null!;
        private Guna2Button btnCancelar = null!;

        public UsuarioCreateDto? NovoUsuario { get; private set; }
        public UsuarioUpdateDto? UsuarioAtualizado { get; private set; }

        public UsuarioForm(UsuarioDto? usuario = null)
        {
            InitializeComponent();
            _service = new UsuarioService();
            _usuario = usuario;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            if (this.DesignMode) return;

            CarregarTipos();

            if (_usuario != null)
            {
                txtNome.Text = _usuario.Nome;
                txtEmail.Text = _usuario.Email;
                // Senha fica vazia na edição
                this.Text = "Editar Usuário";
            }
            else
            {
                this.Text = "Novo Usuário";
            }
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblNome = new Label();
            txtNome = new Guna2TextBox();
            lblEmail = new Label();
            txtEmail = new Guna2TextBox();
            lblSenha = new Label();
            txtSenha = new Guna2TextBox();
            lblTipo = new Label();
            cbTipoUsuario = new Guna2ComboBox();
            btnSalvar = new Guna2Button();
            btnCancelar = new Guna2Button();
            SuspendLayout();
            // 
            // lblNome
            // 
            lblNome.AutoSize = true;
            lblNome.Font = new Font("Segoe UI", 10F);
            lblNome.Location = new Point(20, 15);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(46, 19);
            lblNome.TabIndex = 0;
            lblNome.Text = "Nome";
            // 
            // txtNome
            // 
            txtNome.BorderRadius = 4;
            txtNome.CustomizableEdges = customizableEdges1;
            txtNome.DefaultText = "";
            txtNome.Font = new Font("Segoe UI", 9F);
            txtNome.Location = new Point(20, 35);
            txtNome.Name = "txtNome";
            txtNome.PlaceholderText = "Digite o nome";
            txtNome.SelectedText = "";
            txtNome.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtNome.Size = new Size(320, 35);
            txtNome.TabIndex = 1;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F);
            lblEmail.Location = new Point(20, 80);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(41, 19);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.BorderRadius = 4;
            txtEmail.CustomizableEdges = customizableEdges3;
            txtEmail.DefaultText = "";
            txtEmail.Font = new Font("Segoe UI", 9F);
            txtEmail.Location = new Point(20, 100);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "Digite o email";
            txtEmail.SelectedText = "";
            txtEmail.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtEmail.Size = new Size(320, 35);
            txtEmail.TabIndex = 3;
            // 
            // lblSenha
            // 
            lblSenha.AutoSize = true;
            lblSenha.Font = new Font("Segoe UI", 10F);
            lblSenha.Location = new Point(20, 145);
            lblSenha.Name = "lblSenha";
            lblSenha.Size = new Size(46, 19);
            lblSenha.TabIndex = 4;
            lblSenha.Text = "Senha";
            // 
            // txtSenha
            // 
            txtSenha.BorderRadius = 4;
            txtSenha.CustomizableEdges = customizableEdges5;
            txtSenha.DefaultText = "";
            txtSenha.Font = new Font("Segoe UI", 9F);
            txtSenha.Location = new Point(20, 165);
            txtSenha.Name = "txtSenha";
            txtSenha.PasswordChar = '●';
            txtSenha.PlaceholderText = "Digite a senha";
            txtSenha.SelectedText = "";
            txtSenha.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtSenha.Size = new Size(320, 35);
            txtSenha.TabIndex = 5;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Font = new Font("Segoe UI", 10F);
            lblTipo.Location = new Point(20, 210);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(105, 19);
            lblTipo.TabIndex = 6;
            lblTipo.Text = "Tipo de Usuário";
            // 
            // cbTipoUsuario
            // 
            cbTipoUsuario.BackColor = Color.Transparent;
            cbTipoUsuario.BorderRadius = 4;
            cbTipoUsuario.CustomizableEdges = customizableEdges7;
            cbTipoUsuario.DrawMode = DrawMode.OwnerDrawFixed;
            cbTipoUsuario.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTipoUsuario.FocusedColor = Color.Empty;
            cbTipoUsuario.Font = new Font("Segoe UI", 10F);
            cbTipoUsuario.ForeColor = Color.FromArgb(68, 88, 112);
            cbTipoUsuario.ItemHeight = 30;
            cbTipoUsuario.Location = new Point(20, 230);
            cbTipoUsuario.Name = "cbTipoUsuario";
            cbTipoUsuario.ShadowDecoration.CustomizableEdges = customizableEdges8;
            cbTipoUsuario.Size = new Size(320, 36);
            cbTipoUsuario.TabIndex = 7;
            // 
            // btnSalvar
            // 
            btnSalvar.BorderRadius = 4;
            btnSalvar.CustomizableEdges = customizableEdges9;
            btnSalvar.FillColor = Color.FromArgb(40, 40, 40);
            btnSalvar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSalvar.ForeColor = Color.White;
            btnSalvar.Location = new Point(20, 310);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnSalvar.Size = new Size(150, 40);
            btnSalvar.TabIndex = 8;
            btnSalvar.Text = "SALVAR";
            btnSalvar.Click += BtnSalvar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BorderRadius = 4;
            btnCancelar.CustomizableEdges = customizableEdges11;
            btnCancelar.FillColor = Color.Gray;
            btnCancelar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(190, 310);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnCancelar.Size = new Size(150, 40);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.Click += BtnCancelar_Click;
            // 
            // UsuarioForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(360, 380);
            Controls.Add(lblNome);
            Controls.Add(txtNome);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblSenha);
            Controls.Add(txtSenha);
            Controls.Add(lblTipo);
            Controls.Add(cbTipoUsuario);
            Controls.Add(btnSalvar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            Name = "UsuarioForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Novo Usuário";
            ResumeLayout(false);
            PerformLayout();
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private async void CarregarTipos()
        {
            try
            {
                var tipos = ApiClient.CurrentRole == "" 
                    ? await _service.GetTiposPublicoAsync() 
                    : await _service.GetTiposAsync();
                cbTipoUsuario.DataSource = tipos;
                cbTipoUsuario.DisplayMember = "Nome";
                cbTipoUsuario.ValueMember = "Id";

                if (_usuario != null)
                {
                    cbTipoUsuario.SelectedValue = _usuario.TipoUsuarioId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar tipos: " + ex.Message);
            }
        }

        private void BtnSalvar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Preencha todos os campos obrigatórios.");
                return;
            }

            if (_usuario == null && string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("A senha é obrigatória para novos usuários.");
                return;
            }

            if (cbTipoUsuario.SelectedValue == null)
            {
                MessageBox.Show("Selecione um tipo de usuário.");
                return;
            }

            int tipoId = (int)cbTipoUsuario.SelectedValue;

            if (_usuario == null)
            {
                NovoUsuario = new UsuarioCreateDto
                {
                    Nome = txtNome.Text,
                    Email = txtEmail.Text,
                    Senha = txtSenha.Text,
                    TipoUsuarioId = tipoId
                };
            }
            else
            {
                UsuarioAtualizado = new UsuarioUpdateDto
                {
                    Nome = txtNome.Text,
                    Email = txtEmail.Text,
                    Senha = string.IsNullOrWhiteSpace(txtSenha.Text) ? null : txtSenha.Text,
                    TipoUsuarioId = tipoId
                };
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
