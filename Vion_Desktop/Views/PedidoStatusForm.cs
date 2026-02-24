using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Vion_Desktop.Views
{
    public partial class PedidoStatusForm : Form
    {
        public string? NovoStatus { get; private set; }

        private Guna2ComboBox cboStatus = null!;
        private Guna2Button btnSalvar = null!;
        private Guna2Button btnCancelar = null!;
        private Panel pnlBorder = null!;
        private Label lblTitulo = null!;

        public PedidoStatusForm(string statusAtual)
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(statusAtual) && cboStatus.Items.Contains(statusAtual))
            {
                cboStatus.SelectedItem = statusAtual;
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
            cboStatus = new Guna2ComboBox();
            btnSalvar = new Guna2Button();
            btnCancelar = new Guna2Button();
            pnlBorder = new Panel();
            lblTitulo = new Label();
            pnlBorder.SuspendLayout();
            SuspendLayout();
            // 
            // cboStatus
            // 
            cboStatus.BackColor = Color.Transparent;
            cboStatus.CustomizableEdges = customizableEdges1;
            cboStatus.DrawMode = DrawMode.OwnerDrawFixed;
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.FocusedColor = Color.Empty;
            cboStatus.Font = new Font("Segoe UI", 10F);
            cboStatus.ForeColor = Color.FromArgb(68, 88, 112);
            cboStatus.ItemHeight = 30;
            cboStatus.Items.AddRange(new object[] { "Pendente", "Aprovado", "Em Transporte", "Entregue", "Cancelado" });
            cboStatus.Location = new Point(20, 60);
            cboStatus.Name = "cboStatus";
            cboStatus.ShadowDecoration.CustomizableEdges = customizableEdges2;
            cboStatus.Size = new Size(260, 36);
            cboStatus.TabIndex = 1;
            // 
            // btnSalvar
            // 
            btnSalvar.BorderRadius = 4;
            btnSalvar.CustomizableEdges = customizableEdges3;
            btnSalvar.FillColor = Color.FromArgb(40, 40, 40);
            btnSalvar.Font = new Font("Segoe UI", 9F);
            btnSalvar.ForeColor = Color.White;
            btnSalvar.Location = new Point(20, 120);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnSalvar.Size = new Size(120, 40);
            btnSalvar.TabIndex = 2;
            btnSalvar.Text = "SALVAR";
            btnSalvar.Click += BtnSalvar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BorderColor = Color.FromArgb(40, 40, 40);
            btnCancelar.BorderRadius = 4;
            btnCancelar.BorderThickness = 1;
            btnCancelar.CustomizableEdges = customizableEdges5;
            btnCancelar.FillColor = Color.White;
            btnCancelar.Font = new Font("Segoe UI", 9F);
            btnCancelar.ForeColor = Color.FromArgb(40, 40, 40);
            btnCancelar.Location = new Point(160, 120);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnCancelar.Size = new Size(120, 40);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.Click += BtnCancelar_Click;
            // 
            // pnlBorder
            // 
            pnlBorder.BorderStyle = BorderStyle.FixedSingle;
            pnlBorder.Controls.Add(lblTitulo);
            pnlBorder.Controls.Add(cboStatus);
            pnlBorder.Controls.Add(btnSalvar);
            pnlBorder.Controls.Add(btnCancelar);
            pnlBorder.Dock = DockStyle.Fill;
            pnlBorder.Location = new Point(0, 0);
            pnlBorder.Name = "pnlBorder";
            pnlBorder.Size = new Size(331, 185);
            pnlBorder.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(130, 21);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Atualizar Status";
            // 
            // PedidoStatusForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(331, 185);
            Controls.Add(pnlBorder);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PedidoStatusForm";
            StartPosition = FormStartPosition.CenterParent;
            pnlBorder.ResumeLayout(false);
            pnlBorder.PerformLayout();
            ResumeLayout(false);
        }

        private void BtnSalvar_Click(object? sender, EventArgs e)
        {
            NovoStatus = cboStatus.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(NovoStatus))
            {
                MessageBox.Show("Selecione um status.");
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
