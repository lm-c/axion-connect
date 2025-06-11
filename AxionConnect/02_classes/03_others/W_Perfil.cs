using LmCorbieUI.Metodos.AtributosCustomizados;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxionConnect {
  public class W_Perfil {
    [EhLink]
    [LarguraColunaGrid(80)]
    [DisplayName("Código")]
    [AlinhamentoColunaGrid(System.Windows.Forms.DataGridViewContentAlignment.MiddleRight)]
    public int Codigo { get; set; }

    [LarguraColunaGrid(0)]
    [DisplayName("Nome")]
    public string Descricao { get; set; }

    [LarguraColunaGrid(0)]
    [DisplayName("Observação")]
    public string Observacao { get; set; }

    [Browsable(false)]
    public string Permissoes { get; set; }
  }
}
