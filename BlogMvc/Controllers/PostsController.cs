using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogMvc.Data;
using BlogMvc.Models;
using BlogMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BlogMvc.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Posts.ToListAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.Include(x => x.Comentarios)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            var ViewModel = new PostViewModel{ Id = post.Id, Titulo = post.Titulo, Descricao = post.Descricao, Data = post.Data  };
            
            ViewModel.Comentarios = post.Comentarios.Select(x => new Comentarios { Comentario = x.Comentario, Id = x.Id }).ToList();
            return View(ViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel post)
        {

            if (ModelState.IsValid)
            {
                _context.Add(new Post { Titulo = post.Titulo, Descricao = post.Descricao, Data = post.Data });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var post = await _context.Posts.FindAsync(id);

            if (post == null) return NotFound();

            return View(new PostViewModel { 
                Titulo = post.Titulo,
                Descricao = post.Descricao,
                Data = post.Data
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var post = await _context.Posts.FindAsync(postViewModel.Id);
                    if(post==null) return NotFound();
                    post.Titulo = postViewModel.Titulo;
                    post.Descricao = postViewModel.Descricao;
                    post.Data = postViewModel.Data;

                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(postViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(postViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var post = await _context.Posts.FirstOrDefaultAsync(m => m.Id == id);

            if (post == null) return NotFound();

            return View(new PostViewModel
            {
                Id = post.Id,
                Titulo = post.Titulo,
                Descricao = post.Descricao,
                Data = post.Data
            });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
