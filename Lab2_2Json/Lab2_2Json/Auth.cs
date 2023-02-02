using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_2Json;

public partial class Auth
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long? Age { get; set; }

    public virtual ICollection<Book> Books { get; } = new List<Book>();
    public string BooksStr => string.Join(", ", Books.Select(x => x.Title));
}
