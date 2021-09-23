
namespace SharpFTPClient
{
    partial class MainAppForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainAppForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.userTextBox = new System.Windows.Forms.TextBox();
            this.Host = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Username = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.Label();
            this.Port = new System.Windows.Forms.Label();
            this.quickconnect_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.localDirGroupbox = new System.Windows.Forms.GroupBox();
            this.localDirTreeView = new System.Windows.Forms.TreeView();
            this.ImageListIcons = new System.Windows.Forms.ImageList(this.components);
            this.remoteDirGroupbox = new System.Windows.Forms.GroupBox();
            this.remoteDirTreeView = new System.Windows.Forms.TreeView();
            this.nodeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editPermissionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remoteTreeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.localDirGroupbox.SuspendLayout();
            this.remoteDirGroupbox.SuspendLayout();
            this.nodeContextMenuStrip.SuspendLayout();
            this.remoteTreeContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1326, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // hostTextBox
            // 
            this.hostTextBox.Location = new System.Drawing.Point(59, 19);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(100, 20);
            this.hostTextBox.TabIndex = 1;
            this.hostTextBox.Text = "ftpupload.net";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(466, 19);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(100, 20);
            this.passwordTextBox.TabIndex = 4;
            this.passwordTextBox.Text = "TLX47Qi7vOI";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(649, 19);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(59, 20);
            this.portTextBox.TabIndex = 5;
            // 
            // userTextBox
            // 
            this.userTextBox.Location = new System.Drawing.Point(271, 19);
            this.userTextBox.Name = "userTextBox";
            this.userTextBox.Size = new System.Drawing.Size(100, 20);
            this.userTextBox.TabIndex = 2;
            this.userTextBox.Text = "epiz_29772916";
            // 
            // Host
            // 
            this.Host.AutoSize = true;
            this.Host.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Host.Location = new System.Drawing.Point(17, 23);
            this.Host.Name = "Host";
            this.Host.Size = new System.Drawing.Size(36, 16);
            this.Host.TabIndex = 6;
            this.Host.Text = "Host:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(190, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 7;
            // 
            // Username
            // 
            this.Username.AutoSize = true;
            this.Username.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Username.Location = new System.Drawing.Point(202, 21);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(63, 16);
            this.Username.TabIndex = 8;
            this.Username.Text = "Username:";
            // 
            // Password
            // 
            this.Password.AutoSize = true;
            this.Password.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Password.Location = new System.Drawing.Point(399, 21);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(61, 16);
            this.Password.TabIndex = 9;
            this.Password.Text = "Password:";
            // 
            // Port
            // 
            this.Port.AutoSize = true;
            this.Port.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Port.Location = new System.Drawing.Point(610, 21);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(33, 16);
            this.Port.TabIndex = 10;
            this.Port.Text = "Port:";
            // 
            // quickconnect_button
            // 
            this.quickconnect_button.Location = new System.Drawing.Point(753, 17);
            this.quickconnect_button.Name = "quickconnect_button";
            this.quickconnect_button.Size = new System.Drawing.Size(144, 23);
            this.quickconnect_button.TabIndex = 11;
            this.quickconnect_button.Text = "Quick connect";
            this.quickconnect_button.UseVisualStyleBackColor = true;
            this.quickconnect_button.Click += new System.EventHandler(this.quickconnect_button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.hostTextBox);
            this.groupBox1.Controls.Add(this.quickconnect_button);
            this.groupBox1.Controls.Add(this.Host);
            this.groupBox1.Controls.Add(this.Port);
            this.groupBox1.Controls.Add(this.portTextBox);
            this.groupBox1.Controls.Add(this.Username);
            this.groupBox1.Controls.Add(this.Password);
            this.groupBox1.Controls.Add(this.userTextBox);
            this.groupBox1.Controls.Add(this.passwordTextBox);
            this.groupBox1.Location = new System.Drawing.Point(0, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1326, 64);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(0, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1393, 140);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // localDirGroupbox
            // 
            this.localDirGroupbox.Controls.Add(this.localDirTreeView);
            this.localDirGroupbox.Location = new System.Drawing.Point(0, 290);
            this.localDirGroupbox.Name = "localDirGroupbox";
            this.localDirGroupbox.Size = new System.Drawing.Size(643, 327);
            this.localDirGroupbox.TabIndex = 0;
            this.localDirGroupbox.TabStop = false;
            // 
            // localDirTreeView
            // 
            this.localDirTreeView.ImageIndex = 0;
            this.localDirTreeView.ImageList = this.ImageListIcons;
            this.localDirTreeView.Location = new System.Drawing.Point(0, 0);
            this.localDirTreeView.Name = "localDirTreeView";
            this.localDirTreeView.SelectedImageIndex = 0;
            this.localDirTreeView.Size = new System.Drawing.Size(643, 327);
            this.localDirTreeView.TabIndex = 0;
            this.localDirTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.localDirTreeView_BeforeExpand);
            this.localDirTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.localDirTreeView_AfterSelect);
            // 
            // ImageListIcons
            // 
            this.ImageListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageListIcons.ImageStream")));
            this.ImageListIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageListIcons.Images.SetKeyName(0, "computer.png");
            this.ImageListIcons.Images.SetKeyName(1, "file.png");
            this.ImageListIcons.Images.SetKeyName(2, "folder-closed.png");
            // 
            // remoteDirGroupbox
            // 
            this.remoteDirGroupbox.Controls.Add(this.remoteDirTreeView);
            this.remoteDirGroupbox.Location = new System.Drawing.Point(683, 290);
            this.remoteDirGroupbox.Name = "remoteDirGroupbox";
            this.remoteDirGroupbox.Size = new System.Drawing.Size(643, 327);
            this.remoteDirGroupbox.TabIndex = 1;
            this.remoteDirGroupbox.TabStop = false;
            // 
            // remoteDirTreeView
            // 
            this.remoteDirTreeView.ImageIndex = 0;
            this.remoteDirTreeView.ImageList = this.ImageListIcons;
            this.remoteDirTreeView.Location = new System.Drawing.Point(0, 0);
            this.remoteDirTreeView.Name = "remoteDirTreeView";
            this.remoteDirTreeView.SelectedImageIndex = 0;
            this.remoteDirTreeView.Size = new System.Drawing.Size(643, 327);
            this.remoteDirTreeView.TabIndex = 1;
            this.remoteDirTreeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.remoteDirTreeView_AfterLabelEdit);
            this.remoteDirTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.remoteDirTreeView_BeforeExpand);
            this.remoteDirTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.remoteDirTreeView_NodeMouseClick);
            this.remoteDirTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.remoteDirTreeView_MouseDown);
            // 
            // nodeContextMenuStrip
            // 
            this.nodeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.editPermissionToolStripMenuItem});
            this.nodeContextMenuStrip.Name = "nodeContextMenuStrip";
            this.nodeContextMenuStrip.Size = new System.Drawing.Size(181, 114);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.downloadToolStripMenuItem.Text = "Download";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // editPermissionToolStripMenuItem
            // 
            this.editPermissionToolStripMenuItem.Name = "editPermissionToolStripMenuItem";
            this.editPermissionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editPermissionToolStripMenuItem.Text = "Edit Permission";
            // 
            // remoteTreeContextMenuStrip
            // 
            this.remoteTreeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFolderToolStripMenuItem,
            this.newFileToolStripMenuItem});
            this.remoteTreeContextMenuStrip.Name = "contextMenuStrip1";
            this.remoteTreeContextMenuStrip.Size = new System.Drawing.Size(135, 48);
            // 
            // newFolderToolStripMenuItem
            // 
            this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
            this.newFolderToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.newFolderToolStripMenuItem.Text = "New Folder";
            this.newFolderToolStripMenuItem.Click += new System.EventHandler(this.newFolderToolStripMenuItem_Click);
            // 
            // newFileToolStripMenuItem
            // 
            this.newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
            this.newFileToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.newFileToolStripMenuItem.Text = "New File";
            this.newFileToolStripMenuItem.Click += new System.EventHandler(this.newFileToolStripMenuItem_Click);
            // 
            // MainAppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1326, 795);
            this.Controls.Add(this.remoteDirGroupbox);
            this.Controls.Add(this.localDirGroupbox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainAppForm";
            this.Text = "FTPClient";
            this.Load += new System.EventHandler(this.MainAppForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.localDirGroupbox.ResumeLayout(false);
            this.remoteDirGroupbox.ResumeLayout(false);
            this.nodeContextMenuStrip.ResumeLayout(false);
            this.remoteTreeContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.TextBox userTextBox;
        private System.Windows.Forms.Label Host;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Username;
        private System.Windows.Forms.Label Password;
        private System.Windows.Forms.Label Port;
        private System.Windows.Forms.Button quickconnect_button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox localDirGroupbox;
        private System.Windows.Forms.GroupBox remoteDirGroupbox;
        private System.Windows.Forms.TreeView localDirTreeView;
        private System.Windows.Forms.TreeView remoteDirTreeView;
        private System.Windows.Forms.ImageList ImageListIcons;
        private System.Windows.Forms.ContextMenuStrip nodeContextMenuStrip;
        private System.Windows.Forms.ContextMenuStrip remoteTreeContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editPermissionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFileToolStripMenuItem;
    }
}

