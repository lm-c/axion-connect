using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// As informações gerais sobre um assembly são controladas por
// conjunto de atributos. Altere estes valores de atributo para modificar as informações
// associadas a um assembly.
[assembly: AssemblyTitle(InfoAssembly.TitleView)]
[assembly: AssemblyDescription(InfoAssembly.DescrView)]
[assembly: AssemblyConfiguration(InfoAssembly.Configuration)]
[assembly: AssemblyCompany(InfoAssembly.Company)]
[assembly: AssemblyProduct(InfoAssembly.Product)]
[assembly: AssemblyCopyright(InfoAssembly.Copyright)]
[assembly: AssemblyTrademark(InfoAssembly.Trademark)]
[assembly: AssemblyCulture(InfoAssembly.Culture)]

// Definir ComVisible como false torna os tipos neste assembly invisíveis
// para componentes COM. Caso precise acessar um tipo neste assembly de
// COM, defina o atributo ComVisible como true nesse tipo.
[assembly: ComVisible(false)]

// O GUID a seguir será destinado à ID de typelib se este projeto for exposto para COM
[assembly: Guid("532f4008-0ae8-42a0-b329-c45b55aa6deb")]

[assembly: AssemblyVersion(InfoAssembly.Version)]
[assembly: AssemblyFileVersion(InfoAssembly.Version)]

public class InfoAssembly {
  public const string Version = "4.0.0.0";

  public const string TitleView = "Axion Connect Leonardo Michalak";

  public const string DescrView = "Sistema Integrador ERP, Licenciado para Artama";

  public const string Copyright = "Copyright © 2025 Leonardo Adriano Michalak. Todos os direitos reservados.";
  public const string Company = "Leonardo Adriano Michalak";
  public const string Product = "Sistema para importação de projeto para ERP";
  public const string Configuration = "";
  public const string Trademark = "";
  public const string Culture = "";
}

