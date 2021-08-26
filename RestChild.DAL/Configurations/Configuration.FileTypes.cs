using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Типы файлов
        /// </summary>
        private static void FileTypes(Context context)
        {
            context.FileType.AddOrUpdate(
                t => t.Id,
                new FileType {Id = (long) FileTypeEnum.Photo, Name = "Фотография", IsPhoto = true, ParentId = null},
                new FileType
                {
                    Id = (long) FileTypeEnum.Infrastructure,
                    Name = "Инфраструктура",
                    IsPhoto = true,
                    ParentId = null
                },
                new FileType {Id = (long) FileTypeEnum.Interior, Name = "Интерьеры", IsPhoto = true, ParentId = null},
                new FileType
                {
                    Id = (long) FileTypeEnum.TechObjects,
                    Name = "Технические объекты",
                    IsPhoto = true,
                    ParentId = null
                },
                new FileType
                {
                    Id = 5,
                    Name = "Инфраструктура",
                    IsPhoto = true,
                    ParentId = (long) FileTypeEnum.Infrastructure
                },
                new FileType {Id = 6, Name = "Интерьеры", IsPhoto = true, ParentId = (long) FileTypeEnum.Interior},
                new FileType
                {
                    Id = 7,
                    Name = "Технические объекты",
                    IsPhoto = true,
                    ParentId = (long) FileTypeEnum.TechObjects
                },
                new FileType {Id = (long) FileTypeEnum.Exterior, Name = "Экстерьеры", IsPhoto = true, ParentId = null},
                new FileType {Id = 9, Name = "Экстерьеры", IsPhoto = true, ParentId = (long) FileTypeEnum.Exterior},
                new FileType
                    {Id = (long) FileTypeEnum.Room, Name = "Фотографии номера", IsPhoto = true, ParentId = null},
                new FileType {Id = 11, Name = "Фотографии номера", IsPhoto = true, ParentId = (long) FileTypeEnum.Room},
                new FileType {Id = (long) FileTypeEnum.Link3DTour, Name = "Ссылка 3D тур", IsPhoto = false}
            );

            context.SaveChanges();

            SetEidAndLastUpdateTicks(context.FileType.ToList());
            context.SaveChanges();
        }
    }
}
