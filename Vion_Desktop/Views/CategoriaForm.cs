using System;
using System.Windows.Forms;
using Vion_Desktop.Models;

namespace Vion_Desktop.Views
{
    public partial class CategoriaForm : Form
    {
        public string NomeCategoria { get; private set; } = string.Empty;

        private Guna.UI2.WinForms.Guna2TextBox txtNome = null!;
        private Guna.UI2.WinForms.Guna2Button btnSalvar = null!;
        private Guna.UI2.WinForms.Guna2Button btnCancelar = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNome = null!;

        public CategoriaForm(string? nomeAtual = null)
        {
            InitializeComponent();
            if (nomeAtual != null)
            {
                txtNome.Text = nomeAtual;
                this.Text = "Editar Categoria";
            }
            else
            {
                this.Text = "Nova Categoria";
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
            lblNome = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtNome = new Guna.UI2.WinForms.Guna2TextBox();
            btnSalvar = new Guna.UI2.WinForms.Guna2Button();
            btnCancelar = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // lblNome
            // 
            lblNome.BackColor = Color.Transparent;
            lblNome.Font = new Font("Segoe UI", 10F);
            lblNome.Location = new Point(30, 30);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(119, 19);
            lblNome.TabIndex = 0;
            lblNome.Text = "Nome da Categoria";
            // 
            // txtNome
            // 
            txtNome.BorderRadius = 5;
            txtNome.CustomizableEdges = customizableEdges1;
            txtNome.DefaultText = "";
            txtNome.Font = new Font("Segoe UI", 9F);
            txtNome.Location = new Point(30, 60);
            txtNome.Name = "txtNome";
            txtNome.PlaceholderText = "";
            txtNome.SelectedText = "";
            txtNome.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtNome.Size = new Size(320, 36);
            txtNome.TabIndex = 1;
            // 
            // btnSalvar
            // 
            btnSalvar.BorderRadius = 5;
            btnSalvar.CustomizableEdges = customizableEdges3;
            btnSalvar.Font = new Font("Segoe UI", 9F);
            btnSalvar.ForeColor = Color.White;
            btnSalvar.Location = new Point(190, 130);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnSalvar.Size = new Size(160, 40);
            btnSalvar.TabIndex = 2;
            btnSalvar.Text = "SALVAR";
            btnSalvar.Click += BtnSalvar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BorderRadius = 5;
            btnCancelar.CustomizableEdges = customizableEdges5;
            btnCancelar.FillColor = Color.Gray;
            btnCancelar.Font = new Font("Segoe UI", 9F);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(30, 130);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnCancelar.Size = new Size(140, 40);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.Click += BtnCancelar_Click;
            // 
            // CategoriaForm
            // 
            ClientSize = new Size(384, 211);
            Controls.Add(lblNome);
            Controls.Add(txtNome);
            Controls.Add(btnSalvar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CategoriaForm";
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);
            PerformLayout();
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void BtnSalvar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O nome é obrigatório!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NomeCategoria = txtNome.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
