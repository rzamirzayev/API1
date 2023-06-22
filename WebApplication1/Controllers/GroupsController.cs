using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : Controller
    {
        private readonly DataContext _context;

        public GroupsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetOne/{id}")]
        public ActionResult<Group> GetGroup(int id)
        {
            var data = _context.Groups.Find(id);
            if (data == null) return NotFound();
            return StatusCode(200, data);
        }
        [HttpGet("GetAll")]
        public ActionResult<List<Group>> GetAllGroup()
        {
            var data = _context.Groups.ToList();
            if (!data.Any()) return NotFound();
            return StatusCode(200, data);
        }
        [HttpPost("create")]
        public ActionResult Create(Group group)
        {
            var exitsGroup = _context.Groups.FirstOrDefault(x => x.Name == group.Name);
            if (exitsGroup != null)
            {
                ModelState.AddModelError("Name", "This name already in use.");
                return BadRequest(ModelState);
            }
            _context.Groups.Add(group);
            _context.SaveChanges();
            return StatusCode(200, group);
        }
        [HttpPost("delete")]
        public ActionResult Delete(int id)
        {
            var exitsGroup = _context.Groups.Find(id);
            if (exitsGroup == null) return NotFound();
            _context.Groups.Remove(exitsGroup);
            _context.SaveChanges();
            return StatusCode(200);
        }
        [HttpPost("edit")]
        public ActionResult Edit(Group group)
        {
            var existGroup = _context.Groups.Find(group.Id);
            if (existGroup == null) return NotFound();
            existGroup.Name = group.Name;
            _context.SaveChanges();
            return StatusCode(200);
        }
    }
}
