namespace HighNoonXmlMaker
{
    partial class form_Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_Make = new System.Windows.Forms.Button();
            this.button_Close = new System.Windows.Forms.Button();
            this.buttonAddRow = new System.Windows.Forms.Button();
            this.buttonDeleteRow = new System.Windows.Forms.Button();
            this.s = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.메뉴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.생성ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.불러오기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.닫기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Stage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Phase = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_Level = new System.Windows.Forms.ComboBox();
            this.comboBox_Scene = new System.Windows.Forms.ComboBox();
            this.buttonDeleteStep = new System.Windows.Forms.Button();
            this.buttonAddStep = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.s)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Make
            // 
            this.button_Make.Location = new System.Drawing.Point(802, 282);
            this.button_Make.Name = "button_Make";
            this.button_Make.Size = new System.Drawing.Size(99, 33);
            this.button_Make.TabIndex = 1;
            this.button_Make.Text = "XML 저장";
            this.button_Make.UseVisualStyleBackColor = true;
            this.button_Make.Click += new System.EventHandler(this.button_Make_Click);
            // 
            // button_Close
            // 
            this.button_Close.Location = new System.Drawing.Point(802, 331);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(99, 33);
            this.button_Close.TabIndex = 1;
            this.button_Close.Text = "닫기";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // buttonAddRow
            // 
            this.buttonAddRow.Location = new System.Drawing.Point(802, 186);
            this.buttonAddRow.Name = "buttonAddRow";
            this.buttonAddRow.Size = new System.Drawing.Size(99, 33);
            this.buttonAddRow.TabIndex = 4;
            this.buttonAddRow.Text = "행 추가";
            this.buttonAddRow.UseVisualStyleBackColor = true;
            this.buttonAddRow.Click += new System.EventHandler(this.buttonAddRow_Click);
            // 
            // buttonDeleteRow
            // 
            this.buttonDeleteRow.Location = new System.Drawing.Point(802, 234);
            this.buttonDeleteRow.Name = "buttonDeleteRow";
            this.buttonDeleteRow.Size = new System.Drawing.Size(99, 33);
            this.buttonDeleteRow.TabIndex = 4;
            this.buttonDeleteRow.Text = "행 삭제";
            this.buttonDeleteRow.UseVisualStyleBackColor = true;
            // 
            // s
            // 
            this.s.AllowUserToAddRows = false;
            this.s.AllowUserToOrderColumns = true;
            this.s.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.s.Location = new System.Drawing.Point(25, 71);
            this.s.Name = "s";
            this.s.RowHeadersVisible = false;
            this.s.RowTemplate.Height = 23;
            this.s.Size = new System.Drawing.Size(746, 397);
            this.s.TabIndex = 5;
            this.s.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.메뉴ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(933, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 메뉴ToolStripMenuItem
            // 
            this.메뉴ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.생성ToolStripMenuItem,
            this.불러오기ToolStripMenuItem,
            this.닫기ToolStripMenuItem});
            this.메뉴ToolStripMenuItem.Name = "메뉴ToolStripMenuItem";
            this.메뉴ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.메뉴ToolStripMenuItem.Text = "메뉴";
            // 
            // 생성ToolStripMenuItem
            // 
            this.생성ToolStripMenuItem.Name = "생성ToolStripMenuItem";
            this.생성ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.생성ToolStripMenuItem.Text = "생성";
            this.생성ToolStripMenuItem.Click += new System.EventHandler(this.생성ToolStripMenuItem_Click);
            // 
            // 불러오기ToolStripMenuItem
            // 
            this.불러오기ToolStripMenuItem.Name = "불러오기ToolStripMenuItem";
            this.불러오기ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.불러오기ToolStripMenuItem.Text = "불러오기";
            this.불러오기ToolStripMenuItem.Click += new System.EventHandler(this.불러오기ToolStripMenuItem_Click);
            // 
            // 닫기ToolStripMenuItem
            // 
            this.닫기ToolStripMenuItem.Name = "닫기ToolStripMenuItem";
            this.닫기ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.닫기ToolStripMenuItem.Text = "닫기";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "스테이지 ";
            // 
            // textBox_Stage
            // 
            this.textBox_Stage.Location = new System.Drawing.Point(86, 34);
            this.textBox_Stage.Name = "textBox_Stage";
            this.textBox_Stage.Size = new System.Drawing.Size(100, 21);
            this.textBox_Stage.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(226, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "페이즈";
            // 
            // textBox_Phase
            // 
            this.textBox_Phase.Location = new System.Drawing.Point(273, 34);
            this.textBox_Phase.Name = "textBox_Phase";
            this.textBox_Phase.Size = new System.Drawing.Size(100, 21);
            this.textBox_Phase.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(410, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "난이도";
            // 
            // comboBox_Level
            // 
            this.comboBox_Level.FormattingEnabled = true;
            this.comboBox_Level.Items.AddRange(new object[] {
            "EASY",
            "NORMAL",
            "HARD"});
            this.comboBox_Level.Location = new System.Drawing.Point(457, 35);
            this.comboBox_Level.Name = "comboBox_Level";
            this.comboBox_Level.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Level.TabIndex = 15;
            // 
            // comboBox_Scene
            // 
            this.comboBox_Scene.FormattingEnabled = true;
            this.comboBox_Scene.Location = new System.Drawing.Point(604, 34);
            this.comboBox_Scene.Name = "comboBox_Scene";
            this.comboBox_Scene.Size = new System.Drawing.Size(167, 20);
            this.comboBox_Scene.TabIndex = 16;
            this.comboBox_Scene.SelectedIndexChanged += new System.EventHandler(this.comboBox_Scene_SelectedIndexChanged);
            // 
            // buttonDeleteStep
            // 
            this.buttonDeleteStep.Location = new System.Drawing.Point(802, 137);
            this.buttonDeleteStep.Name = "buttonDeleteStep";
            this.buttonDeleteStep.Size = new System.Drawing.Size(99, 33);
            this.buttonDeleteStep.TabIndex = 4;
            this.buttonDeleteStep.Text = "Step 삭제";
            this.buttonDeleteStep.UseVisualStyleBackColor = true;
            this.buttonDeleteStep.Click += new System.EventHandler(this.buttonDeleteStep_Click);
            // 
            // buttonAddStep
            // 
            this.buttonAddStep.Location = new System.Drawing.Point(802, 87);
            this.buttonAddStep.Name = "buttonAddStep";
            this.buttonAddStep.Size = new System.Drawing.Size(99, 33);
            this.buttonAddStep.TabIndex = 4;
            this.buttonAddStep.Text = "Step 추가";
            this.buttonAddStep.UseVisualStyleBackColor = true;
            this.buttonAddStep.Click += new System.EventHandler(this.buttonAddStep_Click);
            // 
            // form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 561);
            this.Controls.Add(this.comboBox_Scene);
            this.Controls.Add(this.comboBox_Level);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_Phase);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_Stage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.s);
            this.Controls.Add(this.buttonDeleteRow);
            this.Controls.Add(this.buttonAddStep);
            this.Controls.Add(this.buttonDeleteStep);
            this.Controls.Add(this.buttonAddRow);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.button_Make);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "form_Main";
            this.Text = "HighNoonXmlMaker";
            ((System.ComponentModel.ISupportInitialize)(this.s)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_Make;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.Button buttonAddRow;
        private System.Windows.Forms.Button buttonDeleteRow;
        private System.Windows.Forms.DataGridView s;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 메뉴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 생성ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 불러오기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 닫기ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Stage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Phase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_Level;
        private System.Windows.Forms.ComboBox comboBox_Scene;
        private System.Windows.Forms.Button buttonDeleteStep;
        private System.Windows.Forms.Button buttonAddStep;
    }
}

