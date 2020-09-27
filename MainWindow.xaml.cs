using System;
using System.Windows;

namespace Test1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        MyData myData;

        public MainWindow()
        {
            InitializeComponent();
            myData = new MyData("https://jsonplaceholder.typicode.com/posts");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (myData.Read() == 1)
            {
                MessageBox.Show("Произошла ошибка при обращении к сайту.\n" +
                    "Обратитесь к системному администратору.", "Ошибка");
            }
            else
            {
                myListBox.ItemsSource = myData.book;
                userListBox.ItemsSource = myData.userId;
                if (myData.userId.Count != 0)
                {
                    userListBox.SelectedItem = userListBox.Items[0];
                }
             }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (myData.userId.Count != 0)
            {

                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                ExcelApp.Application.Workbooks.Add(Type.Missing);
                ExcelApp.Columns.ColumnWidth = 75;

                int i = 1;
                foreach (Record element in myData.book)
                {
                    if (element.userId == myData.userId[userListBox.SelectedIndex])
                    {
                        ExcelApp.Cells[i, 1] = element.title;
                        ExcelApp.Cells[i + 1, 1] = element.body;

                        ExcelApp.Range["A" + i.ToString(), "A" + i.ToString()].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        ExcelApp.Range["A" + i.ToString(), "A" + (i + 1).ToString()].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                        i = i + 3;
                    }
                }
                ExcelApp.Visible = true;
            }
        }
    }
}
