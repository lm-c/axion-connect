using LmCorbieUI;
using LmCorbieUI.Metodos;
using LmCorbieUI.Metodos.AtributosCustomizados;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxionConnect {
  internal class Processo {
    [DisplayName("Código Interno")]
    [LarguraColunaGrid(120)]
    public int codAxion { get; set; }

    [DisplayName("Código Operação")]
    [LarguraColunaGrid(120)]
    [DataObjectField(true, false)]
    [AlinhamentoColunaGrid(System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter)]
    public int codOperacao { get; set; }

    [Browsable(false)]
    [DisplayName("Abreviatura")]
    [LarguraColunaGrid(120)]
    public string abreviatura { get; set; }

    [DisplayName("Descrição Operação")]
    [LarguraColunaGrid(200)]
    [DataObjectField(false, true)]
    public string descrOperacao { get; set; }

    [DisplayName("Código Máquina")]
    [LarguraColunaGrid(120)]
    public int codMaquina { get; set; }

    [DisplayName("Descrição Máquina")]
    [LarguraColunaGrid(150)]
    public string descrMaquina { get; set; }

    [DisplayName("Máscara Máquina")]
    [LarguraColunaGrid(120)]
    public string mascaraMaquina { get; set; }

    [DisplayName("Tipo")]
    [LarguraColunaGrid(100)]
    public string tipo { get; set; }

    [DisplayName("Cenytro de Custo")]
    [LarguraColunaGrid(100)]
    public string centroCusto { get; set; }

    [Browsable(false)]
    [DisplayName("Fase produção")]
    [LarguraColunaGrid(120)]
    public int faseProducao { get; set; }

    [Browsable(false)]
    public bool carregarControleNaAplicacaoProcesso { get; set; } = false;

    public static List<Processo> ListaProcessos { get; set; } = new List<Processo>();
    public static List<Api.Operacao> ListaOperacoesERP { get; set; } = new List<Api.Operacao>();
    public static List<Api.Maquina> ListaMaquinasERP { get; set; } = new List<Api.Maquina>();

    public static async Task Carregar() {
      try {
        ListaProcessos = new List<Processo>();

        ListaOperacoesERP = await Api.GetOpsAsync();
        ListaMaquinasERP = await Api.GetMaquinasAsync();

        var procs = processos.SelecionarTodos();

        foreach (var processo in procs) {
          var operacao = ListaOperacoesERP.FirstOrDefault(x => x.codOperacao == processo.codigo_operacao);
          var maquina = ListaMaquinasERP.FirstOrDefault(x => x.codMaquina == processo.codigo_maquina);

          ListaProcessos.Add(new Processo {
            codAxion = processo.id,
            codOperacao = operacao.codOperacao,
            abreviatura = operacao.abreviatura,
            descrOperacao = operacao.descricao.Replace("\\", "_").Replace("/", "_"),
            descrMaquina = maquina.descricao.Replace("\\", "_").Replace("/", "_"),
            mascaraMaquina = maquina.mascara,
            codMaquina = maquina.codMaquina,
            tipo = operacao.tipo,
            centroCusto = maquina.centroCusto,
            faseProducao = operacao.faseProducao ?? 0, // Default para 0 se for nulo
            carregarControleNaAplicacaoProcesso = processo.carregarNaAplicacaoProcesso,
          });
        }
        ListaProcessos = ListaProcessos.OrderByDescending(x => x.tipo).ThenBy(x => x.codOperacao).ToList();
      } catch (Exception ex) {
        Toast.Error("Erro ao Selecionar Processos");
      }
    }

    public static async Task<SortableBindingList<ProdutoErp>> GetProdutos() {
      var _listaProduto = new List<ProdutoErp>();

      try {
        MsgBox.ShowWaitMessage("Lendo componentes...");

      } catch (Exception ex) {
        MsgBox.Show($"Erro ao carregar componentes.\n\n{ex.Message}", "Axion LM Projetos",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      } finally {
        MsgBox.CloseWaitMessage();
      }
      return new SortableBindingList<ProdutoErp>(_listaProduto);
    }
  }
}
