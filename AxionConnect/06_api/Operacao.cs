using LmCorbieUI;
using System;
using System.Collections.Generic;
using RestSharp;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using LmCorbieUI.Metodos.AtributosCustomizados;
using System.ComponentModel;

namespace AxionConnect {
  internal partial class Api {
    internal class Operacao {
      public string abreviatura { get; set; }

      [DisplayName("Código Operação")]
      [LarguraColunaGrid(150)]
      [DataObjectField(true, false)]
      public int codOperacao { get; set; }

      [DisplayName("Descrição Operação")]
      [LarguraColunaGrid(150)]
      [DataObjectField(false, true)]
      public string descricao { get; set; }
      public int? faseProducao { get; set; } // Permite valores nulos
      public string situacao { get; set; }
      public string tipo { get; set; }
    }

    private class Response {
      public string continuationToken { get; set; }
      public List<Operacao> data { get; set; }
    }

    internal static async Task<List<Operacao>> GetOpsAsync() {
      var _return = new List<Operacao>();

      try {
        var client = Api.GetClient(modulo: "ppcppadrao", endpoint: "operacao?situacao=1&paginacao=1000");
        var request = Api.CreateRequest(Method.GET);
        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful) {
          Response responseObj = JsonConvert.DeserializeObject<Response>(response.Content);

          _return = responseObj.data;

        } else {
          var errorResponse = JsonConvert.DeserializeObject<List<ApiErrorResponse>>(response.Content);
          var errorMessage = errorResponse?.FirstOrDefault()?.mensagem ?? "Erro desconhecido";
          Toast.Error($"Erro: {response.StatusCode}\r\n{errorMessage}");
        }

      } catch (Exception ex) {
        LmException.ShowException(ex, "Erro ao carregar operações");
      }

      return _return;
    }
  }
}
