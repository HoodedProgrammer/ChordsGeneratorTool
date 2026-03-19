namespace ChordsGenerator
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.chordsFoundList = new System.Windows.Forms.CheckedListBox();
            this.chordsGroupbox = new System.Windows.Forms.GroupBox();
            this.stringsToStrumGroupBox = new System.Windows.Forms.GroupBox();
            this.stringsToStrumList = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.widthLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.widthTextBox = new System.Windows.Forms.TextBox();
            this.heightTextBox = new System.Windows.Forms.TextBox();
            this.settingsBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.settingsTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.browseBtn = new System.Windows.Forms.Button();
            this.generateChordBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.whereToGenerateTextBox = new System.Windows.Forms.TextBox();
            this.targetBrowseBtn = new System.Windows.Forms.Button();
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.chordPreviewLabel = new System.Windows.Forms.Label();
            this.chordsGroupbox.SuspendLayout();
            this.stringsToStrumGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            this.SuspendLayout();
            // 
            // chordsFoundList
            // 
            this.chordsFoundList.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.chordsFoundList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chordsFoundList.CheckOnClick = true;
            this.chordsFoundList.FormattingEnabled = true;
            this.chordsFoundList.Location = new System.Drawing.Point(6, 19);
            this.chordsFoundList.Name = "chordsFoundList";
            this.chordsFoundList.Size = new System.Drawing.Size(228, 287);
            this.chordsFoundList.TabIndex = 0;
            this.chordsFoundList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chordsFoundList_ItemCheck);
            // 
            // chordsGroupbox
            // 
            this.chordsGroupbox.Controls.Add(this.chordsFoundList);
            this.chordsGroupbox.Location = new System.Drawing.Point(12, 75);
            this.chordsGroupbox.Name = "chordsGroupbox";
            this.chordsGroupbox.Size = new System.Drawing.Size(240, 321);
            this.chordsGroupbox.TabIndex = 1;
            this.chordsGroupbox.TabStop = false;
            this.chordsGroupbox.Text = "Chords found";
            // 
            // stringsToStrumGroupBox
            // 
            this.stringsToStrumGroupBox.Controls.Add(this.stringsToStrumList);
            this.stringsToStrumGroupBox.Location = new System.Drawing.Point(258, 75);
            this.stringsToStrumGroupBox.Name = "stringsToStrumGroupBox";
            this.stringsToStrumGroupBox.Size = new System.Drawing.Size(182, 321);
            this.stringsToStrumGroupBox.TabIndex = 2;
            this.stringsToStrumGroupBox.TabStop = false;
            this.stringsToStrumGroupBox.Text = "Strings to strum";
            // 
            // stringsToStrumList
            // 
            this.stringsToStrumList.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.stringsToStrumList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.stringsToStrumList.CheckOnClick = true;
            this.stringsToStrumList.FormattingEnabled = true;
            this.stringsToStrumList.Items.AddRange(new object[] {
            "E - 6th",
            "A - 5th",
            "D - 4th",
            "G - 3rd",
            "B - 2nd",
            "E - 1st"});
            this.stringsToStrumList.Location = new System.Drawing.Point(6, 20);
            this.stringsToStrumList.Name = "stringsToStrumList";
            this.stringsToStrumList.Size = new System.Drawing.Size(170, 287);
            this.stringsToStrumList.TabIndex = 1;
            this.stringsToStrumList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.stringsToStrumList_ItemCheck);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.widthLabel);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.heightLabel);
            this.groupBox1.Controls.Add(this.widthTextBox);
            this.groupBox1.Controls.Add(this.heightTextBox);
            this.groupBox1.Location = new System.Drawing.Point(457, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 89);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Size of the image to generate";
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(6, 60);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(38, 13);
            this.widthLabel.TabIndex = 7;
            this.widthLabel.Text = "Width:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(114, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "px";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(114, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "px";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(6, 34);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(41, 13);
            this.heightLabel.TabIndex = 4;
            this.heightLabel.Text = "Height:";
            // 
            // widthTextBox
            // 
            this.widthTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.widthTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.widthTextBox.Location = new System.Drawing.Point(50, 57);
            this.widthTextBox.Name = "widthTextBox";
            this.widthTextBox.Size = new System.Drawing.Size(63, 20);
            this.widthTextBox.TabIndex = 1;
            // 
            // heightTextBox
            // 
            this.heightTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.heightTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.heightTextBox.Location = new System.Drawing.Point(50, 31);
            this.heightTextBox.Name = "heightTextBox";
            this.heightTextBox.Size = new System.Drawing.Size(63, 20);
            this.heightTextBox.TabIndex = 0;
            // 
            // settingsTextBox
            // 
            this.settingsTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.settingsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.settingsTextBox.Enabled = false;
            this.settingsTextBox.Location = new System.Drawing.Point(122, 12);
            this.settingsTextBox.Name = "settingsTextBox";
            this.settingsTextBox.Size = new System.Drawing.Size(244, 20);
            this.settingsTextBox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Chords config\' folder:";
            // 
            // browseBtn
            // 
            this.browseBtn.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.browseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browseBtn.Location = new System.Drawing.Point(372, 12);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(68, 21);
            this.browseBtn.TabIndex = 6;
            this.browseBtn.Text = "Browse";
            this.browseBtn.UseVisualStyleBackColor = false;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // generateChordBtn
            // 
            this.generateChordBtn.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.generateChordBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.generateChordBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.generateChordBtn.Location = new System.Drawing.Point(457, 359);
            this.generateChordBtn.Name = "generateChordBtn";
            this.generateChordBtn.Size = new System.Drawing.Size(200, 36);
            this.generateChordBtn.TabIndex = 7;
            this.generateChordBtn.Text = "Generate chord image";
            this.generateChordBtn.UseVisualStyleBackColor = false;
            this.generateChordBtn.Click += new System.EventHandler(this.generateChordBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Where to generate:";
            // 
            // whereToGenerateTextBox
            // 
            this.whereToGenerateTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.whereToGenerateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.whereToGenerateTextBox.Enabled = false;
            this.whereToGenerateTextBox.Location = new System.Drawing.Point(121, 39);
            this.whereToGenerateTextBox.Name = "whereToGenerateTextBox";
            this.whereToGenerateTextBox.Size = new System.Drawing.Size(245, 20);
            this.whereToGenerateTextBox.TabIndex = 9;
            // 
            // targetBrowseBtn
            // 
            this.targetBrowseBtn.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.targetBrowseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.targetBrowseBtn.Location = new System.Drawing.Point(372, 36);
            this.targetBrowseBtn.Name = "targetBrowseBtn";
            this.targetBrowseBtn.Size = new System.Drawing.Size(68, 23);
            this.targetBrowseBtn.TabIndex = 10;
            this.targetBrowseBtn.Text = "Browse";
            this.targetBrowseBtn.UseVisualStyleBackColor = false;
            this.targetBrowseBtn.Click += new System.EventHandler(this.targetBrowseBtn_Click);
            // 
            // previewBox
            // 
            this.previewBox.BackColor = System.Drawing.Color.White;
            this.previewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewBox.Location = new System.Drawing.Point(481, 132);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(153, 213);
            this.previewBox.TabIndex = 11;
            this.previewBox.TabStop = false;
            // 
            // chordPreviewLabel
            // 
            this.chordPreviewLabel.AutoSize = true;
            this.chordPreviewLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chordPreviewLabel.Location = new System.Drawing.Point(514, 112);
            this.chordPreviewLabel.Name = "chordPreviewLabel";
            this.chordPreviewLabel.Size = new System.Drawing.Size(75, 13);
            this.chordPreviewLabel.TabIndex = 12;
            this.chordPreviewLabel.Text = "Chord preview";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(689, 407);
            this.Controls.Add(this.chordPreviewLabel);
            this.Controls.Add(this.previewBox);
            this.Controls.Add(this.targetBrowseBtn);
            this.Controls.Add(this.whereToGenerateTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.generateChordBtn);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.settingsTextBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.stringsToStrumGroupBox);
            this.Controls.Add(this.chordsGroupbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Image chord generator";
            this.chordsGroupbox.ResumeLayout(false);
            this.stringsToStrumGroupBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox chordsFoundList;
        private System.Windows.Forms.GroupBox chordsGroupbox;
        private System.Windows.Forms.GroupBox stringsToStrumGroupBox;
        private System.Windows.Forms.CheckedListBox stringsToStrumList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox heightTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.TextBox widthTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.FolderBrowserDialog settingsBrowserDialog;
        private System.Windows.Forms.TextBox settingsTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.Button generateChordBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox whereToGenerateTextBox;
        private System.Windows.Forms.Button targetBrowseBtn;
        private System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.Label chordPreviewLabel;
    }
}

