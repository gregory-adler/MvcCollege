using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcCollege.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcCollege.Models.SchoolViewModels;

namespace MvcCollege.Models
{
    public class AggregationRepository : IAggregationRepository
    {
        public SchoolContext db;
        public AggregationRepository(SchoolContext database)
        {
            db = database;
        }

        public async Task<IList<EnrollmentDateGroup>> groupByEnrollmentDate()
        {
            IQueryable<EnrollmentDateGroup> data =
            from student in db.Students
            group student by student.EnrollmentDate into dateGroup
            select new EnrollmentDateGroup()
            {
                EnrollmentDate = dateGroup.Key,
                StudentCount = dateGroup.Count()
            };

            return await data.AsNoTracking().ToListAsync();
        }
    }
}