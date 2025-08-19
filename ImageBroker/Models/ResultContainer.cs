using SapNwRfc;

namespace ImageBroker.Models;

class ResultContainer<T>
{
    [SapName("ET_RESULT")]
    public T Result { get; set; }
}
