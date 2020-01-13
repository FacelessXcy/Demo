using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("sceneName", "playerPos", "currentHp", "diamondCount", "diamondsPosActive", "damageableWallsActive", "musicName")]
	public class ES3Type_Save : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_Save() : base(typeof(Save)){ Instance = this; }

		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (Save)obj;
			
			writer.WriteProperty("sceneName", instance.sceneName, ES3Type_string.Instance);
			writer.WriteProperty("playerPos", instance.playerPos, ES3Type_Vector3.Instance);
			writer.WriteProperty("currentHp", instance.currentHp, ES3Type_int.Instance);
			writer.WriteProperty("diamondCount", instance.diamondCount, ES3Type_int.Instance);
			writer.WriteProperty("diamondsPosActive", instance.diamondsPosActive);
			writer.WriteProperty("damageableWallsActive", instance.damageableWallsActive);
			writer.WriteProperty("musicName", instance.musicName, ES3Type_string.Instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (Save)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "sceneName":
						instance.sceneName = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "playerPos":
						instance.playerPos = reader.Read<UnityEngine.Vector3>(ES3Type_Vector3.Instance);
						break;
					case "currentHp":
						instance.currentHp = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "diamondCount":
						instance.diamondCount = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "diamondsPosActive":
						instance.diamondsPosActive = reader.Read<System.Collections.Generic.List<System.Boolean>>();
						break;
					case "damageableWallsActive":
						instance.damageableWallsActive = reader.Read<System.Collections.Generic.List<System.Boolean>>();
						break;
					case "musicName":
						instance.musicName = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new Save();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}

	public class ES3Type_SaveArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_SaveArray() : base(typeof(Save[]), ES3Type_Save.Instance)
		{
			Instance = this;
		}
	}
}