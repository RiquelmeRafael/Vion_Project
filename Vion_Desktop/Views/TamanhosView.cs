using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vion_Desktop.Models;
using Vion_Desktop.Services;

namespace Vion_Desktop.Views
{
    public partial class TamanhosView : Form
    {
        private readonly TamanhoService _service;
        private Guna2DataGridView gridTamanhos = null!;
        private Guna2Button btnNovo = null!;
        private Guna2Button btnEditar = null!;
        private Guna2Button btnExcluir = null!;
        private Guna2Button btnAtualizar = null!;

        private Panel panelTop = null!;
        private Label lblTitulo = null!;

        public TamanhosView()
        {
            InitializeComponent();
            _service = new TamanhoService();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            if (this.DesignMode) return;
            
            _ = CarregarDados();
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            
            this.panelTop = new Panel();
            this.lblTitulo = new Label();
            this.btnNovo = new Guna2Button();
            this.btnEditar = new Guna2Button();
            this.btnExcluir = new Guna2Button();
            this.btnAtualizar = new Guna2Button();
            this.gridTamanhos = new Guna2DataGridView();
            
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTamanhos)).BeginInit();
            this.SuspendLayout();
            
            // 
            // panelTop
            // 
            this.panelTop.BackColor = Color.White;
            this.panelTop.Controls.Add(this.lblTitulo);
            this.panelTop.Controls.Add(this.btnNovo);
            this.panelTop.Controls.Add(this.btnEditar);
            this.panelTop.Controls.Add(this.btnExcluir);
            this.panelTop.Controls.Add(this.btnAtualizar);
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Height = 80;
            this.panelTop.Name = "panelTop";
            this.panelTop.TabIndex = 0;
            
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitulo.Location = new Point(20, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new Size(129, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Tamanhos";
            
            // 
            // btnNovo
            // 
            this.btnNovo.BorderRadius = 5;
            this.btnNovo.CustomizableEdges = customizableEdges1;
            this.btnNovo.FillColor = Color.Green;
            this.btnNovo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnNovo.ForeColor = Color.White;
            this.btnNovo.Location = new Point(200, 20);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.ShadowDecoration.CustomizableEdges = customizableEdges2;
            this.btnNovo.Size = new Size(200, 40);
            this.btnNovo.TabIndex = 1;
            this.btnNovo.Text = "Novo Tamanho";
            this.btnNovo.Click += BtnNovo_Click;
            
            // 
            // btnEditar
            // 
            this.btnEditar.BorderRadius = 5;
            this.btnEditar.CustomizableEdges = customizableEdges3;
            this.btnEditar.FillColor = Color.Orange;
            this.btnEditar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnEditar.ForeColor = Color.White;
            this.btnEditar.Location = new Point(410, 20);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            this.btnEditar.Size = new Size(100, 40);
            this.btnEditar.TabIndex = 2;
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += BtnEditar_Click;
            
            // 
            // btnExcluir
            // 
            this.btnExcluir.BorderRadius = 5;
            this.btnExcluir.CustomizableEdges = customizableEdges5;
            this.btnExcluir.FillColor = Color.Red;
            this.btnExcluir.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnExcluir.ForeColor = Color.White;
            this.btnExcluir.Location = new Point(520, 20);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.ShadowDecoration.CustomizableEdges = customizableEdges6;
            this.btnExcluir.Size = new Size(100, 40);
            this.btnExcluir.TabIndex = 3;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.Click += BtnExcluir_Click;
            
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.BorderRadius = 5;
            this.btnAtualizar.CustomizableEdges = customizableEdges7;
            this.btnAtualizar.FillColor = Color.Gray;
            this.btnAtualizar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAtualizar.ForeColor = Color.White;
            this.btnAtualizar.Location = new Point(630, 20);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.ShadowDecoration.CustomizableEdges = customizableEdges8;
            this.btnAtualizar.Size = new Size(100, 40);
            this.btnAtualizar.TabIndex = 4;
            this.btnAtualizar.Text = "Atualizar";
            this.btnAtualizar.Click += BtnAtualizar_Click;
            
            // 
            // gridTamanhos
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            this.gridTamanhos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridTamanhos.AllowUserToAddRows = false;
            this.gridTamanhos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridTamanhos.BackgroundColor = Color.White;
            this.gridTamanhos.BorderStyle = BorderStyle.None;
            this.gridTamanhos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridTamanhos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(40, 40, 40);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            this.gridTamanhos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridTamanhos.ColumnHeadersHeight = 30;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            this.gridTamanhos.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridTamanhos.Dock = DockStyle.Fill;
            this.gridTamanhos.EnableHeadersVisualStyles = false;
            this.gridTamanhos.GridColor = Color.FromArgb(231, 229, 255);
            this.gridTamanhos.Location = new Point(0, 80);
            this.gridTamanhos.MultiSelect = false;
            this.gridTamanhos.Name = "gridTamanhos";
            this.gridTamanhos.ReadOnly = true;
            this.gridTamanhos.RowHeadersVisible = false;
            this.gridTamanhos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridTamanhos.Size = new Size(746, 383);
            this.gridTamanhos.TabIndex = 1;
            this.gridTamanhos.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            this.gridTamanhos.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.gridTamanhos.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            this.gridTamanhos.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            this.gridTamanhos.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            this.gridTamanhos.ThemeStyle.BackColor = Color.White;
            this.gridTamanhos.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            this.gridTamanhos.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(40, 40, 40);
            this.gridTamanhos.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            this.gridTamanhos.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.gridTamanhos.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            this.gridTamanhos.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.gridTamanhos.ThemeStyle.HeaderStyle.Height = 30;
            this.gridTamanhos.ThemeStyle.ReadOnly = true;
            this.gridTamanhos.ThemeStyle.RowsStyle.BackColor = Color.White;
            this.gridTamanhos.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridTamanhos.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 10F);
            this.gridTamanhos.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            this.gridTamanhos.ThemeStyle.RowsStyle.Height = 22;
            this.gridTamanhos.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            this.gridTamanhos.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            
            // 
            // TamanhosView
            // 
            this.BackColor = Color.WhiteSmoke;
            this.ClientSize = new Size(746, 463);
            this.Controls.Add(this.gridTamanhos);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "TamanhosView";
            this.Text = "Gestão de Tamanhos";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTamanhos)).EndInit();
            this.ResumeLayout(false);
        }

        private async void BtnAtualizar_Click(object? sender, EventArgs e)
        {
            await CarregarDados();
        }

        private async Task CarregarDados()
        {
            try
            {
                var tamanhos = await _service.GetAllAsync();
                gridTamanhos.DataSource = tamanhos;
                
                // Guna grid columns auto-sizing usually handles this, but we can keep it
                if (gridTamanhos.Columns["Id"] is DataGridViewColumn colId)
                    colId.Width = 50;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar tamanhos: " + ex.Message);
            }
        }

        private async void BtnNovo_Click(object? sender, EventArgs e)
        {
            using (var form = new TamanhoForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        await _service.CreateAsync(new TamanhoCreateDto { Nome = form.NomeTamanho });
                        await CarregarDados();
                        MessageBox.Show("Tamanho criado com sucesso!");
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
            if (gridTamanhos.SelectedRows.Count == 0) return;
            var item = gridTamanhos.SelectedRows[0].DataBoundItem as TamanhoDto;
            if (item == null) return;

            using (var form = new TamanhoForm(item.Nome))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        await _service.UpdateAsync(item.Id, new TamanhoUpdateDto { Nome = form.NomeTamanho });
                        await CarregarDados();
                        MessageBox.Show("Tamanho atualizado!");
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
            if (gridTamanhos.SelectedRows.Count == 0) return;
            var item = gridTamanhos.SelectedRows[0].DataBoundItem as TamanhoDto;
            if (item == null) return;

            if (MessageBox.Show($"Deseja excluir '{item.Nome}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    await _service.DeleteAsync(item.Id);
                    await CarregarDados();
                    MessageBox.Show("Tamanho excluído!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir: " + ex.Message);
                }
            }
        }
    }
}