using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShareMe.DAL.UnitOfWork;
using ShareMe.Models.Entity;

namespace ShareMe.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagController : Controller
    {
        IUnitOfWork unitOfWork;
        public TagController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var tags = unitOfWork.TagRepository.Get(x => true);
            return View(tags);
        }
        public IActionResult Update(int? tagId)
        {
            var tag = unitOfWork.TagRepository.GetById(tagId);
            if (tag == null)
            {
                tag = new Tag();                                
            }
            return View(tag);
        }

        [HttpPost]
        public IActionResult Update(Tag tag)
        {
            if (tag==null)
            {
                return BadRequest();
            }

            var tagToUpdate = unitOfWork.TagRepository.GetById(tag.TagId);
            if(tagToUpdate == null)
            {
                return BadRequest();
            }
            
            tagToUpdate.Name = tag.Name;

            try
            {                
                unitOfWork.TagRepository.Update(tagToUpdate);
                unitOfWork.Save();
                ViewBag.Message = "Update succeeded";
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "Update failed";
                return View(tag);
            }
        }

        public IActionResult Delete(int? tagId)
        {
            if (tagId==null)
            {
                return BadRequest();
            }
            try
            {
                unitOfWork.TagRepository.Delete((int)tagId);
                unitOfWork.Save();
                ViewBag.Message = "Delete succeeded";
            }
            catch
            {
                ViewBag.Message = "Update failed";                
            }
            return RedirectToAction("Index");
        }
    }
}