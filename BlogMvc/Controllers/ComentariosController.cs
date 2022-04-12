using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlogMvc.Data;
using BlogMvc.Models;
using BlogMvc.ViewModels;

namespace BlogMvc.Controllers
{
    [Authorize]
    public class ComentariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComentariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            if (postViewModel.Comentario == null) return View(postViewModel);
            _context.Comentarios.Add(new Comentarios() { Comentario = postViewModel.Comentario, PostId = postViewModel.Id });
            await _context.SaveChangesAsync();
            return RedirectToAction("Details","Posts", new { Id = postViewModel.Id });
        }

        public async Task<IActionResult> Delete(int? id)
        {

            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario == null) return View(comentario);
            int postid = comentario.PostId;
            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Posts", new { Id = postid });
            
        }
    }
}
