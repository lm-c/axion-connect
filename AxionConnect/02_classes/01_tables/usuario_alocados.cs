using LmCorbieUI.Metodos.AtributosCustomizados;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AxionConnect {
  internal class usuario_alocados {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DataObjectField(true, false)]
    public int id  { get; set; }

    [DataObjectField(false, true)]
    [StringLength(250)]
    public string hostname { get; set; }

    [StringLength(250)]
    public string usuario_pc { get; set; }

    public int usuario_id { get; set; }
    [ForeignKey("usuario_id"), Browsable(false), NaoVerificarAlteracao]
    public usuarios usuario { get; set; }

    public static usuario_alocados model = null;

    internal static void Deslogar() {
      using(ContextoDados db = new ContextoDados()) {
        db.usuario_alocados.Remove(db.usuario_alocados.FirstOrDefault(x=>x.id == model.id));
        db.SaveChanges();
        model = null;
      }
    }
  }
}
