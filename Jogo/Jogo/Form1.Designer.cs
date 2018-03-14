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
            this.tmrPintar = new System.Windows.Forms.Timer(this.components);
            this.tmrBackground = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tmrPintar
            // 
            this.tmrPintar.Enabled = true;
            this.tmrPintar.Interval = 20;
            this.tmrPintar.Tick += new System.EventHandler(this.tmrMain_Tick);
            // 
            // tmrBackground
            // 
            this.tmrBackground.Enabled = true;
            this.tmrBackground.Interval = 20;
            this.tmrBackground.Tick += new System.EventHandler(this.tmrBackground_Tick);
            // 
            // frmJogo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::Jogo.Properties.Resources.fundo1;
            this.ClientSize = new System.Drawing.Size(800, 640);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmJogo";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Jogo_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmJogo_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmJogo_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmJogo_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrPintar;
        private System.Windows.Forms.Timer tmrBackground;
    }
}

