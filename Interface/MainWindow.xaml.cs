using System;
using System.Windows;
using System.Windows.Controls;
using CalcLibrary;

namespace Interface
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Добавляем обработчик для всех кнопок на гриде
            foreach (UIElement c in LayoutRoot.Children)
            {
                if (c is Button)
                {
                    ((Button)c).Click += Button_Click;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBlock.Text == "0") textBlock.Text = "";
            string s = (string)((Button)e.OriginalSource).Content; // Получаем текст кнопки
            textBlock.Text += s; // Добавляем его в текстовое поле
            string text = textBlock.Text;

            if (s == "=") textBlock.Text = Calc.DoOperation(textBlock.Text); // Если равно, то выводим результат операции
            else if (s == "CLEAR") textBlock.Text = "0"; // Очищаем поле
            else if (s == "C") // Стираем один символ
            {
                text = text.Remove(text.Length - 1);
                if (text.Length > 0) text = text.Remove(text.Length - 1); // Один символ для буквы C, второй символ для последнего введенного символа
                if (text.Length == 0) text = "0"; //если после стирания символа стало пусто, выводится 0
                textBlock.Text = text; // Обновляем поле
            }
            else if (s == "+/-") //смена знака применяется ко всему выражению
            {
                text = text.Remove(text.Length - 3);
                if (text == "") //при вводе отрицательного числа
                {
                    text += "-";
                    textBlock.Text = text;
                }
                else //уже введено какое-то число
                {
                    string number = text;
                    if (number[0] != '-') text = "-" + number;
                    else text = number.Trim(new char[] { '-' });
                    textBlock.Text = text;
                }
            }
            else if (s == "e")
            {
                text = text.Remove(text.Length - 1);
                text += Math.Round(Math.E, 4);
                textBlock.Text = text;
            }
            else if (s == "pi")
            {
                text = text.Remove(text.Length - 2);
                text += Math.Round(Math.PI, 4);
                textBlock.Text = text;
            }
        }
    }
}
