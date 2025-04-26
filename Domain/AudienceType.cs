using System.ComponentModel;

namespace Museum.Domain
{
    public enum AudienceType : byte
    {
        [Description("Взрослая аудитория")]
        Adult,

        [Description("Дети")]
        Children,

        [Description("Дети до 6 лет")]
        ChildrenUnder6,

        [Description("Дети от 6 до 14 лет")]
        Children6To14,

        [Description("Пушкинская карта от 14 до 22 лет")]
        PushkinCard14To22,

        [Description("Семейная аудитория")]
        Family
    }
}
