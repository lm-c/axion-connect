using LmCorbieUI;
using System.Windows.Forms;
using System;
using System.Linq;

namespace AxionConnect {
  internal class templates {
    public int id { get; set; }
    public string formato_a4r { get; set; }
    public string formato_a4p { get; set; }
    public string formato_a3 { get; set; }
    public string formato_a2 { get; set; }
    public string formato_a1 { get; set; }
    public string formato_a0 { get; set; }

    public string template_a4r { get; set; }
    public string template_a4p { get; set; }
    public string template_a3 { get; set; }
    public string template_a2 { get; set; }
    public string template_a1 { get; set; }
    public string template_a0 { get; set; }

    public string lista_montagem { get; set; }
    public string lista_soldagem {
      get; set;
    }
    public static templates model = new templates();

    public static bool Salvar(templates model) {
      try {
        using (ContextoDados db = new ContextoDados()) {
          int id = 1;

          var x = db.templates.FirstOrDefault();

          if (x != null)
            id = x.id + 1;

          model.id = id;
          db.templates.Add(model);

          db.SaveChanges();

          return true;
        }
      } catch (Exception ex) {
        MsgBox.Show($"Erro ao salvar\n\n{ex.Message}", "Axion LM Projetos",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }
    }

    public static bool Alterar(templates model) {
      try {
        using (ContextoDados db = new ContextoDados()) {
          int id = model.id;

          templates row = db.templates.Where(x => x.id == id).FirstOrDefault();

          if (row != null) {
            row.id = model.id;
            row.formato_a4r = model.formato_a4r;
            row.formato_a4p = model.formato_a4r;
            row.formato_a3 = model.formato_a3;
            row.formato_a2 = model.formato_a2;
            row.formato_a1 = model.formato_a1;
            row.formato_a0 = model.formato_a0;
            row.template_a4r = model.template_a4r;
            row.template_a4p = model.template_a4p;
            row.template_a3 = model.template_a3;
            row.template_a2 = model.template_a2;
            row.template_a1 = model.template_a1;
            row.template_a0 = model.template_a0;
            row.lista_montagem = model.lista_montagem;
            row.lista_soldagem = model.lista_soldagem;

            db.SaveChanges();
            return true;
          } else
            return false;
        }
      } catch (Exception ex) {
        MsgBox.Show($"Erro ao alterar\n\n{ex.Message}", "Axion LM Projetos",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }
    }

    public static void Carregar() {
      using (ContextoDados db = new ContextoDados()) {
        model = db.templates.FirstOrDefault();
        if (model == null)
          model = new templates();
      }
    }
  }
}
