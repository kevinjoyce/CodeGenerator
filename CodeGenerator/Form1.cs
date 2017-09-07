using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections;

namespace CodeGenerator
{
    public partial class Form1 : Form
    {
        SQLiteConnection conn;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            DBOperator dbo = new DBOperator();
            conn = dbo.getConnection();
            fillLevel12();
        }

        void fillLevel12()
        {
            ArrayList mylist = new ArrayList();
            
            string sql = "select * from level2 where level1_id = 1";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                mylist.Add(new DictionaryEntry(reader["id2"], reader["name"]));

            this.comboBox2.DataSource = mylist;
            this.comboBox2.DisplayMember = "Value";
            this.comboBox2.ValueMember = "Key";
            this.comboBox2.Text="";
        }

        private void watermarkTextBox2_TextChanged(object sender, EventArgs e)
        {
            this.textBox2.Text = "";
            this.textBox2.Text = "1." + this.textBox2.Text + this.comboBox2.SelectedValue + "."
                + this.comboBox3.SelectedValue + "."
                + this.comboBox1.SelectedValue + "."
                + this.watermarkTextBox2.Text;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArrayList mylist = new ArrayList();
            if (this.comboBox2.SelectedValue.ToString().Length > 5) return;
            string sql = "select * from level3 where level1_id = 1 and level2_id = " + this.comboBox2.SelectedValue;
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                mylist.Add(new DictionaryEntry(reader["id3"], reader["name"]));

            this.comboBox3.DataSource = mylist;
            this.comboBox3.DisplayMember = "Value";
            this.comboBox3.ValueMember = "Key";
            this.comboBox3.Text = "";
            this.textBox2.Text = "";
            this.textBox2.Text = "1." + this.textBox2.Text + this.comboBox2.SelectedValue + ".";
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArrayList mylist = new ArrayList();
            if (this.comboBox3.SelectedValue.ToString().Length > 5) return;
            string sql = "select * from level4 where level2_id = 1 and level3_id = " + this.comboBox2.SelectedValue + " and name = " + this.comboBox3.SelectedValue;
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                mylist.Add(new DictionaryEntry(reader["id4"], reader["name2"]));

            this.comboBox1.DataSource = mylist;
            this.comboBox1.DisplayMember = "Value";
            this.comboBox1.ValueMember = "Key";
            this.comboBox1.Text = "";

            this.textBox2.Text = "";
            this.textBox2.Text = "1." + this.textBox2.Text + this.comboBox2.SelectedValue + "." + this.comboBox3.SelectedValue + ".";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox2.Text = "";
            this.textBox2.Text = "1." + this.textBox2.Text + this.comboBox2.SelectedValue + "."
                + this.comboBox3.SelectedValue + "."
                + this.comboBox1.SelectedValue + ".";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(textBox2.Text);
            MessageBox.Show("编码已复制。");
        }

        private void watermarkTextBox3_TextChanged(object sender, EventArgs e)
        {
            this.textBox2.Text = "";
            this.textBox2.Text = "1." + this.textBox2.Text + this.comboBox2.SelectedValue + "."
                + this.comboBox3.SelectedValue + "."
                + this.comboBox1.SelectedValue + "."
                + this.watermarkTextBox2.Text + "."
                + this.watermarkTextBox3.Text;
        }

        private void watermarkTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.textBox2.Text = "";
            this.textBox2.Text = "1." + this.textBox2.Text + this.comboBox2.SelectedValue + "."
                + this.comboBox3.SelectedValue + "."
                + this.comboBox1.SelectedValue + "."
                + this.watermarkTextBox2.Text + "."
                + this.watermarkTextBox3.Text + "."
                + this.watermarkTextBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear_hints();

            string[] code = textBox2.Text.Split('.');
            if (code.Length != 7)
            {
                MessageBox.Show("代码不完整，此页应有7个参数。请重新输入，或用0代替不确定的参数。");
                return;
            }

            seek_level1(code[0]);
            seek_level2(code[0], code[1]);
            seek_level3(code[0], code[1], code[2]);
            seek_level4(code[0], code[1], code[2], code[3]);
            seek_standard(code[4]);
            seek_size1(code[5]);
            seek_size2(code[6]);
        }

        private void clear_hints()
        {
            label78.Text = "";
            label79.Text = "";
            label80.Text = "";
            label81.Text = "";
            label82.Text = "";
            label83.Text = "";
            label84.Text = "";
        }

        private void seek_level1(string id)
        {
            string sql = "select * from level1 where id = " + id;
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                label78.Text = "大类："+reader["name"].ToString();
        }

        private void seek_level2(string id1, string id2)
        {
            string sql = "select * from level2 where level1_id = " + id1 + " and  id2 = " + id2;
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                label79.Text = "子类A："+reader["name"].ToString();
        }

        private void seek_level3(string id1, string id2, string id3)
        {
            string sql = "select * from level3 where level1_id = " + id1 + " and  level2_id = " + id2 + " and id3=" + id3;
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                label80.Text = "子类B：" + reader["name"].ToString();
        }

        private void seek_level4(string id1, string id2, string id3, string id4)
        {
            string sql = "select * from level4 where name = " + id3 + " and  level3_id = " + id2 + " and id4=" + id4;
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                label81.Text = "子类C：" + reader["name2"].ToString();
        }

        private void seek_standard(string standard)
        {
            label82.Text = "标准：" + standard;
        }

        private void seek_size1(string ssize)
        {
            label83.Text = "厚度："+ssize;
        }

        private void seek_size2(string ssize)
        {
            label84.Text = "尺寸"+ssize;
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            this.textBox2.Text = "";
            this.textBox2.Text = "1." + this.textBox2.Text + this.comboBox2.SelectedValue + "."
                + this.comboBox3.SelectedValue + "."
                + this.comboBox1.SelectedValue + ".";
        }
    }
}
