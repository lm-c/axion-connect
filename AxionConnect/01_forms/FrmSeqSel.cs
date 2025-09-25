using LmCorbieUI;
using LmCorbieUI.Controls;
using LmCorbieUI.Design;
using LmCorbieUI.LmForms;
using LmCorbieUI.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AxionConnect.Api;

namespace AxionConnect {
  public partial class FrmSeqSel : LmChildForm {
    MouseHook mh;
    Control _sender;

    internal List<Processo> _dataSource;

    internal FrmSeqSel(object sender, List<Processo> dataSource) {
      InitializeComponent();

      _dataSource = dataSource;
      _sender = sender as Control;

      CarregarControles();
    }

    private void FrmSeqSel_Load(object sender, EventArgs e) {
      mh = new MouseHook();
      mh.SetHook();
      mh.MouseMoveEvent += Mh_MouseMoveEvent; //  mh.MouseClickEvent += Mh_MouseClickEvent;
    }

    private void FrmSeqSel_FormClosed(object sender, FormClosedEventArgs e) {
      mh.UnHook();
    }

    private void Mh_MouseMoveEvent(object sender, MouseEventArgs e) {
      if (e.X < Left || e.X > Left + Width || e.Y < Top || e.Y > Top + Height) {
        mh.UnHook();
        Close();
      }
    }

    private void FrmSeqSel_KeyDown(object sender, KeyEventArgs e) {
      if (e.KeyCode == Keys.Escape)
        Close();
    }

    private void CarregarControles() {
      flpOps.Controls.Clear();

      foreach (var item in _dataSource) {
        LmLabel lbl = new LmLabel {
          Text = $"{item.codOperacao} {item.descrOperacao}",
          Tag = item,
          AutoSize = false,
          Size = new Size(flpOps.Width - 9, 15),
          Margin = new Padding(3, 0, 0, 0),
          FontSize = LmLabelSize.Small,
          Cursor = Cursors.Hand
        };

        lbl.MouseEnter += (s, e) => {
          AnimarCor(lbl, lbl.BackColor, LmCor.Bc_Dgv_CellNormal.Escurecer());
          //lbl.BackColor = LmCor.Bc_Dgv_CellNormal.Escurecer();
          lbl.ForeColor = Color.FromArgb(244, 242, 240);
        };
        lbl.MouseLeave += (s, e) => {
          AnimarCor(lbl, lbl.BackColor, LmCor.Bc_Form);
          //lbl.BackColor = Color.Transparent;
          lbl.ForeColor = Color.FromArgb(44, 42, 40);
        };
        lbl.Click += (s, e) => {
          var processoSelecionado = (Processo)lbl.Tag;
          this.Tag = processoSelecionado; // Guardamos o processo selecionado
          this.DialogResult = DialogResult.OK;
          this.Close();
        };
        flpOps.Controls.Add(lbl);
      }

      int altura = 2;

      foreach (Control ctrl in flpOps.Controls)
        altura += ctrl.Height;

      this.Height = altura + 6;

      var ptScreen = _sender.PointToScreen(Point.Empty);

      this.Location = new Point(ptScreen.X - this.Width + _sender.Width, ptScreen.Y - this.Height + _sender.Height);
    }

    private async void AnimarCor(Label lbl, Color corInicial, Color corFinal, int duracao = 150) {
      int steps = 10;
      for (int i = 0; i <= steps; i++) {
        double progress = (double)i / steps;
        int r = (int)(corInicial.R + (corFinal.R - corInicial.R) * progress);
        int g = (int)(corInicial.G + (corFinal.G - corInicial.G) * progress);
        int b = (int)(corInicial.B + (corFinal.B - corInicial.B) * progress);
        lbl.BackColor = Color.FromArgb(r, g, b);
        await Task.Delay(duracao / steps);
      }
    }
  }
}
