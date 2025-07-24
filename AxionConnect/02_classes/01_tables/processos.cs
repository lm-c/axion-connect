using LmCorbieUI;
using LmCorbieUI.Metodos.AtributosCustomizados;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Windows.Forms;

namespace AxionConnect {
  internal class processos {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DataObjectField(true, false)]
    [LarguraColunaGrid(80)]
    [DisplayName("Código")]
    public int id { get; set; }

    [LarguraColunaGrid(120)]
    [DisplayName("Código Operação")]
    public int codigo_operacao { get; set; }

    [LarguraColunaGrid(120)]
    [DisplayName("Código Máquina")]
    public int? codigo_maquina { get; set; }

    [LarguraColunaGrid(120)]
    [DisplayName("Tipo Operação")]
    public TipoOperacao tipoOperacao { get; set; }

    [LarguraColunaGrid(80)]
    public bool ativo { get; set; }

    public static List<processos> SelecionarTodos() {
      var _return = new List<processos>();

      try {
        using (ContextoDados db = new ContextoDados()) {
          _return = Enumerable.ToList(
           db.processos);
        }
      } catch (Exception ex) {
        LmException.ShowException(ex, "Erro ao Retornar Processos");
      }

      return _return;
    }

    public static processos SelecionarProcesso(int id) {
      var _return = new processos();

      try {
        using (ContextoDados db = new ContextoDados()) {
          _return = Queryable.FirstOrDefault(
           db.processos.Where(x => x.id == id));
        }
      } catch (Exception ex) {
        LmException.ShowException(ex, "Erro ao Retornar Processo");
      }

      return _return;
    }
  }
}
