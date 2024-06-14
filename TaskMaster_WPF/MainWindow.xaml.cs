using Microsoft.SqlServer.Server;
using Syncfusion.Windows.Controls.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace TaskMaster_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public string filePath = "Tasks.txt";
        public MainWindow()
        {
            InitializeComponent();
            FileRead();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var addItemWindow = new AddTask(this);
            addItemWindow.ShowDialog();
        }
        private void FileRead()
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            using (StreamReader sr = new StreamReader(filePath, true))
            {
                while (!sr.EndOfStream)
                {
                    listBox1.Items.Add(sr.ReadLine());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            { 
            DeleteStringInFile(listBox1.SelectedItem.ToString());
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
            catch { }
        }
        private void DeleteStringInFile(string stringToRemove)
        {
            string tempFile = System.IO.Path.GetTempFileName(); // Создание временного файла

            try
            {
                using (var sr = new StreamReader(filePath))
                using (var sw = new StreamWriter(tempFile))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line != stringToRemove)
                        {
                            sw.WriteLine(line);
                        }
                    }
                }

                // Замена оригинального файла на обновлённый временный файл
                File.Delete(filePath);
                File.Move(tempFile, filePath);

                Console.WriteLine("Строка успешно удалена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
            finally
            {
                // Удаление временного файла
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
        }
        private void SortListBoxTime()
        {

            List<string> sortedItems = listBox1.Items.Cast<string>()
                .OrderBy(item => DateTime.ParseExact(item.Split('|')[1] + item.Split('|')[2], "dd.MM.yyyyHH:mm", null))
                .ToList();

            // Очищаем ListBox и добавляем отсортированные записи
            listBox1.Items.Clear();
            foreach (var item in sortedItems)
            {
                listBox1.Items.Add(item);
            }
        }
        private void SortListBoxLetter()
        {

            List<string> sortedItems = listBox1.Items.Cast<string>()
                .OrderBy(item => item.Split('|')[0])
                .ToList();

            // Очищаем ListBox и добавляем отсортированные записи
            listBox1.Items.Clear();
            foreach (var item in sortedItems)
            {
                listBox1.Items.Add(item);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SortListBoxTime();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SortListBoxLetter();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                listBox1.Items.Clear();
                FileRead();
            }
            else if (comboBox1.SelectedIndex == 1) { SortListBoxTime(); }
            else if (comboBox1.SelectedIndex == 2) { SortListBoxLetter(); }
        }
    }
}
