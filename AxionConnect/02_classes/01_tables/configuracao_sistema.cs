using LmCorbieUI.Metodos.AtributosCustomizados;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using LmCorbieUI;
using System.Linq;
using System.Windows.Forms;
using System;
using System.Linq.Dynamic.Core;
using System.Threading;

namespace AxionConnect {
  internal class configuracao_sistema {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int usuario_id { get; set; } = 0;

    [Browsable(false)]
    public bool menu_apenas_icone { get; set; } = false;

    public static configuracao_sistema model = new configuracao_sistema();

    public static void Salvar() {
      Thread t = new Thread(() => { SalvarAsync(); }) { IsBackground = true };
      t.Start();
    }

    public static void Carregar() {
      try {
        using (ContextoDados db = new ContextoDados()) {
          model = db.configuracao_sistema.Where(x => x.usuario_id == usuario_alocados.model.usuario_id).FirstOrDefault();

          if (model == null)
            model = new configuracao_sistema();
        }
      } catch (Exception ex) {
        MessageBox.Show("Aconteceu um Erro ao Retornar Configurações do Sistema, algumas predefinições de usuário podem não ter sidas carregadas.\n" +
            "-------------------------------------\n" +
            "" + ex.Message,
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    private static void SalvarAsync() {
      try {
        using (ContextoDados db = new ContextoDados()) {
          var valPredef = db.configuracao_sistema.Where(x => x.usuario_id == usuario_alocados.model.usuario_id).FirstOrDefault();

          if (valPredef == null) {
            model.usuario_id = usuario_alocados.model.usuario_id;
            db.configuracao_sistema.Add(model);
          } else {
            valPredef.usuario_id = model.usuario_id;
            valPredef.menu_apenas_icone = model.menu_apenas_icone;
          }

          db.SaveChanges();
        }

      } catch (Exception ex) {
        MessageBox.Show("Aconteceu um Erro ao Salvar Configurações do Sistema, algumas predefinições de usuário podem não ter sidas salvas.\n" +
            "-------------------------------------\n" +
            "" + ex.Message,
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }
  }
}
