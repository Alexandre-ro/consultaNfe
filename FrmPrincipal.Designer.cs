namespace ProtipoConsultaWebService
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.btnSelecionarCertificado = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.cmbListarCertificados = new MetroFramework.Controls.MetroComboBox();
            this.lblCertificadoSelecionado = new System.Windows.Forms.Label();
            this.btnBuscarNfe = new MetroFramework.Controls.MetroButton();
            this.btnVerDetalhesCertificado = new MetroFramework.Controls.MetroButton();
            this.dataGridNfe = new System.Windows.Forms.DataGridView();
            this.ClXmlNfe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CheckNfe = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnDanfe = new System.Windows.Forms.DataGridViewButtonColumn();
            this.BtnDownloadXml = new System.Windows.Forms.DataGridViewButtonColumn();
            this.clNumero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clEmpresa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClValor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClEmissao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClManifesto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClChaveAcesso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClCnpj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClIe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClModelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClUf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbManifestacao = new MetroFramework.Controls.MetroComboBox();
            this.lblManifesto = new MetroFramework.Controls.MetroLabel();
            this.btnManifestar = new MetroFramework.Controls.MetroButton();
            this.BtnMarcarTodos = new MetroFramework.Controls.MetroButton();
            this.BtnDesmarcarTodos = new MetroFramework.Controls.MetroButton();
            this.lblNumeroDeNotas = new System.Windows.Forms.Label();
            this.folderBrowserSave = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridNfe)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelecionarCertificado
            // 
            resources.ApplyResources(this.btnSelecionarCertificado, "btnSelecionarCertificado");
            this.btnSelecionarCertificado.Name = "btnSelecionarCertificado";
            this.btnSelecionarCertificado.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnSelecionarCertificado.Click += new System.EventHandler(this.btnSelecionarCertificado_Click);
            // 
            // metroLabel1
            // 
            resources.ApplyResources(this.metroLabel1, "metroLabel1");
            this.metroLabel1.Name = "metroLabel1";
            // 
            // cmbListarCertificados
            // 
            this.cmbListarCertificados.FontSize = MetroFramework.MetroLinkSize.Small;
            this.cmbListarCertificados.FormattingEnabled = true;
            resources.ApplyResources(this.cmbListarCertificados, "cmbListarCertificados");
            this.cmbListarCertificados.Name = "cmbListarCertificados";
            this.cmbListarCertificados.SelectedIndexChanged += new System.EventHandler(this.cmbListarCertificados_SelectedIndexChanged);
            // 
            // lblCertificadoSelecionado
            // 
            resources.ApplyResources(this.lblCertificadoSelecionado, "lblCertificadoSelecionado");
            this.lblCertificadoSelecionado.Name = "lblCertificadoSelecionado";
            // 
            // btnBuscarNfe
            // 
            resources.ApplyResources(this.btnBuscarNfe, "btnBuscarNfe");
            this.btnBuscarNfe.Name = "btnBuscarNfe";
            this.btnBuscarNfe.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnBuscarNfe.Click += new System.EventHandler(this.btnBuscarNfe_Click);
            // 
            // btnVerDetalhesCertificado
            // 
            resources.ApplyResources(this.btnVerDetalhesCertificado, "btnVerDetalhesCertificado");
            this.btnVerDetalhesCertificado.Name = "btnVerDetalhesCertificado";
            this.btnVerDetalhesCertificado.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnVerDetalhesCertificado.Click += new System.EventHandler(this.btnVerDetalhesCertificado_Click);
            // 
            // dataGridNfe
            // 
            this.dataGridNfe.AllowUserToAddRows = false;
            this.dataGridNfe.AllowUserToDeleteRows = false;
            this.dataGridNfe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridNfe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ClXmlNfe,
            this.CheckNfe,
            this.btnDanfe,
            this.BtnDownloadXml,
            this.clNumero,
            this.clEmpresa,
            this.ClValor,
            this.ClEmissao,
            this.ClStatus,
            this.ClManifesto,
            this.ClChaveAcesso,
            this.ClCnpj,
            this.ClIe,
            this.ClTipo,
            this.ClModelo,
            this.ClUf});
            resources.ApplyResources(this.dataGridNfe, "dataGridNfe");
            this.dataGridNfe.MultiSelect = false;
            this.dataGridNfe.Name = "dataGridNfe";
            this.dataGridNfe.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridNfe_CellContentClick);
            // 
            // ClXmlNfe
            // 
            resources.ApplyResources(this.ClXmlNfe, "ClXmlNfe");
            this.ClXmlNfe.Name = "ClXmlNfe";
            this.ClXmlNfe.ReadOnly = true;
            this.ClXmlNfe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // CheckNfe
            // 
            this.CheckNfe.FalseValue = "0";
            resources.ApplyResources(this.CheckNfe, "CheckNfe");
            this.CheckNfe.Name = "CheckNfe";
            this.CheckNfe.ReadOnly = true;
            this.CheckNfe.TrueValue = "1";
            // 
            // btnDanfe
            // 
            resources.ApplyResources(this.btnDanfe, "btnDanfe");
            this.btnDanfe.Name = "btnDanfe";
            this.btnDanfe.ReadOnly = true;
            this.btnDanfe.Text = "Gerar Danfe";
            this.btnDanfe.UseColumnTextForButtonValue = true;
            // 
            // BtnDownloadXml
            // 
            resources.ApplyResources(this.BtnDownloadXml, "BtnDownloadXml");
            this.BtnDownloadXml.Name = "BtnDownloadXml";
            this.BtnDownloadXml.ReadOnly = true;
            this.BtnDownloadXml.Text = "Baixar XML";
            this.BtnDownloadXml.UseColumnTextForButtonValue = true;
            // 
            // clNumero
            // 
            resources.ApplyResources(this.clNumero, "clNumero");
            this.clNumero.Name = "clNumero";
            this.clNumero.ReadOnly = true;
            // 
            // clEmpresa
            // 
            resources.ApplyResources(this.clEmpresa, "clEmpresa");
            this.clEmpresa.Name = "clEmpresa";
            this.clEmpresa.ReadOnly = true;
            // 
            // ClValor
            // 
            resources.ApplyResources(this.ClValor, "ClValor");
            this.ClValor.Name = "ClValor";
            this.ClValor.ReadOnly = true;
            // 
            // ClEmissao
            // 
            resources.ApplyResources(this.ClEmissao, "ClEmissao");
            this.ClEmissao.Name = "ClEmissao";
            this.ClEmissao.ReadOnly = true;
            // 
            // ClStatus
            // 
            resources.ApplyResources(this.ClStatus, "ClStatus");
            this.ClStatus.Name = "ClStatus";
            this.ClStatus.ReadOnly = true;
            // 
            // ClManifesto
            // 
            resources.ApplyResources(this.ClManifesto, "ClManifesto");
            this.ClManifesto.Name = "ClManifesto";
            this.ClManifesto.ReadOnly = true;
            // 
            // ClChaveAcesso
            // 
            resources.ApplyResources(this.ClChaveAcesso, "ClChaveAcesso");
            this.ClChaveAcesso.Name = "ClChaveAcesso";
            this.ClChaveAcesso.ReadOnly = true;
            // 
            // ClCnpj
            // 
            resources.ApplyResources(this.ClCnpj, "ClCnpj");
            this.ClCnpj.Name = "ClCnpj";
            this.ClCnpj.ReadOnly = true;
            // 
            // ClIe
            // 
            resources.ApplyResources(this.ClIe, "ClIe");
            this.ClIe.Name = "ClIe";
            this.ClIe.ReadOnly = true;
            // 
            // ClTipo
            // 
            resources.ApplyResources(this.ClTipo, "ClTipo");
            this.ClTipo.Name = "ClTipo";
            this.ClTipo.ReadOnly = true;
            // 
            // ClModelo
            // 
            resources.ApplyResources(this.ClModelo, "ClModelo");
            this.ClModelo.Name = "ClModelo";
            this.ClModelo.ReadOnly = true;
            // 
            // ClUf
            // 
            resources.ApplyResources(this.ClUf, "ClUf");
            this.ClUf.Name = "ClUf";
            this.ClUf.ReadOnly = true;
            // 
            // cmbManifestacao
            // 
            resources.ApplyResources(this.cmbManifestacao, "cmbManifestacao");
            this.cmbManifestacao.FontSize = MetroFramework.MetroLinkSize.Small;
            this.cmbManifestacao.FormattingEnabled = true;
            this.cmbManifestacao.Name = "cmbManifestacao";
            this.cmbManifestacao.SelectedIndexChanged += new System.EventHandler(this.cmbListaManifesto_SelectedIndexChanged);
            // 
            // lblManifesto
            // 
            resources.ApplyResources(this.lblManifesto, "lblManifesto");
            this.lblManifesto.Name = "lblManifesto";
            // 
            // btnManifestar
            // 
            resources.ApplyResources(this.btnManifestar, "btnManifestar");
            this.btnManifestar.Name = "btnManifestar";
            this.btnManifestar.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnManifestar.Click += new System.EventHandler(this.btnManifestar_Click);
            // 
            // BtnMarcarTodos
            // 
            resources.ApplyResources(this.BtnMarcarTodos, "BtnMarcarTodos");
            this.BtnMarcarTodos.Name = "BtnMarcarTodos";
            this.BtnMarcarTodos.Theme = MetroFramework.MetroThemeStyle.Light;
            this.BtnMarcarTodos.Click += new System.EventHandler(this.BtnMarcarTodos_Click);
            // 
            // BtnDesmarcarTodos
            // 
            resources.ApplyResources(this.BtnDesmarcarTodos, "BtnDesmarcarTodos");
            this.BtnDesmarcarTodos.Name = "BtnDesmarcarTodos";
            this.BtnDesmarcarTodos.Theme = MetroFramework.MetroThemeStyle.Light;
            this.BtnDesmarcarTodos.Click += new System.EventHandler(this.BtnDesmarcarTodos_Click);
            // 
            // lblNumeroDeNotas
            // 
            resources.ApplyResources(this.lblNumeroDeNotas, "lblNumeroDeNotas");
            this.lblNumeroDeNotas.ForeColor = System.Drawing.Color.Maroon;
            this.lblNumeroDeNotas.Name = "lblNumeroDeNotas";
            this.lblNumeroDeNotas.Click += new System.EventHandler(this.lblNumeroDeNotas_Click);
            // 
            // FrmPrincipal
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblNumeroDeNotas);
            this.Controls.Add(this.BtnDesmarcarTodos);
            this.Controls.Add(this.BtnMarcarTodos);
            this.Controls.Add(this.btnManifestar);
            this.Controls.Add(this.lblManifesto);
            this.Controls.Add(this.cmbManifestacao);
            this.Controls.Add(this.dataGridNfe);
            this.Controls.Add(this.btnVerDetalhesCertificado);
            this.Controls.Add(this.btnBuscarNfe);
            this.Controls.Add(this.lblCertificadoSelecionado);
            this.Controls.Add(this.cmbListarCertificados);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.btnSelecionarCertificado);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPrincipal";
            this.Resizable = false;
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridNfe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton btnSelecionarCertificado;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroComboBox cmbListarCertificados;
        private System.Windows.Forms.Label lblCertificadoSelecionado;
        private MetroFramework.Controls.MetroButton btnBuscarNfe;
        private MetroFramework.Controls.MetroButton btnVerDetalhesCertificado;
        public System.Windows.Forms.DataGridView dataGridNfe;
        private MetroFramework.Controls.MetroComboBox cmbManifestacao;
        private MetroFramework.Controls.MetroLabel lblManifesto;
        private MetroFramework.Controls.MetroButton btnManifestar;
        private MetroFramework.Controls.MetroButton BtnMarcarTodos;
        private MetroFramework.Controls.MetroButton BtnDesmarcarTodos;
        private System.Windows.Forms.Label lblNumeroDeNotas;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClXmlNfe;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckNfe;
        private System.Windows.Forms.DataGridViewButtonColumn btnDanfe;
        private System.Windows.Forms.DataGridViewButtonColumn BtnDownloadXml;
        private System.Windows.Forms.DataGridViewTextBoxColumn clNumero;
        private System.Windows.Forms.DataGridViewTextBoxColumn clEmpresa;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClValor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClEmissao;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClManifesto;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClChaveAcesso;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClCnpj;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClIe;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClModelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClUf;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

