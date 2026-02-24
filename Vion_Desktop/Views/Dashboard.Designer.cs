namespace Vion_Desktop.Views
{
    partial class Dashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelMenu = new Guna.UI2.WinForms.Guna2Panel();
            lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnProdutos = new Guna.UI2.WinForms.Guna2Button();
            btnCategorias = new Guna.UI2.WinForms.Guna2Button();
            btnTamanhos = new Guna.UI2.WinForms.Guna2Button();
            btnCupons = new Guna.UI2.WinForms.Guna2Button();
            btnPedidos = new Guna.UI2.WinForms.Guna2Button();
            btnUsuarios = new Guna.UI2.WinForms.Guna2Button();
            btnSair = new Guna.UI2.WinForms.Guna2Button();
            panelConteudo = new Guna.UI2.WinForms.Guna2Panel();
            panelMenu.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(40, 40, 40);
            panelMenu.Controls.Add(lblTitulo);
            panelMenu.Controls.Add(btnProdutos);
            panelMenu.Controls.Add(btnCategorias);
            panelMenu.Controls.Add(btnTamanhos);
            panelMenu.Controls.Add(btnCupons);
            panelMenu.Controls.Add(btnPedidos);
            panelMenu.Controls.Add(btnUsuarios);
            panelMenu.Controls.Add(btnSair);
            panelMenu.CustomizableEdges = customizableEdges15;
            panelMenu.Dock = DockStyle.Left;
            panelMenu.FillColor = Color.Black;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.ShadowDecoration.CustomizableEdges = customizableEdges16;
            panelMenu.Size = new Size(220, 600);
            panelMenu.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(50, 30);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(71, 39);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "VION";
            // 
            // btnProdutos
            // 
            btnProdutos.CustomizableEdges = customizableEdges1;
            btnProdutos.DisabledState.BorderColor = Color.DarkGray;
            btnProdutos.DisabledState.CustomBorderColor = Color.DarkGray;
            btnProdutos.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnProdutos.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnProdutos.FillColor = Color.Black;
            btnProdutos.Font = new Font("Segoe UI", 11F);
            btnProdutos.ForeColor = Color.White;
            btnProdutos.Location = new Point(0, 120);
            btnProdutos.Name = "btnProdutos";
            btnProdutos.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnProdutos.Size = new Size(220, 45);
            btnProdutos.TabIndex = 1;
            btnProdutos.Text = "Produtos";
            btnProdutos.TextAlign = HorizontalAlignment.Left;
            btnProdutos.TextOffset = new Point(20, 0);
            btnProdutos.Click += btnProdutos_Click;
            // 
            // btnCategorias
            // 
            btnCategorias.CustomizableEdges = customizableEdges3;
            btnCategorias.DisabledState.BorderColor = Color.DarkGray;
            btnCategorias.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCategorias.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCategorias.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCategorias.FillColor = Color.Black;
            btnCategorias.Font = new Font("Segoe UI", 11F);
            btnCategorias.ForeColor = Color.White;
            btnCategorias.Location = new Point(0, 170);
            btnCategorias.Name = "btnCategorias";
            btnCategorias.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnCategorias.Size = new Size(220, 45);
            btnCategorias.TabIndex = 2;
            btnCategorias.Text = "Categorias";
            btnCategorias.TextAlign = HorizontalAlignment.Left;
            btnCategorias.TextOffset = new Point(20, 0);
            // 
            // btnTamanhos
            // 
            btnTamanhos.CustomizableEdges = customizableEdges5;
            btnTamanhos.DisabledState.BorderColor = Color.DarkGray;
            btnTamanhos.DisabledState.CustomBorderColor = Color.DarkGray;
            btnTamanhos.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnTamanhos.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnTamanhos.FillColor = Color.Black;
            btnTamanhos.Font = new Font("Segoe UI", 11F);
            btnTamanhos.ForeColor = Color.White;
            btnTamanhos.Location = new Point(0, 220);
            btnTamanhos.Name = "btnTamanhos";
            btnTamanhos.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnTamanhos.Size = new Size(220, 45);
            btnTamanhos.TabIndex = 3;
            btnTamanhos.Text = "Tamanhos";
            btnTamanhos.TextAlign = HorizontalAlignment.Left;
            btnTamanhos.TextOffset = new Point(20, 0);
            // 
            // btnCupons
            // 
            btnCupons.CustomizableEdges = customizableEdges7;
            btnCupons.DisabledState.BorderColor = Color.DarkGray;
            btnCupons.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCupons.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCupons.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCupons.FillColor = Color.Black;
            btnCupons.Font = new Font("Segoe UI", 11F);
            btnCupons.ForeColor = Color.White;
            btnCupons.Location = new Point(0, 270);
            btnCupons.Name = "btnCupons";
            btnCupons.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnCupons.Size = new Size(220, 45);
            btnCupons.TabIndex = 4;
            btnCupons.Text = "Cupons";
            btnCupons.TextAlign = HorizontalAlignment.Left;
            btnCupons.TextOffset = new Point(20, 0);
            // 
            // btnPedidos
            // 
            btnPedidos.CustomizableEdges = customizableEdges9;
            btnPedidos.DisabledState.BorderColor = Color.DarkGray;
            btnPedidos.DisabledState.CustomBorderColor = Color.DarkGray;
            btnPedidos.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnPedidos.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnPedidos.FillColor = Color.Black;
            btnPedidos.Font = new Font("Segoe UI", 11F);
            btnPedidos.ForeColor = Color.White;
            btnPedidos.Location = new Point(0, 320);
            btnPedidos.Name = "btnPedidos";
            btnPedidos.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnPedidos.Size = new Size(220, 45);
            btnPedidos.TabIndex = 5;
            btnPedidos.Text = "Pedidos";
            btnPedidos.TextAlign = HorizontalAlignment.Left;
            btnPedidos.TextOffset = new Point(20, 0);
            // 
            // btnUsuarios
            // 
            btnUsuarios.CustomizableEdges = customizableEdges11;
            btnUsuarios.DisabledState.BorderColor = Color.DarkGray;
            btnUsuarios.DisabledState.CustomBorderColor = Color.DarkGray;
            btnUsuarios.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnUsuarios.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnUsuarios.FillColor = Color.Black;
            btnUsuarios.Font = new Font("Segoe UI", 11F);
            btnUsuarios.ForeColor = Color.White;
            btnUsuarios.Location = new Point(0, 370);
            btnUsuarios.Name = "btnUsuarios";
            btnUsuarios.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnUsuarios.Size = new Size(220, 45);
            btnUsuarios.TabIndex = 6;
            btnUsuarios.Text = "Usuários";
            btnUsuarios.TextAlign = HorizontalAlignment.Left;
            btnUsuarios.TextOffset = new Point(20, 0);
            // 
            // btnSair
            // 
            btnSair.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSair.CustomizableEdges = customizableEdges13;
            btnSair.DisabledState.BorderColor = Color.DarkGray;
            btnSair.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSair.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSair.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSair.FillColor = Color.Firebrick;
            btnSair.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSair.ForeColor = Color.White;
            btnSair.Location = new Point(20, 530);
            btnSair.Name = "btnSair";
            btnSair.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnSair.Size = new Size(180, 40);
            btnSair.TabIndex = 4;
            btnSair.Text = "Sair";
            // 
            // panelConteudo
            // 
            panelConteudo.CustomizableEdges = customizableEdges17;
            panelConteudo.Dock = DockStyle.Fill;
            panelConteudo.Location = new Point(220, 0);
            panelConteudo.Name = "panelConteudo";
            panelConteudo.ShadowDecoration.CustomizableEdges = customizableEdges18;
            panelConteudo.Size = new Size(780, 600);
            panelConteudo.TabIndex = 1;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(panelConteudo);
            Controls.Add(panelMenu);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Dashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Vion - Dashboard";
            panelMenu.ResumeLayout(false);
            panelMenu.PerformLayout();
            ResumeLayout(false);
        }

        private Guna.UI2.WinForms.Guna2Panel panelMenu;
        private Guna.UI2.WinForms.Guna2Button btnProdutos;
        private Guna.UI2.WinForms.Guna2Button btnCategorias;
        private Guna.UI2.WinForms.Guna2Button btnTamanhos;
        private Guna.UI2.WinForms.Guna2Button btnCupons;
        private Guna.UI2.WinForms.Guna2Button btnPedidos;
        private Guna.UI2.WinForms.Guna2Button btnUsuarios;
        private Guna.UI2.WinForms.Guna2Button btnSair;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitulo;
        private Guna.UI2.WinForms.Guna2Panel panelConteudo;
    }
}
