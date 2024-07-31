using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Weatherify.Services
{
    public class DoubleListConverter : ValueConverter<List<double>, string>
    {
        public DoubleListConverter() : base(
	  v => string.Join(",", v),
	  v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
	    .Select(double.Parse).ToList()) { }
    }
}
