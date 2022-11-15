using Conectai.Models.Data;
using Conectai.Models.DB;
using Conectai.Properties;
using System;

namespace Conectai.Models.Negocio.Usuarios
{
	public class CmdPrepararEdicaoUsuario
	{
		//----------------------------------------------------------------------
		#region variáveis
		//----------------------------------------------------------------------
		public Usuario			Usuario					{ get; private set; }
		public string			MsgErro					{ get; private set; }

		private int				m_id;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
		public CmdPrepararEdicaoUsuario( int id )
		{
			m_id = id;
		}

		//----------------------------------------------------------------------
		#region funções public
		//----------------------------------------------------------------------
		public void execCmd( DBConexao db )
		{
			if ( m_id == Usuario.ID_USUARIO_INVALIDO )
			{
				Usuario = new Usuario();
			}
			else
			{
				Usuario umUsuario;
				if(UsuarioDB.lerUsuario( db, m_id, out umUsuario) )
				{
					if(umUsuario == null )
					{
						MsgErro = Mensagens.ERR_USUARIO_NAO_ENCONTRADO;
						return;
					}
					Usuario = umUsuario;
				}
				else
				{
					MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
					return;
				}
			}

			Usuario.ArrPerfil = UsuarioDB.lerPerfis(db);
			if (Usuario.ArrPerfil == null)
			{
				MsgErro = Mensagens.EXCEPTION_MSG_ERRO;
				return;
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}