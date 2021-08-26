using System;
using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Виды связи
        /// </summary>
        private static void TypeOfLinkPeople(Context context)
        {
            context.TypeOfLinkPeople.AddOrUpdate(t => t.Id,
                new TypeOfLinkPeople
                {
                    Id = (long) TypeOfLinkPeopleEnum.Child,
                    Name = "Ребёнок"
                },
                new TypeOfLinkPeople
                {
                    Id = (long) TypeOfLinkPeopleEnum.Counselor,
                    Name = "Вожатый"
                },
                new TypeOfLinkPeople
                {
                    Id = (long) TypeOfLinkPeopleEnum.SeniorCounselor,
                    Name = "Старший вожатый"
                },
                new TypeOfLinkPeople
                {
                    Id = (long) TypeOfLinkPeopleEnum.SwingCounselor,
                    Name = "Подменный вожатый"
                },
                new TypeOfLinkPeople
                {
                    Id = (long) TypeOfLinkPeopleEnum.Administrator,
                    Name = "Администратор"
                },
                new TypeOfLinkPeople
                {
                    Id = (long) TypeOfLinkPeopleEnum.Attendant,
                    Name = "Сопровождающий"
                },
                new TypeOfLinkPeople
                {
                    Id = (long) TypeOfLinkPeopleEnum.Upbringer,
                    Name = "Воспитатель"
                },
                new TypeOfLinkPeople
                {
                    Id = (long) TypeOfLinkPeopleEnum.JuniorUpbringer,
                    Name = "Младший воспитатель"
                },
                new TypeOfLinkPeople
                {
                    Id = (long) TypeOfLinkPeopleEnum.UpbringerAssistant,
                    Name = "Помощник воспитателя"
                });

            SetEidAndLastUpdateTicks(context.TypeOfLinkPeople.ToList());
            context.SaveChanges();
        }
    }
}
