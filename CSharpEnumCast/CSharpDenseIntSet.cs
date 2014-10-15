using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEnumCast
{
    public class CSharpDenseIntSet<TEnum> where TEnum: struct, IConvertible
    {
	    static CSharpDenseIntSet()
	    {
		    if (!typeof (TEnum).IsEnum)
		    {
				throw new ArgumentException("TEnum must be an enumerated type");
		    }
	    }

	    private readonly bool[] _items;

	    public CSharpDenseIntSet(IEnumerable<TEnum> values)
	    {
		    var length = Enum.GetValues(typeof (TEnum)).Cast<TEnum>().Count();
			_items = new bool[length];
		    foreach (var value in values)
		    {
			    var index = value.ToInt32(CultureInfo.InvariantCulture);
			    _items[index] = true;
		    }
	    }

	    public bool Contains(TEnum value)
	    {
		    var index = value.ToInt32(CultureInfo.InvariantCulture);
		    return _items[index];
	    }

	    public bool Contains2(int value)
	    {
		    return _items[value];
	    }
    }
}
