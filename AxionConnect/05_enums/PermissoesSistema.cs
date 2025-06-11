using System.ComponentModel;

namespace AxionConnect {
  public enum TipoPermissao {
    Menu = 0,
    Formulario = 1,
    Configuracao = 2,
    Indefinido = 99
  }

  public enum PermissoesSistema {
    [Description("Solução Completa")]
    Solucao = 1,

    [Description("Aplicação de Matéria Prima"), PermissaoSistema(TipoPermissao.Formulario)]
    AplicacaoProcesso = 101,

    [Description("Propriedades Personalizadas"), PermissaoSistema(TipoPermissao.Formulario)]
    PropsPersonalizadas = 102,

    [Description("Desenhos"), PermissaoSistema(TipoPermissao.Menu)]
    Desenho = 103,

    [Description("Criar/Alterar Desenhos"), PermissaoSistema(TipoPermissao.Formulario)]
    CriarAlterarDesenhos = 10301,

    [Description("Atualizar Templates dos Desenhos"), PermissaoSistema(TipoPermissao.Formulario)]
    AtualizarTemplatesDesenhos = 10302,

    [Description("Exportar Arquivos"), PermissaoSistema(TipoPermissao.Menu)]
    Exportar = 104,

    [Description("Exportar PDF/DWG"), PermissaoSistema(TipoPermissao.Formulario)]
    ExportarPDF = 10401,

    [Description("Exportar DXF"), PermissaoSistema(TipoPermissao.Formulario)]
    ExportarDXF = 10402,

    [Description("Cadastros Gerais"), PermissaoSistema(TipoPermissao.Menu)]
    Cadastros = 105,

    [Description("Cadastro de Usuário"), PermissaoSistema(TipoPermissao.Formulario)]
    UsuarioCad = 10501,

    [Description("Cadastro de Perfil do Usuário"), PermissaoSistema(TipoPermissao.Formulario)]
    PerfilUsuarioCad = 10502,

    [Description("Cadastro de Material"), PermissaoSistema(TipoPermissao.Formulario)]
    MaterialCad = 10503,

    [Description("Cadastro de Matéria prima"), PermissaoSistema(TipoPermissao.Formulario)]
    MateriaPrimaCad = 10504,

    [Description("Redefinir Senha"), PermissaoSistema(TipoPermissao.Formulario)]
    SenhaRedefinir = 10505,

    [Description("Relatórios"), PermissaoSistema(TipoPermissao.Menu)]
    Relatorios = 106,

    [Description("Processo de Fabricação"), PermissaoSistema(TipoPermissao.Formulario)]
    ProcessoFabricacao = 10601,

    [Description("Pack List"), PermissaoSistema(TipoPermissao.Formulario)]
    PackList = 10602,

    [Description("Plano de Pintura"), PermissaoSistema(TipoPermissao.Formulario)]
    PlanoPintura = 10603,
    
    [Description("Manutenção Packlist e Plano de Pintura"), PermissaoSistema(TipoPermissao.Formulario)]
    ManutencaoPinturaPackList = 10604,

    [Description("Report Works"), PermissaoSistema(TipoPermissao.Formulario)]
    ReportWorks = 10605,

    [Description("Integração ERP"), PermissaoSistema(TipoPermissao.Menu)]
    menuIntegracao = 107,
    
    [Description("Cadastro de Produto/Engenharia"), PermissaoSistema(TipoPermissao.Formulario)]
    produtoCad = 10701,
    
    [Description("Configuração de Processo"), PermissaoSistema(TipoPermissao.Formulario)]
    processoConfig = 10702,
    
    [Description("Configuração de Integração"), PermissaoSistema(TipoPermissao.Formulario)]
    IntegracaoConfig = 10703,

    [Description("Configurações"), PermissaoSistema(TipoPermissao.Formulario)]
    Configuracao = 108,


  }
}
