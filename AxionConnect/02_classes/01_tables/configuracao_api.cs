using LmCorbieUI.Metodos.AtributosCustomizados;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using LmCorbieUI;
using System.Linq;
using System.Windows.Forms;
using System;
using System.Linq.Dynamic.Core;

namespace AxionConnect {
  internal class configuracao_api {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DataObjectField(true, false)]
    [LarguraColunaGrid(80)]
    [DisplayName("Código")]
    public int id { get; set; }

    [Browsable(false)]
    public int codigoEmpresa { get; set; }

    [Browsable(false)]
    [StringLength(250)]
    public string endereco { get; set; }

    [Browsable(false)]
    [StringLength(500)]
    public string token { get; set; }
    
    [Browsable(false)]
    [StringLength(250)]
    public string chave_edrawings { get; set; }

    [Browsable(false)]
    [StringLength(15)]
    public string mascara_peca { get; set; }
    
    [Browsable(false)]
    [StringLength(15)]
    public string mascara_conjunto { get; set; }

    ///// <summary>
    ///// último sequencial usado para o código reduzido (Quando alterado nivel 5, deve ser zerado
    ///// </summary>
    //[Browsable(false)]
    //public int sequencial { get; set; }

    [Browsable(false)]
    public int classificacaoOrigem { get; set; }

    [Browsable(false)]
    public int classificacaoFinalidade { get; set; }

    [Browsable(false)]
    public int tipoControleSaida { get; set; }

    [Browsable(false)]
    public int classificacaoFiscal { get; set; }

    [Browsable(false)]
    public int localizacaoEntrada { get; set; }
    
    [Browsable(false)]
    public int localizacaoSaida { get; set; }
    
    [Browsable(false)]
    [StringLength(10)]
    public string codContaContabil { get; set; }

    [Browsable(false)]
    public double perIPI { get; set; }

    public static bool Salvar(configuracao_api configuracao) {
      try {
        using (ContextoDados db = new ContextoDados()) {
          if (configuracao.id == 0) {

            db.configuracao_api.Add(configuracao);
            db.SaveChanges();
            return true;
          } else {
            var modelAlt = db.configuracao_api.FirstOrDefault(x => x.id == configuracao.id);
            modelAlt.codigoEmpresa = configuracao.codigoEmpresa;
            modelAlt.endereco = configuracao.endereco;
            modelAlt.token = configuracao.token;

            modelAlt.mascara_peca = configuracao.mascara_peca;
            modelAlt.mascara_conjunto = configuracao.mascara_conjunto;

            modelAlt.classificacaoOrigem = configuracao.classificacaoOrigem;
            modelAlt.classificacaoFinalidade = configuracao.classificacaoFinalidade;
            modelAlt.tipoControleSaida = configuracao.tipoControleSaida;
            modelAlt.classificacaoFiscal = configuracao.classificacaoFiscal;
            modelAlt.localizacaoEntrada = configuracao.localizacaoEntrada;
            modelAlt.localizacaoSaida = configuracao.localizacaoSaida;
            modelAlt.codContaContabil = configuracao.codContaContabil;
            modelAlt.perIPI = configuracao.perIPI;

            db.SaveChanges();
            return true;
          }
        }
      } catch (Exception ex) {
        MsgBox.Show($"Erro ao Salvar Configuração API.\r\n{ex.Message}", "Axion LM Projetos", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }
    }

    public static configuracao_api Selecionar() {
      var _return = new configuracao_api();

      try {
        using (ContextoDados db = new ContextoDados()) {
          _return = Queryable.FirstOrDefault(db.configuracao_api);
        }
      } catch (Exception ex) {
        Toast.Warning("Erro ao selecionar Configuração API.\n" +
            "-------------------------------------\n" +
            "" + ex.Message);
        _return = null;
      }

      return _return;
    }
  }
}
