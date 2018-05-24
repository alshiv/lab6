using System;
using System.Collections.Generic;
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

namespace minesweeper
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BitmapImage mine = new BitmapImage(new Uri(@"pack://application:,,,/img/x.jpg", UriKind.Absolute));
        pole pl = new pole();
        int score;

        public MainWindow()
        {
            InitializeComponent();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            pole.Children.Clear();
            pl.init( int.Parse(tb1.Text), int.Parse(tb2.Text) );
            pl.plant_mines(int.Parse(tb3.Text));
            pl.calculate();
            pole.IsEnabled = true;

            //указыается количество строк и столбцов в сетке
            pole.Rows = int.Parse(tb1.Text);
            pole.Columns = int.Parse(tb2.Text);
            //указываются размеры сетки (число ячеек * (размер кнопки в ячейки + толщина её границ))
            pole.Width = int.Parse(tb2.Text) * (50 + 4);
            pole.Height = int.Parse(tb1.Text) * (50 + 4);
            //толщина границ сетки
            pole.Margin = new Thickness(3);

            for (int i = 0; i < int.Parse(tb1.Text) * int.Parse(tb2.Text); i++)
            {
                //создание кнопки
                Button btn = new Button();
                //запись номера кнопки
                btn.Tag = i;
                //установка размеров кнопки
                btn.Width = 50;
                btn.Height = 50;
                //текст на кнопке
                btn.Content = " ";
                //толщина границ кнопки
                btn.Margin = new Thickness(2);
                //при нажатии кнопки, будет вызываться метод Btn_Click
                btn.Click += Btn_Click; ;
                //добавление кнопки в сетку
                pole.Children.Add(btn);
            }

            this.Width = 6 * 70;
            this.Height = 7 * 70;

        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            ////получение значения лежащего в Tag
            int n = (int)((Button)sender).Tag;
            ////установка фона нажатой кнопки, цвета и размера шрифта
            //((Button)sender).Background = Brushes.White;
            //((Button)sender).Foreground = Brushes.Red;
            //((Button)sender).FontSize = 23;
            ////запись в нажатую кнопку её номера
            //((Button)sender).Content = (n%5).ToString() + " " + (n/5).ToString();



            if (pl.getCell(n % int.Parse(tb2.Text) , n / int.Parse(tb1.Text)) == 9)
            {
                Button[] btns = new Button[pole.Children.Count];
                pole.Children.CopyTo(btns, 0);

                for (int i = 0; i < pole.Children.Count; i++)
                {
                    if (pl.getCell(i % int.Parse(tb2.Text), i / int.Parse(tb1.Text)) == 9)
                    {
                        //создание и инициализация переменной для хранения изображения мины
                        Image img = new Image();
                        //загрузка изображения mine.jpg из папки imgs
                        img.Source = mine;

                        //создание глобальной переменной для отображения изображения мины
                        StackPanel minePnl;

                        //инициализация и установка ориентации, можно вызвать в методе инициализации формы
                        minePnl = new StackPanel();
                        // minePnl.Orientation = Orientation.Horizontal;
                        //установка толщины границы объекта
                        minePnl.Margin = new Thickness(1);
                        //добавление в объект изображения
                        minePnl.Children.Add(img);
                        btns[i].Content = minePnl;
                    }
                }
                MessageBox.Show("YOU SUCK!");
                pole.IsEnabled = false;
                score = 0;
            }
            else
            {
                ////установка фона нажатой кнопки, цвета и размера шрифта
                ((Button)sender).Background = Brushes.White;
                ((Button)sender).Foreground = Brushes.Red;
                ((Button)sender).FontSize = 23;
                ///запись в нажатую кнопку её номера
                ((Button)sender).Content = pl.getCell(n % int.Parse(tb2.Text), n / int.Parse(tb1.Text));
                score++;
                if (score == int.Parse(tb2.Text) * int.Parse(tb1.Text) - int.Parse(tb3.Text))
                {
                    MessageBox.Show("YOU WIN!");
                    pole.IsEnabled = false;
                }
            }
        }

//        private void Load_Click(object sender, RoutedEventArgs e)
//        {

//        }

//        private void Save_Click(object sender, RoutedEventArgs e)
//        {
//                //создание диалога
//                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
//                //настройка параметров диалога
//                dlg.FileName = "Document"; // Default file name
//                dlg.DefaultExt = ".txt"; // Default file extension
//                dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension
//                //вызов диалога
//                dlg.ShowDialog();
//                //получение выбранного имени файла
//                //открытие файла test.txt для записи
//                using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\test.txt"))
//                {
//                    //lines – массив строк
//                    foreach (string line in lines)
//                        outputFile.WriteLine(line);
//                }
//        }
//    }
//}
