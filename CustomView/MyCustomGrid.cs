using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySlideShow
{
    public class MyCustomGrid : Grid
    {
        int counter = 0;    

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            // Custom logic can be added here if needed when the binding context changes    

            object context = this.BindingContext;
           

        }
    }
}
