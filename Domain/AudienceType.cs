using System.ComponentModel;

namespace Museum.Domain
{
    /// <summary>
    /// Тип аудитории мероприятия
    /// </summary>
    public enum AudienceType : byte
    {
        /// <summary>
        /// Взрослая аудитория
        /// </summary>
        [Description("Взрослая аудитория")]
        Adult,

        /// <summary>
        /// Дети
        /// </summary>
        [Description("Дети")]
        Children,

        /// <summary>
        /// Дети до 6 лет
        /// </summary>
        [Description("Дети до 6 лет")]
        ChildrenUnder6,

        /// <summary>
        /// Дети от 6 до 14 лет
        /// </summary>
        [Description("Дети от 6 до 14 лет")]
        Children6To14,

        /// <summary>
        /// Пушкинская карта (от 14 до 22 лет)
        /// </summary>
        [Description("Пушкинская карта от 14 до 22 лет")]
        PushkinCard14To22,

        /// <summary>
        /// Семейная аудитория
        /// </summary>
        [Description("Семейная аудитория")]
        Family
    }
}
