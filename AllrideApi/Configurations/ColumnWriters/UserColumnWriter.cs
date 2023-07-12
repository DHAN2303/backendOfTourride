using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace AllrideApi.Configurations.ColumnWriterBases
{
    public class UserColumnWriter : ColumnWriterBase
    {
        public UserColumnWriter(/*NpgsqlDbType dbType, int? columnLength = null*/) : base(NpgsqlDbType.Varchar)
        {
        }

        public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
        {
            // Loglama sürecindeki property değerleri varsa bu şekilde elde ediyoruz
            // Loglama sürecinde UserName bilgisi varsa use name proepertysine atıcaz
            var(user,value) =  logEvent.Properties.FirstOrDefault(p=>p.Key == "email");
            return value?.ToString() ?? null;
        }
    }
}
/*
 *  Loglama sürecindeki elde edilen property değerlerini literatürde Enrich olarak geçiyor.
 *  O andaki property değerleri neyse property değerlerini  logEvent.Properties.FirstOrDefault(p=>p.Key == "email");
 *  buradan elde edebiliyoruz. 
 */