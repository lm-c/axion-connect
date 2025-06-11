using LmCorbieUI.Metodos.AtributosCustomizados;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxionConnect {
  public class W_Permissao {
    [LarguraColunaGrid(25)]
    [DisplayName(" "), ToolTipGrid("Mostra Icone de Acordo com o Tipo")]
    [AlinhamentoColunaGrid(System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter)]
    public Bitmap ImgAnexo { get; set; } = new Bitmap(20, 20);

    [LarguraColunaGrid(80)]
    [DisplayName("Código")]
    [DataObjectField(true, false)]
    [AlinhamentoColunaGrid(System.Windows.Forms.DataGridViewContentAlignment.MiddleRight)]
    public int Codigo { get; set; }

    [LarguraColunaGrid(0)]
    [DisplayName("Permissão Específica")]
    [DataObjectField(false, true)]
    public string Descricao { get; set; }
  }
}
