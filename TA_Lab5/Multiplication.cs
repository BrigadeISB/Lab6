using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TA_Lab5.Program;
using System.Threading;
using System.Reflection;


namespace TA_Lab5
{
    public partial class Multiplication : Form
    {
        public Multiplication()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        MyHashTable<string, int> hashTable = new MyHashTable<string, int>(new MultiplicationHashFunction());

        private void listBoxLoad()
        {
            hashTable.Add("Illia", 92);
            hashTable.Add( "Bodya", 95);
            hashTable.Add("Sofia", 99);
            hashTable.Add("Vasya", 90);
            hashTable.Add("Ivan", 91);
            RefreshBox1();
        }

        private void RefreshBox1()
        {
            listBox1.Items.Clear();
            foreach (var bucket in hashTable.items)
            {
                if (bucket != null)
                {
                    foreach (var pair in bucket)
                    {
                        listBox1.Items.Add($"Ім'я: {pair.Key}, Середній бал: {pair.Value}");
                    }
                }
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            var hash1 = new MultiplicationHashFunction();
            foreach (var bucket in hashTable.items)
            {
                if (bucket != null)
                {
                    foreach (var pair in bucket)
                    {
                        int result = hash1.ComputeHash(pair.Key);
                        listBox2.Items.Add($"Ім'я: {pair.Key}, Хеш-Код: {result}");
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBoxLoad();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int intVal = 0;
            if (int.TryParse(textBox2.Text, out intVal))
            {
                hashTable.Add(textBox1.Text, intVal);
                listBox1.Items.Clear();
                foreach (var bucket in hashTable.items)
                {
                    if (bucket != null)
                    {
                        foreach (var pair in bucket)
                        {
                            listBox1.Items.Add($"Ім'я: {pair.Key}, Середній бал: {pair.Value}");
                        }
                    }
                }
                RefreshBox1();
            }
            else
            {
                MessageBox.Show("Некоректне ціле число введено");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int intVal = 0;
            if (int.TryParse(textBox4.Text, out intVal))
            {
                hashTable.Remove(intVal, int.Parse(textBox3.Text));
                RefreshBox1();
                button1_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Некоректно введений хеш-код");
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(hashTable.Search(int.Parse(textBox5.Text)));
        }
    }
}
