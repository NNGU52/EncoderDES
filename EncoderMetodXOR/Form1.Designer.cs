namespace EncoderMetodXOR
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Encode = new System.Windows.Forms.Button();
            this.richTextBox_Plain_Text = new System.Windows.Forms.RichTextBox();
            this.groupBox_text = new System.Windows.Forms.GroupBox();
            this.richTextBox_Cipher_Text = new System.Windows.Forms.RichTextBox();
            this.Decode = new System.Windows.Forms.Button();
            this.groupBox_key = new System.Windows.Forms.GroupBox();
            this.Generating_key = new System.Windows.Forms.Button();
            this.textBox_key = new System.Windows.Forms.TextBox();
            this.groupBox_file = new System.Windows.Forms.GroupBox();
            this.Close_In_Fail = new System.Windows.Forms.Button();
            this.Close_Of_Fail = new System.Windows.Forms.Button();
            this.Open_In_File = new System.Windows.Forms.Button();
            this.Open_Of_File = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox_text.SuspendLayout();
            this.groupBox_key.SuspendLayout();
            this.groupBox_file.SuspendLayout();
            this.SuspendLayout();
            // 
            // Encode
            // 
            this.Encode.Location = new System.Drawing.Point(258, 221);
            this.Encode.Name = "Encode";
            this.Encode.Size = new System.Drawing.Size(119, 38);
            this.Encode.TabIndex = 0;
            this.Encode.Text = "Зашифровать";
            this.Encode.UseVisualStyleBackColor = true;
            this.Encode.Click += new System.EventHandler(this.Encode_Click);
            // 
            // richTextBox_Plain_Text
            // 
            this.richTextBox_Plain_Text.Location = new System.Drawing.Point(6, 22);
            this.richTextBox_Plain_Text.Name = "richTextBox_Plain_Text";
            this.richTextBox_Plain_Text.Size = new System.Drawing.Size(738, 192);
            this.richTextBox_Plain_Text.TabIndex = 1;
            this.richTextBox_Plain_Text.Text = "";
            // 
            // groupBox_text
            // 
            this.groupBox_text.Controls.Add(this.richTextBox_Cipher_Text);
            this.groupBox_text.Controls.Add(this.Decode);
            this.groupBox_text.Controls.Add(this.richTextBox_Plain_Text);
            this.groupBox_text.Controls.Add(this.Encode);
            this.groupBox_text.Location = new System.Drawing.Point(12, 13);
            this.groupBox_text.Name = "groupBox_text";
            this.groupBox_text.Size = new System.Drawing.Size(750, 469);
            this.groupBox_text.TabIndex = 2;
            this.groupBox_text.TabStop = false;
            this.groupBox_text.Text = "Работа с текстом";
            // 
            // richTextBox_Cipher_Text
            // 
            this.richTextBox_Cipher_Text.Location = new System.Drawing.Point(6, 266);
            this.richTextBox_Cipher_Text.Name = "richTextBox_Cipher_Text";
            this.richTextBox_Cipher_Text.Size = new System.Drawing.Size(738, 197);
            this.richTextBox_Cipher_Text.TabIndex = 3;
            this.richTextBox_Cipher_Text.Text = "";
            // 
            // Decode
            // 
            this.Decode.Location = new System.Drawing.Point(383, 221);
            this.Decode.Name = "Decode";
            this.Decode.Size = new System.Drawing.Size(115, 38);
            this.Decode.TabIndex = 2;
            this.Decode.Text = "Расшифровать";
            this.Decode.UseVisualStyleBackColor = true;
            this.Decode.Click += new System.EventHandler(this.Decode_Click);
            // 
            // groupBox_key
            // 
            this.groupBox_key.Controls.Add(this.Generating_key);
            this.groupBox_key.Controls.Add(this.textBox_key);
            this.groupBox_key.Location = new System.Drawing.Point(768, 13);
            this.groupBox_key.Name = "groupBox_key";
            this.groupBox_key.Size = new System.Drawing.Size(262, 89);
            this.groupBox_key.TabIndex = 3;
            this.groupBox_key.TabStop = false;
            this.groupBox_key.Text = "Работа с ключом";
            // 
            // Generating_key
            // 
            this.Generating_key.Location = new System.Drawing.Point(6, 52);
            this.Generating_key.Name = "Generating_key";
            this.Generating_key.Size = new System.Drawing.Size(249, 29);
            this.Generating_key.TabIndex = 1;
            this.Generating_key.Text = "Сгенерировать ключ";
            this.Generating_key.UseVisualStyleBackColor = true;
            this.Generating_key.Click += new System.EventHandler(this.Generating_key_Click);
            // 
            // textBox_key
            // 
            this.textBox_key.Location = new System.Drawing.Point(6, 22);
            this.textBox_key.Name = "textBox_key";
            this.textBox_key.Size = new System.Drawing.Size(249, 25);
            this.textBox_key.TabIndex = 0;
            // 
            // groupBox_file
            // 
            this.groupBox_file.Controls.Add(this.Close_In_Fail);
            this.groupBox_file.Controls.Add(this.Close_Of_Fail);
            this.groupBox_file.Controls.Add(this.Open_In_File);
            this.groupBox_file.Controls.Add(this.Open_Of_File);
            this.groupBox_file.Location = new System.Drawing.Point(768, 108);
            this.groupBox_file.Name = "groupBox_file";
            this.groupBox_file.Size = new System.Drawing.Size(262, 172);
            this.groupBox_file.TabIndex = 4;
            this.groupBox_file.TabStop = false;
            this.groupBox_file.Text = "Работа с файлом";
            // 
            // Close_In_Fail
            // 
            this.Close_In_Fail.Location = new System.Drawing.Point(6, 133);
            this.Close_In_Fail.Name = "Close_In_Fail";
            this.Close_In_Fail.Size = new System.Drawing.Size(249, 31);
            this.Close_In_Fail.TabIndex = 5;
            this.Close_In_Fail.Text = "Выгрузка закрыт. текста из файла";
            this.Close_In_Fail.UseVisualStyleBackColor = true;
            this.Close_In_Fail.Click += new System.EventHandler(this.Close_In_Fail_Click);
            // 
            // Close_Of_Fail
            // 
            this.Close_Of_Fail.Location = new System.Drawing.Point(6, 96);
            this.Close_Of_Fail.Name = "Close_Of_Fail";
            this.Close_Of_Fail.Size = new System.Drawing.Size(249, 31);
            this.Close_Of_Fail.TabIndex = 2;
            this.Close_Of_Fail.Text = "Загрузка закрыт. текста в файл";
            this.Close_Of_Fail.UseVisualStyleBackColor = true;
            this.Close_Of_Fail.Click += new System.EventHandler(this.Close_Of_Fail_Click);
            // 
            // Open_In_File
            // 
            this.Open_In_File.Location = new System.Drawing.Point(6, 57);
            this.Open_In_File.Name = "Open_In_File";
            this.Open_In_File.Size = new System.Drawing.Size(249, 32);
            this.Open_In_File.TabIndex = 1;
            this.Open_In_File.Text = "Выгрузка открыт. текста из файла";
            this.Open_In_File.UseVisualStyleBackColor = true;
            this.Open_In_File.Click += new System.EventHandler(this.Open_In_File_Click);
            // 
            // Open_Of_File
            // 
            this.Open_Of_File.Location = new System.Drawing.Point(6, 22);
            this.Open_Of_File.Name = "Open_Of_File";
            this.Open_Of_File.Size = new System.Drawing.Size(249, 29);
            this.Open_Of_File.TabIndex = 0;
            this.Open_Of_File.Text = "Загрузка открыт. текста в файл";
            this.Open_Of_File.UseVisualStyleBackColor = true;
            this.Open_Of_File.Click += new System.EventHandler(this.Open_Of_File_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 490);
            this.Controls.Add(this.groupBox_file);
            this.Controls.Add(this.groupBox_key);
            this.Controls.Add(this.groupBox_text);
            this.Name = "Form1";
            this.Text = "EncoderDES";
            this.groupBox_text.ResumeLayout(false);
            this.groupBox_key.ResumeLayout(false);
            this.groupBox_key.PerformLayout();
            this.groupBox_file.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Encode;
        private System.Windows.Forms.RichTextBox richTextBox_Plain_Text;
        private System.Windows.Forms.GroupBox groupBox_text;
        private System.Windows.Forms.RichTextBox richTextBox_Cipher_Text;
        private System.Windows.Forms.Button Decode;
        private System.Windows.Forms.GroupBox groupBox_key;
        private System.Windows.Forms.Button Generating_key;
        private System.Windows.Forms.TextBox textBox_key;
        private System.Windows.Forms.GroupBox groupBox_file;
        private System.Windows.Forms.Button Open_In_File;
        private System.Windows.Forms.Button Open_Of_File;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button Close_In_Fail;
        private System.Windows.Forms.Button Close_Of_Fail;
    }
}

