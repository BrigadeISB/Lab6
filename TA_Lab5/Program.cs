using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Xml.Linq;
using System.Security.Policy;


namespace TA_Lab5
{
    internal static class Program
    {
        public class CustomKeyValuePair<TKey, TValue>
        {
            public TKey Key { get; }
            public TValue Value { get; }
            public bool IsDeleted { get; private set; }

            public CustomKeyValuePair(TKey key, TValue value)
            {
                Key = key;
                Value = value;
                IsDeleted = false; // По замовчуванню IsDeleted має значення false
            }

            // Метод для встановлення значення IsDeleted на true
            public void MarkAsDeleted()
            {
                IsDeleted = true;
            }
        }
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public interface IHashFunction<T>
        {
            int ComputeHash(T key);
        }

        public abstract class HashTable<TKey, TValue>
        {
            protected const int DefaultCapacity = 10; 
            public List<CustomKeyValuePair<TKey, TValue>> items;

            public HashTable()
            {
                items = new List<CustomKeyValuePair<TKey, TValue>>();
                for (int i = 0; i < DefaultCapacity; i++)
                {
                    var temp = default(CustomKeyValuePair<TKey, TValue>);
                    items.Add(temp);
                }
                
            }

            public abstract void AddS(TKey key, TValue value);

            public abstract void Remove(int key, TValue value);

            

            public abstract bool TryGetValue(TKey key, out TValue value);
            public abstract void Clear();

            public abstract int Count { get; }
            
        }

        public class MyHashTable<TKey, TValue> : HashTable<TKey, TValue>
        {
            private const double LoadFactor = 0.75;
            private int count;
            private readonly IHashFunction<TKey> hashFunction;
            private readonly IHashFunction<TKey> hashFunction2;

            public MyHashTable(IHashFunction<TKey> hashFunction) : base()
            {
                this.hashFunction = hashFunction;
                
            }

            public MyHashTable(IHashFunction<TKey> hashFunction, IHashFunction<TKey> hashFunction2) : base()
            {
                this.hashFunction = hashFunction;
                this.hashFunction2 = hashFunction2;
            }

            private int ComputeHash(TKey key)
            {
                return Math.Abs(hashFunction.ComputeHash(key) % 10);
            }

            private int ComputeHash2(TKey key)
            {
                return Math.Abs(hashFunction2.ComputeHash(key) % 10);
            }

            //Лінійне зондування
            public bool TryLinearSensing(int hash, TKey key, TValue value)
            {
                
                int k = 9;
                for (int i = 0; i < DefaultCapacity; i++)
                {
                    int ind = (hash + i * k) % DefaultCapacity;

                    if (items[ind] == null || items[ind].Equals(default(CustomKeyValuePair<TKey, TValue>)) || items[ind].IsDeleted == true)
                    {
                        items[ind] = new CustomKeyValuePair<TKey, TValue>(key, value);
                        return true;
                    }
                }
                return false;
            }
            //
            public bool TryDoubleHanging(int hash, int hash2, TKey key, TValue value)
            {

                for (int i = 0; i < DefaultCapacity; i++)
                {
                    int ind = (hash + i * hash2) % DefaultCapacity;

                    if (items[ind] == null || items[ind].Equals(default(CustomKeyValuePair<TKey, TValue>)) || items[ind].IsDeleted == true)
                    {
                        items[ind] = new CustomKeyValuePair<TKey, TValue>(key, value);
                        return true;
                    }
                }
                return false;
            }

            public override void AddS(TKey key, TValue value)
            {
                int hash = ComputeHash(key);
                if(!TryLinearSensing(hash, key, value))
                {
                    MessageBox.Show($"Студент {key} не був доданий. Таблиця повна!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Студент {key} успішно доданий!", "Успішно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

            public void AddS2(TKey key, TValue value)
            {
                int hash1 = ComputeHash(key);
                int hash2 = ComputeHash2(key);
                if (!TryDoubleHanging(hash1, hash2, key, value))
                {
                    MessageBox.Show($"Студент {key} не був доданий. Таблиця повна!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Студент {key} успішно доданий!", "Успішно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

            public void StartAdd(TKey key, TValue value)
            {
                int hash = ComputeHash(key);
                TryLinearSensing(hash, key, value);

            }

            public void StartAdd2(TKey key, TValue value)
            {
                int hash1 = ComputeHash(key);
                int hash2 = ComputeHash2(key);
                TryDoubleHanging(hash1, hash2, key, value);

            }

            public override void Remove(int key, TValue value)
            {
                
                //int index = key;

                //if (items[index] != null)
                //{
                //    if (items[index].Count > 1)
                //    {
                //        int counter = -1;
                //        foreach (var pair in items[index])
                //        {
                //            if(pair.Value.Equals(value))
                //            {
                //                counter++;
                //            }
                //        }
                //        items[index].RemoveAt(counter);
                //    }
                //    else
                //    {
                //        items[index].RemoveAt(0);
                //    }

                //    count--;
                //}

                
            }


            //public int ComputeHash2(string key)
            //{
            //    double code = 0;
            //    const int M = 51;

            //    foreach (char c in key)
            //    {
            //        code += (int)c;
            //    }



            //    double hashValue = code % M;

            //    return (int)hashValue;
            //}



            public override bool TryGetValue(TKey key, out TValue value)
            {
                //int index = ComputeHash(key);

                //if (items[index] != null)
                //{
                //    foreach (var pair in items[index])
                //    {
                //        if (pair.Key.Equals(key))
                //        {
                //            value = pair.Value;
                //            return true;
                //        }
                //    }
                //}

                value = default(TValue);
                return false;
            }

            public override void Clear()
            {
                //Array.Clear(items, 0, items.Length);
                //count = 0;
            }

            public override int Count => count;

            
        }

        public class MultiplicationHashFunction : IHashFunction<string>
        {
            public int ComputeHash(string key)
            {
                const double C = 0.6180339887; 
                const int M = 50;

                double hashValue = M * ((C * GetStringCode(key)) % 1);

                return (int)hashValue;
            }

            private static double GetStringCode(string key)
            {
                double code = 0;

                foreach (char c in key)
                {
                    code += (int)c; 
                }

                return code;
            }
        }

        public class DivisionHashFunction : IHashFunction<string>
        {
            public int ComputeHash(string key)
            {
                
                const int M = 50;

                double hashValue = GetStringCode(key) % M;

                return (int)hashValue;
            }

            private static double GetStringCode(string key)
            {
                double code = 0;

                foreach (char c in key)
                {
                    code += (int)c;
                }

                return code;
            }
        }

    }
}
