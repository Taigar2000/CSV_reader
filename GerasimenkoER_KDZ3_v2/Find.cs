//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace GerasimenkoER_KDZ3_v2
//{
//    public partial class Find : Form
//    {
//        //public Find()
//        //{
//        //    InitializeComponent();
//        //}

//        private Button sortButton = new Button();
//        private DataGridView dataGridView1 = new DataGridView();

//        // Initializes the form.
//        // You can replace this code with designer-generated code.
//        public Find()
//        {
//            PopulateDataGridView();
//            dataGridView1.Dock = DockStyle.Fill;
//            dataGridView1.AllowUserToAddRows = false;
//            dataGridView1.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
//            dataGridView1.MultiSelect = false;

//            sortButton.Dock = DockStyle.Bottom;
//            sortButton.Text = "Sort";

//            Controls.Add(dataGridView1);
//            Controls.Add(sortButton);
//            Text = "DataGridView programmatic sort demo";
//        }



//        // Populates the DataGridView.
//        // Replace this with your own code to populate the DataGridView.
//        public void PopulateDataGridView()
//        {
//            // Add columns to the DataGridView.
//            dataGridView1.ColumnCount = 2;
//            dataGridView1.Columns[0].HeaderText = "Last Name";
//            dataGridView1.Columns[1].HeaderText = "City";
//            // Put the new columns into programmatic sort mode
//            dataGridView1.Columns[0].SortMode =
//                DataGridViewColumnSortMode.Programmatic;
//            dataGridView1.Columns[1].SortMode =
//                DataGridViewColumnSortMode.Programmatic;

//            // Populate the DataGridView.
//            dataGridView1.Rows.Add(new string[] { "Parker", "Seattle" });
//            dataGridView1.Rows.Add(new string[] { "Watson", "Seattle" });
//            dataGridView1.Rows.Add(new string[] { "Osborn", "New York" });
//            dataGridView1.Rows.Add(new string[] { "Jameson", "New York" });
//            dataGridView1.Rows.Add(new string[] { "Brock", "New Jersey" });
//        }

//        protected override void OnLoad(EventArgs e)
//        {
//            sortButton.Click += new EventHandler(sortButton_Click);

//            base.OnLoad(e);
//        }

//        private void sortButton_Click(object sender, System.EventArgs e)
//        {
//            // Check which column is selected, otherwise set NewColumn to null.
//            DataGridViewColumn newColumn =
//                dataGridView1.Columns.GetColumnCount(
//                DataGridViewElementStates.Selected) == 1 ?
//                dataGridView1.SelectedColumns[0] : null;

//            DataGridViewColumn oldColumn = dataGridView1.SortedColumn;
//            ListSortDirection direction;

//            // If oldColumn is null, then the DataGridView is not currently sorted.
//            if (oldColumn != null)
//            {
//                // Sort the same column again, reversing the SortOrder.
//                if (oldColumn == newColumn &&
//                    dataGridView1.SortOrder == SortOrder.Ascending)
//                {
//                    direction = ListSortDirection.Descending;
//                }
//                else
//                {
//                    // Sort a new column and remove the old SortGlyph.
//                    direction = ListSortDirection.Ascending;
//                    oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
//                }
//            }
//            else
//            {
//                direction = ListSortDirection.Ascending;
//            }

//            // If no column has been selected, display an error dialog  box.
//            if (newColumn == null)
//            {
//                MessageBox.Show("Select a single column and try again.",
//                    "Error: Invalid Selection", MessageBoxButtons.OK,
//                    MessageBoxIcon.Error);
//            }
//            else
//            {
//                dataGridView1.Sort(newColumn, direction);
//                newColumn.HeaderCell.SortGlyphDirection =
//                    direction == ListSortDirection.Ascending ?
//                    SortOrder.Ascending : SortOrder.Descending;
//            }
//        }
//    }
//}

#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#endregion
partial class Find : Form
{
    private DataGridView dataGridView1 = new DataGridView();

    
    

    public Find()
    {
        // Initialize the form.
        // This code can be replaced with designer generated code.
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.Dock = DockStyle.Fill;
        dataGridView1.SortCompare += new DataGridViewSortCompareEventHandler(
            this.dataGridView1_SortCompare);
        Controls.Add(this.dataGridView1);
        this.Text = "DataGridView.SortCompare demo";

        PopulateDataGridView();
    }

    // Replace this with your own population code.
    public void PopulateDataGridView()
    {
        // Add columns to the DataGridView.
        dataGridView1.ColumnCount = 3;

        // Set the properties of the DataGridView columns.
        dataGridView1.Columns[0].Name = "ID";
        dataGridView1.Columns[1].Name = "Name";
        dataGridView1.Columns[2].Name = "City";
        dataGridView1.Columns["ID"].HeaderText = "ID";
        dataGridView1.Columns["Name"].HeaderText = "Name";
        dataGridView1.Columns["City"].HeaderText = "City";

        // Add rows of data to the DataGridView.
        dataGridView1.Rows.Add(new string[] { "1", "Parker", "Seattle" });
        dataGridView1.Rows.Add(new string[] { "2", "Parker", "New York" });
        dataGridView1.Rows.Add(new string[] { "3", "Watson", "Seattle" });
        dataGridView1.Rows.Add(new string[] { "4", "Jameson", "New Jersey" });
        dataGridView1.Rows.Add(new string[] { "5", "Brock", "New York" });
        dataGridView1.Rows.Add(new string[] { "6", "Conner", "Portland" });

        // Autosize the columns.
        dataGridView1.AutoResizeColumns();
    }

    private void dataGridView1_SortCompare(object sender,
        DataGridViewSortCompareEventArgs e)
    {
        // Try to sort based on the cells in the current column.
        e.SortResult = System.String.Compare(
            e.CellValue1.ToString(), e.CellValue2.ToString());

        // If the cells are equal, sort based on the ID column.
        if (e.SortResult == 0 && e.Column.Name != "ID")
        {
            e.SortResult = System.String.Compare(
                dataGridView1.Rows[e.RowIndex1].Cells["ID"].Value.ToString(),
                dataGridView1.Rows[e.RowIndex2].Cells["ID"].Value.ToString());
        }
        e.Handled = true;
    }
}