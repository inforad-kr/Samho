using SapNwRfc;

namespace SapBroker.Models;

class ArrayResultContainer<T>
{
    [SapName("ET_RESULT")]
    public T[] Result { get; set; }
}
