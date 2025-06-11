using LmCorbieUI.Metodos.AtributosCustomizados;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using LmCorbieUI.Metodos;
using LmCorbieUI;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Linq.Dynamic.Core;
using System;
using System.Data;

namespace AxionConnect {
  internal class perfis {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DataObjectField(true, false)]
    [NaoVerificarAlteracao]
    [LarguraColunaGrid(50)]
    public int id { get; set; }

    [DataObjectField(false, true)]
    [Required(ErrorMessage = "Campo \"Descrição\" é Obrigatório!")]
    [StringLength(80)]
    [LarguraColunaGrid(250)]
    public string descricao { get; set; }

    [Required(ErrorMessage = "Campo \"Permissão\" é Obrigatório!")]
    [LarguraColunaGrid(150)]
    public string permissoes { get; set; }

    [Browsable(false)]
    [StringLength(255, ErrorMessage = "Comprimento da observação dever ter no máximo 255 Caracteres!")]
    public string observacao { get; set; }

    public static bool Salvar(perfis model) {
      using (ContextoDados db = new ContextoDados())
      using (DbContextTransaction transaction = db.Database.BeginTransaction(IsolationLevel.ReadCommitted)) {

        try {
          if (model.id == 0) {
            if (db.perfis.Any(x => x.descricao == model.descricao)) {
              Toast.Warning("Já existe um perfil com esta descrição");
              transaction.Rollback();
              return false;
            }

            db.perfis.Add(model);
            db.SaveChanges();
            transaction.Commit();
            Toast.Success("Salvo com Sucesso!");
          } else {
            if (db.perfis.Any(x => x.descricao == model.descricao && x.id != model.id)) {
              Toast.Warning("Já existe um perfil com esta descrição");
              transaction.Rollback();
              return false;
            }

            var modelAlt = db.perfis.FirstOrDefault(x => x.id == model.id);
            modelAlt.descricao = model.descricao;
            modelAlt.observacao = model.observacao;
            modelAlt.permissoes = model.permissoes;

            db.SaveChanges();
            transaction.Commit();
            Toast.Success("Alterado com Sucesso!");
          }

          db.SaveChanges();

          return true;
        } catch (Exception ex) {
          transaction.Rollback();
          Toast.Error($"Erro ao Salvar Perfil.\r\n" + ex.Message);

          return false;
        }
      }
    }

    public static bool Excluir(int perfilID) {
      using (ContextoDados db = new ContextoDados())
      using (DbContextTransaction transaction = db.Database.BeginTransaction(IsolationLevel.ReadCommitted)) {

        try {

          var modelAlt = db.perfis.FirstOrDefault(x => x.id == perfilID);

          db.perfis.Remove(modelAlt);

          db.SaveChanges();
          transaction.Commit();
          Toast.Success("Alterado com Sucesso!");

          db.SaveChanges();

          return true;
        } catch (Exception ex) {
          transaction.Rollback();
          Toast.Error($"Erro ao Salvar Perfil.\r\n" + ex.Message);

          return false;
        }
      }
    }

    public static SortableBindingList<W_UsuarioPerfil> SelecionarPerfis(int idUsuario = 0) {
      List<W_UsuarioPerfil> _return = new List<W_UsuarioPerfil>();

      using (ContextoDados db = new ContextoDados()) {
        if (idUsuario > 0) {
          var usu = (from x in db.usuarios.Where(x => x.id == idUsuario) select new { x.perfil }).FirstOrDefault();

          foreach (var idperf in (usu.perfil.Split('^'))) {
            int id = Convert.ToInt32(idperf);

            var perf = db.perfis.Where(x => x.id == id).FirstOrDefault();

            if (perf != null)
              _return.Add(new W_UsuarioPerfil {
                Codigo = perf.id,
                Descricao = perf.descricao,
                Observacao = perf.observacao,
                Permissoes = perf.permissoes,
              });
          }

        } else if (idUsuario == -1) {
          var pfs = (db.perfis).ToList();

          foreach (perfis p in pfs)
            _return.Add(new W_UsuarioPerfil { Codigo = p.id, Descricao = p.descricao });
        }
      }

      return new SortableBindingList<W_UsuarioPerfil>(_return);
    }

    public static SortableBindingList<W_Perfil> Selecionar(int? perfilID = null, int? profissionalID = null, int? possuiPermissao = null, int? naoPossuiPermissao = null) {
      List<W_Perfil> _return = new List<W_Perfil>();

      using (ContextoDados db = new ContextoDados()) {
        object[] valores = new object[10];
        string condicoes = string.Empty;
        short pos = 0;

        condicoes += $"ID >= @{pos} && ";
        valores[pos] = 0;
        pos++;

        if (perfilID != null) {
          condicoes += $"ID == @{pos} && ";
          valores[pos] = perfilID;
          pos++;
        }

        condicoes = condicoes.Substring(0, condicoes.Length - 3);

        var query = Enumerable.ToList(
            from x in db.perfis
            .Where(condicoes, valores)
            select new {
              x.id,
              x.descricao,
              x.observacao,
              x.permissoes,
            });

        foreach (var item in query) {
          if (profissionalID != null) {
            var usuPerfs = db.usuarios.Where(x => x.id == profissionalID).FirstOrDefault().perfil;
            var splPrf = usuPerfs.Split('^');

            if (!splPrf.Contains(item.id.ToString()))
              continue;
          }

          if (possuiPermissao != null) {
            var splPrm = item.permissoes.Split('^');

            if (!splPrm.Contains(possuiPermissao.ToString()))
              continue;
          }

          if (naoPossuiPermissao != null) {
            var splPrm = item.permissoes.Split('^');

            if (splPrm.Contains(naoPossuiPermissao.ToString()))
              continue;
          }

          _return.Add(new W_Perfil {
            Codigo = item.id,
            Descricao = item.descricao,
            Observacao = item.observacao,
            Permissoes = item.permissoes,
          });
        }
      }

      return new SortableBindingList<W_Perfil>(_return);
    }

    public static SortableBindingList<Z_Padrao> SelecionarUsuarios(int perfilID) {
      List<Z_Padrao> _return = new List<Z_Padrao>();

      using (ContextoDados db = new ContextoDados()) {
        var usus = Enumerable.ToList(
            from x in db.usuarios.Where(x => x.ativo)
            .OrderBy(x => x.nome)
            select new { x.id, x.nome, x.perfil });

        foreach (var usu in usus) {
          var perfs = usu.perfil.Split('^');
          if (perfs.Contains(perfilID.ToString()))
            _return.Add(new Z_Padrao { Codigo = usu.id, Descricao = usu.nome });
        }

      }

      return new SortableBindingList<Z_Padrao>(_return);
    }

    public static SortableBindingList<W_Permissao> SelecionarPermissoes(int perfilID, out SortableBindingList<W_Permissao> permissaoNao) {
      List<W_Permissao> _returnSim = new List<W_Permissao>();
      List<W_Permissao> _returnNao = new List<W_Permissao>();

      using (ContextoDados db = new ContextoDados()) {
        var perfil = Queryable.FirstOrDefault(
            from x in db.perfis.Where(x => x.id == perfilID)
            .OrderBy(x => x.descricao)
            select new { x.permissoes });

        var permsSim = perfil.permissoes.Split('^');
        foreach (var permSim in permsSim) {
          try {
            var bmp = new Bitmap(20, 20);
            int permID = Convert.ToInt32(permSim);
            var permissao = ((PermissoesSistema)permID).ObterDescricaoEnum();
            var tipo = (TipoPermissao)((PermissoesSistema)permID).ObterPermissao();

            switch (tipo) {
              case TipoPermissao.Menu:
              bmp = Properties.Resources.menu;
              break;
              case TipoPermissao.Formulario:
              bmp = Properties.Resources.windows;
              break;
              case TipoPermissao.Configuracao:
              bmp = Properties.Resources.tools;
              break;
              default:
              break;
            }

            _returnSim.Add(new W_Permissao { ImgAnexo = bmp, Codigo = permID, Descricao = permissao });
          } catch (Exception) {

          }
        }

        var perfis = Enumerable.ToList(
            from x in db.perfis.Where(x => x.id != perfilID)
            .OrderBy(x => x.descricao)
            select new { x.permissoes });

        foreach (var prf in perfis) {
          var permsNao = prf.permissoes.Split('^');
          foreach (var permNao in permsNao) {
            try {
              int permID = Convert.ToInt32(permNao);

              if (!_returnSim.Any(x => x.Codigo == permID) && !_returnNao.Any(x => x.Codigo == permID)) {
                var bmp = new Bitmap(20, 20);
                var permissao = ((PermissoesSistema)permID).ObterDescricaoEnum();
                var tipo = (TipoPermissao)((PermissoesSistema)permID).ObterPermissao();

                switch (tipo) {
                  case TipoPermissao.Menu:
                  bmp = Properties.Resources.menu;
                  break;
                  case TipoPermissao.Formulario:
                  bmp = Properties.Resources.windows;
                  break;
                  case TipoPermissao.Configuracao:
                  bmp = Properties.Resources.tools;
                  break;
                  default:
                  break;
                }

                _returnNao.Add(new W_Permissao { ImgAnexo = bmp, Codigo = permID, Descricao = permissao });
              }
            } catch (Exception) {

            }
          }
        }

      }

      permissaoNao = new SortableBindingList<W_Permissao>(_returnNao);
      return new SortableBindingList<W_Permissao>(_returnSim);
    }

    public static SortableBindingList<W_Permissao> SelecionarPermissoesCombo() {
      List<W_Permissao> _return = new List<W_Permissao>();

      using (ContextoDados db = new ContextoDados()) {
        var lista = typeof(PermissoesSistema).ObterListaItens();

        foreach (KeyValuePair<Enum, string> item in lista) {
          try {
            var bmp = new Bitmap(20, 20);
            var permID = (int)(PermissoesSistema)item.Key;

            var descricao = item.Value;

            _return.Add(new W_Permissao { ImgAnexo = bmp, Codigo = permID, Descricao = descricao });
          } catch (Exception) {

          }
        }

      }

      return new SortableBindingList<W_Permissao>(_return.OrderBy(x => x.Descricao).ToList());
    }
  }
}
