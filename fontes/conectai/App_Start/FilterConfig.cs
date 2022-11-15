using System.Web;
using System.Web.Mvc;
using Conectai.Filters;

namespace Conectai
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters( GlobalFilterCollection filters )
		{
			filters.Add( new CustomHandleErrorAttribute() );
		}
	}
}