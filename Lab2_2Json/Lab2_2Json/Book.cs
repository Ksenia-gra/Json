using System;
using System.Collections.Generic;

namespace Lab2_2Json;

public partial class Book
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public long CountPage { get; set; }

    public double? Price { get; set; }

    public virtual ICollection<Auth> Auths { get; } = new List<Auth>();
}
