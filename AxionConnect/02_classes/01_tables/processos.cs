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
    public int codigo_maquina { get; set; }

    [LarguraColunaGrid(80)]
    [DisplayName("Mostrar Processo na Inserção de Processo")]
    public bool carregarNaAplicacaoProcesso { get; set; }
    
    [LarguraColunaGrid(80)]
    public bool ativo { get; set; }

    public static bool Salvar(processos processo) {
      try {
        using (ContextoDados db = new ContextoDados()) {
          if (processo.id == 0) {
            if (db.processos.Any(x => x.codigo_operacao == processo.codigo_operacao)) {
              Toast.Warning("Já existe um registro com este processo vinculado a uma máquina");
              return false;
            }

            db.processos.Add(processo);
            db.SaveChanges();

            Toast.Success("Processo Cadastrado com Sucesso!");
          } else {
            if (db.processos.Any((x => x.id != processo.id && x.codigo_operacao == processo.codigo_operacao))) {
              Toast.Warning("Já existe um registro com este processo vinculado a uma máquina");
              return false;
            }

            var modelAlt = db.processos.FirstOrDefault(x => x.id == processo.id);
            modelAlt.codigo_operacao = processo.codigo_operacao;
            modelAlt.codigo_maquina = processo.codigo_maquina;
            modelAlt.ativo = processo.ativo;

            db.SaveChanges();

            Toast.Success("Processo Alterado com Sucesso!");
          }

          return true;
        }
      } catch (Exception ex) {
        MsgBox.Show("Erro ao Salvar Processo.", "Axion LM Projetos", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }
    }

    public static void Excluir(int processo_id) {
      try {
        using (ContextoDados db = new ContextoDados()) {
          db.processos.Remove(db.processos.FirstOrDefault(x => x.id == processo_id));
          db.SaveChanges();
        }
      } catch (Exception ex) {
        MsgBox.Show("Erro ao Excluir Usuario.", "Axion LM Projetos", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

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
