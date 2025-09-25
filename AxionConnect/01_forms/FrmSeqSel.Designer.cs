namespace AxionConnect {
  partial class FrmSeqSel {
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
      this.flpOps = new LmCorbieUI.Controls.LmPanelFlow();
      this.lblTeste1 = new LmCorbieUI.Controls.LmLabel();
      this.lblTeste2 = new LmCorbieUI.Controls.LmLabel();
      this.flpOps.SuspendLayout();
      this.SuspendLayout();
      // 
      // flpOps
      // 
      this.flpOps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
      this.flpOps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.flpOps.Controls.Add(this.lblTeste1);
      this.flpOps.Controls.Add(this.lblTeste2);
      this.flpOps.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flpOps.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
      this.flpOps.Location = new System.Drawing.Point(0, 0);
      this.flpOps.Name = "flpOps";
      this.flpOps.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
      this.flpOps.Size = new System.Drawing.Size(287, 50);
      this.flpOps.TabIndex = 76;
      this.flpOps.WrapContents = false;
      // 
      // lblTeste1
      // 
      this.lblTeste1.BackColor = System.Drawing.Color.Transparent;
      this.lblTeste1.FontSize = LmCorbieUI.Design.LmLabelSize.Small;
      this.lblTeste1.ForeColor = System.Drawing.Color.Red;
      this.lblTeste1.Location = new System.Drawing.Point(3, 6);
      this.lblTeste1.Margin = new System.Windows.Forms.Padding(3);
      this.lblTeste1.Name = "lblTeste1";
      this.lblTeste1.Size = new System.Drawing.Size(275, 15);
      this.lblTeste1.TabIndex = 75;
      this.lblTeste1.Text = "Código Produto: sads sad  dsad ";
      this.lblTeste1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblTeste2
      // 
      this.lblTeste2.BackColor = System.Drawing.Color.Transparent;
      this.lblTeste2.FontSize = LmCorbieUI.Design.LmLabelSize.Small;
      this.lblTeste2.ForeColor = System.Drawing.Color.Red;
      this.lblTeste2.Location = new System.Drawing.Point(3, 27);
      this.lblTeste2.Margin = new System.Windows.Forms.Padding(3);
      this.lblTeste2.Name = "lblTeste2";
      this.lblTeste2.Size = new System.Drawing.Size(275, 15);
      this.lblTeste2.TabIndex = 74;
      this.lblTeste2.Text = "Código Produto:";
      this.lblTeste2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // FrmSeqSel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(287, 50);
      this.Controls.Add(this.flpOps);
      this.Name = "FrmSeqSel";
      this.Padding = new System.Windows.Forms.Padding(0);
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "FrmMoverSequencia";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSeqSel_FormClosed);
      this.Load += new System.EventHandler(this.FrmSeqSel_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSeqSel_KeyDown);
      this.flpOps.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private LmCorbieUI.Controls.LmPanelFlow flpOps;
    private LmCorbieUI.Controls.LmLabel lblTeste1;
    private LmCorbieUI.Controls.LmLabel lblTeste2;
  }
}