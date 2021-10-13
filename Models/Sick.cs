namespace CentWorkTimeTracker.Models
{
    public enum SickType
    {
        Leave = 0, // со справкой
        Day = 1 // без справки  дней в год
    }

    /// <summary>
    /// Модель больничного
    /// </summary>
    public class Sick : DaysBaseModel
    {
        public SickType Type { get; set; }
        public int DaysCount => (To - From).Days;
    }
}