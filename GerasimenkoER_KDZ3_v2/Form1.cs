using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GerasimenkoER_KDZ3_v2;

namespace GerasimenkoER_KDZ3_v2
{
    public partial class Form1 : Form
    {
        bool IS_AUTO_UPDATE_ES = false;

        Data d = new Data();
        List<ОПОП> opop = new List<ОПОП>();
        List<Расположение> adr = new List<Расположение>();
        //bool flagmb = false;
        //string name = "";
        //List<List<string>> data = null;
        //string[] datas = null;
        //char separ = ';';
        //bool rewrite = false;
        //bool issaved = true;
        //bool sne = false, ene = false;
        //Encoding encode = Encoding.Default;
        //int sn = -1;
        //int en = -1;
        #region MetoData
        bool flagmb { get { return d.flagmb; } set { d.flagmb = value; } }
        string name { get { return d.name; } set { d.name = value; } }
        List<List<string>> data { get { return d.data; } set { d.data = value; } }
        string[] datas { get { return d.datas; } set { d.datas = value; } }
        char separ { get { return d.separ; } set { d.separ = value; } }
        bool rewrite { get { return d.rewrite; } set { d.rewrite = value; } }
        bool issaved { get { return d.issaved; } set { d.issaved = value; } }
        bool isadded { get { return d.isaded; } set { d.isaded = value; } }
        bool sne { get { return d.sne; } set { d.sne = value; } }
        bool ene { get { return d.ene; } set { d.ene = value; } }
        Encoding encode { get { return d.encode; } set { d.encode = value; } }
        int sn { get { return d.sn; } set { d.sn = value; } }
        int en { get { return d.en; } set { d.en = value; } }
        #endregion



        #region UI

        bool flagcontrol = false;
        bool flagshift = false;
        /// <summary>
        /// Отлов нажатий клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control) flagcontrol = true;
            if(!e.Control) flagcontrol = false;
            if (e.Shift) flagshift = true;
            if (!e.Shift) flagshift = false;
            if (e.KeyCode == Keys.O && flagcontrol)
            {
                openToolStripButton_Click(sender, e);
            }
            if (e.KeyCode == Keys.S && flagcontrol && !flagshift)
            {
                saveToolStripButton_Click(sender, e);
            }
            if (e.KeyCode == Keys.N && flagcontrol)
            {
                newToolStripMenuItem_Click(sender, e);
            }
            if (e.KeyCode == Keys.S && flagcontrol && flagshift)
            {
                saveAsToolStripMenuItem_Click_1(sender, e);
            }

            #region c
            //if (Frac == null || Frac.isdrawing) return;
            //if (e.KeyCode == Keys.C)
            //{
            //    if (this.Frac.isdrawing) return;
            //    Init();
            //}
            //if (e.KeyCode == Keys.E)
            //{
            //    if (this.Frac.isdrawing) return;
            //    ZoomUp();
            //    if (Frac == null)
            //    {
            //        this.textBox1.Text = "1";
            //    }
            //    else
            //    {
            //        this.textBox1.Text = $"{this.Frac.scale:f3}";
            //    }
            //}
            //if (e.KeyCode == Keys.Q)
            //{
            //    if (this.Frac.isdrawing) return;
            //    ZoomDown();
            //    this.label5.Text = $"Масштаб: ";
            //    if (Frac == null)
            //    {
            //        this.textBox1.Text = "1";
            //    }
            //    else
            //    {
            //        this.textBox1.Text = $"{this.Frac.scale:f3}";
            //    }
            //}
            //if (e.KeyCode == Keys.B)
            //{
            //    if (this.Frac.isdrawing) return;
            //    if (checkBox_buffer.Checked)
            //    {
            //        DoubleBuffered = false;
            //        checkBox_buffer.Checked = false;
            //    }
            //    else
            //    {
            //        DoubleBuffered = true;
            //        checkBox_buffer.Checked = true;
            //    }
            //}
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (this.Frac.isdrawing) return;
            //    Rewrite();
            //}
            //if (e.KeyCode == Keys.L && this.textBox1.Text == "42" && this.textBox_max_depth_of_rec.Text == "42")
            //{
            //    fenableformwhendrawing ^= true;
            //    DropExWindow("В чём заключается смысл Жизни: " + (fenableformwhendrawing ? "42" : "I dont know"));
            //}
            #endregion
        }

        /// <summary>
        /// Вывод сообщения об ошибке
        /// </summary>
        /// <param name="s"></param>
        void DropExWindow(string s)
        {
            if (flagmb) return;
            flagmb = true;
            if (MessageBox.Show(s) == DialogResult.OK)
            {
                flagmb = false;
            }
        }

        #region Save

        /// <summary>
        /// Изменение выбранности пункта меню Перезаписать и статуса Перезаписать при записи данных в файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripMenuItem1.Checked ^= true;
            rewrite = toolStripMenuItem1.Checked;
        }

        /// <summary>
        /// Сохранение таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (name.Length == 0)
                {
                    saveAsToolStripMenuItem_Click(sender, e);
                }
                else
                {
                    if (isadded) CSVconv.SaveStrtoCSV("" + name, dataGridView1.Rows, separ, rewrite, this.encode);
                    else CSVconv.SaveStrtoCSV("" + name, data, separ, rewrite, this.encode);
                }
                issaved = true;
            }
            catch (CSVException ex)
            {
                DropExWindow("Ошибка при сохранении файла\n" + ex.Message + ex.InnerException?.Message);
            }
            catch (NullReferenceException ex)
            {
                DropExWindow("Невозможно сохранить несуществующий оъект\n" + ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                DropExWindow("Невозможно сохранить оъект\n" + ex.Message);
            }
            catch (System.Runtime.InteropServices.ExternalException ex)
            {
                DropExWindow("Невозможно сохранить оъект\n" + ex.Message);
            }
            catch (Exception ex)
            {
                DropExWindow("" + ex.Message);
            }
        }

        /// <summary>
        /// Сохранение таблицы как (старый интерфейс)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialogWithEncoding ofd = new SaveFileDialogWithEncoding();
                ofd.DefaultExt = ".csv";
                ofd.EncodingType = EncodingType.UTF8;
                ofd.Filter = "CSV files (',' *.csv)|*.csv|TSV files ('\t' *.csv)|*.csv|SCSV files (';' *.csv)|*.csv|TSV files ('\t' *.tsv)|*.tsv|SCSV files (';' *.scsv)|*.scsv|All files (*.*)|*.*";
                if (separ == ';') { ofd.FilterIndex = 3; }
                if (separ == ',') { ofd.FilterIndex = 1; }
                if (separ == '\t') { ofd.FilterIndex = 2; }
                //if (ofd.ShowDialog() == DialogResult.OK)
                //{
                //    MessageBox.Show(String.Format("Name={0}, Encoding={1}", ofd.FileName, ofd.EncodingType));
                //}
                //FolderBrowserDialog FBD = new FolderBrowserDialog();
                //Encoding encode = null;
                /*
                 * 
                 * 
                UTF8=65001, //Encoding.UTF8
                //UTF8WithPreamble,
                Unicode=1201, //Encoding.Unicode
                Ansi=1251 //Encoding.Default
                 * 
                */
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    int enc = 1251;
                    switch ((int)ofd.EncodingType)
                    {
                        case (0):
                            enc = 65001;break;
                        case (1):
                            enc = 1201; break;
                        case (2):
                            enc = 1251; break;
                    }
                    encode = Encoding.GetEncoding((int)enc);
                    separ = ofd.FilterIndex-1 == 0 ? ',' : ofd.FilterIndex-1 == 1 ? '\t' : ofd.FilterIndex-1 == 2 ? ';' : ofd.FilterIndex-1 == 3 ? '\t' : ofd.FilterIndex-1 == 4 ? ';' : ',';//FBD.FileName[FBD.FileName.Length - 1];
                    name = ofd.FileName;//.Remove(ofd.FileName.Length - 1);

                    if (isadded) CSVconv.SaveStrtoCSV("" + name, dataGridView1.Rows, separ, rewrite, this.encode);
                    else CSVconv.SaveStrtoCSV("" + name, data, separ, rewrite, this.encode/*,(char)ofd.EncodingType*//*,ofd.Rewrite*/);
                    issaved = true;
                }
            }
            catch(CSVException ex)
            {
                DropExWindow("Ошибка при сохранении файла\n" + ex.Message + ex.InnerException?.Message);
            }
            catch (NullReferenceException ex)
            {
                DropExWindow("Невозможно сохранить несуществующий оъект\n" + ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                DropExWindow("Невозможно сохранить оъект\n" + ex.Message);
            }
            catch (System.Runtime.InteropServices.ExternalException ex)
            {
                DropExWindow("Невозможно сохранить оъект\n" + ex.Message);
            }
            catch (Exception ex)
            {
                DropExWindow("" + ex.Message);
            }
        }

        /// <summary>
        /// Сохранение таблицы как текстовый файл (новый интерфейс)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAstxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog FBD = new SaveFileDialog();
                FBD.Filter = "CSV for EXEL files (';' *.csv)|*.csv|All files (*.*)|*.*";
                //FBD.CreatePrompt = true;
                if (FBD.ShowDialog() == DialogResult.OK)
                {
                    separ = FBD.FilterIndex-1 == 0 ? ';' : ',';//FBD.FileName[FBD.FileName.Length - 1];
                    name = FBD.FileName;//.Remove(FBD.FileName.Length - 1);
                    if (isadded) CSVconv.SaveStrtoCSV("" + name, dataGridView1.Rows, separ, rewrite, this.encode);
                    else CSVconv.SaveStrtoCSV("" + name, data, separ, rewrite, Encoding.Default);
                    issaved = true;
                }
            }
            catch (CSVException ex)
            {
                DropExWindow("Ошибка при сохранении файла\n" + ex.Message + ex.InnerException?.Message);
            }
            catch (NullReferenceException ex)
            {
                DropExWindow("Невозможно сохранить несуществующий оъект\n" + ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                DropExWindow("Невозможно сохранить оъект\n" + ex.Message);
            }
            catch (System.Runtime.InteropServices.ExternalException ex)
            {
                DropExWindow("Невозможно сохранить оъект\n" + ex.Message);
            }
            catch (ArgumentException ex)
            {
                DropExWindow("Невозможно сохранить оъект\n" + ex.Message);
            }
            catch (Exception ex)
            {
                DropExWindow("" + ex.Message);
            }
        }

        /// <summary>
        /// Сохранение таблицы как (новый интерфейс)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog FBD = new SaveFileDialog();
                FBD.Filter = "CSV files (',' *.csv)|*.csv|TSV files ('\t' *.csv)|*.csv|SCSV files (';' *.csv)|*.csv|TSV files ('\t' *.tsv)|*.tsv|SCSV files (';' *.scsv)|*.scsv|All files (*.*)|*.*";
                if (separ == ';') { FBD.FilterIndex = 3; }
                if (separ == ',') { FBD.FilterIndex = 1; }
                if (separ == '\t') { FBD.FilterIndex = 2; }
                if (FBD.ShowDialog() == DialogResult.OK)
                {
                    separ = FBD.FilterIndex - 1 == 0 ? ',' : FBD.FilterIndex - 1 == 1 ? '\t' : FBD.FilterIndex - 1 == 2 ? ';' : FBD.FilterIndex - 1 == 3 ? '\t' : FBD.FilterIndex - 1 == 4 ? ';' : ',';//FBD.FileName[FBD.FileName.Length - 1];
                    name = FBD.FileName;//.Remove(FBD.FileName.Length - 1);
                    if(isadded) CSVconv.SaveStrtoCSV("" + name, dataGridView1.Rows, separ, rewrite, this.encode);
                    else CSVconv.SaveStrtoCSV("" + name, data, separ, rewrite, this.encode);
                    issaved = true;
                }
            }
            catch (CSVException ex)
            {
                DropExWindow("Ошибка при сохранении файла\n" + ex.Message + ex.InnerException?.Message);
            }
            catch (NullReferenceException ex)
            {
                DropExWindow("Невозможно сохранить несуществующий оъект\n" + ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                DropExWindow("Невозможно сохранить оъект\n" + ex.Message);
            }
            catch (System.Runtime.InteropServices.ExternalException ex)
            {
                DropExWindow("Невозможно сохранить оъект\n" + ex.Message);
            }
            catch (ArgumentException ex)
            {
                DropExWindow("Невозможно сохранить оъект\n" + ex.Message);
            }
            catch (Exception ex)
            {
                DropExWindow("" + ex.Message);
            }
        }

        /// <summary>
        /// Сохранение таблицы как (новый интерфейс)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            fileToolStripMenuItem.HideDropDown();
            saveAsToolStripMenuItem_Click(sender, e);

        }

        /// <summary>
        /// Analog of save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem1_Click(sender, e);
        }




        #endregion

        #region Load

        /// <summary>
        /// Load data from file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SaveorLose(issaved))
            {
                return;
            }
            if (encodingToolStripComboBox1.Text != "Encoding Type")
            {
                //encodingToolStripComboBox1.Name
                encode = Encoding.GetEncoding((int)int.Parse(encodingToolStripComboBox1.Text.Split(' ')[0]));
            }
            if (typeToolStripMenuItem.Text != "Separator Type")
            { //typeToolStripMenuItem.Name  
                separ = CSVconv.GetSeparType(typeToolStripMenuItem.Text[0]);
            }
            //contextMenuStrip1.Show();
            try
            {
                OpenFileDialog FBD = new OpenFileDialog();
                FBD.AddExtension = false;
                if (name.Length > 0) { FBD.FileName = name; }
                FBD.Filter = "CSV files (',' *.csv)|*.csv|TSV files ('\t' *.csv)|*.csv|SCSV files (';' *.csv)|*.csv|TSV files ('\t' *.tsv)|*.tsv|SCSV files (';' *.scsv)|*.scsv|All files (*.*)|*.*";
                if (separ == ';') { FBD.FilterIndex = 3; }
                if (separ == ',') { FBD.FilterIndex = 1; }
                if (separ == '\t') { FBD.FilterIndex = 2; }

                if (FBD.ShowDialog() == DialogResult.OK)
                {
                    if (isadded) { toolStripMenuItem5_Click(sender, e); }
                    separ = FBD.FilterIndex-1 == 0 ? ',' : FBD.FilterIndex - 1 == 1 ? '\t' : FBD.FilterIndex - 1 == 2 ? ';' : FBD.FilterIndex - 1 == 3 ? '\t' : FBD.FilterIndex - 1 == 4 ? ';' : ',';//FBD.FileName[FBD.FileName.Length - 1];
                    name = FBD.FileName;//.Remove(FBD.FileName.Length - 1);
                    datas = CSVconv.fscanf(name, this.encode);
                    data=CSVconv.LoadCSVtoStr("" + name, separ, this.encode);
                    UpdateData(data, out opop, out adr);
                    UpdateGrid();
                    issaved = true;
                }
            }
            catch (CSVException ex)
            {
                DropExWindow("Ошибка при загрузке данных из файла\n" + ex.Message + ex.InnerException?.Message);
            }
            catch (NullReferenceException ex)
            {
                DropExWindow("Невозможно загрузить несуществующий оъект\n" + ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                DropExWindow("Невозможно загрузить оъект\n" + ex.Message);
            }
            catch (System.Runtime.InteropServices.ExternalException ex)
            {
                DropExWindow("Невозможно загрузить оъект\n" + ex.Message);
            }
            catch (Exception ex)
            {
                DropExWindow("" + ex.Message);
            }
        }

        /// <summary>
        /// Load data from file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadToolStripMenuItem_Click(object sender, EventArgs e, bool f = true)
        {
            if (!SaveorLose(issaved))
            {
                return;
            }
            //contextMenuStrip1.Show(); c
            try
            {
                if (f || datas == null)
                {
                    #region comment
                    //SaveFileDialog FBD = new SaveFileDialog();
                    //FBD.Filter = "CSV files (',' *.csv)|*.csv|TSV files ('\t' *.csv)|*.csv|SCSV files (';' *.csv)|*.csv|TSV files ('\t' *.tsv)|*.tsv|SCSV files (';' *.scsv)|*.scsv|All files (*.*)|*.*";
                    //if (FBD.ShowDialog() == DialogResult.OK)
                    //{
                    //    separ = FBD.FilterIndex-1 == 0 ? ',' : FBD.FilterIndex-1 == 1 ? '\t' : FBD.FilterIndex-1 == 2 ? ';' : FBD.FilterIndex-1 == 3 ? '\t' : FBD.FilterIndex-1 == 4 ? ';' : ',';//FBD.FileName[FBD.FileName.Length - 1];
                    //    name = FBD.FileName;//.Remove(FBD.FileName.Length - 1);
                    //    datas = CSVconv.fscanf(name);
                    //    data = CSVconv.LoadCSVtoStr("" + name, separ);
                    //}
                    #endregion
                    loadToolStripMenuItem_Click(sender, e);
                }
                else
                {
                    List<List<string>> res = new List<List<string>>();
                    for (int i = 0; i < datas.Length; i++)
                    {
                        res.Add(CSVconv.ConvertCSVlinetoListstr(datas[i], separ));
                    }
                    data = res;
                }
                UpdateGrid();
                issaved = true;
            }
            catch (CSVException ex)
            {
                DropExWindow("Ошибка при загрузке файла\n" + ex.Message + ex.InnerException?.Message);
            }
            catch (NullReferenceException ex)
            {
                DropExWindow("Невозможно загрузить несуществующий оъект\n" + ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                DropExWindow("Невозможно загрузить оъект\n" + ex.Message);
            }
            catch (System.Runtime.InteropServices.ExternalException ex)
            {
                DropExWindow("Невозможно загрузить оъект\n" + ex.Message);
            }
            catch (Exception ex)
            {
                DropExWindow("" + ex.Message);
            }
        }

        /// <summary>
        /// Reopen file with encoding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReopenToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!SaveorLose(issaved))
            {
                return;
            }
            //if (ReopenToolStripMenuItem.DropDownItems[0] != null) { //encodingToolStripComboBox1.Name
            //encode = Encoding.GetEncoding((int)int.Parse(ReopenToolStripMenuItem.DropDownItems[0].Text.Split(' ')[0]));}
            //separ = CSVconv.GetSeparType(ReopenToolStripMenuItem.DropDownItems[1].Text[0]); //typeToolStripMenuItem.Name
            bool reopen = false;
            if (encodingToolStripComboBox1.Text != "Encoding Type") { reopen = encode != Encoding.GetEncoding((int)int.Parse(encodingToolStripComboBox1.Text.Split(' ')[0]));
                //encodingToolStripComboBox1.Name
            encode = Encoding.GetEncoding((int)int.Parse(encodingToolStripComboBox1.Text.Split(' ')[0]));}
            if (typeToolStripMenuItem.Text != "Separator Type") { //typeToolStripMenuItem.Name  
            separ = CSVconv.GetSeparType(typeToolStripMenuItem.Text[0]);}
            loadToolStripMenuItem_Click(sender, e, reopen);
            issaved = false;
        }

        /// <summary>
        /// Quick load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            loadToolStripMenuItem_Click(sender, e);
        }


        #endregion


        /// <summary>
        /// Новая таблица
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SaveorLose(issaved))
            {
                return;
            }
            //if (this.Frac != null && this.Frac.isdrawing) return;
            //(new Form1()).ShowDialog(new Form1());
            dataGridView1.Rows.Clear();
            data = new List<List<string>>();
            data.Add(new List<string>());
            if (dataGridView1.Columns.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Columns.Count; ++i)
                {
                    data[0].Add(((string)(dataGridView1.Columns[i].HeaderText)));
                }
                data.Add(new List<string>());
                for (int i = 0; i < dataGridView1.Columns.Count; ++i)
                {
                    data[1].Add(((string)("")));
                }
            }
            else
            {
                int n = 1;
                for (int i = 0; i < n; ++i)
                {
                    data[0].Add(((string)("")));
                }
                data.Add(new List<string>());
                for (int i = 0; i < n; ++i)
                {
                    data[1].Add(((string)("")));
                }
            }
            UpdateData(data, out opop, out adr);
            UpdateGrid();
            issaved = true;
        }

        #region table_edit

        /// <summary>
        /// Event New table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Add row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try {
                //
                if (dataGridView1.SelectedRows.Count == 0 && data != null) { dataGridView1.Rows.Add(); }
                else { dataGridView1.Rows.Insert(dataGridView1.SelectedRows[dataGridView1.SelectedRows.Count - 1].Index); }
                return;
                
                    
                
                if (dataGridView1.Rows.Count == 0 || dataGridView1.SelectedRows.Count == 0) {
                    dataGridView1.Rows.Add();
                    if (datas == null)
                    {
                        data = new List<List<string>>();
                        datas = new string[0];
                    }
                    data.Add(new List<string>(data[0].Count));
                    for(int i = 0; i < data[data.Count - 1].Count; i++)
                    {
                        data[data.Count - 1][i] = "";
                    }
                    string[] datas2 = datas;
                    Array.Resize(ref datas2, datas.Length + 1);
                    datas = datas2;
                    datas[datas.Length - 1] = "";
                }
                else {
                    data.Insert(dataGridView1.SelectedRows[dataGridView1.SelectedRows.Count - 1].Index, new List<string>(data[0].Count));
                    for (int i = 0; i < data[dataGridView1.SelectedRows[dataGridView1.SelectedRows.Count - 1].Index].Count; i++)
                    {
                        data[dataGridView1.SelectedRows[dataGridView1.SelectedRows.Count - 1].Index][i] = "";
                    }
                    string[] datas2 = datas;
                    Array.Resize(ref datas2, datas.Length + 1);
                    datas = datas2;
                    for (int i = 0; i < data.Count; i++) { 
                        datas[i]=CSVconv.ConvertListstrtoCSVline(data[i],separ);
                    }
                    dataGridView1.Rows.Insert(dataGridView1.SelectedRows[dataGridView1.SelectedRows.Count-1].Index);

                }
                
            }
            catch(System.InvalidOperationException ex)
            {
                DropExWindow(ex.Message + "\nСоздайте колонку");
            }
            catch(Exception ex)
            {
                DropExWindow("\nСоздайте колонку!");
            }
        }

        /// <summary>
        /// Added rows event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //e.RowIndex;
            if (isadded) { return; }
            for (int k = 0; k < e.RowCount; k++) { 
                data.Insert(e.RowIndex+sn, new List<string>(data[0].Count));
                for (int i = 0; i < data[0].Count; i++)
                {
                    data[e.RowIndex + sn].Add("");
                    //data[e.RowIndex+sn][i] = "";
                }
                string[] datas2 = datas;
                Array.Resize(ref datas2, data.Count + 1);
                datas = datas2;
                for (int i = 0; i < data.Count; i++)
                {
                    datas[i] = CSVconv.ConvertListstrtoCSVline(data[i], separ);
                }
                //dataGridView1.Rows.Insert(dataGridView1.SelectedRows[dataGridView1.SelectedRows.Count - 1].Index);
            }
            UpdateData(data, out opop, out adr);
            int _ = 0;
        }
        
        /// <summary>
        /// Remove row from data
        /// </summary>
        /// <param name="d"></param>
        int removeRowInData(DataGridViewRow d)
        {
            int i = 0;
            for(i = 0; i < data.Count; ++i)
            {
                bool flag = true;
                for(int j = 0; j < Math.Min(data[i].Count, d.Cells.Count); ++j)
                {
                    if(data[i][j]!=((string)(d.Cells[j].Value))) { flag = false; break; }
                }
                if (flag) {
                    data.RemoveAt(i);
                    List<string> datasp = datas.ToList();
                    datasp.RemoveAt(i);
                    datas = datasp.ToArray();
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Deleted rows event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_DeleteRow(object sender, DataGridViewRowEventArgs e)
        {
            if(!SaveorLose(false,"Вы точно хотите удалить ячейку?")) { UpdateGrid(); return; }
            if (removeRowInData(e.Row) == -1) { DropExWindow("Not a string in data.\n Please reopen table"); }
            UpdateData(data, out opop, out adr);
        }

        /// <summary>
        /// Add column
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void columnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (datas == null)
            {
                data = new List<List<string>>();
                data.Add(new List<string>());
                //data.Add(new List<string>());
                datas = new string[1];
            }
            string str = columnNameToolStripMenuItem.Text;
            var column = new DataGridViewTextBoxColumn();
            column.HeaderText = str;
            column.Name = str;
            column.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView1.Columns.Add(column);
            //dataGridView1.Columns[dataGridView1.Columns.Count-1].SortMode = DataGridViewColumnSortMode.Programmatic;
            //data[0].Add("");
            for (int i = 0; i < data.Count-1; i++)
            {
                data[i].Add("");
                datas[i] = CSVconv.ConvertListstrtoCSVline(data[i], separ);
            }
            UpdateData(data, out opop, out adr);
        }

        /// <summary>
        /// Edit data of some cell eventHandler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editCellData(object sender, System.Windows.Forms.DataGridViewCellEventArgs e) {
            if(isadded) { return; }
            int ci = e.ColumnIndex;
            int ri = e.RowIndex;
            data[ri+sn][ci] = (string)dataGridView1.Rows[ri].Cells[ci].Value;
            datas[ri+sn] = CSVconv.ConvertListstrtoCSVline(data[ri+sn], separ);
            UpdateData(data, out opop, out adr);
        }

        #endregion

        #region Find

        /// <summary>
        /// Create AhoCorasic exemplar
        /// </summary>
        /// <param name="which"></param>
        /// <returns></returns>
        private AhoCorasik FindSubstring(params string[] which) //string where, vector<pair<vector<int>, string>>
        {
            int max = 0;
            foreach(var i in which) { max = Math.Max(max, i.Length); }
            AhoCorasik act = new AhoCorasik(which,which.Length*max);


            return act;//.find(where);
        }

        /// <summary>
        /// Find equal string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        private int findInString(string s, string[] w)
        {
            int i = 0;
            for(i = 0; i < w.Length; i++)
            {
                if (s == w[i]) return i;
            }
            return i;
        }

        /// <summary>
        /// Only one of equals string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string[] deleteCopy(string[] s)
        {
            List<string> sr = new List<string>();
            for(int i = 0; i < s.Length; ++i)
            {
                bool flag = true;
                foreach (string j in sr)
                {
                    if (j == s[i])
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag) sr.Add(s[i]);
            }
            return sr.ToArray();
        }

        /// <summary>
        /// Find substrings in string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (datas == null) { return; }
            string[] ws=ssesepToolStripMenuItem.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            ws = deleteCopy(ws);
            List<string>[] str = new List<string>[ws.Length];
            for(int i = 0; i < ws.Length; ++i)
            {
                str[i] = new List<string>();
                str[i].Add(ws[i] + ": \n\r");
            }
            AhoCorasik act = FindSubstring(ws);
            for (int di = 0; di < (isadded?dataGridView1.Rows.Count:data.Count); ++di){
                int colnum=0;
                if (isadded)
                {
                    if(!dataGridView1.Rows[di].Visible) { continue; }
                    try
                    {
                        for (int ii = 0; ii < dataGridView1.Rows[di].Cells.Count; ++ii)
                        {
                            string i = (string)dataGridView1.Rows[di].Cells[ii].Value;
                            if (i == null) { continue; }
                            vector<pair<vector<int>, string>> v = act.find(i);
                            for (int j = 0; j < v.size(); ++j)
                            { //ws
                                if (v[j].first.size() == 0) { continue; }
                                int n = findInString(v[j].second, ws);
                                str[n].Add("(R:C)=(" + (string)dataGridView1.Rows[di].Cells[0].Value + ":" + (colnum + 1) + "): " + v[j].first.tostring(",") + '\n' + '\r');
                            }
                            colnum++;
                        }
                    }
                    catch(Exception ex) { }
                }
                else
                {
                    foreach (string i in data[di])
                    {
                        vector<pair<vector<int>, string>> v = act.find(i);
                        for (int j = 0; j < v.size(); ++j)
                        { //ws
                            if (v[j].first.size() == 0) { continue; }
                            int n = findInString(v[j].second, ws);
                            str[n].Add("(R:C)=(" + (string)dataGridView1.Rows[di].Cells[0].Value + ":" + (colnum + 1) + "): " + v[j].first.tostring(",") + '\n' + '\r');
                        }
                        colnum++;
                    }
                }
            }
            vector<string> outs = new vector<string>();
            for(int i = 0; i < str.Length; ++i)
            {
                bool flag = true;
                foreach(var j in str[i]) {
                    outs.append(j);
                    if (flag) { outs.append("Count: " + (str[i].Count - 1)); flag = false; }
                }

            }
            //DropExWindow(outs.tostring("\n"));
            this.textBox1.Lines = outs.toarray();
            //textBox1.Text=outs.tostring("\n\r");
        }


        #endregion

        #region Sort

        //private Button sortButton = new Button();

        private void dataGridView1_SortCompare(object sender,
        DataGridViewSortCompareEventArgs e)
        {
            // Try to sort based on the cells in the current column.
            bool flag = true;
            try
            {
                string e1 = (string)e.CellValue1;
                string e2 = (string)e.CellValue2;
                if (e.CellValue1 == null)
                {
                    e1 = "";
                }
                if (e.CellValue2 == null)
                {
                    e2 = "";
                }
                if (flag && (e.Column.Name == "OPOPNumber" || e.Column.Name == "GLOBALID"))
                {
                    flag = false;
                    int n = Math.Max(e1.Length, e2.Length);
                    e.SortResult = String.Compare((int.Parse(e1)).ToString("D" + n), (int.Parse(e2)).ToString("D" + n));
                }
                if (flag && e.Column.Name == "ROWNUM")
                {
                    flag = false;
                    int n = Math.Max(e1.Length, e2.Length);
                    e.SortResult = String.Compare((int.Parse(e1)).ToString("D" + n), (int.Parse(e2)).ToString("D" + n));
                }

                if (flag) { e.SortResult = System.String.Compare(e1, e2); }
                // If the cells are equal, sort based on the ID column.
                if (e.SortResult == 0 && e.Column.Name != "ROWNUM")
                {
                    int n = Math.Max(e1.Length, e2.Length);
                    e.SortResult = String.Compare((int.Parse(e1)).ToString("D" + n), (int.Parse(e2)).ToString("D" + n));
                }
                e.Handled = true;
            }
            catch(ArgumentNullException ex)
            {
                DropExWindow("Значение ячейки имеет неверный формат\n" + ex.Message);
            }
            catch (FormatException ex)
            {
                DropExWindow("Значение ячейки имеет неверный формат\n" + ex.Message);
            }
            catch (OverflowException ex)
            {
                DropExWindow("Значение ячейки имеет неверный формат\n" + ex.Message);
            }
            catch (Exception ex)
            {
                DropExWindow("Значение ячейки имеет неверный формат\n" + ex.Message);
            }
        }





        #endregion

        #region Filter

        /// <summary>
        /// Start filtering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            issaved = false;
            bool flag = true;
            if (!isadded) {
                DropExWindow("Для начала фильтрации установите галочку в поле Filtering");
                return;
            }
            try
            {
                string colname = nameOfColumnToolStripMenuItem.Text;
                string[] filtert = textToolStripMenuItem.Text.Split(';');
                //BindingSource filter = new BindingSource();
                //filter.DataSource = dataGridView1.Columns[colname];
                ////filter.Filter = colname + " Like " + filtert;
                //if (filtert == "")
                //{
                //    filter.Filter = String.Empty;
                //}
                //else
                //{
                //    filter.Filter = colname + " Like '%" + filtert + "%'";
                //}

                ////DataSet ds = new DataSet();
                ////ds = ((DataSet)(dataGridView1.DataSource));
                ////DataSet deT = (DataSet)(dataGridView1.DataSource);
                ////DataView dv = ds.Tables[0].DefaultView;
                ////dv.RowFilter = string.Format("country LIKE '%{0}%'", filtert);
                ////dataGridView1.DataSource = dv;

                ////(dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format(""+colname+" = '{0}'", filtert);

                //dataGridView1.DataSource = filter;
                AhoCorasik act = FindSubstring(filtert);
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (filtert!=null && act.find((string)(dataGridView1.Rows[i].Cells[dataGridView1.Columns[colname].Index].Value)).size() == 0)
                    {
                        dataGridView1.Rows[i].Visible = false;
                        flag = false;
                        continue;
                    }
                    dataGridView1.Rows[i].Visible = true;
                    flag = false;
                    
                }
            }
            catch(Exception ex)
            {
                if(flag) DropExWindow("Проверьте правильность введённых полей\n" + ex.Message);
            }
        }

        /// <summary>
        /// Change filtering state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            issaved = false;
            isadded ^= true;
            toolStripMenuItem5.Checked = isadded;
            if (!isadded)
            {
                dataGridView1.DataSource = null;
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Visible = true;

            }
        }

        /// <summary>
        /// Filter for selected opop's number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            issaved = false;
            if (!(dataGridView1.SelectedRows.Count != 0 && data != null)) { DropExWindow("Выберите строку с ОПОП для фильтрации"); return; }
            for(int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                try
                {
                    if (!dataGridView1.Rows[i].Visible) { continue; }
                    bool flag = true;
                    foreach (System.Windows.Forms.DataGridViewRow j in dataGridView1.SelectedRows) {//dataGridView1.SelectedRows[dataGridView1.SelectedRows.Count-1]
                        if (((string)(dataGridView1.Rows[i].Cells["OPOPNumber"].Value)) == ((string)(j.Cells["OPOPNumber"].Value))) { flag = false; break; }
                    }
                    if (flag) { dataGridView1.Rows[i].Visible = false; }
                }
                catch(Exception ex) { }
            }
        }


        #endregion

        #endregion




        #region backend

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Form1()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Конструктор от строки
        /// </summary>
        public Form1(string s):this()
        {
            if (s != "")
            {
                DropExWindow(s);
            }
        }

        /// <summary>
        /// Load Form1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        

        /// <summary>
        /// Update data of ОПОП
        /// </summary>
        /// <param name="data"></param>
        /// <param name="opop"></param>
        /// <param name="adr"></param>
        private void UpdateData(List<List<string>> data, out List<ОПОП> opop, out List<Расположение> adr)
        {
            issaved = false;
            opop = new List<ОПОП>();
            adr = new List<Расположение>();
            if (data[0].Count < 11) { return; }
            bool flage = true;
            foreach (var i in data)
            {
                if (flage)
                {
                    flage = false;
                    continue;
                }
                if (i.Count < 11) { continue; }
                bool flag = true;
                int k = 0;
                for (; k < adr.Count; ++k)
                {
                    if (adr[k].AdmArea == i[3] && adr[k].District == i[4])
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    adr.Add(new Расположение());
                }
                opop.Add(new ОПОП(i, adr[k]));

            }
        }

        /// <summary>
        /// Изменение выбранности пункта меню Поверх остальных окон и статуса Поверх остальных окон основного окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void overAllWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            overAllWindowsToolStripMenuItem.Checked ^= true;
            TopMost = overAllWindowsToolStripMenuItem.Checked;
        }

        ///// <summary>
        ///// Загрузка файла .CSV
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>

        /// <summary>
        /// Обновление таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateGrid(object sender = null, EventArgs e = null)
        {
            //isadded = true;
            bool flag = false;
            if (!isadded) { toolStripMenuItem5_Click(sender, e); flag = true; }
            sn = en = -1;
            int ssn=0;
            if (sne) { if (!int.TryParse(toolStripMenuItem3.Text, out ssn) || ssn < 1) { sn = 1; DropExWindow("Uncorrect format of value \"From\" (needed>=1)"); return; } }
            sn = ssn;
            if (ene) { if (!int.TryParse(toolStripMenuItem4.Text, out ssn) || ssn <= sn || ssn>=data.Count) { en = data.Count-1;  DropExWindow("Uncorrect format of value \"To\" (Count of rows>needed>From)"); return; } }
            en = ssn;
            sn = Math.Max(sn,1);
            if (en <= sn) { en = data.Count-1; }
            if(data==null) { return; }
            int maxlen = data[0].Count;
            for(int i = sn; i < en; ++i)
            {
                maxlen = Math.Max(maxlen, data[i].Count);
            }
            dataGridView1.Columns.Clear();
            for (int i = 0; i < maxlen; ++i)
            {
                string str = "";
                if (i < data[0].Count)
                {
                    str = data[0][i];
                }
                if (data[0].Count > i) { str = data[0][i]; }
                var column = new DataGridViewTextBoxColumn();
                column.HeaderText = str;
                column.Name = str;
                column.CellTemplate = new DataGridViewTextBoxCell();
                dataGridView1.Columns.Add(column);
            }
            for (int j = sn; j < en; ++j) { 
                dataGridView1.Rows.Add(data[j].ToArray());
            }
            //isadded = false;
            if (flag) { toolStripMenuItem5_Click(sender, e); flag = false; }
            Invalidate();
        }


        /// <summary>
        /// Закрытие приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Form1Closed(object sender, EventArgs e)
        {
            
            this.Close();
        }

        /// <summary>
        /// Хотите ли вы продолжить и потерять изменения?
        /// </summary>
        /// <param name="s">Предупреждающая строка</param>
        /// <returns>Закрыть (false) ОК (true)</returns>
        public static bool SaveorLose(bool f=false, string s="При продолжении несохранённые данные будут потеряны.\nВы точно хотите продолжить?\n") {//Для отмены закройте это окно.
            if (f) { return f; }
            bool flag = MessageBox.Show(text:s,caption:"WARNING",buttons:MessageBoxButtons.OKCancel,icon:MessageBoxIcon.Warning) == DialogResult.OK;
            return flag;
            //return f || MessageBox.Show(s) == DialogResult.OK;
        }


        /// <summary>
        /// Отлов события Закрытие программы и предложение сохранить данные или выйти из программы
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (datas == null || issaved)
            {
                Dispose();
            }
            else
            {
                if (SaveorLose(issaved))
                {
                    Dispose();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Переопределение перерисовки окна
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                this.Text = name.Split('\\')[name.Split('\\').Length-1];
                //if (datas != null) { UpdateGrid(); }
                base.OnPaint(e);
            }
            catch (Exception ex)
            {
                DropExWindow("" + ex.Message);
            }
        }

        /// <summary>
        /// Закрытие контекстного меню с
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuItem_Closing(object sender, CancelEventArgs e)
        {
            if (IS_AUTO_UPDATE_ES) { 
                if (encodingToolStripComboBox1.Text != "Encoding Type")
                {
                    //encodingToolStripComboBox1.Name
                    encode = Encoding.GetEncoding((int)int.Parse(encodingToolStripComboBox1.Text.Split(' ')[0]));
                }
                if (typeToolStripMenuItem.Text != "Separator Type")
                { //typeToolStripMenuItem.Name  
                    separ = CSVconv.GetSeparType(typeToolStripMenuItem.Text[0]);
                }
            }
            //Invalidate();
        }

        /// <summary>
        /// Обновление номера начальной колонки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fromToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sne^= true;
            fromToolStripMenuItem.Checked = sne;
        }

        /// <summary>
        /// Обновление номера конечной колонки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ene ^= true;
            toToolStripMenuItem.Checked = ene;
        }


        #endregion

        
    }
}
