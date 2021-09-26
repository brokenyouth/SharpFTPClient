using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluentFTP;
using Microsoft.VisualBasic;
using Microsoft.WindowsAPICodePack.Dialogs;
using Timer = System.Windows.Forms.Timer;

namespace SharpFTPClient
{
    public partial class MainAppForm : Form
    {
        private Logger logger;
        private FTPManager ftpManager;
        public MainAppForm()
        {
            logger = new Logger(100u);
            ftpManager = new FTPManager(logger);
            InitializeComponent();
        }

        private void MainAppForm_Load(object sender, EventArgs e)
        {
            mainTimer.Interval = 100;
            mainTimer.Tick += new EventHandler(mainTimer_Tick);
            mainTimer.Start();
            CreateLocalDirectoryView();
        }

        private void localDirTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                if (e.Node.Nodes[0].Text == "..." && e.Node.Nodes[0].Tag == null)
                {
                    e.Node.Nodes.Clear();

                    //get the list of sub direcotires
                    string[] dirs = Directory.GetDirectories(e.Node.Tag.ToString());

                    // add files of root directory
                    DirectoryInfo rootDir = new DirectoryInfo(e.Node.Tag.ToString());
                    foreach (var file in rootDir.GetFiles())
                    {
                        TreeNode n = new TreeNode(file.Name, 1, 1);
                        n.Tag = file;
                        n.ContextMenuStrip = localNodeConextMenuStrip;
                        e.Node.Nodes.Add(n);
                    }


                    foreach (string dir in dirs)
                    {
                        DirectoryInfo di = new DirectoryInfo(dir);
                        TreeNode node = new TreeNode(di.Name, 2, 2);
                        node.Tag = dir;

                        try
                        {
                            //keep the directory's full path in the tag for use later
                            node.Tag = dir;

                            //if the directory has sub directories add the place holder
                            if (di.GetDirectories().Count() > 0)
                                node.Nodes.Add(null, "...", 0, 0);
                            foreach (var file in di.GetFiles())
                            {
                                TreeNode n = new TreeNode(file.Name, 1, 1);
                                n.Tag = file;
                                n.ContextMenuStrip = localNodeConextMenuStrip;
                                node.Nodes.Add(n);
                            }
                        }
                        catch (UnauthorizedAccessException)
                        {
                            //display a locked folder icon
                            //make and add correct image icon later
                            node.ImageIndex = 2;
                            node.SelectedImageIndex = 2;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "DirectoryLister",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            e.Node.Nodes.Add(node);
                        }
                    }
                }
            }
        }

        private void remoteDirTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                if (e.Node.Nodes[0].Text == "..." && e.Node.Nodes[0].Tag == null)
                {
                    // Clear nodes
                    e.Node.Nodes.Clear();

                    string nodeDirectoryPath = e.Node.FullPath;
                    CreateRemoteDirectoryView(e.Node, e.Node.Tag.ToString());
                    e.Node.Expand(); // ??
                }
            }
        }

        private void CreateLocalDirectoryView()
        {
            //get a list of the drives
            string[] drives = Environment.GetLogicalDrives();

            foreach (string drive in drives)
            {
                DriveInfo di = new DriveInfo(drive);

                TreeNode node = new TreeNode(drive, 2, 2);
                node.Tag = drive;
                if (di.IsReady == true)
                    node.Nodes.Add("...");

                localDirTreeView.Nodes.Add(node);
                node.Expand();
            }
        }

        private async void CreateRemoteDirectoryView(TreeNode parentNode, string workingDir)
        {
            // we know that the parent node is a directory
            parentNode.ImageIndex = 2;
            var dirNode = new TreeNode(workingDir);
            var dirListing = await ftpManager.GetDirectoryListing(workingDir);

            var directorties = dirListing.Where(d => d.IsDirectory);
            var files = dirListing.Where(d => !d.IsDirectory);

            foreach (var dir in directorties)
            {
                var node = new TreeNode(dir.Name, 2, 2);
                node.Name = dir.Name;
                node.Tag = dir.FullPath;
                node.ContextMenuStrip = nodeContextMenuStrip;
                if (await ftpManager.GetSubDirectoriesCount(dir.FullPath) > 0)
                {
                    node.Nodes.Add(new TreeNode("...", 2, 2));
                }
                parentNode.Nodes.Add(node);
            }

            foreach (var file in files)
            {
                var node = new TreeNode(file.Name, 1, 1);
                node.Name = file.Name;
                node.Tag = file.FullPath;
                node.ContextMenuStrip = nodeContextMenuStrip;
                parentNode.Nodes.Add(node);
            }
            parentNode.Expand();
        }

        private void Host_Click(object sender, EventArgs e)
        {

        }


        private void localDirTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void remoteDirTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            // We will only deal with clicks outside of a node
            // remoteDirTreeView_NodeMouseClick will deal with node clicks

            // create a temporary variable for the if check
            TreeNode nodeClicked = null;
            int mouseX = e.X;
            int mouseY = e.Y;
            if (e.Button.Equals(MouseButtons.Right))
                nodeClicked = this.remoteDirTreeView.GetNodeAt(mouseX, mouseY);

            if (nodeClicked != null) // we clicked on a node
            {
                // do nothing
            }
            else // no node has been clicked
            {
                // if we get here, this method's real work starts
                if (e.Button.Equals(MouseButtons.Right))
                {

                    if (ftpManager.IsConnected)
                    {
                        this.nodeContextMenuStrip.Show(new Point(mouseX, mouseY));
                    }
                }
            }

        }

        private void localDirTreeView_MouseDown(object sender, MouseEventArgs e)
        {

            // create a temporary variable for the if check
            TreeNode nodeClicked = null;
            int mouseX = e.X;
            int mouseY = e.Y;
            if (e.Button.Equals(MouseButtons.Right))
                nodeClicked = this.localDirTreeView.GetNodeAt(mouseX, mouseY);

            if (nodeClicked != null) // we clicked on a node
            {
                if (e.Button.Equals(MouseButtons.Right))
                {
                    this.localNodeConextMenuStrip.Show(new Point(mouseX, mouseY));
                }
            }
            else // no node has been clicked
            {
                
            }

        }

        private async void quickconnect_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (ftpManager.IsConnected)
                    return;
                Task t = ftpManager.Connect(hostTextBox.Text, userTextBox.Text, passwordTextBox.Text);
                await t;
                if (remoteDirTreeView.Nodes.Count > 0)
                    remoteDirTreeView.Nodes.Clear();
                remoteDirTreeView.Nodes.Add(new TreeNode(hostTextBox.Text + "/" + userTextBox.Text, 0, 0));
                CreateRemoteDirectoryView(remoteDirTreeView.Nodes[0], ftpManager.ftpClient.GetWorkingDirectory());
            }
            catch (FluentFTP.FtpAuthenticationException ex)
            {
                Console.WriteLine(ex.ToString());
            }



        }

        void localDirTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                if (e.Button == MouseButtons.Right)
                {
                    this.localDirTreeView.SelectedNode = e.Node;
                }
            }

        }

        void remoteDirTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                if (e.Button == MouseButtons.Right)
                {
                    this.remoteDirTreeView.SelectedNode = e.Node;
                }
            }

        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.remoteDirTreeView.SelectedNode != null)
            {
                TreeNode targetNode = this.remoteDirTreeView.SelectedNode;
                // prompt a "Choose directory" dialog
                CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                dialog.InitialDirectory = @"C:\";
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    if (targetNode.Nodes.Count > 0) // this is a directory
                    {
                        ftpManager.DownloadDirectory(targetNode.Tag.ToString(), dialog.FileName + @"\" + targetNode.Name);
                    }
                    else // this is a file
                    {
                        ftpManager.DownloadFile(targetNode.Tag.ToString(), dialog.FileName + @"\" + targetNode.Name);
                    }
                }
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.remoteDirTreeView.SelectedNode != null)
            {
                TreeNode targetNode = this.remoteDirTreeView.SelectedNode;
                var originalText = targetNode.Name;
                //edit the label = true 
                remoteDirTreeView.LabelEdit = true;
                if (!remoteDirTreeView.SelectedNode.IsEditing)
                {
                    remoteDirTreeView.SelectedNode.BeginEdit();
                }

            }
        }

        private void remoteDirTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (e.Label.Length > 0)
                {
                    if (e.Label.IndexOfAny(new char[] { '@', ',', '!', '?', '=', '$', '%' }) == -1)
                    {
                        var originalPath = e.Node.Tag.ToString();
                        // Stop editing without canceling the label change.
                        e.Node.EndEdit(false);
                        var newPath = e.Node.Tag.ToString().Substring(0, originalPath.Length - e.Node.Name.Length) + e.Label; // there's probably a better way of doing this
                        e.Node.Name = e.Label;
                        e.Node.Tag = newPath;
                        // Now call rename on this directory or file.
                        ftpManager.RenameFileOrDirectory(originalPath, newPath);
                    }
                    else
                    {
                        // stop editing and inform user
                        e.CancelEdit = true;
                        MessageBox.Show("Invalid character.\n" +
                           "The invalid characters are: '@', ',', '!', '?', '=', '$', '%'",
                           "Rename");
                        e.Node.BeginEdit();
                    }
                }
                else
                {
                    // the user tried to rename the node to an empty string
                    e.CancelEdit = true;
                    MessageBox.Show("Renaming failed. Please type something.",
                      "Rename");
                    e.Node.BeginEdit();
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.remoteDirTreeView.SelectedNode != null)
            {
                TreeNode targetNode = this.remoteDirTreeView.SelectedNode;
                bool isDirectory = targetNode.Nodes.Count > 0 ? true : false;
                ftpManager.Delete(targetNode.Tag.ToString(), isDirectory);
                this.remoteDirTreeView.SelectedNode.Remove();
            }
        }

        private void newFolderToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (this.remoteDirTreeView.SelectedNode != null)
            {
                TreeNode selectedNode = this.remoteDirTreeView.SelectedNode;
                string content = Interaction.InputBox("Enter folder name : ", "New Folder", "New Folder", 500, 350);

                if (content == "")
                {
                    MessageBox.Show("Cannot create a new folder. The specified name is empty.");
                    return;
                }
                else
                {
                    TreeNode newFolderNode = new TreeNode(content, 2, 2);
                    newFolderNode.Tag = selectedNode.Tag + "/" + newFolderNode.Text;
                    selectedNode.Nodes.Add(newFolderNode);
                    Console.WriteLine(newFolderNode.Tag);
                    ftpManager.NewFolder(newFolderNode.Tag.ToString());
                }
            }
        }

        private void newFileToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (this.remoteDirTreeView.SelectedNode != null)
            {
                TreeNode selectedNode = this.remoteDirTreeView.SelectedNode;
                string content = Interaction.InputBox("Enter file name : ", "New File", "New_File", 500, 350);

                if (content == "")
                {
                    MessageBox.Show("Cannot create a new file. The specified name is empty.");
                    return;
                }
                else
                {
                    TreeNode newFileNode = new TreeNode(content, 1, 1);
                    newFileNode.Tag = selectedNode.Tag + "/" + newFileNode.Text;
                    selectedNode.Nodes.Add(newFileNode);
                    ftpManager.NewFile(newFileNode.Tag.ToString(), newFileNode.Text);
                }
            }
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            richTextBox1.Rtf = logger.GetLogAsRichText(true);
        }

        private async void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.localDirTreeView.SelectedNode != null)
            {
                if (ftpManager.IsConnected)
                {
                    TreeNode selectedNode = this.localDirTreeView.SelectedNode;
                    string content = Interaction.InputBox("Enter destination : ", "Upload destination", ftpManager.ftpClient.GetWorkingDirectory(), 500, 350);

                    if (content == "")
                    {
                        return;
                    }

                    if (selectedNode.Nodes.Count > 0)
                    {
                        await ftpManager.UploadDirectory(selectedNode.FullPath, content + "/" + selectedNode.Text);
                    }
                    else
                    {
                        await ftpManager.UploadFile(selectedNode.FullPath, content + "/" + selectedNode.Text);
                    }
                }
                
            }
        }
    }
}

