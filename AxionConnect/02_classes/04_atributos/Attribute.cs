using System;

namespace AxionConnect {
  /// <summary>
  /// Tipo de Permissao da Propriedade
  /// </summary>
  public class PermissaoSistema : Attribute {
    /// <summary>
    /// Define o Tipo desta Pemissao do Sistema
    /// </summary>
    /// <param name="permissao">Escolher tipo de Permissão</param>
    public PermissaoSistema(TipoPermissao permissao) {
      Permissao = permissao;
    }

    public TipoPermissao Permissao { get; }

    //public static int ObterPermissao(this Enum valor) {
    //  FieldInfo fieldInfo = valor.GetType().GetField(valor.ToString());
    //  PermissaoSistema[] atributo = (PermissaoSistema[])fieldInfo.GetCustomAttributes(typeof(PermissaoSistema));

    //  return atributo.Length > 0 ? (int)atributo[0].Permissao : 99;
    //}


  }

  public class TipoPendencia : Attribute {
    public TipoPendencia(PendenciaCritica pendenciaCritica) {
      PendenciaCritica = pendenciaCritica;
    }

    public PendenciaCritica PendenciaCritica { get; }

  }
}
