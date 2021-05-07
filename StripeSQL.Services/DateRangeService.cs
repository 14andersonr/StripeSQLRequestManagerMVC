using StripeSQL.Data;
using StripeSQL.Models;
using StripeSQLRequestManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StripeSQL.Services
{
    public class DateRangeService
    {
        private readonly Guid _userId;

        public DateRangeService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateDateRange(DateRangeCreate model)
        {
            var entity =
                new DateRange()
                {
                    OwnerId = _userId,
                    QuestionId = model.QuestionId,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.DateRanges.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<DateRangeListItem> GetDateRange()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .DateRanges
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new DateRangeListItem
                                {
                                    DateRangeId = e.DateRangeId,
                                    StartDate = e.StartDate,
                                    EndDate = e.EndDate,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }
        public DateRangeDetail GetDateRangeById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .DateRanges
                        .Single(e => e.DateRangeId == id && e.OwnerId == _userId);
                return
                    new DateRangeDetail
                    {
                        DateRangeId = entity.DateRangeId,
                        StartDate = entity.StartDate,
                        EndDate = entity.EndDate,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateDateRange(DateRangeEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .DateRanges
                        .Single(e => e.DateRangeId == model.DateRangeId && e.OwnerId == _userId);

                entity.StartDate = model.StartDate;
                entity.EndDate = model.EndDate;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteDateRange(int dateRangeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .DateRanges
                        .Single(e => e.DateRangeId == dateRangeId && e.OwnerId == _userId);

                ctx.DateRanges.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public List<SelectListItem> QuestionsForDateRange()
        {
            //Look at Questions Database
            QuestionService questionService = new QuestionService(_userId);

            var questionEnumerable = questionService.GetQuestions();

            //Package as SelectListItems

            List<SelectListItem> QuestionList = new List<SelectListItem>();

            foreach (var item in questionEnumerable) 
            {
                QuestionList.Add(new SelectListItem { Text = item.Content, Value = item.QuestionId.ToString() });
                //new SelectListItem { Text = item.Content, Value = item.QuestionId.ToString() };
            }

            return QuestionList;
        }
    }
}
