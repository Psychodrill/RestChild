using System.ComponentModel.DataAnnotations;

namespace RestChild.Comon.Enumeration
{
    public enum RestCategoryEnum
    {
        [Display(Name = "Ребёнок")] Child = 0,
        [Display(Name = "Преподаватель")] Teacher = 1,
        [Display(Name = "Сопровождающий")] Attendant = 2
    }
}
