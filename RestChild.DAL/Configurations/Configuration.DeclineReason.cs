using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Migrations;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Причины отказов
        /// </summary>
        private static void DeclineReason(Context context)
        {
            var trs = context.TypeOfRest.Where(tr =>
                    !tr.Commercial && tr.Id != (long) TypeOfRestEnum.Compensation &&
                    tr.Id != (long) TypeOfRestEnum.CompensationYouthRest &&
                    tr.Id != (long) TypeOfRestEnum.CompensationGroup)
                .ToList();

            var compensationTypeOfRest =
                context.TypeOfRest.FirstOrDefault(t => t.Id == (long) TypeOfRestEnum.Compensation);
            var compensationYouthRestTypeOfRest =
                context.TypeOfRest.FirstOrDefault(t => t.Id == (long) TypeOfRestEnum.CompensationYouthRest);


            var covidTypesOfRest = context.TypeOfRest.Where(t =>
                    t.Id == (long) TypeOfRestEnum.ChildRest ||
                    t.ParentId == (long) TypeOfRestEnum.ChildRest ||
                    t.Id == (long) TypeOfRestEnum.RestWithParents ||
                    t.ParentId == (long) TypeOfRestEnum.RestWithParents ||
                    t.Id == (long) TypeOfRestEnum.RestWithParentsInvalidOrphanComplex ||
                    t.ParentId == (long) TypeOfRestEnum.RestWithParentsInvalidOrphanComplex ||
                    t.Id == (long) TypeOfRestEnum.YouthRestCamps ||
                    t.ParentId == (long) TypeOfRestEnum.YouthRestCamps).ToList();

            var covidRefuseAllVariantsTypesOfRest =
                context.TypeOfRest.Where(t =>
                    t.Id == (long) TypeOfRestEnum.ChildRestCamps ||
                    t.Id == (long) TypeOfRestEnum.RestWithParentsPoor ||
                    t.Id == (long) TypeOfRestEnum.RestWithParentsInvalid ||
                    t.Id == (long) TypeOfRestEnum.RestWithParentsOrphan ||
                    t.Id == (long) TypeOfRestEnum.RestWithParentsComplex ||
                    t.Id == (long) TypeOfRestEnum.ChildRestOrphanCamps ||
                    t.Id == (long) TypeOfRestEnum.YouthRestOrphanCamps ||
                    t.Id == (long) TypeOfRestEnum.TentChildrenCamp ||
                    t.Id == (long) TypeOfRestEnum.TentChildrenCampOrphan).ToList();

            compensationTypeOfRest?.DeclineReasons?.Clear();
            compensationYouthRestTypeOfRest?.DeclineReasons?.Clear();

            context.SaveChanges();

            //2021-2022
            context.DeclineReason.AddOrUpdate(a => a.Id, new DeclineReason
            {
                Id = 202101,
                IsActive = true,
                Name =
                        "Нарушение правил подачи заявления о предоставлении услуг отдыха и оздоровления",
                IsManual = true,
                FirstStage = true,
                SecondStage = true,
                StatusId = (long)StatusEnum.Reject
            },
                new DeclineReason
                {
                    Id = 202103,
                    IsActive = true,
                    Name = "Заявление является повторным. \n\r На указанное(ые) в заявлении сопровождающее(ие) лицо(а) уже подано заявление о предоставлении сертификата на отдых и оздоровление.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long)StatusEnum.RegistrationDecline
                },
                new DeclineReason
                {
                    Id = 202102,
                    IsActive = true,
                    Name = "Заявление является повторным. \n\r На указанное(ые) в заявлении сопровождающее(ие) лицо(а) уже подано заявление о предоставлении бесплатной путёвки для отдыха и оздоровления.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long)StatusEnum.RegistrationDecline
                },
                new DeclineReason
                {
                    Id = 202105,
                    IsActive = true,
                    Name = "Заявление является повторным. \n\r Заявление о предоставлении бесплатной путёвки для отдыха и оздоровления от имени родителя (законного представителя) уже подано.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long)StatusEnum.RegistrationDecline
                },
                new DeclineReason
                {
                    Id = 202106,
                    IsActive = true,
                    Name = "Заявление является повторным. \n\r Заявление о предоставлении сертификата на отдых и оздоровление от имени родителя (законного представителя) уже подано.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long)StatusEnum.RegistrationDecline
                },
                new DeclineReason
                {
                    Id = 202104,
                    IsActive = true,
                    Name = "Заявление является повторным. \n\r Заявление на указанное в заявлении лицо из числа детей-сирот и детей, оставшихся без попечения родителей уже было подано.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long)StatusEnum.RegistrationDecline
                },
                new DeclineReason
                {
                    Id = 202107,
                    IsActive = true,
                    Name = "Заявление является повторным. \n\r На указанного в заявлении ребёнка уже подано заявление о предоставлении услуг отдыха и оздоровления.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long)StatusEnum.RegistrationDecline
                });
                    StatusId = (long)StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 202108,
                    IsActive = true,
                    Name = "пункт 9.1.6(1) Порядка: \"Нарушение установленных пунктом 5.4(1) Порядка правил подачи заявления о предоставлении услуг отдыха и оздоровления (при обращении заявителя для организации отдыха и оздоровления двум и более детям в возрасте от 3 до 7 лет включительно, относящимся к категории детей, указанной в пункте 3.1.4 Порядка, заявителем подается одно заявление о предоставлении услуг отдыха и оздоровления с внесением сведений обо всех детях заявителя, которые относятся к этой категории детей и в отношении которых требуется организация отдыха и оздоровления, и внесением сведений о сопровождающем лице (двух сопровождающих лицах).\"",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long)StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 202109,
                    IsActive = true,
                    Name = "пункт 5.11.1. Порядка: \"Наличие в отношении одного и того же ребенка  другого заявления о предоставлении услуг отдыха и оздоровления в текущем календарном году.\"",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long)StatusEnum.RegistrationDeclineBecauseDuplicate
                },
                new DeclineReason
                {
                    Id = 202110,
                    IsActive = true,
                    Name = "пункт 5.11.1.(1) Порядка: \"Нарушение установленных пунктом 5.4(1) Порядка правил подачи заявления о предоставлении услуг отдыха и оздоровления: \"При обращении заявителя для организации отдыха и оздоровления двум и более детям в возрасте от 3 до 7 лет включительно, относящимся к категории детей, указанной в пункте 3.1.4 Порядка, заявителем подается одно заявление о предоставлении услуг отдыха и оздоровления с внесением сведений обо всех детях заявителя, которые относятся к этой категории детей и в отношении которых требуется организация отдыха и оздоровления, и внесением сведений о сопровождающем лице(двух сопровождающих лицах).\"",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long)StatusEnum.RegistrationDeclineBecauseDuplicate
                },
                new DeclineReason
                {
                    Id = 202111,
                    IsActive = true,
                    Name = "пункт 5.11.1. Порядка: \"Наличие в отношении одного и того же лица из числа детей-сирот и детей, оставшихся без попечения родителей, другого заявления о предоставлении услуг отдыха и оздоровления в текущем календарном году.\"",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long)StatusEnum.RegistrationDeclineBecauseDuplicate
                }

                );
            context.SaveChanges();

            // 2020 - 2021
            context.DeclineReason.AddOrUpdate(a => a.Id, new DeclineReason
                {
                    Id = 202001,
                    IsActive = true,
                    Name =
                        "пункт 9.1.6. Порядка: \"Непредставление в предусмотренный пунктом 6.7 Порядка срок приостановления заявления о предоставлении услуг отдыха и оздоровления соответствующих документов. (Заявитель обязан представить соответствующие документы в ГАУК \"Мосгортур\" в срок не позднее 10 рабочих дней со дня  направления уведомления о приостановлении рассмотрения заявления о предоставлении услуг отдыха и оздоровления).\"",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 202002,
                    IsActive = true,
                    Name =
                        "пункт 9.1.5. Порядка: \"Несоответствие возраста детей возрасту, предусмотренному для соответствующего вида отдыха и оздоровления и указанному в пунктах 3.12, 3.16 - 3.18  Порядка.\"",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 202003,
                    IsActive = true,
                    Name =
                        "пункт 9.1.4. Порядка: \"Неподтверждение отнесения ребёнка к одной из категорий детей, находящихся в трудной жизненной ситуации и указанных в пунктах 3.1.2 - 3.1.13  Порядка, неподтверждение отнесения лица к категории лиц из числа детей - сирот и детей, оставшихся без попечения родителей.\"",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 202004,
                    IsActive = true,
                    Name =
                        "пункт 9.1.4(1). Порядка: \"Неподтверждение факта получения ежемесячного пособия в соответствии с Законом города Москвы от 3 ноября 2004 г. N 67 \"О ежемесячном пособии на ребёнка\" на детей, находящихся в трудной жизненной ситуации и указанных в пунктах 3.1.5 - 3.1.13  Порядка.\"",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 202005,
                    Name =
                        "пункт 9.1.11. Порядка: \"Наличие сведений о нарушении условий использования сертификата на отдых и оздоровление, установленных разделом 8(1) Порядка, в прошедшем и (или) текущем календарном году.\"",
                    IsActive = true,
                    IsManual = true,
                    FirstStage = true,
                    TypeOfRests = trs.ToList(),
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 202006,
                    IsActive = true,
                    Name =
                        "пункт 9.1.8. Порядка: \"Наличия сведений о нарушениях правил отдыха и оздоровления в текущем календарном году ребёнком, отнесенным к одной из категорий детей, находящихся в трудной жизненной ситуации и указанных в пунктах 3.1.3 - 3.1.13  Порядка, сопровождающим лицом (в случае организации совместного либо индивидуального выездного отдыха).\"",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                });
            context.SaveChanges();

            // отказ(ы) по минилоку 2020
            context.DeclineReason.AddOrUpdate(a => a.Id,
                new DeclineReason
                {
                    Id = (long) DeclineReasonEnum.CertificateIssued,
                    Name = "Осуществлен перевыбор",
                    IsActive = false,
                    IsManual = false,
                    FirstStage = true
                },
                new DeclineReason
                {
                    Id = (long) DeclineReasonEnum.NonParticipationOfApplicant,
                    Name =
                        "пункты 2.5. и 2.6. Порядка: \"Заявитель вправе не участвовать в выборе вариантов организации отдыха и оздоровления. При этом неучастие заявителя в выборе вариантов организации отдыха и оздоровления не может являться основанием для отказа в предоставлении услуг отдыха и оздоровления при организации отдыха и оздоровления в 2021 – 2023 годах в порядке, утвержденном постановлением Правительства Москвы от 22 февраля 2017 г. № 56 - ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\", \"В случае неучастия заявителя в выборе вариантов организации отдыха и оздоровления, уведомление об отказе в предоставлении услуг отдыха и оздоровления в связи с неучастием в выборе варианта организации отдыха и оздоровления на основании бесплатной путевки для отдыха и оздоровления за счет средств бюджета города Москвы с датами заезда, начиная с 1 апреля по 27 июля 2020 г. направляется заявителю в срок не позднее 1 октября 2020 г.\".",
                    IsActive = true,
                    IsManual = true,
                    FirstStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = (long) DeclineReasonEnum.RefuseAllVariantsCovid2020,
                    IsActive = true,
                    Name =
                        "пункт 5.8 Порядка: \"Заявитель вправе не участвовать в выборе конкретной организации отдыха и оздоровления из числа предлагаемых ГАУК \"МОСГОРТУР\" в соответствии с пунктом 5.5 Порядка. При этом неучастие заявителя в выборе конкретной организации отдыха и оздоровления не может являться основанием для отказа в предоставлении услуг отдыха и оздоровления при организации отдыха и оздоровления в 2021-2023 годах в порядке, утвержденном постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\".",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                }
            );
            context.SaveChanges();

            // 2019 - 2020
            context.DeclineReason.AddOrUpdate(a => a.Id,
                new DeclineReason
                {
                    Id = 201901,
                    Name =
                        "пункт 9.1.11. Порядка: \"Наличие сведений о нарушении условий использования сертификата на отдых и оздоровление, установленных пунктами 8(1).1-8(1).4 настоящего Порядка\".",
                    IsActive = false,
                    IsManual = true,
                    FirstStage = true,
                    TypeOfRests = trs.ToList(),
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201902,
                    Name =
                        "пункт 9.1.12. Порядка: \"Наличие сведений об отказе заявителя от всех предложенных организаций отдыха и оздоровления на втором этапе заявочной кампании.\"",
                    IsActive = true,
                    IsManual = true,
                    FirstStage = true,
                    TypeOfRests = trs.ToList(),
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201903,
                    IsActive = false,
                    Name =
                        "пункт 9.1.6. Порядка: \"Непредставление в предусмотренный пунктом 6.7 настоящего Порядка срок приостановления заявления о предоставлении услуг отдыха и оздоровления соответствующих документов\".",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201904,
                    IsActive = true,
                    Name =
                        "пункт 9.1.2. Порядка: \"Представление документов, не соответствующих требованиям, установленным правовыми актами Российской Федерации, правовыми актами города Москвы, противоречивых или недостоверных сведений либо утрата силы представленных документов в случае, если в документах указан срок их действия или срок их действия установлен законодательством\".",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201905,
                    IsActive = false,
                    Name =
                        "пункт 9.1.4. Порядка: \"Отсутствие подтверждения отнесения ребёнка к одной из категорий детей, находящихся в трудной жизненной ситуации и указанных в пунктах 3.1.2 - 3.1.13 настоящего Порядка, неподтверждение отнесения лица к категории лиц из числа детей-сирот и детей, оставшихся без попечения родителей\".",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201906,
                    IsActive = false,
                    Name =
                        "пункт 9.1.4(1). Порядка: \"Отсутствие подтверждения получения ежемесячного пособия в соответствии с Законом города Москвы от 3 ноября 2004 г. N 67 \"О ежемесячном пособии на ребёнка\" на детей, находящихся в трудной жизненной ситуации и указанных в пунктах 3.1.5-3.1.13 настоящего Порядка\".",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201907,
                    IsActive = false,
                    Name =
                        "пункт 9.1.8. Порядка: \"Наличие сведений о нарушениях правил отдыха и оздоровления в период отдыха и оздоровления в текущем календарном году ребёнком, сопровождающим лицом (в случае организации совместного выездного отдыха), лицом из числа детей-сирот и детей, оставшихся без попечения родителей\".",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201908,
                    IsActive = true,
                    Name =
                        "пункт 9.1.9. Порядка: \"Наличие сведений о нарушениях сопровождающим лицом в текущем календарном году обязательств, предусмотренных соглашением об осуществлении обязанностей по сопровождению детей во время отдыха и оздоровления родителем или иным законным представителем либо доверенным лицом для сопровождения во время отдыха и оздоровления, заключенным с ГАУК \"МОСГОРТУР\"",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201909,
                    IsActive = true,
                    Name =
                        "пункт 9.1.10. Порядка: \"Наличие сведений о неосуществлении отдыха и оздоровления без уважительных причин, указанных в пункте 10.1.2 Порядка, на основании предоставленной в текущем календарном году путевки для отдыха и оздоровления с оплатой стоимости путевки для отдыха и оздоровления за счет средств бюджета города Москвы.\"",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201910,
                    IsActive = false,
                    Name =
                        "пункт 9.1.5. Порядка: \"Несоответствие возраста детей возрасту, предусмотренному для соответствующего вида отдыха и оздоровления и указанному в пунктах 3.12, 3.16 настоящего Порядка\".",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201911,
                    IsActive = true,
                    Name =
                        "Неучастие заявителя во втором этапе заявочной кампании в целях организации отдыха и оздоровления, предусматривающем необходимость выбора заявителем конкретной организации отдыха и оздоровления.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201912,
                    IsActive = true,
                    Name =
                        "пункт 9.1.3. Порядка: \"Отсутствие места жительства ребёнка, лица из числа детей-сирот и детей, оставшихся без попечения родителей, в городе Москве.\"",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201913,
                    IsActive = true,
                    Name =
                        "пункт 11.17.1. Порядка: \"Представление документов, не соответствующих требованиям, установленным правовыми актами Российской Федерации, правовыми актами города Москвы, противоречивых или недостоверных сведений\".",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject,
                    TypeOfRests = new List<TypeOfRest> {compensationTypeOfRest}
                },
                new DeclineReason
                {
                    Id = 201914,
                    IsActive = true,
                    Name =
                        "пункт 11.17.2. Порядка: \"Отсутствие права на получение компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления\".",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject,
                    TypeOfRests = new List<TypeOfRest> {compensationTypeOfRest}
                },
                new DeclineReason
                {
                    Id = 201915,
                    IsActive = true,
                    Name =
                        "пункт 11.17.3. Порядка: \"Наличие в отношении одного и того же ребёнка сведений о неосуществлении отдыха и оздоровления без уважительных причин, указанных в пункте 10.1.2 Порядка, на основании предоставленной в прошедшем и(или) текущем календарном году бесплатной путевки для отдыха и оздоровления детей, находящихся в трудной жизненной ситуации и указанных в пункте 3.1.4 Порядка\".",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject,
                    TypeOfRests = new List<TypeOfRest> {compensationTypeOfRest}
                },
                new DeclineReason
                {
                    Id = 201916,
                    IsActive = true,
                    Name =
                        "пункт 11.17.4. Порядка: \"Наличие в отношении одного и того же ребёнка, находящегося в трудной жизненной ситуации и указанного в пункте 3.1.4 Порядка, сведений о нарушении условий использования сертификата на отдых и оздоровление, установленных разделом 8(1) Порядка, предоставленного в прошедшем и(или) текущем календарном году\".",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject,
                    TypeOfRests = new List<TypeOfRest> {compensationTypeOfRest}
                },
                new DeclineReason
                {
                    Id = 201917,
                    IsActive = true,
                    Name =
                        "пункт 11(1).13.1. Порядка: \"Представление документов, не соответствующих требованиям, установленным правовыми актами Российской Федерации, правовыми актами города Москвы, предоставление противоречивых или недостоверных сведений\".",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject,
                    TypeOfRests = new List<TypeOfRest> {compensationYouthRestTypeOfRest}
                },
                new DeclineReason
                {
                    Id = 201918,
                    IsActive = true,
                    Name =
                        "пункт 11(1).13.2. Порядка: \"Отсутствие права на получение компенсации за самостоятельно приобретенную лицом из числа детей - сирот и детей, оставшихся без попечения родителей, путевку для отдыха и оздоровления, в том числе в случае непредставления дополнительных сведений в соответствии с пунктом 11(1).9 Порядка\".",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject,
                    TypeOfRests = new List<TypeOfRest> {compensationYouthRestTypeOfRest}
                },
                new DeclineReason
                {
                    Id = 201919,
                    IsActive = true,
                    Name = "Отказ от сертификата, в установленный Порядком срок.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.CancelByApplicant
                }
            );

            context.DeclineReason.AddOrUpdate(a => a.Id,
                new DeclineReason
                {
                    Id = 101,
                    Name = @"10.1 Отсутствие права на получение частичной компенсации.",
                    IsActive = true,
                    IsManual = true,
                    FirstStage = true,
                    TypeOfRests = new TypeOfRest[0],
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 103,
                    Name =
                        @"10.2 Наличие в отношении одного и того же ребёнка в течение одного периода, определяемого со дня первого заезда в организации отдыха и оздоровления в период весенних школьных каникул текущего календарного года до последнего дня выезда последнего заезда в период зимних школьных каникул следующего календарного года, сведений о предоставлении путевки для выездного отдыха и оздоровления или выплате компенсации за самостоятельно приобретенную путевку.",
                    IsActive = true,
                    IsManual = false,
                    FirstStage = true,
                    TypeOfRests = new TypeOfRest[0],
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 105,
                    Name =
                        @"10.3 Несоответствие предоставленных заявителем документов, установленным требованиям, либо представление заявителем противоречивых или недостоверных сведений, либо представление заявителем документов, утративших силу.",
                    IsActive = true,
                    IsManual = true,
                    FirstStage = true,
                    TypeOfRests = new TypeOfRest[0],
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 109,
                    Name = @"10.4 Нарушение срока и порядка подачи заявления на выплату частичной компенсации.",
                    IsActive = true,
                    IsManual = true,
                    FirstStage = true,
                    TypeOfRests = new TypeOfRest[0],
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201,
                    Name = @"14.1 Отсутствие права на получение частичной компенсации.",
                    IsActive = true,
                    IsManual = true,
                    FirstStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 203,
                    Name =
                        @"14.2 Наличие в отношении одного и того же ребёнка в течение одного периода отдыха и оздоровления за текущий год сведений о предоставлении путевки на отдых и оздоровление или выплаты частичной компенсации.
 При этом период отдыха и оздоровления за текущий год определяется:
 со дня первого заезда в организации отдыха и оздоровления в период весенних школьных каникул текущего календарного года до последнего дня выезда последнего заезда в период зимних школьных каникул следующего календарного года для предоставления путевок на отдых и оздоровления за счет средств бюджета города Москвы;
 датой начала отдыха в текущем году для выплаты частичной компенсации.",
                    IsActive = true,
                    IsManual = true,
                    FirstStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 205,
                    Name =
                        @"14.3 Несоответствие представленных заявителем документов, установленным требованиям, либо представление заявителем противоречивых или недостоверных сведений, либо представление заявителем документов, утративших силу.",
                    IsActive = true,
                    IsManual = true,
                    FirstStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 209,
                    Name = @"14.4 Нарушение срока и порядка подачи заявления на выплату частичной компенсации.",
                    IsActive = true,
                    IsManual = true,
                    FirstStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201761,
                    Name =
                        @"Представление документов, не соответствующих требованиям, установленным правовыми актами Российской Федерации, правовыми актами города Москвы, противоречивых или недостоверных сведений.",
                    IsActive = false,
                    IsManual = true,
                    FirstStage = true,
                    TypeOfRests = new List<TypeOfRest> {compensationTypeOfRest, compensationYouthRestTypeOfRest},
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201762,
                    Name =
                        @"Отсутствие права на получение компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления.",
                    IsActive = false,
                    IsManual = true,
                    FirstStage = true,
                    TypeOfRests = new List<TypeOfRest> {compensationTypeOfRest, compensationYouthRestTypeOfRest},
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201763,
                    Name =
                        @"Наличие сведений о неосуществление отдыха и оздоровления без уважительных причин, на основании предоставленной в прошедшем и (или) текущем календарном году путевки для отдыха и оздоровления с оплатой стоимости путевки для отдыха и оздоровления за счет средств бюджета города Москвы.",
                    IsActive = false,
                    IsManual = true,
                    FirstStage = true,
                    TypeOfRests = new List<TypeOfRest> {compensationTypeOfRest, compensationYouthRestTypeOfRest},
                    StatusId = (long) StatusEnum.Reject
                }
            );
            context.SaveChanges();

            var declinesCompensation = context.DeclineReason
                .Where(d => d.Id >= 201913 && d.Id <= 201918).ToList();

            if (compensationTypeOfRest != null)
            {
                compensationTypeOfRest.DeclineReasons.AddRange(declinesCompensation);
                context.SaveChanges();
            }

            if (compensationYouthRestTypeOfRest != null)
            {
                compensationYouthRestTypeOfRest.DeclineReasons.AddRange(declinesCompensation);
                context.SaveChanges();
            }

            //2016

            context.DeclineReason.AddOrUpdate(s => s.Id,
                new DeclineReason
                {
                    Id = 201601,
                    IsActive = true,
                    Name =
                        @"п. 17.1 Порядка Приказа: Отсутствие подтверждения льготной категории, указанной в заявлении.",
                    FirstStage = true,
                    SecondStage = true,
                    IsManual = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201602,
                    IsActive = true,
                    Name =
                        @"п. 17.2 Порядка Приказа: Наличие в отношении одного и того же ребёнка, лица его сопровождающего, сведений о предоставлении путевки в иную организацию отдыха в то же время, что и указано в заявлении.",
                    FirstStage = true,
                    SecondStage = true,
                    IsManual = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201603,
                    IsActive = true,
                    Name =
                        @"п. 17.3 Порядка Приказа: Наличие в отношении одного и того же ребёнка в течение одного периода, определяемого со дня первого заезда в организации отдыха и оздоровления в период весенних школьных каникул текущего календарного года до последнего дня выезда последнего заезда в период зимних школьных каникул следующего календарного года, сведений о предоставлении путевки на отдых и оздоровление или выплате компенсации за самостоятельно приобретенную путевку.",
                    FirstStage = true,
                    IsManual = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201604,
                    IsActive = true,
                    Name =
                        @"п. 17.4 Порядка Приказа: Непредставление заявителем, оригиналов документов, установленных Порядком, подтверждающих сведения, указанные в заявлении, и в установленный срок.",
                    FirstStage = true,
                    SecondStage = true,
                    IsManual = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201605,
                    IsActive = true,
                    Name =
                        @"п. 17.5 Порядка Приказа: Несоответствие представленных заявителем документов, установленным требованиям, либо представление заявителем противоречивых или недостоверных сведений, либо представленные документы утратили силу в случае, если в документах указан срок их действия или срок их действия установлен действующим законодательством.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201606,
                    IsActive = true,
                    Name =
                        @"п. 4.1 Порядка Приказа: Привлечение отдыхающих и сопровождающих лиц в прошедшем и (или) текущем календарном году к ответственности в связи с нарушениями законодательства в период их отдыха и оздоровления.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201607,
                    IsActive = true,
                    Name =
                        @"п. 4.2 Порядка Приказа: Нарушение отдыхающими и сопровождающими лицами в прошедшем и (или) текущем календарном году правил перевозок пассажиров, правил отдыха и оздоровления, правил пребывания в организациях отдыха и оздоровления в период отдыха и оздоровления.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201608,
                    IsActive = true,
                    Name =
                        @"п. 4.3 Порядка Приказа: Неосуществление без уважительных причин отдыха и оздоровления на основании путевки на отдых и оздоровление, выданной в прошедшем и (или) текущем календарном году.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201621,
                    IsActive = true,
                    Name =
                        @"п. 13. Порядка Приказа: Отказ от поданного заявления, в установленный Порядком срок.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.CancelByApplicant
                },
                new DeclineReason
                {
                    Id = 201622,
                    IsActive = true,
                    Name =
                        @"п. 19. Порядка Приказа: Отказ от путевки, в установленный Порядком срок.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.CancelByApplicant
                },
                new DeclineReason
                {
                    Id = 201623,
                    IsActive = true,
                    Name = @"п. 4.3. Порядка Приказа (а) заболевание, травма ребёнка, лица его сопровождающего.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.CancelByApplicant
                },
                new DeclineReason
                {
                    Id = 201624,
                    IsActive = true,
                    Name =
                        @"п. 4.3. Порядка Приказа (б) необходимость осуществления лицом, сопровождающим ребёнка, ухода за больным членом семьи.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.CancelByApplicant
                },
                new DeclineReason
                {
                    Id = 201625,
                    IsActive = true,
                    Name =
                        @"п. 4.3. Порядка Приказа (в) карантин ребёнка, родителя или иного законного представителя ребёнка, лица, сопровождающего ребёнка, близкого родственника, проживающего совместно с ребенком.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.CancelByApplicant
                },
                new DeclineReason
                {
                    Id = 201626,
                    IsActive = true,
                    Name = @"п. 4.3. Порядка Приказа (г) смерть близкого родственника.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.CancelByApplicant
                },
                new DeclineReason
                {
                    Id = 201627,
                    IsActive = true,
                    Name = @"Неуважительная причина отказа от путевки.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.CancelByApplicant,
                    ValidReasons = false
                },
                new DeclineReason
                {
                    Id = 201631,
                    IsActive = true,
                    Name =
                        @"п. 17.1 Порядка Приказа: Отсутствие подтверждения льготной категории, указанной в заявлении.",
                    FirstStage = true,
                    SecondStage = true,
                    IsManual = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201632,
                    IsActive = true,
                    Name =
                        @"п. 17.2 Порядка Приказа: Наличие в отношении одного и того же ребёнка сведений о предоставлении путевки в иную организацию отдыха в то же время, что и указано в заявлении.",
                    FirstStage = true,
                    SecondStage = true,
                    IsManual = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201633,
                    IsActive = true,
                    Name =
                        @"п. 17.4 Порядка Приказа: Непредставление заявителем, оригиналов документов, установленных Порядком, подтверждающих сведения, указанные в заявлении, и в установленный срок.",
                    FirstStage = true,
                    SecondStage = true,
                    IsManual = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201634,
                    IsActive = true,
                    Name =
                        @"п. 17.5 Порядка Приказа: Несоответствие представленных заявителем документов, установленным требованиям, либо представление заявителем противоречивых или недостоверных сведений, либо представленные документы утратили силу в случае, если в документах указан срок их действия или срок их действия установлен действующим законодательством.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201641,
                    IsActive = true,
                    Name = @"Отозвано по инициативе заявителя.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.CancelByApplicant
                });

            // 2017 год
            context.DeclineReason.AddOrUpdate(d => d.Id,
                new DeclineReason
                {
                    Id = 201701,
                    IsActive = false,
                    Name =
                        @"Непредставление заявителем оригиналов документов, подтверждающих сведения, указанные в заявлении, и в срок не позднее 10 рабочих дней со дня получения данного уведомления.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201801,
                    IsActive = false,
                    Name =
                        @"Непредставление оригиналов документов, подтверждающих сведения, указанные в заявлении в течение 10 рабочих дней со дня получения соответствующего об этом уведомления.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201702,
                    IsActive = false,
                    Name =
                        @"Представление документов, несоответствующим требованиям, установленным правовыми актами Российской Федерации, правовыми актами города Москвы, противоречивых или недостоверных сведений, либо утрата силы предоставленных документов в случае, если в документах указан срок их действия или срок их действия установлен законодательством.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201703,
                    IsActive = false,
                    Name =
                        @"Наличие в отношении одного и того же ребёнка, лица из числа детей-сирот и детей, оставшихся без попечения родителей, сопровождающего лица сведений о предоставлении бесплатной путевки для отдыха и оздоровления в другие организации отдыха и оздоровления в текущем календарном году, сведений о выплате компенсации за самостоятельно приобретенную родителями иными законными представителями путевку для отдыха и оздоровления или предоставлении в текущем календарном году сертификата на получение выплаты на самостоятельную организацию отдыха и оздоровления.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201803,
                    IsActive = false,
                    Name =
                        "Наличие в отношении одного и того же ребёнка, лица из числа детей-сирот и детей, оставшихся без попечения родителей, другого заявления о предоставлении услуги по отдыху и оздоровлению в текущем календарном году.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201704,
                    IsActive = false,
                    Name =
                        @"Неучастие заявителя во втором этапе заявочной кампании в целях организации отдыха и оздоровления, предусматривающей необходимость выбора заявителем конкретной организации отдыха и оздоровления.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201705,
                    IsActive = true,
                    Name =
                        @"Отсутствие квоты на отдых и оздоровление.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201706,
                    IsActive = false,
                    Name =
                        @"Отсутствие подтверждения льготной категории.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201707,
                    IsActive = false,
                    Name =
                        @"Отсутствие подтверждения получения ежемесячного пособия на ребёнка.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201708,
                    IsActive = false,
                    Name =
                        @"Наличие сведений о нарушениях правил отдыха и оздоровления, правил перевозок пассажиров, правил пребывания в организациях отдыха и оздоровления в период отдыха и оздоровления в прошедшем и (или) текущем календарном году ребёнком, сопровождающим лицом (в случае организации совместного выездного отдыха), лицом из числа детей-сирот и детей, оставшихся без попечения родителей.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201808,
                    IsActive = false,
                    Name =
                        @"Наличие сведений о нарушениях правил отдыха и оздоровления, правил перевозок пассажиров, правил пребывания в организациях отдыха и оздоровления в период отдыха и оздоровления в текущем календарном году ребёнком, сопровождающим лицом (в случае организации совместного выездного отдыха), лицом из числа детей-сирот и детей, оставшихся без попечения родителей.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201709,
                    IsActive = false,
                    Name =
                        @"Наличие сведений о нарушениях сопровождающим лицом в прошедшем и (или) текущем календарном году обязательств, предусмотренных соглашением об осуществлении обязанностей по сопровождению детей во время отдыха и оздоровления родителем или иным законным представителем либо доверенным лицом для сопровождения во время отдыха и оздоровления, заключенным с ГАУК «МОСГОРТУР».",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201809,
                    IsActive = false,
                    Name =
                        @"Наличие сведений о нарушениях сопровождающим лицом в текущем календарном году обязательств, предусмотренных соглашением об осуществлении обязанностей по сопровождению детей во время отдыха и оздоровления родителем или иным законным представителем либо доверенным лицом для сопровождения во время отдыха и оздоровления, заключенным с ГАУК «МОСГОРТУР».",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201710,
                    IsActive = false,
                    Name =
                        @"Наличие сведений о неосуществлении отдыха и оздоровления без уважительных причин, на основании предоставленной в прошедшем и (или) текущем календарном году путевки для отдыха и оздоровления с оплатой стоимости путевки для отдыха и оздоровления за счет средств бюджета города Москвы.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201810,
                    IsActive = false,
                    Name =
                        @"Наличие сведений о неосуществлении отдыха и оздоровления без уважительных причин, на основании предоставленной в текущем календарном году путевки для отдыха и оздоровления с оплатой стоимости путевки для отдыха и оздоровления за счет средств бюджета города Москвы.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201711,
                    IsActive = false,
                    Name =
                        @"Отсутствие места жительства ребёнка, лица из числа детей-сирот и детей, оставшихся без попечения родителей, в городе Москве.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201712,
                    IsActive = false,
                    Name =
                        @"Несоответствие возраста детей возрасту, предусмотренному для соответствующего вида отдыха и оздоровления.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201713,
                    IsActive = false,
                    Name =
                        @"Отсутствие места жительства ребёнка, в городе Москве.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201714,
                    IsActive = false,
                    Name =
                        @"Наличие сведений о нарушениях правил отдыха и оздоровления, правил перевозок пассажиров, правил пребывания в организациях отдыха и оздоровления в период отдыха и оздоровления в прошедшем и (или) текущем календарном году ребенком.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201715,
                    IsActive = true,
                    Name =
                        @"Наличие сведений о неосуществлении отдыха и оздоровления без уважительных причин, на основании предоставленной в прошедшем и (или) текущем календарном году бесплатной путевки для отдыха и оздоровления.",
                    IsManual = true,
                    FirstStage = true,
                    SecondStage = true,
                    StatusId = (long) StatusEnum.Reject
                },
                new DeclineReason
                {
                    Id = 201751,
                    IsActive = true,
                    Name =
                        @"Необходимость осуществления сопровождающим лицом ухода за больным членом семьи.",
                    FirstStage = true,
                    SecondStage = true,
                    IsManual = true,
                    StatusId = (long) StatusEnum.CancelByApplicant
                    //ValidReasons = true
                },
                new DeclineReason
                {
                    Id = 201752,
                    IsActive = true,
                    Name =
                        @"Карантин ребёнка, карантин лица из числа детей-сирот и детей, оставшихся без попечения родителей, карантин лица, проживающего совместно с ребёнком, лицом из числа детей-сирот и детей, оставшихся без попечения родителей, а также в случае организации совместного выездного отдых карантин сопровождающего лица.",
                    FirstStage = true,
                    SecondStage = true,
                    IsManual = true,
                    StatusId = (long) StatusEnum.CancelByApplicant
                },
                new DeclineReason
                {
                    Id = 201753,
                    IsActive = true,
                    Name =
                        @"Смерть близкого родственника (родителя, бабушки, дедушки, брата, сестры, дяди, тети).",
                    FirstStage = true,
                    SecondStage = true,
                    IsManual = true,
                    StatusId = (long) StatusEnum.CancelByApplicant
                },
                new DeclineReason
                {
                    Id = 201754,
                    IsActive = true,
                    Name =
                        @"Отказ от поданного заявления, в установленный Порядком срок.",
                    FirstStage = true,
                    SecondStage = true,
                    IsManual = true,
                    StatusId = (long) StatusEnum.CancelByApplicant
                },
                new DeclineReason
                {
                    Id = 201755,
                    IsActive = true,
                    Name =
                        @"Отказ от путевки, в установленный Порядком срок.",
                    FirstStage = true,
                    SecondStage = true,
                    IsManual = true,
                    StatusId = (long) StatusEnum.CancelByApplicant
                },
                new DeclineReason
                {
                    Id = 201756,
                    IsActive = true,
                    Name =
                        @"Заболевание, травма сопровождающего лица.",
                    FirstStage = true,
                    SecondStage = true,
                    IsManual = true,
                    StatusId = (long) StatusEnum.CancelByApplicant
                },
                new DeclineReason
                {
                    Id = 201757,
                    IsActive = true,
                    Name =
                        @"Заболевание, травма ребёнка, лица из числа детей-сирот и детей, оставшихся без попечения родителей.",
                    FirstStage = true,
                    SecondStage = true,
                    IsManual = true,
                    StatusId = (long) StatusEnum.CancelByApplicant
                },
                new DeclineReason
                {
                    Id = 201758,
                    IsActive = true,
                    Name =
                        "Получение детьми, санаторно-курортного лечения или реабилитации в то же время, на которое предоставлена бесплатная путевка для отдыха и оздоровления.",
                    FirstStage = true,
                    SecondStage = true,
                    IsManual = true,
                    StatusId = (long) StatusEnum.CancelByApplicant
                }
            );

            context.SaveChanges();

            var drs = context.DeclineReason.Where(d => d.IsActive).Where(d =>
                d.Id >= 201700 && d.Id <= 201713 ||
                d.Id >= 201627 && d.Id <= 201627 ||
                d.Id >= 201751 && d.Id <= 201758 ||
                d.Id > 201800 && d.Id <= 201912 ||
                d.Id > 201918 && d.Id <= 201919 ||
                d.Id > 202000 && d.Id <= 202006 ||
                d.Id > 202100 && d.Id <= 202110
            ).ToList();
            foreach (var tr in trs)
            {
                var localTr = tr;
                if (localTr.DeclineReasons == null)
                {
                    var id = localTr.Id;
                    context.Entry(localTr).State = EntityState.Detached;
                    localTr = context.TypeOfRest.FirstOrDefault(t => t.Id == id);
                }

                localTr?.DeclineReasons.Clear();
                localTr?.DeclineReasons.AddRange(drs);
            }

            context.SaveChanges();

            drs = context.DeclineReason.Where(d =>
                new long[] {201701, 201702, 201706, 201714, 201715}.Contains(d.Id) ||
                d.Id >= 201627 && d.Id <= 201627 || d.Id >= 201751 && d.Id <= 201758).ToList();

            var typeOfRest =
                context.TypeOfRest.FirstOrDefault(t => t.Id == (long) TypeOfRestEnum.ChildRestFederalCamps);
            if (typeOfRest != null && typeOfRest.DeclineReasons == null)
            {
                var id = typeOfRest.Id;
                context.Entry(typeOfRest).State = EntityState.Detached;
                typeOfRest = context.TypeOfRest.FirstOrDefault(t => t.Id == id);
            }

            typeOfRest?.DeclineReasons.Clear();
            typeOfRest?.DeclineReasons.AddRange(drs);

            context.SaveChanges();

            var covidDeclineReason =
                context.DeclineReason.FirstOrDefault(
                    ss => ss.Id == (long) DeclineReasonEnum.NonParticipationOfApplicant);

            covidDeclineReason?.TypeOfRests?.Clear();
            covidDeclineReason?.TypeOfRests?.AddRange(covidTypesOfRest);
            context.SaveChanges();

            covidDeclineReason =
                context.DeclineReason.FirstOrDefault(ss =>
                    ss.Id == (long) DeclineReasonEnum.RefuseAllVariantsCovid2020);
            if (covidDeclineReason != null)
            {
                if (covidDeclineReason.TypeOfRests == null)
                {
                    covidDeclineReason.TypeOfRests = new List<TypeOfRest>(covidRefuseAllVariantsTypesOfRest);
                }
                else
                {
                    covidDeclineReason.TypeOfRests.Clear();
                    covidDeclineReason.TypeOfRests.AddRange(covidRefuseAllVariantsTypesOfRest);
                }

                context.SaveChanges();
            }

            SetEidAndLastUpdateTicks(context.DeclineReason.ToList());
            context.SaveChanges();
        }
    }
}
