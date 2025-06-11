namespace AxionConnect {
  partial class CardOperacao {
    /// <summary> 
    /// Variável de designer necessária.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Limpar os recursos que estão sendo usados.
    /// </summary>
    /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Código gerado pelo Designer de Componentes

    /// <summary> 
    /// Método necessário para suporte ao Designer - não modifique 
    /// o conteúdo deste método com o editor de código.
    /// </summary>
    private void InitializeComponent() {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardOperacao));
      this.lblInfo = new System.Windows.Forms.Label();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.btnRemover = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.btnRemover)).BeginInit();
      this.SuspendLayout();
      // 
      // lblInfo
      // 
      this.lblInfo.AutoEllipsis = true;
      this.lblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblInfo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
      this.lblInfo.Location = new System.Drawing.Point(8, 8);
      this.lblInfo.Margin = new System.Windows.Forms.Padding(0);
      this.lblInfo.Name = "lblInfo";
      this.lblInfo.Size = new System.Drawing.Size(405, 47);
      this.lblInfo.TabIndex = 8;
      this.lblInfo.Text = "Operação: 105 DOBRAR\r\nMáquina: 209 DOBRADEIRA DOBRA LEVE ATÉ 4,75 SORG 3 METROS\r\n" +
    "Tempo Operação: 00:00 | Número Operadores: 1";
      this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lblInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LblInfo_MouseDown);
      this.lblInfo.MouseEnter += new System.EventHandler(this.CardOperacao_MouseEnter);
      this.lblInfo.MouseLeave += new System.EventHandler(this.CardOperacao_MouseLeave);
      // 
      // btnRemover
      // 
      this.btnRemover.Anchor = System.Windows.Forms.AnchorStyles.Right;
      this.btnRemover.BackColor = System.Drawing.Color.Transparent;
      this.btnRemover.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnRemover.Image = ((System.Drawing.Image)(resources.GetObject("btnRemover.Image")));
      this.btnRemover.Location = new System.Drawing.Point(421, 21);
      this.btnRemover.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.btnRemover.Name = "btnRemover";
      this.btnRemover.Size = new System.Drawing.Size(20, 21);
      this.btnRemover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.btnRemover.TabIndex = 7;
      this.btnRemover.TabStop = false;
      this.toolTip1.SetToolTip(this.btnRemover, "Remover Operação");
      this.btnRemover.Click += new System.EventHandler(this.BtnRemover_Click);
      // 
      // CardOperacao
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.btnRemover);
      this.Controls.Add(this.lblInfo);
      this.Font = new System.Drawing.Font("Segoe UI", 8.75F);
      this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.Name = "CardOperacao";
      this.Padding = new System.Windows.Forms.Padding(8, 8, 28, 8);
      this.Size = new System.Drawing.Size(441, 63);
      this.UseCustomBackColor = true;
      this.UseCustomForeColor = true;
      this.MouseEnter += new System.EventHandler(this.CardOperacao_MouseEnter);
      this.MouseLeave += new System.EventHandler(this.CardOperacao_MouseLeave);
      ((System.ComponentModel.ISupportInitialize)(this.btnRemover)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion
    public System.Windows.Forms.PictureBox btnRemover;
    private System.Windows.Forms.Label lblInfo;
    private System.Windows.Forms.ToolTip toolTip1;
  }
}
