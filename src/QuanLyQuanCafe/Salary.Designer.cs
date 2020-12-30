namespace QuanLyQuanCafe
{
    partial class Salary
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnCloseSalary = new System.Windows.Forms.Button();
            this.dgvSalary = new System.Windows.Forms.DataGridView();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnViewSalary = new System.Windows.Forms.Button();
            this.dtpToDateSalary = new System.Windows.Forms.DateTimePicker();
            this.dtPFromDateSalary = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.CheckIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Checkout = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SumTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.txtSalary = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalary)).BeginInit();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(811, 497);
            this.panel1.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.txtSalary);
            this.panel8.Controls.Add(this.txtTime);
            this.panel8.Controls.Add(this.textBox1);
            this.panel8.Controls.Add(this.label4);
            this.panel8.Controls.Add(this.label3);
            this.panel8.Controls.Add(this.label2);
            this.panel8.Controls.Add(this.btnCloseSalary);
            this.panel8.Controls.Add(this.dgvSalary);
            this.panel8.Location = new System.Drawing.Point(0, 85);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(808, 409);
            this.panel8.TabIndex = 5;
            // 
            // btnCloseSalary
            // 
            this.btnCloseSalary.Location = new System.Drawing.Point(708, 362);
            this.btnCloseSalary.Name = "btnCloseSalary";
            this.btnCloseSalary.Size = new System.Drawing.Size(100, 32);
            this.btnCloseSalary.TabIndex = 4;
            this.btnCloseSalary.Text = "Trở về";
            this.btnCloseSalary.UseVisualStyleBackColor = true;
            this.btnCloseSalary.Click += new System.EventHandler(this.btnCloseSalary_Click);
            // 
            // dgvSalary
            // 
            this.dgvSalary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSalary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckIn,
            this.Checkout,
            this.SumTime,
            this.Money});
            this.dgvSalary.Location = new System.Drawing.Point(3, 20);
            this.dgvSalary.Name = "dgvSalary";
            this.dgvSalary.ReadOnly = true;
            this.dgvSalary.Size = new System.Drawing.Size(802, 330);
            this.dgvSalary.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnViewSalary);
            this.panel7.Controls.Add(this.dtpToDateSalary);
            this.panel7.Controls.Add(this.dtPFromDateSalary);
            this.panel7.Location = new System.Drawing.Point(3, 50);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(808, 35);
            this.panel7.TabIndex = 4;
            // 
            // btnViewSalary
            // 
            this.btnViewSalary.Location = new System.Drawing.Point(369, 4);
            this.btnViewSalary.Name = "btnViewSalary";
            this.btnViewSalary.Size = new System.Drawing.Size(75, 23);
            this.btnViewSalary.TabIndex = 2;
            this.btnViewSalary.Text = "Thống kê";
            this.btnViewSalary.UseVisualStyleBackColor = true;
            this.btnViewSalary.Click += new System.EventHandler(this.btnViewSalary_Click);
            // 
            // dtpToDateSalary
            // 
            this.dtpToDateSalary.Location = new System.Drawing.Point(571, 3);
            this.dtpToDateSalary.Name = "dtpToDateSalary";
            this.dtpToDateSalary.Size = new System.Drawing.Size(234, 26);
            this.dtpToDateSalary.TabIndex = 1;
            // 
            // dtPFromDateSalary
            // 
            this.dtPFromDateSalary.Location = new System.Drawing.Point(3, 3);
            this.dtPFromDateSalary.Name = "dtPFromDateSalary";
            this.dtPFromDateSalary.Size = new System.Drawing.Size(231, 26);
            this.dtPFromDateSalary.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(213, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(412, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "CHI TIẾT THỜI GIAN CHẤM CÔNG";
            // 
            // CheckIn
            // 
            this.CheckIn.DataPropertyName = "CheckIn";
            this.CheckIn.HeaderText = "Giờ vào";
            this.CheckIn.Name = "CheckIn";
            this.CheckIn.ReadOnly = true;
            // 
            // Checkout
            // 
            this.Checkout.DataPropertyName = "CheckOut";
            this.Checkout.HeaderText = "Giờ ra";
            this.Checkout.Name = "Checkout";
            this.Checkout.ReadOnly = true;
            // 
            // SumTime
            // 
            this.SumTime.DataPropertyName = "Hour";
            this.SumTime.HeaderText = "Số tiếng làm";
            this.SumTime.Name = "SumTime";
            this.SumTime.ReadOnly = true;
            // 
            // Money
            // 
            this.Money.DataPropertyName = "Money";
            this.Money.HeaderText = "Thành tiền";
            this.Money.Name = "Money";
            this.Money.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 369);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tổng thời gian: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(442, 376);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tổng lương";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 369);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 19);
            this.label4.TabIndex = 7;
            this.label4.Text = "Mức lương";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(109, 369);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 26);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "15 000 Đ";
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(322, 369);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(100, 26);
            this.txtTime.TabIndex = 9;
            // 
            // txtSalary
            // 
            this.txtSalary.Location = new System.Drawing.Point(526, 369);
            this.txtSalary.Name = "txtSalary";
            this.txtSalary.Size = new System.Drawing.Size(151, 26);
            this.txtSalary.TabIndex = 10;
            // 
            // Salary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 500);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Salary";
            this.Text = "Salary";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalary)).EndInit();
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnViewSalary;
        private System.Windows.Forms.DateTimePicker dtpToDateSalary;
        private System.Windows.Forms.DateTimePicker dtPFromDateSalary;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnCloseSalary;
        private System.Windows.Forms.DataGridView dgvSalary;
        private System.Windows.Forms.DataGridViewTextBoxColumn CheckIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Checkout;
        private System.Windows.Forms.DataGridViewTextBoxColumn SumTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Money;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSalary;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}