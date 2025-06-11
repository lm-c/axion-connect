using LmCorbieUI;
using LmCorbieUI.Metodos;
using LmCorbieUI.Metodos.AtributosCustomizados;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static AxionConnect.Api;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace AxionConnect {
  internal class ProdutoErp {

    [LarguraColunaGrid(25)]
    [DisplayName(" "), ToolTipGrid("Abrir 3D")]
    [AlinhamentoColunaGrid(System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter)]
    public Bitmap Img3D { get; set; } = new Bitmap(20, 20);

    [LarguraColunaGrid(25)]
    [DisplayName(" "), ToolTipGrid("Abrir 2D")]
    [AlinhamentoColunaGrid(System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter)]
    public Bitmap Img2D { get; set; } = new Bitmap(20, 20);

    [LarguraColunaGrid(25)]
    [DisplayName(" "), ToolTipGrid("Item Fantasma")]
    [AlinhamentoColunaGrid(System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter)]
    public Bitmap ImgFantasma { get; set; } = new Bitmap(20, 20);

    [LarguraColunaGrid(25)]
    [DisplayName(" "), ToolTipGrid("Pendências")]
    [AlinhamentoColunaGrid(System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter)]
    public Bitmap ImgPendencia { get; set; } = new Bitmap(20, 20);

    //[Browsable(false)]
    [DisplayName("Nível")]
    [LarguraColunaGrid(60)]
    public string Nivel { get; set; }

    [DisplayName("Nome Componente")]
    [LarguraColunaGrid(120)]
    public string Name { get; set; }

    [Browsable(false)]
    [DisplayName("Cód Componente")]
    [LarguraColunaGrid(120)]
    public string CodComponente { get; set; }

    [DisplayName("Cód Produto")]
    [LarguraColunaGrid(120)]
    public string CodProduto { get; set; }

    [DisplayName("Descrição Produto")]
    [LarguraColunaGrid(350)]
    public string Denominacao { get; set; }

    //[Browsable(false)]
    [DisplayName("Referência")]
    [LarguraColunaGrid(150)]
    public string Referencia { get; set; }

    [Browsable(false)]
    [DisplayName("Configuração")]
    [LarguraColunaGrid(150)]
    public string Configuracao { get; set; }

    [Browsable(false)]
    public double Espessura { get; set; }

    [Browsable(false)]
    public double Largura { get; set; }

    [Browsable(false)]
    public double Comprimento { get; set; }

    [Browsable(false)]
    [DisplayName("QTD")]
    [LarguraColunaGrid(50)]
    public double Quantidade { get; set; }

    [Browsable(false)]
    [DisplayName("UM")]
    [LarguraColunaGrid(50)]
    public string UnidadeMedida { get; set; }

    [Browsable(false)]
    public double PesoBruto { get; set; }

    [Browsable(false)]
    public double PesoLiquido { get; set; }

    [Browsable(false)]
    public string PathName { get; set; }

    [Browsable(false)]
    public TipoComponente TipoComponente { get; set; }

    public TipoListaMaterial TipoListaMaterial { get; set; }

    [Browsable(false)]
    public double SobremetalLarg { get; set; } = 0;

    [Browsable(false)]
    public double SobremetalCompr { get; set; } = 0;

    [Browsable(false)]
    public bool CadastrarProdutoErp { get; set; }

    [Browsable(false)]
    public bool CadastrarAddin { get; set; }

    [Browsable(false)]
    public bool Fantasma { get; set; }

    public List<PendenciasEngenharia> Pendencias = new List<PendenciasEngenharia>();
    public List<produto_erp_operacao> Operacoes = new List<produto_erp_operacao>();

    public static async Task<SortableBindingList<ProdutoErp>> GetComponentsFromERPAsync(TreeView treeView, long codProduto) {
      var _listaProduto = new List<ProdutoErp>();
      try {
        using (ContextoDados db = new ContextoDados()) {
          treeView.Nodes.Clear(); // Limpa a árvore antes de começar
          int contador = 0;
          await AdicionarEngenhariaNaArvoreAsync(db, treeView.Nodes, codProduto);

          MsgBox.ShowWaitMessage("Analisando componentes...");
          await PercorrerTreeViewAnalisarCompAsync(db, treeView.Nodes[0], _listaProduto);
        }
      } catch (Exception ex) {
        MsgBox.Show($"Erro ao ler componentes do ERP\n\n{ex.Message}", "Axion LM Projetos",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      treeView.ExpandAll();
      return new SortableBindingList<ProdutoErp>(_listaProduto);
    }

    private static async Task AdicionarEngenhariaNaArvoreAsync(ContextoDados db, TreeNodeCollection nodes, long codProduto, ProdutoErp produtoPai = null, ComponenteEng compEng = null) {
      var engenharia = await Api.GetEngenhariaAsync(codProduto.ToString());

      var prod = db.produto_erp.FirstOrDefault(x => x.codigo_produto == codProduto);

      // Gerar o nível atual (seja produto normal ou item genérico)
      string nivelAtual = produtoPai == null
        ? (nodes.Count + 1).ToString()
        : $"{produtoPai.Nivel}.{nodes.Count + 1}";

      if (prod == null && codProduto.ToString().StartsWith("10")) {
        var itemGenerico = await Api.GetItemGenericoAsync(codProduto.ToString());

        if (itemGenerico != null) {
          ProdutoErp produtoErpFilho = new ProdutoErp {
            Nivel = nivelAtual,
            CodComponente = itemGenerico.codigo.ToString(),
            CodProduto = itemGenerico.codigo.ToString(),
            Name = itemGenerico.codigo.ToString(),
            Denominacao = itemGenerico.nome,
            Configuracao = "",
            Referencia = "",
            UnidadeMedida = itemGenerico?.unidadeMedida,
            PesoBruto = itemGenerico.pesoBruto,
            PesoLiquido = itemGenerico.pesoLiquido,
            PathName = "",
            TipoComponente = TipoComponente.ListaMaterial,
            SobremetalLarg = 0,
            SobremetalCompr = 0,
            Quantidade = compEng.quantidade,
            Espessura = compEng.espessura,
            Largura = compEng.largura,
            Comprimento = compEng.comprimento,
            Fantasma = false,
          };

          int imgIndex = produtoPai.TipoListaMaterial == TipoListaMaterial.Chapa ? 3 : 4;

          TreeNode nodeFilho = nodes.Add(nivelAtual, $"{produtoErpFilho.Name} - {produtoErpFilho.Denominacao}", 0);
          nodeFilho.ImageIndex = imgIndex;
          nodeFilho.SelectedImageIndex = imgIndex;
          nodeFilho.Tag = produtoErpFilho;
        }
      } else {
        ProdutoErp produtoErp = new ProdutoErp {
          Nivel = nivelAtual,
          CodComponente = prod.codigo_componente.ToString(),
          Name = prod.name,
          CodProduto = prod.codigo_produto.ToString(),
          Denominacao = prod.descricao,
          Configuracao = prod.configuracao,
          Referencia = prod.referencia,
          UnidadeMedida = engenharia?.unidadeMedida,
          Quantidade = compEng != null ? compEng.quantidade : 1,
          Espessura = prod.espessura,
          Largura = prod.largura,
          Comprimento = prod.comprimento,
          PesoBruto = prod.peso_bruto,
          PesoLiquido = prod.peso_liquido,
          PathName = prod.pathname,
          TipoComponente = prod.tipo_componente,
          SobremetalLarg = prod.sobremetal_largura,
          SobremetalCompr = prod.sobremetal_comprimento,
          Fantasma = prod.fantasma,
          TipoListaMaterial = prod.espessura > 0 && prod.largura > 0 ? TipoListaMaterial.Chapa : TipoListaMaterial.Soldagem,
        };

        produto_erp_operacao.SelecionarProcessoProduto(produtoErp);

        var iconIndex = produtoErp.TipoComponente == TipoComponente.ItemBiblioteca ? 5 : produtoErp.TipoComponente == TipoComponente.Montagem ? 0 : 1;

        TreeNode node = nodes.Add($"{nivelAtual}", $"{prod.name} - {prod.descricao}", 0);
        node.ImageIndex = iconIndex;
        node.SelectedImageIndex = iconIndex;
        node.Tag = produtoErp;

        // Recursivamente adiciona os filhos
        if (engenharia != null) {
          await VerificarOperacaoERPAsync(db, produtoErp, engenharia?.operacoes);

          foreach (var item in engenharia?.componentes) {
            //string nivelFilho = $"{nivelAtual}.{indiceFilho}";
            long codFilho = Convert.ToInt64(item.codInsumo);

            // Aqui você chama recursivamente o método para adicionar os filhos do componente
            await AdicionarEngenhariaNaArvoreAsync(db, node.Nodes, codFilho, produtoErp, item);
          }
        }
      }
    }

    private static async Task VerificarOperacaoERPAsync(ContextoDados db, ProdutoErp produtoErp, List<OperacaoEng> engenhariaOperacoes) {
      if (engenhariaOperacoes != null && engenhariaOperacoes.Count > 0) {
        // verificar alteração de operações no erp ou solid
        foreach (var operacao in engenhariaOperacoes) {
          var op = Processo.ListaProcessos.FirstOrDefault(x => x.codOperacao == operacao.codOperacao && x.mascaraMaquina == operacao.codMascaraMaquina);
          var minhasOps = produtoErp.Operacoes.Select(x => x.processo_id).ToList();

          if (op != null && !minhasOps.Contains(op.codAxion)) {
            AtualizarOperacao(produtoErp, operacao, op.codAxion);
          } else if (op == null) {
            var opCad = Processo.ListaOperacoesERP.FirstOrDefault(x => x.codOperacao == operacao.codOperacao);
            var maCad = Processo.ListaMaquinasERP.FirstOrDefault(x => x.mascara == operacao.codMascaraMaquina);
            if (opCad == null || maCad == null)
              continue;

            var processo = new processos {
              codigo_maquina = maCad.codMaquina,
              codigo_operacao = opCad.codOperacao,
              carregarNaAplicacaoProcesso = false,
              ativo = true,
            };

            db.processos.Add(processo);
            db.SaveChanges();

            await Processo.Carregar();

            AtualizarOperacao(produtoErp, operacao, processo.id);

            // atualizar props
            if (!minhasOps.Contains(processo.codigo_operacao)) {
              AtualizarOperacao(produtoErp, operacao, processo.id);
            }
          }
        }
      } else produtoErp.CadastrarProdutoErp = true;
    }

    internal static void AdicionarPendencia(ProdutoErp produtoErp, PendenciasEngenharia pendencia) {
      if (!produtoErp.Pendencias.Contains(pendencia)) {
        produtoErp.Pendencias.Add(pendencia);
        produtoErp.ImgPendencia = pendencia.EhPendenciaCritica() || produtoErp.Pendencias.Any(x => x.EhPendenciaCritica()) ? Properties.Resources.error : Properties.Resources.warning;
      }
    }

    internal static void RemoverPendencia(ProdutoErp produtoErp, PendenciasEngenharia pendencia) {
      var list = produtoErp.Pendencias.Where(x => x == pendencia);
      list.ToList().ForEach(x => { produtoErp.Pendencias.Remove(x); });

      if (!produtoErp.Pendencias.Any())
        produtoErp.ImgPendencia = new Bitmap(20, 20);
    }

    private static void AtualizarOperacao(ProdutoErp produtoErp, OperacaoEng operacao, int codAxion) {
      produtoErp.Operacoes.Add(new produto_erp_operacao {
        sequencia = operacao.seqOperacao,
        processo_id = codAxion,
        name = produtoErp.Name,
        referencia = produtoErp.Referencia,
        tempo = operacao.tempoPadraoOperacao.FormatarHora(),
        qtd_operador = Convert.ToInt32(operacao.numOperadores),
      });
    }

    private static async Task PercorrerTreeViewAnalisarCompAsync(ContextoDados db, TreeNode node, List<ProdutoErp> _listaProduto) {
      try {
        var produtoErp = node.Tag as ProdutoErp;
        if (produtoErp != null) {
          if (!_listaProduto.Any(x => x.Name == produtoErp.Name && x.Referencia == produtoErp.Referencia && x.Configuracao == produtoErp.Configuracao)) {
            if (produtoErp.TipoComponente != TipoComponente.ListaMaterial) {
              var nameDesenho = produtoErp.PathName.Substring(0, produtoErp.PathName.Length - 6) + "SLDDRW";
              produtoErp.Img3D = produtoErp.TipoComponente == TipoComponente.Montagem ? Properties.Resources.assembly : Properties.Resources.part;
              produtoErp.Img2D = File.Exists(nameDesenho) ? Properties.Resources.draw : Properties.Resources.not_draw;
              _listaProduto.Add(produtoErp);
            }

            if (node.Nodes.Count > 0) {
              foreach (TreeNode nodeFilho in node.Nodes) {
                await PercorrerTreeViewAnalisarCompAsync(db, nodeFilho, _listaProduto);
              }
            }
          }
        }
      } catch (Exception ex) {
        Toast.Error("Erro ao fazer análise:\r\n" + ex.Message);
      }
    }

  }
}
