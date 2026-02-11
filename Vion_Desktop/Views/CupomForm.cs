using System;
using System.Drawing;
using System.Windows.Forms;
using Vion_Desktop.Models;

namespace Vion_Desktop.Views
{
    public partial class CupomForm : Form
    {
        public CupomCreateDto? Cupom { get; private set; }

        private Guna.UI2.WinForms.Guna2TextBox txtCodigo = null!;
        private Guna.UI2.WinForms.Guna2NumericUpDown numDesconto = null!;
        private Guna.UI2.WinForms.Guna2CheckBox chkAtivo = null!;
        private Guna.UI2.WinForms.Guna2Button btnSalvar = null!;
        private Guna.UI2.WinForms.Guna2Button btnCancelar = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCodigo = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDesconto = null!;

        public CupomForm(CupomDto? cupom = null)
        {
            InitializeComponent();
            if (cupom != null)
            {
                this.Text = "Editar Cupom";
                txtCodigo.Text = cupom.Codigo;
                numDesconto.Value = cupom.PercentualDesconto;
                chkAtivo.Checked = cupom.Ativo;
            }
            else
            {
                this.Text = "Novo Cupom";
                Cupom = new CupomCreateDto();
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
            txtCodigo = new Guna.UI2.WinForms.Guna2TextBox();
            numDesconto = new Guna.UI2.WinForms.Guna2NumericUpDown();
            chkAtivo = new Guna.UI2.WinForms.Guna2CheckBox();
            btnSalvar = new Guna.UI2.WinForms.Guna2Button();
            btnCancelar = new Guna.UI2.WinForms.Guna2Button();
            lblCodigo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblDesconto = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)numDesconto).BeginInit();
            SuspendLayout();
            // 
            // txtCodigo
            // 
            txtCodigo.BorderRadius = 5;
            txtCodigo.CustomizableEdges = customizableEdges1;
            txtCodigo.DefaultText = "";
            txtCodigo.Font = new Font("Segoe UI", 9F);
            txtCodigo.Location = new Point(30, 55);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.PlaceholderText = "";
            txtCodigo.SelectedText = "";
            txtCodigo.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtCodigo.Size = new Size(320, 36);
            txtCodigo.TabIndex = 1;
            // 
            // numDesconto
            // 
            numDesconto.BackColor = Color.Transparent;
            numDesconto.BorderRadius = 5;
            numDesconto.CustomizableEdges = customizableEdges3;
            numDesconto.Font = new Font("Segoe UI", 9F);
            numDesconto.Location = new Point(30, 135);
            numDesconto.Name = "numDesconto";
            numDesconto.ShadowDecoration.CustomizableEdges = customizableEdges4;
            numDesconto.Size = new Size(150, 36);
            numDesconto.TabIndex = 3;
            // 
            // chkAtivo
            // 
            chkAtivo.Checked = true;
            chkAtivo.CheckedState.BorderRadius = 0;
            chkAtivo.CheckedState.BorderThickness = 0;
            chkAtivo.CheckState = CheckState.Checked;
            chkAtivo.Location = new Point(200, 140);
            chkAtivo.Name = "chkAtivo";
            chkAtivo.Size = new Size(104, 24);
            chkAtivo.TabIndex = 4;
            chkAtivo.Text = "Ativo";
            chkAtivo.UncheckedState.BorderRadius = 0;
            chkAtivo.UncheckedState.BorderThickness = 0;
            // 
            // btnSalvar
            // 
            btnSalvar.BorderRadius = 5;
            btnSalvar.CustomizableEdges = customizableEdges5;
            btnSalvar.FillColor = Color.Green;
            btnSalvar.Font = new Font("Segoe UI", 9F);
            btnSalvar.ForeColor = Color.White;
            btnSalvar.Location = new Point(190, 200);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnSalvar.Size = new Size(160, 40);
            btnSalvar.TabIndex = 5;
            btnSalvar.Text = "SALVAR";
            btnSalvar.Click += BtnSalvar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BorderRadius = 5;
            btnCancelar.CustomizableEdges = customizableEdges7;
            btnCancelar.FillColor = Color.Gray;
            btnCancelar.Font = new Font("Segoe UI", 9F);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(30, 200);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnCancelar.Size = new Size(140, 40);
            btnCancelar.TabIndex = 6;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.Click += BtnCancelar_Click;
            // 
            // lblCodigo
            // 
            lblCodigo.BackColor = Color.Transparent;
            lblCodigo.Location = new Point(30, 30);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(42, 17);
            lblCodigo.TabIndex = 0;
            lblCodigo.Text = "Código";
            // 
            // lblDesconto
            // 
            lblDesconto.BackColor = Color.Transparent;
            lblDesconto.Location = new Point(30, 110);
            lblDesconto.Name = "lblDesconto";
            lblDesconto.Size = new Size(74, 17);
            lblDesconto.TabIndex = 2;
            lblDesconto.Text = "Desconto (%)";
            // 
            // CupomForm
            // 
            ClientSize = new Size(384, 261);
            Controls.Add(lblCodigo);
            Controls.Add(txtCodigo);
            Controls.Add(lblDesconto);
            Controls.Add(numDesconto);
            Controls.Add(chkAtivo);
            Controls.Add(btnSalvar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            Name = "CupomForm";
            StartPosition = FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)numDesconto).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void BtnSalvar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Código obrigatório!");
                return;
            }

            Cupom = new CupomCreateDto
            {
                Codigo = txtCodigo.Text.ToUpper(),
                PercentualDesconto = numDesconto.Value,
                Ativo = chkAtivo.Checked
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
