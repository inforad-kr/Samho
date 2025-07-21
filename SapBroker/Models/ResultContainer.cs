using SapNwRfc;

namespace SapBroker.Models;

class ResultContainer<T>
{
    [SapName("ET_RESULT")]
    public T Result { get; set; }
}
