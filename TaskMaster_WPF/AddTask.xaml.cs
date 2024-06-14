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
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace TaskMaster_WPF
{
    /// <summary>
    /// Логика взаимодействия для AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        public string filePath = "Tasks.txt";
        public string task;
        private MainWindow _window;
        public AddTask(MainWindow window)
        {
            InitializeComponent();
            _window = window;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            task = $"{textBox1.Text}|{dateTimePicker2.Text}|{timePicker.Value.Value.ToShortTimeString()}";
            _window.listBox1.Items.Add(task);
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(task);
            }
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
