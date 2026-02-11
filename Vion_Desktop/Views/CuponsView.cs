using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vion_Desktop.Models;
using Vion_Desktop.Services;

namespace Vion_Desktop.Views
{
    public partial class CuponsView : Form
    {
        private readonly CupomService _service;
        private DataGridView gridCupons = null!;
        private Panel panelTop = null!;
        private Label lblTitulo = null!;
        private Guna.UI2.WinForms.Guna2Button btnNovo = null!;
        private Guna.UI2.WinForms.Guna2Button btnEditar = null!;
        private Guna.UI2.WinForms.Guna2Button btnExcluir = null!;
        private Guna.UI2.WinForms.Guna2Button btnAtualizar = null!;

        public CuponsView()
        {
            InitializeComponent();
            _service = new CupomService();
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
            gridCupons = new DataGridView();
            panelTop = new Panel();
            lblTitulo = new Label();
            btnNovo = new Guna.UI2.WinForms.Guna2Button();
            btnEditar = new Guna.UI2.WinForms.Guna2Button();
            btnExcluir = new Guna.UI2.WinForms.Guna2Button();
            btnAtualizar = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)gridCupons).BeginInit();
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // gridCupons
            // 
            gridCupons.AllowUserToAddRows = false;
            gridCupons.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridCupons.BackgroundColor = Color.White;
            gridCupons.BorderStyle = BorderStyle.None;
            gridCupons.Dock = DockStyle.Fill;
            gridCupons.Location = new Point(0, 80);
            gridCupons.MultiSelect = false;
            gridCupons.Name = "gridCupons";
            gridCupons.ReadOnly = true;
            gridCupons.RowHeadersVisible = false;
            gridCupons.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridCupons.Size = new Size(742, 343);
            gridCupons.TabIndex = 0;
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
            panelTop.Size = new Size(742, 80);
            panelTop.TabIndex = 1;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(100, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Cupons";
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
            btnNovo.Size = new Size(150, 40);
            btnNovo.TabIndex = 1;
            btnNovo.Text = "Novo Cupom";
            btnNovo.Click += BtnNovo_Click;
            // 
            // btnEditar
            // 
            btnEditar.BorderRadius = 5;
            btnEditar.CustomizableEdges = customizableEdges3;
            btnEditar.FillColor = Color.Orange;
            btnEditar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEditar.ForeColor = Color.White;
            btnEditar.Location = new Point(370, 20);
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
            btnExcluir.Location = new Point(490, 20);
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
            btnAtualizar.Location = new Point(610, 20);
            btnAtualizar.Name = "btnAtualizar";
            btnAtualizar.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnAtualizar.Size = new Size(100, 40);
            btnAtualizar.TabIndex = 4;
            btnAtualizar.Text = "Atualizar";
            btnAtualizar.Click += BtnAtualizar_Click;
            // 
            // CuponsView
            // 
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(742, 423);
            Controls.Add(gridCupons);
            Controls.Add(panelTop);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CuponsView";
            Text = "Gestão de Cupons";
            ((System.ComponentModel.ISupportInitialize)gridCupons).EndInit();
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
                var dados = await _service.GetAllAsync();
                gridCupons.DataSource = dados;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar: " + ex.Message);
            }
        }

        private async void BtnNovo_Click(object? sender, EventArgs e)
        {
            using (var form = new CupomForm())
            {
                if (form.ShowDialog() == DialogResult.OK && form.Cupom != null)
                {
                    try
                    {
                        await _service.CreateAsync(form.Cupom);
                        await CarregarDados();
                        MessageBox.Show("Cupom criado!");
                    }
                    catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
                }
            }
        }

        private async void BtnEditar_Click(object? sender, EventArgs e)
        {
            if (gridCupons.SelectedRows.Count == 0) return;
            var item = gridCupons.SelectedRows[0].DataBoundItem as CupomDto;
            if (item == null) return;

            using (var form = new CupomForm(item))
            {
                if (form.ShowDialog() == DialogResult.OK && form.Cupom != null)
                {
                    try
                    {
                        await _service.UpdateAsync(item.Id, form.Cupom);
                        await CarregarDados();
                        MessageBox.Show("Cupom atualizado!");
                    }
                    catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
                }
            }
        }

        private async void BtnExcluir_Click(object? sender, EventArgs e)
        {
            if (gridCupons.SelectedRows.Count == 0) return;
            var item = gridCupons.SelectedRows[0].DataBoundItem as CupomDto;
            if (item == null) return;

            if (MessageBox.Show($"Excluir cupom '{item.Codigo}'?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    await _service.DeleteAsync(item.Id);
                    await CarregarDados();
                }
                catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
            }
        }
    }
}
