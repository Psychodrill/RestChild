using System;
using System.Data.Entity.Migrations;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///      Категории происшествий
        /// </summary>
        private static void CategoryIncidents(Context context)
        {
            context.CategoryIncident.AddOrUpdate(r => r.Id,
                new CategoryIncident
                {
                    Id = 1,
                    Eid = 1,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Трансфер"
                },
                new CategoryIncident
                {
                    Id = 2,
                    Eid = 2,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Питание"
                },
                new CategoryIncident
                {
                    Id = 3,
                    Eid = 3,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Здоровье"
                },
                new CategoryIncident
                {
                    Id = 4,
                    Eid = 4,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Персонал"
                },
                new CategoryIncident
                {
                    Id = 5,
                    Eid = 5,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Условия проживания"
                },
                new CategoryIncident
                {
                    Id = 6,
                    Eid = 6,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Информирование"
                },
                new CategoryIncident
                {
                    Id = 7,
                    Eid = 7,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Коммерческая деятельность"
                },
                new CategoryIncident
                {
                    Id = 8,
                    Eid = 8,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Задержка информации по видам трансфера",
                    ParentId = 1
                },
                new CategoryIncident
                {
                    Id = 9,
                    Eid = 9,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Задержка транспорта",
                    ParentId = 1
                },
                new CategoryIncident
                {
                    Id = 10,
                    Eid = 10,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Изменение даты отправки",
                    ParentId = 1
                },
                new CategoryIncident
                {
                    Id = 11,
                    Eid = 11,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Изменение времени сбора",
                    ParentId = 1
                },
                new CategoryIncident
                {
                    Id = 12,
                    Eid = 12,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Разделение одной семьи на разные рейсы",
                    ParentId = 1
                },
                new CategoryIncident
                {
                    Id = 13,
                    Eid = 13,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Отсутствие в списках на выезд коммерческих клиентов",
                    ParentId = 1
                },
                new CategoryIncident
                {
                    Id = 14,
                    Eid = 14,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Некачественное питание в поезде",
                    ParentId = 1
                },
                new CategoryIncident
                {
                    Id = 15,
                    Eid = 15,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Нарушения здоровья детей во время поездки",
                    ParentId = 1
                },
                new CategoryIncident
                {
                    Id = 16,
                    Eid = 16,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Увеличенное время пути",
                    ParentId = 1
                },
                new CategoryIncident
                {
                    Id = 17,
                    Eid = 17,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Задержка трансфера от аэропорта/автостанции до базы отдыха",
                    ParentId = 1
                },
                new CategoryIncident
                {
                    Id = 18,
                    Eid = 18,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Отсутствие трансфера от базы отдыха до моря",
                    ParentId = 1
                },
                new CategoryIncident
                {
                    Id = 19,
                    Eid = 19,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Выезд утром / днем последнего дня пребывания на базе (сокращение времени отдыха на сутки)",
                    ParentId = 1
                },
                new CategoryIncident
                {
                    Id = 20,
                    Eid = 20,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Однообразное питание",
                    ParentId = 2
                },
                new CategoryIncident
                {
                    Id = 21,
                    Eid = 21,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Нарушение хранения овощей/фруктов",
                    ParentId = 2
                },
                new CategoryIncident
                {
                    Id = 22,
                    Eid = 22,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Продукты с истекшим сроком годности в столовой",
                    ParentId = 2
                },
                new CategoryIncident
                {
                    Id = 23,
                    Eid = 23,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Нарушения санитарной обстановки в местах общепита",
                    ParentId = 2
                },
                new CategoryIncident
                {
                    Id = 24,
                    Eid = 24,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Отсутствие контроля раздачи (вынос блюд из столовой, дети отбирают выпечку у других и т.п.)",
                    ParentId = 2
                },
                new CategoryIncident
                {
                    Eid = 25,
                    Id = 25,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Не оказанная должным образом медицинская помощь",
                    ParentId = 3
                },
                new CategoryIncident
                {
                    Eid = 26,
                    Id = 26,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Сокрытие отдыхающими факта плохого самочувствия",
                    ParentId = 3
                },
                new CategoryIncident
                {
                    Eid = 27,
                    Id = 27,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Фальсификация данных в медицинском журнале",
                    ParentId = 3
                },
                new CategoryIncident
                {
                    Id = 28,
                    Eid = 28,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Отсутствие медикаментов / медицинского оборудования",
                    ParentId = 3
                },
                new CategoryIncident
                {
                    Id = 29,
                    Eid = 29,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Детские конфликты",
                    ParentId = 4
                },
                new CategoryIncident
                {
                    Id = 30,
                    Eid = 30,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Конфликты между персоналом – вожатыми/руководителями смен/сотрудниками базы отдыха",
                    ParentId = 4
                },
                new CategoryIncident
                {
                    Id = 31,
                    Eid = 31,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Конфликты отдыхающих с персоналом",
                    ParentId = 4
                },
                new CategoryIncident
                {
                    Id = 32,
                    Eid = 32,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Нарушения отдыхающими правил пребывания",
                    ParentId = 4
                },
                new CategoryIncident
                {
                    Id = 33,
                    Eid = 33,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Несоответствие инфраструктуры базы отдыха параметрам ТЗ",
                    ParentId = 5
                },
                new CategoryIncident
                {
                    Id = 34,
                    Eid = 34,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Размещение отдыхающих в номерах, несоответствующих по площади размещаемому кол-ву человек",
                    ParentId = 5
                },
                new CategoryIncident
                {
                    Id = 35,
                    Eid = 35,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Несоблюдения температурного режима БО",
                    ParentId = 5
                },
                new CategoryIncident
                {
                    Id = 36,
                    Eid = 36,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Предоставление сломанной, устаревшей мебели и/или оборудования",
                    ParentId = 5
                },
                new CategoryIncident
                {
                    Id = 37,
                    Eid = 37,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Присутствие на территории БО объектов, угрожающих жизни и здоровью отдыхающих",
                    ParentId = 5
                },
                new CategoryIncident
                {
                    Id = 38,
                    Eid = 38,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Отсутствие информирования для лиц, не пользующихся системой электронной заявки через Портал",
                    ParentId = 6
                },
                new CategoryIncident
                {
                    Id = 39,
                    Eid = 39,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Некорректная база телефонных номеров АИС ДСО (автоподстановка номеров, указанных заявителями при регистрации на Портале, часто – более двух лет назад)",
                    ParentId = 6
                },
                new CategoryIncident
                {
                    Id = 40,
                    Eid = 40,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Высокая цена предоставляемых услуг",
                    ParentId = 7
                },
                new CategoryIncident
                {
                    Id = 41,
                    Eid = 41,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Перегрузка телефонных линий",
                    ParentId = 7
                },
                new CategoryIncident
                {
                    Id = 42,
                    Eid = 42,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Name = "Сбой в программе \"Самотур\"",
                    ParentId = 7
                });
        }
    }
}
