using System;
using System.IO;
using System.Xml.Serialization;

namespace AxionConnect {
  public static class SerializadorXML<T> where T : class, new() {
    static private FileStream arquivo;
    static private XmlSerializer serializador;

    public static bool Serializar(string filename, Object obj) {
      try {
        using (arquivo = new FileStream(
            filename, FileMode.Create, FileAccess.Write, FileShare.Write)) {
          serializador = new XmlSerializer(typeof(T));
          serializador.Serialize(arquivo, obj);

          return true;
        }
      } catch (Exception) {
        return false;
      }
    }

    public static Object Deserializar(string filename) {
      try {
        using (arquivo = new FileStream(
            filename, FileMode.Open, FileAccess.Read, FileShare.Read)) {
          serializador = new XmlSerializer(typeof(T));
          Object obj = serializador.Deserialize(arquivo);

          return obj;
        }
      } catch (Exception) {
        return new object();
      }
    }
  }
}
