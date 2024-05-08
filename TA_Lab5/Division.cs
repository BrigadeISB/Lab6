using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TA_Lab5.Program;

namespace TA_Lab5
{
    public partial class Division : Form
    {
        public Division()
        {
            InitializeComponent();
        }

        MyHashTable<string, int> hashTable = new MyHashTable<string, int>(new MultiplicationHashFunction(), new DivisionHashFunction());

        private void listBoxLoad()
        {
            hashTable.StartAdd("Illia", 92);
            hashTable.StartAdd("Bodya", 95);
            hashTable.StartAdd("Sofia", 99);
            hashTable.StartAdd("Vasya", 90);
            hashTable.StartAdd("Ivan", 91);
            hashTable.StartAdd("Sania", 91);
            hashTable.StartAdd("Oleh", 91);
            hashTable.StartAdd("Petro", 91);
            hashTable.StartAdd("Gleb", 91);
            RefreshBox1();
        }

        private void RefreshBox1()
        {
            listBox1.Items.Clear();
            foreach (var pair in hashTable.items)
            {
                if (pair != null && pair.IsDeleted == false)
                {

                    listBox1.Items.Add($"Ім'я: {pair.Key}, Середній бал: {pair.Value}");

                }
            }
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            var hash = new DivisionHashFunction();
            foreach (var pair in hashTable.items)
            {
                if (pair != null && pair.IsDeleted == false)
                {

                    int result = hash.ComputeHash(pair.Key);
                    listBox2.Items.Add($"Ім'я: {pair.Key}, Хеш-Код: {result}");

                }
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            listBoxLoad();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            int intVal = 0;
            if (int.TryParse(textBox2.Text, out intVal))
            {
                hashTable.AddS2(textBox1.Text, intVal);
                listBox1.Items.Clear();
                foreach (var pair in hashTable.items)
                {
                    if (pair != null)
                    {

                        listBox1.Items.Add($"Ім'я: {pair.Key}, Середній бал: {pair.Value}");

                    }
                }
                RefreshBox1();
                button1_Click_1(sender, e);
            }
            else
            {
                MessageBox.Show("Некоректно задано параметри учня");
            }
        }

        public string Search(string key)
        {

            string message = "";



            foreach (var pair in hashTable.items)
            {
                if (pair != null && pair.Key == key)
                {

                    message += $"Ім'я: {pair.Key}, Середній бал: {pair.Value}\n";

                }
            }


            return message;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            bool boolVal = true;
            for (int i = 0; i < 10; i++)
            {
                if (textBox4.Text.ToString() == hashTable.items[i].Key)
                {
                    hashTable.items[i].MarkAsDeleted();
                    boolVal = false;
                    break;
                }
            }

            if (boolVal)
            {
                MessageBox.Show("Некоректно введений хеш-код");
            }
            else
            {
                RefreshBox1();
                button1_Click_1(sender, e);
            }

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(Search(textBox5.Text));
        }
    }
}
