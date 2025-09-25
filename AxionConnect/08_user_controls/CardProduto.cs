using LmCorbieUI;
using LmCorbieUI.Controls;
using LmCorbieUI.Design;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static LmCorbieUI.Controls.LmColunaGrid;

namespace AxionConnect {
  public partial class CardProduto : LmUserControl {
    public event MouseEventArgsCtrl MouseDownCtrl;
    public event Click CardExclude;
    private Color defBackColor;
    private int borderRadius = 8;
    private int borderSize = 1;
    private bool isFocused = false;

    public CardProduto() {
      InitializeComponent();

      // Ativar double buffering para renderização mais suave
      this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                   ControlStyles.UserPaint |
                   ControlStyles.DoubleBuffer |
                   ControlStyles.ResizeRedraw |
                   ControlStyles.SupportsTransparentBackColor, true);

      Thread t = new Thread(() => { AplicarTema(); }) { IsBackground = true };
      t.Start();
    }

    private void AplicarTema() {
      Color bcColor = LmCor.Bc_Dgv_CellNormal.Escurecer();// LmPaint.BackColor.Button.Normal(Tema);

      this.BackColor = lblInfo.BackColor = bcColor;
      defBackColor = bcColor;
      this.lblInfo.ForeColor = bcColor.IsDarkColor() ? Color.FromArgb(244, 242, 240) : Color.FromArgb(44, 42, 40);
      //if (bcColor.IsDarkColor())
      //  btnRemover.Image = btnRemover.Image.ApplyColor(this.lblInfo.ForeColor);
    }

    internal void SetText() {
      var proc = this.Tag as ProdutoErp;
      lblInfo.Text = $"{proc.CodProduto} - {proc.Denominacao}";
    }
    
    internal void HideButtonMove() {
      btnMover.Visible = false;
      this.Padding = new Padding(8, 8, 8, 8);
    }

    private void BtnRemover_Click(object sender, EventArgs e) {

    }

    private void LblInfo_MouseDown(object sender, MouseEventArgs e) {
      MouseDownCtrl?.Invoke(this, e);
    }

    private void CardProduto_MouseEnter(object sender, EventArgs e) {
      //if (this.Parent.Controls.OfType<CardProduto>().Count() > 1) {
      //  Cursor = Cursors.NoMoveVert;
      //}

      isFocused = true;

      Color bcColor = lblInfo.BackColor;

      if (bcColor.IsDarkColor())
        this.BackColor = lblInfo.BackColor = bcColor.Escurecer();
      else
        this.BackColor = lblInfo.BackColor = bcColor.Clarear();
    }

    private void CardProduto_MouseLeave(object sender, EventArgs e) {
      this.BackColor = lblInfo.BackColor = defBackColor;
      isFocused = false;
      this.Cursor = Cursors.Default;
    }

    private void LblInfo_MouseUp(object sender, MouseEventArgs e) {
      // this.Cursor = Cursors.Default;
    }

    // inicio desenho do card

    protected override void OnPaint(PaintEventArgs pevent) {
      base.OnPaint(pevent);

      Rectangle rectSurface = this.ClientRectangle;
      Rectangle rectBorder = Rectangle.Inflate(rectSurface, -borderSize, -borderSize);
      int smoothSize = 2;
      if (borderSize > 0)
        smoothSize = borderSize;

      if (borderRadius > 2) //Rounded button
      {
        using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
        using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize))
        using (Pen penSurface = new Pen(this.Parent.BackColor, smoothSize))
        using (Pen penBorder = new Pen(LmCor.Br_Normal, borderSize)) {
          pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
          //Button surface
          this.Region = new Region(pathSurface);
          //Draw surface border for HD result
          pevent.Graphics.DrawPath(penSurface, pathSurface);

          //Button border                    
          if (borderSize >= 1)
            //Draw control border
            pevent.Graphics.DrawPath(penBorder, pathBorder);
        }
      }
    }

    private GraphicsPath GetFigurePath(Rectangle rect, int radius) {
      GraphicsPath path = new GraphicsPath();
      float curveSize = radius * 2F;

      path.StartFigure();
      path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
      path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
      path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
      path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
      path.CloseFigure();
      return path;
    }

    protected override void OnResize(EventArgs e) {
      base.OnResize(e);
      this.Invalidate(); // Redesenhar quando redimensionar
    }

  }
}
