namespace MyKP
{
    partial class ClientMenu
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
            this.tables = new System.Windows.Forms.Button();
            this.bands = new System.Windows.Forms.Button();
            this.cashbox = new System.Windows.Forms.Button();
            this.bar = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.info = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tables
            // 
            this.tables.BackColor = System.Drawing.Color.Black;
            this.tables.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tables.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.tables.Location = new System.Drawing.Point(276, 332);
            this.tables.Name = "tables";
            this.tables.Size = new System.Drawing.Size(241, 44);
            this.tables.TabIndex = 23;
            this.tables.Text = "Столики";
            this.tables.UseVisualStyleBackColor = false;
            this.tables.Click += new System.EventHandler(this.tables_Click);
            // 
            // bands
            // 
            this.bands.BackColor = System.Drawing.Color.Black;
            this.bands.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bands.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.bands.Location = new System.Drawing.Point(276, 243);
            this.bands.Name = "bands";
            this.bands.Size = new System.Drawing.Size(243, 44);
            this.bands.TabIndex = 22;
            this.bands.Text = "Расписание групп";
            this.bands.UseVisualStyleBackColor = false;
            this.bands.Click += new System.EventHandler(this.bands_Click);
            // 
            // cashbox
            // 
            this.cashbox.BackColor = System.Drawing.Color.Black;
            this.cashbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cashbox.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.cashbox.Location = new System.Drawing.Point(276, 156);
            this.cashbox.Name = "cashbox";
            this.cashbox.Size = new System.Drawing.Size(243, 44);
            this.cashbox.TabIndex = 21;
            this.cashbox.Text = "Касса";
            this.cashbox.UseVisualStyleBackColor = false;
            this.cashbox.Click += new System.EventHandler(this.cashbox_Click);
            // 
            // bar
            // 
            this.bar.BackColor = System.Drawing.Color.Black;
            this.bar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bar.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.bar.Location = new System.Drawing.Point(276, 67);
            this.bar.Name = "bar";
            this.bar.Size = new System.Drawing.Size(243, 44);
            this.bar.TabIndex = 20;
            this.bar.Text = "Бар";
            this.bar.UseVisualStyleBackColor = false;
            this.bar.Click += new System.EventHandler(this.bar_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.Color.Black;
            this.buttonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonBack.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.buttonBack.Location = new System.Drawing.Point(18, 17);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(111, 44);
            this.buttonBack.TabIndex = 19;
            this.buttonBack.Text = "Выйти";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // info
            // 
            this.info.BackColor = System.Drawing.Color.Black;
            this.info.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.info.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.info.Location = new System.Drawing.Point(576, 385);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(212, 53);
            this.info.TabIndex = 24;
            this.info.Text = "О приложении";
            this.info.UseVisualStyleBackColor = false;
            this.info.Click += new System.EventHandler(this.info_Click);
            // 
            // ClientMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.info);
            this.Controls.Add(this.tables);
            this.Controls.Add(this.bands);
            this.Controls.Add(this.cashbox);
            this.Controls.Add(this.bar);
            this.Controls.Add(this.buttonBack);
            this.Name = "ClientMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ClientMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button tables;
        private System.Windows.Forms.Button bands;
        private System.Windows.Forms.Button cashbox;
        private System.Windows.Forms.Button bar;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button info;
    }
}