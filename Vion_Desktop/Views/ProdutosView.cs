using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vion_Desktop.Models;
using Vion_Desktop.Services;
using Guna.UI2.WinForms;

namespace Vion_Desktop.Views
{
    public partial class ProdutosView : Form
    {
        private readonly ProdutoService _service;
        private readonly CategoriaService _categoriaService;
        private const string WebBaseUrl = "http://10.136.46.31:5200";
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly Dictionary<string, Image?> _imageCache = new();
        
        private DataGridView gridProdutos = null!;
        private Guna2TextBox txtBusca = null!;
        private Guna2ComboBox cmbFiltroCategoria = null!;
        
        private Guna2Button btnNovo = null!;
        private Guna2Button btnEditar = null!;
        private Guna2Button btnExcluir = null!;
        private Guna2Button btnFiltrar = null!;
        private Guna2Button btnLimpar = null!;

        // Designer controls
        private Panel panelTop = null!;
        private Label lblTitulo = null!;
        private Panel panelGrid = null!;

        public ProdutosView()
        {
            InitializeComponent();
            _service = new ProdutoService();
            _categoriaService = new CategoriaService();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.DesignMode) return;
            await CarregarCategorias();
            await CarregarDados();
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            gridProdutos = new DataGridView();
            txtBusca = new Guna2TextBox();
            cmbFiltroCategoria = new Guna2ComboBox();
            btnNovo = new Guna2Button();
            btnEditar = new Guna2Button();
            btnExcluir = new Guna2Button();
            btnFiltrar = new Guna2Button();
            btnLimpar = new Guna2Button();
            panelTop = new Panel();
            lblTitulo = new Label();
            panelGrid = new Panel();
            ((System.ComponentModel.ISupportInitialize)gridProdutos).BeginInit();
            panelTop.SuspendLayout();
            panelGrid.SuspendLayout();
            SuspendLayout();
            // 
            // gridProdutos
            // 
            gridProdutos.AllowUserToAddRows = false;
            gridProdutos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridProdutos.BackgroundColor = Color.FromArgb(245, 246, 250);
            gridProdutos.BorderStyle = BorderStyle.None;
            gridProdutos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(45, 52, 54);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(45, 52, 54);
            gridProdutos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            gridProdutos.ColumnHeadersHeight = 40;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(116, 185, 255);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            gridProdutos.DefaultCellStyle = dataGridViewCellStyle2;
            gridProdutos.Dock = DockStyle.Fill;
            gridProdutos.EnableHeadersVisualStyles = false;
            gridProdutos.GridColor = Color.FromArgb(230, 230, 230);
            gridProdutos.Location = new Point(20, 20);
            gridProdutos.MultiSelect = false;
            gridProdutos.Name = "gridProdutos";
            gridProdutos.ReadOnly = true;
            gridProdutos.RowHeadersVisible = false;
            gridProdutos.RowTemplate.Height = 40;
            gridProdutos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridProdutos.Size = new Size(935, 298);
            gridProdutos.TabIndex = 0;
            // 
            // txtBusca
            // 
            txtBusca.BorderColor = Color.FromArgb(200, 200, 200);
            txtBusca.BorderRadius = 5;
            txtBusca.CustomizableEdges = customizableEdges1;
            txtBusca.DefaultText = "";
            txtBusca.Font = new Font("Segoe UI", 9F);
            txtBusca.Location = new Point(20, 60);
            txtBusca.Name = "txtBusca";
            txtBusca.PlaceholderText = "Buscar por nome...";
            txtBusca.SelectedText = "";
            txtBusca.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtBusca.Size = new Size(300, 40);
            txtBusca.TabIndex = 1;
            txtBusca.KeyDown += TxtBusca_KeyDown;
            // 
            // cmbFiltroCategoria
            // 
            cmbFiltroCategoria.BackColor = Color.Transparent;
            cmbFiltroCategoria.BorderRadius = 5;
            cmbFiltroCategoria.CustomizableEdges = customizableEdges3;
            cmbFiltroCategoria.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFiltroCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroCategoria.FocusedColor = Color.Empty;
            cmbFiltroCategoria.Font = new Font("Segoe UI", 10F);
            cmbFiltroCategoria.ForeColor = Color.FromArgb(68, 88, 112);
            cmbFiltroCategoria.ItemHeight = 30;
            cmbFiltroCategoria.Location = new Point(340, 60);
            cmbFiltroCategoria.Name = "cmbFiltroCategoria";
            cmbFiltroCategoria.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cmbFiltroCategoria.Size = new Size(200, 36);
            cmbFiltroCategoria.TabIndex = 2;
            // 
            // btnNovo
            // 
            btnNovo.BorderRadius = 5;
            btnNovo.CustomizableEdges = customizableEdges5;
            btnNovo.FillColor = Color.Green;
            btnNovo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNovo.ForeColor = Color.White;
            btnNovo.Location = new Point(20, 120);
            btnNovo.Name = "btnNovo";
            btnNovo.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnNovo.Size = new Size(160, 40);
            btnNovo.TabIndex = 5;
            btnNovo.Text = "Novo Produto";
            btnNovo.Click += BtnNovo_Click;
            // 
            // btnEditar
            // 
            btnEditar.BorderRadius = 5;
            btnEditar.CustomizableEdges = customizableEdges7;
            btnEditar.FillColor = Color.Orange;
            btnEditar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEditar.ForeColor = Color.Black;
            btnEditar.Location = new Point(200, 120);
            btnEditar.Name = "btnEditar";
            btnEditar.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnEditar.Size = new Size(100, 40);
            btnEditar.TabIndex = 6;
            btnEditar.Text = "Editar";
            btnEditar.Click += BtnEditar_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.BorderRadius = 5;
            btnExcluir.CustomizableEdges = customizableEdges9;
            btnExcluir.FillColor = Color.Red;
            btnExcluir.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExcluir.ForeColor = Color.White;
            btnExcluir.Location = new Point(320, 120);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnExcluir.Size = new Size(100, 40);
            btnExcluir.TabIndex = 7;
            btnExcluir.Text = "Excluir";
            btnExcluir.Click += BtnExcluir_Click;
            // 
            // btnFiltrar
            // 
            btnFiltrar.BorderRadius = 5;
            btnFiltrar.CustomizableEdges = customizableEdges11;
            btnFiltrar.FillColor = Color.FromArgb(0, 150, 255);
            btnFiltrar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnFiltrar.ForeColor = Color.White;
            btnFiltrar.Location = new Point(560, 60);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnFiltrar.Size = new Size(100, 40);
            btnFiltrar.TabIndex = 3;
            btnFiltrar.Text = "Filtrar";
            btnFiltrar.Click += BtnFiltrar_Click;
            // 
            // btnLimpar
            // 
            btnLimpar.BorderColor = Color.Gray;
            btnLimpar.BorderRadius = 5;
            btnLimpar.BorderThickness = 1;
            btnLimpar.CustomizableEdges = customizableEdges13;
            btnLimpar.FillColor = Color.Transparent;
            btnLimpar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLimpar.ForeColor = Color.Gray;
            btnLimpar.Location = new Point(670, 60);
            btnLimpar.Name = "btnLimpar";
            btnLimpar.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnLimpar.Size = new Size(100, 40);
            btnLimpar.TabIndex = 4;
            btnLimpar.Text = "Limpar";
            btnLimpar.Click += BtnLimpar_Click;
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.White;
            panelTop.Controls.Add(lblTitulo);
            panelTop.Controls.Add(txtBusca);
            panelTop.Controls.Add(cmbFiltroCategoria);
            panelTop.Controls.Add(btnFiltrar);
            panelTop.Controls.Add(btnLimpar);
            panelTop.Controls.Add(btnNovo);
            panelTop.Controls.Add(btnEditar);
            panelTop.Controls.Add(btnExcluir);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Padding = new Padding(20);
            panelTop.Size = new Size(975, 180);
            panelTop.TabIndex = 1;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(45, 52, 54);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(119, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Produtos";
            // 
            // panelGrid
            // 
            panelGrid.Controls.Add(gridProdutos);
            panelGrid.Dock = DockStyle.Fill;
            panelGrid.Location = new Point(0, 180);
            panelGrid.Name = "panelGrid";
            panelGrid.Padding = new Padding(20);
            panelGrid.Size = new Size(975, 338);
            panelGrid.TabIndex = 0;
            // 
            // ProdutosView
            // 
            BackColor = Color.FromArgb(245, 246, 250);
            ClientSize = new Size(975, 518);
            Controls.Add(panelGrid);
            Controls.Add(panelTop);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ProdutosView";
            Text = "Gestão de Produtos";
            ((System.ComponentModel.ISupportInitialize)gridProdutos).EndInit();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelGrid.ResumeLayout(false);
            ResumeLayout(false);
        }

        private async void TxtBusca_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) await CarregarDados();
        }

        private async void BtnFiltrar_Click(object? sender, EventArgs e)
        {
            await CarregarDados();
        }

        private async Task CarregarCategorias()
        {
            try
            {
                var categorias = await _categoriaService.GetAllAsync();
                categorias.Insert(0, new CategoriaDto { Id = 0, Nome = "Todas as Categorias" });
                
                cmbFiltroCategoria.DataSource = categorias;
                cmbFiltroCategoria.DisplayMember = "Nome";
                cmbFiltroCategoria.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar categorias: " + ex.Message);
            }
        }

        private async void BtnLimpar_Click(object? sender, EventArgs e)
        {
            txtBusca.Text = "";
            cmbFiltroCategoria.SelectedIndex = 0;
            await CarregarDados();
        }

        private async Task CarregarDados()
        {
            try
            {
                // Get Filter Values
                string busca = txtBusca.Text.Trim();
                int? catId = null;
                
                if (cmbFiltroCategoria.SelectedValue is int id && id > 0)
                {
                    catId = id;
                }

                var produtos = await _service.GetAllAsync(busca, catId);
                gridProdutos.DataSource = produtos;
                
                ConfigurarColunas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produtos: " + ex.Message);
            }
        }

        private void ConfigurarColunas()
        {
            string[] hiddenCols = { "ImagemUrl", "ImagemUrl2", "ImagemUrl3", "ImagemUrl4", "Descricao", "CategoriaId", "TamanhoId", "CupomId" };
            
            foreach (var col in hiddenCols)
            {
                if (gridProdutos.Columns[col] is DataGridViewColumn c) c.Visible = false;
            }

            if (gridProdutos.Columns["Id"] is DataGridViewColumn cId)
            {
                cId.HeaderText = "Código";
                cId.DisplayIndex = 0;
                cId.Width = 80;
            }

            if (!gridProdutos.Columns.Contains("Imagem"))
            {
                var imgCol = new DataGridViewImageColumn
                {
                    Name = "Imagem",
                    HeaderText = "Imagem",
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    Width = 80
                };
                gridProdutos.Columns.Insert(1, imgCol);
            }

            foreach (DataGridViewRow row in gridProdutos.Rows)
            {
                if (row.DataBoundItem is ProdutoDto produto)
                {
                    Image? img = null;
                    if (!string.IsNullOrWhiteSpace(produto.ImagemUrl))
                    {
                        try
                        {
                            var url = produto.ImagemUrl.Trim();

                            if (!url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                            {
                                if (url.StartsWith("~"))
                                {
                                    url = url.Substring(1);
                                }

                                if (!url.StartsWith("/"))
                                {
                                    url = "/" + url;
                                }

                                url = WebBaseUrl + url;
                            }

                            if (_imageCache.TryGetValue(url, out var cached))
                            {
                                img = cached;
                            }
                            else
                            {
                                var bytes = _httpClient.GetByteArrayAsync(url).Result;
                                using var ms = new MemoryStream(bytes);
                                img = Image.FromStream(ms);
                                _imageCache[url] = img;
                            }
                        }
                        catch
                        {
                            img = null;
                        }
                    }
                    row.Cells["Imagem"].Value = img;
                }
            }

            if (gridProdutos.Columns["ValorFreteFixo"] is DataGridViewColumn cFrete) cFrete.HeaderText = "Frete Fixo";
            if (gridProdutos.Columns["CupomCodigo"] is DataGridViewColumn cCupom) cCupom.HeaderText = "Cupom";

            if (gridProdutos.Columns["Preco"] is DataGridViewColumn cPreco) cPreco.DefaultCellStyle.Format = "C2";
            if (gridProdutos.Columns["ValorFreteFixo"] is DataGridViewColumn cFrete2) cFrete2.DefaultCellStyle.Format = "C2";
        }

        private async void BtnNovo_Click(object? sender, EventArgs e)
        {
            using (var form = new ProdutoForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Se o form criou múltiplos produtos na lista ProdutosCriados, usamos ela.
                        // Caso contrário, usamos form.Produto (legado/single).
                        
                        if (form.ProdutosCriados != null && form.ProdutosCriados.Count > 0)
                        {
                            foreach (var prod in form.ProdutosCriados)
                            {
                                await _service.CreateAsync(prod);
                            }
                        }
                        else if (form.Produto != null)
                        {
                             await _service.CreateAsync(form.Produto);
                        }

                        await CarregarDados();
                        MessageBox.Show("Produto(s) criado(s) com sucesso!");
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
            if (gridProdutos.SelectedRows.Count == 0) return;
            var item = gridProdutos.SelectedRows[0].DataBoundItem as ProdutoDto;
            if (item == null) return;

            using (var form = new ProdutoForm(item))
            {
                if (form.ShowDialog() == DialogResult.OK && form.Produto != null)
                {
                    try
                    {
                        var updateDto = new ProdutoUpdateDto
                        {
                            Nome = form.Produto.Nome,
                            Descricao = form.Produto.Descricao,
                            Preco = form.Produto.Preco,
                            CategoriaId = form.Produto.CategoriaId,
                            TamanhoId = form.Produto.TamanhoId,
                            Cor = form.Produto.Cor,
                            Estoque = form.Produto.Estoque,
                            ImagemUrl = form.Produto.ImagemUrl,
                            ImagemUrl2 = form.Produto.ImagemUrl2,
                            ImagemUrl3 = form.Produto.ImagemUrl3,
                            ImagemUrl4 = form.Produto.ImagemUrl4,
                            CupomId = form.Produto.CupomId
                        };
                        
                        await _service.UpdateAsync(item.Id, updateDto);
                        await CarregarDados();
                        MessageBox.Show("Produto atualizado!");
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
            if (gridProdutos.SelectedRows.Count == 0) return;
            var item = gridProdutos.SelectedRows[0].DataBoundItem as ProdutoDto;
            if (item == null) return;

            if (MessageBox.Show($"Deseja excluir '{item.Nome}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    await _service.DeleteAsync(item.Id);
                    await CarregarDados();
                    MessageBox.Show("Produto excluído!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir: " + ex.Message);
                }
            }
        }
    }
}
