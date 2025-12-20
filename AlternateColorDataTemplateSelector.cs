using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySlideShow
{
    public class AlternateColorDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EvenTemplate { get; set; }
        public DataTemplate UnevenTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            // Assuming your CollectionView's ItemsSource is a List<YourDataType>
            // You might need to adjust the casting based on your actual data type and container
            var collectionView = (CollectionView)container;
            var items = (System.Collections.IList)collectionView.ItemsSource;
            var index = items.IndexOf(item);

            return index % 2 == 0 ? EvenTemplate : UnevenTemplate;
        }
    }
}
