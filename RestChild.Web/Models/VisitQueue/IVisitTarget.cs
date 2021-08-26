namespace RestChild.Web.Models.VisitQueue
{
   public interface IVisitTarget
   {
      long Id { get; }

      string Name { get; }

      string Description { get; }
   }
}
