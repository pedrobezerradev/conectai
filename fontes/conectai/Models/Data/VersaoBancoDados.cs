
namespace DescomplicaCidadao.Models.Data
{
	public class VersaoBancoDados
	{
		public static int
			MAJOR_VERSION_DB = 1,
			MINOR_VERSION_DB = 0;

		public int Major	{ get; set; }
		public int Minor	{ get; set; }
		public int Revisao	{ get; set; }
	}
}