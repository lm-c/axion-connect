using LmCorbieUI;
using LmCorbieUI.Metodos.AtributosCustomizados;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Forms;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace AxionConnect {
  internal class usuarios {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DataObjectField(true, false)]
    [NaoVerificarAlteracao]
    [LarguraColunaGrid(50)]
    public int id { get; set; }

    [Required(ErrorMessage = "Campo \"Nome\" é Obrigatório!")]
    [StringLength(100)]
    [LarguraColunaGrid(250)]
    public string nome { get; set; }

    [Required(ErrorMessage = "Campo \"Login\" é Obrigatório!")]
    [StringLength(30)]
    [LarguraColunaGrid(150)]
    [DataObjectField(false, true)]
    public string login { get; set; }

    [Browsable(false)]
    [StringLength(100, ErrorMessage = "Comprimento da senha dever ter no máximo 100 Caracteres!")]
    public string senha { get; set; }

    [Browsable(false)]
    [StringLength(250, ErrorMessage = "Comprimento do Email dever ter no máximo 250 Caracteres!")]
    public string email { get; set; }

    [Browsable(false)]
    [StringLength(45)]
    [Required(ErrorMessage = "Campo \"Perfil\" é Obrigatório!")]
    public string perfil { get; set; }

    [LarguraColunaGrid(80)]
    public bool ativo { get; set; }

    //public static usuarios model = new usuarios();

    public static bool Salvar(usuarios usuario) {
      try {
        using (ContextoDados db = new ContextoDados()) {
          if (usuario.id == 0) {
            if (db.usuarios.Any(x => x.login == usuario.login)) {
              Toast.Warning("Já existe um usuário cadastrado com este Login");
              return false;
            }

            usuario.senha = usuario.senha;

            db.usuarios.Add(usuario);
            db.SaveChanges();

            Toast.Success("Usuario Cadastrado com Sucesso!");
          } else {
            if (db.usuarios.Any(x => x.login == usuario.login && x.id != usuario.id)) {
              Toast.Warning("Já existe um usuário cadastrado com este Login");
              return false;
            }

            var modelAlt = db.usuarios.FirstOrDefault(x => x.id == usuario.id);
            modelAlt.nome = usuario.nome;
            modelAlt.login = usuario.login;
            modelAlt.senha = usuario.senha;
            modelAlt.ativo = usuario.ativo;

            db.SaveChanges();
            Toast.Success("Usuario Alterado com Sucesso!");
          }
          return true;
        }
      } catch (Exception ex) {
        MsgBox.Show("Erro ao Salvar Usuario.", "Axion LM Projetos", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }
    }

    public static void Excluir(int usuario_id) {
      try {
        using (ContextoDados db = new ContextoDados()) {
          db.usuarios.Remove(db.usuarios.FirstOrDefault(x => x.id == usuario_id));
          db.SaveChanges();
        }
      } catch (Exception ex) {
        MsgBox.Show("Erro ao Excluir Usuario.", "Axion LM Projetos", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    public static List<usuarios> Selecionar(bool? ativo = null) {
      try {
        using (ContextoDados db = new ContextoDados()) {
          object[] valores = new object[10];
          string condicoes = string.Empty;
          short pos = 0;

          condicoes += $"id >= @{pos} && ";
          valores[pos] = 0;
          pos++;

          if (ativo != null) {
            condicoes += $"ativo == @{pos} && ";
            valores[pos] = ativo;
            pos++;
          }

          condicoes = condicoes.Substring(0, condicoes.Length - 3);

          return db.usuarios.Where(condicoes, valores).OrderBy(x=>x.login).ToList();
        }
      } catch (Exception ex) {
        Toast.Error("Erro ao Listar Usuarios.\r\n" + ex.Message);
        return new List<usuarios>();
      }
    }

    public static usuarios Selecionar(int id) {
      try {
        using (ContextoDados db = new ContextoDados()) {
          return db.usuarios.FirstOrDefault(x => x.id == id);
        }
      } catch (Exception ex) {
        Toast.Error("Erro ao Selecionar Usuario.\r\n" + ex.Message);
        return new usuarios();
      }
    }


    public static List<string> SelecionarPermissoes(int idUsuario) {
      List<string> _return = new List<string>();

      using (ContextoDados db = new ContextoDados()) {
        var usu = (from x in db.usuarios.Where(x => x.id == idUsuario) select new { x.perfil }).FirstOrDefault();

        foreach (var idperf in usu.perfil.Split('^')) {
          int id = Convert.ToInt32(idperf);

          var perf = db.perfis.Where(x => x.id == id).FirstOrDefault();

          if (perf == null)
            continue;

          string[] spl = perf.permissoes.Split('^');
          foreach (var s in spl) {
            if (!_return.Contains(s))
              _return.Add(s);
          }
        }
      }

      return _return;
    }

  }
}
