using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace FotoShrinker
{
    public partial class Form1 : Form
    {
        private readonly string CommentString = "*";
        private string tasksFilePath = "";
        private bool Modifyed = false;
        private int runningTask = 0;

        private class SettingsData
        {
            public bool Enabled = true;
            public string InFolder;
            public string OutFolder;
            public int maxWidth;
            public int Quality;
            public string[] TagData = new string[0];
            public string[] TagResized = new string[0];
            public bool Rename;
        }

        private List<SettingsData> Settings = new List<SettingsData>();

        public Form1()
        {
            InitializeComponent();
        }

        private void LoadSettings(string ConfigPath)
        {
            Properties.Settings.Default.TaskFilePath = ConfigPath;
            Properties.Settings.Default.Save();

            Modifyed = false;
            if (File.Exists(ConfigPath))
            {
                statusPanel1.Text = ConfigPath;
                statusPanelModify.Text = "";
                Settings.Clear();
                var settingsArr = File.ReadAllLines(ConfigPath, Encoding.UTF8);
                gridTasks.Rows.Clear();
                int rowNum = 0;
                foreach (var s in settingsArr)
                {
                    if (!(s.IndexOf("//") == 0))
                    {
                        string[] pices = s.Split(';').Select(f => f.Trim().Replace("/&smicn", ";")).ToArray();

                        if (pices.Length == 6)
                        {
                            SettingsData setItem = new SettingsData();
                            setItem.Enabled = pices[0].IndexOf(CommentString) != 0;
                            setItem.InFolder = pices[0].Replace(CommentString, "");
                            setItem.OutFolder = pices[1];
                            setItem.maxWidth = Convert.ToInt32(pices[2]);
                            setItem.Quality = Convert.ToInt32(pices[3]);
                            setItem.TagData = pices[4].Split(',').Select(f => f.Trim().Replace("/&cmma", ",")).ToArray();
                            setItem.TagResized = pices[5].Split(',').Select(f => f.Trim().Replace("/&cmma", ",")).ToArray();

                            Settings.Add(setItem);

                            gridTasks.Rows.Add();

                            gridTasks[ColumnEnabled.Index, rowNum].Value = setItem.Enabled;
                            gridTasks[ColumnInFolder.Index, rowNum].Value = setItem.InFolder;
                            gridTasks[ColumnOutFolder.Index, rowNum].Value = setItem.OutFolder;
                            gridTasks[ColumnMaxWidth.Index, rowNum].Value = setItem.maxWidth;
                            gridTasks[ColumnQualety.Index, rowNum].Value = setItem.Quality;
                            gridTasks[ColumnTagAll.Index, rowNum].Value = "...";
                            gridTasks[ColumnTAGResized.Index, rowNum].Value = "...";
                            rowNum++;
                        }
                    }
                }
            }
        }

        private void SaveSettings(string ConfigPath)
        {
            var taskList = new List<string>();
            foreach (var i in Settings)
            {
                string taskString = (i.Enabled ? "" : CommentString) +
                i.InFolder.Replace(";", "/&smicn") + ";" +
                i.OutFolder.Replace(";", "/&smicn") + ";" +
                i.maxWidth.ToString().Replace(";", "/&smicn") + ";" +
                i.Quality.ToString() + ";" +
                string.Join(",", i.TagData.Where(f => !string.IsNullOrEmpty(f)).Select(f => f.Replace(";", "/&smicn").Replace("/&cmma", ","))) + ";" +
                string.Join(",", i.TagResized.Where(f => !string.IsNullOrEmpty(f)).Select(f => f.Replace(";", "/&smicn").Replace("/&cmma", ",")));

                taskList.Add(taskString);
            }
            Modifyed = false;
            statusPanelModify.Text = "";
            if (!File.Exists(ConfigPath))
            {
                ConfigPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Tasks.txt";
            }
            File.WriteAllLines(ConfigPath, taskList, Encoding.UTF8);
        }

        private class workerTask
        {
            public SettingsData parametrs { get; set; }
            public int threads;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            runningTask++;
            statusPanelTaskComplited.Text = runningTask.ToString();
            workerTask p = (workerTask)e.Argument;
            ShrinkerClass shrinker = new ShrinkerClass();
            shrinker.ShrinkFolder(p.parametrs.InFolder, p.parametrs.OutFolder, p.parametrs.maxWidth, p.parametrs.Quality, chWriteTag.Checked, p.parametrs.TagData, p.parametrs.TagResized, p.threads, p.parametrs.Rename);

            e.Result = true;
        }

        private void RunTask(SettingsData TaskData, int Threads)
        {
            statusPanelTaskComplited.Text = runningTask.ToString();
            workerTask p = new workerTask { threads = Threads, parametrs = TaskData };
            backgroundWorker1.RunWorkerAsync(p);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bool first = Properties.Settings.Default.FirstStart;
            if (first)
            {
                int cores = trbThreads.Value = Math.Min(Environment.ProcessorCount, 8);
                trbThreads.Value = cores;
                lbThreads.Text = cores.ToString();
            }
            else
            {
                trbThreads.Value = Properties.Settings.Default.Threads;
                lbThreads.Text = Properties.Settings.Default.Threads.ToString();
            }

            string configPath = Properties.Settings.Default.TaskFilePath;
            if (File.Exists(configPath))
            {
                tasksFilePath = configPath;
                LoadSettings(configPath);
            }
            else
            {
                configPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Tasks.txt";
                if (File.Exists(configPath))
                {
                    tasksFilePath = configPath;
                    LoadSettings(configPath);
                }
            }

            toolStripFields.DropDownItems.AddRange(Snippets.GetFields().Select(s => new ToolStripMenuItem(s, null, OnSnippetFieldClick)).ToArray());
        }

        private void OnSnippetFieldClick(object sender, EventArgs e)
        {
            gridTAGs[0, gridTAGs.CurrentRow.Index].Value = gridTAGs[0, gridTAGs.CurrentRow.Index].Value + ((ToolStripMenuItem)sender).Text;
            Modifyed = true;
            statusPanelModify.Text = "*";
        }

        private void gridTasks_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == ColumnTAGResized.Index || e.ColumnIndex == ColumnTagAll.Index)
            {
                if (!gridTAGs.Visible)
                {
                    gridTAGs.Location = new Point(e.Location.X + gridTasks.Location.X - 5, (e.Location.Y + gridTasks.Location.Y) + 5);
                    gridTAGs.Rows.Clear();
                    gridTAGs.Tag = e.ColumnIndex;
                    gridTAGs.Show();

                    if (Settings.Count > e.RowIndex)
                    {
                        if (e.ColumnIndex == ColumnTagAll.Index)
                            foreach (var s in Settings[e.RowIndex].TagData)
                            {
                                gridTAGs.Rows.Add(s);
                            }

                        if (e.ColumnIndex == ColumnTAGResized.Index)
                            foreach (var s in Settings[e.RowIndex].TagResized)
                            {
                                gridTAGs.Rows.Add(s);
                            }
                    }
                    gridTAGs.Focus();
                }
            }
            if (Settings.Count > e.RowIndex)
            {
                if (e.ColumnIndex == ColumnExecute.Index)
                {
                    if (MessageBox.Show("Запустить задание?", "Запустить...", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        RunTask(new SettingsData
                        {
                            InFolder = gridTasks.Rows[e.RowIndex].Cells[ColumnInFolder.Index].Value.ToString(),
                            OutFolder = Settings[e.RowIndex].OutFolder,
                            maxWidth = Settings[e.RowIndex].maxWidth,
                            Quality = Settings[e.RowIndex].Quality,
                            TagData = Settings[e.RowIndex].TagData,
                            TagResized = Settings[e.RowIndex].TagResized,
                            Rename = (bool)gridTasks.Rows[e.RowIndex].Cells[ColumnRename.Index].Value
                        }, trbThreads.Value);
                    }
                }
            }
        }

        private void gridTasks_Click(object sender, EventArgs e)
        {
            gridTAGs.Hide();
        }

        private void gridTasks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColumnInFolder.Index || e.ColumnIndex == ColumnOutFolder.Index)
            {
                if (gridTasks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    folderBrowserDialog1.SelectedPath = gridTasks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    string path = folderBrowserDialog1.SelectedPath;
                    gridTasks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = folderBrowserDialog1.SelectedPath;
                    if (path.LastIndexOf(Path.DirectorySeparatorChar) + 1 != path.Length)
                        gridTasks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = path + Path.DirectorySeparatorChar;
                    gridTasks_CellEndEdit(gridTasks, e);
                    gridTasks.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }

        private void gridTAGs_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                gridTAGs.CommitEdit(DataGridViewDataErrorContexts.Commit);
                gridTAGs.Hide();
            }
            if (e.KeyCode == Keys.Enter && gridTAGs.Rows.Count == gridTAGs.CurrentRow.Index + 1 && gridTAGs.CurrentRow.Cells[0].Value != null && !string.IsNullOrEmpty(gridTAGs.CurrentRow.Cells[0].Value.ToString()))
            {
                var val = gridTAGs.CurrentRow.Cells[0].Value.ToString();
                gridTAGs.CurrentRow.Cells[0].Value = "";
                gridTAGs.Rows.Add();
                gridTAGs.Rows[gridTAGs.Rows.Count - 2].Cells[0].Value = val;
            }
        }

        private void btSaveSettings_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Сохранить задания?", "Сохранить...", MessageBoxButtons.YesNo) == DialogResult.Yes)
                SaveSettings(tasksFilePath);
        }

        private void btRunAll_Click(object sender, EventArgs e)
        {
            foreach (var s in Settings.Where(f => f.Enabled))
            {
                RunTask(s, trbThreads.Value);
            }
        }

        private void gridTasks_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            var row = gridTasks.CurrentRow;
            row.Cells[ColumnEnabled.Index].Value = true;
            var taskItem = new SettingsData
            {
                Enabled = row.Cells[ColumnEnabled.Index].Value == null ? true : (bool)row.Cells[ColumnEnabled.Index].Value,
                InFolder = row.Cells[ColumnInFolder.Index].Value == null ? "" : row.Cells[ColumnInFolder.Index].Value.ToString(),
                OutFolder = row.Cells[ColumnOutFolder.Index].Value == null ? "" : row.Cells[ColumnOutFolder.Index].Value.ToString(),
                maxWidth = row.Cells[ColumnMaxWidth.Index].Value == null ? 2000 : Convert.ToInt32(row.Cells[ColumnMaxWidth.Index].Value),
                Quality = row.Cells[ColumnQualety.Index].Value == null ? 90 : Convert.ToInt32(row.Cells[ColumnQualety.Index].Value)
            };
            Settings.Add(taskItem);
            Modifyed = true;
            statusPanelModify.Text = "*";
        }

        private void gridTAGs_VisibleChanged(object sender, EventArgs e)
        {
            if (!gridTAGs.Visible && gridTasks.CurrentRow.Index < Settings.Count)
            {
                int ri = gridTasks.CurrentRow.Index;
                string[] tag = new string[0];
                for (int i = 0; i < gridTAGs.RowCount - 1; i++)
                {
                    tag = tag.Concat(new string[1] { gridTAGs.Rows[i].Cells[0].Value.ToString() }).ToArray();
                }
                if ((int)gridTAGs.Tag == ColumnTAGResized.Index)
                {
                    Settings[ri].TagResized = tag;
                }
                if ((int)gridTAGs.Tag == ColumnTagAll.Index)
                {
                    Settings[ri].TagData = tag;
                }
            }
        }

        private void gridTasks_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex != ColumnTagAll.Index) && (e.ColumnIndex != ColumnTAGResized.Index) && (e.ColumnIndex != ColumnEnabled.Index))
            {
                for (int i = 0; i < gridTasks.Rows.Count - 1; i++)
                {
                    if (Settings.Count > i)
                    {
                        Settings[i].Enabled = Convert.ToBoolean(gridTasks.Rows[i].Cells[ColumnEnabled.Index].Value);
                        Settings[i].InFolder = gridTasks.Rows[i].Cells[ColumnInFolder.Index].Value.ToString().Replace(";", "/&smicn");
                        Settings[i].OutFolder = gridTasks.Rows[i].Cells[ColumnOutFolder.Index].Value.ToString().Replace(";", "/&smicn");
                        Settings[i].maxWidth = Convert.ToInt32(gridTasks.Rows[i].Cells[ColumnMaxWidth.Index].Value);
                        Settings[i].Quality = Convert.ToInt32(gridTasks.Rows[i].Cells[ColumnQualety.Index].Value);
                        Modifyed = true;
                    }
                }
                statusPanelModify.Text = "*";
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileTask.ShowDialog() == DialogResult.OK)
            {
                tasksFilePath = openFileTask.FileName;
                LoadSettings(tasksFilePath);
            }
        }

        private void DeleteTask(int Index)
        {
            if (Settings.Count >= Index)
            {
                Settings.RemoveAt(Index);
                gridTasks.Rows.RemoveAt(Index);
                Modifyed = true;
                statusPanelModify.Text = "*";
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить задание?", "Удалить...", MessageBoxButtons.YesNo) == DialogResult.Yes)
                DeleteTask(gridTasks.CurrentRow.Index);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.FirstStart = false;
            Properties.Settings.Default.TaskFilePath = tasksFilePath;
            Properties.Settings.Default.Threads = trbThreads.Value;
            Properties.Settings.Default.Save();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            trbThreads.Value = Properties.Settings.Default.Threads;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить строку?", "Удалить...", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                gridTAGs.Rows.RemoveAt(gridTAGs.CurrentRow.Index);
                Modifyed = true;
                statusPanelModify.Text = "*";
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            runningTask--;
            statusPanelTaskComplited.Text = runningTask.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Modifyed && MessageBox.Show("Изменения не сохранены, сохранить?", "Сохранить...", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SaveSettings(tasksFilePath);
            }
        }

        private void trbThreads_Scroll(object sender, EventArgs e)
        {
            lbThreads.Text = trbThreads.Value.ToString();
        }
    }
}
