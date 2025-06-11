using LmCorbieUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AxionConnect {
  internal static class Default {
    public static int ObterPermissao(this Enum valor) {
      FieldInfo fieldInfo = valor.GetType().GetField(valor.ToString());
      PermissaoSistema[] atributo = (PermissaoSistema[])fieldInfo.GetCustomAttributes(typeof(PermissaoSistema));

      return atributo.Length > 0 ? (int)atributo[0].Permissao : 99;
    }

    /// <summary>
    /// Retorna uma lista com os valores de um determinado enumerador.
    /// </summary>
    /// <param name="tipo"> Tipo "Objeto"</param>
    /// <returns></returns>
    public static IList ObterListaItens(this Type tipo, string listaExecao = "") {
      ArrayList lista = new ArrayList();
      if (tipo != null) {
        string[] list = null;
        if (!string.IsNullOrEmpty(listaExecao)) {
          list = listaExecao.Replace(" ", "").Replace("^", ";").Replace(",", ";").Replace(".", ";").Split(';');
        }

        Array enumValores = Enum.GetValues(tipo);
        foreach (Enum valor in enumValores) {
          if (list != null && list.Contains(valor.ToString()))
            continue;

          lista.Add(new KeyValuePair<Enum, string>(valor, valor.ObterDescricaoEnum()));
        }
      }

      return lista;
    }

    public static bool EhPendenciaCritica(this Enum valor) {
      FieldInfo fieldInfo = valor.GetType().GetField(valor.ToString());
      TipoPendencia[] atributo = (TipoPendencia[])fieldInfo.GetCustomAttributes(typeof(TipoPendencia));

      return atributo.Length > 0 ? atributo[0].PendenciaCritica == PendenciaCritica.Sim : false;
    }

  }
}
