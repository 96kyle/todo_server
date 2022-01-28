using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication2.Databases;

namespace WebApplication2.Controllers
{
    public class BaseController : ControllerBase
    {
        public DatabaseContext DatabaseContext { get; }

        public BaseController(DatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public string UserIndex
        {
            get
            {
                return User.Claims.Single(p => p.Type == ClaimTypes.NameIdentifier).Value;
            }
        }

        public User Self
        {
            get
            {
                return DatabaseContext.Users.FirstOrDefault(p => p.Index.ToString() == UserIndex);
            }
        }
    }
}