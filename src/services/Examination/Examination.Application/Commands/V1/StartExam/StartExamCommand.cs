using System;
using MediatR;

namespace Examination.Application.Commands.StartExam
{
    public class StartExamCommand : IRequest<bool>
    {
        public string UserId { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string ExamId { get; set; } = string.Empty;

        public DateTime StartDate { get; set; } = DateTime.UtcNow;
    }
}
