using log4net;
using log4net.Appender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conectai.Models.Negocio.StatusApp
{
	public class CmdExibirStatusApp
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------
		private static readonly ILog logger = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

		public IList<string>	ArrNomesArqLog	{ get; private set; }
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------

		//----------------------------------------------------------------------
		public void execCmd()
		{
			ArrNomesArqLog = new List<string>();

			IAppender [] appenders = logger.Logger.Repository.GetAppenders();

			// Check each appender this logger has
			foreach( IAppender appender in appenders )
			{
				if( appender is FileAppender )
				{
					FileAppender fileAppender = (FileAppender)appender;
					ArrNomesArqLog.Add( fileAppender.File );
				}
			}
		}
	}
}