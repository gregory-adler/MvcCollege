using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcCollege.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MvcCollege.Models
{
    public class CoursesRepository: ICoursesRepository
    {
        public SchoolContext db;
        public CoursesRepository(SchoolContext database)
        {
            db = database;
        }
    }
}
