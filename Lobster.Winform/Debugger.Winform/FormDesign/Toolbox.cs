using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace Debugger.Winform.FormDesign
{
	/// <summary>
	/// Toolbox - it implements the IToolboxService to enable
    /// dragging toolbox items onto the host
	/// </summary>

	public class Toolbox : System.Windows.Forms.UserControl, 
		IToolboxService
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		//private ToolboxTabCollection m_ToolboxTabCollection = null;
		//private string m_filePath = null;
		private Button[] m_tabPageArray = null;
		private int selectedIndex = 0;
		private System.Windows.Forms.Button toolboxTitleButton;
		private IDesignerHost m_designerHost = null;
		private ListBox m_toolsListBox = null;
        private System.Drawing.Design.ToolboxItem pointer; // a "null" tool
        private Dictionary<string, Type[]> controlDic;

        public Toolbox()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
            pointer = new System.Drawing.Design.ToolboxItem();
            pointer.DisplayName = "<Pointer>";
            pointer.Bitmap = new System.Drawing.Bitmap(16, 16);

            InitializeToolbox();
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.toolboxTitleButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // toolboxTitleButton
            // 
            this.toolboxTitleButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolboxTitleButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolboxTitleButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.toolboxTitleButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolboxTitleButton.Location = new System.Drawing.Point(0, 0);
            this.toolboxTitleButton.Name = "toolboxTitleButton";
            this.toolboxTitleButton.Size = new System.Drawing.Size(127, 20);
            this.toolboxTitleButton.TabIndex = 1;
            this.toolboxTitleButton.Text = "工具箱";
            this.toolboxTitleButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolboxTitleButton.UseVisualStyleBackColor = false;
            // 
            // Toolbox
            // 
            this.Controls.Add(this.toolboxTitleButton);
            this.Name = "Toolbox";
            this.Size = new System.Drawing.Size(127, 283);
            this.ResumeLayout(false);

		}
		#endregion

		public void InitializeToolbox()
		{
            //ToolboxXmlManager toolboxXmlManager = new ToolboxXmlManager(this);
            //Tabs = toolboxXmlManager.PopulateToolboxInfo();

            //ToolboxUIManagerVS toolboxUIManagerVS = new ToolboxUIManagerVS(this);
            //toolboxUIManagerVS.FillToolbox();

            controlDic = new Dictionary<string, Type[]>();

            

            controlDic.Add("WindowsForm控件", ControlTypeData.windowsFormsToolTypes);
            controlDic.Add("DotNetBar控件", ControlTypeData.dotNetBarToolTypes);
           
            

            this.Controls.Clear();
            this.ToolsListBox = new ListBox();
            this.TabPageArray = new Button[controlDic.Count];

            #region 生成控件项
            this.SuspendLayout();
            int index = controlDic.Count;
            List<string> nlist = new List<string>(controlDic.Keys);
            nlist.Reverse();
            foreach (var item in nlist)
            {

                index = index - 1;
                // 
                // Tab Button
                // 
                Button button = new Button();

                button.Dock = System.Windows.Forms.DockStyle.Top;
                button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                //button.Location = new System.Drawing.Point(0, (index + 1) * 20);
                button.Name = item;
                button.Size = new System.Drawing.Size(this.Width, 20);
                //button.TabIndex = index + 1;
                button.Text = item;
                button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                //button.Tag =index;
                button.Click += new EventHandler(button_Click);
                this.Controls.Add(button);
                this.TabPageArray[index] = button;
            }

            // 
            // toolboxTitleButton
            // 
            Button toolboxTitleButton = new Button();

            toolboxTitleButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            toolboxTitleButton.Dock = System.Windows.Forms.DockStyle.Top;
            toolboxTitleButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            toolboxTitleButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            toolboxTitleButton.Location = new System.Drawing.Point(0, 0);
            toolboxTitleButton.Name = "toolboxTitleButton";
            toolboxTitleButton.Size = new System.Drawing.Size(this.Width, 20);
            toolboxTitleButton.TabIndex = 0;
            toolboxTitleButton.Text = "工具箱";
            toolboxTitleButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Controls.Add(toolboxTitleButton);

            // 
            // listBox
            // 
            ListBox listBox = new ListBox();

            listBox.BackColor = System.Drawing.SystemColors.ControlLight;
            listBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            listBox.ItemHeight = 18;
            listBox.Location = new System.Drawing.Point(0, (controlDic.Count + 1) * 20);
            listBox.Name = "ToolsListBox";
            listBox.Size = new System.Drawing.Size(this.Width, this.Height - (controlDic.Count + 1) * 20);
            listBox.TabIndex = controlDic.Count + 1;

            this.Controls.Add(listBox);
            
            this.ResumeLayout();
            this.ToolsListBox = listBox;
            this.SizeChanged += new EventHandler(Toolbox_SizeChanged);
            #endregion

            for (int i = 1; i < this.TabPageArray.Length; i++)
                this.TabPageArray[i].Dock = DockStyle.Bottom;
            this.ToolsListBox.Location = new System.Drawing.Point(0, (0 + 2) * 20);
            UpdateToolboxItems("WindowsForm控件");//打开第一个
            AddEventHandlers();
		}



		public IDesignerHost DesignerHost
		{
			set
			{
				m_designerHost = value;
			}
			get
			{
				return m_designerHost;
			}
		}



		internal Button[] TabPageArray
		{
			get
			{
				return m_tabPageArray;
			}
			set
			{
				m_tabPageArray = value;
			}
		}
		internal ListBox ToolsListBox
		{
			get
			{
				return m_toolsListBox;
			}
			set
			{
				m_toolsListBox = value;
			}
		}

		private void AddEventHandlers()
		{
			ToolsListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.list_KeyDown);
			ToolsListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.list_MouseDown);
			ToolsListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.list_DrawItem);
		}

		private void list_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
		{
			try
			{
				ListBox lbSender = sender as ListBox;
				if(lbSender==null)
					return;

				// If this tool is the currently selected tool, draw it with a highlight.
				if (selectedIndex == e.Index)
				{
					e.Graphics.FillRectangle(Brushes.LightSlateGray, e.Bounds);
				}

				System.Drawing.Design.ToolboxItem tbi = lbSender.Items[e.Index] as System.Drawing.Design.ToolboxItem;
				Rectangle BitmapBounds = new Rectangle(e.Bounds.Location.X, e.Bounds.Location.Y + e.Bounds.Height / 2 - tbi.Bitmap.Height / 2, tbi.Bitmap.Width, tbi.Bitmap.Height);
				Rectangle StringBounds = new Rectangle(e.Bounds.Location.X + BitmapBounds.Width + 5, e.Bounds.Location.Y, e.Bounds.Width - BitmapBounds.Width, e.Bounds.Height);

				StringFormat format = new StringFormat();

				format.LineAlignment = StringAlignment.Center;
				format.Alignment = StringAlignment.Near;
				e.Graphics.DrawImage(tbi.Bitmap, BitmapBounds);
				e.Graphics.DrawString(tbi.DisplayName, new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.World), Brushes.Black, StringBounds, format);
			}
			catch(Exception ex)
			{
				ex.ToString();
			}
		}

		private void list_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			try
			{
				ListBox lbSender = sender as ListBox;
				Rectangle lastSelectedBounds = lbSender.GetItemRectangle(0);
				try
				{
					lastSelectedBounds = lbSender.GetItemRectangle(selectedIndex);
				}
				catch(Exception ex)
				{
					ex.ToString();
				}

				selectedIndex = lbSender.IndexFromPoint(e.X, e.Y); // change our selection
				lbSender.SelectedIndex = selectedIndex;
				lbSender.Invalidate(lastSelectedBounds); // clear highlight from last selection
				lbSender.Invalidate(lbSender.GetItemRectangle(selectedIndex)); // highlight new one

				if (selectedIndex != 0)
				{
					if (e.Clicks == 2)
					{
						IDesignerHost idh = (IDesignerHost)this.DesignerHost.GetService (typeof(IDesignerHost));
						IToolboxUser tbu = idh.GetDesigner (idh.RootComponent as IComponent) as IToolboxUser;

						if (tbu != null)
						{
							tbu.ToolPicked ((System.Drawing.Design.ToolboxItem)(lbSender.Items[selectedIndex]));
						}
					}
					else if (e.Clicks < 2)
					{
						System.Drawing.Design.ToolboxItem tbi = lbSender.Items[selectedIndex] as System.Drawing.Design.ToolboxItem;
						IToolboxService tbs = this;  

						// The IToolboxService serializes ToolboxItems by packaging them in DataObjects.
						DataObject d = tbs.SerializeToolboxItem (tbi) as DataObject;

						try
						{
							lbSender.DoDragDrop (d, DragDropEffects.Copy);
						}
						catch (Exception ex)
						{
							MessageBox.Show (ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
			catch(Exception ex)
			{
				ex.ToString();
			}
		}

		private void list_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			try
			{
				ListBox lbSender = sender as ListBox;
				Rectangle lastSelectedBounds = lbSender.GetItemRectangle(0);
				try
				{
					lastSelectedBounds = lbSender.GetItemRectangle(selectedIndex);
				}
				catch(Exception ex)
				{
					ex.ToString();
				}
	
				switch (e.KeyCode)
				{
					case Keys.Up: if (selectedIndex > 0)
								{
									selectedIndex--; // change selection
									lbSender.SelectedIndex = selectedIndex;
									lbSender.Invalidate(lastSelectedBounds); // clear old highlight
									lbSender.Invalidate(lbSender.GetItemRectangle(selectedIndex)); // add new one
								}
						break;

					case Keys.Down: if (selectedIndex + 1 < lbSender.Items.Count)
									{
										selectedIndex++; // change selection
										lbSender.SelectedIndex = selectedIndex;
										lbSender.Invalidate(lastSelectedBounds); // clear old highlight
										lbSender.Invalidate(lbSender.GetItemRectangle(selectedIndex)); // add new one
									}
						break;

					case Keys.Enter:
						if (DesignerHost == null)
							MessageBox.Show ("idh Null");

						IToolboxUser tbu = DesignerHost.GetDesigner (DesignerHost.RootComponent as IComponent) as IToolboxUser;
						
						if (tbu != null)
						{
							// Enter means place the tool with default location and default size.
							tbu.ToolPicked ((System.Drawing.Design.ToolboxItem)(lbSender.Items[selectedIndex]));
							lbSender.Invalidate (lastSelectedBounds); // clear old highlight
							lbSender.Invalidate (lbSender.GetItemRectangle (selectedIndex)); // add new one
						}

						break;

					default:
					{
						Console.WriteLine("Error: Not able to add");
						break;					
					}
				} // switch
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

        private void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (button == null)
                return;

            //int index = (int)button.Tag;

            int index = new List<string>(controlDic.Keys).FindIndex(x => x == button.Name);


            if (button.Dock == DockStyle.Top)
            {
                for (int i = index + 1; i < this.TabPageArray.Length; i++)
                    this.TabPageArray[i].Dock = DockStyle.Bottom;
            }
            else
            {
                for (int i = 0; i <= index; i++)
                    this.TabPageArray[i].Dock = DockStyle.Top;
            }

            this.ToolsListBox.Location = new System.Drawing.Point(0, (index + 2) * 20);
            UpdateToolboxItems(button.Name);
        }

        private void UpdateToolboxItems(string key)
        {
            

            this.ToolsListBox.Items.Clear();
            this.ToolsListBox.Items.Add(pointer);
            if (TabPageArray.Length <= 0)
                return;
            
            foreach (var item in controlDic[key])
            {
                Type type = item;
                System.Drawing.Design.ToolboxItem tbi = new System.Drawing.Design.ToolboxItem(type);
                System.Drawing.ToolboxBitmapAttribute tba = TypeDescriptor.GetAttributes(type)[typeof(System.Drawing.ToolboxBitmapAttribute)] as System.Drawing.ToolboxBitmapAttribute;

                if (tba != null)
                {
                    tbi.Bitmap = (System.Drawing.Bitmap)tba.GetImage(type);
                }

                this.ToolsListBox.Items.Add(tbi);
            }
        }

        private void Toolbox_SizeChanged(object sender, EventArgs e)
        {
            this.ToolsListBox.Size = new System.Drawing.Size(this.Width, this.Height - (TabPageArray.Length + 1) * 20);
        }

        #region IToolboxService Members

        // We only implement what is really essential for ToolboxService

        public System.Drawing.Design.ToolboxItem GetSelectedToolboxItem (IDesignerHost host)
		{
			ListBox list = this.ToolsListBox;
            if (list.Items.Count == 0) return null;

			System.Drawing.Design.ToolboxItem tbi = (System.Drawing.Design.ToolboxItem)list.Items[selectedIndex];
			if(tbi.DisplayName != "<Pointer>")
				return tbi;
			else
				return null;
		}

		public System.Drawing.Design.ToolboxItem GetSelectedToolboxItem ()
		{
			return this.GetSelectedToolboxItem(null);
		}

		public void AddToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem, string category)
		{
		}

		public void AddToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem)
		{
		}

		public bool IsToolboxItem (object serializedObject, IDesignerHost host)
		{
			return false;
		}

		public bool IsToolboxItem (object serializedObject)
		{
			return false;
		}

		public void SetSelectedToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem)
		{
		}

		public void SelectedToolboxItemUsed ()
		{
			ListBox list = this.ToolsListBox;
            if (list.Items.Count == 0) return;

            list.Invalidate(list.GetItemRectangle(selectedIndex));
			selectedIndex = 0;
			list.SelectedIndex = 0;
			list.Invalidate (list.GetItemRectangle (selectedIndex));
		}

		public CategoryNameCollection CategoryNames
		{
			get
			{
				return null;
			}
		}

		void IToolboxService.Refresh ()
		{
		}

		public void AddLinkedToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem, string category, IDesignerHost host)
		{
		}

		public void AddLinkedToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem, IDesignerHost host)
		{
		}

		public bool IsSupported (object serializedObject, ICollection filterAttributes)
		{
			return false;
		}

		public bool IsSupported (object serializedObject, IDesignerHost host)
		{
			return false;
		}

		public string SelectedCategory
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public System.Drawing.Design.ToolboxItem DeserializeToolboxItem (object serializedObject, IDesignerHost host)
		{
			return (System.Drawing.Design.ToolboxItem)((DataObject)serializedObject).GetData (typeof(System.Drawing.Design.ToolboxItem));
		}

		public System.Drawing.Design.ToolboxItem DeserializeToolboxItem (object serializedObject)
		{
			return this.DeserializeToolboxItem (serializedObject, this.DesignerHost);
		}

		public System.Drawing.Design.ToolboxItemCollection GetToolboxItems (string category, IDesignerHost host)
		{
			return null;
		}

		public System.Drawing.Design.ToolboxItemCollection GetToolboxItems (string category)
		{
			return null;
		}

		public System.Drawing.Design.ToolboxItemCollection GetToolboxItems (IDesignerHost host)
		{
			return null;
		}

		public System.Drawing.Design.ToolboxItemCollection GetToolboxItems ()
		{
			return null;
		}

		public void AddCreator (ToolboxItemCreatorCallback creator, string format, IDesignerHost host)
		{
		}

		public void AddCreator (ToolboxItemCreatorCallback creator, string format)
		{
		}

		public bool SetCursor ()
		{
			return false;
		}

		public void RemoveToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem, string category)
		{
		}

		public void RemoveToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem)
		{
		}

		public object SerializeToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem)
		{
			return new DataObject (toolboxItem);
		}

		public void RemoveCreator (string format, IDesignerHost host)
		{
		}

		public void RemoveCreator (string format)
		{
		}

	#endregion

	}// class
}// namespace
