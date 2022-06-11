using DbUp.Engine;

namespace banning_things_in_postgresql
{
    public sealed class PreventTimestampWithoutTimezone : IScriptPreprocessor
    {
        public string Process(string contents)
        {
            var modified =
                $@"
            DO
            $do$
            BEGIN

            {contents}

            IF EXISTS
            (
                SELECT 1 FROM pg_attribute WHERE atttypid = 1114
            )
            THEN
                RAISE EXCEPTION 'your error message';
            END IF;

            END
            $do$
            ";

            return modified;
        }
    }
}