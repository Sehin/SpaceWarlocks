using System;

namespace Damage
{
    public enum DamageType
    {
        EXPLOSSION,
        INSTANT_KILL
    }

    public class DamageDealer
    {
        private Object source;
        private float ammount;
        private DamageType type;

        public DamageDealer(object source, float ammount, DamageType type)
        {
            this.source = source;
            this.ammount = ammount;
            this.type = type;
        }

        public object Source
        {
            get { return source; }
        }

        public float Ammount
        {
            get { return ammount; }
        }

        public DamageType Type
        {
            get { return type; }
        }
    }
}
