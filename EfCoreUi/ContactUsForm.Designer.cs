﻿namespace EfCoreUi
{
    partial class ContactUsForm
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
            this.orchestratorOptionsPage1 = new Microsoft.VisualStudio.TextTemplating.VSHost.OrchestratorOptionsPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(775, 597);
            this.webBrowser1.TabIndex = 0;
            // 
            // ContactUsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 597);
            this.Controls.Add(this.webBrowser1);
            this.Name = "ContactUsForm";
            this.Text = "Code Fuel";
            this.Load += new System.EventHandler(this.ContactUsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.VisualStudio.TextTemplating.VSHost.OrchestratorOptionsPage orchestratorOptionsPage1;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}