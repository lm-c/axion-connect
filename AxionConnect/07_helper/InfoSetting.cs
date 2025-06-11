using LmCorbieUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxionConnect {
  public class InfoSetting {
    static InfoSetting info = new InfoSetting();

    public static bool UnicodeUtf8 = true;
    public bool _UnicodeUtf8 {
      get { return UnicodeUtf8; }
      set { UnicodeUtf8 = value; }
    }

    public static bool UnicodeIso = false;
    public bool _UnicodeIso {
      get { return UnicodeIso; }
      set { UnicodeIso = value; }
    }

    public static bool AddDenominacaoTodasConfig = false;
    public bool _AddDenominacaoTodasConfig {
      get { return AddDenominacaoTodasConfig; }
      set { AddDenominacaoTodasConfig = value; }
    }

    public static bool ManterCopiaGerarCodigo = false;
    public bool _ManterCopiaGerarCodigo {
      get { return ManterCopiaGerarCodigo; }
      set { ManterCopiaGerarCodigo = value; }
    }

    public static int UltimaFamiliaUsada = 35;
    public int _UltimaFamiliaUsada {
      get { return UltimaFamiliaUsada; }
      set { UltimaFamiliaUsada = value; }
    }

    public static int? CodReservadoInicio = null;
    public int? _CodReservadoInicio {
      get { return CodReservadoInicio; }
      set { CodReservadoInicio = value; }
    }

    public static int? CodReservadoFim = null;
    public int? _CodReservadoFim {
      get { return CodReservadoFim; }
      set { CodReservadoFim = value; }
    }

    private static string diretorio = Environment.ExpandEnvironmentVariables("%AppData%") + "\\Lm Projetos\\AxionConnect\\";
    private static string filename = "InfoSetting.jck";

    public static void Salvar() {
      try {
        string pathName = diretorio + "\\" + filename;
        if (!System.IO.Directory.Exists(diretorio))
          System.IO.Directory.CreateDirectory(diretorio);

        SerializadorXML<InfoSetting>.Serializar(pathName, info);
      } catch (Exception ex) {
        Toast.Error($"Erro ao Salvar Configurações!\r\n{ex.Message}");
      }
    }

    public static void Carregar() {
      try {
        string pathName = diretorio + "\\" + filename;

        if (System.IO.File.Exists(pathName)) {
          info = (InfoSetting)SerializadorXML<InfoSetting>.Deserializar(pathName);
        }
      } catch (Exception ex) {
        Toast.Error($"Erro ao Carregar Configurações!\r\n{ex.Message}");
      }
    }

  }
}
