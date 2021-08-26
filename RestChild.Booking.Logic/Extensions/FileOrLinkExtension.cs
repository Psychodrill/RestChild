using RestChild.Comon;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using System.Collections.Generic;
using System.Linq;

namespace RestChild.Booking.Logic.Extensions
{
    /// <summary>
    ///     работа с файлами
    /// </summary>
    public static class FileOrLinkExtension
    {
        /// <summary>
        ///     сравнение файлов
        /// </summary>
        public static string Diff(this LinkToFile persisted, LinkToFile link, IUnitOfWork uw)
        {
            if (link == null)
                return string.Empty;

            persisted = persisted ?? uw.GetById<LinkToFile>(link.Id);

            if (persisted == null)
            {
                if (link.Files != null && link.Files.Any())
                {
                    return $"Добавлены файлы: {string.Join(", ", link.Files.Select(f => f.FileName))}";
                }

                return string.Empty;
            }

            persisted.Files = persisted.Files ?? new List<FileOrLink>();

            var filesToAdd = link.Files?.Where(f => f.Id == 0).ToList() ?? new List<FileOrLink>();
            var res = string.Empty;

            if (filesToAdd.Any())
            {
                res += $"Добавлены файлы: {string.Join(", ", filesToAdd.Select(f => f.FileName))}; ";
            }

            // для удаления.
            var fids = link.Files?.Where(f => f.Id > 0).Select(f => f.Id).ToList() ?? new List<long>();
            var fileToRemove = persisted.Files.Where(f => !fids.Contains(f.Id)).ToList();
            if (fileToRemove.Any())
            {
                res += $"Удалены файлы: {string.Join(", ", fileToRemove.Select(f => f.FileName))};";

            }

            return res;
        }

        /// <summary>
        ///     сохранение файлов
        /// </summary>
        public static LinkToFile SaveFiles(this LinkToFile persisted, LinkToFile link, IUnitOfWork uw)
        {
            if (link == null)
                return persisted;

            persisted = persisted ?? uw.GetById<LinkToFile>(link.Id);

            if (persisted == null)
            {
                persisted = uw.AddEntity(link);
                uw.SaveChanges();
                return persisted;
            }

            persisted.Files = persisted.Files ?? new List<FileOrLink>();

            var filesToAdd = link.Files?.Where(f => f.Id == 0).ToList() ?? new List<FileOrLink>();

            foreach (var file in filesToAdd)
            {
                file.LinkId = persisted.Id;
                persisted.Files.Add(uw.AddEntity(file));
            }

            // для удаления.
            var fids = link.Files?.Where(f => f.Id > 0).Select(f => f.Id).ToList() ?? new List<long>();
            var fileToRemove = persisted.Files.Where(f => !fids.Contains(f.Id)).ToList();
            foreach (var file in fileToRemove)
            {
                persisted.Files.Remove(file);
                uw.Delete(file);
            }

            return persisted;
        }
    }
}
