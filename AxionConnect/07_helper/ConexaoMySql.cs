using MySql.Data.MySqlClient;
using System;

namespace AxionConnect {
  class Prefixo {
   //  public static string GetPrefixo => ""; // para Oficial         
    public static string GetPrefixo => "teste_"; // para testes
  }


  public class ConexaoMySql : IDisposable {
    public static readonly string Database = $"{Prefixo.GetPrefixo}artama";
    public static readonly string Port = "3306";

    public static readonly string Server = "192.168.1.240";
    public static readonly string User = "corbie";
    public static readonly string Pass = "!C0rb13521!";

    public static MySqlConnection GetConexao() {
      return new MySqlConnection($"Server={Server};Port={Port};Database={Database};Uid={User};Pwd={Pass};SSL Mode=None");
    }

    public static string ConnectionString() {
      return $"Server={Server};Port={Port};Database={Database};Uid={User};Pwd={Pass};SSL Mode=None";
    }

    public void Dispose() {
      GC.Collect();
    }
  }
}

