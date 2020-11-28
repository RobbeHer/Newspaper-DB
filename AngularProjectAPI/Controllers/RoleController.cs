using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularProjectAPI.Data;
using AngularProjectAPI.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace AngularProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-AngularAPI-F532DF6B-C09A-4528-B535-CAD19D110ECE;Trusted_Connection=True;MultipleActiveResultSets=true";
        private static IDbConnection db = new SqlConnection(connectionString);

        private readonly NewsContext _context;

        public RoleController(NewsContext context)
        {
            _context = context;
        }

        // GET: api/Role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        [HttpGet("role-of-type/{type}")]
        public async Task<ActionResult<Role>> RoleOfType(String type)
        {
            string sql = "select * from \"role\" where name = '" + type + "'";
            List<Role> role = (List<Role>)db.Query<Role>(sql);
            return role[0];
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

    }
}
