using eDrawings.Interop.EModelViewControl;
using EModelViewMarkup;
using LmCorbieUI;
using LmCorbieUI.Controls;
using LmCorbieUI.Design;
using LmCorbieUI.LmForms;
using LmCorbieUI.Metodos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AxionConnect.Api;

namespace AxionConnect {
  public partial class FrmEngenharia : LmChildForm {
    private EModelViewControl _eDrawingsCtrl;
    private EModelMarkupControl _markupCtrl;

    SortableBindingList<ProdutoErp> _produtos = new SortableBindingList<ProdutoErp>();
    TreeView _arvoreCompleta = new TreeView();
    bool _visualizando3D = false;

    //Color corErro = Color.Red;
    Color corSucesso = Color.Green;

    string tempoPadro = "00:01";
    int numOperadorPadrao = 1;

    public FrmEngenharia() {
      InitializeComponent();

      tbcOperacoes.SelectedIndex = 0;

      ImageList il = new ImageList();
      il.Images.Add(0.ToString(), Properties.Resources.assembly);
      il.Images.Add(1.ToString(), Properties.Resources.part);
      il.Images.Add(2.ToString(), Properties.Resources.weldmentcutlist);
      il.Images.Add(3.ToString(), Properties.Resources.sheetmetal);
      il.Images.Add(4.ToString(), Properties.Resources.weldment);
      il.Images.Add(5.ToString(), Properties.Resources.toolbox_item);

      trvProduto.ImageList = il;
      trvProduto.ItemHeight = 21;

      flpEtapaConsumo.Controls.Clear();

      _produtos = new SortableBindingList<ProdutoErp>();
      dgv.MontarGrid<ProdutoErp>();
    }

    private void FrmEngenharia_Load(object sender, EventArgs e) {
      OnLoadForm();
    }

    private async Task OnLoadForm() {
      try {
        await Processo.Carregar();
        var lists = Processo.ListaProcessos
            .GroupBy(x => x.codOperacao)
            .Select(g => new Z_Padrao {
              Codigo = g.Key,
              Descricao = g.Key + " - " + g.First().descrOperacao
            })
            .ToList()
            .OrderBy(x => x.Codigo);

        txtOperacao.CarregarComboBox(lists);
      } catch (Exception ex) {
        LmException.ShowException(ex, "Erro ao carregar tela");
      }
    }

    private void FrmEngenharia_Loaded(object sender, EventArgs e) {
      Invoke(new MethodInvoker(delegate () {
        trvProduto.BackColor = LmCor.Bc_Form;
        txtTempoOperacao.Text = tempoPadro;
        txtNumeroOperadores.Text = numOperadorPadrao.ToString();
        LimparLabels();
        this.Refresh();
      }));
    }

    protected override void OnShown(EventArgs e) {
      base.OnShown(e);
      // Load eDrawings control
      eDrawingsUC.LoadEDrawings();
    }

    private void OnEDrawingsControlLoaded(EModelViewControl obj) {
      _eDrawingsCtrl = obj;
    }

    private void FrmEngenharia_Resize(object sender, EventArgs e) {
      //if (splContainer.Width > 0)
      //  splContainer.SplitterDistance = splContainer.Width - 381;
    }

    private void TxtCodEngenharia_Enter(object sender, EventArgs e) {
      txtCodEngenharia.Tag = txtCodEngenharia.Text;
    }

    private void TxtCodEngenharia_Leave(object sender, EventArgs e) {
      var tag = txtCodEngenharia.Tag as string;
      if (tag != txtCodEngenharia.Text && long.TryParse(txtCodEngenharia.Text, out long codEngenharia)) {
        CarregarEngenhariaAsync(codEngenharia);
      }
    }

    private async Task CarregarEngenhariaAsync(long codEngenharia) {
      try {
        txtCodEngenharia.ReadOnly = true;
        btnNovaEngenharia.Enabled = btnSalvar.Enabled = false;
        _arvoreCompleta.Nodes.Clear();
        trvProduto.Nodes.Clear();
        _produtos = new SortableBindingList<ProdutoErp>();
        dgv.CarregarGrid(_produtos);

        //await Loader.ShowDuringOperation(async (progress) => {
        //  progress.Report("Iniciando leitura do ERP...");
        _produtos = await ProdutoErp.GetComponentsFromERPAsync(_arvoreCompleta, codEngenharia);
        //});

        CarregarGrid();
        if (_produtos.Count == 0) {
          txtCodEngenharia.ReadOnly = false;
          txtCodEngenharia.Text = string.Empty;
          txtCodEngenharia.Focus();
        }
        btnNovaEngenharia.Enabled = btnSalvar.Enabled = true;
      } catch (Exception ex) {
        LmException.ShowException(ex, "Erro ao processar o código de engenharia.");
      } finally {
        MsgBox.CloseWaitMessage();
      }
    }

    private void BtnSalvar_Click(object sender, EventArgs e) {
      try {
        if (dgv.Grid.CurrentRow == null) {
          Toast.Info($"Nenhum produto selecionado");
          return;
        }

        if (_produtos.Count == 0) {
          Toast.Warning("Carregar componentes primeiro");
          return;
        }

        var produtoERP = dgv.Grid.CurrentRow.DataBoundItem as ProdutoErp;

        if (produtoERP.TipoComponente == TipoComponente.ItemBiblioteca) {
          Toast.Warning("Recurso indisponivel para o componentes de biblioteca.");
          return;
        }

        var possuiErro = false;
        _produtos.Where(x => x.Nivel == produtoERP.Nivel || x.Nivel.StartsWith($"{produtoERP.Nivel}.")).ToList().ForEach(x => {
          if (x.Pendencias.Count > 0) {

            var msgPend = string.Empty;
            x.Pendencias.ForEach(y => {
              msgPend += $"- {y.ObterDescricaoEnum()}\r\n";
            });

            Toast.Warning($"{x.Name} [{x.Nivel}]\r\n{msgPend}");
            possuiErro = true;
          }
        });

        if (possuiErro) {
          return;
        }

        System.Threading.Thread t = new System.Threading.Thread(() => { CadastrarNovo(produtoERP); }) { IsBackground = true };
        t.Start();
      } catch (Exception ex) {
        MsgBox.Show($"Erro ao salvar..\n\n{ex.Message}", "Axion LM Projetos",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      } finally {
      }
    }

    private void BtnNovaEngenharia_Click(object sender, EventArgs e) {
      if (dgv.Grid.CurrentRow != null) {
        var produtoERP = dgv.Grid.CurrentRow.DataBoundItem as ProdutoErp;
        _eDrawingsCtrl.CloseActiveDoc(produtoERP.PathName);
      }

      ptbZoom.Visible = ptbDesenho.Visible = ptbProximoDesenho.Visible = false;
      txtCodEngenharia.ReadOnly = false;
      txtCodEngenharia.Text = string.Empty;

      txtMaquina.CampoObrigatorio = txtOperacao.CampoObrigatorio = false;
      txtMaquina.SelectedValue = txtOperacao.SelectedValue = null;

      txtTempoOperacao.Text = tempoPadro;
      txtNumeroOperadores.Text = numOperadorPadrao.ToString();

      // limpar labels
      LimparLabels();

      flpEtapaConsumo.Controls.Clear();

      _arvoreCompleta.Nodes.Clear();
      trvProduto.Nodes.Clear();
      _produtos = new SortableBindingList<ProdutoErp>();
      dgv.CarregarGrid(_produtos);
      txtCodEngenharia.Focus();
    }

    private void LimparLabels() {
      lblSmLarg.Text =
      lblSmCompr.Text =
      lblEspess.Text =
      lblCodDescMat.Text =
      lblCodigoProduto.Text =
      lblPeso.Text =
      lblNome.Text =
      lblDescricao.Text = string.Empty;
    }

    private void BtnVoltar_Click(object sender, EventArgs e) {
      try {
        if (_produtos.Count == 0) {
          Toast.Warning("Favor Carregar Componentes primeiro.");
          return;
        }

        if (dgv.Grid.CurrentRow.Index > 0)
          dgv.Grid.Rows[dgv.Grid.CurrentRow.Index - 1].Cells[1].Selected = true;
        else {
          Toast.Info($"Você está no primeiro componente");
        }
      } catch (Exception ex) {
        MsgBox.Show($"Erro ao voltar peça\n\n{ex.Message}", "Axion LM Projetos",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void BtnProximo_Click(object sender, EventArgs e) {
      try {
        if (_produtos.Count == 0) {
          Toast.Warning("Favor Carregar Componentes primeiro.");
          return;
        }

        if (dgv.Grid.CurrentRow.Index + 1 < dgv.Grid.RowCount)
          dgv.Grid.Rows[dgv.Grid.CurrentRow.Index + 1].Cells[1].Selected = true;
        else {
          Toast.Info($"Você já chegou no último componente");
        }
      } catch (Exception ex) {
        MsgBox.Show($"Erro ao avançar peça\n\n{ex.Message}", "Axion LM Projetos",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void Dgv_RowIndexChanged(object sender, EventArgs e) {
      try {
        if (sender == null) return;

        AtualizarComponente();
        txtOperacao.Focus();
      } catch (Exception ex) {
        LmException.ShowException(ex, "Erro ao atualizar dados Componente");
      }
    }

    private void AtualizarComponente() {
      try {
        lblPeso.Text = "0,000Kg";
        lblSmLarg.Text =
        lblSmCompr.Text =
        lblEspess.Text =
        lblCodDescMat.Text =
        lblCodigoProduto.Text =
        lblNome.Text =
        lblDescricao.Text = string.Empty;

        ClearControls();
        var produtoErp = dgv.Grid.CurrentRow.DataBoundItem as ProdutoErp;
        var nomeDesenho = produtoErp.PathName.Substring(0, produtoErp.PathName.Length - 6) + "SLDDRW";

        txtMaquina.CampoObrigatorio = txtOperacao.CampoObrigatorio = false;
        txtMaquina.SelectedValue = txtOperacao.SelectedValue = null;

        txtTempoOperacao.Text = tempoPadro;
        txtNumeroOperadores.Text = numOperadorPadrao.ToString();

        ptbZoom.Visible = true;

        if (File.Exists(nomeDesenho)) {
          _eDrawingsCtrl.OpenDoc(nomeDesenho, false, false, false, "");

          string nomeProduto = produtoErp.Name;
          var match = System.Text.RegularExpressions.Regex.Match(nomeProduto, @" - P(\d+)$");
          if (match.Success) {
            string paginaProcurada = $"P{match.Groups[1].Value}";

            try {
              // Percorrer todas as páginas do documento
              int totalSheets = _eDrawingsCtrl.SheetCount;

              for (int i = 0; i < totalSheets; i++) {
                string nomeSheet = _eDrawingsCtrl.SheetName[i];

                // Comparar se o nome da sheet corresponde à página procurada
                if (nomeSheet.Equals(paginaProcurada, StringComparison.OrdinalIgnoreCase)) {
                  // Navegar para a página encontrada
                  _eDrawingsCtrl.ShowSheet(i);
                  //Task.Delay(100);

                  break;
                }
              }
            } catch (Exception ex) {
              // Log do erro caso ocorra problema ao navegar
              // MessageBox.Show($"Erro ao navegar para página {paginaProcurada}: {ex.Message}");
            }
          } else {
            _eDrawingsCtrl.ShowSheet(0);
          }

          this.Refresh();

          ptbDesenho.Visible = true;
          _visualizando3D = false;
          ptbDesenho.Image = produtoErp.TipoComponente == TipoComponente.Montagem ? Properties.Resources.assembly : Properties.Resources.part;
          toolTip1.SetToolTip(ptbDesenho, produtoErp.TipoComponente == TipoComponente.Montagem ? "Abrir Conjunto" : "Abrir Peça");
          int total = _eDrawingsCtrl.SheetCount;
          ptbProximoDesenho.Visible = total > 1;
          ptbZoom.Image = Properties.Resources.zoomfit;
        } else if (File.Exists(produtoErp.PathName)) {
          _eDrawingsCtrl.OpenDoc(produtoErp.PathName, false, false, false, "");
          toolTip1.SetToolTip(ptbDesenho, "Abrir Desenho");
          _visualizando3D = true;
          ptbDesenho.Image = Properties.Resources.draw;
          ptbZoom.Image = Properties.Resources.isometric;
          ptbProximoDesenho.Visible = false;
        } else {
          _eDrawingsCtrl.CloseActiveDoc(produtoErp.PathName);
          Toast.Info($"Produto não encontrado:\r\n{produtoErp.PathName}");
          ptbDesenho.Visible = false;
          ptbZoom.Visible = false;
        }

        _eDrawingsCtrl.BackgroundColorOverride = true;
        _eDrawingsCtrl.BackgroundColor = 0xdfe4e9;

        if (produtoErp.Pendencias.Where(y => y.EhPendenciaCritica()).ToList().Count > 0) {
          var msgPend = string.Empty;
          produtoErp.Pendencias.Where(y => y.EhPendenciaCritica()).ToList().ForEach(y => {
            msgPend += $"- {y.ObterDescricaoEnum()}\r\n";
          });

          Toast.Warning($"{produtoErp.Name} [{produtoErp.Nivel}]\r\n{msgPend}");
        }

        if (produtoErp.Pendencias.Where(y => !y.EhPendenciaCritica()).ToList().Count > 0) {
          var msgPend = string.Empty;
          produtoErp.Pendencias.Where(y => !y.EhPendenciaCritica()).ToList().ForEach(y => {
            msgPend += $"- {y.ObterDescricaoEnum()}\r\n";
          });

          Toast.Info($"{produtoErp.Name} [{produtoErp.Nivel}]\r\n{msgPend}");
        }

        _eDrawingsCtrl.ActivateInkMarkup(0);

        TreeNode node = GetNodeByLevelPath(_arvoreCompleta, produtoErp.Nivel);
        TreeNode clonedNode = (TreeNode)node.Clone();
        trvProduto.Nodes.Clear();
        trvProduto.Nodes.Add(clonedNode);
        clonedNode.ExpandAll();
        AtualizarInformacoes(produtoErp);
        GetProcess(produtoErp);

        CarregarSequenciaOp(produtoErp);
      } catch (Exception ex) {
        MsgBox.Show($"Erro ao Atualizar Dados\n\n{ex.Message}", "Axion LM Projetos",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void AtualizarInformacoes(ProdutoErp produtoErp) {
      lblNome.Text = produtoErp.Name;
      lblDescricao.Text = produtoErp.Denominacao;
      lblSmLarg.Text = produtoErp.SobremetalLarg.ToString("#");
      lblSmCompr.Text = produtoErp.SobremetalCompr.ToString("#");
      lblPeso.Text = produtoErp.PesoBruto + " kg";
      lblCodigoProduto.Text = produtoErp.CodProduto;

      CriarMarkupTexto();

      if (produtoErp.TipoComponente == TipoComponente.Peca) {
        var espess = produtoErp.Espessura;
        var largur = produtoErp.Largura;
        var compri = produtoErp.Comprimento;
        var tipo = produtoErp.TipoListaMaterial;

        lblEspess.Text = tipo == TipoListaMaterial.Chapa ? $"{espess}x{largur}x{compri}" : $"{compri}";

        var nodeFilho = trvProduto.Nodes.Count > 0 && trvProduto.Nodes[0].Nodes.Count > 0 ? trvProduto.Nodes[0].Nodes[0] : null;
        if (nodeFilho != null) {
          ProdutoErp produtoErpFilho = nodeFilho.Tag as ProdutoErp;
          var descricMaterial = produtoErpFilho.Denominacao;
          var codigo = produtoErpFilho.CodProduto;

          lblCodDescMat.Text = $"{codigo} - {descricMaterial}";
        }
      }
    }

    private void CriarMarkupTexto() {
      try {
        if (_eDrawingsCtrl == null) {
          MessageBox.Show("eDrawings não está carregado!");
          return;
        }



      } catch (Exception ex) {
        MessageBox.Show("Erro ao inserir markup: " + ex.Message);
      }
    }


    private void GetProcess(ProdutoErp produtoErp) {
      try {
        ClearControls();

        if (produtoErp.Operacoes != null && produtoErp.Operacoes.Count > 0) {
          foreach (var prc in produtoErp.Operacoes) {
            CardInsert(prc);

            txtMaquina.SelectedValue = txtOperacao.SelectedValue = null;
            txtTempoOperacao.Text = tempoPadro;
            txtNumeroOperadores.Text = numOperadorPadrao.ToString();
            txtOperacao.Focus();
          }
        }
      } catch (Exception ex) {
        MsgBox.Show($"Erro ao retornar Processos\n\n{ex.Message}", "Axion LM Projetos",
             MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void CadastrarNovo(ProdutoErp produtoErp) {
      try {
        Invoke(new MethodInvoker(async () => {

          using (ContextoDados db = new ContextoDados()) {
            var nivelPai = produtoErp.Nivel;
            var startIndex = dgv.Grid.CurrentRow.Index;
            var nameItemPai = Path.GetFileNameWithoutExtension(produtoErp.PathName);

            var config = db.configuracao_api.FirstOrDefault();

            // salvar engenharia
            await Loader.ShowDuringOperation(async (progress) => {
              progress.Report("Criando Engenharia de Produto...");
              var configApi = configuracao_api.Selecionar();

              await PercorrerTreeViewSalvarEngAsync(db, trvProduto.Nodes[0], configApi, progress);
            });

            MsgBox.Show("Cadastro de engenharia finalizado com sucesso", "Axion LM Projetos",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
          }
        }));
      } catch (Exception ex) {
        MsgBox.Show($"Erro ao atualizar tempalte\n\n{ex.Message}", "Axion LM Projetos",
             MessageBoxButtons.OK, MessageBoxIcon.Error);
      } finally {
        // MsgBox.CloseWaitMessage();
        //CarregarGrid();
      }
    }

    private async Task PercorrerTreeViewSalvarEngAsync(ContextoDados db, TreeNode node, configuracao_api configApi, IProgress<string> progress) {
      try {
        if (!Loader._isWorking) {
          MsgBox.Show($"Cadastro de engenharia cancelado pelo usuário.",
            "Ação não Permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }

        var produtoErp = node.Tag as ProdutoErp;
        if (produtoErp != null) {
          if (produtoErp.TipoComponente != TipoComponente.ListaMaterial && produtoErp.TipoComponente != TipoComponente.ItemBiblioteca) {
            foreach (TreeNode nodeFilho in node.Nodes) {
              await PercorrerTreeViewSalvarEngAsync(db, nodeFilho, configApi, progress);
            }

            var produto = _produtos.ToList().FirstOrDefault(x => produtoErp.Name == x.Name && produtoErp.Referencia == x.Referencia && produtoErp.Configuracao == x.Configuracao);

            if (produto != null) {
              produtoErp.CodProduto = produto.CodProduto;
              produtoErp.Denominacao = produto.Denominacao;
              produtoErp.SobremetalCompr = produto.SobremetalCompr;
              produtoErp.SobremetalLarg = produto.SobremetalLarg;
              produtoErp.Operacoes = produto.Operacoes;
            }

            var engenharia = new Engenharia {
              codEmpresa = configApi.codigoEmpresa,
              codProduto = produtoErp.CodProduto,
              tipoModulo = "S",
              codClassificacao = produtoErp.TipoComponente == TipoComponente.Montagem ? 3 : 4,
              nomeArquivoDesenhoEng = produtoErp.Name,
              descricaoProduto = $"{produtoErp.Denominacao} - {produtoErp.Name}",
              engenhariaFantasma = produtoErp.Fantasma,
              descEngenhariaFantasma = produtoErp.Fantasma ? "Engenharia Fantasma" : "",
            };

            if (node.Nodes.Count > 0) {
              System.Collections.IList list = node.Nodes;
              for (int i = 0; i < list.Count; i++) {
                int index = i + 1;
                TreeNode nodeFilho = (TreeNode)list[i];
                var itemFilho = nodeFilho.Tag as ProdutoErp;
                if (itemFilho != null) {

                  var classificacao = itemFilho.CodComponente.StartsWith("10") || itemFilho.CodComponente.StartsWith("20") ? 5 : itemFilho.TipoComponente == TipoComponente.Montagem ? 3 : 4;

                  double qtd = itemFilho.Quantidade;
                  double espessura = itemFilho.Espessura;
                  double largura = itemFilho.Largura + itemFilho.SobremetalLarg;
                  double compr = itemFilho.Comprimento + itemFilho.SobremetalCompr;

                  var componenteEng = new ComponenteEng {
                    seqComponente = index,
                    seqOperacional = itemFilho.SeqOperacional <= produtoErp.Operacoes.Count ? itemFilho.SeqOperacional : 0,
                    codInsumo = itemFilho.CodProduto,
                    quantidade = qtd,
                    itemKanban = 0,
                    comprimento = compr,
                    largura = largura,
                    espessura = espessura,
                    percQuebra = 0,
                    codClassificacaoInsumo = classificacao, // 1 = produto, 3 = subconjunto, 4 = peças e 5 = insumo comprado
                  };
                  engenharia.componentes.Add(componenteEng);
                }
              }
            }

            if (produtoErp.Operacoes != null && produtoErp.Operacoes.Count > 0) {
              for (int seqOperacao = 1; seqOperacao <= produtoErp.Operacoes.Count; seqOperacao++) {
                var proc = produtoErp.Operacoes[seqOperacao - 1];

                var processo = Processo.ListaProcessos.FirstOrDefault(x => x.codAxion == proc.processo_id);
                if (processo == null) {
                  Toast.Warning($"Processo '{proc.processo_id}' não encontrado no Axion.");
                  continue;
                }

                var operacaoEng = new OperacaoEng {
                  seqOperacao = seqOperacao,
                  codOperacao = processo.codOperacao,
                  abreviaturaOperacao = processo.abreviatura,
                  numOperadores = proc.qtd_operador,
                  codFaseOperacao = processo.faseProducao,
                  codMascaraMaquina = processo.mascaraMaquina?.Replace(".", ""),
                  centroCusto = processo.centroCusto,
                  tempoPadraoOperacao = proc.tempo.FormatarHoraDouble(),
                  tempoPreparacaoOperacao = 0,
                  tipoOperacao = processo.tipoOperacao,
                };
                engenharia.operacoes.Add(operacaoEng);
              }
            }

            progress.Report($"Cadastrando Engenharia:\r\n{produtoErp.CodProduto} - {produtoErp.Denominacao}");
            await Api.CadastrarEngenhariaAsync(db, engenharia);

            DataGridViewRow row = dgv.Grid.Rows.ToDynamicList().Where(x => ((ProdutoErp)x.DataBoundItem).CodProduto == produto?.CodProduto).FirstOrDefault();
            if (row != null) {
              var startIndex = row.Index;
              if (startIndex < dgv.Grid.Rows.Count) {
                if (startIndex < dgv.Grid.FirstDisplayedScrollingRowIndex || startIndex > dgv.Grid.FirstDisplayedScrollingRowIndex + dgv.Grid.DisplayedRowCount(false) - 1) {
                  UIThreadHelper.Invoke(dgv.Grid, () => {
                    dgv.Grid.FirstDisplayedScrollingRowIndex = startIndex;
                  });
                }

                row.DefaultCellStyle.ForeColor = row.DefaultCellStyle.SelectionForeColor = corSucesso;
              }
            }
          }
        }
      } catch (Exception ex) {
        Invoke(new MethodInvoker(() => {
          Toast.Error($"Erro ao gerar Engenharia.\n\nItem: {((ProdutoErp)node.Tag).Name}\n\n{ex.Message}");
        }));
      }
    }

    private void ClearControls() {
      flpOperacoes.Controls.Clear();//.OfType<CardOperacao>().Where(x => x.Checked).ToList().ForEach(x => x.Checked = false);
    }

    private void CarregarGrid() {
      dgv.RowIndexChanged -= Dgv_RowIndexChanged;
      dgv.CarregarGrid(_produtos);

      try {
        using (ContextoDados db = new ContextoDados()) {
          System.Collections.IList list = dgv.Grid.Rows;
          for (int i = 0; i < list.Count; i++) {
            DataGridViewRow row = (DataGridViewRow)list[i];
            var produtoErp = row.DataBoundItem as ProdutoErp;

            //row.DefaultCellStyle.ForeColor = produtoErp.CadastrarProdutoErp
            //  ? row.DefaultCellStyle.SelectionForeColor = corErro
            //  : row.DefaultCellStyle.SelectionForeColor = corSucesso;

            if (produtoErp.Operacoes != null && produtoErp.Operacoes.Count == 0 && produtoErp.TipoComponente != TipoComponente.ItemBiblioteca) {
              ProdutoErp.AdicionarPendencia(produtoErp, PendenciasEngenharia.OperacaoNaoPossui);
              //row.DefaultCellStyle.ForeColor = row.DefaultCellStyle.SelectionForeColor = corErro;
            }
            //else
            //row.DefaultCellStyle.ForeColor = row.DefaultCellStyle.SelectionForeColor = corSucesso;
          }
        }
      } catch (Exception ex) {
        Toast.Error("Erro ao verificar pendencias de processos. \r\n" + ex.Message);
      } finally { MsgBox.CloseWaitMessage(); }
      dgv.RowIndexChanged += Dgv_RowIndexChanged;
    }

    private void TxtOperacao_SelectedValueChanched(object sender, EventArgs e) {
      try {
        txtMaquina.SelectedValue = null;
        if (txtOperacao.SelectedValue != null) {

          var idOp = (int)txtOperacao.SelectedValue;
          var op = Processo.ListaOperacoesERP.Where(x => x.codOperacao == idOp).FirstOrDefault();

          if (op.tipo == "Interno") {
            txtMaquina.Enabled = true;
            var lists = Processo.ListaProcessos
              .Where(x => x.codOperacao == idOp && x.tipoOperacao == TipoOperacao.Interna)
              .Select(x => new Z_Padrao {
                Codigo = (int)x.codMaquina,
                Descricao = $"{x.codMaquina} - {x.descrMaquina}",
              })
              .ToList()
              .OrderBy(x => x.Codigo);

            txtMaquina.CarregarComboBox(lists);

            //if (lists.Count() == 1)
            txtMaquina.SelectedValue = lists.FirstOrDefault().Codigo;
          } else {
            txtMaquina.Enabled = false;
            txtMaquina.CarregarComboBox(null);
          }
        } else {
          txtMaquina.Enabled = true;
          txtMaquina.CarregarComboBox(null);
        }
      } catch (Exception ex) {
        LmException.ShowException(ex, "Erro ao selecionar Máquinas");
      }
    }

    private void BtnInserir_Click(object sender, EventArgs e) {
      try {
        if (dgv.Grid.CurrentRow == null) {
          Toast.Info($"Nenhum produto selecionado");
          return;
        }

        txtOperacao.CampoObrigatorio = true;

        if (Controles.PossuiCamposInvalidos(lmPanelOP)) {
          txtMaquina.CampoObrigatorio = txtOperacao.CampoObrigatorio = false;
          return;
        }

        var idOp = (int)txtOperacao.SelectedValue;
        var op = Processo.ListaOperacoesERP.Where(x => x.codOperacao == idOp).FirstOrDefault();

        if (op.tipo == "Interno") {
          txtMaquina.CampoObrigatorio = true;
        }

        if (Controles.PossuiCamposInvalidos(lmPanelOP)) {
          txtMaquina.CampoObrigatorio = txtOperacao.CampoObrigatorio = false;
          return;
        }

        var idMq = (int?)txtMaquina.SelectedValue;

        var proc = Processo.ListaProcessos.FirstOrDefault(x => x.codOperacao == idOp && x.codMaquina == idMq);

        if (flpOperacoes.Controls.OfType<CardOperacao>().Any(x => ((produto_erp_operacao)x.Tag).processo_id == proc.codAxion)) {
          Toast.Warning("Esta Operação com esta Máquina já foi inserida");
          return;
        }

        var produtoERP = dgv.Grid.CurrentRow.DataBoundItem as ProdutoErp;

        var processo = new produto_erp_operacao {
          processo_id = proc.codAxion,
          name = produtoERP.Name,
          referencia = produtoERP.Referencia,
          sequencia = flpOperacoes.Controls.OfType<CardOperacao>().Count() + 1,
          qtd_operador = !string.IsNullOrEmpty(txtNumeroOperadores.Text) ? Convert.ToInt32(txtNumeroOperadores.Text) : 1,
          tempo = !string.IsNullOrEmpty(txtTempoOperacao.Text) ? txtTempoOperacao.Text.FormatarHora() : "00:01",
        };

        // produto_erp_operacao.Salvar(processo);

        CardInsert(processo);

        AtualizarProcessos();

        txtMaquina.CampoObrigatorio = txtOperacao.CampoObrigatorio = false;
        txtMaquina.SelectedValue = txtOperacao.SelectedValue = null;
        txtTempoOperacao.Text = tempoPadro;
        txtNumeroOperadores.Text = numOperadorPadrao.ToString();
        txtOperacao.Focus();
      } catch (Exception ex) {
        LmException.ShowException(ex, "Erro ao inserir Operação ao Componente");
      }
    }

    private void AtualizarProcessos() {
      var produtoErp = dgv.Grid.CurrentRow.DataBoundItem as ProdutoErp;

      //produto_erp_operacao.ExcluirProcessoProduto(produtoERP);

      var operacoes = flpOperacoes.Controls
          .OfType<CardOperacao>()
          .Select(a => (produto_erp_operacao)a.Tag).ToList();

      if (operacoes != null && operacoes.Count() > 0) {
        for (int i = 1; i <= operacoes.Count; i++) {
          produto_erp_operacao operacao = operacoes[i - 1];
          operacao.sequencia = i;

          // produto_erp_operacao.Salvar(operacao);
        }
        produtoErp.Operacoes = operacoes;

        ProdutoErp.RemoverPendencia(produtoErp, PendenciasEngenharia.OperacaoRevisar);
        ProdutoErp.RemoverPendencia(produtoErp, PendenciasEngenharia.OperacaoNaoPossui);
      }
      //else if (produtoERP.TipoComponente != TipoComponente.ItemBiblioteca) {
      //  produtoERP.Operacoes = new List<produto_erp_operacao>();
      //  ProdutoErp.AdicionarPendencia(produtoERP, PendenciasEngenharia.OperacaoNaoPossui);
      //  //dgv.Grid.CurrentRow.DefaultCellStyle.ForeColor = dgv.Grid.CurrentRow.DefaultCellStyle.SelectionForeColor = corErro;
      //}

      CarregarSequenciaOp(produtoErp);
      Toast.Success("Processo atualizado com sucesso!");
    }

    private void CardInsert(produto_erp_operacao proc) {
      CardOperacao card = new CardOperacao {
        Tag = proc,
        Width = flpOperacoes.Width - 9
      };

      card.SetText();

      card.CardExclude += CardOperacaoExclude;
      card.MouseDownCtrl += CardOperacaoMouseDownCtrl;

      flpOperacoes.Controls.Add(card);
    }

    private void CardOperacaoExclude(object sender, EventArgs e) {
      this.flpOperacoes.Controls.Remove((CardOperacao)sender);
      AtualizarProcessos();
    }

    private void CardOperacaoMouseDownCtrl(object sender, MouseEventArgs e) {
      ((CardOperacao)sender).DoDragDrop((CardOperacao)sender, DragDropEffects.Move);
    }

    private void FlpOperacoes_DragEnter(object sender, DragEventArgs e) {
      if (e.Data.GetDataPresent(typeof(CardOperacao)))
        e.Effect = DragDropEffects.Move;
      else
        e.Effect = DragDropEffects.None;
    }

    private void FlpOperacoes_DragDrop(object sender, DragEventArgs e) {
      try {
        var pt = new Point(e.X, e.Y);

        var control = (CardOperacao)e.Data.GetData(typeof(CardOperacao));

        Point mousePosition = flpOperacoes.PointToClient(pt);
        Control destination = flpOperacoes.GetChildAtPoint(mousePosition);

        if (destination == null) {
          pt = new Point(pt.X, pt.Y + 10);
          mousePosition = flpOperacoes.PointToClient(pt);
          destination = flpOperacoes.GetChildAtPoint(mousePosition);
        }

        int indexDestination = flpOperacoes.Controls.IndexOf(destination);
        if (flpOperacoes.Controls.IndexOf(control) < indexDestination)
          indexDestination--;

        flpOperacoes.Controls.SetChildIndex(control, indexDestination);

        AtualizarProcessos();
      } catch (Exception ex) {
        Toast.Error(ex.Message);
      }
    }

    TreeNode GetNodeByLevelPath(TreeView treeView, string path) {
      string[] levels = path.Split('.');
      TreeNode currentNode = null;

      for (int i = 0; i < levels.Length; i++) {
        int index = int.Parse(levels[i]) - 1; // Assume que "1.3" significa índice 0,2

        if (i == 0) {
          // Nível raiz
          if (treeView.Nodes.Count > index)
            currentNode = treeView.Nodes[index];
          else
            return null;
        } else {
          // Nível filho
          if (currentNode != null && currentNode.Nodes.Count > index)
            currentNode = currentNode.Nodes[index];
          else
            return null;
        }
      }

      return currentNode;
    }

    private void LblCodigoProduto_Click(object sender, EventArgs e) {
      if (!string.IsNullOrEmpty(lblCodigoProduto.Text)) {
        Clipboard.SetText(lblCodigoProduto.Text);
        MsgBox.ShowToolTip(lblCodigoProduto, "Código copiado para área de transferência!");
      }
    }

    private void PnlDesenho_Click(object sender, EventArgs e) {
      if (dgv.Grid.CurrentRow != null) {
        var produtoErp = dgv.Grid.CurrentRow.DataBoundItem as ProdutoErp;

        if (_visualizando3D) {
          _visualizando3D = false;

          var nomeDesenho = produtoErp.PathName.Substring(0, produtoErp.PathName.Length - 6) + "SLDDRW";
          if (File.Exists(nomeDesenho)) {
            _eDrawingsCtrl.OpenDoc(nomeDesenho, false, false, false, "");
          }
          ptbDesenho.Image = produtoErp.TipoComponente == TipoComponente.Montagem ? Properties.Resources.assembly : Properties.Resources.part;
          toolTip1.SetToolTip(ptbDesenho, produtoErp.TipoComponente == TipoComponente.Montagem ? "Abrir Conjunto" : "Abrir Peça");

          int total = _eDrawingsCtrl.SheetCount;

          ptbProximoDesenho.Visible = total > 1;
          ptbZoom.Image = Properties.Resources.zoomfit;
        } else {
          _visualizando3D = true;

          _eDrawingsCtrl.OpenDoc(produtoErp.PathName, false, false, false, "");
          ptbDesenho.Image = Properties.Resources.draw;
          toolTip1.SetToolTip(ptbDesenho, "Abrir Desenho");

          ptbProximoDesenho.Visible = false;
          ptbZoom.Image = Properties.Resources.isometric;
        }
      }
    }

    private void PnlProximoDesenho_Click(object sender, EventArgs e) {
      int total = _eDrawingsCtrl.SheetCount;
      int atual = _eDrawingsCtrl.CurrentSheetIndex;
      string atualNome = _eDrawingsCtrl.SheetName[atual];

      int proxima = atual + 1;
      if (proxima >= total)
        proxima = 0; // Volta para primeira

      _eDrawingsCtrl.ShowSheet(proxima);
      Task.Delay(100);

      this.Refresh();
    }

    private void PnlRefresh_Click(object sender, EventArgs e) {
      if (_visualizando3D)
        _eDrawingsCtrl.ViewOrientation = EMVViewOrientation.eMVOrientationIsoMetric;
      _eDrawingsCtrl.ViewOrientation = EMVViewOrientation.eMVOrientationZoomToFit;
    }

    private void TxtOperacao_KeyDown(object sender, KeyEventArgs e) {
      if (e.KeyCode == Keys.Left && tbcOperacoes.SelectedIndex == 1) {
        BtnVoltar_Click(btnVoltar, new EventArgs());
      } else if (e.KeyCode == Keys.Right && tbcOperacoes.SelectedIndex == 1) {
        BtnProximo_Click(btnProximo, new EventArgs());
      }
    }

    private void PnlMedir_Click(object sender, EventArgs e) {
      if (_eDrawingsCtrl != null) {
        _eDrawingsCtrl.ShowToolbar(true);
      }
    }

    private void FrmEngenharia_SizeChanged(object sender, EventArgs e) {
      if (pnlCodigo.Top > 130) {
        flpInfos.Height = 160;
      } else if (pnlCodigo.Top > 50) {
        flpInfos.Height = 81;
      } else if (pnlCodigo.Top > 30) {
        flpInfos.Height = 60;
      } else if (pnlCodigo.Top > 15) {
        flpInfos.Height = 39;
      }
    }

    #region Sequencia Operacional

    private void CarregarSequenciaOp(ProdutoErp produtoErp) {
      if (dgv.Grid.CurrentRow != null) {
        try {
          flpEtapaConsumo.Controls.Clear();

          CriarControlesGrupoEtapas(produtoErp);

        } catch (Exception ex) {
          LmException.ShowException(ex, "Erro ao carregar sequência operacional");
        }
      }
    }

    // Criar Grupo de Etapas
    private void CriarControlesGrupoEtapas(ProdutoErp produtoErp) {
      try {
        if (produtoErp.Operacoes.Count > 0) {
          List<Processo> erp_Operacaos = new List<Processo>();
          foreach (var proc in produtoErp.Operacoes) {
            var operacao = Processo.ListaProcessos.FirstOrDefault(x => x.codAxion == proc.processo_id);
            operacao.sequencia = proc.sequencia;
            erp_Operacaos.Add(operacao);

            var flpProd = new LmPanelFlow {
              Name = "flpProc" + proc.sequencia,
              Dock = DockStyle.Fill,
              Padding = new Padding(0, 5, 0, 9),
              AllowDrop = true,
            };

            flpProd.DragEnter += Panel_DragEnter;
            flpProd.DragDrop += Panel_DragDrop;

            var ctrlOp = new LmGroupBox {
              Name = "gpbOp" + proc.sequencia,
              Size = new System.Drawing.Size(flpEtapaConsumo.Width - 24, 69),
              TabIndex = 0,
              TabStop = false,
              Text = $"{operacao.codOperacao} {operacao.descrOperacao}",
              Tag = proc.sequencia,
            };

            ctrlOp.Controls.Add(flpProd);
            flpEtapaConsumo.Controls.Add(ctrlOp);
          }

          System.Collections.IList list = trvProduto.Nodes[0].Nodes;
          for (int i = 0; i < list.Count; i++) {
            TreeNode nodeFilho = (TreeNode)list[i];
            var prod = nodeFilho.Tag as ProdutoErp;
            if (prod.SeqOperacional == 0)
              prod.SeqOperacional = 1;

            CardProduto card = new CardProduto {
              Tag = prod,
              Width = flpNaoDefinida.Width - 9
            };

            card.SetText();

            if (produtoErp.Operacoes.Count == 1)
              card.HideButtonMove();

            card.MouseDownCtrl += CardProdutoMouseDownCtrl;
            card.btnMover.MouseEnter += (s, e) => {
              var listaNova = new   List<Processo>();
              for (int i1 = 0; i1 < produtoErp.Operacoes.Count; i1++) {
                produto_erp_operacao op = produtoErp.Operacoes[i1];
                if (op.sequencia != prod.SeqOperacional) {
                  listaNova.Add(erp_Operacaos[i1] );
                }
              }
              FrmSeqSel frm = new FrmSeqSel(card.btnMover, listaNova);

              if (frm.ShowDialog() == DialogResult.OK) {
                var processoSelecionado = (Processo)frm.Tag;
                if (processoSelecionado != null) {
                  // Mover o card para o novo painel
                  var destinoPanel = flpEtapaConsumo.Controls
                      .OfType<LmGroupBox>()
                      .FirstOrDefault(x => (int)x.Tag == processoSelecionado.sequencia)
                      ?.Controls.OfType<LmPanelFlow>().FirstOrDefault();

                  if (destinoPanel != null) {
                    var painelAnterior = card.Parent as LmPanelFlow;
                    painelAnterior.Controls.Remove(card);
                    AjustarAlturaGroupBox(painelAnterior);

                    destinoPanel.Controls.Add(card);
                    AjustarAlturaGroupBox(destinoPanel);

                    // Atualizar sequência do produto
                    prod.SeqOperacional = processoSelecionado.sequencia;
                  }
                }
              }
            };

            var flowPanel = flpEtapaConsumo.Controls
              .OfType<LmGroupBox>()
              .FirstOrDefault(x => (int)x.Tag == prod.SeqOperacional).Controls
              .OfType<LmPanelFlow>().FirstOrDefault() as LmPanelFlow;
            if (flowPanel == null) {
              flowPanel = flpEtapaConsumo.Controls
             .OfType<LmGroupBox>()
             .FirstOrDefault().Controls
             .OfType<LmPanelFlow>().FirstOrDefault() as LmPanelFlow;
            }
            flowPanel.Controls.Add(card);
            AjustarAlturaGroupBox(flowPanel);
          }
        }
      } catch (Exception ex) {
        LmException.ShowException(ex, "Erro ao criar grupo de etapas");
      }
    }

    private void CardProdutoMouseDownCtrl(object sender, MouseEventArgs e) {
      ((CardProduto)sender).DoDragDrop((CardProduto)sender, DragDropEffects.Move);
    }

    private void Panel_DragEnter(object sender, DragEventArgs e) {
      if (e.Data.GetDataPresent(typeof(CardProduto))) {
        e.Effect = DragDropEffects.Move;
      } else {
        e.Effect = DragDropEffects.None;
      }
    }

    private void Panel_DragDrop(object sender, DragEventArgs e) {
      if (e.Data.GetDataPresent(typeof(CardProduto))) {
        var card = (CardProduto)e.Data.GetData(typeof(CardProduto));
        var destino = (LmPanelFlow)sender;

        // Remover do pai atual
        if (card.Parent != null) {
          var painelAnterior = card.Parent as LmPanelFlow;
          painelAnterior.Controls.Remove(card);
          AjustarAlturaGroupBox(painelAnterior);
        }

        // Adicionar ao novo painel
        destino.Controls.Add(card);

        // Opcional: alinhar ao topo
        destino.Controls.SetChildIndex(card, 0);
        AjustarAlturaGroupBox(destino);
        var prod = card.Tag as ProdutoErp;
        var seq = Convert.ToInt32(destino.Parent.Tag);
        prod.SeqOperacional = seq;
      }
    }

    private void AjustarAlturaGroupBox(LmPanelFlow painel) {
      if (painel.Parent is LmGroupBox grupo) {
        int alturaTotal = 0;

        foreach (Control ctrl in painel.Controls) {
          alturaTotal += ctrl.Height + ctrl.Margin.Vertical;
        }

        // Padding extra (opcional para manter um respiro visual)
        alturaTotal += painel.Padding.Vertical + 20;

        // Definir altura mínima
        grupo.Height = Math.Max(69, alturaTotal);
      }
    }

    #endregion
  }
}
