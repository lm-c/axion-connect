using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using LmCorbieUI.Metodos.AtributosCustomizados;
using System;

namespace AxionConnect {
  internal class produto_erp {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    [DataObjectField(true, false)]
    public int id { get; set; }

    [DataObjectField(false, true)]
    public long codigo_produto { get; set; }

    [StringLength(50)]
    public string codigo_componente { get; set; }

    public TipoComponente tipo_componente { get; set; }

    [StringLength(70)]
    public string descricao { get; set; }

    [StringLength(50)]
    public string name { get; set; }

    [StringLength(250)]
    public string pathname { get; set; }
    
    [StringLength(50)]
    public string referencia { get; set; } 
    
    [StringLength(150)]
    public string configuracao { get; set; }  
    
    public bool fantasma { get; set; }

    public double sobremetal_largura { get; set; } = 0;
    public double sobremetal_comprimento { get; set; } = 0;

    public double espessura { get; set; } = 0;
    public double largura { get; set; } = 0;
    public double comprimento { get; set; } = 0;
    public double peso_bruto { get; set; } = 0;
    public double peso_liquido { get; set; }
    public DateTime adicionado_em { get; set; }
    public DateTime alterado_em { get; set; }

    public int adicionado_por_id { get; set; }
    [ForeignKey("adicionado_por_id"), Browsable(false), NaoVerificarAlteracao]
    public usuarios adicionado_por { get; set; }
    
    public int alterado_por_id { get; set; }
    [ForeignKey("alterado_por_id"), Browsable(false), NaoVerificarAlteracao]
    public usuarios alterado_por { get; set; }



  }
}
