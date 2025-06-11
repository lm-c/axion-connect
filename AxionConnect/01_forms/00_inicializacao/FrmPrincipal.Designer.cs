
namespace AxionConnect
{
    partial class FrmPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
      this.msMenuJanelaAberta = new LmCorbieUI.Controls.LmMenuJanelaAberta();
      this.stpRodape = new LmCorbieUI.Controls.LmStatusStrip();
      this.tslVersao = new System.Windows.Forms.ToolStripStatusLabel();
      this.tslSecao = new System.Windows.Forms.ToolStripStatusLabel();
      this.tslUsuario = new System.Windows.Forms.ToolStripStatusLabel();
      this.tslFormAberto = new System.Windows.Forms.ToolStripStatusLabel();
      this.tslModo = new System.Windows.Forms.ToolStripStatusLabel();
      this.pnlMain = new LmCorbieUI.Controls.LmPanel();
      this.pnlMenu = new LmCorbieUI.Controls.LmPanel();
      this.menuLogout = new LmCorbieUI.Controls.LmMenuItem();
      this.menuSistema = new LmCorbieUI.Controls.LmMenuItem();
      this.pnlLogo = new LmCorbieUI.Controls.LmPanel();
      this.menuSandwich = new LmCorbieUI.Controls.LmMenuItem();
      this.ptbLogo = new System.Windows.Forms.PictureBox();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.sandwishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.asdasdsadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.stpRodape.SuspendLayout();
      this.pnlMenu.SuspendLayout();
      this.pnlLogo.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ptbLogo)).BeginInit();
      this.SuspendLayout();
      // 
      // msMenuJanelaAberta
      // 
      this.msMenuJanelaAberta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(54)))), ((int)(((byte)(71)))));
      this.msMenuJanelaAberta.Dock = System.Windows.Forms.DockStyle.Top;
      this.msMenuJanelaAberta.Location = new System.Drawing.Point(195, 26);
      this.msMenuJanelaAberta.Name = "msMenuJanelaAberta";
      this.msMenuJanelaAberta.Size = new System.Drawing.Size(666, 21);
      this.msMenuJanelaAberta.TabIndex = 1;
      this.msMenuJanelaAberta.UseSelectable = true;
      // 
      // stpRodape
      // 
      this.stpRodape.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(54)))), ((int)(((byte)(71)))));
      this.stpRodape.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.stpRodape.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.stpRodape.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslVersao,
            this.tslSecao,
            this.tslUsuario,
            this.tslFormAberto,
            this.tslModo});
      this.stpRodape.Location = new System.Drawing.Point(2, 453);
      this.stpRodape.Name = "stpRodape";
      this.stpRodape.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
      this.stpRodape.Size = new System.Drawing.Size(859, 22);
      this.stpRodape.SizingGrip = false;
      this.stpRodape.TabIndex = 2;
      this.stpRodape.Text = "lmStatusStrip1";
      // 
      // tslVersao
      // 
      this.tslVersao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.tslVersao.Name = "tslVersao";
      this.tslVersao.Size = new System.Drawing.Size(44, 17);
      this.tslVersao.Text = "Versão:";
      // 
      // tslSecao
      // 
      this.tslSecao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.tslSecao.Name = "tslSecao";
      this.tslSecao.Size = new System.Drawing.Size(40, 17);
      this.tslSecao.Text = "Seção:";
      // 
      // tslUsuario
      // 
      this.tslUsuario.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
      this.tslUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.tslUsuario.Name = "tslUsuario";
      this.tslUsuario.Size = new System.Drawing.Size(63, 17);
      this.tslUsuario.Text = "Usuário: X";
      // 
      // tslFormAberto
      // 
      this.tslFormAberto.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
      this.tslFormAberto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.tslFormAberto.Name = "tslFormAberto";
      this.tslFormAberto.Size = new System.Drawing.Size(75, 17);
      this.tslFormAberto.Text = "Form Aberto";
      // 
      // tslModo
      // 
      this.tslModo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
      this.tslModo.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
      this.tslModo.Name = "tslModo";
      this.tslModo.Size = new System.Drawing.Size(102, 17);
      this.tslModo.Tag = "MsgRodape";
      this.tslModo.Text = "Inserindo Registro";
      // 
      // pnlMain
      // 
      this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.IsPanelMenu = false;
      this.pnlMain.Location = new System.Drawing.Point(195, 47);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(666, 406);
      this.pnlMain.TabIndex = 3;
      // 
      // pnlMenu
      // 
      this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(54)))), ((int)(((byte)(71)))));
      this.pnlMenu.Controls.Add(this.menuLogout);
      this.pnlMenu.Controls.Add(this.menuSistema);
      this.pnlMenu.Controls.Add(this.pnlLogo);
      this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
      this.pnlMenu.IsPanelMenu = true;
      this.pnlMenu.Location = new System.Drawing.Point(2, 26);
      this.pnlMenu.Name = "pnlMenu";
      this.pnlMenu.Size = new System.Drawing.Size(193, 427);
      this.pnlMenu.TabIndex = 0;
      // 
      // menuLogout
      // 
      this.menuLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.menuLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.menuLogout.Image = ((System.Drawing.Image)(resources.GetObject("menuLogout.Image")));
      this.menuLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.menuLogout.Location = new System.Drawing.Point(0, 397);
      this.menuLogout.Name = "menuLogout";
      this.menuLogout.Size = new System.Drawing.Size(193, 30);
      this.menuLogout.TabIndex = 2;
      this.menuLogout.TabStop = false;
      this.menuLogout.Text = "   Sair";
      this.menuLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.menuLogout.UseSelectable = true;
      this.menuLogout.UseVisualStyleBackColor = false;
      this.menuLogout.Click += new System.EventHandler(this.MenuLogout_Click);
      // 
      // menuSistema
      // 
      this.menuSistema.Dock = System.Windows.Forms.DockStyle.Top;
      this.menuSistema.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.menuSistema.Image = ((System.Drawing.Image)(resources.GetObject("menuSistema.Image")));
      this.menuSistema.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.menuSistema.Location = new System.Drawing.Point(0, 52);
      this.menuSistema.Name = "menuSistema";
      this.menuSistema.Size = new System.Drawing.Size(193, 30);
      this.menuSistema.TabIndex = 1;
      this.menuSistema.TabStop = false;
      this.menuSistema.Text = "   Engenharia de Produto";
      this.menuSistema.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.menuSistema.UseSelectable = true;
      this.menuSistema.UseVisualStyleBackColor = false;
      this.menuSistema.Click += new System.EventHandler(this.MenuSistema_Click);
      // 
      // pnlLogo
      // 
      this.pnlLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(54)))), ((int)(((byte)(71)))));
      this.pnlLogo.Controls.Add(this.menuSandwich);
      this.pnlLogo.Controls.Add(this.ptbLogo);
      this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlLogo.IsPanelMenu = true;
      this.pnlLogo.Location = new System.Drawing.Point(0, 0);
      this.pnlLogo.Name = "pnlLogo";
      this.pnlLogo.Size = new System.Drawing.Size(193, 52);
      this.pnlLogo.TabIndex = 0;
      // 
      // menuSandwich
      // 
      this.menuSandwich.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.menuSandwich.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.menuSandwich.Image = ((System.Drawing.Image)(resources.GetObject("menuSandwich.Image")));
      this.menuSandwich.Location = new System.Drawing.Point(72, 26);
      this.menuSandwich.Name = "menuSandwich";
      this.menuSandwich.Size = new System.Drawing.Size(121, 26);
      this.menuSandwich.TabIndex = 1;
      this.menuSandwich.TabStop = false;
      this.menuSandwich.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.menuSandwich.UseSelectable = true;
      this.menuSandwich.UseVisualStyleBackColor = false;
      this.menuSandwich.Click += new System.EventHandler(this.MenuSandwich_Click);
      // 
      // ptbLogo
      // 
      this.ptbLogo.Dock = System.Windows.Forms.DockStyle.Left;
      this.ptbLogo.Image = ((System.Drawing.Image)(resources.GetObject("ptbLogo.Image")));
      this.ptbLogo.Location = new System.Drawing.Point(0, 0);
      this.ptbLogo.Name = "ptbLogo";
      this.ptbLogo.Size = new System.Drawing.Size(72, 52);
      this.ptbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.ptbLogo.TabIndex = 0;
      this.ptbLogo.TabStop = false;
      // 
      // sandwishToolStripMenuItem
      // 
      this.sandwishToolStripMenuItem.AutoSize = false;
      this.sandwishToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sandwishToolStripMenuItem.Image")));
      this.sandwishToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.sandwishToolStripMenuItem.Name = "sandwishToolStripMenuItem";
      this.sandwishToolStripMenuItem.Size = new System.Drawing.Size(113, 30);
      // 
      // asdasdsadToolStripMenuItem
      // 
      this.asdasdsadToolStripMenuItem.AutoSize = false;
      this.asdasdsadToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("asdasdsadToolStripMenuItem.Image")));
      this.asdasdsadToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.asdasdsadToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.asdasdsadToolStripMenuItem.Name = "asdasdsadToolStripMenuItem";
      this.asdasdsadToolStripMenuItem.Size = new System.Drawing.Size(113, 30);
      this.asdasdsadToolStripMenuItem.Text = " Controles";
      // 
      // FrmPrincipal
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(863, 477);
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.msMenuJanelaAberta);
      this.Controls.Add(this.pnlMenu);
      this.Controls.Add(this.stpRodape);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Location = new System.Drawing.Point(0, 0);
      this.MinimumSize = new System.Drawing.Size(429, 260);
      this.Name = "FrmPrincipal";
      this.Padding = new System.Windows.Forms.Padding(2, 26, 2, 2);
      this.Text = "Form1";
      this.Loaded += new LmCorbieUI.LmForms.LmMainForm.FormLoad(this.FrmPrincipal_Loaded);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPrincipal_FormClosing);
      this.Load += new System.EventHandler(this.FrmPrincipal_Load);
      this.stpRodape.ResumeLayout(false);
      this.stpRodape.PerformLayout();
      this.pnlMenu.ResumeLayout(false);
      this.pnlLogo.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.ptbLogo)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem sandwishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asdasdsadToolStripMenuItem;
        private LmCorbieUI.Controls.LmMenuJanelaAberta msMenuJanelaAberta;
        private LmCorbieUI.Controls.LmStatusStrip stpRodape;
        private LmCorbieUI.Controls.LmPanel pnlMenu;
        private LmCorbieUI.Controls.LmMenuItem menuSandwich;
        private LmCorbieUI.Controls.LmPanel pnlLogo;
        private System.Windows.Forms.PictureBox ptbLogo;
        private LmCorbieUI.Controls.LmMenuItem menuSistema;
        private LmCorbieUI.Controls.LmMenuItem menuLogout;
        private System.Windows.Forms.ToolStripStatusLabel tslVersao;
        private System.Windows.Forms.ToolStripStatusLabel tslSecao;
        private System.Windows.Forms.ToolStripStatusLabel tslUsuario;
        private System.Windows.Forms.ToolStripStatusLabel tslFormAberto;
        private System.Windows.Forms.ToolStripStatusLabel tslModo;
        private System.Windows.Forms.ToolTip toolTip1;
        internal LmCorbieUI.Controls.LmPanel pnlMain;
  }
}

