using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vion_Desktop.Models;
using Vion_Desktop.Services;

namespace Vion_Desktop.Views
{
    public partial class CategoriasView : Form
    {
        private readonly CategoriaService _service;
        private DataGridView gridCategorias = null!;
        private Panel panelTop = null!;
        private Label lblTitulo = null!;
        private Guna.UI2.WinForms.Guna2Button btnNovo = null!;
        private Guna.UI2.WinForms.Guna2Button btnEditar = null!;
        private Guna.UI2.WinForms.Guna2Button btnExcluir = null!;
        private Guna.UI2.WinForms.Guna2Button btnAtualizar = null!;

        public CategoriasView()
        {
            InitializeComponent();
            _service = new CategoriaService();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            gridCategorias = new DataGridView();
            panelTop = new Panel();
            lblTitulo = new Label();
            btnNovo = new Guna.UI2.WinForms.Guna2Button();
            btnEditar = new Guna.UI2.WinForms.Guna2Button();
            btnExcluir = new Guna.UI2.WinForms.Guna2Button();
            btnAtualizar = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)gridCategorias).BeginInit();
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // gridCategorias
            // 
            gridCategorias.AllowUserToAddRows = false;
            gridCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridCategorias.BackgroundColor = Color.White;
            gridCategorias.BorderStyle = BorderStyle.None;
            gridCategorias.Dock = DockStyle.Fill;
            gridCategorias.Location = new Point(0, 80);
            gridCategorias.MultiSelect = false;
            gridCategorias.Name = "gridCategorias";
            gridCategorias.ReadOnly = true;
            gridCategorias.RowHeadersVisible = false;
            gridCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridCategorias.Size = new Size(746, 350);
            gridCategorias.TabIndex = 0;
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.White;
            panelTop.Controls.Add(lblTitulo);
            panelTop.Controls.Add(btnNovo);
            panelTop.Controls.Add(btnEditar);
            panelTop.Controls.Add(btnExcluir);
            panelTop.Controls.Add(btnAtualizar);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(746, 80);
            panelTop.TabIndex = 1;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(135, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Categorias";
            // 
            // btnNovo
            // 
            btnNovo.BorderRadius = 5;
            btnNovo.CustomizableEdges = customizableEdges1;
            btnNovo.FillColor = Color.Green;
            btnNovo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNovo.ForeColor = Color.White;
            btnNovo.Location = new Point(200, 20);
            btnNovo.Name = "btnNovo";
            btnNovo.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnNovo.Size = new Size(200, 40);
            btnNovo.TabIndex = 1;
            btnNovo.Text = "Nova Categoria";
            btnNovo.Click += BtnNovo_Click;
            // 
            // btnEditar
            // 
            btnEditar.BorderRadius = 5;
            btnEditar.CustomizableEdges = customizableEdges3;
            btnEditar.FillColor = Color.Orange;
            btnEditar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEditar.ForeColor = Color.White;
            btnEditar.Location = new Point(410, 20);
            btnEditar.Name = "btnEditar";
            btnEditar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnEditar.Size = new Size(100, 40);
            btnEditar.TabIndex = 2;
            btnEditar.Text = "Editar";
            btnEditar.Click += BtnEditar_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.BorderRadius = 5;
            btnExcluir.CustomizableEdges = customizableEdges5;
            btnExcluir.FillColor = Color.Red;
            btnExcluir.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExcluir.ForeColor = Color.White;
            btnExcluir.Location = new Point(520, 20);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnExcluir.Size = new Size(100, 40);
            btnExcluir.TabIndex = 3;
            btnExcluir.Text = "Excluir";
            btnExcluir.Click += BtnExcluir_Click;
            // 
            // btnAtualizar
            // 
            btnAtualizar.BorderRadius = 5;
            btnAtualizar.CustomizableEdges = customizableEdges7;
            btnAtualizar.FillColor = Color.Gray;
            btnAtualizar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAtualizar.ForeColor = Color.White;
            btnAtualizar.Location = new Point(630, 20);
            btnAtualizar.Name = "btnAtualizar";
            btnAtualizar.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnAtualizar.Size = new Size(100, 40);
            btnAtualizar.TabIndex = 4;
            btnAtualizar.Text = "Atualizar";
            btnAtualizar.Click += BtnAtualizar_Click;
            // 
            // CategoriasView
            // 
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(746, 430);
            Controls.Add(gridCategorias);
            Controls.Add(panelTop);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CategoriasView";
            Text = "Gestão de Categorias";
            ((System.ComponentModel.ISupportInitialize)gridCategorias).EndInit();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ResumeLayout(false);
        }

        private async void BtnAtualizar_Click(object? sender, EventArgs e)
        {
            await CarregarDados();
        }

        private async Task CarregarDados()
        {
            try
            {
                var categorias = await _service.GetAllAsync();
                gridCategorias.DataSource = categorias;
                
                if (gridCategorias.Columns["Id"] != null)
                    gridCategorias.Columns["Id"]!.Width = 50;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar categorias: " + ex.Message);
            }
        }

        private async void BtnNovo_Click(object? sender, EventArgs e)
        {
            using (var form = new CategoriaForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        await _service.CreateAsync(new CategoriaCreateDto { Nome = form.NomeCategoria });
                        await CarregarDados();
                        MessageBox.Show("Categoria criada com sucesso!");
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
            if (gridCategorias.SelectedRows.Count == 0) return;
            var item = gridCategorias.SelectedRows[0].DataBoundItem as CategoriaDto;
            if (item == null) return;

            using (var form = new CategoriaForm(item.Nome))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        await _service.UpdateAsync(item.Id, new CategoriaUpdateDto { Nome = form.NomeCategoria });
                        await CarregarDados();
                        MessageBox.Show("Categoria atualizada!");
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
            if (gridCategorias.SelectedRows.Count == 0) return;
            var item = gridCategorias.SelectedRows[0].DataBoundItem as CategoriaDto;
            if (item == null) return;

            if (MessageBox.Show($"Deseja excluir '{item.Nome}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    await _service.DeleteAsync(item.Id);
                    await CarregarDados();
                    MessageBox.Show("Categoria excluída!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir: " + ex.Message);
                }
            }
        }
    }
}
