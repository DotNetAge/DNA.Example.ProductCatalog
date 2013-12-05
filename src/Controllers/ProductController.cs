using DNA.Data;
using DNA.Example.ProductCatalog.Models;
using DNA.Web;
using DNA.Web.ServiceModel;
using System.Web.Mvc;

namespace DNA.Example.ProductCatalog.Controllers
{
    public class ProductController : Controller
    {
        private App app = null;
        protected App AppContext
        {
            get
            {
                if (app == null)
                    app = App.Get();
                return app;
            }
        }

        protected IUnitOfWorks Storage { get { return AppContext.CurrentWeb.Storage; } }

        /// <summary>
        /// List all products from database.
        /// </summary>
        /// <returns></returns>
        [SiteDashboard(Text = "Products", Group = "Store")]
        public ActionResult List()
        {
            return View(Storage.All<Product>());
        }

        /// <summary>
        /// View the product by specified product id.
        /// </summary>
        /// <param name="id">The product id.</param>
        /// <returns></returns>
        [SiteDashboard]
        public ActionResult Detail(int id)
        {
            return View(Storage.Find<Product>(id));
        }

        /// <summary>
        /// New product form
        /// </summary>
        /// <returns></returns>
        [SiteDashboard]
        public ActionResult New() { return View(); }

        /// <summary>
        /// Edit the product detail by specfied id.
        /// </summary>
        /// <param name="id">The product id.</param>
        /// <returns></returns>
        [SiteDashboard]
        public ActionResult Edit(int id)
        {
            return View(Storage.Find<Product>(id));
        }

        /// <summary>
        /// Post the product detail and save to database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="foms"></param>
        /// <returns></returns>
        [HttpPost, SiteDashboard, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection foms)
        {
            if (ModelState.IsValid)
            {
                var product = Storage.Find<Product>(id);
                if (this.TryUpdateModel(product))
                {
                    Storage.Update(product);
                    Storage.SaveChanges();
                }
                return RedirectToAction("Detail", new { id = id });
            }
            return RedirectToAction("Edit", new { id = id });
        }

        /// <summary>
        /// Post the new product detail and save to database.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost, SiteDashboard, ValidateAntiForgeryToken]
        public ActionResult New(Product product)
        {
            if (ModelState.IsValid)
            {
                Storage.Add(product);
                Storage.SaveChanges();
                return RedirectToAction("Detail", new { id = product.ID });
            }
            return View();
        }

        [SiteDashboard,HttpPost]
        public ActionResult Delete(int id)
        {
            var product = Storage.Find<Product>(id);
            Storage.Delete(product);
            Storage.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
