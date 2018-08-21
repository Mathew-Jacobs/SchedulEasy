using SchedulEasy.Data;
using SchedulEasy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulEasy.Services
{
    public class BusyDayService
    {
        private readonly string _userID;

        public BusyDayService(string userID)
        {
            _userID = userID;
        }

        public BusyDayCreate GetCreateByDate(DateTime date)
        {
                return new BusyDayCreate
                {
                    DefaultDay = date.ToString("yyyy-MM-dd"),
                    Busy = DateTime.Now,
                    BusyEnd = DateTime.Now,
                    Description = ""
                };
        }

        public bool CreateBusyDay(BusyDayCreate model)
        {
            if (model.BusyEnd == null || model.BusyEnd < model.Busy)
            {
                model.BusyEnd = model.Busy;
            }
            var entity =
                new BusyDay()
                {
                    UserID = _userID,
                    Busy = model.Busy,
                    BusyEnd = (DateTimeOffset)model.BusyEnd,
                    Description = model.Description
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.BusyDays.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BusyDayListItem> GetBusyDays()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .BusyDays
                        .Where(e => e.UserID == _userID)
                        .Select(
                        e =>
                            new BusyDayListItem
                            {
                                BusyDayID = e.BusyDayID,
                                Busy = e.Busy,
                                BusyEnd = e.BusyEnd,
                                Description = e.Description
                            }
                        );
                return query.ToArray();
            }
        }

        public BusyDayDetail GetBusyDayByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .BusyDays
                        .Single(e => e.BusyDayID == id && e.UserID == _userID);
                return new BusyDayDetail
                {
                    BusyDayID = entity.BusyDayID,
                    Busy = entity.Busy,
                    BusyEnd = entity.BusyEnd,
                    Description = entity.Description
                };
            }
        }

        public bool UpdateBusyDay(BusyDayEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .BusyDays
                        .Single(e => e.BusyDayID == model.BusyDayID && e.UserID == _userID);

                entity.Busy = model.Busy;
                entity.BusyEnd = model.BusyEnd;
                entity.Description = model.Description;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteDay(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .BusyDays
                        .Single(e => e.BusyDayID == id && e.UserID == _userID);
                ctx.BusyDays.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
