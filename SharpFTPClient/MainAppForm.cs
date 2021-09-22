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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace SharpFTPClient
{
    public partial class MainAppForm : Form
    {
        private FTPManager ftpManager;
        public MainAppForm()
        {
            ftpManager = new FTPManager();
            InitializeComponent();
        }

        private void MainAppForm_Load(object sender, EventArgs e)
        {
            remoteDirTreeView.ContextMenuStrip = remoteTreeContextMenuStrip;
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

                    // add files of rootdirectory
                    DirectoryInfo rootDir = new DirectoryInfo(e.Node.Tag.ToString());
                    foreach (var file in rootDir.GetFiles())
                    {
                        TreeNode n = new TreeNode(file.Name, 1, 1);
                        e.Node.Nodes.Add(n);
                    }


                    foreach (string dir in dirs)
                    {
                        DirectoryInfo di = new DirectoryInfo(dir);
                        TreeNode node = new TreeNode(di.Name, 2, 2);

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
                        Console.WriteLine("heyya");
                        this.remoteTreeContextMenuStrip.Show(new Point(mouseX, mouseY));
                        //Console.WriteLine(ftpManager.IsConnected.ToString());
                    }
                }
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

        private void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("NEW FOLDER");
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("NEW FILE");
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
                var originalText = targetNode.Tag.ToString();
            }
        }
    }
}

