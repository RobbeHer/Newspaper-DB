using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularProjectAPI.Data;
using AngularProjectAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly NewsContext _context;

        public CommentController(NewsContext context)
        {
            _context = context;
        }

        // GET: api/Comment/article/5
        [HttpGet("article/{id}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetArticleComments(int id)
        {
            var comments = await _context.Comments.Include("User").Where(x => x.ArticleID == id).ToListAsync();

            if (comments == null)
            {
                return NotFound();
            }

            return comments;
        }

        // Post: api/Comment
        [HttpPost]
        public async Task<ActionResult<Comment>> PostArticle(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticleComments", new { id = comment.CommentID }, comment);
        }
    }
}
