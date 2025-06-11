using LmCorbieUI.Metodos.AtributosCustomizados;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using LmCorbieUI;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System;
using System.Linq.Dynamic.Core;

namespace AxionConnect {
  internal class materiais {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DataObjectField(true, false)]
    [LarguraColunaGrid(80)]
    public int id { get; set; }

    [DataObjectField(false, true)]
    [Required(ErrorMessage = "Campo \"Descrição\" é Obrigatório!")]
    [StringLength(250)]
    [LarguraColunaGrid(500)]
    [DisplayName("Descrição")]
    public string descricao { get; set; }

    [LarguraColunaGrid(80)]
    [DisplayName("Ativo")]
    public bool ativo { get; set; }

    public static bool Salvar(materiais material) {
      try {
        using (ContextoDados db = new ContextoDados()) {
          if (material.id == 0) {
            if (db.materiais.Any(x => x.descricao == material.descricao)) {
              Toast.Warning("Já existe um material cadastradao com esta descrição");
              return false;
            }

            db.materiais.Add(material);
            db.SaveChanges();

            Toast.Success("Material Cadastrado com Sucesso!");
          } else {
            if (db.materiais.Any((x => x.descricao == material.descricao && x.id != material.id))) {
              Toast.Warning("Já existe um material cadastrado com esta descrição");
              return false;
            }

            var modelAlt = db.materiais.FirstOrDefault(x => x.id == material.id);
            modelAlt.descricao = material.descricao;
            modelAlt.ativo = material.ativo;

            db.SaveChanges();
            Toast.Success("Material Alterado com Sucesso!");
          }
          return true;
        }
      } catch (Exception ex) {
        MsgBox.Show("Erro ao Salvar Material.", "Axion LM Projetos", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }
    }

    public static List<materiais> Selecionar(bool? ativo = null) {
      var _return = new List<materiais>();

      try {
        using (ContextoDados db = new ContextoDados()) {
          var condicoes = string.Empty;
          var valores = new object[10];
          short pos = 0;

          condicoes += $"Id > @{pos} && ";
          valores[pos] = 0;
          pos++;

          if (ativo != null) {
            condicoes += $"Ativo == @{pos} && ";
            valores[pos] = ativo.Value;
            pos++;
          }
          condicoes = condicoes.Substring(0, condicoes.Length - 3);

          _return = Enumerable.ToList(db.materiais.Where(condicoes, valores).OrderBy(x => x.descricao));
        }
      } catch (Exception ex) {
        Toast.Warning("Erro ao selecionar material.\n" +
            "-------------------------------------\n" +
            "" + ex.Message);
        _return = new List<materiais>();
      }

      return _return;
    }

    public static materiais Selecionar(int id) {
      var _return = new materiais();

      try {
        using (ContextoDados db = new ContextoDados()) {
          _return = Queryable.FirstOrDefault(db.materiais.Where(x => x.id == id));
        }
      } catch (Exception ex) {
        Toast.Warning("Erro ao selecionar material.\n" +
            "-------------------------------------\n" +
            "" + ex.Message);
        _return = null;
      }

      return _return;
    }
  }
}
