using System;

namespace WorldYachtsDesktopApp.Services
{
    /// <summary>
    /// Определяет метод для временной блокировки цели.
    /// </summary>
    /// <typeparam name="TBlockTarget">Цель блокировки.</typeparam>
    public interface ITimeoutBlocker<TBlockTarget>
    {
        /// <summary>
        /// Блокирует цель.
        /// </summary>
        /// <param name="target">Цель блокировки.</param>
        /// <param name="timeout">Время до разблокирования.</param>
        void Block(TBlockTarget target, TimeSpan timeout);
    }
}
