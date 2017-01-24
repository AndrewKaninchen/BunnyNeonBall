using UnityEngine;
using System.Collections;

namespace PowerSystem
{	
	[PowerGenerator]
	public class ShootBeamInfoGenerator : PowerInfoGenerator<ShootBeamInfo>
	{
		public static new string powerClassName = "Shoot (Beam)";
		public ShootBeamInfoGenerator()
		{
			powerInfo = ScriptableObject.CreateInstance<ShootBeamInfo>();
			parameters = new MetaParameter[]
			{
				new MetaParameter("Range", rating => powerInfo.range = rating, "How far the beam goes."),				
			};
		}
	}
}
