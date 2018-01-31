using System;
using UnityEngine;

namespace PowerSystem
{
    public abstract class Stat
	{
		public string name;
		public string description;
		public abstract void SetFields();
	}
	public class Stat<T> : Stat
	{
		public T value;
		public Action<T> affectedFieldSetters;

		public Stat(string name, string description, Action<T> affectedFieldSetters)
		{
			this.name = name;
			this.description = description;
			this.affectedFieldSetters = affectedFieldSetters;
		}

		public override void SetFields()
		{
			affectedFieldSetters(value);
			Debug.Log("setando uns field");
		}
	}

    public abstract class PowerEffectBaseCreator <T>
    {
        public static string name;
        public static string description;
        public Stat[] stats;
        
        public T Instance { get; protected set; }
        public virtual T SetInstanceStats()
        {   
            foreach (Stat stat in stats)
            {
                stat.SetFields();
                Debug.Log(stat.name + ": " + stat.GetType().GetField("value").GetValue(stat));
            }
            return Instance;
        }
    }

    public abstract class PowerCreator : PowerEffectBaseCreator<Power> { }

    public abstract class EffectCreator : PowerEffectBaseCreator<Effect> { }

    public abstract class PowerCreator<PowerType, PowerControllerType> : PowerCreator where PowerType : Power where PowerControllerType : PowerController
	{
        public new PowerType Instance { get { return (PowerType)base.Instance; } protected set { base.Instance = value; } }
    }

    public abstract class EffectCreator<EffectType> : EffectCreator where EffectType : Effect
	{
        public new EffectType Instance { get { return (EffectType)base.Instance; } protected set { base.Instance = value; } }
    }
}