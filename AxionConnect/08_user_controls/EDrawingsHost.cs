using eDrawings.Interop.EModelViewControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxionConnect {
  internal class EDrawingsHost : AxHost {
    public event Action<EModelViewControl> EDrawingsControlLoaded;
    private bool _isloaded = false;

    public EDrawingsHost() : base(Api.chave_edrawings) {
      // O GUID acima é o identificador do controle EModelViewControl.
      // Certifique-se de que este GUID corresponde ao controle que você está usando.
      // "C59EEF21-0223-4C39-A708-A3BE9008C67E"
    }
    protected override void OnCreateControl() {
      base.OnCreateControl();
      if (!_isloaded) {
        _isloaded = true;
        try {
          var ctrl = this.GetOcx() as EModelViewControl;
          if (ctrl != null) {
            EDrawingsControlLoaded?.Invoke(ctrl);
          } else {
            MessageBox.Show("Erro ao carregar o controle EDrawings. O controle não foi inicializado corretamente.");
          }
        } catch (Exception ex) {
          MessageBox.Show("Erro ao Carregar EDrawings control: " + ex.Message);
        }
      }
    }
  }
}

