namespace EdisonWebMVC.Migrations
{
    using HireProSol.Models;
    using System.Data.Entity.Migrations;
    /// <summary>
    /// https://msdn.microsoft.com/en-us/data/jj591621.aspx
    /// Above is an important link for commnads of migration process
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(ApplicationDbContext context)
        {

        }
    }
}
