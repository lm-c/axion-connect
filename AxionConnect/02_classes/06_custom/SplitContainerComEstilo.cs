using LmCorbieUI.Design;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace AxionConnect {
  internal class SplitContainerComEstilo : SplitContainer {
    protected override void OnPaint(PaintEventArgs e) {
      base.OnPaint(e);
      var g = e.Graphics;

      // cor da linha do divisor
      Brush brush = new SolidBrush(LmCor.CorPrimaria); // ou Color.DarkGray, ou até Color.Red pra assustar 👀

      if (Orientation == Orientation.Vertical) {
        g.FillRectangle(brush, SplitterDistance, 0, SplitterWidth, Height);
      } else {
        g.FillRectangle(brush, 0, SplitterDistance, Width, SplitterWidth);
      }

      BackColor = LmCor.Bc_Form;
    }
  }
}
