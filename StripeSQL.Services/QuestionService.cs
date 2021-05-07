using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StripeSQL.Data;
using StripeSQL.Models;
using StripeSQL.Services;
using StripeSQLRequestManager.Models;

namespace StripeSQL.Services
{
    public class QuestionService
    {
        private readonly Guid _userId;

        public QuestionService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateQuestion(QuestionCreate model)
        {
            var entity =
                new Question()
                {
                    OwnerId = _userId,
                    Content = model.Content,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Questions.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<QuestionListItem> GetQuestions()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Questions
                        .Where(e => e.OwnerId == _userId).ToList()
                        .Select(
                            e =>
                                new QuestionListItem
                                {
                                    QuestionId = e.QuestionId,
                                    Content = e.Content,
                                    CreatedUtc = e.CreatedUtc,
                                    SQL = e.SQLCollection.Select(r => new SQLCodeListItem
                                    {
                                        SQL = r.SQL
                                    }).ToList(),
                                    DateRanges = e.DateRange.Select(r => new DateRangeListItem
                                    {
                                        StartDate = r.StartDate,
                                        EndDate = r.EndDate
                                    }).ToList(),
                                }
                        );

                return query.ToArray();
            }
        }
        public QuestionDetail GetQuestionById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Questions
                        .Single(e => e.QuestionId == id && e.OwnerId == _userId);
                return
                    new QuestionDetail
                    {
                        QuestionId = entity.QuestionId,
                        Content = entity.Content,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateQuestion(QuestionEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Questions
                        .Single(e => e.QuestionId == model.QuestionId && e.OwnerId == _userId);

                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteQuestion(int questionId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Questions
                        .Single(e => e.QuestionId == questionId && e.OwnerId == _userId);

                ctx.Questions.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
