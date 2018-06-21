using ShopDiaryProject.Domain.ViewModels;
using ShopDiaryProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopDiaryApp.API.Controllers
{
    public class ConsumeController : Controller
    {
        private IConsumeRepository _consumeRepository;

        public CategoryController()
        {
            _categoryRepository = new CategoryRepository();
        }
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCategory(Guid id)
        {
            var cat = _categoryRepository.GetSingle(id);
            return Json(data:cat,behavior:JsonRequestBehavior.AllowGet);
        }
        
        //http://shopdiary.com/Category/AddData
        [HttpPost]
        public JsonResult AddData(CategoryViewModel vm)
        {
            var result = _categoryRepository.Add(vm.ToModel());
            return Json(data: result, behavior: JsonRequestBehavior.AllowGet);
        }
    }
}