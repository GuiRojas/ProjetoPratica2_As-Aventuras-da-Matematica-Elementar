namespace Jogo
{
    partial class frmJogo
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmJogo));
            this.tmrPintar = new System.Windows.Forms.Timer(this.components);
            this.btnContinuar = new System.Windows.Forms.Button();
            this.btnComecar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.btnOnline = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tmrPintar
            // 
            this.tmrPintar.Enabled = true;
            this.tmrPintar.Interval = 20;
            this.tmrPintar.Tick += new System.EventHandler(this.tmrMain_Tick);
            // 
            // btnContinuar
            // 
            this.btnContinuar.Enabled = false;
            this.btnContinuar.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinuar.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.btnContinuar.Location = new System.Drawing.Point(225, 236);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(350, 64);
            this.btnContinuar.TabIndex = 0;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // btnComecar
            // 
            this.btnComecar.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComecar.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.btnComecar.Location = new System.Drawing.Point(225, 311);
            this.btnComecar.Name = "btnComecar";
            this.btnComecar.Size = new System.Drawing.Size(350, 64);
            this.btnComecar.TabIndex = 1;
            this.btnComecar.Text = "Novo jogo";
            this.btnComecar.UseVisualStyleBackColor = true;
            this.btnComecar.Click += new System.EventHandler(this.btnComecar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.btnSair.Location = new System.Drawing.Point(225, 548);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(350, 64);
            this.btnSair.TabIndex = 2;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(53)))), ((int)(((byte)(34)))));
            this.lbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(154, 59);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(268, 37);
            this.lbl1.TabIndex = 3;
            this.lbl1.Text = "As Aventuras da";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(53)))), ((int)(((byte)(34)))));
            this.lbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl2.Location = new System.Drawing.Point(77, 118);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(356, 37);
            this.lbl2.TabIndex = 4;
            this.lbl2.Text = "Matemática Elementar";
            // 
            // btnOnline
            // 
            this.btnOnline.Enabled = false;
            this.btnOnline.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOnline.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.btnOnline.Location = new System.Drawing.Point(225, 386);
            this.btnOnline.Name = "btnOnline";
            this.btnOnline.Size = new System.Drawing.Size(350, 64);
            this.btnOnline.TabIndex = 5;
            this.btnOnline.Text = "Modo online";
            this.btnOnline.UseVisualStyleBackColor = true;
            // 
            // frmJogo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(800, 640);
            this.Controls.Add(this.btnOnline);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnComecar);
            this.Controls.Add(this.btnContinuar);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmJogo";
            this.Text = "Aventuras Da Matematica Elementar";
            this.Load += new System.EventHandler(this.Jogo_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmJogo_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmJogo_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmJogo_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrPintar;
        private System.Windows.Forms.Button btnContinuar;
        private System.Windows.Forms.Button btnComecar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Button btnOnline;
        //   private System.Windows.Forms.Label label1;
    }
}

