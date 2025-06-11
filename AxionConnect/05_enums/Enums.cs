using System.ComponentModel;

namespace AxionConnect {
  public enum TipoListaMaterial {
    [Description("Chapa")]
    Chapa = 0,
    [Description("Laminados")]
    Soldagem = 1
  }

  public enum SwDwgPaperSizes_e {
    A0 = 0,
    A1 = 1,
    A2 = 2,
    A3 = 3,
    A4P = 4,
    A4R = 5,
  }
  public enum TipoComponente {
    [Description("Montagem")]
    Montagem = 0,
    [Description("Peça")]
    Peca = 1,
    [Description("Lista de material")]
    ListaMaterial = 2,
    [Description("Item de Biblioteca")]
    ItemBiblioteca = 3,
  }

  public enum SeriadoFiltro {
    [Description("Todos")]
    Todos = 0,
    Sim = 1,
    [Description("Não")]
    Nao = 2,
    Vazia = 3
  }

}
