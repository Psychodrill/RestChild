using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Типы проверки документов в базовом регистре
        /// </summary>
        private static void ExchangeBaseRegistryType(Context context)
        {
            // новый вид проверки документов
            if (!context.ExchangeBaseRegistryType.Any(c => c.Id == (long) ExchangeBaseRegistryTypeEnum.Snils2040))
            {
                context.ExchangeBaseRegistryType.AddOrUpdate(r => r.Id,
                    new ExchangeBaseRegistryType
                    {
                        Id = (long) ExchangeBaseRegistryTypeEnum.Benefit,
                        Name = "Наличие льготной категории",
                        Eid = (long) ExchangeBaseRegistryTypeEnum.Benefit
                    },
                    new ExchangeBaseRegistryType
                    {
                        Id = (long) ExchangeBaseRegistryTypeEnum.Payments,
                        IsDeleted = true,
                        SendMessage = false,
                        Name = "Выплаты",
                        Eid = (long) ExchangeBaseRegistryTypeEnum.Payments
                    },
                    new ExchangeBaseRegistryType
                    {
                        Id = (long) ExchangeBaseRegistryTypeEnum.Relationship,
                        Name = "Проверка родства",
                        Eid = (long) ExchangeBaseRegistryTypeEnum.Relationship
                    },
                    new ExchangeBaseRegistryType
                    {
                        Id = (long) ExchangeBaseRegistryTypeEnum.Snils,
                        IsDeleted = true,
                        SendMessage = false,
                        Name = "Проверка СНИЛС",
                        Eid = (long) ExchangeBaseRegistryTypeEnum.Snils
                    },
                    new ExchangeBaseRegistryType
                    {
                        Id = (long) ExchangeBaseRegistryTypeEnum.Snils2040,
                        Name = "Проверка СНИЛС",
                        Eid = (long) ExchangeBaseRegistryTypeEnum.Snils2040
                    },
                    new ExchangeBaseRegistryType
                    {
                        Id = (long) ExchangeBaseRegistryTypeEnum.CpmpkExchange,
                        Name = "Проверка по ЦПМПК",
                        IsDeleted = true,
                        Eid = (long) ExchangeBaseRegistryTypeEnum.CpmpkExchange
                    },
                    new ExchangeBaseRegistryType
                    {
                        Id = (long) ExchangeBaseRegistryTypeEnum.PassportDataBySNILS,
                        Name = "Запроса паспортного досье по СНИЛС",
                        Eid = (long) ExchangeBaseRegistryTypeEnum.PassportDataBySNILS
                    },
                    new ExchangeBaseRegistryType
                    {
                        Id = (long) ExchangeBaseRegistryTypeEnum.SNILSByFio,
                        Name = "Запроса СНИЛС по ФИО",
                        Eid = (long) ExchangeBaseRegistryTypeEnum.SNILSByFio
                    },
                    new ExchangeBaseRegistryType
                    {
                        Id = (long) ExchangeBaseRegistryTypeEnum.RelationshipSmev,
                        Name = "Предоставление из ЕГР ЗАГС сведений об актах гражданского состояния",
                        Eid = (long) ExchangeBaseRegistryTypeEnum.RelationshipSmev
                    }
                );
            }

            // новый вид проверки документов
            if (!context.ExchangeBaseRegistryType.Any(c =>
                c.Id == (long) ExchangeBaseRegistryTypeEnum.PassportRegistration))
            {
                context.ExchangeBaseRegistryType.AddOrUpdate(r => r.Id,
                    new ExchangeBaseRegistryType
                    {
                        Id = (long) ExchangeBaseRegistryTypeEnum.PassportRegistration,
                        Name = "Проверка адреса регистрации ребёнка (МВД)",
                        IsDeleted = false,
                        SendMessage = false,
                        Eid = (long) ExchangeBaseRegistryTypeEnum.PassportRegistration
                    }
                );
            }

            // новый вид проверки документов
            if (!context.ExchangeBaseRegistryType.Any(c =>
                c.Id == (long) ExchangeBaseRegistryTypeEnum.GetPassportRegistration))
            {
                context.ExchangeBaseRegistryType.AddOrUpdate(r => r.Id,
                    new ExchangeBaseRegistryType
                    {
                        Id = (long) ExchangeBaseRegistryTypeEnum.GetPassportRegistration,
                        Name = "Получение регистрации по месту жительства",
                        IsDeleted = false,
                        SendMessage = false,
                        Eid = (long) ExchangeBaseRegistryTypeEnum.GetPassportRegistration
                    }
                );
            }

            // новый вид проверки документов
            if (!context.ExchangeBaseRegistryType.Any(c =>
                c.Id == (long) ExchangeBaseRegistryTypeEnum.AisoLegalRepresentationCheck))
            {
                context.ExchangeBaseRegistryType.AddOrUpdate(r => r.Id,
                    new ExchangeBaseRegistryType
                    {
                        Id = (long) ExchangeBaseRegistryTypeEnum.AisoLegalRepresentationCheck,
                        Name = "Проверка законного представительства внутри АИС ДО",
                        IsDeleted = false,
                        SendMessage = false,
                        Eid = (long) ExchangeBaseRegistryTypeEnum.AisoLegalRepresentationCheck
                    }
                );
            }
        }
    }
}
