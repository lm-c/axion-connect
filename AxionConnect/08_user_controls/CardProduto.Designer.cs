namespace AxionConnect {
  partial class CardProduto {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardProduto));
      this.lblInfo = new System.Windows.Forms.Label();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.btnMover = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.btnMover)).BeginInit();
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
      this.lblInfo.Size = new System.Drawing.Size(405, 15);
      this.lblInfo.TabIndex = 8;
      this.lblInfo.Text = "102030008 - Barra chata laminada 3/16\"x2\"x6000";
      this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lblInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LblInfo_MouseDown);
      this.lblInfo.MouseEnter += new System.EventHandler(this.CardProduto_MouseEnter);
      this.lblInfo.MouseLeave += new System.EventHandler(this.CardProduto_MouseLeave);
      // 
      // btnMover
      // 
      this.btnMover.Anchor = System.Windows.Forms.AnchorStyles.Right;
      this.btnMover.BackColor = System.Drawing.Color.Transparent;
      this.btnMover.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnMover.Image = ((System.Drawing.Image)(resources.GetObject("btnMover.Image")));
      this.btnMover.Location = new System.Drawing.Point(421, 5);
      this.btnMover.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.btnMover.Name = "btnMover";
      this.btnMover.Size = new System.Drawing.Size(20, 21);
      this.btnMover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.btnMover.TabIndex = 9;
      this.btnMover.TabStop = false;
      this.toolTip1.SetToolTip(this.btnMover, "Mover Para");
      // 
      // CardProduto
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.btnMover);
      this.Controls.Add(this.lblInfo);
      this.Font = new System.Drawing.Font("Segoe UI", 8.75F);
      this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.Name = "CardProduto";
      this.Padding = new System.Windows.Forms.Padding(8, 8, 28, 8);
      this.Size = new System.Drawing.Size(441, 31);
      this.UseCustomBackColor = true;
      this.UseCustomForeColor = true;
      this.MouseEnter += new System.EventHandler(this.CardProduto_MouseEnter);
      this.MouseLeave += new System.EventHandler(this.CardProduto_MouseLeave);
      ((System.ComponentModel.ISupportInitialize)(this.btnMover)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Label lblInfo;
    private System.Windows.Forms.ToolTip toolTip1;
    internal System.Windows.Forms.PictureBox btnMover;
  }
}
