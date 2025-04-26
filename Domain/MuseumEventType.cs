using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Domain
{
    public enum MuseumEventType
    {
        [Description("Акция")]
        Promotion,

        [Description("Выставка")]
        Exhibition,

        [Description("Квест")]
        Quest,

        [Description("Лекция")]
        Lecture,

        [Description("Мастер-класс")]
        MasterClass,

        [Description("Музейное занятие")]
        MuseumActivity,

        [Description("Экскурсия")]
        Tour
    }
}
