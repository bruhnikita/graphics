using System;
using System.Drawing;
using System.Windows.Forms;
using NCalc; // Убедитесь, что NCalc установлен

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private Point previousPoint;
        private double minY = double.MaxValue;
        private double maxY = double.MinValue;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Введите формулу");
                    return;
                }

                var rangeParts = textBox2.Text.Split(';');
                if (rangeParts.Length != 2 ||
                    !double.TryParse(rangeParts[0], out double minX) ||
                    !double.TryParse(rangeParts[1], out double maxX))
                {
                    MessageBox.Show("Введите допустимый диапазон (например: -10;10)");
                    return;
                }

                // Сбрасываем minY и maxY перед вычислениями
                minY = double.MaxValue;
                maxY = double.MinValue;

                EvaluateFormulas(minX, maxX);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void EvaluateFormulas(double minX, double maxX)
        {
            string[] formulas = textBox1.Text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            using (Graphics graphics = graphsPlace.CreateGraphics())
            {
                graphics.Clear(Color.White);
                DrawAxes(graphics, minX, maxX);

                foreach (var formula in formulas)
                {
                    string trimmedFormula = formula.Trim();
                    if (!string.IsNullOrEmpty(trimmedFormula))
                    {
                        DrawFormula(graphics, trimmedFormula, minX, maxX, GetNextColor(Array.IndexOf(formulas, trimmedFormula)));
                    }
                }
            }
        }

        private double EvaluateFormula(string formula, double x)
        {
            try
            {
                var expression = new NCalc.Expression(formula);
                expression.Parameters["x"] = x;

                var result = expression.Evaluate();

                if (result is double resultValue)
                {
                    Console.WriteLine($"x: {x}, result: {resultValue}"); // Логирование значений

                    if (double.IsInfinity(resultValue) || double.IsNaN(resultValue))
                    {
                        Console.WriteLine($"Недопустимый результат для x = {x}: {resultValue}");
                        return double.NaN;
                    }
                    return resultValue;
                }
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine($"Деление на ноль для x = {x} в формуле: {formula}");
            }
            catch (OverflowException)
            {
                Console.WriteLine($"Переполнение для x = {x} в формуле: {formula}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при вычислении для x = {x}: {ex.Message}");
            }
            return double.NaN; // Вернуть NaN в случае ошибки
        }

        private void DrawFormula(Graphics graphics, string formula, double minX, double maxX, Color color)
        {
            Pen pen = new Pen(color, 2);
            previousPoint = Point.Empty;

            for (double x = minX; x <= maxX; x += 0.1)
            {
                double y = EvaluateFormula(formula, x);

                // Проверка на NaN
                if (double.IsNaN(y))
                {
                    continue; // Пропускаем, если y - NaN
                }

                // Проверка на слишком большие значения
                if (y < -10000 || y > 10000) // Порог можно настроить
                {
                    continue; // Пропускаем слишком большие значения
                }

                // Обновление minY и maxY для корректной отрисовки
                if (y < minY) minY = y;
                if (y > maxY) maxY = y;

                int xPixel = (int)((x - minX) / (maxX - minX) * graphsPlace.Width);
                int yPixel = graphsPlace.Height - (int)((y - minY) / (maxY - minY) * graphsPlace.Height);

                if (x == minX)
                {
                    previousPoint = new Point(xPixel, yPixel);
                }
                else
                {
                    graphics.DrawLine(pen, previousPoint, new Point(xPixel, yPixel));
                    previousPoint = new Point(xPixel, yPixel);
                }
            }
        }

        private void DrawAxes(Graphics graphics, double minX, double maxX)
        {
            // Рисуем оси
            graphics.DrawLine(Pens.Black, 0, graphsPlace.Height / 2, graphsPlace.Width, graphsPlace.Height / 2); // Ось X
            graphics.DrawLine(Pens.Black, graphsPlace.Width / 2, 0, graphsPlace.Width / 2, graphsPlace.Height); // Ось Y
        }

        private Color GetNextColor(int index)
        {
            Color[] colors = { Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Purple, Color.Orange, Color.Pink };
            return colors[index % colors.Length];
        }
    }
}
