using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.Extensions.Extensions
{
    public static class CounselorTaskExtension
    {
        public static string GetSubject(this CounselorTask self)
        {
            if (self == null)
            {
                return string.Empty;
            }

            switch (self.StateId)
            {
                case StateMachineStateEnum.CounselorTask.Delivered:
                    return "Новая задача в АИС \"Отдых\"";
                case StateMachineStateEnum.CounselorTask.Approved:
                    return "Новая задача на согласование в АИС \"Отдых\"";
                case StateMachineStateEnum.CounselorTask.Sended:
                    return "Новое сообщение в АИС \"Отдых\"";
                case StateMachineStateEnum.CounselorTask.Completion:
                    return "Новая задача на доработку в АИС \"Отдых\"";
            }

            return string.Empty;
        }

        public static string GetEmail(this CounselorTask self)
        {
            ResponsibilityForTask perfomerEntity = null;

            switch (self?.StateId)
            {
                case StateMachineStateEnum.CounselorTask.Delivered:
                    perfomerEntity = self?.Executor;
                    break;
                case StateMachineStateEnum.CounselorTask.Approved:
                    perfomerEntity = self?.Author;
                    break;
                case StateMachineStateEnum.CounselorTask.Sended:
                    perfomerEntity = self?.Executor;
                    break;
                case StateMachineStateEnum.CounselorTask.Completion:
                    perfomerEntity = self?.Executor;
                    break;
            }

            if (perfomerEntity == null)
            {
                return string.Empty;
            }

            return perfomerEntity.Account != null
                ? perfomerEntity.Account.Email
                : perfomerEntity.AdministratorTour != null
                    ? perfomerEntity.AdministratorTour.Email
                    : perfomerEntity.Counselors?.Email;
        }

        public static string GetBody(this CounselorTask self)
        {
            var header = string.Empty;

            if (self == null)
            {
                return string.Empty;
            }

            switch (self.StateId)
            {
                case StateMachineStateEnum.CounselorTask.Delivered:
                    header = "Вам была поставлена задача";
                    break;
                case StateMachineStateEnum.CounselorTask.Approved:
                    header = "Вам была отправлена задача на согласование";
                    break;
                case StateMachineStateEnum.CounselorTask.Sended:
                    header = "Вам было отправлено сообщение";
                    break;
                case StateMachineStateEnum.CounselorTask.Completion:
                    header = "Вам была отправлена задача на доработку";
                    break;
            }

            return $"{header}<br/><b>Автор:</b> {self.Author.GetInfo()}<br/>" +
                   (self.StateId == StateMachineStateEnum.CounselorTask.Delivered ||
                    self.StateId == StateMachineStateEnum.CounselorTask.Completion
                       ? $"<b>Срок исполнения:</b> {self.DatePlanFinish.FormatEx()}<br/>"
                       : "") +
                   $"<b>Тема:</b> {self.Subject.FormatEx()}<br/>" +
                   $"<b>Содержание:</b> {self.Body}<br/>";
        }
    }
}
