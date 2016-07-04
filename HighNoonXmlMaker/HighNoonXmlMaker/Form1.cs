using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace HighNoonXmlMaker
{
    public partial class form_Main : Form
    {
        /// <summary>
        /// 현재 선택한 row
        /// </summary>
        int selectedRowIndex;

        DataSet phase;
       
        public form_Main()
        {
            InitializeComponent();
            phase = new DataSet();
            phase.DataSetName = "Phase";
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Make_Click(object sender, EventArgs e)
        {
            // error check
            if(textBox_Phase.Text =="")
            {
                MessageBox.Show("Phase를 입력해주세요");
                return;
            }

            if(textBox_Stage.Text == "")
            {
                MessageBox.Show("Stage를 입력해주세요");
                return;
            }

            string level = string.Empty;
            if (comboBox_Level.Text == "EASY")
                level = "EZ";
            else if(comboBox_Level.Text == "NORMAL")
                level = "NM";
            else if (comboBox_Level.Text == "HARD")
                level = "HD";

            
            if (!Directory.Exists("Xml"))
                Directory.CreateDirectory("Xml");

            string fileName = string.Empty;
            fileName = "Xml/" + textBox_Stage.Text + "_" + textBox_Phase.Text + "_" + level + ".xml";

            //if (!File.Exists(fileName))
            //    File.Create(fileName);

            phase.WriteXml(fileName);

            MessageBox.Show("저장되었습니다.");
        }

        private void buttonAddRow_Click(object sender, EventArgs e)
        {
            phase.Tables[comboBox_Scene.SelectedIndex].Rows.Add();
        }

        /// <summary>
        /// selectedRowIndex 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (s.SelectedRows.Count > 0)
                selectedRowIndex = s.SelectedRows[0].Index;
        }

        private void MakeEnemyTable()
        {
            string tableName = "Step"+ (comboBox_Scene.Items.Count + 1).ToString();
            comboBox_Scene.Items.Add(tableName);

            DataTable step = new DataTable(tableName);
            DataColumn col = new DataColumn("Index");
            col.AutoIncrement = true;
            col.AutoIncrementSeed = 1;

            step.Columns.Add(col);
            step.Columns.Add(new DataColumn("Spawn"));
            step.Columns.Add(new DataColumn("Destination"));
            step.Columns.Add(new DataColumn("ActiveTime"));
            step.Columns.Add(new DataColumn("ActiveType"));
            step.Columns.Add(new DataColumn("AimTime"));
            step.Columns.Add(new DataColumn("ItemType"));

            phase.Tables.Add(step);
            comboBox_Scene.SelectedIndex = comboBox_Scene.Items.Count - 1;

        }

        private void 생성ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("기존에 작업하던 파일이 있을경우 저장을 해주십시오.\n 계속하시겠습니까?","저장유무 확인", MessageBoxButtons.YesNo) 
                == DialogResult.Yes)
                MakeEnemyTable();
        }

        private void 불러오기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(MessageBox.Show("기존에 작업하던 파일이 있을경우 저장을 해주십시오.\n 계속하시겠습니까?", "저장유무 확인", MessageBoxButtons.YesNo)
               == DialogResult.Yes) )
                return;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.DefaultExt = ".xml";

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                // 초기화
                comboBox_Scene.Items.Clear();
                phase.Clear();

                phase.ReadXml(dialog.FileName);
                textBox_Stage.Text = dialog.SafeFileName.Substring(0, 1);
                textBox_Phase.Text = dialog.SafeFileName.Substring(2, 1);

                string level = dialog.SafeFileName.Substring(4, 2);

                XmlDocument d = new XmlDocument();
                d.Load(dialog.FileName);
                
                if (level == "EZ")
                    comboBox_Level.Text = "EASY";
                else if (level == "NM")
                    comboBox_Level.Text = "NORMAL";
                else if (level == "HD")
                    comboBox_Level.Text = "HARD";

                for (int i=0; i<phase.Tables.Count; ++i)
                    comboBox_Scene.Items.Add((i + 1).ToString() + " Step");

                comboBox_Scene.SelectedIndex = 0;
            }
        }

        private void comboBox_Scene_SelectedIndexChanged(object sender, EventArgs e)
        {
            s.DataSource = phase.Tables[comboBox_Scene.SelectedIndex];
        }

        private void buttonDeleteStep_Click(object sender, EventArgs e)
        {
            phase.Tables.RemoveAt(comboBox_Scene.SelectedIndex);

            if(phase.Tables.Count > 0)
              comboBox_Scene.SelectedIndex = comboBox_Scene.Items.Count - 1;
        }

        private void buttonAddStep_Click(object sender, EventArgs e)
        {
            MakeEnemyTable();
        }
    }
}
