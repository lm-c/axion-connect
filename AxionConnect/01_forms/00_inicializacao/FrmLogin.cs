using LmCorbieUI;
using LmCorbieUI.LmForms;
using LmCorbieUI.Metodos;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxionConnect {
  public partial class FrmLogin : LmSingleForm {
    string login, senha;

    readonly string userPC;
    readonly string hostname;

    static FrmLogin instancia;

    public static FrmLogin Instancia {
      get {
        if (instancia == null)
          instancia = new FrmLogin();

        return instancia;
      }
    }

    public FrmLogin() {
      InitializeComponent();

      ptb.BringToFront();
      lblCarregando.BringToFront();

      userPC = Environment.UserName;
      hostname = Dns.GetHostName();
    }

    private void FrmLogin_Load(object sender, EventArgs e) {
      instancia = this;
    }

    private void FrmLogin_Loaded(object sender, EventArgs e) {
      try {
         if (Web.IsConnected()) {
          using (ContextoDados db = new ContextoDados()) {
            var usuAlocado = Queryable.FirstOrDefault(
              from x in db.usuario_alocados.Where(x => x.hostname == hostname && x.usuario_pc == userPC)
              select new { x, x.usuario });
            Invoke(new MethodInvoker(delegate () {
              if (usuAlocado != null) {
                usuario_alocados.model = usuAlocado.x;
                usuario_alocados.model.usuario = usuAlocado.x.usuario;
                txtUsuario.Text = usuario_alocados.model.usuario.login;
                txtSenha.Text = usuario_alocados.model.usuario.senha.DescriptografarAES();

                Logar();

              }
            }));
          }
        } else {
          Invoke(new MethodInvoker(delegate () {
            Toast.Warning("Sem Internet");
          }));
        }
      } catch (Exception ex) {
        LmException.ShowException(ex, "Erro ao inicializar login");
      } finally {
        Invoke(new MethodInvoker(delegate () {
          ptb.Visible = lblCarregando.Visible = false;
        }));
      }
    }

    private void BtnLogin_Click(object sender, EventArgs e) {
      Logar();
    }

    private void Logar() {
      if (Controles.PossuiCamposInvalidos(this))
        return;

      using (ContextoDados db = new ContextoDados()) {
        login = txtUsuario.Text;

        try {
          senha = db.usuarios.FirstOrDefault(x => x.login == login)?.senha;

          if (login == txtUsuario.Text && senha == Criptografar.EncryptAES(txtSenha.Text)) {
            var usu = (db.usuarios.Where(x => x.login == txtUsuario.Text)).FirstOrDefault();

            if (usu != null) {
              if (!usu.ativo) {
                Toast.Warning("Usuário Inativo!");
                return;
              }
            } else {
              Toast.Error("Retornou Usuário Inválido.\nLogin Cancelado.");
              return;
            }

            if (usuario_alocados.model == null) {
              usuario_alocados.model = new usuario_alocados();
              usuario_alocados.model.usuario_pc = Environment.UserName;
              usuario_alocados.model.hostname = Dns.GetHostName();
              usuario_alocados.model.usuario_id = usu.id;

              db.usuario_alocados.Add(usuario_alocados.model);
              db.SaveChanges();
            }

            //FrmPrincipal.Instancia.ConfigurarPermissoes();

            // Carregar Variáveis de Ambiente
            string nomeSistem = "Axion Connect Artama";
            string pastaRaiz = "LM Projetos Data";
            string cliente = "Artama";
            string mail = "michalakleo@gmail.com";

            ValPadrao.DefinirPadrao(pastaRaiz, nomeSistem, cliente, mail);

            //FrmPrincipal.Instancia.Carregarrodape(InfoAssembly.Version, usu.login);

            // Carregar Globais
            var configAPI = configuracao_api.Selecionar();

            Api.token = configAPI.token;
            Api.chave_edrawings = configAPI.chave_edrawings;
            Api.url = configAPI.endereco;
            Api.codigoEmpresa = configAPI.codigoEmpresa;


                        
            templates.Carregar();
            InfoSetting.Carregar();
            configuracao_sistema.Carregar();
            this.Hide();
            FrmPrincipal frmPrincipal = new FrmPrincipal();
            frmPrincipal.Show();
            frmPrincipal.MenuSistema_Click(null, null);
          } else {
            MsgBox.Show("Usuário ou Senha Inválido.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
          }
        } catch (Exception ex) {
          LmException.ShowException(ex, "Erro ao Logar no Sistema");

          txtUsuario.Focus();
        }
      }
    }
  }
}
