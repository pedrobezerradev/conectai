using System.Web;
using System.Web.Optimization;

namespace Conectai
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles( BundleCollection bundles )
		{
			bundles.Add( new ScriptBundle( "~/bundles/jquery" ).Include(
						"~/Scripts/jquery-{version}.js" ) );
			/*
			 * Parse, validate, manipulate, and display dates and times in JavaScript
			 * http://momentjs.com/
			 */
			bundles.Add( new ScriptBundle( "~/bundles/moment" ).Include(
					  "~/Scripts/moment-with-locales.js" ) );

			bundles.Add( new ScriptBundle( "~/bundles/bootstrap" ).Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/bootstrap-datetimepicker.js" ) );
		}
	}
}
