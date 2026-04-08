using LmCorbieUI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using static AxionConnect.Api;

namespace AxionConnect {
  internal partial class Api {
    internal class Engenharia {
      public int codEmpresa { get; set; }
      public string codProduto { get; set; }
      public string descricaoProduto { get; set; } = "";
      public string tipoModulo { get; set; } = "E";
      public int codClassificacao { get; set; }
      public string nomeArquivoDesenhoEng { get; set; }
      public bool engenhariaFantasma { get; set; }
      public string descEngenhariaFantasma { get; set; }
      public string unidadeMedida { get; set; }
      public int statusEngenharia { get; set; }
      public List<ComponenteEng> componentes = new List<ComponenteEng>();
      public List<OperacaoEng> operacoes = new List<OperacaoEng>();
    }

    internal class ComponenteEng {
      public int seqComponente { get; set; }
      public int seqOperacional { get; set; }
      public string codInsumo { get; set; }
      public double quantidade { get; set; }
      public int itemKanban { get; set; }
      public double comprimento { get; set; }
      public double largura { get; set; }
      public double espessura { get; set; }
      public double percQuebra { get; set; }
      public int codClassificacaoInsumo { get; set; }
      public string codPEInsumo { get; set; }
      public string centroCusto { get; set; }
    }

    internal class OperacaoEng {
      public int seqOperacao { get; set; }
      public int codOperacao { get; set; }
      public string abreviaturaOperacao { get; set; }
      public double numOperadores { get; set; }
      public int codFaseOperacao { get; set; }
      public string codMascaraMaquina { get; set; }
      public int codLinhaProducao { get; set; }
      public double tempoPadraoOperacao { get; set; }
      public double tempoPreparacaoOperacao { get; set; }
      public string centroCusto { get; set; }
      public TipoOperacao tipoOperacao { get; set; }
    }

    internal static async Task<bool> CadastrarEngenhariaAsync(ContextoDados db, Engenharia engenharia) {
      try {
        JObject jsonObject = new JObject();

        var client = Api.GetClient(modulo: "ppcppadrao", endpoint: $"pendenciaEngenharia");
        var request = Api.CreateRequest(Method.PUT);
        var centroCusto = engenharia.operacoes.Count > 0 ? engenharia.operacoes[0].centroCusto : string.Empty;

        var bodyObject = "" +
          "{" +
             $"\"codEmpresa\": {engenharia.codEmpresa}," +
             $"\"tipoModulo\": \"{engenharia.tipoModulo}\"," +
             $"\"codClassificacao\": {engenharia.codClassificacao}," +
             $"\"nomeArquivoDesenhoEng\": \"{engenharia.nomeArquivoDesenhoEng}\"," +
             $"\"codProduto\": \"{engenharia.codProduto}\"," +
             $"\"centroCustoDestino\": \"{centroCusto}\"," +
             $"\"engenhariaFantasma\": {engenharia.engenhariaFantasma.ToString().ToLower()}," +
             $"\"descEngenhariaFantasma\": \"{engenharia.descEngenhariaFantasma}\"," +

             "\"componentes\": [";

        foreach (var componente in engenharia.componentes) {
          bodyObject += "{" +
              $"\"seqComponente\": {componente.seqComponente}," +
              $"\"seqOperacaoConsumo\": {componente.seqOperacional}," +
              $"\"codInsumo\": \"{componente.codInsumo}\"," +
              $"\"quantidade\": {componente.quantidade.ToString("0.000").Replace(",", ".")}," +
              $"\"comprimento\": {componente.comprimento.ToString("0.000").Replace(",", ".")}," +
              $"\"largura\": {componente.largura.ToString("0.000").Replace(",", ".")}," +
              $"\"espessura\": {componente.espessura.ToString("0.000").Replace(",", ".")}," +
              $"\"percQuebra\": {componente.percQuebra.ToString("0.000").Replace(",", ".")}," +
              $"\"codClassificacaoInsumo\": {componente.codClassificacaoInsumo}," +
              $"\"centroCustoDestino\": \"{centroCusto}\"," +
              $"\"itemKanban\": {componente.itemKanban}" +
              "},";
        }

        bodyObject = bodyObject.TrimEnd(',') + "]," +

        "\"operacoes\": [";

        foreach (var operacao in engenharia.operacoes) {
          bodyObject += "{" +
              $"\"seqOperacao\": {operacao.seqOperacao}," +
              $"\"codOperacao\": {operacao.codOperacao}," +
              // $"\"codOperacao\": 22," +
              $"\"numOperadores\": {operacao.numOperadores}," +
              $"\"codFaseOperacao\": {operacao.codFaseOperacao}," +

               (operacao.tipoOperacao != TipoOperacao.Externa
               ? $"\"codMascaraMaquina\": \"{operacao.codMascaraMaquina}\","                 // utiliza tela de configuação de processo/máquina (Customizada)
               : $"") +
              // $"\"maquina\": \"112000\"," +

              $"\"tempoPadraoOperacao\": {operacao.tempoPadraoOperacao.ToString().Replace(",", ".")}," +
              $"\"tempoPreparacaoOperacao\": {operacao.tempoPreparacaoOperacao.ToString().Replace(",", ".")}" +
              "},";
        }
        bodyObject = bodyObject.TrimEnd(',') + "]" +
          "}";

        request.AddJsonBody(bodyObject);

        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful) {
          var responseData = response.Content;
          jsonObject = JObject.Parse(responseData);

          return true;
        } else {
          var errorResponse = JsonConvert.DeserializeObject<List<ApiErrorResponse>>(response.Content);
          var errorMessage = errorResponse?.FirstOrDefault()?.mensagem ?? "Erro ao Cadastrar Engenharia";
          throw new Exception($"{response.StatusCode}\r\n{errorMessage}");
        }
      } catch (Exception ex) {
        throw new Exception($"{ex.Message}");
      }
    }

    internal static async Task<Engenharia> GetEngenhariaAsync(string codigo) {
      Engenharia _return = null;

      try {
        var client = Api.GetClient(modulo: "ppcppadrao", endpoint: $"engenharia/{codigo}");
        var request = Api.CreateRequest(Method.GET);

        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful) {
          var responseData = response.Content;
          var jsonObject = JObject.Parse(responseData);

          _return = new Engenharia {
            descricaoProduto = jsonObject["descricao"]?.ToString(),

            codClassificacao = int.TryParse(jsonObject["codClassificacao"]?.ToString(), out var codClass)
              ? codClass : 0,

            statusEngenharia = int.TryParse(jsonObject["statusEngenharia"]?.ToString(), out var status)
              ? status : 0,

            unidadeMedida = jsonObject["unidadeMedida"]?.ToString(),

            componentes = jsonObject["componentes"]?.Select(c => new ComponenteEng {
              seqComponente = int.TryParse(c["seqComponente"]?.ToString(), out var seqC) ? seqC : 0,

              seqOperacional = int.TryParse(c["seqOperacional"]?.ToString(), out var seqO) ? seqO : 0,

              codInsumo = c["codItem"]?.ToString(),

              quantidade = double.TryParse(c["quantidade"]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var qtd)
                ? qtd : 0,

              centroCusto = c["centroCusto"]?.ToString(),

              comprimento = double.TryParse(c["comprimento"]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var comp)
                ? comp : 0,

              largura = double.TryParse(c["largura"]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var larg)
                ? larg : 0,

              espessura = double.TryParse(c["espessura"]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var esp)
                ? esp : 0,

              percQuebra = double.TryParse(c["percQuebra"]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var pq)
                ? pq : 0,

              codClassificacaoInsumo = int.TryParse(c["codClassificacao"]?.ToString(), out var codCi) ? codCi : 0,
            }).ToList() ?? new List<ComponenteEng>(),

            operacoes = jsonObject["operacoes"]?.Select(o => new OperacaoEng {
              seqOperacao = int.TryParse(o["seqOperacao"]?.ToString(), out var seqOp) ? seqOp : 0,

              codOperacao = int.TryParse(o["codOperacao"]?.ToString(), out var codOp) ? codOp : 0,

              abreviaturaOperacao = o["abreviaturaOperacao"]?.ToString(),

              numOperadores = double.TryParse(o["numOpradores"]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var numOp)
                ? numOp : 0,

              codFaseOperacao = int.TryParse(o["faseProducao"]?.ToString(), out var fase) ? fase : 0,

              codMascaraMaquina = o["mascMaquina"]?.ToString(),

              tempoPadraoOperacao = double.TryParse(o["tempoPadrao"]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var tp)
                ? tp * 60 : 0,

              tempoPreparacaoOperacao = double.TryParse(o["tempoPreparacao"]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var tprep)
                ? tprep * 60 : 0,

              centroCusto = o["centroCusto"]?.ToString(),
            }).ToList() ?? new List<OperacaoEng>()
          };


          var tipoEnc = jsonObject["tipoEngenharia"]?.ToString();

          _return.engenhariaFantasma = tipoEnc == "2";

          foreach (var op in _return.operacoes) {
            if (op.tempoPadraoOperacao == 0)
              op.tempoPadraoOperacao = "00:01".FormatarHoraDouble();
            if (op.numOperadores == 0)
              op.numOperadores = 1;
          }
        }
      } catch (Exception ex) {
        LmException.ShowException(ex, "Erro ao carregar item genérico");
      }

      return _return;
    }

  }
}
