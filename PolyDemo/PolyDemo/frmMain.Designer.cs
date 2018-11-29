namespace PolyDemo
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnShapes = new System.Windows.Forms.Button();
            this.tmrAddShapes = new System.Windows.Forms.Timer(this.components);
            this.btnRenderable = new System.Windows.Forms.Button();
            this.cbErase = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnShapes
            // 
            this.btnShapes.Location = new System.Drawing.Point(12, 24);
            this.btnShapes.Name = "btnShapes";
            this.btnShapes.Size = new System.Drawing.Size(75, 23);
            this.btnShapes.TabIndex = 0;
            this.btnShapes.Text = "Shapes";
            this.btnShapes.UseVisualStyleBackColor = true;
            this.btnShapes.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // tmrAddShapes
            // 
            this.tmrAddShapes.Interval = 1000;
            this.tmrAddShapes.Tick += new System.EventHandler(this.tmrAddShapes_Tick);
            // 
            // btnRenderable
            // 
            this.btnRenderable.Location = new System.Drawing.Point(101, 24);
            this.btnRenderable.Name = "btnRenderable";
            this.btnRenderable.Size = new System.Drawing.Size(75, 23);
            this.btnRenderable.TabIndex = 2;
            this.btnRenderable.Text = "Renderable";
            this.btnRenderable.UseVisualStyleBackColor = true;
            this.btnRenderable.Click += new System.EventHandler(this.btnRenderable_Click);
            // 
            // cbErase
            // 
            this.cbErase.AutoSize = true;
            this.cbErase.Checked = true;
            this.cbErase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbErase.Location = new System.Drawing.Point(12, 59);
            this.cbErase.Name = "cbErase";
            this.cbErase.Size = new System.Drawing.Size(135, 17);
            this.cbErase.TabIndex = 3;
            this.cbErase.Text = "Erase Between Frames";
            this.cbErase.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AcceptButton = this.btnShapes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(190, 88);
            this.Controls.Add(this.cbErase);
            this.Controls.Add(this.btnRenderable);
            this.Controls.Add(this.btnShapes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "PolyDemo";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnShapes;
        private System.Windows.Forms.Timer tmrAddShapes;
        private System.Windows.Forms.Button btnRenderable;
        private System.Windows.Forms.CheckBox cbErase;
    }
}

