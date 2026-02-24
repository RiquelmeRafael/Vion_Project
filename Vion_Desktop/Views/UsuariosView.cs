using Guna.UI2.WinForms;
using Vion_Desktop.Models;
using Vion_Desktop.Services;

namespace Vion_Desktop.Views
{
    public class UsuariosView : Form
    {
        private Guna2DataGridView grid = null!;
        private Guna2Button? btnNovo;
        private Guna2Button? btnEditar;
        private Guna2Button? btnExcluir;
        private UsuarioService _service;

        private Panel panelTop = null!;
        private Label lblTitle = null!;

        public UsuariosView()
        {
            _service = new UsuarioService();
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.DesignMode) return;

            bool isAdmin = ApiClient.CurrentRole == "Admin";
            if (!isAdmin)
            {
                if (btnNovo != null) btnNovo.Visible = false;
                if (btnEditar != null) btnEditar.Visible = false;
                if (btnExcluir != null) btnExcluir.Visible = false;
            }

            _ = CarregarDados();
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panelTop = new Panel();
            lblTitle = new Label();
            btnNovo = new Guna2Button();
            btnEditar = new Guna2Button();
            btnExcluir = new Guna2Button();
            grid = new Guna2DataGridView();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.White;
            panelTop.Controls.Add(lblTitle);
            panelTop.Controls.Add(btnNovo);
            panelTop.Controls.Add(btnEditar);
            panelTop.Controls.Add(btnExcluir);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1498, 44);
            panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(220, 21);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Gerenciamento de Usuários";
            // 
            // btnNovo
            // 
            btnNovo.BorderRadius = 4;
            btnNovo.CustomizableEdges = customizableEdges1;
            btnNovo.FillColor = Color.FromArgb(40, 40, 40);
            btnNovo.Font = new Font("Segoe UI", 9F);
            btnNovo.ForeColor = Color.White;
            btnNovo.Location = new Point(260, 10);
            btnNovo.Name = "btnNovo";
            btnNovo.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnNovo.Size = new Size(130, 30);
            btnNovo.TabIndex = 1;
            btnNovo.Text = "NOVO USUÁRIO";
            btnNovo.Click += BtnNovo_Click;
            // 
            // btnEditar
            // 
            btnEditar.BorderRadius = 4;
            btnEditar.CustomizableEdges = customizableEdges3;
            btnEditar.FillColor = Color.FromArgb(0, 120, 215);
            btnEditar.Font = new Font("Segoe UI", 9F);
            btnEditar.ForeColor = Color.White;
            btnEditar.Location = new Point(400, 10);
            btnEditar.Name = "btnEditar";
            btnEditar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnEditar.Size = new Size(100, 30);
            btnEditar.TabIndex = 2;
            btnEditar.Text = "EDITAR";
            btnEditar.Click += BtnEditar_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.BorderRadius = 4;
            btnExcluir.CustomizableEdges = customizableEdges5;
            btnExcluir.FillColor = Color.FromArgb(220, 53, 69);
            btnExcluir.Font = new Font("Segoe UI", 9F);
            btnExcluir.ForeColor = Color.White;
            btnExcluir.Location = new Point(510, 10);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnExcluir.Size = new Size(100, 30);
            btnExcluir.TabIndex = 3;
            btnExcluir.Text = "EXCLUIR";
            btnExcluir.Click += BtnExcluir_Click;
            // 
            // grid
            // 
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            grid.ColumnHeadersHeight = 30;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            grid.DefaultCellStyle = dataGridViewCellStyle3;
            grid.Dock = DockStyle.Fill;
            grid.GridColor = Color.FromArgb(231, 229, 255);
            grid.Location = new Point(0, 44);
            grid.MultiSelect = false;
            grid.Name = "grid";
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.Size = new Size(1498, 591);
            grid.TabIndex = 1;
            grid.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            grid.ThemeStyle.AlternatingRowsStyle.Font = null;
            grid.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            grid.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            grid.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            grid.ThemeStyle.BackColor = Color.White;
            grid.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            grid.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(40, 40, 40);
            grid.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grid.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            grid.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ThemeStyle.HeaderStyle.Height = 30;
            grid.ThemeStyle.ReadOnly = true;
            grid.ThemeStyle.RowsStyle.BackColor = Color.White;
            grid.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 10F);
            grid.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            grid.ThemeStyle.RowsStyle.Height = 25;
            grid.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            grid.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            grid.CellContentClick += grid_CellContentClick;
            // 
            // UsuariosView
            // 
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(1498, 635);
            Controls.Add(grid);
            Controls.Add(panelTop);
            FormBorderStyle = FormBorderStyle.None;
            Name = "UsuariosView";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ResumeLayout(false);
        }

        private async Task CarregarDados()
        {
            try
            {
                var usuarios = await _service.GetAllAsync();
                grid.DataSource = usuarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar usuários: " + ex.Message);
            }
        }

        private async void BtnNovo_Click(object? sender, EventArgs e)
        {
            using (var form = new UsuarioForm())
            {
                if (form.ShowDialog() == DialogResult.OK && form.NovoUsuario != null)
                {
                    try
                    {
                        await _service.CreateAsync(form.NovoUsuario);
                        await CarregarDados();
                        MessageBox.Show("Usuário criado com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao criar: " + ex.Message);
                    }
                }
            }
        }

        private async void BtnEditar_Click(object? sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um usuário.");
                return;
            }

            var item = grid.SelectedRows[0].DataBoundItem as UsuarioDto;
            if (item == null) return;

            using (var form = new UsuarioForm(item))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        await _service.UpdateAsync(item.Id, form.UsuarioAtualizado!);
                        await CarregarDados();
                        MessageBox.Show("Usuário atualizado com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao atualizar: " + ex.Message);
                    }
                }
            }
        }

        private async void BtnExcluir_Click(object? sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um usuário.");
                return;
            }

            var item = grid.SelectedRows[0].DataBoundItem as UsuarioDto;
            if (item == null) return;

            if (MessageBox.Show($"Tem certeza que deseja excluir {item.Nome}?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    await _service.DeleteAsync(item.Id);
                    await CarregarDados();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir: " + ex.Message);
                }
            }
        }

        private void grid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
