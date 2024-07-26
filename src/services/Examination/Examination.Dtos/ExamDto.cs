using System;

namespace Examination.Dtos
{
    public class ExamDto
    {
        public string Id { set; get; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string ShortDesc { get; set; } = string.Empty;

        public int NumberOfQuestions { get; set; } = 0;

        public TimeSpan? Duration { get; set; }

        public Enums.Level Level { get; set; } = Enums.Level.Easy;

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
