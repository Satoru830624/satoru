namespace PrimaFileIntegrationApp
{
    partial class FileIntegrationForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.FilePathTextBox = new System.Windows.Forms.TextBox();
            this.FilePathSelectButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.RunFileIntegrationButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
            // 
            // FilePathTextBox
            // 
            this.FilePathTextBox.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FilePathTextBox.Location = new System.Drawing.Point(23, 38);
            this.FilePathTextBox.Name = "FilePathTextBox";
            this.FilePathTextBox.Size = new System.Drawing.Size(343, 25);
            this.FilePathTextBox.TabIndex = 0;
            this.FilePathTextBox.TextChanged += new System.EventHandler(this.FilePathTextBox_TextChanged);
            // 
            // FilePathSelectButton
            // 
            this.FilePathSelectButton.Location = new System.Drawing.Point(372, 38);
            this.FilePathSelectButton.Name = "FilePathSelectButton";
            this.FilePathSelectButton.Size = new System.Drawing.Size(83, 25);
            this.FilePathSelectButton.TabIndex = 1;
            this.FilePathSelectButton.Text = "参照";
            this.FilePathSelectButton.UseVisualStyleBackColor = true;
            this.FilePathSelectButton.Click += new System.EventHandler(this.FilePathSelectButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("HG丸ｺﾞｼｯｸM-PRO", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(20, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "統合/編集したいファイルのディレクトリを選択する。";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // RunFileIntegrationButton
            // 
            this.RunFileIntegrationButton.Font = new System.Drawing.Font("HG丸ｺﾞｼｯｸM-PRO", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RunFileIntegrationButton.Location = new System.Drawing.Point(83, 69);
            this.RunFileIntegrationButton.Name = "RunFileIntegrationButton";
            this.RunFileIntegrationButton.Size = new System.Drawing.Size(309, 31);
            this.RunFileIntegrationButton.TabIndex = 3;
            this.RunFileIntegrationButton.Text = "ファイルの統合/編集を実行";
            this.RunFileIntegrationButton.UseVisualStyleBackColor = true;
            this.RunFileIntegrationButton.Click += new System.EventHandler(this.RunFileIntegrationButton_Click);
            // 
            // FileIntegrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 111);
            this.Controls.Add(this.RunFileIntegrationButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FilePathSelectButton);
            this.Controls.Add(this.FilePathTextBox);
            this.Name = "FileIntegrationForm";
            this.Text = "PRIMAファイル統合/編集 Ver.1.5(2018.03)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox FilePathTextBox;
        private System.Windows.Forms.Button FilePathSelectButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RunFileIntegrationButton;
    }
}

