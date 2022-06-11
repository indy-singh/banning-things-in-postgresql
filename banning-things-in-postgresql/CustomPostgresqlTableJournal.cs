using System;
using DbUp.Engine.Output;
using DbUp.Engine.Transactions;
using DbUp.Postgresql;

namespace banning_things_in_postgresql
{
    public sealed class CustomPostgresqlTableJournal : PostgresqlTableJournal
    {
        public CustomPostgresqlTableJournal(Func<IConnectionManager> connectionManager, Func<IUpgradeLog> logger, string schema, string tableName) : base(connectionManager, logger, schema, tableName)
        {
        }

        protected override string CreateSchemaTableSql(string quotedPrimaryKeyName)
        {
            return
                $@"CREATE TABLE {FqSchemaTableName}
(
    schemaversionsid bigserial PRIMARY KEY,
    scriptname character varying(255) NOT NULL,
    applied timestamp with time zone NOT NULL
)";
        }
    }
}