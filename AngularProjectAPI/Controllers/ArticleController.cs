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
    public class ArticleController : ControllerBase
    {
        //private static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-AngularAPI-F532DF6B-C09A-4528-B535-CAD19D110ECE;Trusted_Connection=True;MultipleActiveResultSets=true";
        //private static IDbConnection db = new SqlConnection(connectionString);
        
        private readonly NewsContext _context;

        public ArticleController(NewsContext context)
        {
            _context = context;
        }

        // GET: api/Article
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            return await _context.Articles.ToListAsync();
        }

        [HttpGet("articles-of-status/{status}")]
        public async Task<ActionResult<IEnumerable<Article>>> ArticlesOfStatus(String status)
        {
            var articles = await _context.Articles.Include("User").Include("Tag").Include("ArticleStatus").Where(s => s.ArticleStatus.Name == status).ToListAsync();

            if (articles == null)
            {
                return NotFound();
            }

            return articles;
        }

        [HttpGet("articles-of-user/{id}")]
        public async Task<ActionResult<IEnumerable<Article>>> ArticlesOfUser(int id)
        {
            var articles = await _context.Articles.Include("User").Include("Tag").Include("ArticleStatus").Where(s => s.UserID == id).ToListAsync();

            if (articles == null)
            {
                return NotFound();
            }

            return articles;
        }

        // GET: api/Article/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await _context.Articles.Include("Tag").SingleOrDefaultAsync(x => x.ArticleID == id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        // PUT: api/Article/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int id, Article article)
        {
            if (id != article.ArticleID)
            {
                return BadRequest();
            }

            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Article
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new { id = article.ArticleID }, article);
        }

        // DELETE: api/Article/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Article>> DeleteArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return article;
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.ArticleID == id);
        }
    }
}
