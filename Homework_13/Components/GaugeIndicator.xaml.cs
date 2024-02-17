using System;
using System.ComponentModel;
using System.Windows;

namespace Homework_13.Components
{    
    /// <summary>
    /// Логика взаимодействия для GaugeIndicator.xaml
    /// </summary>
    public partial class GaugeIndicator 
    {
        #region ValueProperty
        /// <summary>
        /// статическе свойство для регистрации свойства зависимости
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),          //имя свойства зависимости (можно было просто указать "Name",
                                        //но здесь использована конструкция nameof(Value), позволяющее рефакторинг
                typeof(double),         //тип свойства зависимости
                typeof(GaugeIndicator), //тип, которому принадлежит свойство
                                        //на этом можно ограничиться, но можно также сделать доп. настройки:
                new PropertyMetadata(   //
                    default(double),    //значение свойства по умолчанию
                    OnValuePropretyChanged,
                    OnCoerceValue),
                OnValidateValue
                );

        /// <summary>
        /// необязательный метод, который будет вызываться всякий раз, когда свойство меняется
        /// </summary>
        /// <param name="d">Объект, для которого изменяется свойство</param>
        /// <param name="e">Объект, содержащий информацию о том, как это свойство изменяется</param>
        private static void OnValuePropretyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }

        /// <summary>
        /// Необязательный метод, позволяющий скорректировать значение свойства
        /// </summary>
        /// <param name="d">Объект, для которого изменяется свойство</param>
        /// <param name="baseValue">Установленное значение свойства</param>
        /// <returns>Скорректированное значение свойства</returns>
        private static object OnCoerceValue(DependencyObject d, object baseValue)
        {
            var value = (double)baseValue;
            return Math.Max(0, Math.Min(100, value));            
        }

        /// <summary>
        /// Необязательный метод, получающий новое значение и возвращающий истину либо лож,
        /// если метод возвращает лож, то привязка не срабатывает, если возвращает истину, то новое значение
        /// становится значением этого свойства
        /// </summary>
        /// <param name="value">Новое установленное значение</param>
        /// <returns></returns>
        private static bool OnValidateValue(object value)
        {
            return true;            
        }    

        /// <summary>
        /// 
        /// </summary>
        [Category("Моя категория")]
        [Description("Угол поворота стрелки")]
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        #endregion

        public GaugeIndicator()
        {
            InitializeComponent();

        }       

    }
}
