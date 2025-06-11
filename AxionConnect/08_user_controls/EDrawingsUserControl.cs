using eDrawings.Interop.EModelViewControl;
using LmCorbieUI.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxionConnect {
  public partial class EDrawingsUserControl : UserControl {
    public event Action<EModelViewControl> EDrawingsControlLoaded;

    public EDrawingsUserControl() {
      InitializeComponent();
      //LoadEDrawings();
    }

    public void LoadEDrawings() {
      BackColor = LmCor.Bc_Form;
      var host = new EDrawingsHost();
      host.EDrawingsControlLoaded += OnControlLoaded;
      this.Controls.Add(host);
      host.Dock = DockStyle.Fill;
    }

    private void OnControlLoaded(EModelViewControl ctrl) {
      EDrawingsControlLoaded?.Invoke(ctrl);
    }
  }
}
