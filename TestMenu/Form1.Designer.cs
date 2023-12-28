using System.Threading.Tasks;

namespace TestMenu
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
        /// 



        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }





        public void ModeAddMigration()
        //private void InitializeComponent()
        {

            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxDbContextClass = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxStartupProject = new System.Windows.Forms.ComboBox();
            this.label_startup_project = new System.Windows.Forms.Label();
            this.comboBoxMigrationProject = new System.Windows.Forms.ComboBox();
            this.label_migrations_project = new System.Windows.Forms.Label();
            this.textBox_migration_name = new System.Windows.Forms.TextBox();
            this.migration_name = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxMigrationFolder = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBoxTargetFramework = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxBuildConfig = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxAdditionalArg = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.errorProvider_migration_name = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider_comboBoxMigrationProject = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider_comboBoxStartupProject = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider_comboBoxDbContextClass = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider_comboBoxMigrationFolder = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_migration_name)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxMigrationProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxStartupProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxDbContextClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxMigrationFolder)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.comboBoxDbContextClass);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxStartupProject);
            this.groupBox1.Controls.Add(this.label_startup_project);
            this.groupBox1.Controls.Add(this.comboBoxMigrationProject);
            this.groupBox1.Controls.Add(this.label_migrations_project);
            this.groupBox1.Controls.Add(this.textBox_migration_name);
            this.groupBox1.Controls.Add(this.migration_name);
            this.groupBox1.Location = new System.Drawing.Point(19, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(567, 205);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Common";
            // 
            // comboBoxDbContextClass
            // 
            this.comboBoxDbContextClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDbContextClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDbContextClass.FormattingEnabled = true;
            this.comboBoxDbContextClass.Location = new System.Drawing.Point(161, 158);
            this.comboBoxDbContextClass.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxDbContextClass.Name = "comboBoxDbContextClass";
            this.comboBoxDbContextClass.Size = new System.Drawing.Size(353, 24);
            this.comboBoxDbContextClass.TabIndex = 8;
            this.comboBoxDbContextClass.SelectedIndexChanged += new System.EventHandler(this.comboBoxDbContextClass_SelectedIndexChanged);
            this.comboBoxDbContextClass.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxDbContextClass_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 162);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "DbContext class:";
            // 
            // comboBoxStartupProject
            // 
            this.comboBoxStartupProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartupProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxStartupProject.FormattingEnabled = true;
            this.comboBoxStartupProject.Location = new System.Drawing.Point(161, 114);
            this.comboBoxStartupProject.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxStartupProject.Name = "comboBoxStartupProject";
            this.comboBoxStartupProject.Size = new System.Drawing.Size(353, 24);
            this.comboBoxStartupProject.TabIndex = 6;
            this.comboBoxStartupProject.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            this.comboBoxStartupProject.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxStartupProject_Validating);
            // 
            // label_startup_project
            // 
            this.label_startup_project.AutoSize = true;
            this.label_startup_project.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_startup_project.Location = new System.Drawing.Point(23, 118);
            this.label_startup_project.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_startup_project.Name = "label_startup_project";
            this.label_startup_project.Size = new System.Drawing.Size(105, 15);
            this.label_startup_project.TabIndex = 5;
            this.label_startup_project.Text = "Startup project:";
            // 
            // comboBoxMigrationProject
            // 
            this.comboBoxMigrationProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMigrationProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxMigrationProject.FormattingEnabled = true;
            this.comboBoxMigrationProject.Location = new System.Drawing.Point(161, 71);
            this.comboBoxMigrationProject.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxMigrationProject.Name = "comboBoxMigrationProject";
            this.comboBoxMigrationProject.Size = new System.Drawing.Size(353, 24);
            this.comboBoxMigrationProject.TabIndex = 4;
            this.comboBoxMigrationProject.SelectedIndexChanged += new System.EventHandler(this.comboBoxMigrationProject_SelectedIndexChanged);
            this.comboBoxMigrationProject.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxMigrationProject_Validating);
            // 
            // label_migrations_project
            // 
            this.label_migrations_project.AutoSize = true;
            this.label_migrations_project.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_migrations_project.Location = new System.Drawing.Point(19, 75);
            this.label_migrations_project.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_migrations_project.Name = "label_migrations_project";
            this.label_migrations_project.Size = new System.Drawing.Size(127, 15);
            this.label_migrations_project.TabIndex = 2;
            this.label_migrations_project.Text = "Migrations project:";
            // 
            // textBox_migration_name
            // 
            this.textBox_migration_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_migration_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_migration_name.Location = new System.Drawing.Point(161, 32);
            this.textBox_migration_name.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_migration_name.Name = "textBox_migration_name";
            this.textBox_migration_name.Size = new System.Drawing.Size(353, 22);
            this.textBox_migration_name.TabIndex = 1;
            this.textBox_migration_name.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_migration_name_Validating);
            // 
            // migration_name
            // 
            this.migration_name.AutoSize = true;
            this.migration_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.migration_name.Location = new System.Drawing.Point(23, 33);
            this.migration_name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.migration_name.Name = "migration_name";
            this.migration_name.Size = new System.Drawing.Size(112, 15);
            this.migration_name.TabIndex = 0;
            this.migration_name.Text = "Migration name:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxMigrationFolder);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(19, 223);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(567, 79);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Additional Options";
            // 
            // comboBoxMigrationFolder
            // 
            this.comboBoxMigrationFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMigrationFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxMigrationFolder.FormattingEnabled = true;
            this.comboBoxMigrationFolder.Location = new System.Drawing.Point(161, 37);
            this.comboBoxMigrationFolder.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxMigrationFolder.Name = "comboBoxMigrationFolder";
            this.comboBoxMigrationFolder.Size = new System.Drawing.Size(353, 24);
            this.comboBoxMigrationFolder.TabIndex = 9;
            this.comboBoxMigrationFolder.SelectedIndexChanged += new System.EventHandler(this.comboBox4_SelectedIndexChanged);
            this.comboBoxMigrationFolder.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxMigrationFolder_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(20, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Migration Folder:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(388, 610);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 29);
            this.button1.TabIndex = 10;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(495, 610);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 29);
            this.button2.TabIndex = 11;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.comboBoxTargetFramework);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.comboBoxBuildConfig);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(19, 309);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(567, 145);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Build Options";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(22, 29);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(248, 20);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "Skip project build process (--no-build)";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // comboBoxTargetFramework
            // 
            this.comboBoxTargetFramework.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTargetFramework.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxTargetFramework.FormattingEnabled = true;
            this.comboBoxTargetFramework.Location = new System.Drawing.Point(161, 104);
            this.comboBoxTargetFramework.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxTargetFramework.Name = "comboBoxTargetFramework";
            this.comboBoxTargetFramework.Size = new System.Drawing.Size(353, 24);
            this.comboBoxTargetFramework.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(18, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Target framework:";
            // 
            // comboBoxBuildConfig
            // 
            this.comboBoxBuildConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBuildConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxBuildConfig.FormattingEnabled = true;
            this.comboBoxBuildConfig.Location = new System.Drawing.Point(161, 62);
            this.comboBoxBuildConfig.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxBuildConfig.Name = "comboBoxBuildConfig";
            this.comboBoxBuildConfig.Size = new System.Drawing.Size(353, 24);
            this.comboBoxBuildConfig.TabIndex = 11;
            this.comboBoxBuildConfig.SelectedIndexChanged += new System.EventHandler(this.comboBox5_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(18, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Build configuration:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxAdditionalArg);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(19, 462);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(567, 107);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Execution";
            // 
            // textBoxAdditionalArg
            // 
            this.textBoxAdditionalArg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxAdditionalArg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAdditionalArg.Location = new System.Drawing.Point(177, 28);
            this.textBoxAdditionalArg.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxAdditionalArg.Multiline = true;
            this.textBoxAdditionalArg.Name = "textBoxAdditionalArg";
            this.textBoxAdditionalArg.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxAdditionalArg.Size = new System.Drawing.Size(353, 38);
            this.textBoxAdditionalArg.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(18, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Additional arguments:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(19, 610);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 29);
            this.button3.TabIndex = 14;
            this.button3.Text = "Preview";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // errorProvider_migration_name
            // 
            this.errorProvider_migration_name.ContainerControl = this;
            // 
            // errorProvider_comboBoxMigrationProject
            // 
            this.errorProvider_comboBoxMigrationProject.ContainerControl = this;
            // 
            // errorProvider_comboBoxStartupProject
            // 
            this.errorProvider_comboBoxStartupProject.ContainerControl = this;
            // 
            // errorProvider_comboBoxDbContextClass
            // 
            this.errorProvider_comboBoxDbContextClass.ContainerControl = this;
            // 
            // errorProvider_comboBoxMigrationFolder
            // 
            this.errorProvider_comboBoxMigrationFolder.ContainerControl = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 651);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_migration_name)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxMigrationProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxStartupProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxDbContextClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxMigrationFolder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void ModeRemoveMigration()
        //private void InitializeComponent()
        {

            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxDbContextClass = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxStartupProject = new System.Windows.Forms.ComboBox();
            this.label_startup_project = new System.Windows.Forms.Label();
            this.comboBoxMigrationProject = new System.Windows.Forms.ComboBox();
            this.label_migrations_project = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBoxTargetFramework = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxBuildConfig = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxAdditionalArg = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.errorProvider_comboBoxMigrationProject = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider_comboBoxStartupProject = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider_comboBoxDbContextClass = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxMigrationProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxStartupProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxDbContextClass)).BeginInit();
            this.SuspendLayout();
            //
            //groupBox1
            //
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.comboBoxDbContextClass);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxStartupProject);
            this.groupBox1.Controls.Add(this.label_startup_project);
            this.groupBox1.Controls.Add(this.comboBoxMigrationProject);
            this.groupBox1.Controls.Add(this.label_migrations_project);
            this.groupBox1.Location = new System.Drawing.Point(19, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(567, 164);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Common";
            //
            //comboBox3
            //
            this.comboBoxDbContextClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDbContextClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDbContextClass.FormattingEnabled = true;
            this.comboBoxDbContextClass.Location = new System.Drawing.Point(161, 117);
            this.comboBoxDbContextClass.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxDbContextClass.Name = "comboBoxDbContextClass";
            this.comboBoxDbContextClass.Size = new System.Drawing.Size(353, 24);
            this.comboBoxDbContextClass.TabIndex = 8;
            this.comboBoxDbContextClass.SelectedIndexChanged += new System.EventHandler(this.comboBoxDbContextClass_SelectedIndexChanged);
            this.comboBoxDbContextClass.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxDbContextClass_Validating);
            //
            //label3
            //
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 121);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "DbContext class:";
            //
            //comboBox2
            //
            this.comboBoxStartupProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartupProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxStartupProject.FormattingEnabled = true;
            this.comboBoxStartupProject.Location = new System.Drawing.Point(161, 73);
            this.comboBoxStartupProject.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxStartupProject.Name = "comboBoxStartupProject";
            this.comboBoxStartupProject.Size = new System.Drawing.Size(353, 24);
            this.comboBoxStartupProject.TabIndex = 6;
            this.comboBoxStartupProject.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            this.comboBoxStartupProject.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxStartupProject_Validating);

            //
            //label2
            //
            this.label_startup_project.AutoSize = true;
            this.label_startup_project.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_startup_project.Location = new System.Drawing.Point(23, 77);
            this.label_startup_project.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_startup_project.Name = "label_startup_project";
            this.label_startup_project.Size = new System.Drawing.Size(105, 15);
            this.label_startup_project.TabIndex = 5;
            this.label_startup_project.Text = "Startup project:";
            //
            //comboBox1
            //
            this.comboBoxMigrationProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMigrationProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxMigrationProject.FormattingEnabled = true;
            this.comboBoxMigrationProject.Location = new System.Drawing.Point(161, 30);
            this.comboBoxMigrationProject.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxMigrationProject.Name = "comboBoxMigrationProject";
            this.comboBoxMigrationProject.Size = new System.Drawing.Size(353, 24);
            this.comboBoxMigrationProject.TabIndex = 4;
            this.comboBoxMigrationProject.SelectedIndexChanged += new System.EventHandler(this.comboBoxMigrationProject_SelectedIndexChanged);
            this.comboBoxMigrationProject.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxMigrationProject_Validating);

            //
            //label1
            //
            this.label_migrations_project.AutoSize = true;
            this.label_migrations_project.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_migrations_project.Location = new System.Drawing.Point(19, 34);
            this.label_migrations_project.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_migrations_project.Name = "label_migrations_project";
            this.label_migrations_project.Size = new System.Drawing.Size(127, 15);
            this.label_migrations_project.TabIndex = 2;
            this.label_migrations_project.Text = "Migrations project:";
            //
            //button1
            //
            this.button1.Location = new System.Drawing.Point(388, 454);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 29);
            this.button1.TabIndex = 10;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            //
            //button2
            //
            this.button2.Location = new System.Drawing.Point(495, 454);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 29);
            this.button2.TabIndex = 11;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            //
            //groupBox3
            //
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.comboBoxTargetFramework);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.comboBoxBuildConfig);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(19, 179);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(567, 145);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Build Options";
            //
            //checkBox1
            //
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(22, 29);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(248, 20);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "Skip project build process (--no-build)";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            //
            //comboBox6
            //
            this.comboBoxTargetFramework.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTargetFramework.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxTargetFramework.FormattingEnabled = true;
            this.comboBoxTargetFramework.Location = new System.Drawing.Point(161, 104);
            this.comboBoxTargetFramework.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxTargetFramework.Name = "comboBoxTargetFramework";
            this.comboBoxTargetFramework.Size = new System.Drawing.Size(353, 24);
            this.comboBoxTargetFramework.TabIndex = 13;
            //
            //label6
            //
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(18, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Target framework:";
            //
            //comboBox5
            //
            this.comboBoxBuildConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBuildConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxBuildConfig.FormattingEnabled = true;
            this.comboBoxBuildConfig.Location = new System.Drawing.Point(161, 62);
            this.comboBoxBuildConfig.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxBuildConfig.Name = "comboBoxBuildConfig";
            this.comboBoxBuildConfig.Size = new System.Drawing.Size(353, 24);
            this.comboBoxBuildConfig.TabIndex = 11;
            this.comboBoxBuildConfig.SelectedIndexChanged += new System.EventHandler(this.comboBox5_SelectedIndexChanged);
            //
            //label5
            //
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(18, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Build configuration:";
            //
            //groupBox4
            //
            this.groupBox4.Controls.Add(this.textBoxAdditionalArg);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(19, 325);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(567, 107);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Execution";
            //
            //textBox2
            //
            this.textBoxAdditionalArg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxAdditionalArg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAdditionalArg.Location = new System.Drawing.Point(177, 28);
            this.textBoxAdditionalArg.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxAdditionalArg.Multiline = true;
            this.textBoxAdditionalArg.Name = "textBoxAdditionalArg";
            this.textBoxAdditionalArg.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxAdditionalArg.Size = new System.Drawing.Size(353, 38);
            this.textBoxAdditionalArg.TabIndex = 9;
            //
            //label7
            //
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(18, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Additional arguments:";
            //
            //button3
            //
            this.button3.Location = new System.Drawing.Point(19, 454);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 29);
            this.button3.TabIndex = 14;
            this.button3.Text = "Preview";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // errorProvider_comboBoxMigrationProject
            // 
            this.errorProvider_comboBoxMigrationProject.ContainerControl = this;
            // 
            // errorProvider_comboBoxStartupProject
            // 
            this.errorProvider_comboBoxStartupProject.ContainerControl = this;
            // 
            // errorProvider_comboBoxDbContextClass
            // 
            this.errorProvider_comboBoxDbContextClass.ContainerControl = this;
            //
            //Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 492);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxMigrationProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxStartupProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxDbContextClass)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void ModeGenerateSqlScript()
        //private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxToMigration = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxFromMigration = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxDbContextClass = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxStartupProject = new System.Windows.Forms.ComboBox();
            this.label_startup_project = new System.Windows.Forms.Label();
            this.comboBoxMigrationProject = new System.Windows.Forms.ComboBox();
            this.label_migrations_project = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxTransactions = new System.Windows.Forms.CheckBox();
            this.checkBoxIdempotent = new System.Windows.Forms.CheckBox();
            this.textBoxScript = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBoxTargetFramework = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxBuildConfig = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxAdditionalArg = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.errorProvider_comboBoxMigrationProject = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider_comboBoxStartupProject = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider_comboBoxDbContextClass = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider_comboBoxFromMigration = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider_comboBoxToMigration = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider_textBoxScript = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxMigrationProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxStartupProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxDbContextClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxFromMigration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxToMigration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_textBoxScript)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.comboBoxToMigration);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.comboBoxFromMigration);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.comboBoxDbContextClass);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxStartupProject);
            this.groupBox1.Controls.Add(this.label_startup_project);
            this.groupBox1.Controls.Add(this.comboBoxMigrationProject);
            this.groupBox1.Controls.Add(this.label_migrations_project);
            this.groupBox1.Location = new System.Drawing.Point(19, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(567, 248);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Common";
            // 
            // comboBoxToMigration
            // 
            this.comboBoxToMigration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxToMigration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxToMigration.FormattingEnabled = true;
            this.comboBoxToMigration.Location = new System.Drawing.Point(161, 77);
            this.comboBoxToMigration.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxToMigration.Name = "comboBoxToMigration";
            this.comboBoxToMigration.Size = new System.Drawing.Size(353, 24);
            this.comboBoxToMigration.TabIndex = 12;
            this.comboBoxToMigration.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxToMigration_Validating);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(19, 80);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 15);
            this.label8.TabIndex = 11;
            this.label8.Text = "To migration:";
            // 
            // comboBoxFromMigration
            // 
            this.comboBoxFromMigration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFromMigration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxFromMigration.FormattingEnabled = true;
            this.comboBoxFromMigration.Location = new System.Drawing.Point(161, 35);
            this.comboBoxFromMigration.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxFromMigration.Name = "comboBoxFromMigration";
            this.comboBoxFromMigration.Size = new System.Drawing.Size(353, 24);
            this.comboBoxFromMigration.TabIndex = 0;
            this.comboBoxFromMigration.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxFromMigration_Validating);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(19, 38);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 15);
            this.label9.TabIndex = 9;
            this.label9.Text = "From migration:";
            // 
            // comboBoxDbContextClass
            // 
            this.comboBoxDbContextClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDbContextClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDbContextClass.FormattingEnabled = true;
            this.comboBoxDbContextClass.Location = new System.Drawing.Point(161, 201);
            this.comboBoxDbContextClass.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxDbContextClass.Name = "comboBoxDbContextClass";
            this.comboBoxDbContextClass.Size = new System.Drawing.Size(353, 24);
            this.comboBoxDbContextClass.TabIndex = 8;
            this.comboBoxDbContextClass.SelectedIndexChanged += new System.EventHandler(this.comboBoxDbContextClass_SelectedIndexChanged);
            this.comboBoxDbContextClass.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxDbContextClass_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 205);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "DbContext class:";
            // 
            // comboBoxStartupProject
            // 
            this.comboBoxStartupProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartupProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxStartupProject.FormattingEnabled = true;
            this.comboBoxStartupProject.Location = new System.Drawing.Point(161, 157);
            this.comboBoxStartupProject.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxStartupProject.Name = "comboBoxStartupProject";
            this.comboBoxStartupProject.Size = new System.Drawing.Size(353, 24);
            this.comboBoxStartupProject.TabIndex = 6;
            this.comboBoxStartupProject.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            this.comboBoxStartupProject.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxStartupProject_Validating);
            // 
            // label_startup_project
            // 
            this.label_startup_project.AutoSize = true;
            this.label_startup_project.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_startup_project.Location = new System.Drawing.Point(23, 161);
            this.label_startup_project.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_startup_project.Name = "label_startup_project";
            this.label_startup_project.Size = new System.Drawing.Size(105, 15);
            this.label_startup_project.TabIndex = 5;
            this.label_startup_project.Text = "Startup project:";
            // 
            // comboBoxMigrationProject
            // 
            this.comboBoxMigrationProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMigrationProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxMigrationProject.FormattingEnabled = true;
            this.comboBoxMigrationProject.Location = new System.Drawing.Point(161, 114);
            this.comboBoxMigrationProject.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxMigrationProject.Name = "comboBoxMigrationProject";
            this.comboBoxMigrationProject.Size = new System.Drawing.Size(353, 24);
            this.comboBoxMigrationProject.TabIndex = 4;
            this.comboBoxMigrationProject.SelectedIndexChanged += new System.EventHandler(this.comboBoxMigrationProject_SelectedIndexChanged);
            this.comboBoxMigrationProject.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxMigrationProject_Validating);
            // 
            // label_migrations_project
            // 
            this.label_migrations_project.AutoSize = true;
            this.label_migrations_project.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_migrations_project.Location = new System.Drawing.Point(19, 118);
            this.label_migrations_project.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_migrations_project.Name = "label_migrations_project";
            this.label_migrations_project.Size = new System.Drawing.Size(127, 15);
            this.label_migrations_project.TabIndex = 2;
            this.label_migrations_project.Text = "Migrations project:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxTransactions);
            this.groupBox2.Controls.Add(this.checkBoxIdempotent);
            this.groupBox2.Controls.Add(this.textBoxScript);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(19, 268);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(567, 120);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Additional Options";
            // 
            // checkBoxTransactions
            // 
            this.checkBoxTransactions.AutoSize = true;
            this.checkBoxTransactions.Location = new System.Drawing.Point(22, 90);
            this.checkBoxTransactions.Name = "checkBoxTransactions";
            this.checkBoxTransactions.Size = new System.Drawing.Size(119, 20);
            this.checkBoxTransactions.TabIndex = 16;
            this.checkBoxTransactions.Text = "No transactions";
            this.checkBoxTransactions.UseVisualStyleBackColor = true;
            // 
            // checkBoxIdempotent
            // 
            this.checkBoxIdempotent.AutoSize = true;
            this.checkBoxIdempotent.Location = new System.Drawing.Point(22, 62);
            this.checkBoxIdempotent.Name = "checkBoxIdempotent";
            this.checkBoxIdempotent.Size = new System.Drawing.Size(165, 20);
            this.checkBoxIdempotent.TabIndex = 15;
            this.checkBoxIdempotent.Text = "Make script idempotent";
            this.checkBoxIdempotent.UseVisualStyleBackColor = true;
            // 
            // textBoxScript
            // 
            this.textBoxScript.Location = new System.Drawing.Point(161, 28);
            this.textBoxScript.Name = "textBoxScript";
            this.textBoxScript.Size = new System.Drawing.Size(353, 22);
            this.textBoxScript.TabIndex = 14;
            this.textBoxScript.Text = "script.sql";
            this.textBoxScript.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxScript_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "Output file:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(388, 675);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 29);
            this.button1.TabIndex = 10;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(495, 675);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 29);
            this.button2.TabIndex = 11;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.comboBoxTargetFramework);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.comboBoxBuildConfig);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(19, 391);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(567, 145);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Build Options";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(22, 29);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(248, 20);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "Skip project build process (--no-build)";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // comboBoxTargetFramework
            // 
            this.comboBoxTargetFramework.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTargetFramework.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxTargetFramework.FormattingEnabled = true;
            this.comboBoxTargetFramework.Location = new System.Drawing.Point(161, 104);
            this.comboBoxTargetFramework.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxTargetFramework.Name = "comboBoxTargetFramework";
            this.comboBoxTargetFramework.Size = new System.Drawing.Size(353, 24);
            this.comboBoxTargetFramework.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(18, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Target framework:";
            // 
            // comboBoxBuildConfig
            // 
            this.comboBoxBuildConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBuildConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxBuildConfig.FormattingEnabled = true;
            this.comboBoxBuildConfig.Location = new System.Drawing.Point(161, 62);
            this.comboBoxBuildConfig.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxBuildConfig.Name = "comboBoxBuildConfig";
            this.comboBoxBuildConfig.Size = new System.Drawing.Size(353, 24);
            this.comboBoxBuildConfig.TabIndex = 11;
            this.comboBoxBuildConfig.SelectedIndexChanged += new System.EventHandler(this.comboBox5_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(18, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Build configuration:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxAdditionalArg);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(19, 544);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(567, 107);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Execution";
            // 
            // textBoxAdditionalArg
            // 
            this.textBoxAdditionalArg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxAdditionalArg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAdditionalArg.Location = new System.Drawing.Point(177, 28);
            this.textBoxAdditionalArg.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxAdditionalArg.Multiline = true;
            this.textBoxAdditionalArg.Name = "textBoxAdditionalArg";
            this.textBoxAdditionalArg.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxAdditionalArg.Size = new System.Drawing.Size(353, 38);
            this.textBoxAdditionalArg.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(18, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Additional arguments:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(19, 675);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 29);
            this.button3.TabIndex = 14;
            this.button3.Text = "Preview";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // errorProvider_comboBoxMigrationProject
            // 
            this.errorProvider_comboBoxMigrationProject.ContainerControl = this;
            // 
            // errorProvider_comboBoxStartupProject
            // 
            this.errorProvider_comboBoxStartupProject.ContainerControl = this;
            // 
            // errorProvider_comboBoxDbContextClass
            // 
            this.errorProvider_comboBoxDbContextClass.ContainerControl = this;
            // 
            // errorProvider_comboBoxFromMigration
            // 
            this.errorProvider_comboBoxFromMigration.ContainerControl = this;
            // 
            // errorProvider_comboBoxToMigration
            // 
            this.errorProvider_comboBoxToMigration.ContainerControl = this;
            // 
            // errorProvider_textBoxScript
            // 
            this.errorProvider_textBoxScript.ContainerControl = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 710);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxMigrationProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxStartupProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxDbContextClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxFromMigration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxToMigration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_textBoxScript)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void ModeUpdateDatabase()
        //private void InitializeComponent()
        {

            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxToMigration = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxDbContextClass = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxStartupProject = new System.Windows.Forms.ComboBox();
            this.label_startup_project = new System.Windows.Forms.Label();
            this.comboBoxMigrationProject = new System.Windows.Forms.ComboBox();
            this.label_migrations_project = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxConnection = new System.Windows.Forms.ComboBox();
            this.checkBoxUseDefaultConnection = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBoxTargetFramework = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxBuildConfig = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxAdditionalArg = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.errorProvider_comboBoxMigrationProject = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider_comboBoxStartupProject = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider_comboBoxDbContextClass = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider_comboBoxToMigration = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider_comboBoxConnection = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxMigrationProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxStartupProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxDbContextClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxToMigration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxConnection)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.comboBoxToMigration);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.comboBoxDbContextClass);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxStartupProject);
            this.groupBox1.Controls.Add(this.label_startup_project);
            this.groupBox1.Controls.Add(this.comboBoxMigrationProject);
            this.groupBox1.Controls.Add(this.label_migrations_project);
            this.groupBox1.Location = new System.Drawing.Point(19, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(567, 195);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Common";
            // 
            // comboBoxToMigration
            // 
            this.comboBoxToMigration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxToMigration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxToMigration.FormattingEnabled = true;
            this.comboBoxToMigration.Location = new System.Drawing.Point(161, 24);
            this.comboBoxToMigration.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxToMigration.Name = "comboBoxToMigration";
            this.comboBoxToMigration.Size = new System.Drawing.Size(353, 24);
            this.comboBoxToMigration.TabIndex = 12;
            this.comboBoxToMigration.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxToMigration_Validating);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(19, 27);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 15);
            this.label8.TabIndex = 11;
            this.label8.Text = "Target migration:";
            // 
            // comboBoxDbContextClass
            // 
            this.comboBoxDbContextClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDbContextClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDbContextClass.FormattingEnabled = true;
            this.comboBoxDbContextClass.Location = new System.Drawing.Point(161, 148);
            this.comboBoxDbContextClass.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxDbContextClass.Name = "comboBoxDbContextClass";
            this.comboBoxDbContextClass.Size = new System.Drawing.Size(353, 24);
            this.comboBoxDbContextClass.TabIndex = 8;
            this.comboBoxDbContextClass.SelectedIndexChanged += new System.EventHandler(this.comboBoxDbContextClass_SelectedIndexChanged);
            this.comboBoxDbContextClass.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxDbContextClass_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 152);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "DbContext class:";
            // 
            // comboBoxStartupProject
            // 
            this.comboBoxStartupProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartupProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxStartupProject.FormattingEnabled = true;
            this.comboBoxStartupProject.Location = new System.Drawing.Point(161, 104);
            this.comboBoxStartupProject.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxStartupProject.Name = "comboBoxStartupProject";
            this.comboBoxStartupProject.Size = new System.Drawing.Size(353, 24);
            this.comboBoxStartupProject.TabIndex = 6;
            this.comboBoxStartupProject.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            this.comboBoxStartupProject.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxStartupProject_Validating);
            // 
            // label_startup_project
            // 
            this.label_startup_project.AutoSize = true;
            this.label_startup_project.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_startup_project.Location = new System.Drawing.Point(23, 108);
            this.label_startup_project.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_startup_project.Name = "label_startup_project";
            this.label_startup_project.Size = new System.Drawing.Size(105, 15);
            this.label_startup_project.TabIndex = 5;
            this.label_startup_project.Text = "Startup project:";
            // 
            // comboBoxMigrationProject
            // 
            this.comboBoxMigrationProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMigrationProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxMigrationProject.FormattingEnabled = true;
            this.comboBoxMigrationProject.Location = new System.Drawing.Point(161, 61);
            this.comboBoxMigrationProject.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxMigrationProject.Name = "comboBoxMigrationProject";
            this.comboBoxMigrationProject.Size = new System.Drawing.Size(353, 24);
            this.comboBoxMigrationProject.TabIndex = 4;
            this.comboBoxMigrationProject.SelectedIndexChanged += new System.EventHandler(this.comboBoxMigrationProject_SelectedIndexChanged);
            this.comboBoxMigrationProject.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxMigrationProject_Validating);
            // 
            // label_migrations_project
            // 
            this.label_migrations_project.AutoSize = true;
            this.label_migrations_project.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_migrations_project.Location = new System.Drawing.Point(19, 65);
            this.label_migrations_project.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_migrations_project.Name = "label_migrations_project";
            this.label_migrations_project.Size = new System.Drawing.Size(127, 15);
            this.label_migrations_project.TabIndex = 2;
            this.label_migrations_project.Text = "Migrations project:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxConnection);
            this.groupBox2.Controls.Add(this.checkBoxUseDefaultConnection);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(19, 209);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(567, 120);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Additional Options";
            // 
            // comboBoxConnection
            // 
            this.comboBoxConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxConnection.FormattingEnabled = true;
            this.comboBoxConnection.Location = new System.Drawing.Point(161, 64);
            this.comboBoxConnection.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxConnection.Name = "comboBoxConnection";
            this.comboBoxConnection.Size = new System.Drawing.Size(353, 24);
            this.comboBoxConnection.TabIndex = 13;
            this.comboBoxConnection.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxConnection_Validating);
            // 
            // checkBoxUseDefaultConnection
            // 
            this.checkBoxUseDefaultConnection.AutoSize = true;
            this.checkBoxUseDefaultConnection.Location = new System.Drawing.Point(22, 33);
            this.checkBoxUseDefaultConnection.Name = "checkBoxUseDefaultConnection";
            this.checkBoxUseDefaultConnection.Size = new System.Drawing.Size(266, 20);
            this.checkBoxUseDefaultConnection.TabIndex = 15;
            this.checkBoxUseDefaultConnection.Text = " Use default connection of startup project";
            this.checkBoxUseDefaultConnection.UseVisualStyleBackColor = true;
            this.checkBoxUseDefaultConnection.CheckedChanged += new System.EventHandler(this.checkBoxUseDefaultConnection_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 67);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "Connection:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(388, 614);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 29);
            this.button1.TabIndex = 10;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(495, 614);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 29);
            this.button2.TabIndex = 11;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.comboBoxTargetFramework);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.comboBoxBuildConfig);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(19, 332);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(567, 145);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Build Options";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(22, 29);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(248, 20);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "Skip project build process (--no-build)";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // comboBoxTargetFramework
            // 
            this.comboBoxTargetFramework.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTargetFramework.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxTargetFramework.FormattingEnabled = true;
            this.comboBoxTargetFramework.Location = new System.Drawing.Point(161, 104);
            this.comboBoxTargetFramework.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxTargetFramework.Name = "comboBoxTargetFramework";
            this.comboBoxTargetFramework.Size = new System.Drawing.Size(353, 24);
            this.comboBoxTargetFramework.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(18, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Target framework:";
            // 
            // comboBoxBuildConfig
            // 
            this.comboBoxBuildConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBuildConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxBuildConfig.FormattingEnabled = true;
            this.comboBoxBuildConfig.Location = new System.Drawing.Point(161, 62);
            this.comboBoxBuildConfig.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxBuildConfig.Name = "comboBoxBuildConfig";
            this.comboBoxBuildConfig.Size = new System.Drawing.Size(353, 24);
            this.comboBoxBuildConfig.TabIndex = 11;
            this.comboBoxBuildConfig.SelectedIndexChanged += new System.EventHandler(this.comboBox5_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(18, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Build configuration:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxAdditionalArg);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(19, 485);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(567, 107);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Execution";
            // 
            // textBoxAdditionalArg
            // 
            this.textBoxAdditionalArg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxAdditionalArg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAdditionalArg.Location = new System.Drawing.Point(177, 28);
            this.textBoxAdditionalArg.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxAdditionalArg.Multiline = true;
            this.textBoxAdditionalArg.Name = "textBoxAdditionalArg";
            this.textBoxAdditionalArg.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxAdditionalArg.Size = new System.Drawing.Size(353, 38);
            this.textBoxAdditionalArg.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(18, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Additional arguments:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(19, 614);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 29);
            this.button3.TabIndex = 14;
            this.button3.Text = "Preview";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // errorProvider_comboBoxMigrationProject
            // 
            this.errorProvider_comboBoxMigrationProject.ContainerControl = this;
            // 
            // errorProvider_comboBoxStartupProject
            // 
            this.errorProvider_comboBoxStartupProject.ContainerControl = this;
            // 
            // errorProvider_comboBoxDbContextClass
            // 
            this.errorProvider_comboBoxDbContextClass.ContainerControl = this;
            // 
            // errorProvider_comboBoxToMigration
            // 
            this.errorProvider_comboBoxToMigration.ContainerControl = this;
            // 
            // errorProvider_comboBoxConnection
            // 
            this.errorProvider_comboBoxConnection.ContainerControl = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 648);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxMigrationProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxStartupProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxDbContextClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxToMigration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxConnection)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void ModeDropDatabase()
        //private void InitializeComponent()
        {

            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxDbContextClass = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxStartupProject = new System.Windows.Forms.ComboBox();
            this.label_startup_project = new System.Windows.Forms.Label();
            this.comboBoxMigrationProject = new System.Windows.Forms.ComboBox();
            this.label_migrations_project = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBoxTargetFramework = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxBuildConfig = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxAdditionalArg = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.errorProvider_comboBoxMigrationProject = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider_comboBoxStartupProject = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider_comboBoxDbContextClass = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxMigrationProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxStartupProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxDbContextClass)).BeginInit();
            this.SuspendLayout();
            //
            //groupBox1
            //
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.comboBoxDbContextClass);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxStartupProject);
            this.groupBox1.Controls.Add(this.label_startup_project);
            this.groupBox1.Controls.Add(this.comboBoxMigrationProject);
            this.groupBox1.Controls.Add(this.label_migrations_project);
            this.groupBox1.Location = new System.Drawing.Point(19, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(567, 164);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Common";
            //
            //comboBox3
            //
            this.comboBoxDbContextClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDbContextClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDbContextClass.FormattingEnabled = true;
            this.comboBoxDbContextClass.Location = new System.Drawing.Point(161, 117);
            this.comboBoxDbContextClass.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxDbContextClass.Name = "comboBox3";
            this.comboBoxDbContextClass.Size = new System.Drawing.Size(353, 24);
            this.comboBoxDbContextClass.TabIndex = 8;
            this.comboBoxDbContextClass.SelectedIndexChanged += new System.EventHandler(this.comboBoxDbContextClass_SelectedIndexChanged);
            this.comboBoxDbContextClass.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxDbContextClass_Validating);
            //
            //label3
            //
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 121);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "DbContext class:";
            //
            //comboBox2
            //
            this.comboBoxStartupProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartupProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxStartupProject.FormattingEnabled = true;
            this.comboBoxStartupProject.Location = new System.Drawing.Point(161, 73);
            this.comboBoxStartupProject.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxStartupProject.Name = "comboBox2";
            this.comboBoxStartupProject.Size = new System.Drawing.Size(353, 24);
            this.comboBoxStartupProject.TabIndex = 6;
            this.comboBoxStartupProject.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            this.comboBoxStartupProject.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxStartupProject_Validating);

            //
            //label2
            //
            this.label_startup_project.AutoSize = true;
            this.label_startup_project.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_startup_project.Location = new System.Drawing.Point(23, 77);
            this.label_startup_project.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_startup_project.Name = "label_startup_project";
            this.label_startup_project.Size = new System.Drawing.Size(105, 15);
            this.label_startup_project.TabIndex = 5;
            this.label_startup_project.Text = "Startup project:";
            //
            //comboBox1
            //
            this.comboBoxMigrationProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMigrationProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxMigrationProject.FormattingEnabled = true;
            this.comboBoxMigrationProject.Location = new System.Drawing.Point(161, 30);
            this.comboBoxMigrationProject.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxMigrationProject.Name = "comboBox1";
            this.comboBoxMigrationProject.Size = new System.Drawing.Size(353, 24);
            this.comboBoxMigrationProject.TabIndex = 4;
            this.comboBoxMigrationProject.SelectedIndexChanged += new System.EventHandler(this.comboBoxMigrationProject_SelectedIndexChanged);
            this.comboBoxMigrationProject.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxMigrationProject_Validating);

            //
            //label1
            //
            this.label_migrations_project.AutoSize = true;
            this.label_migrations_project.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_migrations_project.Location = new System.Drawing.Point(19, 34);
            this.label_migrations_project.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_migrations_project.Name = "label1";
            this.label_migrations_project.Size = new System.Drawing.Size(127, 15);
            this.label_migrations_project.TabIndex = 2;
            this.label_migrations_project.Text = "Migrations project:";
            //
            //button1
            //
            this.button1.Location = new System.Drawing.Point(388, 454);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 29);
            this.button1.TabIndex = 10;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            //
            //button2
            //
            this.button2.Location = new System.Drawing.Point(495, 454);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 29);
            this.button2.TabIndex = 11;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            //
            //groupBox3
            //
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.comboBoxTargetFramework);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.comboBoxBuildConfig);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(19, 179);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(567, 145);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Build Options";
            //
            //checkBox1
            //
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(22, 29);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(248, 20);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "Skip project build process (--no-build)";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            //
            //comboBox6
            //
            this.comboBoxTargetFramework.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTargetFramework.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxTargetFramework.FormattingEnabled = true;
            this.comboBoxTargetFramework.Location = new System.Drawing.Point(161, 104);
            this.comboBoxTargetFramework.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxTargetFramework.Name = "comboBox6";
            this.comboBoxTargetFramework.Size = new System.Drawing.Size(353, 24);
            this.comboBoxTargetFramework.TabIndex = 13;
            //
            //label6
            //
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(18, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Target framework:";
            //
            //comboBox5
            //
            this.comboBoxBuildConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBuildConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxBuildConfig.FormattingEnabled = true;
            this.comboBoxBuildConfig.Location = new System.Drawing.Point(161, 62);
            this.comboBoxBuildConfig.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxBuildConfig.Name = "comboBox5";
            this.comboBoxBuildConfig.Size = new System.Drawing.Size(353, 24);
            this.comboBoxBuildConfig.TabIndex = 11;
            this.comboBoxBuildConfig.SelectedIndexChanged += new System.EventHandler(this.comboBox5_SelectedIndexChanged);
            //
            //label5
            //
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(18, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Build configuration:";
            //
            //groupBox4
            //
            this.groupBox4.Controls.Add(this.textBoxAdditionalArg);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(19, 325);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(567, 107);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Execution";
            //
            //textBox2
            //
            this.textBoxAdditionalArg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxAdditionalArg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAdditionalArg.Location = new System.Drawing.Point(177, 28);
            this.textBoxAdditionalArg.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxAdditionalArg.Multiline = true;
            this.textBoxAdditionalArg.Name = "textBox2";
            this.textBoxAdditionalArg.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxAdditionalArg.Size = new System.Drawing.Size(353, 38);
            this.textBoxAdditionalArg.TabIndex = 9;
            //
            //label7
            //
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(18, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Additional arguments:";
            //
            //button3
            //
            this.button3.Location = new System.Drawing.Point(19, 454);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 29);
            this.button3.TabIndex = 14;
            this.button3.Text = "Preview";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            //
            //Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 492);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxMigrationProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxStartupProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_comboBoxDbContextClass)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxMigrationProject;
        private System.Windows.Forms.Label label_migrations_project;
        private System.Windows.Forms.ComboBox comboBoxDbContextClass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxStartupProject;
        private System.Windows.Forms.Label label_startup_project;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxMigrationFolder;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBoxTargetFramework;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxBuildConfig;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBoxAdditionalArg;
        private System.Windows.Forms.TextBox textBox_migration_name;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBoxScript;
        private System.Windows.Forms.Label migration_name;
        private System.Windows.Forms.ComboBox comboBox7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxConnection;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBoxTransactions;
        private System.Windows.Forms.CheckBox checkBoxIdempotent;
        private System.Windows.Forms.CheckBox checkBoxUseDefaultConnection;

        private System.Windows.Forms.ComboBox comboBoxToMigration;

        private System.Windows.Forms.ComboBox comboBoxFromMigration;
        private System.Windows.Forms.ErrorProvider errorProvider_migration_name;
        private System.Windows.Forms.ErrorProvider errorProvider_comboBoxMigrationProject;
        private System.Windows.Forms.ErrorProvider errorProvider_comboBoxStartupProject;
        private System.Windows.Forms.ErrorProvider errorProvider_comboBoxDbContextClass;
        private System.Windows.Forms.ErrorProvider errorProvider_comboBoxMigrationFolder;
        private System.Windows.Forms.ErrorProvider errorProvider_comboBoxFromMigration;
        private System.Windows.Forms.ErrorProvider errorProvider_textBoxScript;
        private System.Windows.Forms.ErrorProvider errorProvider_comboBoxToMigration;
        private System.Windows.Forms.ErrorProvider errorProvider_comboBoxConnection;
    }
}