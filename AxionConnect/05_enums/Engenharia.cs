using System.ComponentModel;

namespace AxionConnect {
  public enum TipoMateriaPrima {
    [Description("Soldagem")]
    Soldagem = 0,
    [Description("Chapa")]
    Chapa = 1,
  }

  public enum TipoDocumento {
    [Description("Peça")]
    Peca = 0,
    [Description("Montagem")]
    Montagem = 1,
  }

  public enum TipoSequencia {
    [Description("Processamento")]
    Processamento = 0,
    [Description("Destino")]
    Destino = 1,
  }

  public enum TipoOperacao {
    Interna = 0,
    Externa = 1,
  }

  public enum TipoLogEngenharia {
    [Description("Duplicação de Produto")]
    DuplicacaoProduto = 0,
    [Description("Pendência de Engenharia")]
    PendenciEngenharia = 1,
  }

  public enum PendenciaCritica {
    Nao = 0,
    Sim = 1,
  }

  public enum PendenciasEngenharia {
    [Description("Nescessário revisar operações"), TipoPendencia(PendenciaCritica.Sim)]
    OperacaoRevisar = 0,
    [Description("Não possui operações"), TipoPendencia(PendenciaCritica.Sim)]
    OperacaoNaoPossui = 1,
    [Description("Aberto como somente leitura"), TipoPendencia(PendenciaCritica.Nao)]
    SomenteLeitura = 2,
    [Description("Material não cadastrado no ERP"), TipoPendencia(PendenciaCritica.Sim)]
    MateriaPrimaInexistente = 3,
    [Description("Produto Sofreu Alteração"), TipoPendencia(PendenciaCritica.Nao)]
    ItemAlterado = 4,
    [Description("Material errado no projeto"), TipoPendencia(PendenciaCritica.Sim)]
    MateriaErrado = 5,
  }
}