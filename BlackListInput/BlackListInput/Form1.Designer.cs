namespace BlackListInput
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.blackListButton = new System.Windows.Forms.Button();
            this.blandButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("HG丸ｺﾞｼｯｸM-PRO", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "ASINまたはブランド名を入力する";
            // 
            // inputTextBox
            // 
            this.inputTextBox.Font = new System.Drawing.Font("HG丸ｺﾞｼｯｸM-PRO", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.inputTextBox.Location = new System.Drawing.Point(12, 51);
            this.inputTextBox.Multiline = true;
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(254, 131);
            this.inputTextBox.TabIndex = 1;
            this.inputTextBox.TextChanged += new System.EventHandler(this.inputTextBox_TextChanged);
            // 
            // blackListButton
            // 
            this.blackListButton.Font = new System.Drawing.Font("HG丸ｺﾞｼｯｸM-PRO", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.blackListButton.Location = new System.Drawing.Point(12, 188);
            this.blackListButton.Name = "blackListButton";
            this.blackListButton.Size = new System.Drawing.Size(130, 58);
            this.blackListButton.TabIndex = 2;
            this.blackListButton.Text = "ブラックリスト登録";
            this.blackListButton.UseVisualStyleBackColor = true;
            this.blackListButton.Click += new System.EventHandler(this.blackListButton_Click);
            // 
            // blandButton
            // 
            this.blandButton.Font = new System.Drawing.Font("HG丸ｺﾞｼｯｸM-PRO", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.blandButton.Location = new System.Drawing.Point(145, 188);
            this.blandButton.Name = "blandButton";
            this.blandButton.Size = new System.Drawing.Size(121, 58);
            this.blandButton.TabIndex = 3;
            this.blandButton.Text = "規制ブランド登録";
            this.blandButton.UseVisualStyleBackColor = true;
            this.blandButton.Click += new System.EventHandler(this.blandButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(165, 252);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ver1.1(2018.01.25)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 276);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.blandButton);
            this.Controls.Add(this.blackListButton);
            this.Controls.Add(this.inputTextBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "ブラックリスト登録";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.Button blackListButton;
        private System.Windows.Forms.Button blandButton;
        private System.Windows.Forms.Label label2;
    }
}

