using Guna.UI2.WinForms;
using Vion_Desktop.Models;
using Vion_Desktop.Services;

namespace Vion_Desktop.Views
{
    public class UsuariosView : Form
    {
        private Guna2Button? btnNovo;
        private Guna2Button? btnEditar;
        private Guna2Button? btnExcluir;
        private UsuarioService _service;

        private Panel panelTop = null!;
        private Label lblTitle = null!;

        // Grid components
        private Panel panelGrid = null!;
        private DataGridView grid = null!;

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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            
            panelTop = new Panel();
            lblTitle = new Label();
            btnNovo = new Guna2Button();
            btnEditar = new Guna2Button();
            btnExcluir = new Guna2Button();
            
            panelGrid = new Panel();
            grid = new DataGridView();
            
            panelTop.SuspendLayout();
            panelGrid.SuspendLayout();
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
            panelTop.Height = 60;
            panelTop.Name = "panelTop";
            panelTop.TabIndex = 1;
            
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
            // panelGrid
            // 
            panelGrid.Controls.Add(grid);
            panelGrid.Dock = DockStyle.Fill;
            panelGrid.Location = new Point(0, 60);
            panelGrid.Name = "panelGrid";
            panelGrid.Padding = new Padding(20);
            panelGrid.TabIndex = 0;

            // 
            // grid
            // 
            grid.AllowUserToAddRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.BackgroundColor = Color.FromArgb(245, 246, 250);
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            
            dataGridViewCellStyle1.BackColor = Color.FromArgb(45, 52, 54);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(45, 52, 54);
            grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            grid.ColumnHeadersHeight = 40;
            
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(116, 185, 255);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            grid.DefaultCellStyle = dataGridViewCellStyle2;
            
            grid.Dock = DockStyle.Fill;
            grid.EnableHeadersVisualStyles = false;
            grid.GridColor = Color.FromArgb(230, 230, 230);
            grid.MultiSelect = false;
            grid.Name = "grid";
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.RowTemplate.Height = 40;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.TabIndex = 0;

            // 
            // UsuariosView
            // 
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(791, 535);
            Controls.Add(panelGrid); // Added last (Top of Z-order) -> Docked first (Fill)
            Controls.Add(panelTop);  // Added first (Bottom of Z-order) -> Docked last (Top)
            // Wait, previous logic was:
            // Controls.Add(panelTop); // Index 0
            // Controls.Add(panelGrid); // Index 0, panelTop becomes 1
            // Result: [panelGrid, panelTop]
            // Docking: panelTop (Index 1) -> panelGrid (Index 0)
            // panelTop Top -> panelGrid Fill.
            // Correct.
            
            FormBorderStyle = FormBorderStyle.None;
            Name = "UsuariosView";
            
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelGrid.ResumeLayout(false);
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

            if (MessageBox.Show($"Deseja excluir o usuário {item.Nome}?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    await _service.DeleteAsync(item.Id);
                    await CarregarDados();
                    MessageBox.Show("Usuário excluído com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir: " + ex.Message);
                }
            }
        }
    }
}
