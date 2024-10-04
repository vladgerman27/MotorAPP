namespace MyKP
{
    partial class EmployeeMenu
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
            this.salary = new System.Windows.Forms.Button();
            this.delivery = new System.Windows.Forms.Button();
            this.stock = new System.Windows.Forms.Button();
            this.bands = new System.Windows.Forms.Button();
            this.bar = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.employees = new System.Windows.Forms.Button();
            this.info = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // salary
            // 
            this.salary.BackColor = System.Drawing.Color.Black;
            this.salary.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.salary.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.salary.Location = new System.Drawing.Point(298, 356);
            this.salary.Name = "salary";
            this.salary.Size = new System.Drawing.Size(241, 44);
            this.salary.TabIndex = 24;
            this.salary.Text = "Зарплата";
            this.salary.UseVisualStyleBackColor = false;
            this.salary.Click += new System.EventHandler(this.salary_Click);
            // 
            // delivery
            // 
            this.delivery.BackColor = System.Drawing.Color.Black;
            this.delivery.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.delivery.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.delivery.Location = new System.Drawing.Point(296, 291);
            this.delivery.Name = "delivery";
            this.delivery.Size = new System.Drawing.Size(243, 44);
            this.delivery.TabIndex = 23;
            this.delivery.Text = "Поставки";
            this.delivery.UseVisualStyleBackColor = false;
            this.delivery.Click += new System.EventHandler(this.delivery_Click);
            // 
            // stock
            // 
            this.stock.BackColor = System.Drawing.Color.Black;
            this.stock.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stock.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.stock.Location = new System.Drawing.Point(298, 222);
            this.stock.Name = "stock";
            this.stock.Size = new System.Drawing.Size(243, 44);
            this.stock.TabIndex = 22;
            this.stock.Text = "Склад";
            this.stock.UseVisualStyleBackColor = false;
            this.stock.Click += new System.EventHandler(this.stock_Click);
            // 
            // bands
            // 
            this.bands.BackColor = System.Drawing.Color.Black;
            this.bands.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bands.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.bands.Location = new System.Drawing.Point(296, 154);
            this.bands.Name = "bands";
            this.bands.Size = new System.Drawing.Size(243, 44);
            this.bands.TabIndex = 21;
            this.bands.Text = "Расписание групп";
            this.bands.UseVisualStyleBackColor = false;
            this.bands.Click += new System.EventHandler(this.bands_Click);
            // 
            // bar
            // 
            this.bar.BackColor = System.Drawing.Color.Black;
            this.bar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bar.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.bar.Location = new System.Drawing.Point(296, 89);
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
            this.buttonBack.Location = new System.Drawing.Point(14, 27);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(111, 44);
            this.buttonBack.TabIndex = 19;
            this.buttonBack.Text = "Выйти";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // employees
            // 
            this.employees.BackColor = System.Drawing.Color.Black;
            this.employees.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.employees.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.employees.Location = new System.Drawing.Point(296, 27);
            this.employees.Name = "employees";
            this.employees.Size = new System.Drawing.Size(243, 44);
            this.employees.TabIndex = 18;
            this.employees.Text = "Сотрудники";
            this.employees.UseVisualStyleBackColor = false;
            this.employees.Click += new System.EventHandler(this.employees_Click);
            // 
            // info
            // 
            this.info.BackColor = System.Drawing.Color.Black;
            this.info.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.info.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.info.Location = new System.Drawing.Point(576, 352);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(212, 53);
            this.info.TabIndex = 26;
            this.info.Text = "О приложении";
            this.info.UseVisualStyleBackColor = false;
            this.info.Click += new System.EventHandler(this.info_Click);
            // 
            // EmployeeMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.info);
            this.Controls.Add(this.salary);
            this.Controls.Add(this.delivery);
            this.Controls.Add(this.stock);
            this.Controls.Add(this.bands);
            this.Controls.Add(this.bar);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.employees);
            this.Name = "EmployeeMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmployeeMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button salary;
        private System.Windows.Forms.Button delivery;
        private System.Windows.Forms.Button stock;
        private System.Windows.Forms.Button bands;
        private System.Windows.Forms.Button bar;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button employees;
        private System.Windows.Forms.Button info;
    }
}