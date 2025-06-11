using System;
using System.Net;
using System.Windows.Forms;

namespace AxionConnect {
  internal static class Program {
    /// <summary>
    /// Ponto de entrada principal para o aplicativo.
    /// ⚠⚠⚠  Este projeto deve ser compilado como x64 para que o controle ActiveX do eDrawings funcione corretamente.⚠⚠⚠ 
    /// </summary>
    [STAThread]
    static void Main() {

      // Forçar o uso de TLS 1.2 (ou versões anteriores se necessário)
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

      // Ignorar erros de certificado SSL
      ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new FrmLogin());
    }
  }
}
