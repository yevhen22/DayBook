using DayBook.DataLayer;
using DayBook.Models;
using Microsoft.AspNet.Identity;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DayBook.JobsScheduler
{
    public class AccountRemover : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            using (DataRepository<DeleteUserModel> repo = new DataRepository<DeleteUserModel>())
            {
                var accountForDelete = await repo.GetRecords();

                var deleteNow = accountForDelete
                    .Where(p => (DateTime.Now - p.AddedUserTime).TotalDays > 2);
                if (deleteNow != null)
                {
                    using (ApplicationDbContext _db = new ApplicationDbContext())
                    {
                        foreach (var item in deleteNow)
                        {
                            var user = _db.Users.FirstOrDefault(p => p.Id.Equals(item.UserId));
                            if (user != null)
                            {
                                _db.Users.Remove(user);
                            }
                            await _db.SaveChangesAsync();
                        }
                    }
                }
            }
        }

    }
}