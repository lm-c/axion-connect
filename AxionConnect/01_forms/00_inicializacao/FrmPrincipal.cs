using LmCorbieUI;
using LmCorbieUI.Controls;
using LmCorbieUI.Design;
using LmCorbieUI.LmForms;
using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AxionConnect {
  public partial class FrmPrincipal : LmMainForm {
    private bool logout;

    #region Construtor

    static readonly DateTime dt = DateTime.Now;
    internal string SecaoAtual =
         dt.Year +
         dt.Month.ToString("00") +
         dt.Day.ToString("00") +
         dt.Hour.ToString("00") +
         dt.Minute.ToString("00") +
         dt.Second.ToString("00") +
         dt.Millisecond.ToString("000");

    string versaoAtual = "0";

    static FrmPrincipal instancia;
    internal static FrmPrincipal Instancia {
      get {
        if (instancia == null)
          instancia = new FrmPrincipal();
        return instancia;
      }
    }

    public FrmPrincipal() {
      InitializeComponent();

      Text = "Axion Connect";

      instancia = this;

      WindowState = FormWindowState.Maximized;

      if (ConexaoMySql.Database.StartsWith("teste"))
        LmCor.CorPrimaria = Color.DarkRed;

      tslSecao.Text = $"Seção: {SecaoAtual}";
      //InfoDefault.Secao = SecaoAtual;

      versaoAtual = Assembly.GetExecutingAssembly().GetName().Version.ToString();
      tslVersao.Text = $"Versão: {versaoAtual}";

      if (!ValPadrao.NomeSistema.Contains("Versão"))
        ValPadrao.NomeSistema += " " + tslVersao.Text;

      tslModo.Text = string.Empty;
      tslFormAberto.Text = string.Empty;

      //tslUsuario.Text = $"{InfoDefault.UsuarioLogado_Login}";
    }

    #endregion

    private void FrmPrincipal_Load(object sender, EventArgs e) {
      PosicionarMenu(false);

      SetarAparencia(this);

      System.Threading.Thread t2 = new System.Threading.Thread(() => { ConfigurarRodape(); }) { IsBackground = true };
      t2.Start();
    }

    private void FrmPrincipal_Loaded(object sender, EventArgs e) {
      Invoke(new MethodInvoker(delegate () {

      }));

    }

    private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e) {
      configuracao_sistema.Salvar();

      if (!logout)
        Application.Exit();
      else
        FrmLogin.Instancia.Show();
    }

    internal void PosicionarMenu(bool salvarPropriedades = true) {
      var sizeX = configuracao_sistema.model.menu_apenas_icone ? 45 : 190;
      var sizeY =  45;

      pnlMenu.SendToBack();
      pnlMenu.Width = sizeX;

      menuSandwich.Height = sizeY;

      if (configuracao_sistema.model.menu_apenas_icone) {
        pnlLogo.Height = sizeY;
        ptbLogo.Visible = false;
      } else {
        pnlLogo.Height = 60;
        ptbLogo.Visible = true;
      }

      ptbLogo.Visible = !configuracao_sistema.model.menu_apenas_icone;

      foreach (var menuItem in pnlMenu.Controls.OfType<LmMenuItem>()) {

        menuItem.Size = new Size(sizeX, sizeY);

        var text = !string.IsNullOrEmpty(menuItem.Text) ? menuItem.Text.Trim() : toolTip1.GetToolTip(menuItem).Trim();

        menuItem.Text = configuracao_sistema.model.menu_apenas_icone ? "" : "   " + text;
        toolTip1.SetToolTip(menuItem, configuracao_sistema.model.menu_apenas_icone ? text : "");

        menuItem.ImageAlign =
        configuracao_sistema.model.menu_apenas_icone || string.IsNullOrEmpty(text)
        ? ContentAlignment.MiddleCenter
        : ContentAlignment.MiddleLeft;
      }

      if (salvarPropriedades) {
        configuracao_sistema.Salvar();
      }
    }

    private void SetarAparencia(Control control) {
      AtualizarRecursivo(control);

      FrmPrincipal.Instancia.Refresh();
      FrmPrincipal.Instancia.Invalidate();
    }

    private void AtualizarRecursivo(Control control) {
      foreach (var item in control.Controls) {
        if (item is LmMenuItem menuItem) {
          menuItem.AplicarStilo();
          menuItem.Refresh();
        }

        if (item is LmButton btn) {
          btn.AplicarStilo();
          btn.Refresh();
        }

        if (control.Controls.Count > 0)
          AtualizarRecursivo((Control)item);
      }
    }

    private void ConfigurarRodape() {
      //InfoDefaultUI.UsuarioLogado_Login = InfoDefault.UsuarioLogado_Login;
      //InfoDefaultUI.DadosEmpresa = EmpresaDATA.DadosEmpresa();

      //Invoke(new MethodInvoker(delegate () {
      //  tmrMemory.Enabled = true;
      //}));
    }

    private void RemoverLinhasSeparacaoMenu(ToolStripItemCollection items) {
      System.Collections.IList list = items;
      for (int k = 0; k < items.Count; k++) {
        if (list[k] is ToolStripSeparator)
          continue;

        ToolStripMenuItem item = (ToolStripMenuItem)list[k];
        if (item.DropDownItems != null && item.DropDownItems.Count > 0) {
          for (int i = 1; i < item.DropDownItems.Count - 1; i++) {
            if (item.DropDownItems[i] is ToolStripSeparator && item.DropDownItems[i].Available) {
              bool ocultar = true;
              for (int h = i - 1; h >= 0; h--) {
                if (item.DropDownItems[h].Available && !(item.DropDownItems[h] is ToolStripSeparator)) {
                  ocultar = false;
                  break;
                } else if (item.DropDownItems[h].Available && item.DropDownItems[h] is ToolStripSeparator) {
                  break;
                }
              }
              if (ocultar) {
                item.DropDownItems[i].Visible = false;
                continue;
              }

              ocultar = true;
              for (int h = i + 1; h < item.DropDownItems.Count; h++) {
                if (item.DropDownItems[h].Available && !(item.DropDownItems[h] is ToolStripSeparator)) {
                  ocultar = false;
                  break;
                } else if (item.DropDownItems[h].Available && item.DropDownItems[h] is ToolStripSeparator) {
                  break;
                }
              }
              if (ocultar) {
                item.DropDownItems[i].Visible = false;
                continue;
              }
            }
          }
        }

        if (item.DropDownItems != null && item.DropDownItems.Count > 0)
          RemoverLinhasSeparacaoMenu(item.DropDownItems);
      }
    }

    // ----------------------------------- Menu superior
    private void MenuSandwich_Click(object sender, EventArgs e) {
      if (configuracao_sistema.model.menu_apenas_icone)
        configuracao_sistema.model.menu_apenas_icone = false;
      else
        configuracao_sistema.model.menu_apenas_icone = true;

      PosicionarMenu();
    }

    // ----------------------------------- Menu Engenharia

    internal void MenuSistema_Click(object sender, EventArgs e) => OpenFormChild(new FrmEngenharia());


    private void MenuLogout_Click(object sender, EventArgs e) {
      logout = true;
      Close();
    }
  }
}

