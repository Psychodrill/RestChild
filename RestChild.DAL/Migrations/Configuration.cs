using System.Data.Entity.Migrations;

namespace RestChild.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            CommandTimeout = int.MaxValue;
        }

        protected override void Seed(Context context)
        {
            Configurations.Configuration.Seed(context);
        }

    }
}
