using StripeSQL.Data;
using StripeSQL.Models;
using StripeSQLRequestManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeSQL.Services
{
    class SQLCodeService
    {
        private readonly Guid _userId;

        public SQLCodeService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateSQLCode(SQLCodeCreate model)
        {
            var entity =
                new SQLCode()
                {
                    OwnerId = _userId,
                    DateRangeId = model.DateRangeId,
                    QuestionId = model.QuestionId,
                    SQL = model.SQL,
                    Resolved = model.Resolved,
                    ResolvedDate = model.ResolvedDate,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.SQLCodes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<SQLCodeListItem> GetSQLCodes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .SQLCodes
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new SQLCodeListItem
                                {
                                    SQLCodeId = e.SQLCodeId,
                                    SQL = e.SQL,
                                    Resolved = e.Resolved,
                                    ResolvedDate = e.ResolvedDate,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }
        public SQLCodeDetail GetSQLCodeById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .SQLCodes
                        .Single(e => e.SQLCodeId == id && e.OwnerId == _userId);
                return
                    new SQLCodeDetail
                    {
                        SQLCodeId = entity.SQLCodeId,
                        SQL = entity.SQL,
                        Resolved = entity.Resolved,
                        ResolvedDate = entity.ResolvedDate,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateSQLCode(SQLCodeEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .SQLCodes
                        .Single(e => e.SQLCodeId == model.SQLCodeId && e.OwnerId == _userId);

                entity.SQL = model.SQL;
                entity.Resolved = model.Resolved;
                entity.ResolvedDate = model.ResolvedDate;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteSQLCode(int sQLCodeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .SQLCodes
                        .Single(e => e.SQLCodeId == sQLCodeId && e.OwnerId == _userId);

                ctx.SQLCodes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
