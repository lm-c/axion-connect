using LmCorbieUI.Metodos.AtributosCustomizados;
using LmCorbieUI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxionConnect {
  internal class produto_erp_operacao {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    [DataObjectField(true, false)]
    public int id { get; set; }

    public int sequencia { get; set; }

    [StringLength(50)]
    public string name { get; set; }

    [StringLength(50)]
    public string referencia { get; set; }

    [StringLength(10)]
    public string tempo { get; set; }

    public int qtd_operador { get; set; }

    public int processo_id { get; set; }
    [ForeignKey("processo_id"), Browsable(false), NaoVerificarAlteracao]
    public processos processo { get; set; }

    //public static bool Salvar(produto_erp_operacao processo) {
    //  try {
    //    using (ContextoDados db = new ContextoDados()) {

    //      db.produto_erp_operacao.Add(processo);
    //      db.SaveChanges();

    //      return true;
    //    }
    //  } catch (Exception ex) {
    //    MsgBox.Show("Erro ao Salvar Processo.", "Axion LM Projetos", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //    return false;
    //  }
    //}

    //public static void ExcluirProcessoProduto(ProdutoErp produtoErp) {
    //  try {
    //    using (ContextoDados db = new ContextoDados()) {
    //      db.produto_erp_operacao.RemoveRange(db.produto_erp_operacao.Where(x => x.name == produtoErp.Name && x.referencia == produtoErp.Referencia).ToList());
    //      db.SaveChanges();
    //    }
    //  } catch (Exception ex) {
    //    MsgBox.Show("Erro ao Excluir Usuario.", "Axion LM Projetos", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //  }
    //}

    //public static void SelecionarProcessoProduto(ProdutoErp produtoErp) {
    //  var ops = new List<produto_erp_operacao>();

    //  try {
    //    using (ContextoDados db = new ContextoDados()) {
    //      ops = Enumerable.ToList(
    //       db.produto_erp_operacao.Where(x => x.name == produtoErp.Name && x.referencia == produtoErp.Referencia).OrderBy(x => x.sequencia));

    //      produtoErp.Operacoes = ops;
    //    }
    //  } catch (Exception ex) {
    //    LmException.ShowException(ex, "Erro ao Retornar Processos");
    //  }
    //}

  }
}
