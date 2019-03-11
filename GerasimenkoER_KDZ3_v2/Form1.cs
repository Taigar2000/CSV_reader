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

        bool flagmb { get { return d.flagmb; } set { d.flagmb = value; } }
        string name { get { return d.name; } set { d.name = value; } }
        List<List<string>> data { get { return d.data; } set { d.data = value; } }
        string[] datas { get { return d.datas; } set { d.datas = value; } }
        char separ { get { return d.separ; } set { d.separ = value; } }
        bool rewrite { get { return d.rewrite; } set { d.rewrite = value; } }
        bool issaved { get { return d.issaved; } set { d.issaved = value; } }
        bool sne { get { return d.sne; } set { d.sne = value; } }
        bool ene { get { return d.ene; } set { d.ene = value; } }
        Encoding encode { get { return d.encode; } set { d.encode = value; } }
        int sn { get { return d.sn; } set { d.sn = value; } }
        int en { get { return d.en; } set { d.en = value; } }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
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

        /// <summary>
        /// Загрузка файла .CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        

        #region UI

        /// <summary>
        /// Отлов нажатий клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
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
                    CSVconv.SaveStrtoCSV("" + name, data, separ, rewrite, this.encode);
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

                    CSVconv.SaveStrtoCSV("" + name, data, separ, rewrite, this.encode/*,(char)ofd.EncodingType*//*,ofd.Rewrite*/);
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
                if (FBD.ShowDialog() == DialogResult.OK)
                {
                    separ = FBD.FilterIndex-1 == 0 ? ';' : ',';//FBD.FileName[FBD.FileName.Length - 1];
                    name = FBD.FileName;//.Remove(FBD.FileName.Length - 1);
                    CSVconv.SaveStrtoCSV("" + name, data, separ, rewrite, Encoding.Default);
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
                    CSVconv.SaveStrtoCSV("" + name, data, separ, rewrite, this.encode);
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
                    separ = FBD.FilterIndex-1 == 0 ? ',' : FBD.FilterIndex - 1 == 1 ? '\t' : FBD.FilterIndex - 1 == 2 ? ';' : FBD.FilterIndex - 1 == 3 ? '\t' : FBD.FilterIndex - 1 == 4 ? ';' : ',';//FBD.FileName[FBD.FileName.Length - 1];
                    name = FBD.FileName;//.Remove(FBD.FileName.Length - 1);
                    datas = CSVconv.fscanf(name, this.encode);
                    data=CSVconv.LoadCSVtoStr("" + name, separ, this.encode);
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
        }




        #endregion


        /// <summary>
        /// Новое окно фрактала
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (this.Frac != null && this.Frac.isdrawing) return;
            (new Form1()).ShowDialog(new Form1());
        }

        /// <summary>
        /// Add row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try {
                //dataGridView1.Rows.Add();
                dataGridView1.Rows.Insert(dataGridView1.SelectedRows[dataGridView1.SelectedRows.Count-1].Index);
            }
            catch(System.InvalidOperationException ex)
            {
                DropExWindow(ex.Message + "\nСоздайте колонку");
            }
        }

        /// <summary>
        /// Add column
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void columnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = "";
            var column = new DataGridViewColumn();
            column.HeaderText = str;
            column.Name = str;
            column.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView1.Columns.Add(column);
        }

        #endregion




        #region backend

        /// <summary>
        /// Обновление таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateGrid(object sender = null, EventArgs e = null)
        {
            sn = en = -1;
            int ssn=0;
            if (sne) { if (!int.TryParse(toolStripMenuItem3.Text, out ssn) || ssn < 1) {DropExWindow("Uncorrect format of value \"From\""); return; } }
            sn = ssn;
            if (ene) { if (!int.TryParse(toolStripMenuItem4.Text, out ssn) || ssn <= sn) {DropExWindow("Uncorrect format of value \"To\""); return; } }
            en = ssn;
            sn = Math.Max(sn,1);
            if (en <= sn) { en = data.Count; }
            if(data==null) { return; }
            int maxlen = data[0].Count;
            for(int i = 0; i < data.Count; ++i)
            {
                maxlen = Math.Max(maxlen, data[i].Count);
            }
            dataGridView1.Columns.Clear();
            for (int i = 0; i < maxlen; ++i)
            {
                string str = "";
                if (data[0].Count > i) { str = data[0][i]; }
                var column = new DataGridViewColumn();
                column.HeaderText = str;
                column.Name = str;
                column.CellTemplate = new DataGridViewTextBoxCell();
                dataGridView1.Columns.Add(column);
            }
            for (int j = sn; j < en; ++j) { 
                dataGridView1.Rows.Add(data[j].ToArray());
            }
            Invalidate();
        }


        /// <summary>
        /// Закрытие приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Form1Closed(object sender, EventArgs e)
        {
            
            
        }

        /// <summary>
        /// Хотите ли вы продолжить и потерять изменения?
        /// </summary>
        /// <param name="s">Предупреждающая строка</param>
        /// <returns>Закрыть (false) ОК (true)</returns>
        public static bool SaveorLose(bool f=false, string s="При продолжении несохранённые данные будут потеряны.\nВы точно хотите продолжить?\nДля отмены закройте жто окно.") { return f || MessageBox.Show(s) == DialogResult.OK; }


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
                UpdateGrid();
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


        private AhoCorasik FindSubstring(params string[] which) //string where, vector<pair<vector<int>, string>>
        {
            int max = 0;
            foreach(var i in which) { max = Math.Max(max, i.Length); }
            AhoCorasik act = new AhoCorasik(which,which.Length*max);


            return act;//.find(where);
        }

        private int findInString(string s, string[] w)
        {
            int i = 0;
            for(i = 0; i < w.Length; i++)
            {
                if (s == w[i]) return i;
            }
            return i;
        }

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

        private void findToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (datas == null) { return; }
            string[] ws=ssesepToolStripMenuItem.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            ws = deleteCopy(ws);
            string[] str = new string[ws.Length];
            for(int i = 0; i < ws.Length; ++i)
            {
                str[i] = ws[i] + ": \n\r";
            }
            int colnum=0;
            AhoCorasik act = FindSubstring(ws);
            foreach (var i in datas){
                vector<pair<vector<int>, string>> v = act.find(i);
                for(int j = 0; j<v.size();++j) { //ws
                    if (v[j].first.size() == 0) { continue; }
                    int n = findInString(v[j].second, ws);
                    str[n] += ""+colnum+": "+v[j].first.tostring(",")+"\n\r"; 
                }
                colnum++;
            }
            vector<string> outs = new vector<string>();
            outs.fill(str.ToList());
            //DropExWindow(outs.tostring("\n"));
            textBox1.Text=outs.tostring("\n\r");
        }

        
    }
}
