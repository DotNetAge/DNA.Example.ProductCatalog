using DNA.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNA.Example.ProductCatalog
{
    public class  ProductCatalogRegisteration:SolutionModule
    {
        public override string Name
        {
            get { return "ProductCatalog"; }
        }

        public override void RegisterWidgets(IWidgetCollection widgets)
        {
            widgets.Register<ProductCatalogRegisteration>("Catalog", "Product catalog", category: "store");
        }
    }
}
