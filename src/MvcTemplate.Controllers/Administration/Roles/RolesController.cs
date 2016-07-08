using Microsoft.AspNetCore.Mvc;
using MvcTemplate.Components.Mvc;
using MvcTemplate.Objects;
using MvcTemplate.Services;
using MvcTemplate.Validators;
using System;

namespace MvcTemplate.Controllers.Administration
{
    [Area("Administration")]
    public class RolesController : ValidatedController<IRoleValidator, IRoleService>
    {
        public RolesController(IRoleValidator validator, IRoleService service)
            : base(validator, service)
        {
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View(Service.GetViews());
        }

        [HttpGet]
        public ViewResult Create()
        {
            RoleView role = new RoleView();
            Service.SeedPermissions(role);

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([BindExcludeId] RoleView role)
        {
            if (!Validator.CanCreate(role))
            {
                Service.SeedPermissions(role);

                return View(role);
            }

            Service.Create(role);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(Int32 id)
        {
            return NotEmptyView(Service.GetView(id));
        }

        [HttpGet]
        public ActionResult Edit(Int32 id)
        {
            return NotEmptyView(Service.GetView(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoleView role)
        {
            if (!Validator.CanEdit(role))
            {
                Service.SeedPermissions(role);

                return View(role);
            }

            Service.Edit(role);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(Int32 id)
        {
            return NotEmptyView(Service.GetView(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public RedirectToActionResult DeleteConfirmed(Int32 id)
        {
            Service.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
