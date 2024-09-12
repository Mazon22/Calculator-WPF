using System;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        private string _input = string.Empty;                                                                                                   
        private string _operand1 = string.Empty;                                                                                               
        private string _operand2 = string.Empty;                                                                                        
        private char _operation;                                                                                                           
        private bool _isOperationPerformed = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод обработки нажатия цифровых кнопок.
        /// </summary>
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            // Если ранее была выполнена операция, очищаем поле.
            if (_isOperationPerformed)
            {
                _input = string.Empty;
                _isOperationPerformed = false;
            }

            // Обрабатываем ввод числа.
            _input += button.Content.ToString();
            InputTextBox.Text = _input;
        }

        /// <summary>
        /// Метод обработки нажатия кнопки запятой.
        /// </summary>
        private void BtnComma_Click(object sender, RoutedEventArgs e)
        {
            if (!_input.Contains(","))
            {
                _input += ",";
                InputTextBox.Text = _input;
            }
        }

        /// <summary>
        /// Метод обработки нажатия кнопок операций (+, -, *, /).
        /// </summary>
        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (!string.IsNullOrEmpty(_input))
            {
                _operand1 = _input;
                _operation = Convert.ToChar(button.Content);
                _isOperationPerformed = true;
            }
        }

        /// <summary>
        /// Метод обработки нажатия кнопки равно (=).
        /// </summary>
        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_operand1) && !string.IsNullOrEmpty(_input))
            {
                _operand2 = _input;

                try
                {
                    double num1 = Convert.ToDouble(_operand1);
                    double num2 = Convert.ToDouble(_operand2);
                    double result = 0;

                    // Выполняем арифметическую операцию.
                    switch (_operation)
                    {
                        case '+':
                            result = num1 + num2;
                            break;
                        case '-':
                            result = num1 - num2;
                            break;
                        case '*':
                            result = num1 * num2;
                            break;
                        case '/':
                            // Обрабатываем деление на 0.
                            if (num2 != 0)
                                result = num1 / num2;
                            else
                                throw new DivideByZeroException("Деление на 0 невозможно");
                            break;
                    }

                    // Отображаем результат.
                    InputTextBox.Text = result.ToString();
                    _input = result.ToString();
                }
                catch (DivideByZeroException ex)
                {
                    MessageBox.Show(ex.Message);
                    InputTextBox.Text = "Ошибка";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка ввода: " + ex.Message);
                }
                finally
                {
                    _operand1 = string.Empty;
                    _operand2 = string.Empty;
                }
            }
        }

        /// <summary>
        /// Метод обработки нажатия кнопки сброса (C).
        /// </summary>
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            _input = string.Empty;
            _operand1 = string.Empty;
            _operand2 = string.Empty;
            InputTextBox.Text = string.Empty;
            _isOperationPerformed = false;
        }
    }
}