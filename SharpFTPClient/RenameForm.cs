using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpFTPClient
{
    public partial class RenameForm : Form
    {
        public RenameForm()
        {
            InitializeComponent();
        }

        public void SetTextField(string _text)
        {
            this.renameTextBox.Text = _text;
        }

        public string GetTextField()
        {
            return this.renameTextBox.Text;
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (renameTextBox.Text != "")
            {
                this.renameTextBox.Text = renameTextBox.Text;
            }
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
