using DbUp;
using DbUp.Postgresql;

namespace banning_things_in_postgresql
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var upgradeEngineBuilder = DeployChanges.To
                .PostgresqlDatabase("Server=localhost;Database=postgres;Username=postgres;Password=admin")
                .WithPreprocessor(new PreventTimestampWithoutTimezone())
                .WithScriptsFromFileSystem("./Migrations")
                .WithTransactionPerScript()
                .LogToConsole();

            upgradeEngineBuilder.Configure(c => c.Journal = new CustomPostgresqlTableJournal(() => c.ConnectionManager, () => c.Log, "public", "schema_version"));

            upgradeEngineBuilder.Build().PerformUpgrade();
        }
    }
}
