using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ParkingSpace.Resources
{
  public class Grouping<K, T> : ObservableCollection<T>
  {
    public Grouping(K name, IEnumerable<T> items)
    {
      this.Name = name;

      foreach (T item in items)
      {
        Items.Add(item);
      }
    }
    public K Name { get; private set; }

  }
}
