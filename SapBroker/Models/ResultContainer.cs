using SapNwRfc;

namespace SapBroker.Models;

class ResultContainer<T>
{
    [SapName("E_RESULT")]
    public T Result { get; set; }
}
