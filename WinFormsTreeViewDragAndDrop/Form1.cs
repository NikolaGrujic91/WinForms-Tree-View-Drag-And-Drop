using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsTreeViewDragAndDrop
{
    public partial class Form1 : Form
    {
        #region Constructors

        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private void Form1_Load(object sender, EventArgs e)
        {
            TreeNode node = this.treeView1.Nodes.Add("Fruits");
            node.Nodes.Add("Apple");
            node.Nodes.Add("Peach");
            node.Expand();

            node = this.treeView2.Nodes.Add("Vegetables");
            node.Nodes.Add("Tomato");
            node.Nodes.Add("Eggplant");
            node.Expand();

            this.treeView1.AllowDrop = true;
            this.treeView2.AllowDrop = true;
        }

        private void treeView_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the tree.
            var tree = (TreeView)sender;

            // Get the node underneath the mouse.
            TreeNode node = tree.GetNodeAt(e.X, e.Y);
            tree.SelectedNode = node;

            // Start the drag-and-drop operation with a cloned copy of the node.
            if (node != null)
            {
                tree.DoDragDrop(node.Clone(), DragDropEffects.Copy);
            }
        }

        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            // Get the tree.
            var tree = (TreeView)sender;

            // Drag and drop denied by default.
            e.Effect = DragDropEffects.None;

            // Is it a valid format?
            if (e.Data.GetData(typeof(TreeNode)) == null)
            {
                return;
            }

            // Get the screen point.
            var pt = new Point(e.X, e.Y);

            // Convert to a point in the TreeView's coordinate system.
            pt = tree.PointToClient(pt);

            // Is the mouse over a valid node?
            TreeNode node = tree.GetNodeAt(pt);

            if (node == null)
            {
                return;
            }

            e.Effect = DragDropEffects.Copy;
            tree.SelectedNode = node;
        }

        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            // Get the tree.
            var tree = (TreeView)sender;

            // Get the screen point.
            var pt = new Point(e.X, e.Y);

            // Convert to a point in the TreeView's coordinate system.
            pt = tree.PointToClient(pt);

            // Get the node underneath the mouse.
            TreeNode node = tree.GetNodeAt(pt);

            // Add a child node.
            node.Nodes.Add((TreeNode)e.Data.GetData(typeof(TreeNode)));

            // Show the newly added node if it is not already visible.
            node.Expand();
        }

        #endregion
    }
}
