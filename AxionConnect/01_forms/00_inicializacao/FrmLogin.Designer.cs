namespace AxionConnect {
  partial class FrmLogin {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
      this.cmxToolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.txtSenha = new LmCorbieUI.Controls.LmTextBox();
      this.txtUsuario = new LmCorbieUI.Controls.LmTextBox();
      this.btnLogin = new LmCorbieUI.Controls.LmButton();
      this.ptb = new System.Windows.Forms.PictureBox();
      this.lblCarregando = new LmCorbieUI.Controls.LmLabel();
      ((System.ComponentModel.ISupportInitialize)(this.ptb)).BeginInit();
      this.SuspendLayout();
      // 
      // txtSenha
      // 
      this.txtSenha.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.txtSenha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(238)))), ((int)(((byte)(242)))));
      this.txtSenha.BorderRadius = 0;
      this.txtSenha.BorderSize = 2;
      this.txtSenha.CampoObrigatorio = true;
      this.txtSenha.F7ToolTipText = null;
      this.txtSenha.F8ToolTipText = null;
      this.txtSenha.F9ToolTipText = null;
      this.txtSenha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.txtSenha.Icon = ((System.Drawing.Image)(resources.GetObject("txtSenha.Icon")));
      this.txtSenha.IconF7 = null;
      this.txtSenha.IconToolTipText = null;
      this.txtSenha.Lines = new string[0];
      this.txtSenha.Location = new System.Drawing.Point(138, 125);
      this.txtSenha.MaxLength = 32767;
      this.txtSenha.Name = "txtSenha";
      this.txtSenha.PasswordChar = '●';
      this.txtSenha.Propriedade = null;
      this.txtSenha.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.txtSenha.SelectedText = "";
      this.txtSenha.SelectionLength = 0;
      this.txtSenha.SelectionStart = 0;
      this.txtSenha.ShortcutsEnabled = true;
      this.txtSenha.ShowIcon = true;
      this.txtSenha.Size = new System.Drawing.Size(220, 30);
      this.txtSenha.TabIndex = 4;
      this.txtSenha.UnderlinedStyle = true;
      this.txtSenha.UseSelectable = true;
      this.txtSenha.UseSystemPasswordChar = true;
      this.txtSenha.Valor_Decimais = ((short)(0));
      this.txtSenha.WaterMark = "Senha";
      this.txtSenha.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(95)))), ((int)(((byte)(95)))));
      this.txtSenha.WaterMarkFont = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
      // 
      // txtUsuario
      // 
      this.txtUsuario.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.txtUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(238)))), ((int)(((byte)(242)))));
      this.txtUsuario.BorderRadius = 0;
      this.txtUsuario.BorderSize = 2;
      this.txtUsuario.CampoObrigatorio = true;
      this.txtUsuario.F7ToolTipText = null;
      this.txtUsuario.F8ToolTipText = null;
      this.txtUsuario.F9ToolTipText = null;
      this.txtUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.txtUsuario.Icon = ((System.Drawing.Image)(resources.GetObject("txtUsuario.Icon")));
      this.txtUsuario.IconF7 = null;
      this.txtUsuario.IconToolTipText = null;
      this.txtUsuario.Lines = new string[0];
      this.txtUsuario.Location = new System.Drawing.Point(138, 89);
      this.txtUsuario.MaxLength = 32767;
      this.txtUsuario.Name = "txtUsuario";
      this.txtUsuario.PasswordChar = '\0';
      this.txtUsuario.Propriedade = null;
      this.txtUsuario.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.txtUsuario.SelectedText = "";
      this.txtUsuario.SelectionLength = 0;
      this.txtUsuario.SelectionStart = 0;
      this.txtUsuario.ShortcutsEnabled = true;
      this.txtUsuario.ShowIcon = true;
      this.txtUsuario.Size = new System.Drawing.Size(220, 30);
      this.txtUsuario.TabIndex = 3;
      this.txtUsuario.UnderlinedStyle = true;
      this.txtUsuario.UseSelectable = true;
      this.txtUsuario.Valor_Decimais = ((short)(0));
      this.txtUsuario.WaterMark = "Usuário";
      this.txtUsuario.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(95)))), ((int)(((byte)(95)))));
      this.txtUsuario.WaterMarkFont = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
      // 
      // btnLogin
      // 
      this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.btnLogin.BorderColor = System.Drawing.Color.PaleVioletRed;
      this.btnLogin.BorderRadius = 15;
      this.btnLogin.BorderSize = 0;
      this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnLogin.Image = ((System.Drawing.Image)(resources.GetObject("btnLogin.Image")));
      this.btnLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnLogin.Location = new System.Drawing.Point(138, 178);
      this.btnLogin.Name = "btnLogin";
      this.btnLogin.Size = new System.Drawing.Size(220, 30);
      this.btnLogin.TabIndex = 5;
      this.btnLogin.Text = " Entrar";
      this.btnLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnLogin.UseVisualStyleBackColor = false;
      this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
      // 
      // ptb
      // 
      this.ptb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.ptb.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ptb.BackgroundImage")));
      this.ptb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.ptb.Location = new System.Drawing.Point(6, 34);
      this.ptb.Name = "ptb";
      this.ptb.Size = new System.Drawing.Size(485, 229);
      this.ptb.TabIndex = 6;
      this.ptb.TabStop = false;
      // 
      // lblCarregando
      // 
      this.lblCarregando.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.lblCarregando.BackColor = System.Drawing.Color.Transparent;
      this.lblCarregando.FontSize = LmCorbieUI.Design.LmLabelSize.Tall;
      this.lblCarregando.FontWeight = LmCorbieUI.Design.LmLabelWeight.Bold;
      this.lblCarregando.Location = new System.Drawing.Point(30, 244);
      this.lblCarregando.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.lblCarregando.Name = "lblCarregando";
      this.lblCarregando.Size = new System.Drawing.Size(436, 39);
      this.lblCarregando.TabIndex = 349;
      this.lblCarregando.Text = "Carregando...";
      this.lblCarregando.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // FrmLogin
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(497, 296);
      this.Controls.Add(this.lblCarregando);
      this.Controls.Add(this.txtSenha);
      this.Controls.Add(this.txtUsuario);
      this.Controls.Add(this.btnLogin);
      this.Controls.Add(this.ptb);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Location = new System.Drawing.Point(0, 0);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FrmLogin";
      this.Text = "Logar no Sistema";
      this.TextAlign = LmCorbieUI.LmForms.LmFormTextAlign.Center;
      this.Loaded += new LmCorbieUI.LmForms.LmSingleForm.FormLoad(this.FrmLogin_Loaded);
      this.Load += new System.EventHandler(this.FrmLogin_Load);
      ((System.ComponentModel.ISupportInitialize)(this.ptb)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolTip cmxToolTip1;
    private LmCorbieUI.Controls.LmTextBox txtSenha;
    private LmCorbieUI.Controls.LmTextBox txtUsuario;
    private LmCorbieUI.Controls.LmButton btnLogin;
    private System.Windows.Forms.PictureBox ptb;
    private LmCorbieUI.Controls.LmLabel lblCarregando;
  }
}