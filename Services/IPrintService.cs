namespace WorldYachtsDesktopApp.Services
{
    /// <summary>
    /// Определяет метод для печати.
    /// </summary>
    /// <typeparam name="TPrintTarget">Цель печати.</typeparam>
    public interface IPrintService<TPrintTarget>
    {
        /// <summary>
        /// Распечатывает цель по заданному пути.
        /// </summary>
        /// <param name="target">Цель распечатки.</param>
        /// <param name="description">Описание распечатки.</param>
        /// <param name="path">Путь распечатанного файла.</param>
        /// <returns><see langword="true"/>, если печать успешна, 
        /// иначе <see langword="false"/>.</returns>
        bool Print(TPrintTarget target, string description);
    }
}
