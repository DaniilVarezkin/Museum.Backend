using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Domain
{
    /// <summary>
    /// Тип музейного мероприятия
    /// </summary>
    public enum MuseumEventType
    {
        /// <summary>
        /// Акция
        /// </summary>
        Promotion,

        /// <summary>
        /// Выставка
        /// </summary>
        Exhibition,

        /// <summary>
        /// Квест
        /// </summary>
        Quest,

        /// <summary>
        /// Лекция
        /// </summary>
        Lecture,

        /// <summary>
        /// Мастер-класс
        /// </summary>
        MasterClass,

        /// <summary>
        /// Музейное занятие
        /// </summary>
        MuseumActivity,

        /// <summary>
        /// Экскурсия
        /// </summary>
        Tour
    }
}
