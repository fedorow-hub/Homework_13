using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Homework_13.ViewModels.Base
{
    public abstract class ViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>
        /// Метод для обновления значения свойства, для которого определено поле, в котором это свойство хранит свои данные
        /// (решает проблему кольцевого изменения значений свойств)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">ссылка на поле свойства</param>
        /// <param name="value">новое значение, которое мы хотим установить</param>
        /// <param name="prop">определяется компилятором самостоятельно</param>
        /// <returns></returns>
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string prop = null)
        {
            if (Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(prop);
            return true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private bool _Disposed;

        protected virtual void Dispose(bool Disposing)
        {
            if (!Disposing || _Disposed) return;
            _Disposed = true;
            //освобождение управляемых ресурсов
        }
    }
}
