using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using GEDAssinaturaDigital = GEDLib.iText7.GEDAssinaturaDigital;
using iTextSharp.text;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Globalization;
using ProtipoConsultaWebService.NFeDistribuicaoDF;
using System.Net;
using Org.BouncyCastle.Asn1.Cms;
using System.IO.Compression;
using System.Reflection;
using System.ServiceModel.Configuration;
using Org.BouncyCastle.X509;
using System.Runtime.InteropServices;
using GEDEntity;
using DanfeSharp;
using DanfeSharp.Model;
using MetroFramework;
using System.Text.RegularExpressions;
using X509Certificate = Org.BouncyCastle.X509.X509Certificate;
namespace ProtipoConsultaWebService
{
    public partial class FrmPrincipal : MetroForm
    {
        //Variaveis referentes ao certificado
        private X509Certificate2Collection certificados;
        private string InicioValidade;
        private string FimValidade;
        private string NumeroSerie;
        private string Qualificador;
        private string Requerente;
        private int indexCertificadoSelecionado;
        private string CnpjCertificado;
        private string CpfCertificado;
        private bool CertificadoPessoaFisica;

        //Variaveis referentes a Nfes
        private string NumeroNfe;
        private string EmpresaNfe;
        private string ValorNfe;
        private string EmissaoNfe;
        private string StatusNfe;
        private string ChaveAcessoNfe;
        private string CnpjNfe;
        private string IeNfe;
        private string TipoNfe;
        private string ModeloNfe;
        private string UfDestinoNfe;
        private string Manifesto;
        private string EventoSelecionado;
        private string CodigoEventoSelecionado;

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            //Combo com os Certificados
            cmbListarCertificados.Items.Clear();
            cmbListarCertificados.Items.Insert(0, "- Selecione -");
            cmbListarCertificados.SelectedIndex = 0;

            certificados = GEDAssinaturaDigital.Certificados();

            for (int i = 0; i < certificados.Count; i++)
            {
                GEDEntity.CertificadoDigital certificado = new GEDEntity.CertificadoDigital(certificados[i]);
                cmbListarCertificados.Items.Add(certificado);
            }

            //Combo de Manifestacao
            cmbManifestacao.Items.Clear();
            cmbManifestacao.Items.Insert(0, "- Selecione -");
            cmbManifestacao.Items.Insert(1, "Confirmação da Operação");
            cmbManifestacao.Items.Insert(2, "Ciência da Operação");
            cmbManifestacao.Items.Insert(3, "Desconhecimento da Operação");
            cmbManifestacao.Items.Insert(4, "Operação não Realizada");

            cmbManifestacao.SelectedIndex = 0;
        }

        private void btnSelecionarCertificado_Click(object sender, EventArgs e)
        {
            int selectedIndex   = cmbListarCertificados.SelectedIndex;
            Object selectedItem = cmbListarCertificados.SelectedItem;
            
            //LIMPO O DATAGRID AO TROCAR DE CERTIFICADO
            dataGridNfe.Rows.Clear();
            lblNumeroDeNotas.Text = "";

            if (selectedIndex.ToString() != "0")
            {
                this.HabilitaCampos();
                lblCertificadoSelecionado.Text   = "Certificado de: " + selectedItem.ToString();
                this.indexCertificadoSelecionado = selectedIndex - 1;
                
                GEDEntity.CertificadoDigital certificado = new GEDEntity.CertificadoDigital(certificados[indexCertificadoSelecionado]);
                
                this.InicioValidade = certificado.InicioValidade.ToString();
                this.FimValidade    = certificado.FimValidade.ToString();
                this.Requerente     = certificado.ToString();
                this.NumeroSerie    = certificado.NumeroSerie.ToString();
                this.Qualificador   = certificado.Qualificador.ToString();
                
                var tipoCertificado = certificado.Requerente.ToString();
                var cpfCnpj         = certificado.certificado.FriendlyName;

                /* CONVERTE O CERTIFICADO DA BIBLIOTECA .NET PARA O CERTIFICADO BOUNCY CASTLE */
                X509CertificateParser parser = new X509CertificateParser();
                X509Certificate parsedCertificate = parser.ReadCertificate(certificado.certificado.RawData);

                List<string> camposOrganizacao = iTextSharp.text.pdf.security.CertificateInfo.GetSubjectFields(parsedCertificate).GetFieldArray("OU");

                if (camposOrganizacao.Count > 3)     /* CERTIFICADO DE PESSOA FISICA */
                {
                    var cpf                      = new GEDEntity.RequerentePessoaFisica(parsedCertificate).CPF;
                    var cpfSemPonto              = this.RemoverPontoCpfCnpj(cpf);
                    this.CpfCertificado          = cpfSemPonto;
                    this.CertificadoPessoaFisica = true;
                    btnManifestar.Enabled        = false;
                    cmbManifestacao.Enabled      = false;
                }
                else                                  /* CERTIFICADO DE PESSOA JURÍDICA */
                {
                    var cnpj                     = new GEDEntity.RequerentePessoaJuridica(parsedCertificate).CNPJ;
                    var cnpjSemPonto             = this.RemoverPontoCpfCnpj(cnpj);
                    this.CnpjCertificado         = cnpjSemPonto;
                    this.CertificadoPessoaFisica = false;
                }

            }
            else
            {
               this.DesabilitaCampos();
            }
        }

        private string RemoverPontoCpfCnpj(string cpfCnpj) 
        {
            Regex reg = new Regex("[0-9]");
            StringBuilder cpfCnpjSemPonto = new StringBuilder();

            foreach (Match m in reg.Matches(cpfCnpj)) 
            {
              cpfCnpjSemPonto.Append(m.Value); ;
            }

            return cpfCnpjSemPonto.ToString();
        }

        private void cmbListarCertificados_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridCertificados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnBuscarNfe_Click(object sender, EventArgs e)
        {
            //LIMPANDO O DATAGRID
            dataGridNfe.Rows.Clear();
            
            try
            {
             this.ConsultarNfe();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha na leitura do Certificado!" + ex);
            }
        }

        private void DesabilitaCampos() 
        {
            lblCertificadoSelecionado.Text    = "";
            lblNumeroDeNotas.Text             = "";
            btnBuscarNfe.Enabled              = false;
            btnVerDetalhesCertificado.Enabled = false;
            btnManifestar.Enabled             = false;
            cmbManifestacao.Enabled           = false;
            dataGridNfe.Rows.Clear();
        }

        private void HabilitaCampos() 
        {
            btnBuscarNfe.Enabled              = true;
            btnVerDetalhesCertificado.Enabled = true;
            btnManifestar.Enabled             = true;
            cmbManifestacao.Enabled           = true;
        }

        public void ConsultarNfe()
        {
            var certificado = GEDAssinaturaDigital.Certificados()[this.indexCertificadoSelecionado];
            XmlDocument doc = new XmlDocument();
            XmlNode NodeRequest, NodeResponse;
            NFeDistribuicaoDF.NFeDistribuicaoDFe distribuicao = new NFeDistribuicaoDF.NFeDistribuicaoDFe();
            string NSU, base64, xmlNota;

            try
            {
                //Lendo o Certificado instalado
                distribuicao.ClientCertificates.Add(certificado);
            }
            catch (Exception e)
            {
                MessageBox.Show("Não foi possível ler o Certificado Digital instalado " + e.Message);
            }

            if (this.CertificadoPessoaFisica is true)
            {
                doc.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
                   + "<distDFeInt versao=\"1.01\" xmlns=\"http://www.portalfiscal.inf.br/nfe\">"
                   + "<tpAmb>1</tpAmb>"
                   + "<cUFAutor>31</cUFAutor>"
                   + "<CPF>" + this.CpfCertificado + "</CPF>"
                   + "<distNSU>"
                   + "<ultNSU>000000000000001</ultNSU>"
                   + "</distNSU>"
                   + "</distDFeInt>");
            }
            else
            {
                doc.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
                     + "<distDFeInt versao=\"1.01\" xmlns=\"http://www.portalfiscal.inf.br/nfe\">"
                     + "<tpAmb>1</tpAmb>"
                     + "<cUFAutor>31</cUFAutor>"
                     + "<CNPJ>" + this.CnpjCertificado + "</CNPJ>"
                     + "<distNSU>"
                     + "<ultNSU>000000000000001</ultNSU>"
                     + "</distNSU>"
                     + "</distDFeInt>");
            }

            NodeRequest = doc.DocumentElement;

            try
            {
                NodeResponse = distribuicao.nfeDistDFeInteresse(NodeRequest);

                XmlDocument node = new XmlDocument();
                node.LoadXml(NodeResponse.OuterXml);
                XmlNodeList list = node.GetElementsByTagName("docZip");

                if (list.Count > 0)
                {
                    foreach (XmlNode nodes in list)
                    {
                        NSU = nodes.Attributes["NSU"].Value;
                        base64 = nodes.InnerText;

                        //Descompactar a nota
                        xmlNota = GzipDecode(base64);

                        //Carregar xml descompactado
                        doc.LoadXml(xmlNota);

                        //Removo os Namespaces do XML
                        XmlDocument nfe = RemoveNameSpaceXml(doc);

                        //Pegando os valores do XML compactado

                        string firstChild = nfe.FirstChild.Name;

                        if (firstChild == "resNFe")
                         {
                                XmlNode globalNode  = nfe.SelectSingleNode("resNFe");
                                XmlNode chaveNode   = globalNode.SelectSingleNode("chNFe");
                                XmlNode empresaNode = globalNode.SelectSingleNode("xNome");
                                XmlNode valorNode   = globalNode.SelectSingleNode("vNF");
                                XmlNode emissaoNode = globalNode.SelectSingleNode("dhEmi");
                                XmlNode statusNode  = globalNode.SelectSingleNode("cSitNFe");
                                XmlNode cnpjNode    = globalNode.SelectSingleNode("CNPJ");
                                XmlNode ieNode      = globalNode.SelectSingleNode("IE");
                                XmlNode tipoNode    = globalNode.SelectSingleNode("tpNF");

                                this.NumeroNfe      = this.StringNumeroNfe(chaveNode.InnerText);
                                this.EmpresaNfe     = empresaNode.InnerText;
                                this.ValorNfe       = valorNode.InnerText;
                                this.EmissaoNfe     = DateTime.Parse(emissaoNode.InnerText).ToString("dd-MM-yyyy");
                                this.StatusNfe      = this.StringStatusNfe(statusNode.InnerText);
                                this.ChaveAcessoNfe = chaveNode.InnerText;
                                this.CnpjNfe        = cnpjNode.InnerText;
                                this.IeNfe          = ieNode.InnerText;
                                this.TipoNfe        = this.StringTipoNfe(tipoNode.InnerText);
                                this.ModeloNfe      = this.StringModelNfe(chaveNode.InnerText);
                                this.UfDestinoNfe   = this.StringUfNfe(chaveNode.InnerText);
                                this.Manifesto      = "Sim";

                                //Exibindo os itens da Nota no DataGrid
                                DataRow row;
                                dataGridNfe.Rows.Add(nfe,
                                                     false,
                                                     "",
                                                     "",
                                                     this.NumeroNfe,
                                                     this.EmpresaNfe,
                                                     this.ValorNfe,
                                                     this.EmissaoNfe,
                                                     this.StatusNfe,
                                                     this.Manifesto,
                                                     this.ChaveAcessoNfe,
                                                     this.CnpjNfe,
                                                     this.IeNfe,
                                                     this.TipoNfe,
                                                     this.ModeloNfe,
                                                     this.UfDestinoNfe);

                                var numLinhasDatGrid = dataGridNfe.Rows.Count.ToString();
                                lblNumeroDeNotas.Text = $"{numLinhasDatGrid} notas encontradas!";
                            }
                        }
                    }
                else
                    {
                        MessageBox.Show(string.Format("Foram encontradas {0}", list.Count));
                    }
                }
            catch (Exception e)
            {
                MessageBox.Show("A consulta ao Web Service não foi realizada, tente novamente! " + e.Message);
            }
         
        }

        private string StringStatusNfe(string status)
        {
            if (String.IsNullOrEmpty(status))
            {
                MessageBox.Show("Status não informada!");
                return null;
            }
            else
            {
                if (status == "1")
                {
                    return this.StatusNfe = "Autorizada";
                }
                else if (status == "2")
                {
                    return this.StatusNfe = "Negada";
                }
                else
                {
                    return this.StatusNfe = "Cancelada";
                }
            }
        }

        private string StringTipoNfe(string tipo)
        {
            if (String.IsNullOrEmpty(tipo))
            {
                MessageBox.Show("Tipo não informada!");
                return null;
            }
            else
            {
                if (tipo == "0")
                {
                    return this.TipoNfe = "Entrada";
                }
                else
                {
                    return this.TipoNfe = "Saída";
                }
            }
        }

        private string StringNumeroNfe(string chave)
        {
            if (String.IsNullOrEmpty(chave))
            {
                MessageBox.Show("Chave não informada!");
                return null;
            }
            else
            {
                string numero = chave.Substring(25, 9);
                this.NumeroNfe = numero;
                return this.NumeroNfe;
            }
        }

        private string StringUfNfe(string chave)
        {
            if (String.IsNullOrEmpty(chave))
            {
                MessageBox.Show("Chave não informada!");
                return null;
            }
            else
            {
                string uf = chave.Substring(0, 2);
                this.UfDestinoNfe = uf;

                switch (uf)
                {
                    case "12":
                        this.UfDestinoNfe = "AC";
                        break;

                    case "13":
                        this.UfDestinoNfe = "AM";
                        break;

                    case "14":
                        this.UfDestinoNfe = "RO";
                        break;

                    case "15":
                        this.UfDestinoNfe = "PA";
                        break;

                    case "16":
                        this.UfDestinoNfe = "AP";
                        break;

                    case "17":
                        this.UfDestinoNfe = "TO";
                        break;

                    case "21":
                        this.UfDestinoNfe = "MA";
                        break;

                    case "22":
                        this.UfDestinoNfe = "PI";
                        break;

                    case "23":
                        this.UfDestinoNfe = "CE";
                        break;

                    case "24":
                        this.UfDestinoNfe = "RN";
                        break;

                    case "25":
                        this.UfDestinoNfe = "PB";
                        break;

                    case "26":
                        this.UfDestinoNfe = "PE";
                        break;

                    case "27":
                        this.UfDestinoNfe = "AL";
                        break;

                    case "28":
                        this.UfDestinoNfe = "SE";
                        break;

                    case "29":
                        this.UfDestinoNfe = "BA";
                        break;

                    case "31":
                        this.UfDestinoNfe = "MG";
                        break;

                    case "32":
                        this.UfDestinoNfe = "ES";
                        break;

                    case "33":
                        this.UfDestinoNfe = "RJ";
                        break;

                    case "35":
                        this.UfDestinoNfe = "SP";
                        break;

                    case "41":
                        this.UfDestinoNfe = "PR";
                        break;

                    case "42":
                        this.UfDestinoNfe = "SC";
                        break;

                    case "50":
                        this.UfDestinoNfe = "MS";
                        break;

                    case "51":
                        this.UfDestinoNfe = "MT";
                        break;

                    case "52":
                        this.UfDestinoNfe = "GO";
                        break;

                    case "53":
                        this.UfDestinoNfe = "DF";
                        break;

                    default:
                        this.UfDestinoNfe = "**";
                        break;
                }

                return this.UfDestinoNfe;
            }
        }

        private string StringModelNfe(string chave)
        {
            if (String.IsNullOrEmpty(chave))
            {
                MessageBox.Show("Chave não informada!");
                return null;
            }
            else
            {
                string modelo = chave.Substring(20, 2);
                switch (modelo)
                {
                    case "55":
                        modelo = "55 - NF-e";
                        this.ModeloNfe = modelo;
                        return this.ModeloNfe;

                    case "65":
                        modelo = "65 - NFCe";
                        this.ModeloNfe = modelo;
                        return this.ModeloNfe;

                    case "57":
                        modelo = "57 - CTE";
                        this.ModeloNfe = modelo;
                        return this.ModeloNfe;

                    case "58":
                        modelo = "58 - MDF-e";
                        this.ModeloNfe = modelo;
                        return this.ModeloNfe;

                    default:
                        this.ModeloNfe = "NFE";
                        return this.ModeloNfe;
                }
            }
        }

        public XmlDocument RemoveNameSpaceXml(XmlDocument doc)
        {
            var xml = doc.OuterXml;
            var newxml = System.Text.RegularExpressions.Regex.Replace(xml, @"xmlns[:xsi|:xsd]*="".*?""", "");
            var newdoc = new XmlDocument();
            newdoc.LoadXml(newxml);
            return newdoc;
        }

        private static string GzipDecode(string inputStr)
        {
            byte[] inputBytes = Convert.FromBase64String(inputStr);

            using (var inputStream  = new MemoryStream(inputBytes))
            using (var gZipStream   = new GZipStream(inputStream, CompressionMode.Decompress))
            using (var outputStream = new MemoryStream())
            {
                gZipStream.CopyTo(outputStream);
                var outputBytes = outputStream.ToArray();

                string decompressed = Encoding.UTF8.GetString(outputBytes);

                return decompressed;
            }
        }

        private void btnVerDetalhesCertificado_Click(object sender, EventArgs e)
        {
            var certificado = GEDAssinaturaDigital.Certificados()[this.indexCertificadoSelecionado];

            MessageBox.Show("Certificado de: " + this.Requerente + "\n" +
                             "Inicio da validade: " + this.InicioValidade + "\n" +
                             "Fim da validade " + this.FimValidade + "\n" +
                             "Número de Série: " + this.NumeroSerie + "\n" +
                             "Qualificador: " + this.Qualificador);
        }

        private void dataGridNfe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.CertificadoPessoaFisica is false)
            {
                var senderGrid = (DataGridView)sender;
                var checkNfe = dataGridNfe.Rows[e.RowIndex].Cells["CheckNfe"];

                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0 && e.ColumnIndex == 3)
                {
                    string nfeSelecionada = dataGridNfe.Rows[e.RowIndex].Cells["ClChaveAcesso"].Value.ToString();
                    string status = dataGridNfe.Rows[e.RowIndex].Cells["ClStatus"].Value.ToString();

                    if (status == "Cancelada")
                    {
                        MessageBox.Show("Não é possível fazer Download do Xml de uma NFe cancelada!");
                    }
                    else
                    {
                        this.DownloadNfe(nfeSelecionada);
                    }
                }

                else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0 && e.ColumnIndex == 2)
                {
                    string nfeSelecionada = dataGridNfe.Rows[e.RowIndex].Cells["ClChaveAcesso"].Value.ToString();
                    string status = dataGridNfe.Rows[e.RowIndex].Cells["ClStatus"].Value.ToString();

                    if (status == "Cancelada")
                    {
                        MessageBox.Show("Não é possível Gerar Danfe de uma NFe cancelada!");
                    }
                    else
                    {
                        this.DownloadNfe(nfeSelecionada, true);
                    }

                }

                if (checkNfe.Value is false)
                {
                    checkNfe.Value = true;
                }
                else
                {
                    checkNfe.Value = false;
                }
            }
            else 
            {
                MessageBox.Show("Não é possível Gerar Danfe ou fazer Download do Xml com Certificado E-Cpf");  
            }
          
        }

        private void BtnMarcarTodos_Click(object sender, EventArgs e)
        {
            int totalDeLinhas = Convert.ToInt32(dataGridNfe.Rows.Count.ToString());
            int selecionados  = 0;

            for (int index = 0; index < totalDeLinhas; index++)
            {
                var checksNfe = dataGridNfe.Rows[index].Cells["CheckNfe"];

                if (checksNfe.Value is false)
                {
                    dataGridNfe.Rows[index].Cells["CheckNfe"].Value = true;
                }
            }
        }

        private void BtnDesmarcarTodos_Click(object sender, EventArgs e)
        {
            int totalDeLinhas = Convert.ToInt32(dataGridNfe.Rows.Count.ToString());
            int selecionados  = 0;

            for (int index = 0; index < totalDeLinhas; index++)
            {
                var checksNfe = dataGridNfe.Rows[index].Cells["CheckNfe"];

                if (checksNfe.Value is true)
                {
                    dataGridNfe.Rows[index].Cells["CheckNfe"].Value = false;
                }
            }
        }

        private void ManifestarNfe()
        {
            var certificado    = GEDAssinaturaDigital.Certificados()[this.indexCertificadoSelecionado];
            var numeroDeLinhas = dataGridNfe.Rows.Count;
            NFeRecepcaoEvento.RecepcaoEvento recepcao = new NFeRecepcaoEvento.RecepcaoEvento();
            recepcao.ClientCertificates.Add(certificado);
            List<string> notas = new List<string>();

            if (cmbManifestacao.SelectedIndex == 1)
            {
                this.EventoSelecionado = "Confirmacao da Operacao";
                this.CodigoEventoSelecionado = "210200";
            }
            else if (cmbManifestacao.SelectedIndex == 2)
            {
                this.EventoSelecionado = "Ciencia da Operacao";
                this.CodigoEventoSelecionado = "210210";
            }
            else if (cmbManifestacao.SelectedIndex == 3)
            {
                this.EventoSelecionado = "Desconhecimento da operacao ";
                this.CodigoEventoSelecionado = "210220";
            }
            else if (cmbManifestacao.SelectedIndex == 4)
            {
                this.EventoSelecionado = "Operacao não Realizada";
                this.CodigoEventoSelecionado = "210240";
            }

            for (int index = 0; index < numeroDeLinhas; index++)
            {
                var checksNfe = dataGridNfe.Rows[index].Cells["CheckNfe"];
                
                if (checksNfe.Value is true && cmbManifestacao.SelectedIndex == 1)
                {
                    string chaveNfe = dataGridNfe.Rows[index].Cells["ClChaveAcesso"].Value.ToString();
                    notas.Add(chaveNfe);
                }
            }

            if (notas.Count > 0)
            {
                var sXml = new StringBuilder();
                sXml.Append(@"<?xml version=""1.0"" encoding=""UTF-8""?>
                <envEvento xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""1.00"">
                <idLote>1</idLote>");

                if (this.CertificadoPessoaFisica is true)
                {
                    foreach (var nota in notas)
                    {
                    sXml.Append(@"
                    <evento xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""1.00"">
                    <infEvento Id=""ID210200"+nota+@"01"">
                    <cOrgao>91</cOrgao>
                    <tpAmb>1</tpAmb>
                    <CPF>"+this.CpfCertificado+@"</CPF>
                    <chNFe>"+nota+@"</chNFe>
                    <dhEvento>"+DateTime.Now.AddDays(-1).ToString("yyyy-MM-ddTHH:mm:ss")+@"-03:00</dhEvento>
                    <tpEvento>"+this.CodigoEventoSelecionado+@"</tpEvento>
                    <nSeqEvento>1</nSeqEvento>
                    <verEvento>1.00</verEvento>
                    <detEvento versao=""1.01"">
                    <descEvento>"+this.EventoSelecionado+@"</descEvento>
                    </detEvento>
                    </infEvento>
                    </evento>
                            ");
                    }
                }
                else 
                {
                    foreach (var nota in notas)
                    {
                    sXml.Append(@"
                    <evento xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""1.00"">
                    <infEvento Id=""ID210200"+nota+@"01"">
                    <cOrgao>91</cOrgao>
                    <tpAmb>1</tpAmb>
                    <CNPJ>"+this.CnpjCertificado+@"</CNPJ>
                    <chNFe>"+nota+@"</chNFe>
                    <dhEvento>"+DateTime.Now.AddDays(-1).ToString("yyyy-MM-ddTHH:mm:ss")+@"-03:00</dhEvento>
                    <tpEvento>"+this.CodigoEventoSelecionado+@"</tpEvento>
                    <nSeqEvento>1</nSeqEvento>
                    <verEvento>1.00</verEvento>
                    <detEvento versao=""1.00"">
                    <descEvento>"+this.EventoSelecionado+@"</descEvento>
                    </detEvento>
                    </infEvento>
                    </evento>
                       ");
                    }
                }
                sXml.Append("</envEvento>");
                var xml = new XmlDocument();
                xml.LoadXml(sXml.ToString().Replace("\r\n  ", "").Replace("\r\n", "").Replace("xmlns=\"\"", ""));
                var index = 0;
                
                foreach (var nota in notas)
                {
                    var docXml     = new SignedXml(xml);
                    var referencia = new Reference(string.Empty);
                    referencia.Uri = "#ID210200" + nota + "01";
                    referencia.AddTransform(new XmlDsigEnvelopedSignatureTransform());
                    referencia.AddTransform(new XmlDsigC14NTransform());

                    docXml.AddReference(referencia);
                    docXml.SigningKey = certificado.PrivateKey;

                    var key = new KeyInfo();
                    key.AddClause(new KeyInfoX509Data(certificado));
                    docXml.KeyInfo = key;
                    docXml.ComputeSignature();
                    
                    index++;
                    xml.ChildNodes[1].ChildNodes[index].AppendChild(xml.ImportNode(docXml.GetXml(), true));
                }
                
                    NFeRecepcaoEvento.nfeCabecMsg recepcaoMsg = new NFeRecepcaoEvento.nfeCabecMsg();
                    recepcaoMsg.cUF           = "31";
                    recepcaoMsg.versaoDados   = "1.00";
                    recepcao.nfeCabecMsgValue = recepcaoMsg;

                    var resposta = recepcao.nfeRecepcaoEvento(xml);
                    var fileResp = "c:\\RespXml\\respXml.xml";
                    var fileReq  = "c:\\ReqXml\\reqXml.xml";
                    
                    File.WriteAllText(fileReq, xml.OuterXml);
                    File.WriteAllText(fileResp, resposta.OuterXml);
                    System.Diagnostics.Process.Start(fileReq);
                    System.Diagnostics.Process.Start(fileResp);
            }
            else 
            {
                    MessageBox.Show("Necessário selecionar ao menos uma Nota!");
            }
        }

        private void DownloadNfe(string chave, bool danfe = false) 
        {
            var certificado    = GEDAssinaturaDigital.Certificados()[this.indexCertificadoSelecionado];
            var numeroDeLinhas = dataGridNfe.Rows.Count;
            var sXml = @"<distDFeInt xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""1.01"">
                        <tpAmb>1</tpAmb>
                        <cUFAutor>31</cUFAutor>
                        <CNPJ>"+this.CnpjCertificado+@"</CNPJ>
                        <consChNFe>
                        <chNFe>"+chave+@"</chNFe>
                        </consChNFe>
                        </distDFeInt>";

            var xml = this.ConverterStringToXml(sXml);
            if (danfe is true)
            {
                this.BaixarXml(xml, true);
            }
            else 
            {
                this.BaixarXml(xml);
            }
        }

        private void BaixarXml(XmlDocument xml, bool danfe = false) 
        {
           NFeDistribuicaoDF.NFeDistribuicaoDFe distribuicao = new NFeDistribuicaoDF.NFeDistribuicaoDFe();
           var certificado = GEDAssinaturaDigital.Certificados()[this.indexCertificadoSelecionado];
           distribuicao.ClientCertificates.Add(certificado);
           var NodeRequest  = xml.DocumentElement;
           XElement element = XElement.Parse(xml.InnerXml);

          var arquivo    = distribuicao.nfeDistDFeInteresse(xml);
          var arqString  = XElement.Parse(arquivo.OuterXml).ToString();  
          var xmlNota    = ConverterStringToXml(arqString);
          
          var conteuZip  = xmlNota.GetElementsByTagName("docZip")[0].InnerText;
          byte[] dados   = Convert.FromBase64String(conteuZip);
          var xmlRetorno = descompactar(dados);

          XmlDocument xmlNfe = new XmlDocument();
          xmlNfe.LoadXml(xmlRetorno);
          string descricaoNota = xmlNfe.GetElementsByTagName("xNome")[0].InnerText.Replace("/", "");

            if (danfe is true) 
            {
             this.SalvarDanfe(xmlRetorno);
            }
            else 
            {
             this.SalvarXml(xmlNfe, descricaoNota);
            }
        }

        private void SalvarXml(XmlDocument xml, string descricao) 
        {
            folderBrowserSave.Description = "Selecione uma pasta para salvar o Xml";
            DialogResult resultado        = folderBrowserSave.ShowDialog();
            if (resultado == DialogResult.OK) 
            {
                    try
                    {
                        string caminho = string.Concat(folderBrowserSave.SelectedPath, "\\" + descricao + ".xml");
                        File.WriteAllText(caminho, xml.OuterXml);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Não foi possível salvar o Xml! " + e.Message,
                                       "Atenção",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation,
                                        MessageBoxDefaultButton.Button1);
                    }

                    try
                    {
                        string caminho = string.Concat(folderBrowserSave.SelectedPath, "\\" + descricao + ".xml");
                        File.WriteAllText(caminho, xml.OuterXml);

                        MessageBox.Show("O Xml foi salvo em: " + caminho);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Não foi possível salvar o Xml! " + e.Message,
                                       "Atenção",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation,
                                        MessageBoxDefaultButton.Button1);
                    }
            }
        }

        private void SalvarDanfe(string xml = "") 
        {
            var modelo = DanfeViewModel.CreateFromXmlString(xml);
            var danfe                       = new DanfeDocumento(modelo);
            saveFileDialog.Title            = "Selecione uma pasta para salvar o Arquivo";
            saveFileDialog.DefaultExt       = ".pdf";
            saveFileDialog.RestoreDirectory = true;

            DialogResult resultado = saveFileDialog.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                try
                {
                    FileStream fs       = new FileStream(saveFileDialog.FileName, FileMode.Create);
                    StreamWriter writer = new StreamWriter(fs);
                    string caminho      = saveFileDialog.FileName;

                    fs.Close();
                    
                    danfe.Gerar();
                    danfe.Salvar(caminho);
                    
                    MessageBox.Show("Danfe foi salvo em: " + caminho);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Não foi possível salvar o arquivo de Danfe! " + e.Message,
                                       "Atenção",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Exclamation,
                                       MessageBoxDefaultButton.Button1);
                }
                
            }
        }

        public string descompactar(byte[] conteudo)
        {
            using (var memory = new MemoryStream(conteudo))
            using (var compression = new GZipStream(memory, CompressionMode.Decompress))
            using (var reader = new StreamReader(compression))
            {
                return reader.ReadToEnd();
            }
        }

        private XmlDocument ConverterStringToXml(string texto)
        {
            var xml = new XmlDocument();
            xml.LoadXml(texto);
            return xml;
        }

        private void cmbListaManifesto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnManifestar_Click(object sender, EventArgs e)
        {
            ManifestarNfe();
        }

        private void lblNumeroDeNotas_Click(object sender, EventArgs e)
        {

        }
    }
}
