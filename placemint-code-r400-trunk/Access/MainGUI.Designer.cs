namespace PlaceMint.Access
{
    partial class MainGUI
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
                slotHook.Dispose();
                groupHook.Dispose();
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rippleItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.loadItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.configureItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.helpItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView = new System.Windows.Forms.TreeView();
            this.nodeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maximizeItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.taskBarContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tbPauseItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbRippleItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbRecentItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tbShowItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbExitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.toBottomItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.nodeContextMenu.SuspendLayout();
            this.taskBarContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.toolsMenu,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(592, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pauseItem,
            this.rippleItem,
            this.fileSeparator,
            this.loadItem,
            this.recentItem,
            this.exitItem});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(35, 20);
            this.fileMenu.Text = "&File";
            // 
            // pauseItem
            // 
            this.pauseItem.Name = "pauseItem";
            this.pauseItem.Size = new System.Drawing.Size(157, 22);
            this.pauseItem.Text = "&Pause";
            this.pauseItem.Click += new System.EventHandler(this.togglePaused);
            // 
            // rippleItem
            // 
            this.rippleItem.Name = "rippleItem";
            this.rippleItem.Size = new System.Drawing.Size(157, 22);
            this.rippleItem.Text = "Ripple Forward";
            this.rippleItem.Click += new System.EventHandler(this.rippleItem_Click);
            // 
            // fileSeparator
            // 
            this.fileSeparator.Name = "fileSeparator";
            this.fileSeparator.Size = new System.Drawing.Size(154, 6);
            // 
            // loadItem
            // 
            this.loadItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadItem.Name = "loadItem";
            this.loadItem.Size = new System.Drawing.Size(157, 22);
            this.loadItem.Text = "&Load";
            this.loadItem.Click += new System.EventHandler(this.loadItem_Click);
            // 
            // recentItem
            // 
            this.recentItem.Name = "recentItem";
            this.recentItem.Size = new System.Drawing.Size(157, 22);
            this.recentItem.Text = "Recent";
            // 
            // exitItem
            // 
            this.exitItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exitItem.Name = "exitItem";
            this.exitItem.Size = new System.Drawing.Size(157, 22);
            this.exitItem.Text = "E&xit";
            this.exitItem.Click += new System.EventHandler(this.exitItem_Click);
            // 
            // toolsMenu
            // 
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureItem,
            this.optionsItem});
            this.toolsMenu.Name = "toolsMenu";
            this.toolsMenu.Size = new System.Drawing.Size(44, 20);
            this.toolsMenu.Text = "&Tools";
            // 
            // configureItem
            // 
            this.configureItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.configureItem.Name = "configureItem";
            this.configureItem.Size = new System.Drawing.Size(132, 22);
            this.configureItem.Text = "&Configure";
            this.configureItem.Click += new System.EventHandler(this.configureItem_Click);
            // 
            // optionsItem
            // 
            this.optionsItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.optionsItem.Name = "optionsItem";
            this.optionsItem.Size = new System.Drawing.Size(132, 22);
            this.optionsItem.Text = "&Options";
            this.optionsItem.Click += new System.EventHandler(this.optionsItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpItem,
            this.aboutItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(40, 20);
            this.helpMenu.Text = "&Help";
            // 
            // helpItem
            // 
            this.helpItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.helpItem.Name = "helpItem";
            this.helpItem.Size = new System.Drawing.Size(114, 22);
            this.helpItem.Text = "Help";
            this.helpItem.Click += new System.EventHandler(this.helpItem_Click);
            // 
            // aboutItem
            // 
            this.aboutItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.aboutItem.Name = "aboutItem";
            this.aboutItem.Size = new System.Drawing.Size(114, 22);
            this.aboutItem.Text = "About";
            this.aboutItem.Click += new System.EventHandler(this.aboutItem_Click);
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 24);
            this.treeView.Margin = new System.Windows.Forms.Padding(0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(592, 242);
            this.treeView.TabIndex = 1;
            this.treeView.DoubleClick += new System.EventHandler(this.treeView_DoubleClick);
            this.treeView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.treeView_KeyPress);
            this.treeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseClick);
            // 
            // nodeContextMenu
            // 
            this.nodeContextMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.nodeContextMenu.ImageScalingSize = new System.Drawing.Size(0, 0);
            this.nodeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showItem,
            this.restoreItem,
            this.maximizeItem,
            this.minimizeItem,
            this.toBottomItem});
            this.nodeContextMenu.Name = "contextMenu";
            this.nodeContextMenu.ShowImageMargin = false;
            this.nodeContextMenu.Size = new System.Drawing.Size(128, 136);
            // 
            // showItem
            // 
            this.showItem.Name = "showItem";
            this.showItem.Size = new System.Drawing.Size(127, 22);
            this.showItem.Text = "Show";
            this.showItem.Click += new System.EventHandler(this.showItem_Click);
            // 
            // restoreItem
            // 
            this.restoreItem.Name = "restoreItem";
            this.restoreItem.Size = new System.Drawing.Size(127, 22);
            this.restoreItem.Text = "Restore";
            this.restoreItem.Click += new System.EventHandler(this.restoreItem_Click);
            // 
            // maximizeItem
            // 
            this.maximizeItem.Name = "maximizeItem";
            this.maximizeItem.Size = new System.Drawing.Size(127, 22);
            this.maximizeItem.Text = "Maximize";
            this.maximizeItem.Click += new System.EventHandler(this.maximizeItem_Click);
            // 
            // minimizeItem
            // 
            this.minimizeItem.Name = "minimizeItem";
            this.minimizeItem.Size = new System.Drawing.Size(127, 22);
            this.minimizeItem.Text = "Minimize";
            this.minimizeItem.Click += new System.EventHandler(this.minimizeItem_Click);
            // 
            // taskIcon
            // 
            this.taskIcon.ContextMenuStrip = this.taskBarContextMenu;
            this.taskIcon.Text = "PlaceMint";
            this.taskIcon.DoubleClick += new System.EventHandler(this.taskIcon_DoubleClick);
            // 
            // taskBarContextMenu
            // 
            this.taskBarContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbPauseItem,
            this.tbRippleItem,
            this.tbRecentItem,
            this.tbSeparator,
            this.tbShowItem,
            this.tbExitItem});
            this.taskBarContextMenu.Name = "taskBarContextMenu";
            this.taskBarContextMenu.ShowCheckMargin = true;
            this.taskBarContextMenu.ShowImageMargin = false;
            this.taskBarContextMenu.Size = new System.Drawing.Size(158, 120);
            // 
            // tbPauseItem
            // 
            this.tbPauseItem.Name = "tbPauseItem";
            this.tbPauseItem.Size = new System.Drawing.Size(157, 22);
            this.tbPauseItem.Text = "Pause";
            this.tbPauseItem.Click += new System.EventHandler(this.togglePaused);
            // 
            // tbRippleItem
            // 
            this.tbRippleItem.Name = "tbRippleItem";
            this.tbRippleItem.Size = new System.Drawing.Size(157, 22);
            this.tbRippleItem.Text = "Ripple Forward";
            this.tbRippleItem.Click += new System.EventHandler(this.rippleItem_Click);
            // 
            // tbRecentItem
            // 
            this.tbRecentItem.Name = "tbRecentItem";
            this.tbRecentItem.Size = new System.Drawing.Size(157, 22);
            this.tbRecentItem.Text = "Recent";
            // 
            // tbSeparator
            // 
            this.tbSeparator.Name = "tbSeparator";
            this.tbSeparator.Size = new System.Drawing.Size(154, 6);
            // 
            // tbShowItem
            // 
            this.tbShowItem.Name = "tbShowItem";
            this.tbShowItem.Size = new System.Drawing.Size(157, 22);
            this.tbShowItem.Text = "Show";
            this.tbShowItem.Click += new System.EventHandler(this.taskIcon_DoubleClick);
            // 
            // tbExitItem
            // 
            this.tbExitItem.CheckOnClick = true;
            this.tbExitItem.Name = "tbExitItem";
            this.tbExitItem.Size = new System.Drawing.Size(157, 22);
            this.tbExitItem.Text = "Exit";
            this.tbExitItem.Click += new System.EventHandler(this.exitItem_Click);
            // 
            // refreshTimer
            // 
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // toBottomItem
            // 
            this.toBottomItem.Name = "toBottomItem";
            this.toBottomItem.Size = new System.Drawing.Size(127, 22);
            this.toBottomItem.Text = "To Bottom";
            this.toBottomItem.Click += new System.EventHandler(this.toBottomItem_Click);
            // 
            // MainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 266);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "MainGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MainGUI";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainGUI_FormClosed);
            this.ResizeEnd += new System.EventHandler(this.MainGUI_ResizeEnd);
            this.Resize += new System.EventHandler(this.MainGUI_Resize);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.nodeContextMenu.ResumeLayout(false);
            this.taskBarContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem loadItem;
        private System.Windows.Forms.ToolStripMenuItem exitItem;
        private System.Windows.Forms.ToolStripMenuItem configureItem;
        private System.Windows.Forms.ToolStripMenuItem optionsItem;
        private System.Windows.Forms.ToolStripMenuItem helpItem;
        private System.Windows.Forms.ToolStripMenuItem aboutItem;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ContextMenuStrip nodeContextMenu;
        private System.Windows.Forms.ToolStripMenuItem showItem;
        private System.Windows.Forms.ToolStripMenuItem restoreItem;
        private System.Windows.Forms.ToolStripMenuItem minimizeItem;
        private System.Windows.Forms.NotifyIcon taskIcon;
        private System.Windows.Forms.ContextMenuStrip taskBarContextMenu;
        private System.Windows.Forms.ToolStripMenuItem tbShowItem;
        private System.Windows.Forms.ToolStripMenuItem tbExitItem;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.ToolStripMenuItem pauseItem;
        private System.Windows.Forms.ToolStripSeparator fileSeparator;
        private System.Windows.Forms.ToolStripMenuItem tbPauseItem;
        private System.Windows.Forms.ToolStripSeparator tbSeparator;
        private System.Windows.Forms.ToolStripMenuItem maximizeItem;
        private System.Windows.Forms.ToolStripMenuItem recentItem;
        private System.Windows.Forms.ToolStripMenuItem tbRecentItem;
        private System.Windows.Forms.ToolStripMenuItem tbRippleItem;
        private System.Windows.Forms.ToolStripMenuItem rippleItem;
        private System.Windows.Forms.ToolStripMenuItem toBottomItem;
    }
}