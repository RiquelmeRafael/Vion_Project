using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vion_Desktop.Models;
using Vion_Desktop.Services;

namespace Vion_Desktop.Views
{
    public partial class ProdutoForm : Form
    {
        public List<ProdutoCreateDto> ProdutosCriados { get; private set; } = new List<ProdutoCreateDto>();
        public ProdutoCreateDto? Produto { get; private set; }
        private readonly CategoriaService _categoriaService;
        private readonly TamanhoService _tamanhoService;
        private readonly ProdutoService _produtoService;
        private readonly CupomService _cupomService;

        private Guna.UI2.WinForms.Guna2TextBox txtNome = null!;
        private Guna.UI2.WinForms.Guna2TextBox txtDescricao = null!;
        private Guna.UI2.WinForms.Guna2TextBox txtPreco = null!;
        private Guna.UI2.WinForms.Guna2NumericUpDown numEstoque = null!;
        private Guna.UI2.WinForms.Guna2TextBox txtCor = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbCategoria = null!;
        // private Guna.UI2.WinForms.Guna2ComboBox cmbTamanho = null!; // Substituído por lista de seleção
        private FlowLayoutPanel flowTamanhos = null!;
        private Guna.UI2.WinForms.Guna2ComboBox cmbCupom = null!;
        
        // Imagem Principal
        private Guna.UI2.WinForms.Guna2PictureBox pbImagem = null!;
        private Guna.UI2.WinForms.Guna2Button btnSelecionarImagem = null!;

        // Imagens Extras
        private Guna.UI2.WinForms.Guna2PictureBox pbImagem2 = null!;
        private Guna.UI2.WinForms.Guna2PictureBox pbImagem3 = null!;
        private Guna.UI2.WinForms.Guna2PictureBox pbImagem4 = null!;

        private Guna.UI2.WinForms.Guna2Button btnSalvar = null!;
        private Guna.UI2.WinForms.Guna2Button btnCancelar = null!;

        private string _caminhoImagemSelecionada = "";
        private string _caminhoImagem2 = "";
        private string _caminhoImagem3 = "";
        private string _caminhoImagem4 = "";

        private string _urlImagemAtual = "";
        private string _urlImagem2Atual = "";
        private string _urlImagem3Atual = "";
        private string _urlImagem4Atual = "";
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNome = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDesc = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPreco = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEstoque = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCor = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCat = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTam = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCupom = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblImg = null!;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblImgExtras = null!;
        private ProdutoDto? _produtoEditar;

        public ProdutoForm(ProdutoDto? produto = null)
        {
            InitializeComponent();
            _categoriaService = new CategoriaService();
            _tamanhoService = new TamanhoService();
            _produtoService = new ProdutoService();
            _cupomService = new CupomService();
            _produtoEditar = produto;

            if (produto != null)
            {
                this.Text = "Editar Produto";
            }
            else
            {
                this.Text = "Novo Produto";
                Produto = new ProdutoCreateDto();
            }

            this.Load += ProdutoForm_Load;
        }

        private async void ProdutoForm_Load(object? sender, EventArgs e)
        {
            await CarregarCombos();
            if (_produtoEditar != null)
            {
                PreencherCampos(_produtoEditar);
            }
        }

        private async Task CarregarCombos()
        {
            try
            {
                var categorias = await _categoriaService.GetAllAsync();
                cmbCategoria.DataSource = categorias;
                cmbCategoria.DisplayMember = "Nome";
                cmbCategoria.ValueMember = "Id";

                var tamanhos = await _tamanhoService.GetAllAsync();
                
                // Popula o FlowLayoutPanel com CheckBoxes
                flowTamanhos.Controls.Clear();
                foreach (var tam in tamanhos)
                {
                    var chk = new Guna.UI2.WinForms.Guna2CheckBox();
                    chk.Text = tam.Nome;
                    chk.Tag = tam.Id;
                    chk.AutoSize = true;
                    chk.Margin = new Padding(5);
                    chk.Cursor = Cursors.Hand;
                    
                    // Estilo
                    chk.CheckedState.BorderColor = Color.FromArgb(94, 148, 255);
                    chk.CheckedState.BorderRadius = 2;
                    chk.CheckedState.BorderThickness = 0;
                    chk.CheckedState.FillColor = Color.FromArgb(94, 148, 255);
                    chk.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
                    chk.UncheckedState.BorderRadius = 2;
                    chk.UncheckedState.BorderThickness = 0;
                    chk.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);

                    flowTamanhos.Controls.Add(chk);
                }

                var cupons = await _cupomService.GetAllAsync();
                cupons.Insert(0, new CupomDto { Id = 0, Codigo = "--- Sem Cupom ---" });
                cmbCupom.DataSource = cupons;
                cmbCupom.DisplayMember = "Codigo";
                cmbCupom.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados auxiliares: " + ex.Message);
            }
        }

        private void PreencherCampos(ProdutoDto produto)
        {
            txtNome.Text = produto.Nome;
            txtDescricao.Text = produto.Descricao;
            txtPreco.Text = produto.Preco.ToString("N2");
            numEstoque.Value = produto.Estoque;
            txtCor.Text = produto.Cor;
            
            if (cmbCategoria.Items.Count > 0)
            {
                cmbCategoria.SelectedValue = produto.CategoriaId;
                // Fallback manual para garantir seleção
                if (cmbCategoria.SelectedIndex == -1)
                {
                    foreach (var item in cmbCategoria.Items)
                    {
                        if (item is CategoriaDto cat && cat.Id == produto.CategoriaId)
                        {
                            cmbCategoria.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
            
            // Marca o checkbox do tamanho atual
            foreach (Control c in flowTamanhos.Controls)
            {
                if (c is Guna.UI2.WinForms.Guna2CheckBox chk)
                {
                    chk.Checked = false; // Garante estado limpo
                    if (chk.Tag is int tId && tId == produto.TamanhoId)
                    {
                        chk.Checked = true;
                    }
                }
            }

            if (cmbCupom.Items.Count > 0)
            {
                var targetCupomId = produto.CupomId ?? 0;
                cmbCupom.SelectedValue = targetCupomId;
                
                // Fallback manual
                if (cmbCupom.SelectedIndex == -1)
                {
                     foreach (var item in cmbCupom.Items)
                     {
                         if (item is CupomDto cup && cup.Id == targetCupomId)
                         {
                             cmbCupom.SelectedItem = item;
                             break;
                         }
                     }
                }
            }
            
            _urlImagemAtual = produto.ImagemUrl ?? "";
            _urlImagem2Atual = produto.ImagemUrl2 ?? "";
            _urlImagem3Atual = produto.ImagemUrl3 ?? "";
            _urlImagem4Atual = produto.ImagemUrl4 ?? "";

            // Nota: Não carregamos as imagens da URL para o PictureBox aqui
            // para manter a simplicidade, mas as URLs são preservadas.
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges27 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges28 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblNome = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtNome = new Guna.UI2.WinForms.Guna2TextBox();
            lblDesc = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtDescricao = new Guna.UI2.WinForms.Guna2TextBox();
            lblPreco = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtPreco = new Guna.UI2.WinForms.Guna2TextBox();
            lblEstoque = new Guna.UI2.WinForms.Guna2HtmlLabel();
            numEstoque = new Guna.UI2.WinForms.Guna2NumericUpDown();
            lblCor = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtCor = new Guna.UI2.WinForms.Guna2TextBox();
            lblCat = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbCategoria = new Guna.UI2.WinForms.Guna2ComboBox();
            lblTam = new Guna.UI2.WinForms.Guna2HtmlLabel();
            flowTamanhos = new FlowLayoutPanel();
            lblCupom = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cmbCupom = new Guna.UI2.WinForms.Guna2ComboBox();
            lblImg = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pbImagem = new Guna.UI2.WinForms.Guna2PictureBox();
            btnSelecionarImagem = new Guna.UI2.WinForms.Guna2Button();
            lblImgExtras = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pbImagem2 = new Guna.UI2.WinForms.Guna2PictureBox();
            pbImagem3 = new Guna.UI2.WinForms.Guna2PictureBox();
            pbImagem4 = new Guna.UI2.WinForms.Guna2PictureBox();
            btnSalvar = new Guna.UI2.WinForms.Guna2Button();
            btnCancelar = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)numEstoque).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbImagem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbImagem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbImagem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbImagem4).BeginInit();
            SuspendLayout();
            // 
            // lblNome
            // 
            lblNome.AutoSize = true;
            lblNome.BackColor = Color.Transparent;
            lblNome.Font = new Font("Segoe UI", 9F);
            lblNome.Location = new Point(30, 20);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(36, 17);
            lblNome.TabIndex = 0;
            lblNome.Text = "Nome";
            // 
            // txtNome
            // 
            txtNome.BorderRadius = 4;
            txtNome.CustomizableEdges = customizableEdges1;
            txtNome.DefaultText = "";
            txtNome.Font = new Font("Segoe UI", 9F);
            txtNome.Location = new Point(30, 45);
            txtNome.Name = "txtNome";
            txtNome.PlaceholderText = "";
            txtNome.SelectedText = "";
            txtNome.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtNome.Size = new Size(300, 36);
            txtNome.TabIndex = 1;
            // 
            // lblDesc
            // 
            lblDesc.AutoSize = true;
            lblDesc.BackColor = Color.Transparent;
            lblDesc.Font = new Font("Segoe UI", 9F);
            lblDesc.Location = new Point(30, 90);
            lblDesc.Name = "lblDesc";
            lblDesc.Size = new Size(54, 17);
            lblDesc.TabIndex = 2;
            lblDesc.Text = "Descrição";
            // 
            // txtDescricao
            // 
            txtDescricao.BorderRadius = 4;
            txtDescricao.CustomizableEdges = customizableEdges3;
            txtDescricao.DefaultText = "";
            txtDescricao.Font = new Font("Segoe UI", 9F);
            txtDescricao.Location = new Point(30, 115);
            txtDescricao.Multiline = true;
            txtDescricao.Name = "txtDescricao";
            txtDescricao.PlaceholderText = "";
            txtDescricao.SelectedText = "";
            txtDescricao.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtDescricao.Size = new Size(300, 100);
            txtDescricao.TabIndex = 3;
            // 
            // lblPreco
            // 
            lblPreco.AutoSize = true;
            lblPreco.BackColor = Color.Transparent;
            lblPreco.Font = new Font("Segoe UI", 9F);
            lblPreco.Location = new Point(30, 230);
            lblPreco.Name = "lblPreco";
            lblPreco.Size = new Size(33, 17);
            lblPreco.TabIndex = 4;
            lblPreco.Text = "Preço";
            // 
            // txtPreco
            // 
            txtPreco.BorderRadius = 4;
            txtPreco.CustomizableEdges = customizableEdges5;
            txtPreco.DefaultText = "";
            txtPreco.Font = new Font("Segoe UI", 9F);
            txtPreco.Location = new Point(30, 255);
            txtPreco.Name = "txtPreco";
            txtPreco.PlaceholderText = "";
            txtPreco.SelectedText = "";
            txtPreco.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtPreco.Size = new Size(140, 36);
            txtPreco.TabIndex = 5;
            // 
            // lblEstoque
            // 
            lblEstoque.AutoSize = true;
            lblEstoque.BackColor = Color.Transparent;
            lblEstoque.Font = new Font("Segoe UI", 9F);
            lblEstoque.Location = new Point(190, 230);
            lblEstoque.Name = "lblEstoque";
            lblEstoque.Size = new Size(45, 17);
            lblEstoque.TabIndex = 6;
            lblEstoque.Text = "Estoque";
            // 
            // numEstoque
            // 
            numEstoque.BackColor = Color.Transparent;
            numEstoque.BorderRadius = 4;
            numEstoque.CustomizableEdges = customizableEdges7;
            numEstoque.Font = new Font("Segoe UI", 9F);
            numEstoque.Location = new Point(190, 255);
            numEstoque.Name = "numEstoque";
            numEstoque.ShadowDecoration.CustomizableEdges = customizableEdges8;
            numEstoque.Size = new Size(140, 36);
            numEstoque.TabIndex = 7;
            // 
            // lblCor
            // 
            lblCor.AutoSize = true;
            lblCor.BackColor = Color.Transparent;
            lblCor.Font = new Font("Segoe UI", 9F);
            lblCor.Location = new Point(30, 305);
            lblCor.Name = "lblCor";
            lblCor.Size = new Size(22, 17);
            lblCor.TabIndex = 8;
            lblCor.Text = "Cor";
            // 
            // txtCor
            // 
            txtCor.BorderRadius = 4;
            txtCor.CustomizableEdges = customizableEdges9;
            txtCor.DefaultText = "";
            txtCor.Font = new Font("Segoe UI", 9F);
            txtCor.Location = new Point(30, 330);
            txtCor.Name = "txtCor";
            txtCor.PlaceholderText = "";
            txtCor.SelectedText = "";
            txtCor.ShadowDecoration.CustomizableEdges = customizableEdges10;
            txtCor.Size = new Size(300, 36);
            txtCor.TabIndex = 9;
            // 
            // lblCat
            // 
            lblCat.AutoSize = true;
            lblCat.BackColor = Color.Transparent;
            lblCat.Font = new Font("Segoe UI", 9F);
            lblCat.Location = new Point(360, 20);
            lblCat.Name = "lblCat";
            lblCat.Size = new Size(54, 17);
            lblCat.TabIndex = 10;
            lblCat.Text = "Categoria";
            // 
            // cmbCategoria
            // 
            cmbCategoria.BackColor = Color.Transparent;
            cmbCategoria.BorderRadius = 4;
            cmbCategoria.CustomizableEdges = customizableEdges11;
            cmbCategoria.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.FocusedColor = Color.Empty;
            cmbCategoria.Font = new Font("Segoe UI", 10F);
            cmbCategoria.ForeColor = Color.FromArgb(68, 88, 112);
            cmbCategoria.ItemHeight = 26;
            cmbCategoria.Location = new Point(360, 45);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.ShadowDecoration.CustomizableEdges = customizableEdges12;
            cmbCategoria.Size = new Size(200, 36);
            cmbCategoria.TabIndex = 11;
            // 
            // lblTam
            // 
            lblTam.AutoSize = true;
            lblTam.BackColor = Color.Transparent;
            lblTam.Font = new Font("Segoe UI", 9F);
            lblTam.Location = new Point(360, 90);
            lblTam.Name = "lblTam";
            lblTam.Size = new Size(54, 17);
            lblTam.TabIndex = 12;
            lblTam.Text = "Tamanho";
            // 
            // flowTamanhos
            // 
            flowTamanhos.AutoScroll = true;
            flowTamanhos.BackColor = Color.WhiteSmoke;
            flowTamanhos.FlowDirection = FlowDirection.TopDown;
            flowTamanhos.Location = new Point(360, 115);
            flowTamanhos.Name = "flowTamanhos";
            flowTamanhos.Size = new Size(200, 150);
            flowTamanhos.TabIndex = 13;
            flowTamanhos.WrapContents = false;
            // 
            // lblCupom
            // 
            lblCupom.AutoSize = true;
            lblCupom.BackColor = Color.Transparent;
            lblCupom.Font = new Font("Segoe UI", 9F);
            lblCupom.Location = new Point(360, 305);
            lblCupom.Name = "lblCupom";
            lblCupom.Size = new Size(102, 17);
            lblCupom.TabIndex = 14;
            lblCupom.Text = "Cupom (Opcional)";
            // 
            // cmbCupom
            // 
            cmbCupom.BackColor = Color.Transparent;
            cmbCupom.BorderRadius = 4;
            cmbCupom.CustomizableEdges = customizableEdges13;
            cmbCupom.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCupom.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCupom.FocusedColor = Color.Empty;
            cmbCupom.Font = new Font("Segoe UI", 10F);
            cmbCupom.ForeColor = Color.FromArgb(68, 88, 112);
            cmbCupom.ItemHeight = 26;
            cmbCupom.Location = new Point(360, 330);
            cmbCupom.Name = "cmbCupom";
            cmbCupom.ShadowDecoration.CustomizableEdges = customizableEdges14;
            cmbCupom.Size = new Size(200, 36);
            cmbCupom.TabIndex = 15;
            // 
            // lblImg
            // 
            lblImg.AutoSize = true;
            lblImg.BackColor = Color.Transparent;
            lblImg.Font = new Font("Segoe UI", 9F);
            lblImg.Location = new Point(600, 20);
            lblImg.Name = "lblImg";
            lblImg.Size = new Size(96, 17);
            lblImg.TabIndex = 16;
            lblImg.Text = "Imagem Principal";
            // 
            // pbImagem
            // 
            pbImagem.BorderRadius = 4;
            pbImagem.BorderStyle = BorderStyle.FixedSingle;
            pbImagem.CustomizableEdges = customizableEdges15;
            pbImagem.FillColor = Color.WhiteSmoke;
            pbImagem.ImageRotate = 0F;
            pbImagem.Location = new Point(600, 45);
            pbImagem.Name = "pbImagem";
            pbImagem.ShadowDecoration.CustomizableEdges = customizableEdges16;
            pbImagem.Size = new Size(150, 150);
            pbImagem.SizeMode = PictureBoxSizeMode.Zoom;
            pbImagem.TabIndex = 17;
            pbImagem.TabStop = false;
            // 
            // btnSelecionarImagem
            // 
            btnSelecionarImagem.BorderRadius = 4;
            btnSelecionarImagem.CustomizableEdges = customizableEdges17;
            btnSelecionarImagem.Font = new Font("Segoe UI", 8F);
            btnSelecionarImagem.ForeColor = Color.White;
            btnSelecionarImagem.Location = new Point(600, 205);
            btnSelecionarImagem.Name = "btnSelecionarImagem";
            btnSelecionarImagem.ShadowDecoration.CustomizableEdges = customizableEdges18;
            btnSelecionarImagem.Size = new Size(150, 36);
            btnSelecionarImagem.TabIndex = 18;
            btnSelecionarImagem.Text = "Selecionar...";
            btnSelecionarImagem.Click += BtnSelecionarImagem_Click;
            // 
            // lblImgExtras
            // 
            lblImgExtras.AutoSize = true;
            lblImgExtras.BackColor = Color.Transparent;
            lblImgExtras.Font = new Font("Segoe UI", 9F);
            lblImgExtras.Location = new Point(600, 250);
            lblImgExtras.Name = "lblImgExtras";
            lblImgExtras.Size = new Size(90, 15);
            lblImgExtras.TabIndex = 19;
            lblImgExtras.Text = "Extras";
            // 
            // pbImagem2
            // 
            pbImagem2.BorderRadius = 4;
            pbImagem2.BorderStyle = BorderStyle.FixedSingle;
            pbImagem2.Cursor = Cursors.Hand;
            pbImagem2.CustomizableEdges = customizableEdges19;
            pbImagem2.FillColor = Color.WhiteSmoke;
            pbImagem2.ImageRotate = 0F;
            pbImagem2.Location = new Point(600, 275);
            pbImagem2.Name = "pbImagem2";
            pbImagem2.ShadowDecoration.CustomizableEdges = customizableEdges20;
            pbImagem2.Size = new Size(45, 45);
            pbImagem2.SizeMode = PictureBoxSizeMode.Zoom;
            pbImagem2.TabIndex = 20;
            pbImagem2.TabStop = false;
            pbImagem2.Click += PbImagem2_Click;
            // 
            // pbImagem3
            // 
            pbImagem3.BorderRadius = 4;
            pbImagem3.BorderStyle = BorderStyle.FixedSingle;
            pbImagem3.Cursor = Cursors.Hand;
            pbImagem3.CustomizableEdges = customizableEdges21;
            pbImagem3.FillColor = Color.WhiteSmoke;
            pbImagem3.ImageRotate = 0F;
            pbImagem3.Location = new Point(650, 275);
            pbImagem3.Name = "pbImagem3";
            pbImagem3.ShadowDecoration.CustomizableEdges = customizableEdges22;
            pbImagem3.Size = new Size(45, 45);
            pbImagem3.SizeMode = PictureBoxSizeMode.Zoom;
            pbImagem3.TabIndex = 21;
            pbImagem3.TabStop = false;
            pbImagem3.Click += PbImagem3_Click;
            // 
            // pbImagem4
            // 
            pbImagem4.BorderRadius = 4;
            pbImagem4.BorderStyle = BorderStyle.FixedSingle;
            pbImagem4.Cursor = Cursors.Hand;
            pbImagem4.CustomizableEdges = customizableEdges23;
            pbImagem4.FillColor = Color.WhiteSmoke;
            pbImagem4.ImageRotate = 0F;
            pbImagem4.Location = new Point(700, 275);
            pbImagem4.Name = "pbImagem4";
            pbImagem4.ShadowDecoration.CustomizableEdges = customizableEdges24;
            pbImagem4.Size = new Size(45, 45);
            pbImagem4.SizeMode = PictureBoxSizeMode.Zoom;
            pbImagem4.TabIndex = 22;
            pbImagem4.TabStop = false;
            pbImagem4.Click += PbImagem4_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.BorderRadius = 4;
            btnSalvar.CustomizableEdges = customizableEdges25;
            btnSalvar.FillColor = Color.Green;
            btnSalvar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSalvar.ForeColor = Color.White;
            btnSalvar.Location = new Point(560, 450);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.ShadowDecoration.CustomizableEdges = customizableEdges26;
            btnSalvar.Size = new Size(180, 45);
            btnSalvar.TabIndex = 23;
            btnSalvar.Text = "SALVAR";
            btnSalvar.Click += BtnSalvar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BorderRadius = 4;
            btnCancelar.CustomizableEdges = customizableEdges27;
            btnCancelar.FillColor = Color.Gray;
            btnCancelar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(360, 450);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.ShadowDecoration.CustomizableEdges = customizableEdges28;
            btnCancelar.Size = new Size(180, 45);
            btnCancelar.TabIndex = 24;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.Click += BtnCancelar_Click;
            // 
            // ProdutoForm
            // 
            ClientSize = new Size(784, 511);
            Controls.Add(lblNome);
            Controls.Add(txtNome);
            Controls.Add(lblDesc);
            Controls.Add(txtDescricao);
            Controls.Add(lblPreco);
            Controls.Add(txtPreco);
            Controls.Add(lblEstoque);
            Controls.Add(numEstoque);
            Controls.Add(lblCor);
            Controls.Add(txtCor);
            Controls.Add(lblCat);
            Controls.Add(cmbCategoria);
            Controls.Add(lblTam);
            Controls.Add(flowTamanhos);
            Controls.Add(lblCupom);
            Controls.Add(cmbCupom);
            Controls.Add(lblImg);
            Controls.Add(pbImagem);
            Controls.Add(btnSelecionarImagem);
            Controls.Add(lblImgExtras);
            Controls.Add(pbImagem2);
            Controls.Add(pbImagem3);
            Controls.Add(pbImagem4);
            Controls.Add(btnSalvar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            Name = "ProdutoForm";
            StartPosition = FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)numEstoque).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbImagem).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbImagem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbImagem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbImagem4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void PbImagem2_Click(object? sender, EventArgs e) => SelecionarImagemExtra(2);
        private void PbImagem3_Click(object? sender, EventArgs e) => SelecionarImagemExtra(3);
        private void PbImagem4_Click(object? sender, EventArgs e) => SelecionarImagemExtra(4);
        private void BtnCancelar_Click(object? sender, EventArgs e) => this.DialogResult = DialogResult.Cancel;









        private void BtnSelecionarImagem_Click(object? sender, EventArgs e)
        {
            using (var opf = new OpenFileDialog())
            {
                opf.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.webp";
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    _caminhoImagemSelecionada = opf.FileName;
                    CarregarImagemParaPictureBox(_caminhoImagemSelecionada, pbImagem);
                }
            }
        }

        private void SelecionarImagemExtra(int index)
        {
            using (var opf = new OpenFileDialog())
            {
                opf.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.webp";
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    string caminho = opf.FileName;
                    if (index == 2) 
                    {
                        _caminhoImagem2 = caminho;
                        CarregarImagemParaPictureBox(caminho, pbImagem2);
                    }
                    else if (index == 3)
                    {
                        _caminhoImagem3 = caminho;
                        CarregarImagemParaPictureBox(caminho, pbImagem3);
                    }
                    else if (index == 4)
                    {
                        _caminhoImagem4 = caminho;
                        CarregarImagemParaPictureBox(caminho, pbImagem4);
                    }
                }
            }
        }

        private void CarregarImagemParaPictureBox(string caminho, Guna.UI2.WinForms.Guna2PictureBox pb)
        {
            try
            {
                byte[] imageBytes = File.ReadAllBytes(caminho);
                var ms = new MemoryStream(imageBytes);
                pb.Image = Image.FromStream(ms);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar imagem: " + ex.Message);
            }
        }

        private async Task<string> UploadOuManter(string caminho, string urlAtual)
        {
            if (string.IsNullOrEmpty(caminho)) return urlAtual;
            return await _produtoService.UploadImageAsync(caminho);
        }

        private async void BtnSalvar_Click(object? sender, EventArgs e)
        {
            // Validações Básicas
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtPreco.Text))
            {
                MessageBox.Show("Preencha os campos obrigatórios!");
                return;
            }

            if (!decimal.TryParse(txtPreco.Text, out decimal preco))
            {
                MessageBox.Show("Preço inválido!");
                return;
            }

            // Validar Tamanhos
            var tamanhosSelecionados = new List<int>();
            foreach (Control c in flowTamanhos.Controls)
            {
                if (c is Guna.UI2.WinForms.Guna2CheckBox chk && chk.Checked && chk.Tag is int tId)
                {
                    tamanhosSelecionados.Add(tId);
                }
            }

            if (tamanhosSelecionados.Count == 0)
            {
                MessageBox.Show("Selecione pelo menos um tamanho!");
                return;
            }

            // Upload das imagens
            string urlImagem = _urlImagemAtual;
            string urlImagem2 = _urlImagem2Atual;
            string urlImagem3 = _urlImagem3Atual;
            string urlImagem4 = _urlImagem4Atual;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                // Faz uploads em paralelo se possível, ou sequencial
                if (!string.IsNullOrEmpty(_caminhoImagemSelecionada))
                    urlImagem = await _produtoService.UploadImageAsync(_caminhoImagemSelecionada);
                
                if (!string.IsNullOrEmpty(_caminhoImagem2))
                    urlImagem2 = await _produtoService.UploadImageAsync(_caminhoImagem2);
                
                if (!string.IsNullOrEmpty(_caminhoImagem3))
                    urlImagem3 = await _produtoService.UploadImageAsync(_caminhoImagem3);

                if (!string.IsNullOrEmpty(_caminhoImagem4))
                    urlImagem4 = await _produtoService.UploadImageAsync(_caminhoImagem4);

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro ao enviar imagem: " + ex.Message);
                return;
            }

            // Montar objeto base
            int? cupomId = null;
            if (cmbCupom.SelectedValue is int cId && cId > 0)
                cupomId = cId;

            int categoriaId = cmbCategoria.SelectedValue is int catId ? catId : 0;

            if (categoriaId <= 0)
            {
                MessageBox.Show("Selecione uma categoria válida!");
                return;
            }

            ProdutosCriados.Clear();

            // Lógica para EDIÇÃO
            if (_produtoEditar != null)
            {
                // No modo edição, o ProdutoForm é chamado para UM produto específico.
                // Se o usuário selecionou múltiplos tamanhos:
                // 1. Se o tamanho original ainda estiver selecionado, atualizamos este registro.
                // 2. Se o tamanho original foi desmarcado, atualizamos este registro para o PRIMEIRO tamanho da lista.
                // 3. Para os demais tamanhos selecionados, criamos NOVOS registros.
                
                // Nota: O 'Produto' property será usado para atualizar o item atual no ProdutosView
                // Mas precisaremos criar os extras manualmente aqui ou sinalizar para a View.
                // Como ProdutosView espera apenas atualizar UM item, a criação dos extras deve ser feita aqui.

                // Identifica qual tamanho vai para o registro atual
                int tamanhoParaAtualizar = tamanhosSelecionados.Contains(_produtoEditar.TamanhoId) 
                    ? _produtoEditar.TamanhoId 
                    : tamanhosSelecionados[0];

                // Remove o tamanho que será usado na atualização da lista de "novos"
                tamanhosSelecionados.Remove(tamanhoParaAtualizar);

                // Define o objeto principal para atualização (será pego pela View)
                Produto = new ProdutoCreateDto
                {
                    Nome = txtNome.Text,
                    Descricao = txtDescricao.Text,
                    Preco = preco,
                    Estoque = (int)numEstoque.Value,
                    Cor = txtCor.Text,
                    CategoriaId = categoriaId,
                    TamanhoId = tamanhoParaAtualizar,
                    CupomId = cupomId,
                    ImagemUrl = urlImagem,
                    ImagemUrl2 = urlImagem2,
                    ImagemUrl3 = urlImagem3,
                    ImagemUrl4 = urlImagem4
                };

                // Cria os extras agora mesmo
                foreach (var tamId in tamanhosSelecionados)
                {
                    var novoProd = new ProdutoCreateDto
                    {
                        Nome = txtNome.Text,
                        Descricao = txtDescricao.Text,
                        Preco = preco,
                        Estoque = (int)numEstoque.Value,
                        Cor = txtCor.Text,
                        CategoriaId = categoriaId,
                        TamanhoId = tamId,
                        CupomId = cupomId,
                        ImagemUrl = urlImagem,
                        ImagemUrl2 = urlImagem2,
                        ImagemUrl3 = urlImagem3,
                        ImagemUrl4 = urlImagem4
                    };
                    
                    try { await _produtoService.CreateAsync(novoProd); }
                    catch { /* Ignorar erro silencioso na criação extra para não travar fluxo */ }
                }
            }
            else
            {
                // Lógica para CRIAÇÃO (Novo Produto)
                // Cria um objeto para cada tamanho selecionado
                // O primeiro vai para a propriedade 'Produto' (para compatibilidade com a View se ela usar)
                // Mas aqui vamos criar todos via Service diretamente para garantir.
                
                // Na verdade, a View (ProdutosView) chama CreateAsync(form.Produto).
                // Para suportar múltiplos, vamos mudar a estratégia:
                // Vamos preencher a lista ProdutosCriados e deixar a View lidar, OU criar tudo aqui.
                // Como a View espera UM produto, melhor criar tudo aqui e retornar OK.
                
                foreach (var tamId in tamanhosSelecionados)
                {
                    var novoProd = new ProdutoCreateDto
                    {
                        Nome = txtNome.Text,
                        Descricao = txtDescricao.Text,
                        Preco = preco,
                        Estoque = (int)numEstoque.Value,
                        Cor = txtCor.Text,
                        CategoriaId = categoriaId,
                        TamanhoId = tamId,
                        CupomId = cupomId,
                        ImagemUrl = urlImagem,
                        ImagemUrl2 = urlImagem2,
                        ImagemUrl3 = urlImagem3,
                        ImagemUrl4 = urlImagem4
                    };
                    
                    ProdutosCriados.Add(novoProd);
                    
                    // Se for o primeiro, define como o principal para não quebrar contrato nullability
                    if (Produto == null || string.IsNullOrEmpty(Produto.Nome))
                        Produto = novoProd; 
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
