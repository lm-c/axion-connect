using LmCorbieUI.Metodos.AtributosCustomizados;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxionConnect {
  public class W_UsuarioPerfil {
    [LarguraColunaGrid(80)]
    [DisplayName("Código")]
    [DataObjectField(true, false)]
    [AlinhamentoColunaGrid(System.Windows.Forms.DataGridViewContentAlignment.MiddleRight)]
    public int Codigo { get; set; }

    [LarguraColunaGrid(200)]
    [DisplayName("Descrição")]
    [DataObjectField(false, true)]
    public string Descricao { get; set; }

    [LarguraColunaGrid(500)]
    [DisplayName("Observação")]
    public string Observacao { get; set; }

    [Browsable(false)]
    public string Permissoes { get; set; }
  }
}
