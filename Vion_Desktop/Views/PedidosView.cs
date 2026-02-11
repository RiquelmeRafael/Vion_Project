using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;
using Vion_Desktop.Models;
using Vion_Desktop.Services;

namespace Vion_Desktop.Views
{
    public partial class PedidosView : Form
    {
        private Guna2DataGridView grid = null!;
        private Panel panelTop = null!;
        private Label lblTitle = null!;
        private Guna2Button btnAtualizarStatus = null!;
        private Guna2Button btnRefresh = null!;
        private PedidoService _service;

        public PedidosView()
        {
            _service = new PedidoService();
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _ = CarregarDados();
        }

        private async void BtnRefresh_Click(object? sender, EventArgs e)
        {
            await CarregarDados();
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
            grid = new Guna2DataGridView();
            panelTop = new Panel();
            lblTitle = new Label();
            btnAtualizarStatus = new Guna2Button();
            btnRefresh = new Guna2Button();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // grid
            // 
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(40, 40, 40);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
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
            grid.Location = new Point(0, 60);
            grid.MultiSelect = false;
            grid.Name = "grid";
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.Size = new Size(747, 426);
            grid.TabIndex = 0;
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
            grid.ThemeStyle.HeaderStyle.Height = 23;
            grid.ThemeStyle.ReadOnly = true;
            grid.ThemeStyle.RowsStyle.BackColor = Color.White;
            grid.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 10F);
            grid.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            grid.ThemeStyle.RowsStyle.Height = 25;
            grid.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            grid.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.White;
            panelTop.Controls.Add(lblTitle);
            panelTop.Controls.Add(btnAtualizarStatus);
            panelTop.Controls.Add(btnRefresh);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(747, 60);
            panelTop.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(291, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Gerenciamento de Pedidos";
            // 
            // btnAtualizarStatus
            // 
            btnAtualizarStatus.BorderRadius = 4;
            btnAtualizarStatus.CustomizableEdges = customizableEdges1;
            btnAtualizarStatus.FillColor = Color.FromArgb(40, 40, 40);
            btnAtualizarStatus.Font = new Font("Segoe UI", 9F);
            btnAtualizarStatus.ForeColor = Color.White;
            btnAtualizarStatus.Location = new Point(300, 15);
            btnAtualizarStatus.Name = "btnAtualizarStatus";
            btnAtualizarStatus.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnAtualizarStatus.Size = new Size(160, 35);
            btnAtualizarStatus.TabIndex = 1;
            btnAtualizarStatus.Text = "ALTERAR STATUS";
            btnAtualizarStatus.Click += BtnAtualizarStatus_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BorderRadius = 4;
            btnRefresh.CustomizableEdges = customizableEdges3;
            btnRefresh.FillColor = Color.Gray;
            btnRefresh.Font = new Font("Segoe UI", 9F);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(480, 15);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnRefresh.Size = new Size(120, 35);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "ATUALIZAR";
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // PedidosView
            // 
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(747, 486);
            Controls.Add(grid);
            Controls.Add(panelTop);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PedidosView";
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ResumeLayout(false);
        }

        private async Task CarregarDados()
        {
            try
            {
                var pedidos = await _service.GetAllAsync();
                
                // Hack para exibir nome do cliente (nested object) no grid
                var listaFormatada = pedidos.Select(p => new 
                {
                    p.Id,
                    p.DataPedido,
                    ClienteNome = p.Usuario?.Nome ?? "N/A",
                    p.Total,
                    p.Status,
                    p.FormaPagamento
                }).ToList();

                grid.DataSource = listaFormatada;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar pedidos: " + ex.Message);
            }
        }

        private async void BtnAtualizarStatus_Click(object? sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um pedido.");
                return;
            }

            var item = grid.SelectedRows[0].DataBoundItem;
            if (item == null) return;

            // Usando reflection simples para pegar as props do tipo anônimo
            var idProp = item.GetType().GetProperty("Id");
            var statusProp = item.GetType().GetProperty("Status");

            if (idProp != null)
            {
                var idVal = idProp.GetValue(item);
                if (idVal == null) return;
                int id = (int)idVal;
                
                string statusAtual = statusProp?.GetValue(item)?.ToString() ?? "";

                using (var form = new PedidoStatusForm(statusAtual))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            await _service.UpdateStatusAsync(id, form.NovoStatus!);
                            await CarregarDados();
                            MessageBox.Show("Status atualizado!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erro ao atualizar: " + ex.Message);
                        }
                    }
                }
            }
        }
    }
}
