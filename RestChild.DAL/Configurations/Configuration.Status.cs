using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Статусы
        /// </summary>
        private static void Status(Context context)
        {
            context.Status.AddOrUpdate(t => t.Id,
                new Status
                {
                    Id = (long) StatusEnum.Draft,
                    Name = "Черновик",
                    MpguName = "Черновик",
                    CommerceName = "Черновик",
                    ForCommerce = true,
                    ForPreferential = true
                },
                new Status
                {
                    Id = (long) StatusEnum.ErrorRequest,
                    Name = "Ошибочное заявление",
                    MpguName = "Ошибочное заявление",
                    ExternalUid = ((long) StatusEnum.ErrorRequest).ToString(CultureInfo.InvariantCulture),
                    ForCommerce = false,
                    ForPreferential = true
                },
                new Status
                {
                    Id = (long) StatusEnum.OperatorCheck,
                    Name = "Проверка оператором",
                    MpguName = "На исполнении",
                    MpguDescription = "Ваше заявление находится на обработке в ОИВ",
                    MpguComment = "Ваше заявление находится на обработке в ОИВ",
                    ExternalUid = ((long) StatusEnum.OperatorCheck).ToString(CultureInfo.InvariantCulture),
                    ForCommerce = false,
                    ForPreferential = true
                }, new Status
                {
                    Id = (long) StatusEnum.Send,
                    Name = "На исполнении. Зарегистрировано",
                    MpguName = "На исполнении",
                    MpguDescription = "На исполнении. Зарегистрировано",
                    MpguComment =
                        "Ваше заявление зарегистрировано. Срок исполнения не превышает 15 рабочих дней с момента регистрации заявления",
                    ExternalUid = ((long) StatusEnum.Send).ToString(CultureInfo.InvariantCulture),
                    ForCommerce = false,
                    ForPreferential = true
                }, new Status
                {
                    Id = (long) StatusEnum.WaitApplicant,
                    Name = "Ожидание прихода Заявителя в ОИВ для подтверждения сведений, указанных в заявлении",
                    MpguName = "На исполнении",
                    MpguDescription =
                        "Ожидание прихода Заявителя в ОИВ для подтверждения сведений, указанных в заявлении.",
                    MpguComment =
                        @"Ожидание предоставления сведений.

Для подтверждения сведений, указанных в заявлении, Вам необходимо явиться в течение пяти рабочих дней с момента получения этого уведомления по адресу: г. Москва, Малый Харитоньевский переулок д. 6 стр. 3 с 8:00 до 20:00 ежедневно.
При себе необходимо иметь оригиналы следующих документов:
а) документ, удостоверяющий личность заявителя + доверенность (в случае подачи заявления лицом, уполномоченным в установленном порядке).
б) документ, подтверждающий полномочия заявителя как родителя (законного представителя) (по желанию заявителя);
в) документ, удостоверяющий личность ребёнка: свидетельство о рождении ребёнка или документ, подтверждающий факт рождения и регистрации ребёнка, выданный в установленном порядке (в случае рождения ребенка на территории иностранного государства), паспорт гражданина Российской Федерации (при достижении ребенком возраста 14 лет);
д) документ, подтверждающий льготную категорию ребёнка;
е) документ, содержащий сведения о месте жительства ребёнка в городе Москве (в случае, если в документе, удостоверяющем личность ребёнка, отсутствуют сведения о его месте жительства в городе Москве, или сведения о многоквартирном доме, в котором проживает ребенок, не содержатся в Базовом регистре);
ж) документ, подтверждающий полномочия сопровождающего лица как родителя (законного представителя) (для совместного отдыха и по желанию заявителя);
з) документ, подтверждающий уважительную причину неиспользования ранее выданной путевки.",
                    ExternalUid = ((long) StatusEnum.WaitApplicant).ToString(CultureInfo.InvariantCulture),
                    ForCommerce = false,
                    ForPreferential = true
                }, new Status
                {
                    Id = (long) StatusEnum.Reject,
                    Name = "Отказ в предоставлении услуги",
                    MpguName = "Услуга оказана",
                    MpguDescription = "Отказ в предоставлении путевки на отдых и оздоровление",
                    MpguComment = @"Отказ в предоставлении путевки на отдых и оздоровление.
Вам отказано в предоставлении путевки на отдых и оздоровление в соответствии с ",
                    CommerceName = "Аннулировано",
                    ExternalUid = ((long) StatusEnum.Reject).ToString(CultureInfo.InvariantCulture),
                    IsFinal = true,
                    ForCommerce = true,
                    ForPreferential = true
                }, new Status
                {
                    Id = (long) StatusEnum.CancelByApplicant,
                    Name = "Отозвано. По инициативе заявителя",
                    MpguName = "Отозвано",
                    MpguDescription = "Отозвано по инициативе Заявителя",
                    MpguComment = @"Отозвано по инициативе заявителя
в соответствии с ",
                    ExternalUid = ((long) StatusEnum.CancelByApplicant).ToString(CultureInfo.InvariantCulture),
                    IsFinal = true,
                    ForCommerce = false,
                    ForPreferential = true
                }, new Status
                {
                    Id = (long) StatusEnum.ApplicantCome,
                    Name = "На исполнении. Формирование результата предоставления услуги",
                    MpguName = "На исполнении",
                    MpguDescription = "На исполнении. Формирование результата предоставления услуги",
                    MpguComment = "На исполнении. Формирование результата предоставления услуги",
                    ExternalUid = ((long) StatusEnum.ApplicantCome).ToString(CultureInfo.InvariantCulture),
                    IsFinal = false,
                    ForCommerce = false,
                    ForPreferential = true
                }, new Status
                {
                    Id = (long) StatusEnum.CertificateIssued,
                    Name = "Услуга оказана",
                    MpguName = "Услуга оказана",
                    MpguDescription = "Путевка на отдых и оздоровление.",
                    MpguComment =
                        "Услуга оказана. Путевка носит информационный характер. Путевку на отдых и оздоровление вы можете распечатать самостоятельно из личного кабинета или получить в бумажном виде в ГАУК «Мосгортур» по адресу: г. Москва, Малый Харитоньевский переулок д. 6 стр. 3 с 8:00 до 20:00 ежедневно.",
                    ExternalUid = ((long) StatusEnum.CertificateIssued).ToString(CultureInfo.InvariantCulture),
                    CommerceName = "Оплачено",
                    IsFinal = true,
                    ForCommerce = true,
                    ForPreferential = true
                }, new Status
                {
                    Id = (long) StatusEnum.ReadyToPay,
                    Name = "Готово к оплате",
                    MpguName = "Готово к оплате",
                    MpguDescription = "Ожидание поступления сведений об оплате заявления",
                    MpguComment = @"Ожидание поступления сведений об оплате заявления",
                    CommerceName = "Готово к оплате",
                    ExternalUid = ((long) StatusEnum.ReadyToPay).ToString(CultureInfo.InvariantCulture),
                    ForCommerce = true,
                    ForPreferential = false
                }, new Status
                {
                    Id = (long) StatusEnum.ReadyToPayFull,
                    Name = "Готово к доплате",
                    MpguName = "Готово к доплате",
                    MpguDescription = "Ожидание поступления сведений об оплате заявления",
                    MpguComment = @"Ожидание поступления сведений об оплате заявления",
                    CommerceName = "Готово к доплате",
                    ExternalUid = ((long) StatusEnum.ReadyToPayFull).ToString(CultureInfo.InvariantCulture),
                    ForCommerce = true,
                    ForPreferential = false
                }, new Status
                {
                    Id = (long) StatusEnum.OnWorking,
                    Name = "В работе",
                    MpguName = "В работе",
                    MpguDescription = "В работе",
                    MpguComment = @"В работе",
                    CommerceName = "В работе",
                    ExternalUid = ((long) StatusEnum.OnWorking).ToString(CultureInfo.InvariantCulture),
                    ForCommerce = true,
                    ForPreferential = false
                }, new Status
                {
                    Id = (long) StatusEnum.OnApprove,
                    Name = "Запрошено",
                    MpguName = "Запрошено",
                    MpguDescription = "Запрошено",
                    MpguComment = @"Запрошено",
                    CommerceName = "Запрошено",
                    ExternalUid = ((long) StatusEnum.OnApprove).ToString(CultureInfo.InvariantCulture),
                    ForCommerce = true,
                    ForPreferential = false
                }, new Status
                {
                    Id = (long) StatusEnum.Payed,
                    Name = "Оплачен аванс",
                    MpguName = "Оплачен аванс",
                    MpguDescription = "Оплачен аванс",
                    MpguComment = @"Оплачен аванс",
                    CommerceName = "Оплачен аванс",
                    ExternalUid = ((long) StatusEnum.Payed).ToString(CultureInfo.InvariantCulture),
                    ForCommerce = true,
                    ForPreferential = false
                }, new Status
                {
                    Id = (long) StatusEnum.Denial,
                    Name = "Отказ",
                    MpguName = "Отказ",
                    MpguDescription = "Отказ",
                    MpguComment = @"Отказ",
                    CommerceName = "Отказ",
                    ExternalUid = ((long) StatusEnum.Denial).ToString(CultureInfo.InvariantCulture),
                    ForCommerce = true,
                    ForPreferential = false
                }, new Status
                {
                    Id = (long) StatusEnum.RegistrationDecline,
                    Name = "Отказ в регистрации",
                    MpguName = "Отказ в регистрации",
                    MpguDescription = "Отказ в регистрации",
                    MpguComment = "",
                    ExternalUid = ((long) StatusEnum.RegistrationDecline).ToString(CultureInfo.InvariantCulture),
                    ForCommerce = false,
                    ForPreferential = true
                }, new Status
                {
                    Id = (long) StatusEnum.Ranging,
                    Name = "Ранжирование",
                    MpguName = "Ранжирование",
                    MpguDescription = "Ранжирование",
                    MpguComment = "",
                    ExternalUid = ((long) StatusEnum.Ranging).ToString(CultureInfo.InvariantCulture),
                    ForCommerce = false,
                    ForPreferential = true
                }, new Status
                {
                    Id = (long) StatusEnum.IncludedInList,
                    Name = "Включено в список",
                    MpguName = "Включено в список",
                    MpguDescription = "Включено в список",
                    MpguComment = "",
                    ExternalUid = ((long) StatusEnum.IncludedInList).ToString(CultureInfo.InvariantCulture),
                    ForCommerce = false,
                    ForPreferential = true
                }, new Status
                {
                    Id = (long) StatusEnum.DecisionMaking,
                    Name = "Принятие решения",
                    MpguName = "Принятие решения",
                    MpguDescription = "Принятие решения",
                    MpguComment = "",
                    ExternalUid = ((long) StatusEnum.DecisionMaking).ToString(CultureInfo.InvariantCulture),
                    ForCommerce = false,
                    ForPreferential = true
                }, new Status
                {
                    Id = (long) StatusEnum.DecisionMakingCovid,
                    Name = "Принятие решения (дополнительная кампания)",
                    MpguName = "Принятие решения (дополнительная кампания)",
                    MpguDescription = "Принятие решения (дополнительная кампания)",
                    MpguComment = "",
                    ExternalUid = ((long) StatusEnum.DecisionMakingCovid).ToString(CultureInfo.InvariantCulture),
                    ForCommerce = false,
                    ForPreferential = true
                }, new Status
                {
                    Id = (long) StatusEnum.DecisionIsMade,
                    Name = "Решение принято",
                    MpguName = "Решение принято",
                    MpguDescription = "Решение принято",
                    MpguComment = "",
                    ExternalUid = ((long) StatusEnum.DecisionIsMade).ToString(CultureInfo.InvariantCulture),
                    ForCommerce = false,
                    ForPreferential = true
                }, new Status
                {
                    Id = (long) StatusEnum.WaitApplicantMoney,
                    Name = "Вызов для подтверждения данных платежа",
                    MpguName = "Вызов для подтверждения данных платежа",
                    MpguDescription = "Вызов для подтверждения данных платежа",
                    MpguComment = "",
                    ExternalUid = ((long) StatusEnum.WaitApplicantMoney).ToString(CultureInfo.InvariantCulture),
                    ForCommerce = false,
                    ForPreferential = true
                }
                );

            SetEidAndLastUpdateTicks(context.Status.ToList());
            context.SaveChanges();
        }
    }
}
