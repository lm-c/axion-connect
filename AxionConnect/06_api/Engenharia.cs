using LmCorbieUI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }

    internal static async Task<bool> CadastrarEngenhariaAsync(ContextoDados db, Engenharia engenharia) {
      try {
        JObject jsonObject = new JObject();

        var client = Api.GetClient(modulo: "ppcppadrao", endpoint: $"pendenciaEngenharia");
        var request = Api.CreateRequest(Method.PUT);

        var bodyObject = "" +
          "{" +
             $"\"codEmpresa\": {engenharia.codEmpresa}," +
             $"\"tipoModulo\": \"{engenharia.tipoModulo}\"," +
             $"\"codClassificacao\": {engenharia.codClassificacao}," +
             $"\"nomeArquivoDesenhoEng\": \"{engenharia.nomeArquivoDesenhoEng}\"," +
             $"\"codProduto\": \"{engenharia.codProduto}\"," +
             $"\"engenhariaFantasma\": {engenharia.engenhariaFantasma.ToString().ToLower()}," +
             $"\"descEngenhariaFantasma\": \"{engenharia.descEngenhariaFantasma}\"," +

             "\"componentes\": [";

        foreach (var componente in engenharia.componentes) {
          bodyObject += "{" +
              $"\"seqComponente\": {componente.seqComponente}," +
              $"\"codInsumo\": \"{componente.codInsumo}\"," +
              $"\"quantidade\": {componente.quantidade.ToString().Replace(",", ".")}," +
              $"\"comprimento\": {componente.comprimento.ToString().Replace(",", ".")}," +
              $"\"largura\": {componente.largura.ToString().Replace(",", ".")}," +
              $"\"espessura\": {componente.espessura.ToString().Replace(",", ".")}," +
              $"\"percQuebra\": {componente.percQuebra.ToString().Replace(",", ".")}," +
              $"\"codClassificacaoInsumo\": {componente.codClassificacaoInsumo}," +
              $"\"itemKanban\": {componente.itemKanban}" +
              "},";
        }

        bodyObject = bodyObject.TrimEnd(',') + "]," +

        "\"operacoes\": [";

        foreach (var operacao in engenharia.operacoes) {
          bodyObject += "{" +
              $"\"seqOperacao\": {operacao.seqOperacao}," +
              $"\"codOperacao\": {operacao.codOperacao}," +
              $"\"numOperadores\": {operacao.numOperadores}," +
              $"\"codFaseOperacao\": {operacao.codFaseOperacao}," +
              $"\"codMascaraMaquina\": \"{operacao.codMascaraMaquina}\"," +
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
            codClassificacao = jsonObject["codClassificacao"]?.ToObject<int>() ?? 0,
            statusEngenharia = jsonObject["statusEngenharia"]?.ToObject<int>() ?? 0,
            unidadeMedida = jsonObject["unidadeMedida"]?.ToString(),

            componentes = jsonObject["componentes"]?.Select(c => new ComponenteEng {
              seqComponente = c["seqComponente"]?.ToObject<int>() ?? 0,
              codInsumo = c["codItem"]?.ToString(),
              quantidade = c["quantidade"]?.ToObject<double>() ?? 0,
              centroCusto = c["centroCusto"]?.ToString(),
              comprimento = c["comprimento"]?.ToObject<double>() ?? 0,
              largura = c["largura"]?.ToObject<double>() ?? 0,
              espessura = c["espessura"]?.ToObject<double>() ?? 0,
              percQuebra = c["percQuebra"]?.ToObject<double>() ?? 0,
              codClassificacaoInsumo = c["codClassificacao"]?.ToObject<int>() ?? 0,
            }).ToList() ?? new List<ComponenteEng>(),

            operacoes = jsonObject["operacoes"]?.Select(o => new OperacaoEng {
              seqOperacao = o["seqOperacao"]?.ToObject<int>() ?? 0,
              codOperacao = o["codOperacao"]?.ToObject<int>() ?? 0,
              abreviaturaOperacao = o["abreviaturaOperacao"]?.ToString(),
              numOperadores = o["numOpradores"]?.ToObject<double>() ?? 0,
              codFaseOperacao = o["faseProducao"]?.ToObject<int>() ?? 0,
              codMascaraMaquina = o["mascMaquina"]?.ToString(),
              tempoPadraoOperacao = o["tempoPadrao"]?.ToObject<double>() ?? 0,
              tempoPreparacaoOperacao = o["tempoPreparacao"]?.ToObject<double>() ?? 0,
              centroCusto = o["centroCusto"]?.ToString(),
            }).ToList() ?? new List<OperacaoEng>()
          };

        }
      } catch (Exception ex) {
        LmException.ShowException(ex, "Erro ao carregar item genérico");
      }

      return _return;
    }

    internal static async Task<string> DuplicarItemGenericoAsync(ContextoDados db, ItemGenerico itemGenerico) {
      var configApi = configuracao_api.Selecionar();

      try {

        var mascara = itemGenerico.tipoDocumento == TipoDocumento.Peca ? configApi.mascara_peca : configApi.mascara_conjunto;

        var codigo = string.Empty;
        var mascara_ent = mascara;
        var mascara_sai = mascara;
        var descricao = itemGenerico.nome;
        var pesoBruto = itemGenerico.pesoBruto;
        var pesoLiquido = itemGenerico.pesoLiquido;
        var unidadeMedida = itemGenerico.unidadeMedida;


        JObject jsonObject = new JObject();

        var client = Api.GetClient(modulo: "ppcppadrao", endpoint: $"duplicarItemImportacao");
        var request = Api.CreateRequest(Method.PUT);
        var response = await client.ExecuteAsync(request);

        var bodyObject = "" +
          "{" +
             $"\"tipoModulo\": \"E\"," +
             $"\"codigoItem\": null," +
             $"\"descricaoItem\": \"{descricao}\"," +
             $"\"descricaoCompleta\": \"{descricao}\"," +
             $"\"nivelMascaraEntrada\": \"{mascara_ent}\"," +
             $"\"nivelMascaraSaida\": \"{mascara_sai}\"," +
             $"\"pesoLiquido\": {pesoLiquido.ToString().Replace(",", ".")}," +
             $"\"pesoBruto\": {pesoBruto.ToString().Replace(",", ".")}," +
             $"\"unidadeMedida\": \"{unidadeMedida}\"" +
           "}";

        request.AddJsonBody(bodyObject);

        response = await client.ExecuteAsync(request);

        if (response.IsSuccessful) {
          var responseData = response.Content;
          jsonObject = JObject.Parse(responseData);
          codigo = (string)jsonObject["codigo"];

          if (codigo.EndsWith("9999")) {
            var splitMascEnt = mascara_ent.Split('.');
            splitMascEnt[splitMascEnt.Length - 1] = (Convert.ToInt32(splitMascEnt[splitMascEnt.Length - 1]) + 1).ToString("00");

            if (itemGenerico.tipoDocumento == TipoDocumento.Peca) {
              configApi.mascara_peca = string.Join(".", splitMascEnt);
            } else {
              configApi.mascara_conjunto = string.Join(".", splitMascEnt);
            }

            db.SaveChanges();
          }

          return codigo;
        } else {
          var errorResponse = JsonConvert.DeserializeObject<List<ApiErrorResponse>>(response.Content);
          var errorMessage = errorResponse?.FirstOrDefault()?.mensagem ?? "Erro ao Duplicar Produto";
          throw new Exception($"Erro: {response.StatusCode}\r\n{errorMessage}");
        }
      } catch (Exception ex) {
        Toast.Error($"Erro ao Duplicar Produto: {ex.Message}");
        return string.Empty;
      }

    }
  }
}
