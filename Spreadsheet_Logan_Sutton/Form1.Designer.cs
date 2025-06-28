namespace Spreadsheet_Logan_Sutton
{
    partial class Form1
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColumnA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnA,
            this.ColumnB,
            this.ColumnC,
            this.ColumnD});
            this.dataGridView1.Location = new System.Drawing.Point(2, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(799, 451);
            this.dataGridView1.TabIndex = 0;
            // 
            // ColumnA
            // 
            this.ColumnA.HeaderText = "A";
            this.ColumnA.MinimumWidth = 6;
            this.ColumnA.Name = "ColumnA";
            this.ColumnA.Width = 187;
            // 
            // ColumnB
            // 
            this.ColumnB.HeaderText = "B";
            this.ColumnB.MinimumWidth = 6;
            this.ColumnB.Name = "ColumnB";
            this.ColumnB.Width = 186;
            // 
            // ColumnC
            // 
            this.ColumnC.HeaderText = "C";
            this.ColumnC.MinimumWidth = 6;
            this.ColumnC.Name = "ColumnC";
            this.ColumnC.Width = 187;
            // 
            // ColumnD
            // 
            this.ColumnD.HeaderText = "D";
            this.ColumnD.MinimumWidth = 6;
            this.ColumnD.Name = "ColumnD";
            this.ColumnD.Width = 186;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnC;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnD;
    }
}

