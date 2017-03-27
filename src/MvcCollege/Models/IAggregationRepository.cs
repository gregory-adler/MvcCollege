using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcCollege.Models;
using MvcCollege.Models.SchoolViewModels;

namespace MvcCollege.Models
{
    public interface IAggregationRepository
    {
        Task<IList<EnrollmentDateGroup>> groupByEnrollmentDate();
    }
}