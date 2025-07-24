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
    internal class ItemGenerico {
      public string codigo { get; set; }
      public string nome { get; set; }
      public string refTecnica { get; set; }
      public string mascaraEntrada { get; set; }
      public string mascaraSaida { get; set; }
      public string classificacaoFiscal { get; set; } 
      public string unidadeMedida { get; set; }
      public int finalidade { get; set; }
      public int origem { get; set; }
      public int tipo { get; set; }
      public int procedencia { get; set; }
      public double pesoBruto { get; set; }
      public double pesoLiquido { get; set; }
      public double pesoPadraoNBR { get; set; }
      public int situacao { get; set; }
      public TipoDocumento tipoDocumento { get; set; }
    }

    internal static async Task<ItemGenerico> GetItemGenericoAsync(string codigo) {
      ItemGenerico _return = null;

      try {
        var client = Api.GetClient(modulo: "itens", endpoint: $"itemGenerico/{codigo}");
        var request = Api.CreateRequest(Method.GET);

        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful) {
          var responseData = response.Content;
          var jsonObject = JObject.Parse(responseData);

          _return = new ItemGenerico {
            codigo = jsonObject["codigo"]?.ToString(),
            nome = jsonObject["nome"]?.ToString(),
            refTecnica = jsonObject["refTecnica"]?.ToString(),
            unidadeMedida = jsonObject["unidadeMedida"]?.ToString(),
            classificacaoFiscal = jsonObject["classificacaoFiscal"]?.ToString(),
            finalidade = jsonObject["finalidade"]?.ToObject<int>() ?? 0,
            origem = jsonObject["origem"]?.ToObject<int>() ?? 0,
            tipo = jsonObject["tipo"]?.ToObject<int>() ?? 0,
            procedencia = jsonObject["procedencia"]?.ToObject<int>() ?? 0,
            
            pesoBruto = jsonObject["dadosSaida"]["pesoBruto"]?.ToObject<double>() ?? 0,
            pesoLiquido = jsonObject["dadosSaida"]["pesoLiquido"]?.ToObject<double>() ?? 0,
            mascaraSaida = jsonObject["dadosSaida"]["mascara"]?.ToString(),

            mascaraEntrada = jsonObject["dadosEntrada"]["mascara"]?.ToString(),
            pesoPadraoNBR = jsonObject["dadosEntrada"]["pesoPadraoNBR"]?.ToObject<double>() ?? 0,
            situacao = jsonObject["dadosEntrada"]["situacao"]?.ToObject<int>() ?? 0,
          };
        }
      } catch (Exception ex) {
        LmException.ShowException(ex, "Erro ao carregar item genérico");
      }

      return _return;
    }
  }
}
